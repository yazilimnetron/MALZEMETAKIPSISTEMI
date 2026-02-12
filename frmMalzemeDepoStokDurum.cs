using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeDepoStokDurum : Form
    {
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
        public frmMalzemeDepoStokDurum()
        {
            InitializeComponent();
            //layoutControlDepoStokDurumu.LayoutKontrolleriniSifirla();
        }

        private void frmMalzemeDepoStokDurum_Load(object sender, EventArgs e)
        {
            var controller = new MyController();
            checkedComboBoxEditGrup.Properties.DataSource = controller.GetItems();
            checkedComboBoxEditGrup.Properties.ValueMember = "MALZEMEGRUP_ID";
            checkedComboBoxEditGrup.Properties.DisplayMember = "MALZEMEGRUP_ADI";

            dateEditBaslangicTarih.DateTime = Convert.ToDateTime("2016/01/01");
            dateEditBitisTarih.DateTime = DateTime.Now;
        }

        private void radioGroupCalismaYontemi_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitForm();
        }

        private void barButtonItemKapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void barSubItemYazdir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DevExpress.XtraBars.BarButtonItem bi = (DevExpress.XtraBars.BarButtonItem)e.Item;
            //string uzanti = string.Empty;

            //if (bi == barButtonItemYazdir)
            //{
            //    this.gridControlDepoDurum.ShowPrintPreview();
            //}
            //else if (bi == barButtonItemExcel)
            //{
            //    clGenelTanim.OpenSaveDlg(this.gridControlDepoDurum, saveFileDialogMalzemeRapor, "xls");
            //}
            //else if (bi == barButtonItemPdf)
            //{
            //    clGenelTanim.OpenSaveDlg(this.gridControlDepoDurum, saveFileDialogMalzemeRapor, "pdf");
            //}
        }

        private void frmMalzemeDepoStokDurum_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            //frmGirisE.pictureEdit1.BringToFront();
        }

        public void InitForm()
        {
            DateTime baslangic = dateEditBaslangicTarih.DateTime.Date;
            DateTime bitisExclusive = dateEditBitisTarih.DateTime.Date.AddDays(1);
            var selectedGroupIds = GetSelectedGroupIds();
            bool stokAlanindanCalis = checkEditTarih.Checked;

            string sql = BuildReportQuery(baslangic, bitisExclusive, selectedGroupIds, stokAlanindanCalis);
            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sql);
            gridControlDepoDurum.DataSource = dtMalzemeler;

            this.gridViewDepoDurum.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewDepoDurum.Columns["MALZEME MATERYEL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewDepoDurum.Columns["MALZEME MATERYEL"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewDepoDurum.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewDepoDurum.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewDepoDurum.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewDepoDurum.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewDepoDurum.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewDepoDurum.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewDepoDurum.Columns["STOK FAZLASI TOPLAM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewDepoDurum.Columns["STOK FAZLASI TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewDepoDurum.Columns["STOK FAZLASI TOPLAM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewDepoDurum.Columns["MALZEME GİRİŞ T.FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewDepoDurum.Columns["MALZEME GİRİŞ T.FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewDepoDurum.Columns["MALZEME GİRİŞ T.FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewDepoDurum.Columns["MALZEME ÇIKIŞ T.FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewDepoDurum.Columns["MALZEME ÇIKIŞ T.FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewDepoDurum.Columns["MALZEME ÇIKIŞ T.FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewDepoDurum.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewDepoDurum.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            this.gridViewDepoDurum.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.DisplayFormat = "Toplam Fiyat : {0:c2}";

            this.gridViewDepoDurum.Columns["STOK FAZLASI TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewDepoDurum.Columns["STOK FAZLASI TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            this.gridViewDepoDurum.Columns["STOK FAZLASI TOPLAM FİYAT"].SummaryItem.DisplayFormat = "Toplam Fiyat : {0:c2}";

            this.gridViewDepoDurum.Columns["MALZEME GİRİŞ T.FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewDepoDurum.Columns["MALZEME GİRİŞ T.FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            this.gridViewDepoDurum.Columns["MALZEME GİRİŞ T.FİYAT"].SummaryItem.DisplayFormat = "Toplam Fiyat : {0:c2}";

            this.gridViewDepoDurum.Columns["MALZEME ÇIKIŞ T.FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewDepoDurum.Columns["MALZEME ÇIKIŞ T.FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            this.gridViewDepoDurum.Columns["MALZEME ÇIKIŞ T.FİYAT"].SummaryItem.DisplayFormat = "Toplam Fiyat : {0:c2}";

            this.gridViewDepoDurum.Columns["MALZEME_ID"].Visible = false;
            this.gridViewDepoDurum.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewDepoDurum.Columns["MALZEME OUDBTNO"].Visible = false;

            this.SetGridFont(gridViewDepoDurum, new Font("Tahoma", 10, FontStyle.Bold));
            this.gridViewDepoDurum.BestFitColumns();
        }

        private List<int> GetSelectedGroupIds()
        {
            return checkedComboBoxEditGrup.Properties
                .GetItems().GetCheckedValues()
                .Cast<object>()
                .Select(v => clGenelTanim.DBToInt32(v))
                .Where(v => v > 0)
                .Distinct()
                .ToList();
        }

        private string BuildReportQuery(DateTime baslangic, DateTime bitisExclusive, List<int> selectedGroupIds, bool stokAlanindanCalis)
        {
            string bas = baslangic.ToString("yyyy-MM-dd");
            string bitEx = bitisExclusive.ToString("yyyy-MM-dd");
            string grupFilter = selectedGroupIds.Count > 0 ? $"AND m.MALZEME_ANAGRUBU IN ({string.Join(",", selectedGroupIds)})" : string.Empty;
            string stokExpr = stokAlanindanCalis
                ? "ISNULL(b.[MALZEME STOK MIKTARI],0)"
                : "ISNULL(gp.[MALZEME GİRİŞ ADET],0) - ISNULL(cp.[MALZEME ÇIKIŞ ADET],0)";

            var sb = new StringBuilder(4096);
            sb.AppendLine("WITH Base AS (");
            sb.AppendLine("    SELECT m.MALZEME_ID, m.MALZEME_OUDBTNO AS [MALZEME OUDBTNO], m.MALZEME_TURU,");
            sb.AppendLine("           m.MALZEME_MATERYAL AS [MALZEME MATERYEL], m.MALZEME_PARCANO AS [MALZEME PARÇANO], m.MALZEME_ADI AS [MALZEME ADI],");
            sb.AppendLine("           m.MALZEME_STOKMIKTARI AS [MALZEME STOK MIKTARI], m.MALZEME_MINADET AS [MALZEME MİN ADET], m.MALZEME_MAXADET AS [MALZEME MAX ADET],");
            sb.AppendLine("           m.MALZEME_RAFNO AS [MALZEME RAFNO],");
            sb.AppendLine("           ag.MALZEMEANAGRUP_ADI AS [MALZEME ANA GRUBU],");
            sb.AppendLine("           gr.MALZEMEGRUP_ADI AS [MALZEME GRUBU],");
            sb.AppendLine("           m.MALZEME_STOKSAY AS [MALZEME STOK SAY],");
            sb.AppendLine("           kat.MALZEMEKATEGORI_ADI + ' (' + kat.MALZEMEKATEGORI_KODU + ')' AS [MALZEME SATINALMA KATEGORISI],");
            sb.AppendLine("           m.MALZEME_NOTU AS [MALZEME NOTU]");
            sb.AppendLine("    FROM TBL_LST_MALZEMELER m WITH (NOLOCK)");
            sb.AppendLine("    LEFT JOIN TBL_LST_MALZEMEANAGRUPLAR ag WITH (NOLOCK) ON ag.MALZEMEANAGRUP_ID = m.MALZEME_ANAGRUBU");
            sb.AppendLine("    LEFT JOIN TBL_LST_MALZEMEGRUPLAR gr WITH (NOLOCK) ON gr.MALZEMEGRUP_ID = m.MALZEME_GRUBU");
            sb.AppendLine("    LEFT JOIN TBL_LST_MALZEMEKATEGORILER kat WITH (NOLOCK) ON kat.MALZEMEKATEGORI_ID = m.MALZEME_SATINALMAKATEGORI");
            sb.AppendLine("    WHERE 1=1");
            if (!string.IsNullOrEmpty(grupFilter))
                sb.AppendLine($"      {grupFilter}");
            sb.AppendLine("), GirisPeriod AS (");
            sb.AppendLine("    SELECT mg.MALZEMEGIRIS_MALZEMELERID AS MALZEME_ID, ISNULL(SUM(mg.MALZEMEGIRIS_ADET),0) AS [MALZEME GİRİŞ ADET]");
            sb.AppendLine("    FROM TBL_LST_MALZEMEGIRIS mg WITH (NOLOCK)");
            sb.AppendLine($"    WHERE mg.MALZEMEGIRIS_TARIH >= '{bas}' AND mg.MALZEMEGIRIS_TARIH < '{bitEx}'");
            sb.AppendLine("    GROUP BY mg.MALZEMEGIRIS_MALZEMELERID");
            sb.AppendLine("), CikisPeriod AS (");
            sb.AppendLine("    SELECT mc.MALZEMECIKIS_MALZEMELERID AS MALZEME_ID, ISNULL(SUM(mc.MALZEMECIKIS_ADET),0) AS [MALZEME ÇIKIŞ ADET]");
            sb.AppendLine("    FROM TBL_LST_MALZEMECIKIS mc WITH (NOLOCK)");
            sb.AppendLine($"    WHERE mc.MALZEMECIKIS_TARIHI >= '{bas}' AND mc.MALZEMECIKIS_TARIHI < '{bitEx}'");
            sb.AppendLine("    GROUP BY mc.MALZEMECIKIS_MALZEMELERID");
            sb.AppendLine("), LastGiris AS (");
            sb.AppendLine("    SELECT e.MALZEMEGIRIS_MALZEMELERID AS MALZEME_ID, e.MALZEMEGIRIS_PARABIRIMI AS [MALZEME GİRİŞ P.BİRİMİ],");
            sb.AppendLine("           ISNULL(e.MALZEMEGIRIS_BIRIMFIYAT,0) AS [MALZEME GİRİŞ B.FİYAT], ISNULL(e.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) AS [MALZEME B.FİYAT],");
            sb.AppendLine("           CONVERT(varchar(10), e.MALZEMEGIRIS_TARIH, 121) AS [MALZEME G.TARİH], YEAR(e.MALZEMEGIRIS_TARIH) AS [MALZEME G.YILI],");
            sb.AppendLine("           ROW_NUMBER() OVER(PARTITION BY e.MALZEMEGIRIS_MALZEMELERID ORDER BY e.MALZEMEGIRIS_TARIH DESC) rn");
            sb.AppendLine("    FROM TBL_LST_MALZEMEGIRIS e WITH (NOLOCK)");
            sb.AppendLine("), LastCikis AS (");
            sb.AppendLine("    SELECT e.MALZEMECIKIS_MALZEMELERID AS MALZEME_ID, CONVERT(varchar(10), e.MALZEMECIKIS_TARIHI, 121) AS [MALZEME Ç.TARİH],");
            sb.AppendLine("           YEAR(e.MALZEMECIKIS_TARIHI) AS [MALZEME Ç.YILI],");
            sb.AppendLine("           ROW_NUMBER() OVER(PARTITION BY e.MALZEMECIKIS_MALZEMELERID ORDER BY e.MALZEMECIKIS_TARIHI DESC) rn");
            sb.AppendLine("    FROM TBL_LST_MALZEMECIKIS e WITH (NOLOCK)");
            sb.AppendLine(")");
            sb.AppendLine("SELECT b.MALZEME_ID, b.[MALZEME OUDBTNO],");
            sb.AppendLine("       CASE WHEN b.MALZEME_TURU=1 THEN 'NLAG' WHEN b.MALZEME_TURU=2 THEN 'UNBW' ELSE '<<Seçiniz>>' END [MALZEME TURU],");
            sb.AppendLine("       b.[MALZEME MATERYEL], b.[MALZEME PARÇANO], b.[MALZEME ADI],");
            sb.AppendLine("       CASE WHEN lg.[MALZEME GİRİŞ P.BİRİMİ]=1 THEN 'TL' WHEN lg.[MALZEME GİRİŞ P.BİRİMİ]=2 THEN '€' WHEN lg.[MALZEME GİRİŞ P.BİRİMİ]=3 THEN '$'");
            sb.AppendLine("            WHEN lg.[MALZEME GİRİŞ P.BİRİMİ]=4 THEN 'JPY' WHEN lg.[MALZEME GİRİŞ P.BİRİMİ]=5 THEN 'CHF' WHEN lg.[MALZEME GİRİŞ P.BİRİMİ]=6 THEN 'GBP' END [MALZEME G.PARA BİRİMİ],");
            sb.AppendLine("       ISNULL(lg.[MALZEME GİRİŞ B.FİYAT],0) AS [MALZEME GİRİŞ B.FİYAT],");
            sb.AppendLine("       ISNULL(gp.[MALZEME GİRİŞ ADET],0) AS [MALZEME GİRİŞ ADET],");
            sb.AppendLine("       ISNULL(gp.[MALZEME GİRİŞ ADET],0) * ISNULL(lg.[MALZEME B.FİYAT],0) AS [MALZEME GİRİŞ T.FİYAT],");
            sb.AppendLine("       ISNULL(cp.[MALZEME ÇIKIŞ ADET],0) AS [MALZEME ÇIKIŞ ADET],");
            sb.AppendLine("       ISNULL(cp.[MALZEME ÇIKIŞ ADET],0) * ISNULL(lg.[MALZEME B.FİYAT],0) AS [MALZEME ÇIKIŞ T.FİYAT],");
            sb.AppendLine($"       {stokExpr} AS [MALZEME STOK MİKTAR],");
            sb.AppendLine("       b.[MALZEME MİN ADET], b.[MALZEME MAX ADET],");
            sb.AppendLine($"       CASE WHEN {stokExpr} > b.[MALZEME MAX ADET] THEN {stokExpr} - b.[MALZEME MAX ADET] ELSE 0 END AS [STOK FAZLASI MİKTAR],");
            sb.AppendLine("       ISNULL(lg.[MALZEME B.FİYAT],0) AS [MALZEME BİRİM FİYAT],");
            sb.AppendLine($"       {stokExpr} * ISNULL(lg.[MALZEME B.FİYAT],0) AS [MALZEME TOPLAM FİYAT],");
            sb.AppendLine($"       CASE WHEN {stokExpr} > b.[MALZEME MAX ADET] THEN ({stokExpr} - b.[MALZEME MAX ADET]) * ISNULL(lg.[MALZEME B.FİYAT],0) ELSE 0 END AS [STOK FAZLASI TOPLAM FİYAT],");
            sb.AppendLine("       lg.[MALZEME G.TARİH] AS [MALZEME GİRİŞ TARİH], lc.[MALZEME Ç.TARİH] AS [MALZEME ÇIKIŞ TARİH],");
            sb.AppendLine("       b.[MALZEME RAFNO], b.[MALZEME ANA GRUBU], b.[MALZEME GRUBU], b.[MALZEME STOK SAY], b.[MALZEME SATINALMA KATEGORISI], b.[MALZEME NOTU],");
            sb.AppendLine("       lg.[MALZEME G.YILI], lc.[MALZEME Ç.YILI]");
            sb.AppendLine("FROM Base b");
            sb.AppendLine("LEFT JOIN GirisPeriod gp ON gp.MALZEME_ID = b.MALZEME_ID");
            sb.AppendLine("LEFT JOIN CikisPeriod cp ON cp.MALZEME_ID = b.MALZEME_ID");
            sb.AppendLine("LEFT JOIN LastGiris lg ON lg.MALZEME_ID = b.MALZEME_ID AND lg.rn = 1");
            sb.AppendLine("LEFT JOIN LastCikis lc ON lc.MALZEME_ID = b.MALZEME_ID AND lc.rn = 1");
            sb.AppendLine("WHERE ISNULL(gp.[MALZEME GİRİŞ ADET],0) > 0");
            sb.AppendLine("ORDER BY b.MALZEME_ID DESC");

            return sb.ToString();
        }

        private void barButtonItemListele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitForm();
        }

        public class MyModel
        {
            public int MALZEMEGRUP_ID { get; set; }
            public string MALZEMEGRUP_ADI { get; set; }
            public string FullId { get { return String.Format("{0}{1}", MALZEMEGRUP_ID, MALZEMEGRUP_ADI); } }
        }

        public class MyModel2
        {
            public int MALZEME_DEPARTMANID { get; set; }
            public string MALZEME_DEPARTMANADI { get; set; }
            public string FullId { get { return String.Format("{0}{1}", MALZEME_DEPARTMANID, MALZEME_DEPARTMANADI); } }
        }

        public class MyController
        {
            public List<MyModel> GetItems()
            {
                var list = new List<MyModel>();
                //fill the list from your database

                string strQuery = "SELECT MALZEMEANAGRUP_ID, MALZEMEANAGRUP_ADI AS 'MALZEME GRUBU' FROM TBL_LST_MALZEMEANAGRUPLAR (NOLOCK)  WHERE ISNULL (MALZEMEANAGRUP_DURUM,0)=1 ORDER BY 2";

                using (SqlConnection conn = new SqlConnection(clSqlTanim.connectionString))
                {
                    using (SqlCommand sCommand = new SqlCommand(strQuery, conn))
                    {
                        conn.Open();
                        using (SqlDataReader dr = sCommand.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                MyModel departmanlar = new MyModel();
                                departmanlar.MALZEMEGRUP_ID = Convert.ToInt32(dr[0]);
                                departmanlar.MALZEMEGRUP_ADI = dr[1].ToString();
                                list.Add(departmanlar);
                            }
                        }
                        conn.Close();
                    }
                }

                return list;
            }
        }

        public class MyController2
        {
            public List<MyModel2> GetItems()
            {
                var list = new List<MyModel2>();
                //fill the list from your database

                string strQuery = "SELECT MALZEME_DEPARTMANID, MALZEME_DEPARTMANADI as 'MALZEME DEPARTMANI' FROM TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK)  WHERE ISNULL (MALZEME_DEPARTMANDURUM,0)=1 ORDER BY 2";

                using (SqlConnection conn = new SqlConnection(clSqlTanim.connectionString))
                {
                    using (SqlCommand sCommand = new SqlCommand(strQuery, conn))
                    {
                        conn.Open();
                        using (SqlDataReader dr = sCommand.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                MyModel2 departmanlar2 = new MyModel2();
                                departmanlar2.MALZEME_DEPARTMANID = Convert.ToInt32(dr[0]);
                                departmanlar2.MALZEME_DEPARTMANADI = dr[1].ToString();
                                list.Add(departmanlar2);
                            }
                        }
                        conn.Close();
                    }
                }

                return list;
            }
        }

        private void barButtonItemYazdir1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");

            DevExpress.XtraBars.BarButtonItem bi = (DevExpress.XtraBars.BarButtonItem)e.Item;
            string uzanti = string.Empty;

            if (bi == barButtonItemYazdir1)
            {
                this.gridControlDepoDurum.ShowPrintPreview();
            }
            else if (bi == barButtonItemExcel)
            {
                clGenelTanim.OpenSaveDlg(this.gridControlDepoDurum, saveFileDialogMalzemeRapor, "xls");

            }
            else if (bi == barButtonItemPdf)
            {
                clGenelTanim.OpenSaveDlg(this.gridControlDepoDurum, saveFileDialogMalzemeRapor, "pdf");

            }
        }

        private void barButtonItemExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");
            clGenelTanim.OpenSaveDlg(this.gridControlDepoDurum, saveFileDialogMalzemeRapor, "xls");
        }


    }
}
