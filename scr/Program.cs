using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace pbpb
{
    static class Program
    {   

        public static Assembly ResolveEventHandler( Object sender, ResolveEventArgs args ) {
            String dllName = new AssemblyName( args.Name ).Name + ".dll";
            var assem = Assembly.GetExecutingAssembly();
            String resourceName = assem.GetManifestResourceNames().FirstOrDefault( rn =>
             rn.EndsWith( dllName ) );
            if (resourceName == null) return null;
            // Not found, maybe another handler will find it
            using (var stream = assem.GetManifestResourceStream( resourceName )) {
                Byte[] assemblyData = new Byte[stream.Length];
                stream.Read( assemblyData, 0, assemblyData.Length );
                return Assembly.Load( assemblyData );
            }
        }



        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(ResolveEventHandler);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
