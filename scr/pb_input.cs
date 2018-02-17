using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32;
using SnLib;
using System.Windows.Forms;

namespace pbpb
{
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

    public static class PubgInput
    {
        public delegate void InputEventHandler( PubgInputEventArgs e );

        public static event InputEventHandler InputEvent;

        private static void RaiseInputEvent(Keys key, bool release, bool ispress) =>
            InputEvent?.Invoke( new PubgInputEventArgs( key, release, ispress ) );

        public static Keys LastKey { get; set; }

        private static void KeyDownOrUp(Keys key, bool release)
        {

            SKeybd.MouseMove(-55, 0);

            ReleaseKey(LastKey);

            if (!release) SKeybd.KeyDown(key);
            else          SKeybd.KeyUp(key);

            LastKey = key;

            RaiseInputEvent(key, release, false);
        }

        private static void KeyPress(Keys key)
        {            

            MoveMouse(-25, 0);

            ReleaseKey(LastKey);

            SKeybd.KeyPress(key);

            LastKey = key;

            RaiseInputEvent(key, false, true);
        }

        public static void MoveMouse(int x, int y) {

            SKeybd.MouseMove(x, y);
        }

        private static void ReleaseKey(Keys key) {

            SKeybd.KeyUp(key);
        }

        public static int EjectClickedTime {get; set;} = int.MaxValue;
        public static void Eject() {

            EjectClickedTime = Environment.TickCount;

            KeyPress(Keys.F);
        }

        public static int ParachuteClickedTime {get; set;} = int.MaxValue;
        public static void Parachute() {

            ParachuteClickedTime = Environment.TickCount;

            KeyPress(Keys.F);
        }

        public static int DownClickedTime {get; set;} = int.MaxValue;
        public static void Down() {

            DownClickedTime = Environment.TickCount;

            KeyPress(Keys.Z);
        }

        public static void Sit() {

            KeyPress(Keys.C);
        }

        public static void Jump() {

            KeyPress(Keys.Space);
        }

        public static void Forward() {

            KeyDownOrUp( Keys.W, false );
        }

        public static void ReleaseValueKeys() {

            KeyDownOrUp(Keys.W, true); KeyDownOrUp(Keys.S, true);
            KeyDownOrUp(Keys.Z, true); KeyDownOrUp(Keys.C, true);
            KeyDownOrUp(Keys.F, true);
        }

        public static void Back() {

            KeyDownOrUp( Keys.S, false );

        }
    }
}
