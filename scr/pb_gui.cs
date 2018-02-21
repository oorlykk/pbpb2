using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SnLib;

namespace pbpb
{
    public struct _Settings {

        public bool HiddenMode;

        public bool PassiveMode;

        public bool SaveReward;

        public bool IdleAutolaunch;

        public int IdleAutolaunchTimeout;

        public _Settings( bool tryloadfromregedit )
        {
            if (tryloadfromregedit) {

                this = Load();

            }

            else {

                HiddenMode = false;
                PassiveMode = true;
                SaveReward = true;
                IdleAutolaunch = true;
                IdleAutolaunchTimeout = 5;

            }
        }

        public void Save()  {

            byte[] data = SStruct.RawSerialize( this );

            Microsoft.Win32.RegistryKey exampleRegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey( "PBPB" );

            exampleRegistryKey.SetValue( "settings", data );

            exampleRegistryKey.Close();
        }

        private static _Settings Load()
        {

            try {

                Microsoft.Win32.RegistryKey exampleRegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey( "PBPB" );

                byte[] data = (byte[]) exampleRegistryKey.GetValue( "settings" );

                exampleRegistryKey.Close();

                return SStruct.ReadStruct<_Settings>(data);
                          
            }
            catch {

                return new _Settings(true);
            }

        }
    }

    partial class Form1
    {

        public _Settings Settings;
        
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
