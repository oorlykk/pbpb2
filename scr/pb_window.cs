using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Win32;
using snlib = SnLib;

namespace pbpb
{

    public class SteamWindow {

        public static IntPtr FindWindow(string WindowName) => PubgWindow.FindWindow(WindowName);

        public static int KillExecutedSteamTime = int.MaxValue;
        public static void KillExecuteSteam() {

            Log.Add("(PW) KillExecute Steam!");

            KillExecutedSteamTime = Environment.TickCount;        
            ForceTaskKill("steam.exe");
            Thread.Sleep(5000);
            ForceTaskKill("gameoverlayui.exe");
            //ForceTaskKill("steamwebhelper.exe");
            KillExecuted = false;
        }
    }

    public static class PubgWindow {

        public static void ShowLastWinError(bool show) {

            if (!show) return;

            Win32Exception e = new Win32Exception( Marshal.GetLastWin32Error() );
            throw ( e );
        }

        public static IntPtr FindWindow(string WindowName) => (IntPtr)User32.FindWindow( null, WindowName );

        private static int m_partfullhd;

        public static int PartFullHD { get => m_partfullhd; set => m_partfullhd = value; }

        public static int Width => (1920 / 10) * m_partfullhd;

        public static int Height => (1080 / 10) * m_partfullhd;

        public static int PosX => Setti.PubgWindowAbsoluteX;
        public static int PosY => Setti.PubgWindowAbsoluteY;

        private static void ForceTaskKill(string task) =>
            Shell32.ShellExecute(IntPtr.Zero, "open", "taskkill.exe", "/f /t /im " + task, "", User32.SW_HIDE);

        public static bool KillExecuted = false;
        public static int KillExecutedTime = int.MaxValue;
        public static void KillExecute() {

            Log.Add("(PW) KillExecute PUBG!");

            KillExecuted = true;
            KillExecutedTime = Environment.TickCount;
            PubgRound.End();
            ForceTaskKill("TslGame.exe");         
        }

        public static void StartExecute() {

            Log.Add("(PW) StartExecute PUBG!");

            PubgRound.End();
            Shell32.ShellExecute( IntPtr.Zero, "open", "steam://rungameid/578080", "low", "", User32.SW_SHOWNORMAL );
        }

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

        private static IntPtr BEHandle => FindWindow("BattlEye Launcher" );
        private static bool BEVisible => User32.IsWindowVisible(BEHandle) > 0;
        public static void HideBE() {

            if (!BEVisible) return;

            User32.ShowWindow(BEHandle, User32.SW_HIDE);

            Log.Add( "(PW) Hide BEye" );
                    
        }

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
            if (!SEExists) return;

            User32.SetForegroundWindow( SEHandle );

            Thread.Sleep( 100 );

            //snlib.SKeybd.KeyPress( Keys.Space );

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

        
        public static bool IsFocused => (IntPtr)User32.GetForegroundWindow() == Handle;

        public static IntPtr PredFocus;

        public static bool FocusSettted;

        public static void SetFocus() {

            if (IsFocused) return;          

            PredFocus = (IntPtr)User32.GetForegroundWindow();

            User32.SetForegroundWindow( Handle );

            FocusSettted = true;

            Log.Add( String.Format( "(PW) Set Focus  {0}", Handle ) );
        }

        public static void RestoreFocus() {

            if (!FocusSettted) return;

            User32.SetForegroundWindow( PredFocus );

            FocusSettted = false;

            Log.Add( String.Format( "(PW) Focus restore => {0}", PredFocus ) );
        }


        private static int styleNone = GenStyleNone();
        private static int StyleNone => styleNone;

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
                return !( r.Left == PosX && r.Top == PosY && r.Right == PosX + Width && r.Bottom == PosY + Height );
            }
        }

        public static bool SetupWindow() {

            if (first) {

                first = false;

                OrigStyle = User32.GetWindowLong( Handle, User32.GWL_STYLE );
                RECT rc = new RECT();
                User32.GetWindowRect( Handle, ref rc );
                OrigWidth = rc.Right - rc.Left;
                OrigHeight = rc.Bottom - rc.Top;

            }
            bool result = User32.SetWindowLong(Handle, User32.GWL_STYLE, StyleNone) > 0;
            ShowLastWinError(!result);

            User32.SetWindowPos(Handle, (IntPtr) 0, 0, 0, 0, 0, User32.SWP_NOMOVE | User32.SWP_NOSIZE |           User32.SWP_NOZORDER | User32.SWP_FRAMECHANGED);

            int flags = User32.SWP_SHOWWINDOW | User32.SWP_NOCOPYBITS;
            result = User32.SetWindowPos(Handle, (IntPtr) 0, PosX, PosY, Width, Height, flags) > 0;

            ShowLastWinError(!result);

            return result;

        } 

        public static void RestoreOrigWindowStyle() => User32.SetWindowLong(Handle, User32.GWL_STYLE, OrigStyle);
        public static void RestoreOrigWindowPos() => User32.SetWindowPos(Handle, (IntPtr) 0, 0, 0, OrigWidth, OrigHeight, User32.SWP_FRAMECHANGED | User32.SWP_NOACTIVATE ); 

        public static void Hide() => User32.ShowWindow(Handle, User32.SW_HIDE);
        public static void Show() => User32.ShowWindow(Handle, User32.SW_SHOW);
    }
}
