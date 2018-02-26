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
        _PubgInputMessage pi;

        protected override void WndProc( ref Message m )
        {
            if (m.Msg == Form1.WM_SCRUPDATE) {

                if (Visible) {

                    BackgroundImage = null;
                    BackgroundImage = PubgStatus.RawScr;
                }
            }

            base.WndProc( ref m );
        }

        public FormView()
        {
            InitializeComponent();

            Text = Form1.ViewFormTitle;

            pi = new _PubgInputMessage();
            pi.AsPostMessage = true;
        }

        private void FormView_KeyUp( object sender, KeyEventArgs e )
        {
            pi.KeyDownOrUp(e.KeyCode, true);
            
        }

        private void FormView_KeyPress( object sender, KeyPressEventArgs e )
        {
            //
        }

        private void FormView_KeyDown( object sender, KeyEventArgs e )
        {
            pi.KeyDownOrUp(e.KeyCode, false);
        }

        private void FormView_MouseClick( object sender, MouseEventArgs e )
        {
            if (e.Button == MouseButtons.Left) pi.ClickLeftMouse(0, 0);
        }
    }
}
