using System;
using System.Threading;
using SnLib;

namespace pbpb
{
    public class ___qeqweqweq {}

    partial class Form1
    {

        public const int MAX_NOLASTGOOD = (1000 * 60) * 5;

        void PubgRestarterProc() {

            do {                    

                if (Setti.PassiveMode && STime.GetUserIdleTime() < 5000) {

                    Log.Add( "(PR) No idle time for actions. Wait..." );

                    goto EXIT;
                }

                        
                if (
                    ((!Setti.PassiveMode) && (Environment.TickCount - PubgStatus.LastGoodTick > MAX_NOLASTGOOD)) 
                    ||
                    ((Setti.PassiveMode) && (Environment.TickCount - PubgStatus.LastGoodTick > MAX_NOLASTGOOD * 3))
                   ) {            

                    PubgStatus.SetLastGood();               

                    PubgWindow.KillExecute();            

                    if (Setti.CanRestartSteam) {

                        Thread.Sleep(2000);
                        PubgWindow.KillExecuteSteam();
                    }

                    goto EXIT;
                }


                if (PubgWindow.SEExists || PubgWindow.CrashExists || PubgWindow.SURExists) {
                    
                    string s = String.Format( 
                        "(PR) error found ( crash {0}, steam {1}, update {2} )", 
                        PubgWindow.CrashExists.ToYesNoString(), 
                        PubgWindow.SEExists.ToYesNoString(),
                        PubgWindow.SURExists.ToYesNoString() );
                    
                    Log.Add( s );

                    if (PubgWindow.SURExists) PubgWindow.CloseSUR();

                    PubgWindow.CloseSE();

                    if (PubgWindow.CrashExists) PubgWindow.KillCrash();  
                    
                    PubgWindow.KillExecute();

                    goto EXIT;
                }


                if (!PubgWindow.Exists) {

                    if (PubgWindow.SUExists) {

                        Log.Add( "(PR) wait steam updating..." );

                        goto EXIT;
                    }

                    PubgStatus.SetLastGood();

                    PubgWindow.StartExecute();

                    Thread.Sleep(30000);
                }

                EXIT:; 
                //nope

            } while (!BotStopper.WaitOne(90000, false));
        }
    }
}