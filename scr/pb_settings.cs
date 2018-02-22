using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SnLib;

namespace pbpb
{

    public class Settings {

        public static bool HiddenMode = false;
        public static bool PassiveMode = false;
        public static bool SaveReward = true;
        public static bool IdleAutolaunch = true;
        public static int IdleAutolaunchTimeout = 5;
        public static int PubgWindowAbsoluteX = Screen.PrimaryScreen.Bounds.Width + 1;
        public static int PubgWindowAbsoluteY = 0;

        public static void Save()  {

            IFormatter fo = new BinaryFormatter();

            MemoryStream ms = new MemoryStream();

            object o = SSerialize.StaticClassSave(typeof(Settings));

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

                SSerialize.StaticClassSaveLoad( typeof( Settings ), o );

                result = true;

            } catch { }

            return result;

        }
    }

    partial class Form1
    {

        public void ReadGui( object sender = null, EventArgs e = null) {

            Settings.HiddenMode                    =      chb_HiddenMode.Checked;
            Settings.PassiveMode                   =      chb_PassiveMode.Checked;
            Settings.SaveReward                    =      chb_SaveReward.Checked;
            Settings.IdleAutolaunch                =      chb_AutoStartOnIdle.Checked;
            Settings.IdleAutolaunchTimeout         =      (int)ne_MaxIdle.Value;
            Settings.PubgWindowAbsoluteX           =      (int)ne_PosX.Value;
            Settings.PubgWindowAbsoluteY           =      (int)ne_PosY.Value;
        }

        public void WriteGui( object sender = null, EventArgs e = null ) {

            chb_HiddenMode.Checked          =      Settings.HiddenMode;
            chb_PassiveMode.Checked         =      Settings.PassiveMode;
            chb_SaveReward.Checked          =      Settings.SaveReward;
            chb_AutoStartOnIdle.Checked     =      Settings.IdleAutolaunch;
            ne_MaxIdle.Value                =      Settings.IdleAutolaunchTimeout;
            ne_PosX.Value                   =      Settings.PubgWindowAbsoluteX;
            ne_PosY.Value                   =      Settings.PubgWindowAbsoluteY;
        }
    }
}
