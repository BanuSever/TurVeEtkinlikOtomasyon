using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurOtomasyon.Helpers
{
    public static class FormHelper
    {
        public static void CenterPanel(Form form, Panel panel)
        {
            panel.Left = (form.Width - panel.Width) / 2;
            panel.Top = (form.Height - panel.Height) / 2;
        }
    }
}
