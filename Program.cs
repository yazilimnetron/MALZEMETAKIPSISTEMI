using System;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    static class Program
    {
        public static string versionCode = "3.0";
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
