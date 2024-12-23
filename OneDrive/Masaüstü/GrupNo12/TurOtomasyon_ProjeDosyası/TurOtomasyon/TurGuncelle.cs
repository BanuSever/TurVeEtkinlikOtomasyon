using Npgsql;
using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using TurOtomasyon.Helpers;
using System.Globalization;


namespace TurOtomasyon
{
    public partial class TurGuncelle : MetroFramework.Forms.MetroForm
    {
        public string TurId { get; set; } // anasayfadan tur id
        private string selectedImagePath = "";
        public TurGuncelle()
        {
            InitializeComponent();
        }
        public string grol;
        public string gmail;
        public int kapama = 0;
        private void TurGuncelle_Load(object sender, EventArgs e)
        {
            LoadTurDetails();
        }
        private void geri_btn_Click(object sender, EventArgs e)
        {
            kapama = 1;
            this.Close();
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.rol = "admin";
            
            anasayfa.Show();
        }
        private void TurGuncelle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (kapama == 0)
            {
                Anasayfa anasayfa = new Anasayfa();
                anasayfa.rol = grol;
                anasayfa.mail = gmail;
                anasayfa.Show();
            }
        }

        private void LoadTurDetails()
        {
            if (int.TryParse(TurId, out int turIdInt)) 
            {
                string query = "SELECT tur_adi, tur_lokasyon, tur_ucreti, tur_suresi, tur_aciklama, foto_url,tur_duraklar, tur_tarihi FROM turlar WHERE tur_id = @tur_id";

                using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
                {
                    try
                    {
                        conn.Open();
                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@tur_id", turIdInt);

                            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    float turUcreti = reader.GetFloat(reader.GetOrdinal("tur_ucreti"));

                                    t_adi.Text = reader["tur_adi"].ToString();
                                    t_lokasyon.Text = reader["tur_lokasyon"].ToString();
                                    t_ucret.Text = turUcreti.ToString();  
                                    t_suresi.Text = reader["tur_suresi"].ToString();
                                    aciklama_txt.Text = reader["tur_aciklama"].ToString();
                                    t_duraklar.Text = reader["tur_duraklar"].ToString();
                                    t_tarih.Text = reader["tur_tarihi"].ToString();

                                    string fotoUrl = reader["foto_url"].ToString();
                                    string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                                    string fotoPath = Path.Combine(projectDirectory, fotoUrl);

                                    //foto yukle
                                    if (File.Exists(fotoPath))
                                    {
                                        pictureBox1.Image = Image.FromFile(fotoPath);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Resim dosyası bulunamadı: " + fotoPath, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Geçersiz Tur ID.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string SaveImageAndGetRelativePath(string selectedImagePath)
        {
            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory; //proje kok dizini
                string imagesDirectory = Path.Combine(projectDirectory, "img"); // img klasorunun yolu

                // klasor yoksa olustur
                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }
                
                //yeni dosya yolu ve adi olustur
                string newFileName = Path.GetFileName(selectedImagePath);
                string newFilePath = Path.Combine(imagesDirectory, newFileName);

                File.Copy(selectedImagePath, newFilePath, overwrite: true);
                //yolu vt kaydet
                return $"img/{newFileName}";
            }
            return string.Empty; // foto secilmediyse bos string
        }


        private void kaydet_btn_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(t_adi.Text) || string.IsNullOrEmpty(t_lokasyon.Text) || string.IsNullOrEmpty(t_ucret.Text) ||
                string.IsNullOrEmpty(t_suresi.Text) || string.IsNullOrEmpty(aciklama_txt.Text) || string.IsNullOrEmpty(t_duraklar.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurduğunuzdan emin olun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (aciklama_txt.Text.Length < 50)
            {
                MessageBox.Show("Açıklama en az 50 karakter uzunluğunda olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                string fileExtension = Path.GetExtension(selectedImagePath).ToLower();
                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                {
                    MessageBox.Show("Lütfen yalnızca .jpg,.png veya .jpeg formatında bir fotoğraf seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            string query = @"UPDATE turlar 
                     SET tur_adi = @tur_adi, 
                         tur_lokasyon = @tur_lokasyon, 
                         tur_ucreti = @tur_ucreti, 
                         tur_suresi = @tur_suresi,
                         tur_tarihi = @tur_tarihi,
                         tur_duraklar = @tur_duraklar,
                         tur_aciklama = @tur_aciklama,
                         foto_url = COALESCE(@foto_url, foto_url) -- Eğer fotoğraf seçilmemişse mevcut fotoğraf URL'sini korur
                     WHERE tur_id = @tur_id";

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string fotoUrl = string.IsNullOrEmpty(selectedImagePath) ? null : SaveImageAndGetRelativePath(selectedImagePath);

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tur_id", int.Parse(TurId));
                        cmd.Parameters.AddWithValue("@tur_adi", t_adi.Text);
                        cmd.Parameters.AddWithValue("@tur_lokasyon", t_lokasyon.Text);
                        cmd.Parameters.AddWithValue("@tur_ucreti", float.Parse(t_ucret.Text));
                        cmd.Parameters.AddWithValue("@tur_suresi", t_suresi.Text);
                        cmd.Parameters.AddWithValue("@tur_duraklar", t_duraklar.Text);
                        cmd.Parameters.AddWithValue("@tur_tarihi", t_tarih.Text);
                        cmd.Parameters.AddWithValue("@tur_aciklama", aciklama_txt.Text);

                        if (!string.IsNullOrEmpty(fotoUrl))
                        {
                            cmd.Parameters.AddWithValue("@foto_url", fotoUrl);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@foto_url", DBNull.Value); 
                        }

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tur bilgileri başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        Anasayfa anasayfa = new Anasayfa();
                        anasayfa.rol = "admin";
                        anasayfa.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void foto_ekle_btn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                selectedImagePath = openFileDialog1.FileName;
                pictureBox1.ImageLocation = selectedImagePath;
            }
        }

        private void t_ucret_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void t_tarih_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '/' && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true; 
                    MessageBox.Show("Lütfen yalnızca rakam ve / kullanınız.", "Geçersiz Karakter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
           

        }

        private void t_tarih_Leave(object sender, EventArgs e)
        {
            string input = t_tarih.Text;

            if (!DateTime.TryParseExact(input, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                MessageBox.Show("Lütfen tarihi Yıl/Ay/Gün formatında giriniz!", "Hatalı Tarih", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                t_tarih.Focus(); 
            }
        }
    }
}
