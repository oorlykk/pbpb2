using System;
using System.Threading;
using System.Windows.Forms;
using Win32;
using snlib = SnLib;

namespace pbpb
{

    public static class PubgWindow {

        private static IntPtr FindWindow(string WindowName) => (IntPtr)User32.FindWindow( null, WindowName );

        private static int m_partfullhd;

        public static int PartFullHD { get => m_partfullhd; set => m_partfullhd = value; }

        public static int Width => (1920 / 10) * m_partfullhd;

        public static int Height => (1080 / 10) * m_partfullhd;

        public static int PosX = 0;
        public static int PosY = 0;

        public static void KillExecute() =>
            Shell32.ShellExecute(IntPtr.Zero, "open", "taskkill.exe", "/f /im TslGame.exe", "", User32.SW_HIDE);

        public static void StartExecute() =>
            Shell32.ShellExecute(IntPtr.Zero, "open", "steam://rungameid/578080", "", "", User32.SW_SHOWNORMAL);

        public static IntPtr Handle => FindWindow("PLAYERUNKNOWN'S BATTLEGROUNDS ");
        public static bool Exists => !Handle.Equals(IntPtr.Zero);
        public static void CloseMsg() {

            User32.SendMessage(Handle, User32.WM_CLOSE, 0, 0);
            User32.SendMessage(Handle, User32.WM_QUIT, 0, 0);
            User32.SendMessage(Handle, User32.WM_DESTROY, 0, 0);

        } 

        public static IntPtr CrashHandle => FindWindow("BATTLEGROUNDS Crash Reporter");
        public static bool CrashExists => !CrashHandle.Equals(IntPtr.Zero);
        public static void KillCrash()
        {
            User32.SetForegroundWindow( CrashHandle );
            Thread.Sleep( 150 );

            snlib.SKeybd.KeyPress( Keys.Tab );
            Thread.Sleep( 50 );
            snlib.SKeybd.KeyPress( Keys.Space ); 

            User32.SendMessage( SEHandle, User32.WM_CLOSE, 0, 0 );
            User32.SendMessage( SEHandle, User32.WM_QUIT, 0, 0 );
            User32.SendMessage( SEHandle, User32.WM_DESTROY, 0, 0 );

            Shell32.ShellExecute(IntPtr.Zero, "open", "taskkill.exe", "/f /im BroCrashReporter.exe", "", User32.SW_HIDE);
        }

        public static IntPtr BEHandle => FindWindow("BattlEye Launcher" );
        public static bool BEVisible => User32.IsWindowVisible(BEHandle) > 0;
        public static void HideBE() => User32.ShowWindow(BEHandle, User32.SW_HIDE);

        // Steam Error
        public static IntPtr SEHandle {
            get {

                IntPtr result = FindWindow("Steam - Error");

                if (result.ToInt32() <= 0)
                    result = FindWindow("Steam — Ошибка");

                return result;
            }
        }   
        public static bool SEExists => !SEHandle.Equals(IntPtr.Zero);
        public static void CloseSE()
        {

            User32.SetForegroundWindow( SEHandle );

            Thread.Sleep( 50 );

            snlib.SKeybd.KeyPress( Keys.Space );

            User32.SendMessage( SEHandle, User32.WM_CLOSE, 0, 0 );
            User32.SendMessage( SEHandle, User32.WM_QUIT, 0, 0 );
            User32.SendMessage( SEHandle, User32.WM_DESTROY, 0, 0 );

        }   

        //Steam Update Ready
        public static IntPtr SURHandle {
            get {

                IntPtr result = FindWindow("Ready - PLAYERUNKNOWN'S BATTLEGROUNDS");

                if (result.ToInt32() <= 0)
                    result = FindWindow("Готово — PLAYERUNKNOWN'S BATTLEGROUNDS");

                return result;
            }
        }
        public static bool SURExists => !SURHandle.Equals(IntPtr.Zero);
        public static void CloseSUR() {

            User32.SendMessage( SURHandle, User32.WM_CLOSE, 0, 0 );

            User32.SendMessage( SURHandle, User32.WM_QUIT, 0, 0 );

            User32.SendMessage( SURHandle, User32.WM_DESTROY, 0, 0 );
        }

        //Steam Updating
        public static IntPtr SUHandle {
            get {

                IntPtr result = FindWindow( "Updating PLAYERUNKNOWN'S BATTLEGROUNDS" );

                if (result.ToInt32() <= 0)
                    result = FindWindow( "Обновление PLAYERUNKNOWN'S BATTLEGROUNDS" );

                return result;
            }
        }
        public static bool SUExists => !SUHandle.Equals(IntPtr.Zero);

        //
        private static int styleNone = GenStyleNone();

        private static int StyleNone => styleNone;

        public static bool IsFocused => (IntPtr)User32.GetForegroundWindow() == Handle;       

        public static void SetFocus() => User32.SetForegroundWindow( Handle );

        private static int OrigStyle;
        private static int OrigWidth; 
        private static int OrigHeight; 
        private static bool first = true;

        private static int GenStyleNone() {

            Form frm = new Form();

            frm.FormBorderStyle = FormBorderStyle.None;

            int result = User32.GetWindowLong(frm.Handle, User32.GWL_STYLE);

            frm.Close();

            return result;
        }

        public static bool NeedSetupWindow {
            get {

                RECT r = new RECT();
                User32.GetWindowRect( Handle, ref r );
                //return !( r.Left == 0 && r.Top == 0 && r.Right == Width && r.Bottom == Height );
                return !(r.Top == 0 && r.Bottom == Height );
            }
        }

        public static void SetupWindow() {

            if (first) {

                first = false;

                OrigStyle = User32.GetWindowLong( Handle, User32.GWL_STYLE );
                RECT rc = new RECT();
                User32.GetWindowRect( Handle, ref rc );
                OrigWidth = rc.Right - rc.Left;
                OrigHeight = rc.Bottom - rc.Top;

            }
            User32.SetWindowLong(Handle, User32.GWL_STYLE, StyleNone);

            int flags = User32.SWP_NOZORDER | User32.SWP_SHOWWINDOW | User32.SWP_NOCOPYBITS |
                        User32.SWP_FRAMECHANGED;

            User32.SetWindowPos(Handle, (IntPtr) 0, PosX, PosY, Width, Height, flags);

        } 

        public static void RestoreOrigWindowSize() {

            User32.SetWindowLong(Handle, User32.GWL_STYLE, OrigStyle);
            User32.SetWindowPos(Handle, (IntPtr) 0, 0, 0, OrigWidth, OrigHeight, User32.SWP_SHOWWINDOW);

        }

        public static void Hide() => User32.ShowWindow(Handle, User32.SW_HIDE);
        public static void Show() => User32.ShowWindow(Handle, User32.SW_SHOW);
    }
}
