using Npgsql;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TurOtomasyon.Helpers;

namespace TurOtomasyon
{
    public partial class Odeme : MetroFramework.Forms.MetroForm
    {
        public string turid;
        public string kullid;
        public string ucret;
        public string kisi;

        public Odeme()
        {
            InitializeComponent();
        }
        private string GetKullaniciId(string eposta)
        {
            string query = "SELECT k_id FROM kullanicilar WHERE k_eposta = @eposta";
            string kullaniciId = null;

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@eposta", eposta);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            kullaniciId = result.ToString(); 
                        }
                        else
                        {
                            MessageBox.Show("Bu e-posta adresine ait kullanıcı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return kullaniciId;
        }

        private void ValidateAndSaveReservation()
        {
            if (string.IsNullOrWhiteSpace(ad_txt.Text))
            {
                MessageBox.Show("Ad alanı boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ad_txt.Focus();
                return;
            }

            string kartNumarasi = kartno_txt.Text.Replace(" ", "");
            if (string.IsNullOrWhiteSpace(kartNumarasi) || kartNumarasi.Length != 16)
            {
                MessageBox.Show("Geçerli bir kart numarası girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kartno_txt.Focus();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(cvv_txt.Text) || cvv_txt.Text.Length != 3)
            {
                MessageBox.Show("CVV 3 haneli olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cvv_txt.Focus();
                return;
            }

            if (ay_combo.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir ay seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ay_combo.Focus();
                return;
            }

            if (yil_combo.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir yıl seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                yil_combo.Focus();
                return;
            }

            if (!onay_box.Checked)
            {
                MessageBox.Show("Devam etmek için şartları kabul etmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                onay_box.Focus();
                return;
            }

            string query = "INSERT INTO rezervasyon (r_turid, r_kulid, r_kisisayisi, r_tarih, r_odeme) VALUES (@turid, @kulid, @kisisayisi, @tarih, @odeme)";

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        // turid değerini inte 
                        if (!int.TryParse(turid, out int turIdInt))
                        {
                            MessageBox.Show("Geçersiz Tur ID.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string eposta = kullid; // 
                        string kulIdStr = GetKullaniciId(eposta);

                        if (string.IsNullOrWhiteSpace(kulIdStr))
                        {
                            MessageBox.Show("Geçersiz Kullanıcı ID.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        int kulIdInt = int.Parse(kulIdStr);

                        cmd.Parameters.AddWithValue("@turid", turIdInt);
                        cmd.Parameters.AddWithValue("@kulid", kulIdInt);
                        cmd.Parameters.AddWithValue("@kisisayisi", int.Parse(kisi));
                        cmd.Parameters.AddWithValue("@tarih", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                        cmd.Parameters.AddWithValue("@odeme", int.Parse(ucret));

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Rezervasyon başarılı bir şekilde kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Rezervasyon kaydedilemedi. Lütfen tekrar deneyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Odeme_Load(object sender, EventArgs e)
        {
            kartno_txt.Mask = "0000 0000 0000 0000";
            kartno_txt.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            ucret_lbl.Text = ucret;
            ad_txt.TabIndex = 0;
            kartno_txt.TabIndex = 1;
            cvv_txt.TabIndex=2;
            ay_combo.TabIndex=3;
            yil_combo.TabIndex=4;
            onay_box.TabIndex=5;
        }

        private void uyeol_btn_Click(object sender, EventArgs e)
        {
            ValidateAndSaveReservation();
        }

        private void cvv_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled=!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ad_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; 
            }
        }
    }
}
