using DevExpress.Utils;
using DevExpress.XtraBars;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmGirisEkran : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmGirisEkran()
        {

            InitializeComponent();

            ribbon.Minimized = true;

            barButtonItemMalzemeTanimlama.Links[0].Visible = false;
            barButtonItemMalzemeIstatistikleri.Links[0].Visible = false;
            barButtonItemKullaniciEkle.Links[0].Visible = false;
            barButtonItemDepartmanEkle.Links[0].Visible = false;
            barButtonItemYetkiEkle.Links[0].Visible = false;
            barButtonItemAnaGrupEkle.Links[0].Visible = false;
            barButtonItemMalzemeTalepIhtiyac.Links[0].Visible = false;
            barButtonItemMalzemeTalepDetay.Links[0].Visible = false;
            barButtonItemYillikKullanimRaporu.Links[0].Visible = false;
            barButtonItemStokDurumlari.Links[0].Visible = false;
            barButtonItemSiparisKarsilama.Links[0].Visible = false;
            barButtonItemSiparisOlusturma.Links[0].Visible = false;
            barButtonItemSiparisOnayi.Links[0].Visible = false;
            barButtonItemSiparisRaporu.Links[0].Visible = false;
            barButtonItemSiparisIstatistik.Links[0].Visible = false;
            barButtonItemDepoStokDurum.Links[0].Visible = false;
            barButtonItemNotEkle.Links[0].Visible = false;
            barButtonItemGrupEkle.Links[0].Visible = false;
            barButtonItemSatinAlmaKategoriEkle.Links[0].Visible = false;

            string strSQL = "SELECT TBL_LST_MALZEMEMENULER.MALZEMEISTEMMENU_ID, TBL_LST_MALZEMEMENULER.MALZEMEISTEMMENU_ADI, ";
            strSQL += "TBL_LST_MALZEMEMENULER.MALZEMEISTEMMENU_SIRANO, TBL_LST_MALZEMEYETKILER.MALZEMEISTEMMENU_YETKI ";
            strSQL += "FROM TBL_LST_MALZEMEYETKILER (NOLOCK) ";
            strSQL += "JOIN TBL_LST_MALZEMEMENULER ON TBL_LST_MALZEMEYETKILER.MALZEMEISTEMMENULER_ID=TBL_LST_MALZEMEMENULER.MALZEMEISTEMMENU_ID ";
            strSQL += "AND ISNULL(TBL_LST_MALZEMEMENULER.MALZEMEISTEMMENU_DURUM,0)=1 AND TBL_LST_MALZEMEYETKILER.MALZEMEISTEMKULLANICI_KID=" + clGenelTanim.DBToInt32(clGenelTanim.KullaniciKodu);

            DataSet ds = clSqlTanim.RunStoredProcDS(strSQL, "Yetki");

            foreach (DataRow Satir in ds.Tables[0].Rows)
            {
                if (bool.Parse(Satir["MALZEMEISTEMMENU_YETKI"].ToString()) == true)
                {
                    foreach (DevExpress.XtraBars.Ribbon.RibbonPage MenuItem in ribbon.Pages)
                    {
                        if (MenuItem.Text == Satir["MALZEMEISTEMMENU_ADI"].ToString()) MenuItem.Visible = true;

                        foreach (DevExpress.XtraBars.Ribbon.RibbonPageGroup SubItem in MenuItem.Groups)
                        {
                            foreach (BarBaseButtonItemLink SubItem2 in SubItem.ItemLinks)
                            {
                                if (SubItem2.Caption == Satir["MALZEMEISTEMMENU_ADI"].ToString()) SubItem2.Visible = true;
                            }
                        }
                    }
                }
            }

        }

        public void viewChildForm(Form _form)
        {
            if (!isFormActived(_form))
            {
                _form.MdiParent = this;
                _form.Show();
            }
        }

        private bool isFormActived(Form form)
        {
            bool isOpenend = false;
            if (MdiChildren.Count() > 0)
            {
                foreach (var item in MdiChildren)
                {
                    if (form.Name == item.Name)
                    {
                        xtraTabbedMdiManager1.Pages[item].MdiChild.Activate();
                        isOpenend = true;
                    }
                }
            }
            return isOpenend;
        }
        private bool FormYuklumu(string formAdi)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name.Equals(formAdi, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        frmMalzemeTanimlama fr1;
        private void barButtonItemMalzemeTanimlama_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if(fr1==null)
            //{
            //    fr1 = new frmMalzemeTanimlama();
            //    fr1.MdiParent = this;
            //    this.Show();
            //}
            var form = new frmMalzemeTanimlama();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemMalzemeTalepleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeSiparisKarsilama();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemMalzemeTalep_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var form = new frmMalzemeSiparisOlustur();
            //viewChildForm(form);
            //pictureEdit1.SendToBack();
            //ribbon.Minimized = true;
            //ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemKullaniciEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmKullaniciEkle();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemDepartmanEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmDepartmanEkle();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemYetkiEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmYetkiEkle();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemMalzemeIstatistikleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeTalepIstatistikleri();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemGrupEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmAnaGrupEkle();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemMalzemeOtomatikTalep_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeTalepIhtiyac();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemMalzemeOtomatikTalepDetay_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeTalepAcilanlar();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemYillikKullanim_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeYillikKullanim();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemStokDurumlari_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeStokDurumlari();
            viewChildForm(form);
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
            pictureEdit1.SendToBack();
        }

        private void frmGirisEkran_Load(object sender, EventArgs e)
        {
            //string curentVersiyon = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            string versiyonBilgisi = "@ Versiyon  : " + Program.versionCode.ToString();
            barStaticItemVersiyon.Caption = versiyonBilgisi.ToString();

            //try
            //{
            //    if (CheckUpdate())
            //    {
            //        DialogResult dialog = MessageBox.Show("Yeni güncellemeler var. \n\rŞimdi Yüklemek istermisiniz?", "Güncelleme Bulundu", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //        if (dialog == DialogResult.Yes)
            //        {
            //            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(updateMe));
            //            t.Start();

            //            this.Close();
            //        }
            //    }
            //}
            //catch (Exception a)
            //{
            //    MessageBox.Show(a.Message);
            //}
        }

        private void frmGirisEkran_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItemMalzemeTalepIslemleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeSiparisKarsilama();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }


        private void barButtonItemSiparisOlusturma_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeSiparisOlustur();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemSiparisKarsilama_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeSiparisKarsilama();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemSiparisOnayi_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeSiparisOnayi();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemSiparisRaporu_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeSiparisRaporu();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemSiparisIstatistik_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var form = new frmMalzemeSiparisIstatistikleri();
            //viewChildForm(form);
            //pictureEdit1.SendToBack();
            //ribbon.Minimized = true;
            //ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemDepoStokDurum_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeDepoStokDurum();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemNotEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeNotlar();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemGrupEkle_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            var form = new frmGrupEkle();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }

        private void barButtonItemSatinAlmaKategoriEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmSatinAlmaKategoriEkle();
            viewChildForm(form);
            pictureEdit1.SendToBack();
            ribbon.Minimized = true;
            ribbon.ShowExpandCollapseButton = DefaultBoolean.True;
        }
    }
}