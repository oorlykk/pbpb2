using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnLib;
using Win32;

namespace pbpb {

    
	public class ___wqeqweqweq {}

    partial class Form1
    {
        public static PCS Pcs = new PCS();

        void Init_Pcs() {

            Pcs.Add( PubgControls.btnStart, new PubgControl( PubgControls.btnStart.ToString(),
                                                    (ulong) PubgControls.btnStart,
                                                    30, 487, 142, 525 ) );

            Pcs.Add( PubgControls.btnExit, new PubgControl( PubgControls.btnExit.ToString(),
                                                    (ulong) PubgControls.btnExit,
                                                    803, 462, 913, 493 ) );

            Pcs.Add( PubgControls.labAlive, new PubgControl( PubgControls.labAlive.ToString(),
                                                    (ulong) PubgControls.labAlive,
                                                    915, 19, 938, 30 ) );

            Pcs.Add( PubgControls.labJoined, new PubgControl( PubgControls.labJoined.ToString(),
                                                    (ulong) PubgControls.labJoined,
                                                    905, 15, 942, 35 ) );

            Pcs.Add( PubgControls.labEject, new PubgControl( PubgControls.labEject.ToString(),
                                                    (ulong) PubgControls.labEject,
                                                    554, 304, 580, 321 ) );

            Pcs.Add( PubgControls.labReleaseParachute, new PubgControl( PubgControls.labReleaseParachute.ToString(),
                                                    (ulong) PubgControls.labReleaseParachute,
                                                    595, 311, 635, 316 ) );

            Pcs.Add( PubgControls.btnMatchCanContinueContinue, new PubgControl( PubgControls.btnMatchCanContinueContinue.ToString(),
                                                    (ulong) PubgControls.btnMatchCanContinueContinue,
                                                    426, 300, 483, 320 ) );

            Pcs.Add( PubgControls.btnMatchCanContinueCancel, new PubgControl( PubgControls.btnMatchCanContinueCancel.ToString(),
                                                     (ulong) PubgControls.btnMatchCanContinueCancel,
                                                     510, 310 ) );

            Pcs.Add( PubgControls.labWater, new PubgControl( PubgControls.labWater.ToString(),
                                                    (ulong) PubgControls.labWater,
                                                    587, 507, 613, 526 ) );          

            //Pcs.Add( PubgControls.labWrongMatchState, new PubgControl( PubgControls.labWrongMatchState.ToString(),
            //                                        (ulong) PubgControls.labWrongMatchState,
            //                                        311, 246, 462, 264 ) );

            Pcs.Add( PubgControls.labMainManu, new PubgControl( PubgControls.labMainManu.ToString(),
                                                    (ulong) PubgControls.labMainManu,
                                                    382, 215, 465, 224 ) );

            Pcs.Add( PubgControls.btnSoloSquad, new PubgControl( PubgControls.btnSoloSquad.ToString(),
                                                    (ulong) PubgControls.btnSoloSquad,
                                                    68, 395 ) );

            Pcs.Add( PubgControls.btnConfirmExit, new PubgControl( PubgControls.btnConfirmExit.ToString(),
                                                    (ulong) PubgControls.btnConfirmExit,
                                                    420, 298 ) );

        }

        public const int WM_ACTIVATEAPP = User32.WM_USER + 0x0001;

        public const int WM_SCRUPDATE = User32.WM_USER + 0x0002;

        protected override void WndProc( ref Message m )
        {

            if (m.Msg == WM_ACTIVATEAPP) {

                Show(); WindowState = FormWindowState.Normal;
            } 
            else if (m.Msg == WM_SCRUPDATE) {

                if (Visible && Setti.DrawScr /*&& User32.FindWindow( null, Form1.ViewFormTitle ) == 0*/) {

                    PanelView.BackgroundImage.Dispose();
                    PanelView.BackgroundImage = null;
                    if (PubgStatus.RawScr != null) 
                        PanelView.BackgroundImage = new Bitmap(PubgStatus.RawScr);                                    
                }

                string rtimestr = STime.TickToStr( Environment.TickCount - PubgRound.StartedTime );
                lab_CurrentRounTime.Text = String.Format( "{0}", rtimestr );
            }

            base.WndProc( ref m );
        }


        bool AppIsLaunched => User32.FindWindow( null, Form1.AppTitle ) > 0;

        void AppActivate() => 
            User32.SendMessage( (IntPtr) User32.FindWindow( null, AppTitle ), WM_ACTIVATEAPP, 0, 0 );


        void Init_HotKeysMon() {

            Task.Run( () => {

                bool IsPres( Keys key ) => ( User32.GetAsyncKeyState( (int) key ) < 0 );         
                
                while (IsHandleCreated) {
                    try {
                        if (IsPres( Keys.Pause )) {

                            bool launched = !BotStopper.WaitOne( 0, false );
                            string msg = "Bot " + ( launched ? "stopped" : "launched" );
                            if (launched) {
                                StopBotClick();
                                PubgWindow.CloseMsg(); PubgWindow.KillExecute(); PubgWindow.HideBE();
                                Log.Add( msg + " [user key]" );
                            }
                            else {
                                StartBotClick();
                                Log.Add( msg + " [user key]" );
                            }
                            tray.BalloonTipText = msg;
                            tray.ShowBalloonTip( 2000 );
                            Thread.Sleep( 3000 );
                        }
                    }
                    catch (Exception e) {

                        Log.Add("HotKeysMon exception..." + e.Message);
                    }
                    Thread.Sleep(50);
                }
                
            } );
        }

        // ?
        void test1(bool fromfile) {

            Bitmap scr;

            if (fromfile)
                 scr = new Bitmap(full_scr_filename);
            else
                 scr = SGraph.Scr("", PubgWindow.Width, PubgWindow.Height, PubgWindow.PosX, PubgWindow.PosY);

            PubgControl pc = Pcs[PubgControls.labMainManu];
            pc.ControlImageFromImage(scr);          
            int dist = pc.CalcDistance(true);

            Log.Add( String.Format("calc: {0} , cmp: {1}{2}=> dist: {3}", 
                pc.ControlImageHash, pc.ComparableHash, Environment.NewLine, dist) );

            scr.Save( filename_now );
            scr.Dispose();
            pc.ControlImage.Save( filename_now + ".bmp" );

        }
   }
}