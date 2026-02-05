using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmGirisEkran____ : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Image img;
        MdiClient client;
        public frmGirisEkran____()
        {
            InitializeComponent();

            frmGiris frmgiris = new frmGiris();
            frmgiris.ShowDialog();

            //barButtonItemMalzemeTanimlama.Links[0].Visible = false;
            //barButtonItemMalzemeTalepleri.Links[0].Visible = false;
            //barButtonItemMalzemeTalep.Links[0].Visible = false;
            //barButtonItemMalzemeIstatistikleri.Links[0].Visible = false;
            //barButtonItemKullaniciEkle.Links[0].Visible = false;
            //barButtonItemDepartmanEkle.Links[0].Visible = false;
            //barButtonItemYetkiEkle.Links[0].Visible = false;

            string strSQL = "SELECT TBL_LST_MALZEMEMENULER.MALZEMEISTEMMENU_ID, TBL_LST_MALZEMEMENULER.MALZEMEISTEMMENU_ADI, ";
            strSQL += "TBL_LST_MALZEMEMENULER.MALZEMEISTEMMENU_SIRANO, TBL_LST_MALZEMEYETKILER.MALZEMEISTEMMENU_YETKI ";
            strSQL += "FROM TBL_LST_MALZEMEYETKILER (NOLOCK) ";
            strSQL += "JOIN TBL_LST_MALZEMEMENULER ON TBL_LST_MALZEMEYETKILER.MALZEMEISTEMMENULER_ID=TBL_LST_MALZEMEMENULER.MALZEMEISTEMMENU_ID ";
            strSQL += "AND TBL_LST_MALZEMEYETKILER.MALZEMEISTEMKULLANICI_KID=" + clGenelTanim.DBToInt32(clGenelTanim.KullaniciKodu);

            DataSet ds = clSqlTanim.RunStoredProcDS(strSQL, "Yetki");

            foreach(DataRow Satir in ds.Tables[0].Rows)
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

        private void barButtonItemMalzemeTanimlama_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeTanimlama();
            viewChildForm(form);
        }

        private void barButtonItemMalzemeTalepleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var form = new frmExcelYukle();
            //viewChildForm(form);
            var form = new frmMalzemeTalepIslemleri();
            viewChildForm(form);
        }

        private void barButtonItemMalzemeTalep_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeTalep();
            viewChildForm(form);
        }

        private void barButtonItemKullaniciTanim_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmKullaniciEkle();
            viewChildForm(form);
        }

        private void barButtonItemDepartmanEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmDepartmanEkle();
            viewChildForm(form);
        }

        private void barButtonItemMalzemeIstatistikleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmMalzemeTalepIstatiskleri();
            viewChildForm(form);
        }

        private void barButtonItemYetkiEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new frmYetkiEkle();
            viewChildForm(form);
        }


        private void frmGirisEkran_Load(object sender, EventArgs e)
        {
            // img = MALZEME_TAKIP_SISTEMI.Properties.Resources.BekaertDesleelogo;
            client = Controls.OfType<MdiClient>().FirstOrDefault();
            client.Paint += client_Paint;
        }
        void client_Paint(object sender, PaintEventArgs e)
        {
            //MdiClient client = sender as MdiClient;
            //e.Graphics.DrawImage(img, new Rectangle(new Point(-client.Left, -client.Top), this.ClientSize));
        }

        private void barButtonItemClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

    }
}