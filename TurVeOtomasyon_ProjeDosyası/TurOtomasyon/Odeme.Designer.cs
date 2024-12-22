namespace TurOtomasyon
{
    partial class Odeme
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
            this.ad_txt = new MetroFramework.Controls.MetroTextBox();
            this.uyeol_btn = new MetroFramework.Controls.MetroButton();
            this.cvv_txt = new MetroFramework.Controls.MetroTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.eposta_lbl = new System.Windows.Forms.Label();
            this.soyad_lbl = new System.Windows.Forms.Label();
            this.ad_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.onay_box = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ucret_lbl = new System.Windows.Forms.Label();
            this.ay_combo = new MetroFramework.Controls.MetroComboBox();
            this.yil_combo = new MetroFramework.Controls.MetroComboBox();
            this.kartno_txt = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // ad_txt
            // 
            // 
            // 
            // 
            this.ad_txt.CustomButton.Image = null;
            this.ad_txt.CustomButton.Location = new System.Drawing.Point(227, 2);
            this.ad_txt.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ad_txt.CustomButton.Name = "";
            this.ad_txt.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.ad_txt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.ad_txt.CustomButton.TabIndex = 1;
            this.ad_txt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ad_txt.CustomButton.UseSelectable = true;
            this.ad_txt.CustomButton.Visible = false;
            this.ad_txt.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.ad_txt.Lines = new string[0];
            this.ad_txt.Location = new System.Drawing.Point(28, 73);
            this.ad_txt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ad_txt.MaxLength = 40;
            this.ad_txt.Name = "ad_txt";
            this.ad_txt.PasswordChar = '\0';
            this.ad_txt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ad_txt.SelectedText = "";
            this.ad_txt.SelectionLength = 0;
            this.ad_txt.SelectionStart = 0;
            this.ad_txt.ShortcutsEnabled = true;
            this.ad_txt.Size = new System.Drawing.Size(255, 30);
            this.ad_txt.TabIndex = 86;
            this.ad_txt.UseSelectable = true;
            this.ad_txt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ad_txt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.ad_txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ad_txt_KeyPress);
            // 
            // uyeol_btn
            // 
            this.uyeol_btn.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.uyeol_btn.Location = new System.Drawing.Point(30, 581);
            this.uyeol_btn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uyeol_btn.Name = "uyeol_btn";
            this.uyeol_btn.Size = new System.Drawing.Size(253, 44);
            this.uyeol_btn.TabIndex = 85;
            this.uyeol_btn.Text = "Ödeme Yap";
            this.uyeol_btn.UseSelectable = true;
            this.uyeol_btn.Click += new System.EventHandler(this.uyeol_btn_Click);
            // 
            // cvv_txt
            // 
            // 
            // 
            // 
            this.cvv_txt.CustomButton.Image = null;
            this.cvv_txt.CustomButton.Location = new System.Drawing.Point(227, 2);
            this.cvv_txt.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cvv_txt.CustomButton.Name = "";
            this.cvv_txt.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.cvv_txt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.cvv_txt.CustomButton.TabIndex = 1;
            this.cvv_txt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cvv_txt.CustomButton.UseSelectable = true;
            this.cvv_txt.CustomButton.Visible = false;
            this.cvv_txt.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.cvv_txt.Lines = new string[0];
            this.cvv_txt.Location = new System.Drawing.Point(28, 243);
            this.cvv_txt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cvv_txt.MaxLength = 3;
            this.cvv_txt.Name = "cvv_txt";
            this.cvv_txt.PasswordChar = '\0';
            this.cvv_txt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.cvv_txt.SelectedText = "";
            this.cvv_txt.SelectionLength = 0;
            this.cvv_txt.SelectionStart = 0;
            this.cvv_txt.ShortcutsEnabled = true;
            this.cvv_txt.Size = new System.Drawing.Size(255, 30);
            this.cvv_txt.TabIndex = 83;
            this.cvv_txt.UseSelectable = true;
            this.cvv_txt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.cvv_txt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.cvv_txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cvv_txt_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(25, 292);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 25);
            this.label5.TabIndex = 81;
            this.label5.Text = "Ay";
            // 
            // eposta_lbl
            // 
            this.eposta_lbl.AutoSize = true;
            this.eposta_lbl.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.eposta_lbl.Location = new System.Drawing.Point(24, 201);
            this.eposta_lbl.Name = "eposta_lbl";
            this.eposta_lbl.Size = new System.Drawing.Size(191, 25);
            this.eposta_lbl.TabIndex = 80;
            this.eposta_lbl.Text = "Güvenlik Kodu / CVV ";
            // 
            // soyad_lbl
            // 
            this.soyad_lbl.AutoSize = true;
            this.soyad_lbl.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.soyad_lbl.Location = new System.Drawing.Point(24, 31);
            this.soyad_lbl.Name = "soyad_lbl";
            this.soyad_lbl.Size = new System.Drawing.Size(90, 25);
            this.soyad_lbl.TabIndex = 78;
            this.soyad_lbl.Text = "Ad Soyad";
            // 
            // ad_lbl
            // 
            this.ad_lbl.AutoSize = true;
            this.ad_lbl.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ad_lbl.Location = new System.Drawing.Point(24, 114);
            this.ad_lbl.Name = "ad_lbl";
            this.ad_lbl.Size = new System.Drawing.Size(131, 25);
            this.ad_lbl.TabIndex = 77;
            this.ad_lbl.Text = "Kart Numarası";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(159, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 25);
            this.label1.TabIndex = 90;
            this.label1.Text = "Yıl";
            // 
            // onay_box
            // 
            this.onay_box.Location = new System.Drawing.Point(28, 446);
            this.onay_box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.onay_box.Name = "onay_box";
            this.onay_box.Size = new System.Drawing.Size(275, 115);
            this.onay_box.TabIndex = 91;
            this.onay_box.Text = "Kart bilgilerimi girerek onayladığım bu ödemeye dair bu servisi aldığımı kabul ed" +
    "iyorum.";
            this.onay_box.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.onay_box.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(24, 397);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 25);
            this.label2.TabIndex = 92;
            this.label2.Text = "Tutar: ";
            // 
            // ucret_lbl
            // 
            this.ucret_lbl.AutoSize = true;
            this.ucret_lbl.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ucret_lbl.Location = new System.Drawing.Point(158, 397);
            this.ucret_lbl.Name = "ucret_lbl";
            this.ucret_lbl.Size = new System.Drawing.Size(0, 25);
            this.ucret_lbl.TabIndex = 94;
            // 
            // ay_combo
            // 
            this.ay_combo.FormattingEnabled = true;
            this.ay_combo.ItemHeight = 24;
            this.ay_combo.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.ay_combo.Location = new System.Drawing.Point(28, 331);
            this.ay_combo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ay_combo.Name = "ay_combo";
            this.ay_combo.Size = new System.Drawing.Size(121, 30);
            this.ay_combo.TabIndex = 95;
            this.ay_combo.UseSelectable = true;
            // 
            // yil_combo
            // 
            this.yil_combo.FormattingEnabled = true;
            this.yil_combo.ItemHeight = 24;
            this.yil_combo.Items.AddRange(new object[] {
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35"});
            this.yil_combo.Location = new System.Drawing.Point(162, 331);
            this.yil_combo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.yil_combo.Name = "yil_combo";
            this.yil_combo.Size = new System.Drawing.Size(121, 30);
            this.yil_combo.TabIndex = 96;
            this.yil_combo.UseSelectable = true;
            // 
            // kartno_txt
            // 
            this.kartno_txt.Location = new System.Drawing.Point(28, 145);
            this.kartno_txt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.kartno_txt.Name = "kartno_txt";
            this.kartno_txt.Size = new System.Drawing.Size(255, 31);
            this.kartno_txt.TabIndex = 97;
            // 
            // Odeme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 652);
            this.Controls.Add(this.kartno_txt);
            this.Controls.Add(this.yil_combo);
            this.Controls.Add(this.ay_combo);
            this.Controls.Add(this.ucret_lbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.onay_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ad_txt);
            this.Controls.Add(this.uyeol_btn);
            this.Controls.Add(this.cvv_txt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.eposta_lbl);
            this.Controls.Add(this.soyad_lbl);
            this.Controls.Add(this.ad_lbl);
            this.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Odeme";
            this.Padding = new System.Windows.Forms.Padding(25, 94, 25, 31);
            this.Load += new System.EventHandler(this.Odeme_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox ad_txt;
        private MetroFramework.Controls.MetroButton uyeol_btn;
        private MetroFramework.Controls.MetroTextBox cvv_txt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label eposta_lbl;
        private System.Windows.Forms.Label soyad_lbl;
        private System.Windows.Forms.Label ad_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox onay_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ucret_lbl;
        private MetroFramework.Controls.MetroComboBox ay_combo;
        private MetroFramework.Controls.MetroComboBox yil_combo;
        private System.Windows.Forms.MaskedTextBox kartno_txt;
    }
}