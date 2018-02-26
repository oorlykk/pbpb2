using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SnLib;

namespace pbpb
{

    public sealed class Setti {
        //
        public static bool IsChanged;
        //
        public static bool HiddenMode = true;
        public static bool PassiveMode = true;
        public static bool SaveReward = true;
        public static bool IdleAutolaunch = true;
        public static int IdleAutolaunchTimeout = 5;
        public static int PubgWindowAbsoluteY = 0;
        public static bool CanRestartSteam = true;
        public static bool DrawScr = true;
        public static int PubgWindowAbsoluteX = Screen.PrimaryScreen.Bounds.Width + 1;

        public static void Save()  {

            IsChanged = false;

            IFormatter fo = new BinaryFormatter();

            MemoryStream ms = new MemoryStream();

            object o = SSerialize.StaticClassSave(typeof(Setti));

            fo.Serialize(ms, o);

            var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey( "PBPB" );

            key.SetValue( Form1.AppTitle, ms.ToArray() );

            key.Close();

            ms.Close();

        }

        public static bool Load()
        {   
            
            bool result = false;

            try {

                var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey( "PBPB" );

                byte[] data = (byte[]) key.GetValue( Form1.AppTitle );

                key.Close();

                MemoryStream ms = new MemoryStream();

                ms.Write( data, 0, data.Length );

                ms.Position = 0;

                IFormatter fo = new BinaryFormatter();

                object[,] o = fo.Deserialize( ms ) as object[,];

                ms.Close();

                SSerialize.StaticClassSaveLoad( typeof( Setti ), o );

                result = true;

                Log.Add("Settings loaded.");

            } catch {

                Log.Add("Default settings. " + PubgWindowAbsoluteX.ToString());
                
            }

            return result;

        }
    }

    partial class Form1
    {

        public void ReadGui( object sender = null, EventArgs e = null) {
            //
            Setti.IsChanged = true;
            //
            Setti.HiddenMode                    =      chb_HiddenMode.Checked;
            Setti.PassiveMode                   =      chb_PassiveMode.Checked;
            Setti.SaveReward                    =      chb_SaveReward.Checked;
            Setti.IdleAutolaunch                =      chb_AutoStartOnIdle.Checked;
            Setti.CanRestartSteam               =      chb_CanKillSteam.Checked;
            Setti.DrawScr                       =      chb_view.Checked;
            Setti.IdleAutolaunchTimeout         =      (int)ne_MaxIdle.Value;
            Setti.PubgWindowAbsoluteX           =      (int)ne_PosX.Value;
            Setti.PubgWindowAbsoluteY           =      (int)ne_PosY.Value;
        }

        public void WriteGui( object sender = null, EventArgs e = null ) {

            chb_HiddenMode.Checked          =      Setti.HiddenMode;
            chb_PassiveMode.Checked         =      Setti.PassiveMode;
            chb_SaveReward.Checked          =      Setti.SaveReward;
            chb_AutoStartOnIdle.Checked     =      Setti.IdleAutolaunch;
            chb_CanKillSteam.Checked        =      Setti.CanRestartSteam;
            chb_view.Checked                =      Setti.DrawScr;

            ne_MaxIdle.Value                =      Setti.IdleAutolaunchTimeout;
            ne_PosX.Value                   =      Setti.PubgWindowAbsoluteX;
            ne_PosY.Value                   =      Setti.PubgWindowAbsoluteY;
        }
    }
}
