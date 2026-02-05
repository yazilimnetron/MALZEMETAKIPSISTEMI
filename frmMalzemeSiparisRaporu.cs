using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeSiparisRaporu : Form
    {
        public frmMalzemeSiparisRaporu()
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
            //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            //frmGirisE.pictureEdit1.BringToFront();
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT MALZEMEDEPOISTEM_ID AS 'SİPARİŞ NO', e.MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', f.MALZEME_DEPARTMANADI AS 'MASRAF YERİ', Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylandı' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' else 'Onaysız' end 'SİPARİŞ DURUM', ");
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

            sb.AppendFormat("WHERE CONVERT(VARCHAR(10),a.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)>={0}", "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.AppendFormat(" AND CONVERT(VARCHAR(10),a.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)<={0} ", "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.AppendFormat(" AND a.MALZEMEDEPOISTEM_DEPARTMANID={0}", clGenelTanim.currentMalzemeKullanıcıDepartmanId.ToString());
            sb.Append(" ORDER BY 4");

            DataTable dtSiparisler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlSatisRapor.DataSource = dtSiparisler;

            this.gridViewSatisRapor.Columns["SİPARİŞ NO"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewSatisRapor.Columns["SİPARİŞ NO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewSatisRapor.Columns["SİPARİŞ NO"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.SetGridFont(gridViewSatisRapor, new Font("Tahoma", 10, FontStyle.Bold));


            this.gridViewSatisRapor.Columns["BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewSatisRapor.Columns["BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewSatisRapor.Columns["BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewSatisRapor.Columns["TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewSatisRapor.Columns["TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewSatisRapor.Columns["TOPLAM FİYAT"].SummaryItem.DisplayFormat = "TOPLAM FİYAT: {0:c2}";

            //GridGroupSummaryItem item = new GridGroupSummaryItem();
            //item.FieldName = "SİPARİŞ NO";
            //item.ShowInGroupColumnFooter = gridViewSatisRapor.Columns["SİPARİŞ NO"];
            //item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //item.DisplayFormat = "Sum = {0:c2}";
            //gridViewSatisRapor.GroupSummary.Add(item);

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

        public void grupla()
        {
            //gridView1.Columns["value"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;

            //GridGroupSummaryItem grSummaryValue = new GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "value", gridView1.Columns["value"], "{0:n2}");
            //gridView1.GroupSummary.Add(grSummaryValue);

            //GridGroupSummaryItem grSummaryDocNr = new GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "docNr", gridView1.Columns["docNr"], "{0:n}");
            //gridView1.GroupSummary.Add(grSummaryDocNr);

            //gridView1.Columns["type"].Group();
            //gridView1.ExpandAllGroups();
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
