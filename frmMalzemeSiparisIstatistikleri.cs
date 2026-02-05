using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeSiparisIstatistikleri : Form
    {
        public frmMalzemeSiparisIstatistikleri()
        {
            InitializeComponent();
        }

        void SetGridFont(GridView view, Font font)
        {
            foreach (AppearanceObject ap in view.Appearance)

                ap.Font = font;
        }

        private void SetContainsFilter(GridView view)
        {
            foreach (GridColumn col in view.Columns)
                col.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
        }

        private void barButtonItemKapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();

                //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
                //frmGirisE.pictureEdit1.BringToFront();
            }
        }

        private void barButtonItemYazdir1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraBars.BarButtonItem bi = (DevExpress.XtraBars.BarButtonItem)e.Item;
            string uzanti = string.Empty;

            if (bi == barButtonItemYazdir)
            {
                this.gridControlSiparisIstatistik.ShowPrintPreview();
            }
            else if (bi == barButtonItemExcel)
            {
                clGenelTanim.OpenSaveDlg(this.gridControlSiparisIstatistik, saveFileDialogMalzemeRapor, "xls");
            }
            else if (bi == barButtonItemPdf)
            {
                clGenelTanim.OpenSaveDlg(this.gridControlSiparisIstatistik, saveFileDialogMalzemeRapor, "pdf");
            }
        }

        private void frmMalzemeSiparisIstatistikleri_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            //frmGirisE.pictureEdit1.BringToFront();
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT MALZEMEDEPOISTEM_ID AS 'SİPARİŞ NO', e.MALZEME_DEPARTMANADI AS 'MASRAF YERİ', Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylandı' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', ");
            sb.Append("f.MALZEMEKULLANICI_ADI as 'SİPARİŞ EDEN', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ', g.MALZEMEKULLANICI_ADI as 'ONAY VEREN', MALZEMEDEPOISTEM_ONAYTARIHI AS 'ONAY TARİHİ', MALZEMEDEPOISTEM_TESLIMTARIHI AS 'TESLİM TARİHİ', d.MALZEMEKULLANICI_ADI AS 'TESLİM ALAN', ");
            sb.Append("MALZEMEISTEM_MATERYALNO AS 'MATERYALNO', MALZEMEISTEM_ADI 'MALZEME ADI', MALZEMECIKIS_ADET AS 'ÇIKIŞ ADET', MALZEMECIKIS_SORGUBIRIMFIYAT AS 'BİRİM FİYAT', MALZEMECIKIS_SORGUTOPLAMFIYAT 'TOPLAM FİYAT' ");
            sb.Append("FROM TBL_LST_MALZEMEDEPOISTEM a (NOLOCK) ");
            sb.Append("JOIN TBL_LST_MALZEMEISTEM b (nolock) on a.MALZEMEDEPOISTEM_ID = b.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
            sb.Append("JOIN TBL_LST_MALZEMECIKIS c (nolock) on b.MALZEMEISTEM_MALZEMELERID = c.MALZEMECIKIS_MALZEMELERID AND b.MALZEMEISTEM_MALZEMEDEPOISTEMID= c.MALZEMECIKIS_MALZEMEDEPOISTEM_ID ");
            sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR d (nolock) on d.MALZEMEKULLANICI_ID = a.MALZEMEDEPOISTEM_TESLIMKULLANICIID ");
            sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR e (nolock) on e.MALZEME_DEPARTMANID=c.MALZEMECIKIS_DEPARTMANID ");
            sb.Append("left JOIN TBL_LST_MALZEMEKULLANICILAR f (nolock) on f.MALZEMEKULLANICI_ID=a.MALZEMEDEPOISTEM_KULLANICIID ");
            sb.Append("left JOIN TBL_LST_MALZEMEKULLANICILAR g (nolock) on g.MALZEMEKULLANICI_ID=a.MALZEMEDEPOISTEM_ONAYKULLANICIID ");
            sb.AppendFormat("WHERE CONVERT(VARCHAR(10),c.MALZEMECIKIS_TARIHI ,121)>={0}", "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.AppendFormat(" AND CONVERT(VARCHAR(10),c.MALZEMECIKIS_TARIHI ,121)<={0} ", "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.Append(" ORDER BY 4");

            DataTable dtSiparisler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlSiparisIstatistik.DataSource = dtSiparisler;

            this.gridViewSiparisIstatistik.Columns["SİPARİŞ NO"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridViewSiparisIstatistik.Columns["SİPARİŞ NO"].Visible = false;
            this.gridViewSiparisIstatistik.Columns["SİPARİŞ NO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewSiparisIstatistik.Columns["SİPARİŞ NO"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.SetGridFont(gridViewSiparisIstatistik, new Font("Tahoma", 10, FontStyle.Bold));


            this.gridViewSiparisIstatistik.Columns["BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewSiparisIstatistik.Columns["BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewSiparisIstatistik.Columns["BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewSiparisIstatistik.Columns["TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewSiparisIstatistik.Columns["TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewSiparisIstatistik.Columns["TOPLAM FİYAT"].SummaryItem.DisplayFormat = "TOPLAM FİYAT: {0:c2}";

            this.gridViewSiparisIstatistik.BestFitColumns();
        }

        private void barButtonItemListele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitForm();
        }

        private void frmMalzemeSiparisIstatistikleri_Load(object sender, EventArgs e)
        {
            layoutControlRaporlama.LayoutKontrolleriniSifirla();
        }

        private void barButtonItemExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");
            clGenelTanim.OpenSaveDlg(this.gridControlSiparisIstatistik, saveFileDialogMalzemeRapor, "xls");
        }
    }
}
