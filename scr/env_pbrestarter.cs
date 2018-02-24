using System;
using System.Threading;
using SnLib;

namespace pbpb
{

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
                   
                    Log.Add( String.Format( "(PR) KillExecute! (lastgood is so long)") );

                    PubgStatus.ResetLastGood();               

                    PubgWindow.KillExecute();            

                    if (Setti.CanRestartSteam) {

                        Log.Add( String.Format( "(PR) KillExecuteSteam! (lastgood is so long)") );

                        Thread.Sleep(1000);

                        PubgWindow.KillExecuteSteam();
                    }

                    goto EXIT;
                }


                if (PubgWindow.SEExists || PubgWindow.CrashExists || PubgWindow.SURExists) {
                    
                    string s = String.Format( 
                        "(PR) KillExecute! (error => crash {0}, steam {1}, update {2})", 
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

                    PubgStatus.ResetLastGood();

                    PubgWindow.StartExecute();

                    Log.Add( "(PR) StartExecute! (no way)" );

                    Thread.Sleep(30000);
                }

                EXIT:; 
                //nope

            } while (!BotStopper.WaitOne(90000, false));
        }
    }
}