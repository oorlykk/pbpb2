using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace pbpb
{
    public static class Log {

        public static int MasSize = (1000*1000)*50;

        public static void show_error( string str ) {
            MessageBox.Show(str);
        }

        public static event ResolveEventHandler LogEvent;

        private static string m_last;

        private static string __initstr => "Init [ " + DateTime.Now.ToString() + " ] build: " + Form1.AppBuild;    

        private static StringBuilder m_Log = new StringBuilder();

        private static void add_str(string str) {
            if (Last == "") m_Log.Append(__initstr);

            m_Log.Append(str);
            m_last = str;

            LogEvent?.Invoke( null, new ResolveEventArgs(m_last) );
        }

        public static void Clear() => m_Log.Clear();

        public static void Append(string str) => add_str(str);

        public static void Add(string str) {
            
            if (Text.Length > MasSize) Clear();

            string t = DateTime.Now.ToLongTimeString();
            string s = String.Format("[{0}] {1}", t, str);
            add_str( Environment.NewLine + s );

        }

        public static string Text => m_Log.ToString();

        public static string Last => m_last;

        public static void Save(string filename) {

            StreamWriter file = new StreamWriter( filename );
            file.WriteLine( m_Log.ToString() );
            file.Close();
        }

        public static string Save() {

            string filename = Path.GetTempPath() + PubgRound.GetRewardName() + ".txt";
            Save(filename);
            return filename;
        }
    }

}
