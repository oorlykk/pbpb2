using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32;
using SnLib;
using System.Windows.Forms;
using System.Threading;

namespace pbpb
{      

    public class _PubgInputMessage : PubgInput
    {

        public IntPtr Handle { get => PubgWindow.Handle; set => Handle = value; }

        public bool AsPostMessage;

        public override void KeyDownOrUp( Keys key, bool release )
        {
            //if (key != LastKey) ReleaseKey(LastKey);

            if (!release)

                if (!AsPostMessage)

                    User32.SendMessage( Handle, User32.WM_KEYDOWN, (int)key, 0 );
                else

                    User32.PostMessage( Handle, User32.WM_KEYDOWN, (int)key, 0 );
            else

                if (!AsPostMessage)

                    User32.SendMessage( Handle, User32.WM_KEYUP, (int)key, 0 );
                else

                    User32.PostMessage( Handle, User32.WM_KEYUP, (int)key, 0 );

            LastKey = key;

            RaiseInputEvent(key, release, false);
        }

        public override void KeyPress(Keys key)
        {            
            
            if (key != LastKey) ReleaseKey(LastKey);

            User32.SendMessage( Handle, User32.WM_KEYDOWN, (int)key, 0 );
            User32.SendMessage( Handle, User32.WM_KEYUP, (int)key, 0 );

            LastKey = key;

            RaiseInputEvent(key, false, true);
        }

        public override void ClickLeftMouse(int x = 0, int y = 0) {
            
            if (x == 0 && y == 0) {

                POINT pos = new POINT();
                User32.GetCursorPos(out pos);
                x = pos.x;
                y = pos.y;
            }

            int lp = (int)(((ushort)x) | (uint)(y << 16));
            int wp = 0; //User32.MK_LBUTTON;

            User32.PostMessage( Handle, User32.WM_LBUTTONDOWN, wp, lp );
            User32.SendMessage( Handle, User32.WM_LBUTTONUP, wp, lp );
        }

        public override void ReleaseKey(Keys key) {

            User32.SendMessage( Handle, User32.WM_KEYUP, (int)key, 0 ); 
        }

        public override void MoveMouse( int x, int y ) {

            //int lp = (int) ( ( (ushort) x ) | (uint) ( y << 16 ) );
            //User32.SendMessage( Handle, User32.WM_MOUSEMOVE, 0, lp );
            ChangeViewPerson();
        }

        public override void AssistInWater()
        {

            ReleaseKey( Keys.W ); ReleaseKey( Keys.S ); ReleaseKey( Keys.A ); ReleaseKey( Keys.D );
            ReleaseKey( Keys.Z ); ReleaseKey( Keys.C ); ReleaseKey( Keys.F );

            Form1.InitInput_event();

            Form1.PubgInput.MoveMouse( 1000, 1000 );

            Form1.InitInput_message();

            Back();
        }

    }

    public class PubgInput
    {
        public delegate void InputEventHandler( PubgInputEventArgs e );

        public event InputEventHandler InputEvent;

        public void RaiseInputEvent(Keys key, bool release, bool ispress) =>
            InputEvent?.Invoke( new PubgInputEventArgs( key, release, ispress ) );

        public static bool CanInteract => (!Setti.PassiveMode) || (Setti.PassiveMode && STime.GetUserIdleTime() > 5000);

        public bool IsInputEvent => (this.GetType() == typeof(PubgInput));
        public bool IsInputMessage => (this.GetType() == typeof(_PubgInputMessage));

        public Keys LastKey { get; set; }    

        public POINT OldCursorPos = new POINT();

        private void SetFocus() {

            User32.GetCursorPos( out OldCursorPos );
            PubgWindow.SetFocus();
        }

        private void RestoreFocus() {

            PubgWindow.RestoreFocus();
            if (PubgWindow.FocusSettted)
                User32.SetCursorPos(OldCursorPos.x, OldCursorPos.y);
        }

        public virtual void KeyDownOrUp(Keys key, bool release)
        {                  

            SetFocus();

            ReleaseKey(LastKey);

            Thread.Sleep(55);     

            if (!release) SKeybd.KeyDown(key);
            else          SKeybd.KeyUp(key);

            Thread.Sleep(55);

            RestoreFocus();

            LastKey = key;

            RaiseInputEvent(key, release, false);
        }

        public virtual void KeyPress(Keys key)
        {                                       

            SetFocus();

            ReleaseKey(LastKey);

            Thread.Sleep(55);

            SKeybd.KeyPress(key);

            Thread.Sleep(55);

            RestoreFocus();

            LastKey = key;

            RaiseInputEvent(key, false, true);
        }

        public virtual void MoveMouse(int x, int y) {

            SetFocus();
            Thread.Sleep(55);

            SKeybd.MouseMove( x, y );

            Thread.Sleep(55);
            RestoreFocus();
        }

        public virtual void ClickLeftMouse(int x, int y) {
            
            x += PubgWindow.PosX;
            y += PubgWindow.PosY;

            SetFocus();
            Thread.Sleep(55);

            SKeybd.LBClickEx(x, y, true, 64, 1600, 64);
            SKeybd.LBClickEx(x+1, y+1, true);

            Thread.Sleep(55);
            RestoreFocus();
        }

        public virtual void ReleaseKey(Keys key) {

            SKeybd.KeyUp(key);
        }

        public int EjectClickedTime {get; set;} = int.MaxValue;
        public void Eject() {

            EjectClickedTime = Environment.TickCount;

            KeyPress(Keys.F);
        }

        public int ParachuteClickedTime {get; set;} = int.MaxValue;
        public void Parachute() {

            ParachuteClickedTime = Environment.TickCount;

            KeyPress(Keys.F);
        }

        public int DownClickedTime {get; set;} = int.MaxValue;
        public void Down() {

            DownClickedTime = Environment.TickCount;

            KeyPress(Keys.Z);
        }

        public void Sit() {

            KeyPress(Keys.C);
        }

        public void Jump() {

            KeyPress(Keys.Space);
        }

        public void Forward() {

            KeyDownOrUp( Keys.W, false );
        }

        public void Back() {

            KeyDownOrUp( Keys.S, false );

        }

        public void ChangeViewPerson() {

            KeyPress(Keys.V);
        }

        public virtual void AssistInWater() {

            ReleaseKey(Keys.W); ReleaseKey(Keys.S); ReleaseKey(Keys.A); ReleaseKey(Keys.D);
            ReleaseKey(Keys.Z); ReleaseKey(Keys.C); ReleaseKey(Keys.F); 

            MoveMouse(1000, 1000);

            Back();
        }
        public void ClickCenter() => ClickLeftMouse(480, 308);

    }

    public class PubgInputEventArgs
    {
        public PubgInputEventArgs( Keys key, bool release, bool ispress )
        {
            Key = key;
            IsUp = release;
            IsPress = ispress;
        }
        public Keys Key { get; private set; }
        public bool IsUp { get; private set; }
        public bool IsPress { get; private set; }
    }

    partial class Form1 : Form {

        public static PubgInput PubgInput;

        public static void InitInput_event() {

            PubgInput = new PubgInput();
            PubgInput.InputEvent += new PubgInput.InputEventHandler(PubgInputEvent);

            Log.Add( "Input switched to <event>" );
        }

        public static void InitInput_message() {

            PubgInput = new _PubgInputMessage();
            PubgInput.InputEvent += new PubgInput.InputEventHandler(PubgInputEvent);

            Log.Add( "Input switched to <message>" );
        }
    }

}
