using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
using TurOtomasyon.Helpers;

namespace TurOtomasyon
{
    public partial class RezervasyonlariGor : MetroFramework.Forms.MetroForm
    {
        public RezervasyonlariGor()
        {
            InitializeComponent();
        }

        private void RezervasyonlariGor_Load(object sender, EventArgs e)
        {
            LoadRezervasyonlar();
        }
        int kapama = 0;
        public string ceomail;

        private void LoadRezervasyonlar()
        {
            string query = @"
                SELECT 
                    r.r_id AS RezervasyonID,
                    t.tur_adi AS TurAdi,
                    CONCAT(k.k_adi, ' ', k.k_soyadi) AS Kullanici,
                    r.r_kisisayisi AS KisiSayisi,
                    r.r_odeme AS Odeme,
                    r.r_tarih AS Tarih
                FROM 
                    rezervasyon r
                JOIN 
                    turlar t ON r.r_turid = t.tur_id
                JOIN 
                    kullanicilar k ON r.r_kulid = k.k_id";

            string query2 = @"
                SELECT 
                    k_id AS kID, 
                    k_adi AS kAdi,  
                    k_soyadi AS kSoyadi, 
                    k_eposta AS kEposta, 
                    k_telefon AS kTelefon
                FROM 
                    kullanicilar
                WHERE 
                    k_rol = 'uye'";

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                        dataGridView1.Columns["RezervasyonID"].HeaderText = "Rezervasyon No";
                        dataGridView1.Columns["TurAdi"].HeaderText = "Tur Adı";
                        dataGridView1.Columns["Kullanici"].HeaderText = "Kullanıcı Adı";
                        dataGridView1.Columns["KisiSayisi"].HeaderText = "Kişi Sayısı";
                        dataGridView1.Columns["Odeme"].HeaderText = "Ödeme Tutarı";
                        dataGridView1.Columns["Tarih"].HeaderText = "Tarih";
                    }

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query2, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView2.DataSource = dt;

                        dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                        dataGridView2.Columns["kID"].HeaderText = "Üye ID";
                        dataGridView2.Columns["kAdi"].HeaderText = "Adı";
                        dataGridView2.Columns["kSoyadi"].HeaderText = "Soyadı";
                        dataGridView2.Columns["kEposta"].HeaderText = "E-Posta";
                        dataGridView2.Columns["kTelefon"].HeaderText = "Telefon";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void geri_btn_Click(object sender, EventArgs e)
        {
            kapama = 1;
            this.Hide();
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.rol = "ceo";
            anasayfa.mail=ceomail;
            anasayfa.Show();
            
        }

        private void RezervasyonlariGor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(kapama!=1)
            {
                Anasayfa anasayfa=new Anasayfa();
                anasayfa.rol = "ceo";
                anasayfa.mail = ceomail;
                anasayfa.Show();
            }
        }

        private void tur_filtre_txt_TextChanged(object sender, EventArgs e)
        {
            string filterText = tur_filtre_txt.Text.ToLower();

            DataTable dataTable = dataGridView1.DataSource as DataTable;

            if (dataTable != null)
            {
                if (string.IsNullOrWhiteSpace(filterText))
                {
                    dataTable.DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    dataTable.DefaultView.RowFilter = string.Format("TurAdi LIKE '{0}%'", filterText.Replace("'", "''"));
                }
            }
        }

        private void uye_filtre_txt_TextChanged(object sender, EventArgs e)
        {
            string filterText = uye_filtre_txt.Text.ToUpper();

            DataTable dataTable = dataGridView2.DataSource as DataTable;

            if (dataTable != null)
            {
                if (string.IsNullOrWhiteSpace(filterText))
                {
                    dataTable.DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    dataTable.DefaultView.RowFilter = string.Format("kAdi LIKE '{0}%'", filterText.Replace("'", "''"));
                }
            }
        }

    }
}
