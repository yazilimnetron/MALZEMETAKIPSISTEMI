using System;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    static class Program
    {
        public static string versionCode = "2.5.3";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmGiris());
        }
    }
}
