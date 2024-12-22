using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
using TurOtomasyon.Helpers;

namespace TurOtomasyon
{
    public partial class Muhasebe : MetroFramework.Forms.MetroForm
    {
        public Muhasebe()
        {
            InitializeComponent();
            GelirleriGoster();
            int selectedAy = 0; 
            int selectedYil = 0; 
            GiderleriGoster(selectedAy, selectedYil);
        }
        private void GelirleriGoster()
        {
            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "SELECT k.k_adi AS \"Müşteri Adı\", k.k_soyadi AS \"Müşteri Soyadı\", " +
                                   "o.o_tutar AS \"Ödeme Miktarı\", o.o_tarih AS \"Ödeme Tarihi\", o.o_kontrol AS \"Kontrol\" " +
                                   "FROM odemeler o " +
                                   "INNER JOIN rezervasyon r ON o.o_rezervasyonid = r.r_id " +
                                   "INNER JOIN kullanicilar k ON r.r_kulid = k.k_id";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    if (!dataGridView1.Columns.Contains("İade"))
                    {
                        DataGridViewTextBoxColumn iadeColumn = new DataGridViewTextBoxColumn
                        {
                            Name = "İade",
                            HeaderText = "İade Durumu",
                            ReadOnly = true
                        };
                        dataGridView1.Columns.Add(iadeColumn);
                    }
                    dataGridView1.Columns["Kontrol"].Visible = false;

                    string toplamGelirQuery = "SELECT COALESCE(SUM(o_tutar), 0) FROM odemeler";
                    NpgsqlCommand toplamGelirCmd = new NpgsqlCommand(toplamGelirQuery, conn);
                    top_gelir_lbl.Text = "Toplam Gelir: " + toplamGelirCmd.ExecuteScalar().ToString() + " TL";

                    dataGridView1.CellFormatting += dataGridView1_CellFormatting;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "İade" && e.RowIndex >= 0)
            {
                // kontrol degerine gore iade sutunu dolduruldu
                var kontrolValue = dataGridView1.Rows[e.RowIndex].Cells["Kontrol"].Value;
                if (kontrolValue is DBNull || kontrolValue == null || (bool)kontrolValue)
                {
                    e.Value = "Satın Alındı";
                }
                else
                {
                    e.Value = "İade Edilecek";
                }
            }
        }

        private void GiderleriGoster(int selectedAy, int selectedYil)
        {
            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "SELECT otobus AS \"Otobüs Gideri\", akaryakit AS \"Akaryakıt Gideri\", hizmet AS \"Hizmet Gideri\", " +
                                   "calisan AS \"Çalışan Gideri\", vergi AS \"Vergi Gideri\" FROM giderler " +
                                   "WHERE yil = @yil AND ay = @ay";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@yil", selectedYil);
                    da.SelectCommand.Parameters.AddWithValue("@ay", selectedAy);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;

                    string toplamGiderQuery = "SELECT COALESCE(SUM(otobus + akaryakit + hizmet + calisan + vergi), 0) FROM giderler " +
                                              "WHERE yil = @yil AND ay = @ay";
                    NpgsqlCommand toplamGiderCmd = new NpgsqlCommand(toplamGiderQuery, conn);
                    toplamGiderCmd.Parameters.AddWithValue("@yil", selectedYil);
                    toplamGiderCmd.Parameters.AddWithValue("@ay", selectedAy);
                    top_gider_lbl.Text = "Toplam Gider: " + toplamGiderCmd.ExecuteScalar().ToString() + " TL";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        int kapama = 0;
        private void cikis_btn_Click(object sender, EventArgs e)
        {
            kapama = 1;
            this.Close();

            Tanitim tanitim = new Tanitim();
            tanitim.kapama = 0;
            tanitim.Show();
        }

        private void Muhasebe_Load(object sender, EventArgs e)
        {
            otobus_gider_txt.TabIndex = 0;
            akaryakit_txt.TabIndex = 1;
            hizmet_gider_txt.TabIndex = 2;
            calisan_gider_txt.TabIndex = 3;
            vergi_txt.TabIndex = 4;
        }

        private void ekle_btn_Click(object sender, EventArgs e)
        {
            if (comboBoxAy.SelectedItem == null || comboBoxYil.SelectedItem == null)
            {
                MessageBox.Show("Lütfen ay ve yıl seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (NpgsqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    int selectedAy = Convert.ToInt32(comboBoxAy.SelectedItem);  
                    int selectedYil = Convert.ToInt32(comboBoxYil.SelectedItem); 

                    string getCurrentValueQuery = "SELECT otobus, akaryakit, hizmet, calisan, vergi " +
                                                  "FROM giderler WHERE yil = @yil AND ay = @ay";
                    NpgsqlCommand getCurrentValueCmd = new NpgsqlCommand(getCurrentValueQuery, conn);
                    getCurrentValueCmd.Parameters.AddWithValue("@yil", selectedYil);
                    getCurrentValueCmd.Parameters.AddWithValue("@ay", selectedAy);

                    NpgsqlDataReader reader = getCurrentValueCmd.ExecuteReader();
                    decimal currentOtobus = 0, currentAkaryakit = 0, currentHizmet = 0, currentCalisan = 0, currentVergi = 0;

                    if (reader.Read())
                    {
                        currentOtobus = reader.GetDecimal(0); 
                        currentAkaryakit = reader.GetDecimal(1); 
                        currentHizmet = reader.GetDecimal(2); 
                        currentCalisan = reader.GetDecimal(3); 
                        currentVergi = reader.GetDecimal(4); 
                    }
                    reader.Close();

                    decimal newOtobus = string.IsNullOrEmpty(otobus_gider_txt.Text) ? 0 : decimal.Parse(otobus_gider_txt.Text);
                    decimal newAkaryakit = string.IsNullOrEmpty(akaryakit_txt.Text) ? 0 : decimal.Parse(akaryakit_txt.Text);
                    decimal newHizmet = string.IsNullOrEmpty(hizmet_gider_txt.Text) ? 0 : decimal.Parse(hizmet_gider_txt.Text);
                    decimal newCalisan = string.IsNullOrEmpty(calisan_gider_txt.Text) ? 0 : decimal.Parse(calisan_gider_txt.Text);
                    decimal newVergi = string.IsNullOrEmpty(vergi_txt.Text) ? 0 : decimal.Parse(vergi_txt.Text);

                    decimal updatedOtobus = currentOtobus + newOtobus;
                    decimal updatedAkaryakit = currentAkaryakit + newAkaryakit;
                    decimal updatedHizmet = currentHizmet + newHizmet;
                    decimal updatedCalisan = currentCalisan + newCalisan;
                    decimal updatedVergi = currentVergi + newVergi;

                    string updateQuery = "UPDATE giderler SET " +
                                         "otobus = @otobus, akaryakit = @akaryakit, hizmet = @hizmet, " +
                                         "calisan = @calisan, vergi = @vergi " +
                                         "WHERE yil = @yil AND ay = @ay";

                    NpgsqlCommand updateCmd = new NpgsqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@yil", selectedYil);
                    updateCmd.Parameters.AddWithValue("@ay", selectedAy);
                    updateCmd.Parameters.AddWithValue("@otobus", updatedOtobus);
                    updateCmd.Parameters.AddWithValue("@akaryakit", updatedAkaryakit);
                    updateCmd.Parameters.AddWithValue("@hizmet", updatedHizmet);
                    updateCmd.Parameters.AddWithValue("@calisan", updatedCalisan);
                    updateCmd.Parameters.AddWithValue("@vergi", updatedVergi);
                    updateCmd.ExecuteNonQuery();

                    MessageBox.Show("Giderler başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    otobus_gider_txt.Clear();
                    akaryakit_txt.Clear();
                    hizmet_gider_txt.Clear();
                    calisan_gider_txt.Clear();
                    vergi_txt.Clear();

                    GiderleriGoster(selectedAy, selectedYil);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }





        private void Muhasebe_Resize(object sender, EventArgs e)
        {
            //FormHelper.CenterPanel(this, panel2);
        }

        private void otobus_gider_txt_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void akaryakit_txt_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void hizmet_gider_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void calisan_gider_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void vergi_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void Muhasebe_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (kapama != 1)
            {
                Tanitim tanitim = new Tanitim();
                tanitim.Show();
            }
        }

        private void comboBoxAy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void filtrele_txt_Click(object sender, EventArgs e)
        {
            if (filtre_ay.SelectedItem == null || filtre_yil.SelectedItem == null)
            {
                MessageBox.Show("Lütfen ay ve yıl seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlem yapılmaz
            }
            int selectedAy = Convert.ToInt32(filtre_ay.SelectedItem);  // ComboBox'tan seçilen ay
            int selectedYil = Convert.ToInt32(filtre_yil.SelectedItem);  // ComboBox'tan seçilen yıl

            // Giderleri yeniden güncelle
            GiderleriGoster(selectedAy, selectedYil);
        }


    }
}