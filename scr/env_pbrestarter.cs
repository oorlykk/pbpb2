using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using SnLib;
using Win32;
using IH = ImageHasher;

namespace pbpb {

    public partial class Form1
    {

        public const int MAX_NOLASTGOOD = (1000 * 60) * 5;

        void PubgRestarterProc() {

            do {            

                if (Environment.TickCount - PubgStatus.LastGoodTick > MAX_NOLASTGOOD) {

                    PubgStatus.ResetLastGood();

                    Log.Add( String.Format( "(PR) KillExecute! (lastgood is so long)") );

                    string filename = RewardsFolder + @"llg\" + RewardNewName + ".jpg";
                    //SGraph.Scr( filename, PubgWindow.Width, PubgWindow.Height, 0, 0, true);

                    PubgWindow.KillExecute();

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

                    if (PubgWindow.SEExists) PubgWindow.CloseSE();

                    if (PubgWindow.CrashExists) PubgWindow.KillCrash();  
                    
                    PubgWindow.KillExecute();

                    goto EXIT;
                }

                if (PubgWindow.BEVisible) {

                    PubgWindow.HideBE();

                    Log.Add( String.Format( "(PR) Hide BEye => {0}", PubgWindow.BEHandle ) );
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

            } while (!BotStopper.WaitOne(30000, false));
        }
    }
}