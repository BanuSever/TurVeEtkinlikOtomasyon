using Npgsql;
using System;
using System.Data;
using System.Windows.Controls;
using System.Windows.Forms;
using TurOtomasyon.Helpers;

namespace TurOtomasyon
{
    public partial class Rezervasyonlarim : MetroFramework.Forms.MetroForm
    {
        public string kullaniciMail; 

        public Rezervasyonlarim(string kullaniciMail)
        {
            InitializeComponent();
            this.kullaniciMail = kullaniciMail; 
        }


        private void Rezervasyonlarim_Load(object sender, EventArgs e)
        {
            int kullaniciId = GetKullaniciId(kullaniciMail);
            if (kullaniciId > 0)
            {
                LoadRezervasyonlar(kullaniciId);
            }
            else
            {
                MessageBox.Show("Kullanıcı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetKullaniciId(string email)
        {
            string query = "SELECT k_id FROM kullanicilar WHERE k_eposta = @email";

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return -1; 
        }

        private bool buttonsAdded = false; 

        private void LoadRezervasyonlar(int kullaniciId)
        {
            string query = @"SELECT t.tur_id, t.tur_adi, t.tur_suresi, t.tur_tarihi, r.r_id, r.r_kisisayisi, r.r_odeme
                     FROM turlar t
                     JOIN rezervasyon r ON t.tur_id = r.r_turid
                     WHERE r.r_kulid = @kulid AND (r.r_kontrol = true OR r.r_kontrol IS NULL)";

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@kulid", kullaniciId);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        dataGridView1.DataSource = dt;

                        dataGridView1.Columns["tur_adi"].HeaderText = "Tur Adı";
                        dataGridView1.Columns["tur_suresi"].HeaderText = "Tur Süresi";
                        dataGridView1.Columns["tur_tarihi"].HeaderText = "Tur Tarihi";
                        dataGridView1.Columns["r_kisisayisi"].HeaderText = "Kişi Sayısı";
                        dataGridView1.Columns["r_odeme"].HeaderText = "Ödeme";

                        dataGridView1.Columns["tur_id"].Visible = false;
                        dataGridView1.Columns["r_id"].Visible = false;

                        if (!buttonsAdded)
                        {
                            DataGridViewButtonColumn iptalButton = new DataGridViewButtonColumn();
                            iptalButton.Name = "Iptal";
                            iptalButton.HeaderText = "İptal";
                            iptalButton.Text = "İptal Et";
                            iptalButton.UseColumnTextForButtonValue = true;
                            dataGridView1.Columns.Add(iptalButton);

                            DataGridViewButtonColumn detaylarButton = new DataGridViewButtonColumn();
                            detaylarButton.Name = "Detaylar";
                            detaylarButton.HeaderText = "Detaylar";
                            detaylarButton.Text = "Detayları Gör";
                            detaylarButton.UseColumnTextForButtonValue = true;
                            dataGridView1.Columns.Add(detaylarButton);

                            buttonsAdded = true;
                        }
                    }
                }
            }
        }


            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Iptal"].Index)
            {
                var result = MessageBox.Show("Emin misiniz? Bu rezervasyonu iptal edeceksiniz.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int rezervasyonId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["r_id"].Value);
                    int turId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["tur_id"].Value);

                    DeleteRezervasyonAndPayment(rezervasyonId, turId);
                    MessageBox.Show("Rezervasyon Başarıyla İptal Edildi. Ücret 7 İş Günü İçerisinde İade Edilecektir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int kullaniciId = GetKullaniciId(kullaniciMail);
                    if (kullaniciId > 0)
                    {
                        LoadRezervasyonlar(kullaniciId); 
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (e.ColumnIndex == dataGridView1.Columns["Detaylar"].Index)
            {
                kapama = 1;
                DetaySayfasi detaySayfasi = new DetaySayfasi();
                detaySayfasi.kontrol = 0;
                detaySayfasi.kulmail = kullaniciMail;
                detaySayfasi.TurId= Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["tur_id"].Value);
                this.Close();
                detaySayfasi.Show();
            }
        }

        private void DeleteRezervasyonAndPayment(int rezervasyonId, int turId)
        {
            bool kontrol = false;
            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                using (NpgsqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string deletePaymentQuery = "update odemeler set o_kontrol=@kontrol  WHERE o_rezervasyonid = @rezervasyonId";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(deletePaymentQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@rezervasyonId", rezervasyonId);
                            cmd.Parameters.AddWithValue("@kontrol", kontrol);
                            cmd.ExecuteNonQuery();
                        }

                        string deleteReservationQuery = "update  rezervasyon set r_kontrol=@kontrol WHERE r_id = @rezervasyonId";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(deleteReservationQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@rezervasyonId", rezervasyonId);
                            cmd.Parameters.AddWithValue("@kontrol", kontrol);
                            cmd.ExecuteNonQuery();
                        }

                        // transactionu bitir
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // hata varsa transactionu geri al
                        transaction.Rollback();
                        MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public int kapama = 0;
        private void geri_btn_Click(object sender, EventArgs e)
        {
            kapama = 1;
            this.Close();
            
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.mail = kullaniciMail;
            anasayfa.Show();
        }

        private void Rezervasyonlarim_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if(kapama!=1)
            {
                Anasayfa anasayfa = new Anasayfa();
                anasayfa.mail = kullaniciMail;
                anasayfa.Show();
            }
            
        }
    }
}
