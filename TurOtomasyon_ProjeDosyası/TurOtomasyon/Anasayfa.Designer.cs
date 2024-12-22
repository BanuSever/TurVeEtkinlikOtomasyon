namespace TurOtomasyon
{
    partial class Anasayfa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.artan_radio = new System.Windows.Forms.RadioButton();
            this.azalan_radio = new System.Windows.Forms.RadioButton();
            this.giris_btn = new MetroFramework.Controls.MetroButton();
            this.filtre_combo = new MetroFramework.Controls.MetroComboBox();
            this.rezervasyon_gor_btn = new MetroFramework.Controls.MetroButton();
            this.filtre_txt = new MetroFramework.Controls.MetroTextBox();
            this.admin_panel = new System.Windows.Forms.Panel();
            this.guncelle_btn = new MetroFramework.Controls.MetroButton();
            this.sil_btn = new MetroFramework.Controls.MetroButton();
            this.ekle_btn = new MetroFramework.Controls.MetroButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.npgsqlDataAdapter1 = new Npgsql.NpgsqlDataAdapter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.kullanici_panel = new System.Windows.Forms.Panel();
            this.bilgi_duzenle_btn = new System.Windows.Forms.Button();
            this.rez_btn = new System.Windows.Forms.Button();
            this.cikis_btn = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.admin_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.kullanici_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.giris_btn);
            this.panel3.Controls.Add(this.filtre_combo);
            this.panel3.Controls.Add(this.rezervasyon_gor_btn);
            this.panel3.Controls.Add(this.filtre_txt);
            this.panel3.Controls.Add(this.admin_panel);
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.panel3.Location = new System.Drawing.Point(4, 91);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1251, 574);
            this.panel3.TabIndex = 3;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Image = global::TurOtomasyon.Properties.Resources.icons8_mail_18;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(341, 513);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 25);
            this.label4.TabIndex = 59;
            this.label4.Text = "      iletisim@lightour.com";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Image = global::TurOtomasyon.Properties.Resources.icons8_phone_18;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(341, 542);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 25);
            this.label3.TabIndex = 58;
            this.label3.Text = "     0212 511 4444";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Image = global::TurOtomasyon.Properties.Resources.icons8_placeholder_24;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(571, 513);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(352, 51);
            this.label2.TabIndex = 57;
            this.label2.Text = "     Levent, Pınar maslak, Büyükdere Cd. No: 106, 34398 Sarıyer/İstanbul";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Image = global::TurOtomasyon.Properties.Resources.icons8_search_24;
            this.label1.Location = new System.Drawing.Point(148, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 20);
            this.label1.TabIndex = 56;
            this.label1.Text = "       ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.artan_radio);
            this.panel1.Controls.Add(this.azalan_radio);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.panel1.Location = new System.Drawing.Point(631, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 35);
            this.panel1.TabIndex = 18;
            // 
            // artan_radio
            // 
            this.artan_radio.AutoSize = true;
            this.artan_radio.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.artan_radio.Location = new System.Drawing.Point(2, 6);
            this.artan_radio.Margin = new System.Windows.Forms.Padding(2);
            this.artan_radio.Name = "artan_radio";
            this.artan_radio.Size = new System.Drawing.Size(73, 27);
            this.artan_radio.TabIndex = 17;
            this.artan_radio.TabStop = true;
            this.artan_radio.Text = "Artan";
            this.artan_radio.UseVisualStyleBackColor = true;
            // 
            // azalan_radio
            // 
            this.azalan_radio.AutoSize = true;
            this.azalan_radio.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.azalan_radio.Location = new System.Drawing.Point(89, 6);
            this.azalan_radio.Margin = new System.Windows.Forms.Padding(2);
            this.azalan_radio.Name = "azalan_radio";
            this.azalan_radio.Size = new System.Drawing.Size(82, 27);
            this.azalan_radio.TabIndex = 16;
            this.azalan_radio.TabStop = true;
            this.azalan_radio.Text = "Azalan";
            this.azalan_radio.UseVisualStyleBackColor = true;
            // 
            // giris_btn
            // 
            this.giris_btn.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.giris_btn.Location = new System.Drawing.Point(999, 12);
            this.giris_btn.Name = "giris_btn";
            this.giris_btn.Size = new System.Drawing.Size(96, 35);
            this.giris_btn.TabIndex = 55;
            this.giris_btn.Text = "Filtrele";
            this.giris_btn.UseSelectable = true;
            this.giris_btn.Click += new System.EventHandler(this.giris_btn_Click);
            // 
            // filtre_combo
            // 
            this.filtre_combo.FormattingEnabled = true;
            this.filtre_combo.ItemHeight = 24;
            this.filtre_combo.Items.AddRange(new object[] {
            "Tur Adı",
            "Ücret",
            "Şehir",
            "Tarih"});
            this.filtre_combo.Location = new System.Drawing.Point(824, 15);
            this.filtre_combo.Margin = new System.Windows.Forms.Padding(2);
            this.filtre_combo.Name = "filtre_combo";
            this.filtre_combo.Size = new System.Drawing.Size(160, 30);
            this.filtre_combo.TabIndex = 14;
            this.filtre_combo.UseSelectable = true;
            // 
            // rezervasyon_gor_btn
            // 
            this.rezervasyon_gor_btn.BackColor = System.Drawing.Color.RosyBrown;
            this.rezervasyon_gor_btn.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.rezervasyon_gor_btn.Location = new System.Drawing.Point(9, 513);
            this.rezervasyon_gor_btn.Margin = new System.Windows.Forms.Padding(2);
            this.rezervasyon_gor_btn.Name = "rezervasyon_gor_btn";
            this.rezervasyon_gor_btn.Size = new System.Drawing.Size(210, 50);
            this.rezervasyon_gor_btn.TabIndex = 11;
            this.rezervasyon_gor_btn.Text = "Rezervasyonlar ve Üyeler";
            this.rezervasyon_gor_btn.UseSelectable = true;
            this.rezervasyon_gor_btn.Click += new System.EventHandler(this.rezervasyon_gor_btn_Click);
            // 
            // filtre_txt
            // 
            // 
            // 
            // 
            this.filtre_txt.CustomButton.Image = null;
            this.filtre_txt.CustomButton.Location = new System.Drawing.Point(392, 2);
            this.filtre_txt.CustomButton.Margin = new System.Windows.Forms.Padding(2);
            this.filtre_txt.CustomButton.Name = "";
            this.filtre_txt.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.filtre_txt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.filtre_txt.CustomButton.TabIndex = 1;
            this.filtre_txt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.filtre_txt.CustomButton.UseSelectable = true;
            this.filtre_txt.CustomButton.Visible = false;
            this.filtre_txt.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.filtre_txt.Lines = new string[0];
            this.filtre_txt.Location = new System.Drawing.Point(196, 15);
            this.filtre_txt.Margin = new System.Windows.Forms.Padding(2);
            this.filtre_txt.MaxLength = 32767;
            this.filtre_txt.Name = "filtre_txt";
            this.filtre_txt.PasswordChar = '\0';
            this.filtre_txt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.filtre_txt.SelectedText = "";
            this.filtre_txt.SelectionLength = 0;
            this.filtre_txt.SelectionStart = 0;
            this.filtre_txt.ShortcutsEnabled = true;
            this.filtre_txt.Size = new System.Drawing.Size(420, 30);
            this.filtre_txt.TabIndex = 13;
            this.filtre_txt.UseSelectable = true;
            this.filtre_txt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.filtre_txt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.filtre_txt.TextChanged += new System.EventHandler(this.filtre_txt_TextChanged);
            // 
            // admin_panel
            // 
            this.admin_panel.Controls.Add(this.guncelle_btn);
            this.admin_panel.Controls.Add(this.sil_btn);
            this.admin_panel.Controls.Add(this.ekle_btn);
            this.admin_panel.Location = new System.Drawing.Point(928, 507);
            this.admin_panel.Margin = new System.Windows.Forms.Padding(2);
            this.admin_panel.Name = "admin_panel";
            this.admin_panel.Size = new System.Drawing.Size(321, 56);
            this.admin_panel.TabIndex = 9;
            this.admin_panel.Visible = false;
            // 
            // guncelle_btn
            // 
            this.guncelle_btn.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.guncelle_btn.Location = new System.Drawing.Point(102, 4);
            this.guncelle_btn.Margin = new System.Windows.Forms.Padding(2);
            this.guncelle_btn.Name = "guncelle_btn";
            this.guncelle_btn.Size = new System.Drawing.Size(120, 50);
            this.guncelle_btn.TabIndex = 6;
            this.guncelle_btn.Text = "Tur Güncelle";
            this.guncelle_btn.UseSelectable = true;
            this.guncelle_btn.Click += new System.EventHandler(this.guncelle_btn_Click);
            // 
            // sil_btn
            // 
            this.sil_btn.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.sil_btn.Location = new System.Drawing.Point(225, 4);
            this.sil_btn.Margin = new System.Windows.Forms.Padding(2);
            this.sil_btn.Name = "sil_btn";
            this.sil_btn.Size = new System.Drawing.Size(96, 50);
            this.sil_btn.TabIndex = 8;
            this.sil_btn.Text = "Tur Sil";
            this.sil_btn.UseSelectable = true;
            this.sil_btn.Click += new System.EventHandler(this.sil_btn_Click);
            // 
            // ekle_btn
            // 
            this.ekle_btn.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.ekle_btn.Location = new System.Drawing.Point(2, 4);
            this.ekle_btn.Margin = new System.Windows.Forms.Padding(2);
            this.ekle_btn.Name = "ekle_btn";
            this.ekle_btn.Size = new System.Drawing.Size(96, 50);
            this.ekle_btn.TabIndex = 7;
            this.ekle_btn.Text = "Tur Ekle";
            this.ekle_btn.UseSelectable = true;
            this.ekle_btn.Click += new System.EventHandler(this.ekle_btn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(139)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(0, 61);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1249, 442);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // npgsqlDataAdapter1
            // 
            this.npgsqlDataAdapter1.DeleteCommand = null;
            this.npgsqlDataAdapter1.InsertCommand = null;
            this.npgsqlDataAdapter1.SelectCommand = null;
            this.npgsqlDataAdapter1.UpdateCommand = null;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(204)))), ((int)(((byte)(207)))));
            this.panel4.Controls.Add(this.kullanici_panel);
            this.panel4.Controls.Add(this.cikis_btn);
            this.panel4.Location = new System.Drawing.Point(293, 37);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(962, 55);
            this.panel4.TabIndex = 11;
            // 
            // kullanici_panel
            // 
            this.kullanici_panel.Controls.Add(this.bilgi_duzenle_btn);
            this.kullanici_panel.Controls.Add(this.rez_btn);
            this.kullanici_panel.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kullanici_panel.Location = new System.Drawing.Point(460, 2);
            this.kullanici_panel.Margin = new System.Windows.Forms.Padding(2);
            this.kullanici_panel.Name = "kullanici_panel";
            this.kullanici_panel.Size = new System.Drawing.Size(382, 48);
            this.kullanici_panel.TabIndex = 15;
            // 
            // bilgi_duzenle_btn
            // 
            this.bilgi_duzenle_btn.FlatAppearance.BorderSize = 0;
            this.bilgi_duzenle_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bilgi_duzenle_btn.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.bilgi_duzenle_btn.Image = global::TurOtomasyon.Properties.Resources.icons8_edit_16;
            this.bilgi_duzenle_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bilgi_duzenle_btn.Location = new System.Drawing.Point(182, 9);
            this.bilgi_duzenle_btn.Margin = new System.Windows.Forms.Padding(2);
            this.bilgi_duzenle_btn.Name = "bilgi_duzenle_btn";
            this.bilgi_duzenle_btn.Size = new System.Drawing.Size(192, 34);
            this.bilgi_duzenle_btn.TabIndex = 11;
            this.bilgi_duzenle_btn.Text = "Bilgilerimi düzenle";
            this.bilgi_duzenle_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bilgi_duzenle_btn.UseVisualStyleBackColor = true;
            this.bilgi_duzenle_btn.Click += new System.EventHandler(this.bilgi_duzenle_btn_Click);
            // 
            // rez_btn
            // 
            this.rez_btn.FlatAppearance.BorderSize = 0;
            this.rez_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rez_btn.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rez_btn.Image = global::TurOtomasyon.Properties.Resources.icons8_schedule_16;
            this.rez_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rez_btn.Location = new System.Drawing.Point(2, 7);
            this.rez_btn.Margin = new System.Windows.Forms.Padding(2);
            this.rez_btn.Name = "rez_btn";
            this.rez_btn.Size = new System.Drawing.Size(176, 36);
            this.rez_btn.TabIndex = 10;
            this.rez_btn.Text = "Rezervasyonlarım";
            this.rez_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rez_btn.UseVisualStyleBackColor = true;
            this.rez_btn.Click += new System.EventHandler(this.rez_btn_Click);
            // 
            // cikis_btn
            // 
            this.cikis_btn.FlatAppearance.BorderSize = 0;
            this.cikis_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cikis_btn.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cikis_btn.Image = global::TurOtomasyon.Properties.Resources.icons8_close_16;
            this.cikis_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cikis_btn.Location = new System.Drawing.Point(846, 11);
            this.cikis_btn.Margin = new System.Windows.Forms.Padding(2);
            this.cikis_btn.Name = "cikis_btn";
            this.cikis_btn.Size = new System.Drawing.Size(114, 33);
            this.cikis_btn.TabIndex = 12;
            this.cikis_btn.Text = "Çıkış Yap";
            this.cikis_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cikis_btn.UseVisualStyleBackColor = true;
            this.cikis_btn.Click += new System.EventHandler(this.cikis_btn_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TurOtomasyon.Properties.Resources.LIGHTOUR1;
            this.pictureBox2.Location = new System.Drawing.Point(4, 35);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(291, 57);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // Anasayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 670);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Anasayfa";
            this.Padding = new System.Windows.Forms.Padding(11, 60, 11, 15);
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Load += new System.EventHandler(this.Anasayfa_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.admin_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.kullanici_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton artan_radio;
        private System.Windows.Forms.RadioButton azalan_radio;
        private MetroFramework.Controls.MetroButton giris_btn;
        private MetroFramework.Controls.MetroComboBox filtre_combo;
        private MetroFramework.Controls.MetroButton rezervasyon_gor_btn;
        private MetroFramework.Controls.MetroTextBox filtre_txt;
        private System.Windows.Forms.Panel admin_panel;
        private MetroFramework.Controls.MetroButton guncelle_btn;
        private MetroFramework.Controls.MetroButton sil_btn;
        private MetroFramework.Controls.MetroButton ekle_btn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Npgsql.NpgsqlDataAdapter npgsqlDataAdapter1;
        private System.Windows.Forms.Button cikis_btn;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel kullanici_panel;
        private System.Windows.Forms.Button bilgi_duzenle_btn;
        private System.Windows.Forms.Button rez_btn;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}