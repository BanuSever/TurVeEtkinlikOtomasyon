using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurOtomasyon.Helpers;

namespace TurOtomasyon
{
    public partial class BilgiDuzenleme : MetroFramework.Forms.MetroForm
    {
        public string bilgimail;
        public string rolb;
        public BilgiDuzenleme()
        {
            InitializeComponent();
        }
        public string eposta;
        private void BilgiDuzenleme_Load(object sender, EventArgs e)
        {
            ad_txt.Focus();
            ad_txt.TabIndex = 0;
            soyad_txt.TabIndex = 1;
            telno_txt.TabIndex = 2;
            eposta_txt.TabIndex = 3;
            parola_txt.TabIndex = 4;
            parola_txt.UseSystemPasswordChar = true;

            if (string.IsNullOrEmpty(bilgimail))
            {
                MessageBox.Show("E-posta bilgisi eksik!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string komut = "SELECT k_adi, k_soyadi, k_sifre, k_eposta, k_telefon FROM kullanicilar WHERE k_eposta = @mail";
                    NpgsqlCommand cmd = new NpgsqlCommand(komut, conn);
                    cmd.Parameters.AddWithValue("@mail", bilgimail);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ad_txt.Text = reader["k_adi"].ToString();
                            soyad_txt.Text = reader["k_soyadi"].ToString();
                            parola_txt.Text = reader["k_sifre"].ToString();
                            eposta_txt.Text = reader["k_eposta"].ToString();
                            telno_txt.Text = reader["k_telefon"].ToString();
                            eposta=eposta_txt.Text;
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı bilgileri bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void kaydet_btn_Click(object sender, EventArgs e)
        {
            
            string eposta = eposta_txt.Text;            
            string telno = telno_txt.Text;

            if (string.IsNullOrWhiteSpace(telno) || telno.Contains("_"))
            {
                MessageBox.Show("Telefon numarası eksik veya hatalı girildi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           
            string epostaPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(eposta, epostaPattern))
            {
                MessageBox.Show("Geçersiz e-posta formatı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (parola_txt.Text.Length < 6)
            {
                MessageBox.Show("Şifre en az 6 karakter olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string telnoKontrolQuery = "SELECT COUNT(*) FROM kullanicilar WHERE k_telefon = @telno AND k_eposta != @mail";
                    using (NpgsqlCommand telnoCmd = new NpgsqlCommand(telnoKontrolQuery, conn))
                    {
                        telnoCmd.Parameters.AddWithValue("@telno", telno);
                        telnoCmd.Parameters.AddWithValue("@mail", bilgimail);

                        int telCount = Convert.ToInt32(telnoCmd.ExecuteScalar());
                        if (telCount > 0)
                        {
                            MessageBox.Show("Bu telefon numarası başka bir kullanıcı tarafından kullanılıyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string epostaKontrolQuery = "SELECT COUNT(*) FROM kullanicilar WHERE k_eposta = @eposta AND k_eposta != @mail";
                    using (NpgsqlCommand epostaCmd = new NpgsqlCommand(epostaKontrolQuery, conn))
                    {
                        epostaCmd.Parameters.AddWithValue("@eposta", eposta);
                        epostaCmd.Parameters.AddWithValue("@mail", bilgimail);

                        int epostaCount = Convert.ToInt32(epostaCmd.ExecuteScalar());
                        if (epostaCount > 0)
                        {
                            MessageBox.Show("Bu e-posta adresi başka bir kullanıcı tarafından kullanılıyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string updateQuery = "UPDATE kullanicilar SET k_adi = @ad, k_soyadi = @soyad, k_sifre = @parola, k_eposta = @eposta, k_telefon = @telno WHERE k_eposta = @mail";
                    using (NpgsqlCommand updateCmd = new NpgsqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@ad", ad_txt.Text.ToUpper());
                        updateCmd.Parameters.AddWithValue("@soyad", soyad_txt.Text.ToUpper());
                        updateCmd.Parameters.AddWithValue("@parola", parola_txt.Text);
                        updateCmd.Parameters.AddWithValue("@eposta", eposta);
                        updateCmd.Parameters.AddWithValue("@telno", telno);
                        updateCmd.Parameters.AddWithValue("@mail", bilgimail);

                        int result = updateCmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Bilgiler başarıyla güncellendi.", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Anasayfa anasayfa = new Anasayfa();
                            anasayfa.mail = eposta;
                            anasayfa.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Bilgiler güncellenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public int kapama = 0;
        private void geri_btn_Click(object sender, EventArgs e)
        {
            kapama = 1;
            this.Close();
            
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.rol = rolb;
            anasayfa.mail = eposta;
            
            anasayfa.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (parola_txt.UseSystemPasswordChar == true)
            {
                parola_txt.UseSystemPasswordChar = false;
            }
            else
            {
                parola_txt.UseSystemPasswordChar = true;
            }
        }
        private void BilgiDuzenleme_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (kapama == 0)
            {
                Anasayfa anasayfa = new Anasayfa();
                anasayfa.rol = rolb;
                anasayfa.mail = eposta;
                anasayfa.Show();
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            FormHelper.CenterPanel(this, panel1);
        }

        private void telno_txt_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void telno_txt_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        

        private void parola_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
