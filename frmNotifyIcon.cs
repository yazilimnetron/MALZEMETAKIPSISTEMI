using MALZEME_TAKIP_SISTEMI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmNotifyIcon : Form
    {
        public frmNotifyIcon()
        {
            InitializeComponent();
        }

        private void frmNotifyIcon_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("Select COUNT(*) adet from TBL_LST_MALZEMEDEPOISTEM where MALZEMEDEPOISTEM_DURUM not in ('3','2')");
            DataTable dtMalzemeler3 = clSqlTanim.RunStoredProcDependency(sb.ToString());
        }

        private void frmNotifyIcon_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("Select COUNT(*) adet from TBL_LST_MALZEMEDEPOISTEM where MALZEMEDEPOISTEM_DURUM not in ('3','2') ");
            sb.AppendFormat("Where isnull(MALZEMEDEPOISTEM_DURUM,0) not in ('3','2') "); 

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());

            if (dtMalzemeler.Rows.Count > 0)
            {
                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    for (int i = 0; i < dtMalzemeler.Rows.Count; i++)
                    {
                        synth.Speak("Today " + DateTime.Today.ToLongDateString() + "  you have the following appointment at time " + dtMalzemeler.Rows[i]["adet"].ToString() + " you have  " + dtMalzemeler.Rows[i]["adet"].ToString());
                    }
                    return;
                }
            }
        }
    }
}
