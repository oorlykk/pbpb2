using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using pbpb.Properties;
using SnLib;
using Win32;

namespace pbpb {
    public partial class Form1 : Form
    {
        static string uniq = "dGhleg==";

        public static string AppTitle = "PBPB v1.4";
        public const int PartFullHDPreset = 5;    

        static bool AppIsExp;
        static string DTstr => DateTime.Now.ToString().Replace(":", " ");
        static string filename_now => SPath.Desctop + DTstr + ".bmp";
        static string full_scr_filename = SPath.Desctop + "scr.bmp";
        public static Random RND = new Random();      
        private static ManualResetEvent BotStopper = new ManualResetEvent(true);
             
        public static Task PubgStatusChecker, PubgRestarter = null;

        public Form1()
        {

            if (CheckAppIsDup()) Environment.Exit( 0 );

            InitializeComponent();
            Text = AppTitle;
            Icon = Resources.gray;
            tray.Icon = Resources.gray;
          
            DateTime thisDate = DateTime.Today;
            string date_convert1 = Convert.ToString( "25.02.2018" );
            DateTime pDate = Convert.ToDateTime( date_convert1 );
            int date_cmp_res = thisDate.CompareTo( pDate );
            AppIsExp = (date_cmp_res > 0);
            panel_test.Visible = false;
            if (Environment.MachineName == "NORM") {
                AppIsExp = false;
                panel_test.Visible = true;
            }

            PubgWindow.PartFullHD = PartFullHDPreset;

            Log.LogEvent += new ResolveEventHandler(PubgLogEvent);

            Init_Pcs();

            InitInput_message();
                      
            Init_HotKeysMon();

            Application.ApplicationExit += new EventHandler(OnApplicationExit);

            Setti.Load(); WriteGui();
            
        }

        private void OnApplicationExit( object sender, EventArgs  e ) {

            if (Setti.IsChanged) Setti.Save();
        }


        void PubgInputEvent( PubgInputEventArgs e ) {

            string act;

            if (e.IsPress) act = "press";
            else {
                if (e.IsUp) act = "up";
                else act = "down";
            }

            string s = String.Format( "{0} {1}", e.Key.ToString(), act);
            Log.Add(s);

        }

        Assembly PubgLogEvent( object sender, ResolveEventArgs e ) {
            if (panel1.Visible) panel1.Hide();
            txLog.AppendText(e.Name);
            return null;
        }

        private void btnStartStopBot_click( object sender, EventArgs e ) {            
            if (btnStartStopBot.Text == "on") StartBotClick(); else StopBotClick();
        }

        private void StartBotClick( object sender = null, EventArgs e = null) {
            
            if (btnStartStopBot.Text == "off") {
                Log.Add("< Act failed > (already started)");
                return;
            }

            BotStopper.Reset();
            PubgStatus.ResetLastGood();
            PubgInput.EjectClickedTime = int.MaxValue;
            PubgInput.ParachuteClickedTime = int.MaxValue;
            PubgStatusChecker = Task.Run( () => PubgRestarterProc() );
            PubgRestarter = Task.Run( () => PubgStatusProc() );

            btnStartStopBot.Text = "off";         
            tray.Icon = Resources.main;
            Icon = tray.Icon;
        }

        private void StopBotClick( object sender = null, EventArgs e = null ) {

            BotStopper.Set();

            btnStartStopBot.Text = "on";  
            tray.Icon = Resources.gray;
            Icon = tray.Icon;
        }

        private void btnTag_Click( object sender, EventArgs e ) {

            string t;

            if ( sender.GetType().ToString() == "System.Windows.Forms.Button" )
                t = ((Control)sender).Tag.ToString();
            else 
                t = ((ToolStripItem)sender).Tag.ToString();

            if (t == "h") PubgWindow.Hide();
            else if (t == "s") PubgWindow.Show();
            else if (t == "clearlog") { Log.Clear(); txLog.Clear(); }
            else if (t == "scr") SGraph.Scr(full_scr_filename, PubgWindow.Width, PubgWindow.Height, PubgWindow.PosX, PubgWindow.PosY);
            else if (t == "fromf") test1(true);
            else if (t == "froms") test1(false);
            else if (t == "run") { PubgWindow.CloseSE(); PubgWindow.StartExecute(); }
            else if (t == "kill") { PubgWindow.CloseMsg(); PubgWindow.KillExecute(); PubgWindow.CloseSE(); }
            else if (t == "exit") Application.Exit();
            else if (t == "show") tray_Click(tray, null);
            else if (t == "autolauchifidle") {
                chb_AutoStartOnIdle.Checked = !chb_AutoStartOnIdle.Checked;
                ReadGui();
            }
            else if (t == "cfg") {
                string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                local += @"\TslGame\Saved\Config\WindowsNoEditor\GameUserSettings.ini";
                Shell32.ShellExecute(Handle, "open", local, null, null, User32.SW_SHOWNORMAL);
            }
            else if (t == "about") {
                string owner = Encoding.UTF8.GetString( Convert.FromBase64String( uniq ) );
                DateTime build = DateTimeExtensions.GetLinkerTime(Assembly.GetExecutingAssembly());
                string s = String.Format(" {0} {1} {2} Build time: {3} {4} {5} Owner: {6}", 
                    AppTitle, Environment.NewLine, Environment.NewLine,
                    build, Environment.NewLine, Environment.NewLine, owner);
                MessageBox.Show(s);
            }
        }

        private void tray_Click( object sender, MouseEventArgs e ) {

            if (e != null && e.Button == MouseButtons.Right) return;

            bool v = User32.IsWindowVisible(Handle) > 0;

            if (v) Hide();
            else { Show(); WindowState = FormWindowState.Normal; }

        }

        private void trayms_Opening( object sender, System.ComponentModel.CancelEventArgs e ) {

            bool stopped = BotStopper.WaitOne(0, false);

            tsmiBotStatus.Text = stopped ? "Bot [ Stopped ]" : "Bot [ Working ]";

            tsmiStartBot.Visible = stopped; tsmiStopBot.Visible = !stopped;

            tsmiAutolauchifidle.Checked = Setti.IdleAutolaunch;
        }

        private void OpenRewardsFolderClick( object sender = null, EventArgs e = null) {
            
            Shell32.ShellExecute(IntPtr.Zero, "open", PubgRound.RewardsFolder, "", "", User32.SW_SHOWNORMAL);
        }

        private void tmrIdleCheck_Tick( object sender, EventArgs e )
        {
            if (!Setti.IdleAutolaunch || !BotStopper.WaitOne(0, false)) return;
          
            if (STime.GetUserIdleTime() > Setti.IdleAutolaunchTimeout * 1000 * 60) {

                Log.Add("Auto Launch Bot! (user idle)");

                StartBotClick();
            }
        }

        private void btnttt_Click( object sender, EventArgs e ) {
            //
        }

        private void cbox_PubgInput_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( ((ComboBox)sender).SelectedIndex == 0 ) InitInput_event();
            else InitInput_message(); 
        }

        private void Form1_FormClosing( object sender, FormClosingEventArgs e ) {

            if (e.CloseReason == CloseReason.UserClosing) {

                e.Cancel = true;

                tray_Click( tray, null );

                StopBotClick();
            }
        }
    }
}
