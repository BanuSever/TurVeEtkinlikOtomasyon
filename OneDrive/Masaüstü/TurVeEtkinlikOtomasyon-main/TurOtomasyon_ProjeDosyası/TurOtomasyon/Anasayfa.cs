using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurOtomasyon.Helpers;

namespace TurOtomasyon
{
    public partial class Anasayfa : MetroFramework.Forms.MetroForm
    {
        int kontrol = 1;
        public string mail;
        public string rol;
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void LoadTurlar()
        {

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT tur_id, tur_adi, tur_tarihi, tur_ucreti, tur_lokasyon, foto_url FROM turlar WHERE t_kontrol = true OR t_kontrol IS NULL ";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dataGridView1.Rows.Clear();

                    bool dataExists = false;

                    while (reader.Read())
                    {
                        dataExists = true;

                        // foto yolu
                        string fotoUrl = reader["foto_url"].ToString();
                        string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        string fotoPath = Path.Combine(projectDirectory, fotoUrl);

                        // foto dosyası kontrol edildi ve yeniden boyutlandırıldı
                        Image resim = null;
                        if (File.Exists(fotoPath))
                        {
                            try
                            {
                                resim = Image.FromFile(fotoPath);

                                int maxWidth = dataGridView1.Columns["fotoColumn"].Width;
                                int maxHeight = 150;
                                double ratioX = (double)maxWidth / resim.Width;
                                double ratioY = (double)maxHeight / resim.Height;
                                double ratio = Math.Min(ratioX, ratioY);
                                int newWidth = (int)(resim.Width * ratio);
                                int newHeight = (int)(resim.Height * ratio);
                                resim = new Bitmap(resim, newWidth, newHeight);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Resim yüklenemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Resim dosyası bulunamadı: " + fotoPath, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        dataGridView1.Rows.Add(reader["tur_id"], resim,  reader["tur_lokasyon"], reader["tur_adi"], reader["tur_tarihi"], reader["tur_ucreti"], reader["tur_lokasyon"]);
                    }

                    if (!dataExists)
                    {
                        MessageBox.Show("Veri bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void filtre_txt_TextChanged(object sender, EventArgs e)
        {
            string filterText = filtre_txt.Text.ToLower(); 

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["tur_lokasyon"].Value != null)
                {
                    string lokasyon = row.Cells["tur_lokasyon"].Value.ToString().ToLower();

                    if (lokasyon.StartsWith(filterText))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            DataGridViewTextBoxColumn turIdColumn = new DataGridViewTextBoxColumn();
            turIdColumn.HeaderText = "Tur ID";
            turIdColumn.Name = "turIdColumn";
            turIdColumn.Visible = false;
            dataGridView1.Columns.Add(turIdColumn);

            // foto sütunu
            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
            imgColumn.HeaderText = "Fotoğraf";
            imgColumn.Name = "fotoColumn";
            dataGridView1.Columns.Add(imgColumn);
            dataGridView1.Columns.Add("tur_lokasyon", "Lokasyon");
            dataGridView1.Columns.Add("tur_adi", "Tur Adı");
            dataGridView1.Columns.Add("tur_tarihi", "Tur Tarihi");
            dataGridView1.Columns.Add("tur_ucreti", "Tur Ücreti");

            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Detaylar";
            btnColumn.Text = "Detaylar";
            btnColumn.Name = "detaylarButton";
            btnColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnColumn);

            LoadTurlar();

            if (rol == "admin")
            {
                admin_panel.Visible = true;
                kullanici_panel.Visible = false;
                kontrol = 2;
            }
            else
            {
                admin_panel.Visible = false;
            }

            if (rol == "ceo")
            {
                rezervasyon_gor_btn.Visible = true;
                kullanici_panel.Visible=false;
                kontrol = 2;
            }
            else
            {
                rezervasyon_gor_btn.Visible = false;
            }
        }

        private void bilgi_duzenle_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            BilgiDuzenleme bilgiDuzenleme = new BilgiDuzenleme();
            bilgiDuzenleme.bilgimail = mail;
            bilgiDuzenleme.rolb = rol;
            bilgiDuzenleme.kapama = 0;
            bilgiDuzenleme.Show();
        }

        private void rezervasyon_gor_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            RezervasyonlariGor rezervasyonlariGor = new RezervasyonlariGor();
            rezervasyonlariGor.ceomail = mail;
            rezervasyonlariGor.Show();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
                return; // gecersiz satir

            // satirdaki verilerin kontrolu
            var turAdiCell = dataGridView1.Rows[e.RowIndex].Cells["tur_adi"];
            if (turAdiCell.Value == null || string.IsNullOrEmpty(turAdiCell.Value.ToString()))
                return; // null veri var mi?

            if (e.ColumnIndex == dataGridView1.Columns["detaylarButton"].Index)
            {
                string turId = dataGridView1.Rows[e.RowIndex].Cells["turIdColumn"].Value.ToString();
                
                DetaySayfasi detaySayfasi = new DetaySayfasi();
                detaySayfasi.TurId = turId;
                detaySayfasi.kulmail = mail;
                detaySayfasi.drol = rol;
                detaySayfasi.kontrol = kontrol;
                detaySayfasi.kapama = 1;
                detaySayfasi.Show();
            }
        }

        private void ekle_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            TurEkle turekle = new TurEkle();
            turekle.trol=rol;
            turekle.tmail=mail;
            turekle.kapama=0;
            turekle.Show();

        }

        private void guncelle_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string turId = dataGridView1.SelectedRows[0].Cells["turIdColumn"].Value.ToString();

                TurGuncelle turGuncelle = new TurGuncelle();
                turGuncelle.TurId = turId; 
                turGuncelle.grol=rol;
                turGuncelle.gmail=mail;
                turGuncelle.kapama = 0;
                turGuncelle.Show();
                this.Hide(); 
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz bir turu seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rez_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            string kullaniciMail = mail;
            Rezervasyonlarim rezervasyonlarim = new Rezervasyonlarim(kullaniciMail);
            rezervasyonlarim.kapama=0;
            rezervasyonlarim.Show();

        }

        private void sil_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string sturId = dataGridView1.SelectedRows[0].Cells["turIdColumn"].Value.ToString();
                DialogResult result = MessageBox.Show("Seçtiğiniz turu silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //string connString = "server=localhost; port=5432; Database=Tur; user Id=postgres; password=123";
                    string query = "update turlar set t_kontrol=@kontrol where tur_id=@turid";
                    bool kontrol=false;

                    using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
                    {
                        try
                        {
                            conn.Open();
                            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@turid", int.Parse(sturId));
                                cmd.Parameters.AddWithValue("@kontrol",kontrol);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Tur başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadTurlar(); 
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz bir tur seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SortDataGridView(string columnName, bool isAscending)
        {
            if (isAscending)
            {
                dataGridView1.Sort(dataGridView1.Columns[columnName], ListSortDirection.Ascending);
            }
            else
            {
                dataGridView1.Sort(dataGridView1.Columns[columnName], ListSortDirection.Descending);
            }
        }

        private void giris_btn_Click(object sender, EventArgs e)
        {
            string selectedFilter = filtre_combo.SelectedItem?.ToString();
            bool artan = artan_radio.Checked; 
            bool azalan = azalan_radio.Checked; 

            if (!artan && !azalan)
            {
                MessageBox.Show("Lütfen sıralama için bir seçenek seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            if (string.IsNullOrEmpty(selectedFilter))
            {
                MessageBox.Show("Lütfen filtreleme için bir seçenek seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (selectedFilter)
            {
                case "Tur Adı":
                    SortDataGridView("tur_adi", artan);
                    break;
                case "Ücret":
                    SortDataGridView("tur_ucreti",  artan);
                    break;
                case "Şehir":
                    SortDataGridView("tur_lokasyon", artan);
                    break;
                case "Tarih":
                    SortDataGridView("tur_tarihi", artan);
                    break;
                default:
                    MessageBox.Show("Geçersiz filtre seçeneği.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void cikis_btn_Click(object sender, EventArgs e)
        {
            
            this.Close();
            Tanitim tanitim = new Tanitim();
            tanitim.kapama = 0;
            tanitim.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}