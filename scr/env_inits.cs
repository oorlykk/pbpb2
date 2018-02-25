using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using snlib = SnLib;
using Win32;
using IH = ImageHasher;

namespace pbpb {  

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

            Pcs.Add( PubgControls.btnMatchCanContinue, new PubgControl( PubgControls.btnMatchCanContinue.ToString(),
                                                    (ulong) PubgControls.btnMatchCanContinue,
                                                    426, 300, 483, 320 ) );

            Pcs.Add( PubgControls.labWater, new PubgControl( PubgControls.labWater.ToString(),
                                                    (ulong) PubgControls.labWater,
                                                    587, 507, 613, 526 ) );          

            Pcs.Add( PubgControls.btnSoloSquad, new PubgControl( PubgControls.btnSoloSquad.ToString(),
                                                    (ulong) PubgControls.btnSoloSquad,
                                                    68, 395 ) );

            Pcs.Add( PubgControls.btnConfirmExit, new PubgControl( PubgControls.btnConfirmExit.ToString(),
                                                    (ulong) PubgControls.btnConfirmExit,
                                                    420, 290 ) );

            Pcs.Add( PubgControls.labWrongMatchState, new PubgControl( PubgControls.labWrongMatchState.ToString(),
                                                    (ulong) PubgControls.labWrongMatchState,
                                                    311, 246, 462, 264 ) );

        }
    }

}
