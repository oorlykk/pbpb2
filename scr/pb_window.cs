using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Win32;
using snlib = SnLib;

namespace pbpb
{

    public class NativeWindow
    {
        public string Caption { get; private set; }

        public NativeWindow(string window_text) {

            Caption = window_text;
        }
        protected virtual IntPtr Find() => (IntPtr)User32.FindWindow( null, Caption );
        public IntPtr Handle => Find();
        public bool Exist => (int)Handle != 0;
        public void Hide() => User32.ShowWindow(Handle, User32.SW_HIDE);
        public void Show() => User32.ShowWindow(Handle, User32.SW_SHOW);
        public void SetForegorund() => User32.SetForegroundWindow(Handle);
               
        public virtual void SetClose()
        {
            User32.SendMessage( Handle, User32.WM_CLOSE, 0, 0 );
            User32.SendMessage( Handle, User32.WM_QUIT, 0, 0 );
            User32.SendMessage( Handle, User32.WM_DESTROY, 0, 0 );
        }
    }

    public class NativeWindows {    

        public static NativeWindow SteamErrorEn = new NativeWindow( "Steam - Error");
        public static NativeWindow SteamErrorRu = new NativeWindow( "Steam — Ошибка");


        public static NativeWindow SteamConnectErrorEn = new NativeWindow( "Connection Error");
        public static NativeWindow SteamConnectErrorRu = new NativeWindow( "Ошибка подключения");

        public static NativeWindow SteamUpdatEn = new NativeWindow( "Updating PLAYERUNKNOWN'S BATTLEGROUNDS");
        public static NativeWindow SteamUpdateRu = new NativeWindow( "Обновление PLAYERUNKNOWN'S BATTLEGROUNDS");

        public static NativeWindow SteamUpdateReadyEn = new NativeWindow( "Ready - PLAYERUNKNOWN'S BATTLEGROUNDS");
        public static NativeWindow SteamUpdateReadyRu = new NativeWindow( "Готово — PLAYERUNKNOWN'S BATTLEGROUNDS");

        public static NativeWindow PubgCrashReporter = new NativeWindow( "BATTLEGROUNDS Crash Reporter");

        public static NativeWindow BattlEyeLauncher = new NativeWindow( "BattlEye Launcher" );

        public static NativeWindow SteamGameOverlayUICrash = new NativeWindow( "gameoverlayui.exe" );
        public static NativeWindow SteamClientBootstrapper = new NativeWindow( "Steam Client Bootstrapper" );
    }


    public class NativeUtils {

        private static void ForceTaskKill(string task) =>
            Shell32.ShellExecute(IntPtr.Zero, "open", "taskkill.exe", "/F /T /IM " + task, "", User32.SW_HIDE);

        public static void KillExecutePubgCrash() {

            ForceTaskKill( "BroCrashReporter.exe" );
        }

        public static void KillExecutePubg() {

            ForceTaskKill( "TslGame.exe" );
            ForceTaskKill( "BroCrashReporter.exe" );
        }

        public static void KillExecuteSteam() {

            ForceTaskKill( "steam.exe" );
            ForceTaskKill( "gameoverlayui.exe" );
            ForceTaskKill("steamwebhelper.exe");
            ForceTaskKill( "steamservice.exe" );
            ForceTaskKill( "steamerrorreporter.exe" );
        }

        public static void StartExecute()
        {
            Shell32.ShellExecute( IntPtr.Zero, "open", "steam://rungameid/578080", "", "", User32.SW_SHOWNORMAL );
        }

        public static void ShutdownExecute()
        {
            Log.Add("Shutdown PC Execute!");
            Log.Save();
            Shell32.ShellExecute( IntPtr.Zero, "open", Environment.SystemDirectory + "\\shutdown.exe", "/s /f /t 5", "", 0 );
        }
    }

   public static class PubgWindow {

        public static NativeWindow Window = new NativeWindow( "PLAYERUNKNOWN'S BATTLEGROUNDS " );

        public static void ThrowLastWinError(bool show) {

            if (!show) return;

            Win32Exception e = new Win32Exception( Marshal.GetLastWin32Error() );
            throw ( e );
        }

        private static int m_partfullhd;

        public static int PartFullHD { get => m_partfullhd; set => m_partfullhd = value; }

        public static int Width => (1920 / 10) * m_partfullhd;

        public static int Height => (1080 / 10) * m_partfullhd;

        public static int PosX => Setti.PubgWindowAbsoluteX;
        public static int PosY => Setti.PubgWindowAbsoluteY;

        public static bool KillExecuted;
        public static int KillExecutedTime;

        public static void KillExecute() {

            Log.Add( "(PW) KillExecute PUBG!" );
            PubgRound.End();
            KillExecutedTime = Environment.TickCount;
            PubgWindow.Window.SetClose();
            NativeUtils.KillExecutePubg();
            KillExecuted = true;
        }

        public static int KillExecutedSteamTime;
        public static void KillExecuteSteam()
        {
            Log.Add( "(PW) KillExecute Steam!" );
            KillExecutedSteamTime = Environment.TickCount;
            NativeUtils.KillExecuteSteam();
            KillExecuted = false;
        }

        public static void StartExecute() {

            PubgRound.End();
            
            Log.Add( "(PW) StartExecute PUBG!" );
                      
            NativeUtils.StartExecute();
        }

        public static IntPtr Handle => Window.Handle;
        public static bool Exists => !Handle.Equals(IntPtr.Zero);
        public static void CloseMsg() => Window.SetClose();

        public static IntPtr CrashHandle => NativeWindows.PubgCrashReporter.Handle;
        public static bool CrashExists => NativeWindows.PubgCrashReporter.Exist;
        public static void KillCrash()
        {

            if (NativeWindows.PubgCrashReporter.Exist) {

                Log.Add( "(PU) KillExecute Crash!" );

                NativeWindows.PubgCrashReporter.SetClose();   
                
                NativeUtils.KillExecutePubgCrash();
            }                    
        }

        private static IntPtr BEHandle => NativeWindows.BattlEyeLauncher.Handle;
        private static bool BEVisible => User32.IsWindowVisible(BEHandle) > 0;
        public static void HideBE() {

            if (!BEVisible) return;

            NativeWindows.BattlEyeLauncher.Hide();

            Log.Add( "(PW) Hide BEye" );                   
        }

        // Steam Error
        public static IntPtr SEHandle {
            get {

                IntPtr result = NativeWindows.SteamErrorEn.Handle;

                if ((int)result == 0)
                    result = NativeWindows.SteamErrorRu.Handle;

                return result;
            }
        }   
        public static bool SEExists => !SEHandle.Equals(IntPtr.Zero);
        public static void CloseSE()
        {
            if (!SEExists) return;

            if (NativeWindows.SteamErrorEn.Exist) {

                NativeWindows.SteamErrorEn.SetClose();
                return;
            }
            NativeWindows.SteamErrorRu.SetClose();
        }   

        //Steam Update Ready
        public static IntPtr SURHandle {
            get {

                IntPtr result = NativeWindows.SteamUpdateReadyEn.Handle;

                if ((int)result == 0)
                    result = NativeWindows.SteamUpdateReadyRu.Handle;

                return result;
            }
        }
        public static bool SURExists => !SURHandle.Equals(IntPtr.Zero);
        public static void CloseSUR() {

            if (!SURExists) return;

            if (NativeWindows.SteamUpdateReadyEn.Exist) {

                NativeWindows.SteamUpdateReadyEn.SetClose();
                return;
            }
            NativeWindows.SteamUpdateReadyRu.SetClose();
        }

         //Steam Updating
        public static IntPtr SUHandle {
            get {

                IntPtr result = NativeWindows.SteamUpdatEn.Handle;

                if ((int)result == 0)
                    result = NativeWindows.SteamUpdateRu.Handle;

                return result;
            }
        }
        public static bool SUExists => !SUHandle.Equals(IntPtr.Zero);  

        //Steam Connection Error
        public static bool SCEExitst => NativeWindows.SteamConnectErrorEn.Exist || NativeWindows.SteamConnectErrorRu.Exist;
        public static void CloseSCE() {

            if (NativeWindows.SteamConnectErrorEn.Exist) {

                NativeWindows.SteamConnectErrorEn.SetClose();
                return;
            }
            if (NativeWindows.SteamConnectErrorRu.Exist)
                NativeWindows.SteamConnectErrorRu.SetClose();
            
        }   
     
        public static bool IsFocused => (IntPtr)User32.GetForegroundWindow() == Handle;

        public static IntPtr PredFocus;

        public static bool FocusSettted;

        public static void SetFocus() {

            if (IsFocused) return;          

            PredFocus = (IntPtr)User32.GetForegroundWindow();

            Window.SetForegorund();

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
            ThrowLastWinError(!result);

            User32.SetWindowPos(Handle, (IntPtr) 0, 0, 0, 0, 0, User32.SWP_NOMOVE | User32.SWP_NOSIZE |           User32.SWP_NOZORDER | User32.SWP_FRAMECHANGED);

            int flags = User32.SWP_SHOWWINDOW | User32.SWP_NOCOPYBITS;
            result = User32.SetWindowPos(Handle, (IntPtr) 0, PosX, PosY, Width, Height, flags) > 0;

            ThrowLastWinError(!result);

            return result;

        } 

        public static void RestoreOrigWindowStyle() => User32.SetWindowLong(Handle, User32.GWL_STYLE, OrigStyle);
        public static void RestoreOrigWindowPos() => User32.SetWindowPos(Handle, (IntPtr) 0, 0, 0, OrigWidth, OrigHeight, User32.SWP_FRAMECHANGED | User32.SWP_NOACTIVATE ); 

        public static void Hide() => User32.ShowWindow(Handle, User32.SW_HIDE);
        public static void Show() => User32.ShowWindow(Handle, User32.SW_SHOW);

        public static bool IsWindowVisible => User32.IsWindowVisible( Handle ) != 0;
        

    }
}
