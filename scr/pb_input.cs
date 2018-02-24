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

    public class _PubgInput2 : _PubgInput
    {

        public override void KeyDownOrUp( Keys key, bool release )
        {

            ReleaseKey(LastKey);

            if (!release) User32.SendMessage( PubgWindow.Handle, User32.WM_KEYDOWN, (int)key, 0 );
            else          User32.SendMessage( PubgWindow.Handle, User32.WM_KEYUP, (int)key, 0 );

            LastKey = key;

            RaiseInputEvent(key, release, false);
        }

        public override void KeyPress(Keys key)
        {            
            
            ReleaseKey(LastKey);

            User32.SendMessage( PubgWindow.Handle, User32.WM_KEYDOWN, (int)key, 0 );
            User32.SendMessage( PubgWindow.Handle, User32.WM_KEYUP, (int)key, 0 );

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
            int wp = User32.MK_LBUTTON;

            User32.SendMessage( PubgWindow.Handle, User32.WM_LBUTTONDOWN, wp, lp );
            User32.SendMessage( PubgWindow.Handle, User32.WM_LBUTTONUP, wp, lp );

            Thread.Sleep(64);

            User32.SendMessage( PubgWindow.Handle, User32.WM_LBUTTONDOWN, wp, lp );
            User32.SendMessage( PubgWindow.Handle, User32.WM_LBUTTONUP, wp, lp );
        }

        public override void ReleaseKey(Keys key) {

            User32.SendMessage( PubgWindow.Handle, User32.WM_KEYUP, (int)key, 0 ); 
        }

        public override void MoveMouse(int x, int y) {

            //int lp = (int)(((ushort)x) | (uint)(y << 16));
            //int wp = 0;
            //User32.SendMessage( PubgWindow.Handle, User32.WM_MOUSEMOVE, wp, lp );
        }

    }

    public class _PubgInput
    {
        public delegate void InputEventHandler( PubgInputEventArgs e );

        public event InputEventHandler InputEvent;

        public void RaiseInputEvent(Keys key, bool release, bool ispress) =>
            InputEvent?.Invoke( new PubgInputEventArgs( key, release, ispress ) );

        public static Keys LastKey { get; set; }

        public virtual void KeyDownOrUp(Keys key, bool release)
        {

            //SKeybd.MouseMove(-55, 0);

            ReleaseKey(LastKey);

            if (!release) SKeybd.KeyDown(key);
            else          SKeybd.KeyUp(key);

            LastKey = key;

            RaiseInputEvent(key, release, false);
        }

        public virtual void KeyPress(Keys key)
        {            
            
            //MoveMouse(-25, 0);

            ReleaseKey(LastKey);

            SKeybd.KeyPress(key);

            LastKey = key;

            RaiseInputEvent(key, false, true);
        }

        public virtual void MoveMouse(int x, int y) {

            SKeybd.MouseMove( x, y );
        }

        public virtual void ClickLeftMouse(int x, int y) {
            
            x += PubgWindow.PosX;
            y += PubgWindow.PosY;

            SKeybd.LBClickEx(x, y, true, 100, 1600, 100);
            Thread.Sleep(50);
            SKeybd.LBClickEx(x+1, y+1, true, 1, 50, 1);
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

        public void ReleaseValueKeys() {

            KeyDownOrUp(Keys.W, true); KeyDownOrUp(Keys.S, true);
            KeyDownOrUp(Keys.Z, true); KeyDownOrUp(Keys.C, true);
            KeyDownOrUp(Keys.F, true);
        }

        public void Back() {

            KeyDownOrUp( Keys.S, false );

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

        public static _PubgInput PubgInput;

        public void InitInput0() {

            PubgInput = new _PubgInput();
            PubgInput.InputEvent += new _PubgInput.InputEventHandler(PubgInputEvent);
        }

        public void InitInput2() {

            PubgInput = new _PubgInput2();
            PubgInput.InputEvent += new _PubgInput.InputEventHandler(PubgInputEvent);
        }
    }

}
