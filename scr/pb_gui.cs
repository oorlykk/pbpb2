using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace pbpb
{
    public static class Gui {

        public static bool HiddenMode = true;
        public static bool PassiveMode = true;
    }

    partial class Form1
    {
        void ReadGui( object sender = null, EventArgs e = null) {

            Gui.HiddenMode = chb_HiddenMode.Checked;
            Gui.PassiveMode = chb_PassiveMode.Checked;
        }
    }
}
