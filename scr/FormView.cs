using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pbpb
{
    public partial class FormView : Form
    {

        protected override void WndProc( ref Message m )
        {
            if (m.Msg == Form1.WM_SCRUPDATE) {

                if (Visible)
                    try {

                        BackgroundImage = PubgStatus.RawScr;

                    }
                    catch { }
            }

            base.WndProc( ref m );
        }

        public FormView()
        {
            InitializeComponent();

            Text = Form1.ViewFormTitle;
        }

    }
}
