using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeSiparisRaporu : FrmBase
    {
        public frmMalzemeSiparisRaporu()
        {
            InitializeComponent();
        }

        private void barButtonItemKapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void barButtonItemYazdir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemYazdir1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraBars.BarButtonItem bi = (DevExpress.XtraBars.BarButtonItem)e.Item;
            string uzanti = string.Empty;

            if (bi == barButtonItemYazdir)
            {
                this.gridControlSatisRapor.ShowPrintPreview();
            }
            else if (bi == barButtonItemExcel)
            {
                clGenelTanim.OpenSaveDlg(this.gridControlSatisRapor, saveFileDialogMalzemeRapor, "xls");
            }
            else if (bi == barButtonItemPdf)
            {
                clGenelTanim.OpenSaveDlg(this.gridControlSatisRapor, saveFileDialogMalzemeRapor, "pdf");
            }
        }

        private void barButtonItemPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void frmMalzemeSiparisRaporu_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT MALZEMEDEPOISTEM_ID AS 'SİPARİŞ NO', e.MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', f.MALZEME_DEPARTMANADI AS 'MASRAF YERİ', Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylandi' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' else 'Onaysiz' end 'SİPARİŞ DURUM', ");
            sb.Append("g.MALZEMEKULLANICI_ADI as 'SİPARİŞ EDEN', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ', h.MALZEMEKULLANICI_ADI as 'ONAY VEREN', MALZEMEDEPOISTEM_ONAYTARIHI AS 'ONAY TARİHİ', d.MALZEMEKULLANICI_ADI AS 'TESLİM ALAN', MALZEMEDEPOISTEM_TESLIMTARIHI AS 'TESLİM TARİHİ', ");
            sb.Append("MALZEMEISTEM_MATERYALNO AS 'MATERYALNO', MALZEMEISTEM_ADI 'MALZEME ADI', MALZEMECIKIS_ADET AS 'ÇIKIŞ ADET', MALZEMECIKIS_SORGUBIRIMFIYAT AS 'BİRİM FİYAT', MALZEMECIKIS_SORGUTOPLAMFIYAT 'TOPLAM FİYAT' ");
            sb.Append("FROM TBL_LST_MALZEMEDEPOISTEM a (NOLOCK) ");
            sb.Append("JOIN TBL_LST_MALZEMEISTEM b (nolock) on a.MALZEMEDEPOISTEM_ID = b.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
            sb.Append("JOIN TBL_LST_MALZEMECIKIS c (nolock) on b.MALZEMEISTEM_MALZEMELERID = c.MALZEMECIKIS_MALZEMELERID AND b.MALZEMEISTEM_MALZEMEDEPOISTEMID= c.MALZEMECIKIS_MALZEMEDEPOISTEM_ID ");
            sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR d (nolock) on d.MALZEMEKULLANICI_ID = a.MALZEMEDEPOISTEM_TESLIMKULLANICIID ");
            sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR e (nolock) on e.MALZEME_DEPARTMANID=a.MALZEMEDEPOISTEM_DEPARTMANID ");
            sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR f (nolock) on f.MALZEME_DEPARTMANID=c.MALZEMECIKIS_DEPARTMANID ");
            sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR g (nolock) on g.MALZEMEKULLANICI_ID=a.MALZEMEDEPOISTEM_KULLANICIID ");
            sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR h (nolock) on h.MALZEMEKULLANICI_ID=a.MALZEMEDEPOISTEM_ONAYKULLANICIID ");

            sb.Append("WHERE CONVERT(DATE, a.MALZEMEDEPOISTEM_ISTEMTARIHI) >= @basTarih AND CONVERT(DATE, a.MALZEMEDEPOISTEM_ISTEMTARIHI) <= @bitTarih ");
            sb.Append("AND a.MALZEMEDEPOISTEM_DEPARTMANID=@deptId ");
            sb.Append(" ORDER BY 4");

            DataTable dtSiparisler = clSqlTanim.RunStoredProc(sb.ToString(), new[] {
                new SqlParameter("@basTarih", SqlDbType.Date) { Value = dateEditBasTarih.DateTime.Date },
                new SqlParameter("@bitTarih", SqlDbType.Date) { Value = dateEditBitTarih.DateTime.Date },
                new SqlParameter("@deptId", clGenelTanim.currentMalzemeKullaniciDepartmanId)
            });
            gridControlSatisRapor.DataSource = dtSiparisler;

            this.gridViewSatisRapor.Columns["SİPARİŞ NO"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewSatisRapor.Columns["SİPARİŞ NO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewSatisRapor.Columns["SİPARİŞ NO"].SummaryItem.DisplayFormat = "{0} Kayit";

            this.SetGridFont(gridViewSatisRapor, new Font("Tahoma", 10, FontStyle.Bold));


            this.gridViewSatisRapor.Columns["BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewSatisRapor.Columns["BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewSatisRapor.Columns["BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewSatisRapor.Columns["TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewSatisRapor.Columns["TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewSatisRapor.Columns["TOPLAM FİYAT"].SummaryItem.DisplayFormat = "TOPLAM FİYAT: {0:c2}";

            // Make the group footers always visible.
            gridViewSatisRapor.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
            // Create and setup the first summary item.
            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = "SİPARİŞ NO";
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridViewSatisRapor.GroupSummary.Add(item);
            // Create and setup the second summary item.
            GridGroupSummaryItem item1 = new GridGroupSummaryItem();
            item1.FieldName = "TOPLAM FİYAT";
            item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            item1.Format = new System.Globalization.CultureInfo("de");
            item1.DisplayFormat = "Toplam : {0:c2}";
            item1.ShowInGroupColumnFooter = gridViewSatisRapor.Columns["TOPLAM FİYAT"];
            gridViewSatisRapor.GroupSummary.Add(item1);


            gridViewSatisRapor.Columns["SİPARİŞ NO"].Group();
            gridViewSatisRapor.ExpandAllGroups();

            this.gridViewSatisRapor.BestFitColumns();
        }

        private void barButtonItemListele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitForm();
        }

        private void frmMalzemeSiparisRaporu_Load(object sender, EventArgs e)
        {
            layoutControlRaporlama.LayoutKontrolleriniSifirla();
        }
    }
}
