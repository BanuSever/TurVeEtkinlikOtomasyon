using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurOtomasyon.Helpers;

namespace TurOtomasyon
{
    public partial class Tanitim : MetroFramework.Forms.MetroForm
    {
        public Tanitim()
        {
            InitializeComponent();
        }

        KullaniciGiris giris = new KullaniciGiris();

        public int kapama = 0;
        private void Tanitim_Resize(object sender, EventArgs e)
        {
            FormHelper.CenterPanel(this,panel1);
        }

        private void kullanici_btn_Click_1(object sender, EventArgs e)
        {
            kapama = 1;
            giris.Show();
            this.Hide();
        }

        private void Tanitim_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(kapama!=1)
            {
                Environment.Exit(0);
            }
        }
    }
}
