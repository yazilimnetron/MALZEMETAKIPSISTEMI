using DevExpress.XtraEditors;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
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
                DataSet ds = clSqlTanim.RunStoredProcDS("exec sel_Menuler @kulId", "yetki",
                    new[] { new SqlParameter("@kulId", comboBoxEditKullaniciAdi.SecilenDeger().Id) });
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

            DataTable dt = clSqlTanim.RunStoredProc("exec sel_Menuler @kulId",
                new[] { new SqlParameter("@kulId", KID) });
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
            int kulId1 = clGenelTanim.DBToInt32(comboBoxEditKullaniciAdi.SecilenDeger().Id.ToString());
            clSqlTanim.ExecuteNonQuery("exec up_MenuEkle @kulId, @menuId, @durum",
                new[] { new SqlParameter("@kulId", kulId1), new SqlParameter("@menuId", sn.SetGetId), new SqlParameter("@durum", 1) });
            YetkiTazele();
        }

        private void trvTumYetkilerKullanici_DoubleClick(object sender, EventArgs e)
        {
            clSqlTanim.mucalsstree sn = (clSqlTanim.mucalsstree)trvTumYetkilerKullanici.SelectedNode;
            if (sn == null) return;
            int kulId2 = clGenelTanim.DBToInt32(comboBoxEditKullaniciAdi.SecilenDeger().Id.ToString());
            clSqlTanim.ExecuteNonQuery("exec up_MenuEkle @kulId, @menuId, @durum",
                new[] { new SqlParameter("@kulId", kulId2), new SqlParameter("@menuId", sn.SetGetId), new SqlParameter("@durum", 0) });
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
