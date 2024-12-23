using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;
using TurOtomasyon.Helpers;

namespace TurOtomasyon
{
    public partial class KullaniciGiris : MetroFramework.Forms.MetroForm
    {
        public KullaniciGiris()
        {
            InitializeComponent();
        }

        Uyelik uyelik = new Uyelik();
        int kapama = 0;

        private void KullaniciGiris_Load(object sender, EventArgs e)
        {
            sifre_txt.UseSystemPasswordChar = true;
        }

        private void KullaniciGiris_Resize(object sender, EventArgs e)
        {
            FormHelper.CenterPanel(this, panel1);
        }

        private void uyelik_btn_Click(object sender, EventArgs e)
        {
            kapama = 1;
            uyelik.Show();
            this.Hide();
        }

        private void giris_btn_Click(object sender, EventArgs e)
        {
            string eposta = eposta_txt.Text; 
            string parola = sifre_txt.Text; 

           
            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                   
                    string komut = "SELECT k_rol FROM kullanicilar WHERE k_eposta = @eposta AND k_sifre = @parola";
                    NpgsqlCommand komut2 = new NpgsqlCommand(komut, conn);
                    komut2.Parameters.AddWithValue("@eposta", eposta);
                    komut2.Parameters.AddWithValue("@parola", parola);

                    object rolObj = komut2.ExecuteScalar();

                    if (rolObj != null) 
                    {

                        string kullaniciRol = rolObj.ToString();
                        if (kullaniciRol == "muhasebe")
                        {
                            MessageBox.Show("Giriş başarılı!", "Giriş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            kapama = 1;
                            this.Close();
                            Muhasebe muhasebe = new Muhasebe();
                            muhasebe.Show();
                            
                        }else
                        {
                            MessageBox.Show("Giriş başarılı!", "Giriş", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            kapama = 1;
                            this.Close();
                            Anasayfa anasayfa = new Anasayfa
                            {
                                mail = eposta,
                                rol = kullaniciRol
                            };
                            anasayfa.Show();
                        }
                        

                    }
                    else
                    {
                        
                        MessageBox.Show("Geçersiz e-posta veya şifre!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            this.Hide();
            kapama = 1;
            Tanitim tanitim = new Tanitim();
            tanitim.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sifre_txt.UseSystemPasswordChar == true)
            {
                sifre_txt.UseSystemPasswordChar = false;
            }
            else
            {
                sifre_txt.UseSystemPasswordChar = true;
            }
        }

        private void KullaniciGiris_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (kapama != 1)
            {
                
                Tanitim tanitim=new Tanitim();
                tanitim.Show();
            }
        }

        private void sifre_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
    }
}
