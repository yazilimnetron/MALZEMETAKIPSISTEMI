using System;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmAdetDuzenle : DevExpress.XtraEditors.XtraForm
    {
        public Boolean Tamam = false;
        public frmAdetDuzenle()
        {
            InitializeComponent();
        }

        private void simpleButtonKapat_Click(object sender, EventArgs e)
        {
            Tamam = false;
            this.Close();
        }

        private void simpleButtonTamam_Click(object sender, EventArgs e)
        {
            Tamam = true;
            this.Close();
        }

        private void frmAdetDuzenle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButtonTamam_Click(new object(), new System.EventArgs());
            }
        }
    }
}