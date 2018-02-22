using System;
using System.IO;
using System.Threading;
using SnLib;
using Win32;

namespace pbpb
{

    partial class Form1
    {

        public const int MAX_NOLASTGOOD_FOR_INPUT = ( 1000 * 60 ) / 2;

        void PubgStatusProc() {        

            while (!BotStopper.WaitOne(3000, false)) {             

                if (Settings.PassiveMode && STime.GetUserIdleTime() < 5000) {

                    Log.Add( "(PS) No idle time for actions. Wait..." );
                    continue;
                }

                if (!PubgWindow.Exists) {
                    Log.Add( "(PS) No way :( Wait..." );
                    continue;
                }
                
                PubgWindow.Show();

                if (PubgWindow.NeedSetupWindow) {

                    PubgWindow.SetupWindow();
                    Log.Add( String.Format( "(PW) Setup {1} => {0}", PubgWindow.PartFullHD, PubgWindow.Handle ) );
                }

                bool needfocus = !PubgWindow.IsFocused;
                if (needfocus) {

                    PubgWindow.SetFocus();

                    Log.Add( String.Format( "(PW) Focus => {0}", PubgWindow.Handle ) );
                }

                PubgStatuses ps = PubgStatus.Now( Pcs );              

                Log.Add( String.Format( "(PS) {0}", ps ) );

                if (AppIsExp) ps = PubgStatuses.Unknown;  //Magic EXP!

                if (PubgStatuses.Unknown.HasFlags( ps )) {

                    Log.Append( " di: " + PubgStatus.LastDistance.ToString() );

                    if (Environment.TickCount - PubgStatus.LastGoodTick > MAX_NOLASTGOOD_FOR_INPUT) {

                        PubgInput.ClickCenter();

                        Thread.Sleep( 1500 );

                        if (RND.Next( 2 ) == 0) PubgInput.MoveMouse( -700, 0 );
                                           else PubgInput.MoveMouse( 0, -700 );

                        Log.Add( "+Add input (lastgood is long)" );                        
                    }
                }

                else if (PubgStatuses.MatchCanContinue.HasFlags( ps )) {

                    Log.Append( " di: " + Pcs[PubgControls.btnMatchCanContinue].LastDistance.ToString() );

                    Pcs[PubgControls.btnMatchCanContinue].ClickLeftMouse();

                    Log.Add( "click MatchCanContinue" );
                }

                else if (PubgStatuses.Lobby.HasFlags( ps )) {

                    PubgInput.EjectClickedTime = int.MaxValue;

                    PubgInput.ParachuteClickedTime = int.MaxValue;

                    Log.Append( " di: " + Pcs[PubgControls.btnStart].LastDistance.ToString() );

                    Pcs[PubgControls.btnStart].ClickLeftMouse();

                    Log.Add( "click Start" );

                    Thread.Sleep(5000);

                }
                else if (PubgStatuses.ExitToLobby.HasFlags( ps )) {

                    Log.Append( " di: " + Pcs[PubgControls.btnExit].LastDistance.ToString() );

                    Thread.Sleep( 7500 );

                    if (Settings.SaveReward) {
                        
                        if (!Directory.Exists(RewardsFolder))
                            try {
                                Directory.CreateDirectory(RewardsFolder);
                            } catch (Exception e) { e.ToString(); }

                        string filename = RewardsFolder + RewardNewName + ".jpg";

                        Log.Add( "Save reward " + filename );

                        SGraph.Scr( filename, PubgWindow.Width, PubgWindow.Height, PubgWindow.PosX, PubgWindow.PosY, true);
                    }

                    Pcs[PubgControls.btnExit].ClickLeftMouse();

                    Log.Add( "click ExitToLobby" );

                    Thread.Sleep( 1500 );

                    Pcs[PubgControls.btnConfirmExit].ClickLeftMouse();

                    Log.Add( "click ExitToLobby confirm" );
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
                }

                else if (PubgStatuses.Water.HasFlags( ps )) {

                    Log.Append( " di: " + Pcs[PubgControls.labWater].LastDistance.ToString() );  
                    PubgInput.ReleaseValueKeys();
                    Log.Append( "(PH) Release all value keys W,S,..>");
                    Thread.Sleep(100);
                    PubgInput.Forward();
                }

                else if (PubgStatuses.Alive.HasFlags( ps )) {

                    Log.Append( " di: " + Pcs[PubgControls.labAlive].LastDistance.ToString() );

                    //PubgInput.MoveMouse(-150, -150);

                    int cd = (1000 * 60 * 2) + 45000;
                    if ( (Environment.TickCount - PubgInput.EjectClickedTime > cd ||
                          Environment.TickCount - PubgInput.ParachuteClickedTime > cd )) 
                    {
                        PubgInput.EjectClickedTime = int.MaxValue;
                        PubgInput.ParachuteClickedTime = int.MaxValue;
                        PubgInput.Down(); Thread.Sleep(100);
                        PubgInput.Forward();
                    }
                }

                if (Settings.HiddenMode) PubgWindow.Hide();

                if (needfocus) {
                    PubgWindow.RestoreFocus();
                    Log.Add( String.Format( "(PW) Focus restore => {0}", PubgWindow.PredFocus ) );
                }

            }
        }
    }
}