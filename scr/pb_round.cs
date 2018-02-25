using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnLib;

namespace pbpb
{
    public static class PubgRound {

        public static bool IsLive;

        public static int StartedTime;

        public static int EndedTime;

        public static string EndReason = "none";

        //public static string RewardsFolder = AppDomain.CurrentDomain.BaseDirectory + @"rewards\";
        public static string RewardsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\rewards\";

        public static bool RewardSaved;

        public static bool WaterAssisted;

        public static bool WrongMatchStateFound = false;

        private static string GetRewardName() {

            long t = Environment.TickCount - StartedTime;

            string st = STime.TickToStr( t ).Replace( ":", "." );

            string nt = DateTime.Now.ToString().Replace( ":", "." );

            return nt + " - " + st;
        }

        public static bool SaveReward() {

            try {

                if (!Directory.Exists( RewardsFolder ))
                    Directory.CreateDirectory( RewardsFolder );

                string filename = RewardsFolder + GetRewardName() + ".jpg";

                SGraph.Scr( filename, PubgWindow.Width, PubgWindow.Height, PubgWindow.PosX, PubgWindow.PosY, true );

                RewardSaved = true;

                Log.Add( "Reward saved " + filename );

                return true;

            }
            catch {

                Log.Add( "Can't save reward... error" );

                return false;
            }
        }

        public static void Set() {

            if (IsLive) return;

            StartedTime = Environment.TickCount;
            IsLive = true;
            RewardSaved = false;

            Log.Add("New Round Set.");
        }

        public static void End(bool savereward = false, string endreason = "") {
            
            EndedTime = Environment.TickCount;
            IsLive = false;
            WaterAssisted = false;

            if (savereward) SaveReward();                

            if (endreason != "") {

                EndReason = endreason;
                Log.Add( String.Format("Round End ({0})", endreason) );
            }
            
        }

    }

    partial class Form1 {

        //public PubgRound PubgRound = null;
    }
}
