using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnLib;
using Win32;

namespace pbpb {

    [Flags]
    public enum PubgStatuses {
        None = 0x0,
        Unknown = 0x1,
        Lobby = 0x2,
        Prepare = 0x4,
        Eject = 0x8,
        Parachute = 0x10,
        Alive = 0x20,
        ExitToLobby = 0x40,
        MatchCanContinue = 0x80,
        Water = 0x100,
        //WrongMatchState = 0x200,
        MainManuExit = 0x200,
    }

    public static class PubgStatus {

        public static int LastDistance { get; private set; }

        private static PubgStatuses m_Status;

        public static PubgStatuses Status { get => m_Status; set => m_Status = value; }

        private static int m_LastGoodTick = Environment.TickCount;

        public static int LastGoodTick { get => m_LastGoodTick; }

        public static void SetLastGood() => m_LastGoodTick = Environment.TickCount; 

        public static Bitmap RawScr;

        public static PubgStatuses Now( PCS Pcs ) {

            PubgStatuses result = PubgStatuses.Unknown;

            if (RawScr != null) {RawScr.Dispose(); RawScr = null;}

            RawScr = SGraph.Scr( "", PubgWindow.Width, PubgWindow.Height, PubgWindow.PosX, PubgWindow.PosY );
             
            if (RawScr == null) goto EXIT;
            
            foreach (var key in Pcs) {

                PubgControl pc = key.Value;
                PubgControls pcname = key.Key;

                if (pc.IsNative) continue;

                pc.ControlImageFromImage( RawScr );          

                LastDistance = pc.CalcDistance( true );

                if (LastDistance < 5) {

                    if (PubgStatuses.Unknown.HasFlag(result))
                        result ^= PubgStatuses.Unknown;

                    if (pcname == PubgControls.btnStart) 
                        result |= PubgStatuses.Lobby;

                    else if (pcname == PubgControls.btnExit) 
                        result |= PubgStatuses.ExitToLobby;

                    else if (pcname == PubgControls.labJoined) 
                        result |= PubgStatuses.Prepare;

                    else if (pcname == PubgControls.labEject) 
                        result |= PubgStatuses.Eject;

                    else if (pcname == PubgControls.labReleaseParachute) 
                        result |= PubgStatuses.Parachute;

                    else if (pcname == PubgControls.labAlive) 
                        result |= PubgStatuses.Alive;

                    else if (pcname == PubgControls.btnMatchCanContinue) 
                        result |= PubgStatuses.MatchCanContinue;

                    else if (pcname == PubgControls.labWater)
                        result |= PubgStatuses.Water;

                    else if (pcname == PubgControls.labMainManu)
                        result |= PubgStatuses.MainManuExit;
                }

            }

            if (!result.HasFlags( PubgStatuses.Unknown ) &&
                !result.HasFlags( PubgStatuses.Lobby ) &&
                !result.HasFlags( PubgStatuses.MatchCanContinue ) &&
                !result.HasFlags( PubgStatuses.MainManuExit )) {

                SetLastGood();
            }

            User32.SendMessage((IntPtr)User32.FindWindow(null, Form1.AppTitle), Form1.WM_SCRUPDATE, 0, 0);
            User32.SendMessage((IntPtr)User32.FindWindow(null, Form1.ViewFormTitle), Form1.WM_SCRUPDATE, 0, 0);            

            Application.DoEvents();          

            EXIT:

            m_Status = result;

            return result; 

        }

    }
}
