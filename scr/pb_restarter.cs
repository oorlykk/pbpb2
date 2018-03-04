using System;
using System.Threading;
using SnLib;

namespace pbpb
{
    public class ___qeqweqweq {}

    partial class Form1
    {

        public const int MAX_NOLASTGOOD = (1000 * 60) * 6;

        void PubgRestarterProc() {

            Log.Add("(MAIN) RestarterProc enter!");

            do {

                try {

                    if (!PubgInput.CanInteract) {

                        Log.Add( "(PR) No idle time for actions. Wait..." );

                        goto EXIT;
                    }


                    if (
                        ( ( !Setti.PassiveMode ) && ( Environment.TickCount - PubgStatus.LastGoodTick > MAX_NOLASTGOOD ) )
                        ||
                        ( ( Setti.PassiveMode ) && ( Environment.TickCount - PubgStatus.LastGoodTick > MAX_NOLASTGOOD * 3 ) )
                       ) {

                        bool executed = PubgWindow.KillExecuted;
                        PubgWindow.KillExecute();

                        if (Setti.CanRestartSteam && executed) {

                            Thread.Sleep( 3000 );
                            PubgWindow.KillExecuteSteam();
                        }

                        PubgStatus.SetLastGood();

                        goto EXIT;
                    }


                    if (PubgWindow.SEExists || PubgWindow.CrashExists || PubgWindow.SURExists ||
                        PubgWindow.SCEExitst || NativeWindows.SteamGameOverlayUICrash.Exists) {

                        string s = String.Format(
                            "(PR) error found ( crash {0}, steam {1}, update {2}, conerror {3}, overlayerror {4} )",
                            PubgWindow.CrashExists.ToYesNoString(),
                            PubgWindow.SEExists.ToYesNoString(),
                            PubgWindow.SURExists.ToYesNoString(),
                            PubgWindow.SCEExitst.ToYesNoString(),
                            NativeWindows.SteamGameOverlayUICrash.Exists.ToYesNoString());

                        Log.Add( s );

                        NativeWindows.SteamGameOverlayUICrash.SetClose();

                        PubgWindow.CloseSUR();

                        PubgWindow.CloseSE();

                        PubgWindow.CloseSCE();

                        PubgWindow.KillCrash();

                        PubgWindow.KillExecute();

                        goto EXIT;
                    }


                    if (!PubgWindow.Exists) {

                        if (PubgWindow.SUExists) {

                            Log.Add( "(PR) wait steam updating..." );

                            goto EXIT;
                        }

                        PubgWindow.StartExecute();

                        Thread.Sleep( 30000 );
                    }

                    EXIT:;
                    //nope
                }
                catch (Exception e) {

                    Log.Add( "(PR) exception: " + e.Message );
                }

            } while (!BotStopper.WaitOne(30000, false));

            Log.Add("(MAIN) RestarterProc leave!");
        }
    }
}