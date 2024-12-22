using Npgsql;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using TurOtomasyon.Helpers;

namespace TurOtomasyon
{
    public partial class TurEkle : MetroFramework.Forms.MetroForm
    {
        private string selectedImagePath = ""; 

        public TurEkle()
        {
            InitializeComponent();
        }

        public string trol;
        public string tmail;
        private void geri_btn_Click(object sender, EventArgs e)
        {
            kapama = 1;
            this.Close();
            
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.rol = "admin";
            anasayfa.Show();
        }

        private void foto_ekle_btn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff"; 
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                selectedImagePath = openFileDialog1.FileName; // secilen fotograf yolu
                pictureBox1.ImageLocation = selectedImagePath; // picturebox'ta goster
            }
        }

        private void kaydet_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(t_adi.Text) ||
                string.IsNullOrWhiteSpace(t_lokasyon.Text) ||
                string.IsNullOrWhiteSpace(t_ucret.Text) ||
                string.IsNullOrWhiteSpace(t_suresi.Text) ||
                string.IsNullOrWhiteSpace(t_duraklar.Text) ||
                string.IsNullOrWhiteSpace(richTextBox1.Text) ||
                pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen tüm alanları doldurduğunuzdan ve fotoğraf eklediğinizden emin olun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (richTextBox1.Text.Length < 50)
            {
                MessageBox.Show("Açıklama en az 50 karakter olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!float.TryParse(t_ucret.Text, out float turUcreti))
            {
                MessageBox.Show("Lütfen geçerli bir ücret girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // fotograf uzantisini kontrol etme
            string fileExtension = Path.GetExtension(selectedImagePath).ToLower();
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };

            if (Array.IndexOf(validExtensions, fileExtension) == -1)
            {
                MessageBox.Show("Fotoğraf yalnızca geçerli bir resim formatında olmalıdır. (.jpg, .jpeg, .png, .gif, .bmp, .tiff)", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"INSERT INTO turlar 
                             (tur_adi, tur_lokasyon, tur_ucreti, tur_suresi, tur_duraklar, tur_tarihi, tur_aciklama, foto_url) 
                             VALUES 
                             (@tur_adi, @tur_lokasyon, @tur_ucreti, @tur_suresi, @tur_duraklar, @tur_tarihi, @tur_aciklama, @foto_url)";

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    //secilen fotografi kaydet
                    string fotoUrl = SaveImageAndGetRelativePath(selectedImagePath);

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tur_adi", t_adi.Text);
                        cmd.Parameters.AddWithValue("@tur_lokasyon", t_lokasyon.Text);
                        cmd.Parameters.AddWithValue("@tur_ucreti", turUcreti); 
                        cmd.Parameters.AddWithValue("@tur_suresi", t_suresi.Text);
                        cmd.Parameters.AddWithValue("@tur_duraklar", t_duraklar.Text);
                        string formattedDate = dateTimePicker1.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        cmd.Parameters.AddWithValue("@tur_tarihi", formattedDate);
                        cmd.Parameters.AddWithValue("@tur_aciklama", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@foto_url", fotoUrl);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Tur başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        t_adi.Clear();
                        t_lokasyon.Clear();
                        t_ucret.Clear();
                        t_suresi.Clear();
                        t_duraklar.Clear();
                        richTextBox1.Clear();
                        pictureBox1.Image = null;

                        kapama = 1;
                        this.Close();
                        
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

        private string SaveImageAndGetRelativePath(string selectedImagePath)
        {
            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory; // proje kok dizini
                string imagesDirectory = Path.Combine(projectDirectory, "img"); // img klasorunun yolu

                // klasor yoksa olustur
                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }

                // yeni dosya adi ve yolu
                string newFileName = Path.GetFileName(selectedImagePath);
                string newFilePath = Path.Combine(imagesDirectory, newFileName);

                File.Copy(selectedImagePath, newFilePath, overwrite: true);

                return $"img/{newFileName}";
            }
            return string.Empty; // foto secilmediyse bos string dondur
        }
        public int kapama = 0;
        private void TurEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(kapama!=1)
            {
                Anasayfa anasayfa = new Anasayfa();
                anasayfa.rol = trol;
                anasayfa.mail=tmail;
                anasayfa.Show();
            }
        }

        private void t_ucret_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
