using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SnLib;

namespace pbpb
{
    [Serializable]
    public class _Settings {

        public bool HiddenMode = true;
        public bool PassiveMode = true;
        public bool SaveReward = true;
        public bool IdleAutolaunch = true;
        public int IdleAutolaunchTimeout = 5;

        public void Save()
        {
            using (MemoryStream stream = new MemoryStream()) {

                IFormatter formatter = new BinaryFormatter(); 

                formatter.Serialize(stream, this);  

                Microsoft.Win32.RegistryKey exampleRegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey( "PBPB" );

                exampleRegistryKey.SetValue( "settings", stream.ToArray() );

                exampleRegistryKey.Close();

            }
        }

        public static _Settings Load()
        {
            MemoryStream stream = new MemoryStream();

            try {

                Microsoft.Win32.RegistryKey exampleRegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey( "PBPB" );

                byte[] data = (byte[]) exampleRegistryKey.GetValue( "settings" );

                exampleRegistryKey.Close();              

                stream.Write( data, 0, data.Length );

                stream.Position = 0;

                IFormatter formatter = new BinaryFormatter();

                return (_Settings) formatter.Deserialize( stream );

            }
            catch {

                return new _Settings();
            }
            finally {

                stream.Close();
            }
        }
    }

    partial class Form1
    {

        public _Settings Settings = new _Settings();
        
        void ReadGui( object sender = null, EventArgs e = null) {

            Settings.HiddenMode                    =      chb_HiddenMode.Checked;
            Settings.PassiveMode                   =      chb_PassiveMode.Checked;
            Settings.SaveReward                    =      chb_SaveReward.Checked;
            Settings.IdleAutolaunch                =      chb_AutoStartOnIdle.Checked;
            Settings.IdleAutolaunchTimeout         =      (int)ne_MaxIdle.Value;
        }

        void WriteGui( object sender = null, EventArgs e = null ) {

            chb_HiddenMode.Checked          =      Settings.HiddenMode;
            chb_PassiveMode.Checked         =      Settings.PassiveMode;
            chb_SaveReward.Checked          =      Settings.SaveReward;
            chb_AutoStartOnIdle.Checked     =      Settings.IdleAutolaunch;
            ne_MaxIdle.Value                =      Settings.IdleAutolaunchTimeout;
        }
    }
}
