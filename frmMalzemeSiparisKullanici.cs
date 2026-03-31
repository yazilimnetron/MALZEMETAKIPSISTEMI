using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeSiparisKullanici : Form
    {
        public frmMalzemeSiparisKullanici()
        {
            InitializeComponent();
        }

        private void simpleButtonTamam_Click(object sender, EventArgs e)
        {
            var f = Application.OpenForms["frmMalzemeSiparisKarsilama"] as frmMalzemeSiparisKarsilama;
            if (f == null) return;
            f.malzemesiparisKullanici = Convert.ToInt32(comboBoxEditKullanici.SecilenDeger().Id.ToString());
            //this.Hide();
            //f.ShowDialog();
            this.Close();
        }

        private void frmMalzemeSiparisKullanici_Load(object sender, EventArgs e)
        {
            comboBoxEditKullanici.Doldur("select MALZEMEKULLANICI_ID, MALZEMEKULLANICI_ADI from TBL_LST_MALZEMEKULLANICILAR where isnull(MALZEMEKULLANICI_DURUM,0) =1 and isnull(MALZEMEKULLANICI_SIPARISKAPAT,0)=1 order by 2", true);
            comboBoxEditKullanici.SelectedIndex = 0;
        }

        private void simpleButtonIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
