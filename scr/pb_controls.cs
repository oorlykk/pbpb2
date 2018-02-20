using System;
using System.Collections;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SnLib;
using Win32;
using IH = ImageHasher;

namespace pbpb
{

    public enum PubgControls : ulong {

        btnStart = 18410924907680276608,
        btnExit = 18410913813996339328,
        labAlive = 2821267018762805729,
        //labJoined = 69540876582912,
        labJoined = 14114528523191117031,
        //labEject = 105924403798016,
        labEject = 13238258234994063104,
        //labReleaseParachute = 31057834917632,
        labReleaseParachute = 13671979250605947648,
        btnMatchCanContinue =  18410856832886014080,
        labWater = 882281628581721955,
        btnSoloSquad = 91,
        btnConfirmExit = 92,
    };

    public class PubgControlNative {
        
        public string AliasName;

        private Rectangle R; 
        
        public int X => R.X;
        public int Y => R.Y;
        public int Width => R.Width;
        public int Height => R.Height;      
        
        public PubgControlNative(string aliasname, int x, int y, int sx = 0, int sy = 0) {

            AliasName = aliasname;

            if (sx == 0 && sy == 0) {
                sx = x + 1;
                sy = y + 1;
            }
            
            int w = sx - x; int h = sy -y;

            R = new Rectangle( x, y, w, h );

        }
        
        public long LastClickedTick {get; private set;}

        public void ClickLeftMouse()
        {
            LastClickedTick = Environment.TickCount;

            int x = PubgWindow.PosX + X + ( Width / 2 );
            int y = PubgWindow.PosY + Y + ( Height / 2 );

            SKeybd.LBClickEx2( x, y, true, 64, 1600, 64 );
            SKeybd.LBClickEx2( x, y, true);
        }

    }

    public class PubgControl : PubgControlNative, IDisposable {

        public Bitmap ControlImage;

        private ulong m_ComparableHash;

        public ulong ComparableHash => m_ComparableHash;

        private ulong m_ControlImageHash;

        public ulong ControlImageHash => m_ControlImageHash;

        public bool IsNative => ControlImage == null;

        public int LastDistance { get; private set; } = 99;

        public int CalcDistance(bool rehash = false) {

            if (rehash) CalcHash();

            LastDistance = IH.ImageHasher.ComputeHammingDistance(ComparableHash, ControlImageHash);

            return LastDistance;

        }

        public ulong CalcHash() {

            m_ControlImageHash = IH.ImageHasher.ComputeAverageHash( ControlImage );
            return m_ControlImageHash;
            
        }

        public void ControlImageFromImage(Bitmap pubg_image) {

            SGraph.Cut(pubg_image, ControlImage, X, Y);

        }

        public PubgControl(string aliasname, ulong hash, int x, int y, int sx = 0, int sy = 0) :

            base(aliasname, x, y, sx, sy) {

            m_ComparableHash = hash;

            if (sx == 0 && sy == 0)
                ControlImage = null;
            else
                ControlImage = new Bitmap( Width, Height );

        }

        // IDisposable 
        public void Dispose() {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        ~PubgControl() { 
            Dispose( false );
        }

        protected virtual void Dispose( bool disposing ) {
            if (disposing) {  
                if (ControlImage != null) {
                    ControlImage.Dispose();
                    ControlImage = null;
                }
            }
        }       
    }    

}