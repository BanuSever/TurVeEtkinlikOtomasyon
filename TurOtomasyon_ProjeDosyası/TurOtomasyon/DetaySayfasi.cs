using Npgsql;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TurOtomasyon.Helpers;

namespace TurOtomasyon
{
    public partial class DetaySayfasi : MetroFramework.Forms.MetroForm
    {
        public string drol;

        public string kulmail;
        public string TurId;
        public int kontrol = 1;
        

        public DetaySayfasi()
        {
            InitializeComponent();
        }
        

        private void DetaySayfasi_Load(object sender, EventArgs e)
        {
            if (kontrol == 0)
            {
                panel3.Visible = false;
                geri_btn.Visible = true;
            }else if (kontrol == 2)
            {
                panel3.Visible=false;
                geri_btn.Visible=false;
            }
            LoadTurDetay();
        }
        int turUcreti;
        private void LoadTurDetay()
        {
            if (drol == "ceo")
            {
                satin_al_btn.Visible = false;
            }

            string query = "SELECT tur_adi, tur_suresi, tur_lokasyon, tur_ucreti, tur_aciklama, foto_url, tur_duraklar, tur_tarihi FROM turlar WHERE tur_id = @tur_id";

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tur_id", int.Parse(TurId));

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sure_lbl.Text = reader["tur_suresi"].ToString();
                            lokasyon_lbl.Text = reader["tur_lokasyon"].ToString();
                            turUcreti = int.Parse(reader["tur_ucreti"].ToString());
                            aciklama_lbl.Text = reader["tur_aciklama"].ToString();
                            tur_durak.Text = reader["tur_duraklar"].ToString();
                            tur_ad_lbl.Text = reader["tur_adi"].ToString();

                            string turTarihiStr = reader["tur_tarihi"].ToString();
                            DateTime turTarihi;
                            if (DateTime.TryParseExact(turTarihiStr, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out turTarihi))
                            {
                                if (turTarihi < DateTime.Today)
                                {
                                    panel3.Visible = false;
                                    label8.Visible = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Geçersiz tarih formatı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            // foto yukleme
                            string fotoUrl = reader["foto_url"].ToString();
                            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            string fotoPath = Path.Combine(projectDirectory, fotoUrl);

                            if (File.Exists(fotoPath))
                            {
                                pictureBox1.Image = Image.FromFile(fotoPath);
                            }
                            else
                            {
                                MessageBox.Show("Resim dosyası bulunamadı: " + fotoPath, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bu tur bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }


        private void kisi_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int kisiSayisi = int.Parse(kisi_combo.SelectedItem.ToString());
            tur_ucret.Text=(turUcreti*kisiSayisi).ToString();
        }

        private void satin_al_btn_Click(object sender, EventArgs e)
        {
            if (kisi_combo.SelectedItem == null)
            {
                MessageBox.Show("Lütfen kişi sayısını seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            Odeme odeme = new Odeme();
            odeme.kullid = kulmail;
            odeme.turid = TurId;
            odeme.kisi = kisi_combo.SelectedItem.ToString();
            odeme.ucret = tur_ucret.Text;
            odeme.ShowDialog();
        }

        int geribtn = 0;
        public int kapama = 0;
        private void geri_btn_Click(object sender, EventArgs e)
        {
            geribtn = 1;
            
            this.Close();
            Rezervasyonlarim rezervasyonlarim=new Rezervasyonlarim(kulmail);
            rezervasyonlarim.Show();
        }

        private void DetaySayfasi_Resize(object sender, EventArgs e)
        {
            FormHelper.CenterPanel(this, panel1);
        }

        private void DetaySayfasi_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (geribtn != 1&& kapama!=1) {
                Anasayfa anasayfa = new Anasayfa();
                anasayfa.mail = kulmail;
                anasayfa.Show();
            
            }
        }

    }
}
