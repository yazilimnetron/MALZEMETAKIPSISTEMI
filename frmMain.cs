using System;
using System.Collections.Generic;
using System.Linq;

using DevExpress.XtraBars.Ribbon;

using DevExpress.Utils;

using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using System.Drawing;


namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMain : RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
            IsMdiContainer = true;
            Shown += frmMain_Shown;
        }

        void frmMain_Shown(object sender, EventArgs e)
        {
            client.Invalidate();
        }

        Image img;
        MdiClient client;
        private void frmMain_Load(object sender, EventArgs e)
        {
            img = MALZEME_TAKIP_SISTEMI.Properties.Resources.BekaertDesleelogo;
            client = Controls.OfType<MdiClient>().FirstOrDefault();
            client.Paint += client_Paint;
        }

        void client_Paint(object sender, PaintEventArgs e)
        {
            MdiClient client = sender as MdiClient;
            e.Graphics.DrawImage(img, new Rectangle(new Point(-client.Left, -client.Top), this.ClientSize));
        }
    }
}