using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SnLib;
using Win32;

namespace pbpb
{

    partial class Form1
    {

        public const int MAX_NOLASTGOOD_FOR_INPUT = ( 1000 * 60 ) / 2;

        public static bool IsPositiveTimeForInput => (!Setti.PassiveMode) || (Setti.PassiveMode && STime.GetUserIdleTime() > 5000);

        void PubgStatusProc() {        

            while (!BotStopper.WaitOne(2000, false)) {          

                if (!PubgWindow.Exists) {

                    Log.Add( "(PS) No way :( Wait..." );
                    continue;
                }

                PubgWindow.HideBE();

                if ((PubgInput.GetType() == typeof(PubgInput)) && ( !IsPositiveTimeForInput )) {

                    Log.Add( "(PS) No idle time for actions. Wait..." );
                    continue;
                }
                
                PubgWindow.Show();

                Thread.Sleep(500);

                if (PubgWindow.NeedSetupWindow) {

                    PubgWindow.SetupWindow();
                    Log.Add( String.Format( "(PW) Setup {1} => {0}", PubgWindow.PartFullHD, PubgWindow.Handle ) );
                }

                Thread.Sleep(500);

                PubgStatuses ps = PubgStatus.Now( Pcs );              

                Log.Add( String.Format( "(PS) {0}", ps ) );

                if (AppIsExp) ps = PubgStatuses.Unknown;  //Magic EXP!

                if (Setti.DrawScr) {

                    Form1 f = (Form1) Form.ActiveForm;
                    if (f != null && f.Visible && PubgStatus.RawScr != null) {
                        
                        f.PanelView.BackgroundImage = new Bitmap( PubgStatus.RawScr );
                    }
                }

                if ( !ps.HasFlags(PubgStatuses.Unknown) && 
                     !ps.HasFlags(PubgStatuses.Lobby) &&
                     !ps.HasFlags(PubgStatuses.MatchCanContinue) ) {

                    PubgStatus.SetLastGood();
                }

                if (PubgStatuses.Unknown.HasFlags( ps )) {

                    Log.Append( " di: " + PubgStatus.LastDistance.ToString() );

                    if (Environment.TickCount - PubgStatus.LastGoodTick > MAX_NOLASTGOOD_FOR_INPUT) {

                        PubgInput.ClickCenter();

                        Thread.Sleep( 1500 );

                        if (RND.Next( 2 ) == 0) PubgInput.MoveMouse( 100, 100 );
                                           else PubgInput.MoveMouse( -200, -200 );

                        Log.Add( "(PS) Add input (lastgood is long)" );                        
                    }
                }

                else if (PubgStatuses.MatchCanContinue.HasFlags( ps )) {

                    Log.Append( " di: " + Pcs[PubgControls.btnMatchCanContinueContinue].LastDistance.ToString() );

                    if (PubgRound.ContinueClickCount < 2) {

                        Log.Add( "(PS) click MatchCanContinue" );

                        PubgRound.ContinueClickCount += 1;
                        Pcs[PubgControls.btnMatchCanContinueContinue].ClickLeftMouse();
                    }
                    else {

                        Log.Add( "(PS) click MatchCanContinueCancel ( > cont index )" );

                        PubgRound.ContinueClickCount = 0;
                        Pcs[PubgControls.btnMatchCanContinueCancel].ClickLeftMouse();
                    }
                }

                else if (PubgStatuses.Lobby.HasFlags( ps )) {

                    Log.Add( "click Start" );

                    PubgInput.EjectClickedTime = int.MaxValue;

                    PubgInput.ParachuteClickedTime = int.MaxValue;

                    Log.Append( " di: " + Pcs[PubgControls.btnStart].LastDistance.ToString() );

                    Pcs[PubgControls.btnStart].ClickLeftMouse();               

                    Thread.Sleep(5000);

                }
                else if (PubgStatuses.ExitToLobby.HasFlags( ps )) {

                    Log.Append( " di: " + Pcs[PubgControls.btnExit].LastDistance.ToString() );

                    Thread.Sleep( 7000 );

                    PubgRound.End( !PubgRound.RewardSaved && Setti.SaveReward, "dead" );

                    if ((PubgInput.GetType() == typeof(_PubgInputMessage)) && (!IsPositiveTimeForInput)) {

                        Log.Add("Can't exit (PassiveMode: no idle time)");

                        goto EXIT;
                    }

                    bool inputswitched = false;

                    if ( PubgInput.GetType() == typeof(_PubgInputMessage) ) {

                        PubgInput.KeyPress(Keys.Escape);

                        inputswitched = true;

                        InitInput_event();
                        
                    } else {

                        Pcs[PubgControls.btnExit].ClickLeftMouse();

                        Log.Add( "click ExitToLobby" );
                    }                                
                    
                    Thread.Sleep( 3000 );

                    Pcs[PubgControls.btnConfirmExit].ClickLeftMouse();

                    Log.Add( "click ExitToLobby confirm" );

                    if (inputswitched) InitInput_message();

                }

                else if (PubgStatuses.Eject.HasFlags( ps )) {

                    Log.Append( " di: " + Pcs[PubgControls.labEject].LastDistance.ToString() );

                    PubgInput.EjectClickedTime = Environment.TickCount;
                    if (RND.Next( 1 ) == 0) {
                        Thread.Sleep(RND.Next(300)); Thread.Sleep(RND.Next(300)); Thread.Sleep(RND.Next(300));
                        Thread.Sleep(RND.Next(300)); Thread.Sleep(RND.Next(300)); Thread.Sleep(RND.Next(300));
                        Thread.Sleep(RND.Next(300)); Thread.Sleep(RND.Next(300)); Thread.Sleep(RND.Next(300));
                        PubgInput.Eject();
                    } else Log.Add( "skip EJECT press (no lucky)" );

                }

                else if (PubgStatuses.Parachute.HasFlags( ps )) {

                    Log.Append( " di: " + Pcs[PubgControls.labReleaseParachute].LastDistance.ToString() );

                    PubgInput.ParachuteClickedTime = Environment.TickCount;
                    if (RND.Next( 1 ) == 0) {
                        PubgInput.Parachute(); Thread.Sleep(100);
                        PubgInput.Back();
                    }
                    else Log.Add( "skip Parachute press (no lucky)" );

                }

                else if (PubgStatuses.Prepare.HasFlags( ps )) {

                    Log.Append( " di: " + Pcs[PubgControls.labJoined].LastDistance.ToString() );

                    PubgRound.Set();
                }

                else if (PubgStatuses.Water.HasFlags( ps )) {

                    Log.Append( " di: " + Pcs[PubgControls.labWater].LastDistance.ToString() );

                    if (!PubgRound.WaterAssisted) {

                        Log.Add( "(PS) Water Assist" );

                        PubgInput.AssistInWater();

                        PubgRound.WaterAssisted = true;
                    }
                }

                else if (PubgStatuses.Alive.HasFlags( ps )) {

                    Log.Append( " di: " + Pcs[PubgControls.labAlive].LastDistance.ToString() );

                    PubgRound.Set();

                    int cd = (1000 * 60 * 2) + 45000;
                    if ( (Environment.TickCount - PubgInput.EjectClickedTime > cd ||
                          Environment.TickCount - PubgInput.ParachuteClickedTime > cd )) 
                    {
                        PubgInput.EjectClickedTime = int.MaxValue;
                        PubgInput.ParachuteClickedTime = int.MaxValue;
                        PubgInput.Down();
                    }
                }
                
                EXIT:

                if (Setti.HiddenMode) PubgWindow.Hide();

            }
        }
    }
}