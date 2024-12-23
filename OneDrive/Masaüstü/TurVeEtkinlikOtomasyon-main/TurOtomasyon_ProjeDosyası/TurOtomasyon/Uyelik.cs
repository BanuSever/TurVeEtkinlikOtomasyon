using Npgsql;
using System;
using System.Drawing; 
using System.Linq;
using System.Text.RegularExpressions; 
using System.Windows.Forms;
using TurOtomasyon.Helpers;

namespace TurOtomasyon
{
    public partial class Uyelik : MetroFramework.Forms.MetroForm
    {
        public Uyelik()
        {
            InitializeComponent();
        }

        int kapama = 0;
        private void uyeol_btn_Click(object sender, EventArgs e)
        {
            string ad = ad_txt.Text.ToUpper();
            string soyad = soyad_txt.Text.ToUpper();
            string telno = telno_txt.Text; 
            string eposta = eposta_txt.Text;
            string parola = parola_txt.Text;
            string parola2 = parola2_txt.Text;

            if (string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(soyad) ||
                string.IsNullOrWhiteSpace(telno) || string.IsNullOrWhiteSpace(eposta) ||
                string.IsNullOrWhiteSpace(parola) || string.IsNullOrWhiteSpace(parola2))
            {
                MessageBox.Show("Tüm alanları doldurmanız gerekiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(telno) || telno.Contains("_")) 
            {
                MessageBox.Show("Lütfen telefon numaranızı eksiksiz girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (parola.Length < 6)
            {
                MessageBox.Show("Şifreniz en az 6 karakter uzunluğunda olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (parola != parola2)
            {
                MessageBox.Show("Şifreler eşleşmiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string epostaKontrol = "SELECT COUNT(*) FROM kullanicilar WHERE k_eposta = @eposta";
                    NpgsqlCommand epostaCmd = new NpgsqlCommand(epostaKontrol, conn);
                    epostaCmd.Parameters.AddWithValue("@eposta", eposta);

                    if (Convert.ToInt32(epostaCmd.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Bu e-posta adresi zaten kayıtlı! Lütfen başka bir e-posta kullanın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string telefonKontrol = "SELECT COUNT(*) FROM kullanicilar WHERE k_telefon = @telno";
                    NpgsqlCommand telefonCmd = new NpgsqlCommand(telefonKontrol, conn);
                    telefonCmd.Parameters.AddWithValue("@telno", telno); 

                    if (Convert.ToInt32(telefonCmd.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Bu telefon numarası zaten kayıtlı! Lütfen başka bir numara kullanın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string kayitKomutu = "INSERT INTO kullanicilar(k_adi, k_soyadi, k_sifre, k_eposta, k_telefon, k_rol) " +
                                         "VALUES(@ad, @soyad, @parola, @eposta, @telno, @rol)";
                    NpgsqlCommand kayitCmd = new NpgsqlCommand(kayitKomutu, conn);
                    kayitCmd.Parameters.AddWithValue("@ad", ad);
                    kayitCmd.Parameters.AddWithValue("@soyad", soyad);
                    kayitCmd.Parameters.AddWithValue("@parola", parola);
                    kayitCmd.Parameters.AddWithValue("@eposta", eposta);
                    kayitCmd.Parameters.AddWithValue("@telno", telno); 
                    kayitCmd.Parameters.AddWithValue("@rol", "uye");

                    kayitCmd.ExecuteNonQuery();

                    MessageBox.Show("Başarıyla kayıt oldunuz.", "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    kapama = 1;
                    this.Close();
                    Tanitim tanitim = new Tanitim();
                    tanitim.Show();
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
            this.Close();
            KullaniciGiris kullaniciGiris = new KullaniciGiris();
            kullaniciGiris.Show();
        }

        private void parola_txt_TextChanged(object sender, EventArgs e)
        {
            string parola = parola_txt.Text;
            int strength = 0;
            if (parola.Length == 0)
            {
                
                strength = 0;
            }
            else if (parola.Length < 6 )
            {
                strength = 1;  
            }
            else
            {
                bool hasLetter = parola.Any(c => Char.IsLetter(c));  
                bool hasDigit = parola.Any(c => Char.IsDigit(c));  
                bool hasSymbol = parola.Any(c => !Char.IsLetterOrDigit(c)); 

                if (hasLetter && hasDigit && parola.Length > 10 && hasSymbol)
                {
                    strength = 4;  
                }
                else if (hasLetter && hasDigit && parola.Length > 6)
                {
                    strength = 3;  
                }
                else if (hasLetter || hasDigit)
                {
                    strength = 2;  
                }
                else
                {
                    strength = 1;  
                }
            }
            UpdatePasswordStrength(strength);
        }

        private void UpdatePasswordStrength(int strength)
        {
            switch (strength)
            {
                case 0:
                    s_uyari_lbl.Text = "";
                    break;
                case 1:
                    s_uyari_lbl.Text = "Zayıf Şifre";
                    s_uyari_lbl.ForeColor = Color.Red;
                    break;
                case 2:
                    s_uyari_lbl.Text = "Orta Şifre";
                    s_uyari_lbl.ForeColor = Color.Yellow;
                    break;
                case 4:
                    s_uyari_lbl.Text = "Güçlü Şifre";
                    s_uyari_lbl.ForeColor = Color.Green;
                    break;
            }
        }

        private void Uyelik_Load(object sender, EventArgs e)
        {
            ad_txt.Focus();
            ad_txt.TabIndex = 0;
            soyad_txt.TabIndex = 1;
            telno_txt.TabIndex = 2;
            eposta_txt.TabIndex = 3;
            parola_txt.TabIndex = 4;
            parola2_txt.TabIndex = 5;

            parola_txt.UseSystemPasswordChar = true;
            parola2_txt.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (parola_txt.UseSystemPasswordChar && parola2_txt.UseSystemPasswordChar == true)
            {
                parola_txt.UseSystemPasswordChar = false;
                parola2_txt.UseSystemPasswordChar = false;
            }
            else
            {
                parola_txt.UseSystemPasswordChar = true;
                parola2_txt.UseSystemPasswordChar = true;
            }
        }


        private void eposta_txt_Leave(object sender, EventArgs e)
        {
            string epostaPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(eposta_txt.Text, epostaPattern))
            {
                e_uyari_lbl.Text = "Geçersiz e-posta formatı!";
                e_uyari_lbl.ForeColor = Color.Red;
                return;
            }
            else
            {
                e_uyari_lbl.Text = "";
            }
        }

        private void parola2_txt_TextChanged(object sender, EventArgs e)
        {
            if (parola_txt.Text != parola2_txt.Text)
            {
                esles_lbl.Text = "Şifreler uyuşmuyor!";
                esles_lbl.ForeColor = Color.Red;
                return;
            }
            else
            {
                esles_lbl.Text = "";
            }
        }

        private void Uyelik_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (kapama != 1)
            {
                Tanitim tanitim = new Tanitim();
                tanitim.Show();
            }
        }
    }
}
