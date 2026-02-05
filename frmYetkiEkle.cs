using DevExpress.XtraEditors;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using System;
using System.Data;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmYetkiEkle : Form
    {
        int KID = 0;
        public frmYetkiEkle()
        {
            InitializeComponent();
        }

        private void frmYetkiEkle_Load(object sender, EventArgs e)
        {
            comboBoxEditKullaniciAdi.Doldur("Select MALZEMEKULLANICI_ID,MALZEMEKULLANICI_ADI from TBL_LST_MALZEMEKULLANICILAR order by 2", true);
            comboBoxEditKullaniciAdi.SelectedIndex = 0;

            InitForm();
        }

        public void InitForm()
        {
            string strSQL = "exec sel_Menuler";

            if (trvTumYetkiler.Nodes.Count == 0)
            {
                DataTable dtTum = clSqlTanim.RunStoredProc(strSQL);
                clSqlTanim.FillTree(trvTumYetkiler, dtTum);
            }

            if (comboBoxEditKullaniciAdi.SelectedIndex > 0)
            {
                trvTumYetkilerKullanici.Nodes.Clear();
                strSQL = "exec sel_Menuler " + comboBoxEditKullaniciAdi.SecilenDeger().Id.ToString();
                DataSet ds = clSqlTanim.RunStoredProcDS(strSQL, "yetki");
                clSqlTanim.FillTree(trvTumYetkilerKullanici, ds.Tables[0]);
            }
        }

        private void YetkiTazele()
        {
            trvTumYetkilerKullanici.Nodes.Clear();
            if (KID == 0)
            {
                KID = clGenelTanim.KullaniciKodu;
                comboBoxEditKullaniciAdi.SecilenDeger().Id = KID;
                return;
            }
            KID = clGenelTanim.DBToInt32(comboBoxEditKullaniciAdi.SecilenDeger().Id.ToString());

            string strSQL = "exec sel_Menuler " + KID.ToString();
            DataTable dt = clSqlTanim.RunStoredProc(strSQL);
            clSqlTanim.FillTree(trvTumYetkilerKullanici, dt);
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            YetkiTazele();
        }

        private void trvTumYetkiler_DoubleClick(object sender, EventArgs e)
        {
            clSqlTanim.mucalsstree sn = (clSqlTanim.mucalsstree)trvTumYetkiler.SelectedNode;
            if (sn == null) return;
            string strQuery = "exec up_MenuEkle " + comboBoxEditKullaniciAdi.SecilenDeger().Id.ToString() + "," + sn.SetGetId.ToString() + ",1";
            clSqlTanim.RunStoredProc(strQuery);
            YetkiTazele();
        }

        private void trvTumYetkilerKullanici_DoubleClick(object sender, EventArgs e)
        {
            clSqlTanim.mucalsstree sn = (clSqlTanim.mucalsstree)trvTumYetkilerKullanici.SelectedNode;
            if (sn == null) return;
            string strQuery = "exec up_MenuEkle " + comboBoxEditKullaniciAdi.SecilenDeger().Id.ToString() + "," + sn.SetGetId.ToString() + ",0";
            clSqlTanim.RunStoredProc(strQuery);
            YetkiTazele();
        }

        private void barButtonItemYetkilerKapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();

                //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
                //frmGirisE.pictureEdit1.BringToFront();
            }
        }

        private void barButtonItemYetkilerYeniKayıt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            layoutControlMalzemeTalepYetkiler.LayoutKontrolleriniSifirla();
        }

        private void barButtonItemYetkilerKaydet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitForm();
            XtraMessageBox.Show("Kayıt İşlemi Başarılı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmYetkiEkle_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            //frmGirisE.pictureEdit1.BringToFront();
        }
    }

}
