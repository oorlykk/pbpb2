using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnLib;

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
        WrongMatchState = 0x200,
    }

    public static class PubgStatus {

        public static int LastDistance { get; private set; }

        private static PubgStatuses m_Status;

        public static PubgStatuses Status { get => m_Status; set => m_Status = value; }

        private static long m_LastGoodTick = Environment.TickCount;

        public static long LastGoodTick { get => m_LastGoodTick; private set => m_LastGoodTick = value; }

        public static void ResetLastGood() => m_LastGoodTick = Environment.TickCount; 

        public static PubgStatuses Now( PCS Pcs ) {

            PubgStatuses result = PubgStatuses.Unknown;

            Bitmap scr = SGraph.Scr( "", PubgWindow.Width, PubgWindow.Height, PubgWindow.PosX, PubgWindow.PosY );

            if (Setti.DrawScr) SGraph.DrawImage(scr, Form1.DrawScrToHandle);

            foreach (var key in Pcs) {

                PubgControl pc = key.Value;
                PubgControls pcname = key.Key;

                if (pc.IsNative) continue;

                pc.ControlImageFromImage( scr );

                LastDistance = pc.CalcDistance( true );

                if (LastDistance < 5) {

                    if (pcname.NotIn(PubgControls.btnStart, PubgControls.btnMatchCanContinue,
                                     PubgControls.labWrongMatchState)) {

                        m_LastGoodTick = Environment.TickCount;
                    }

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

                    else if (pcname == PubgControls.labWrongMatchState)
                        result |= PubgStatuses.WrongMatchState;
                }

            }

            scr.Dispose();

            m_Status = result;

            return result; 

        }

    }
}
