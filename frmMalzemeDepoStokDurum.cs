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
        string whereString = string.Empty;
        string whereString2 = string.Empty;

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
            // 1) Tarihleri ISO formatına çevir (literal olarak gömeceğiz)
            var baslangic = dateEditBaslangicTarih.DateTime.Date.ToString("yyyy-MM-dd");
            var bitis = dateEditBitisTarih.DateTime.Date.ToString("yyyy-MM-dd");

            // 2) Grup filtresi (seçiliyse IN listesi üret)
            var selectedGroups = checkedComboBoxEditGrup.Properties
                .GetItems().GetCheckedValues()
                .Cast<string>()
                .ToList();

            // Tek tırnak kaçırma ile güvenli IN listesi
            string SafeQ(string s) => "N'" + (s ?? "").Replace("'", "''") + "'";
            string inList = selectedGroups.Count > 0
                ? string.Join(",", selectedGroups.Select(SafeQ))
                : "";

            // 3) Sorgu inşası (text olarak çalıştıracağız)
            var sb = new StringBuilder(2048);

            if (!checkEditTarih.Checked)
            {
                sb.AppendLine("SELECT MALZEME_ID, [MALZEME OUDBTNO], CASE WHEN [MALZEME TURU]=1 THEN 'NLAG' WHEN [MALZEME TURU]=2 THEN 'UNBW' ELSE '<<Seçiniz>>' END [MALZEME TURU],");
                sb.AppendLine("       [MALZEME MATERYEL], [MALZEME PARÇANO], [MALZEME ADI],");
                sb.AppendLine("       CASE WHEN [MALZEME GİRİŞ P.BİRİMİ] = 1 THEN 'TL' WHEN [MALZEME GİRİŞ P.BİRİMİ] = 2 THEN '€' WHEN [MALZEME GİRİŞ P.BİRİMİ] = 3 THEN '$'");
                sb.AppendLine("            WHEN [MALZEME GİRİŞ P.BİRİMİ] = 4 THEN 'JPY' WHEN [MALZEME GİRİŞ P.BİRİMİ] = 5 THEN 'CHF' WHEN [MALZEME GİRİŞ P.BİRİMİ] = 6 THEN 'GBP' END [MALZEME G.PARA BİRİMİ],");
                sb.AppendLine("       ISNULL([MALZEME GİRİŞ B.FİYAT],0) [MALZEME GİRİŞ B.FİYAT],");
                sb.AppendLine("       ISNULL([MALZEME GİRİŞ ADET],0) [MALZEME GİRİŞ ADET], ISNULL([MALZEME GİRİŞ ADET],0)*[MALZEME B.FİYAT] [MALZEME GİRİŞ T.FİYAT],");
                sb.AppendLine("       ISNULL([MALZEME ÇIKIŞ ADET],0) [MALZEME ÇIKIŞ ADET], ISNULL([MALZEME ÇIKIŞ ADET],0)*[MALZEME B.FİYAT] [MALZEME ÇIKIŞ T.FİYAT],");
                sb.AppendLine("       (ISNULL([MALZEME GİRİŞ ADET],0) - ISNULL([MALZEME ÇIKIŞ ADET],0)) [MALZEME STOK MİKTAR], [MALZEME MİN ADET], [MALZEME MAX ADET],");
                sb.AppendLine("       CASE WHEN (ISNULL([MALZEME GİRİŞ ADET],0) - ISNULL([MALZEME ÇIKIŞ ADET],0)) > [MALZEME MAX ADET]");
                sb.AppendLine("            THEN (ISNULL([MALZEME GİRİŞ ADET],0) - ISNULL([MALZEME ÇIKIŞ ADET],0)) - [MALZEME MAX ADET] ELSE 0 END AS [STOK FAZLASI MİKTAR],");
                sb.AppendLine("       [MALZEME B.FİYAT] AS [MALZEME BİRİM FİYAT],");
                sb.AppendLine("       (ISNULL([MALZEME GİRİŞ ADET],0) - ISNULL([MALZEME ÇIKIŞ ADET],0)) * [MALZEME B.FİYAT] AS [MALZEME TOPLAM FİYAT],");
                sb.AppendLine("       CASE WHEN (ISNULL([MALZEME GİRİŞ ADET],0) - ISNULL([MALZEME ÇIKIŞ ADET],0)) > [MALZEME MAX ADET]");
                sb.AppendLine("            THEN ((ISNULL([MALZEME GİRİŞ ADET],0) - ISNULL([MALZEME ÇIKIŞ ADET],0)) - [MALZEME MAX ADET]) * [MALZEME B.FİYAT] ELSE 0 END AS [STOK FAZLASI TOPLAM FİYAT],");
                sb.AppendLine("       [MALZEME G.TARİH] AS [MALZEME GİRİŞ TARİH], [MALZEME Ç.TARİH] AS [MALZEME ÇIKIŞ TARİH], [MALZEME RAFNO], [MALZEME ANA GRUBU],");
                sb.AppendLine("       [MALZEME GRUBU], [MALZEME STOK SAY], [MALZEME SATINALMA KATEGORISI], [MALZEME NOTU], [MALZEME G.YILI], [MALZEME Ç.YILI]");
                sb.AppendLine("FROM (");
                sb.AppendLine("    SELECT m.MALZEME_ID, m.MALZEME_OUDBTNO AS [MALZEME OUDBTNO], m.MALZEME_TURU AS [MALZEME TURU],");
                sb.AppendLine("           m.MALZEME_MATERYAL AS [MALZEME MATERYEL], m.MALZEME_PARCANO AS [MALZEME PARÇANO], m.MALZEME_ADI AS [MALZEME ADI],");
                sb.AppendLine("           (SELECT TOP 1 e.MALZEMEGIRIS_PARABIRIMI");
                sb.AppendLine("            FROM TBL_LST_MALZEMEGIRIS e");
                sb.AppendLine("            WHERE m.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID");
                sb.AppendLine("            ORDER BY e.MALZEMEGIRIS_TARIH DESC) AS [MALZEME GİRİŞ P.BİRİMİ],");
                sb.AppendLine("           (SELECT TOP 1 ISNULL(e.MALZEMEGIRIS_BIRIMFIYAT,0)");
                sb.AppendLine("            FROM TBL_LST_MALZEMEGIRIS e");
                sb.AppendLine("            WHERE m.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID");
                sb.AppendLine("            ORDER BY e.MALZEMEGIRIS_TARIH DESC) AS [MALZEME GİRİŞ B.FİYAT],");
                sb.AppendLine("           m.MALZEME_MINADET AS [MALZEME MİN ADET],");

                // GİRİŞ ADET (sargable tarih)
                sb.AppendLine($"           (SELECT SUM(mg.MALZEMEGIRIS_ADET)");
                sb.AppendLine("            FROM TBL_LST_MALZEMEGIRIS mg");
                sb.AppendLine("            WHERE m.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID");
                sb.AppendLine($"              AND mg.MALZEMEGIRIS_TARIH >= '{baslangic}'");
                sb.AppendLine($"              AND mg.MALZEMEGIRIS_TARIH <  DATEADD(day,1,'{bitis}')) AS [MALZEME GİRİŞ ADET],");

                // ÇIKIŞ ADET (sargable tarih)
                sb.AppendLine($"           (SELECT SUM(mc.MALZEMECIKIS_ADET)");
                sb.AppendLine("            FROM TBL_LST_MALZEMECIKIS mc");
                sb.AppendLine("            WHERE m.MALZEME_ID = mc.MALZEMECIKIS_MALZEMELERID");
                sb.AppendLine($"              AND mc.MALZEMECIKIS_TARIHI >= '{baslangic}'");
                sb.AppendLine($"              AND mc.MALZEMECIKIS_TARIHI <  DATEADD(day,1,'{bitis}')) AS [MALZEME ÇIKIŞ ADET],");

                sb.AppendLine("           m.MALZEME_MAXADET AS [MALZEME MAX ADET],");
                sb.AppendLine("           (SELECT TOP 1 e.MALZEMEGIRIS_SORGUBIRIMFIYAT");
                sb.AppendLine("            FROM TBL_LST_MALZEMEGIRIS e");
                sb.AppendLine("            WHERE m.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID");
                sb.AppendLine("            ORDER BY e.MALZEMEGIRIS_TARIH DESC) AS [MALZEME B.FİYAT],");
                sb.AppendLine("           (SELECT TOP 1 CONVERT(varchar(10), e.MALZEMEGIRIS_TARIH, 121)");
                sb.AppendLine("            FROM TBL_LST_MALZEMEGIRIS e");
                sb.AppendLine("            WHERE m.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID");
                sb.AppendLine("            ORDER BY e.MALZEMEGIRIS_TARIH DESC) AS [MALZEME G.TARİH],");
                sb.AppendLine("           (SELECT TOP 1 CONVERT(varchar(10), e.MALZEMECIKIS_TARIHI, 121)");
                sb.AppendLine("            FROM TBL_LST_MALZEMECIKIS e");
                sb.AppendLine("            WHERE m.MALZEME_ID = e.MALZEMECIKIS_MALZEMELERID");
                sb.AppendLine("            ORDER BY e.MALZEMECIKIS_TARIHI DESC) AS [MALZEME Ç.TARİH],");
                sb.AppendLine("           m.MALZEME_RAFNO AS [MALZEME RAFNO],");

                // ANA GRUP ADI (seçime göre IN)
                sb.Append("           (SELECT d.MALZEMEANAGRUP_ADI");
                sb.AppendLine("            FROM TBL_LST_MALZEMEANAGRUPLAR d");
                sb.AppendLine("            WHERE m.MALZEME_ANAGRUBU = d.MALZEMEANAGRUP_ID");
                if (!string.IsNullOrEmpty(inList))
                    sb.AppendLine($"              AND d.MALZEMEANAGRUP_ADI IN ({inList})");
                sb.AppendLine("           ) AS [MALZEME ANA GRUBU],");

                // >>> MALZEME GRUBU ADI (ID yerine AD)
                sb.AppendLine("           (SELECT TOP 1 g.MALZEMEGRUP_ADI");
                sb.AppendLine("            FROM TBL_LST_MALZEMEGRUPLAR g");
                sb.AppendLine("            WHERE g.MALZEMEGRUP_ID = m.MALZEME_GRUBU) AS [MALZEME GRUBU],");

                sb.AppendLine("           m.MALZEME_STOKSAY AS [MALZEME STOK SAY],");
                sb.AppendLine("           (SELECT k.MALZEMEKATEGORI_ADI + ' (' + k.MALZEMEKATEGORI_KODU + ')'");
                sb.AppendLine("            FROM TBL_LST_MALZEMEKATEGORILER k WHERE m.MALZEME_SATINALMAKATEGORI = k.MALZEMEKATEGORI_ID)");
                sb.AppendLine("           AS [MALZEME SATINALMA KATEGORISI],");
                sb.AppendLine("           m.MALZEME_NOTU AS [MALZEME NOTU],");
                sb.AppendLine("           (SELECT TOP 1 YEAR(e.MALZEMEGIRIS_TARIH)");
                sb.AppendLine("            FROM TBL_LST_MALZEMEGIRIS e");
                sb.AppendLine("            WHERE m.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID");
                sb.AppendLine("            ORDER BY e.MALZEMEGIRIS_TARIH DESC) AS [MALZEME G.YILI],");
                sb.AppendLine("           (SELECT TOP 1 YEAR(e.MALZEMECIKIS_TARIHI)");
                sb.AppendLine("            FROM TBL_LST_MALZEMECIKIS e");
                sb.AppendLine("            WHERE m.MALZEME_ID = e.MALZEMECIKIS_MALZEMELERID");
                sb.AppendLine("            ORDER BY e.MALZEMECIKIS_TARIHI DESC) AS [MALZEME Ç.YILI]");
                sb.AppendLine("    FROM TBL_LST_MALZEMELER m WITH (NOLOCK)");
                sb.AppendLine(") SORGU");
                sb.AppendLine("WHERE ISNULL([MALZEME GİRİŞ ADET],0) > 0");
                if (!string.IsNullOrEmpty(inList))
                    sb.AppendLine("  AND [MALZEME ANA GRUBU] IS NOT NULL");
            }
            else
            {
                // "stoktan" varyant — yine grup filtresi ve MALZEME GRUBU adını ekledik.
                sb.AppendLine("SELECT MALZEME_ID, [MALZEME OUDBTNO], CASE WHEN [MALZEME TURU]=1 THEN 'NLAG' WHEN [MALZEME TURU]=2 THEN 'UNBW' ELSE '<<Seçiniz>>' END [MALZEME TURU],");
                sb.AppendLine("       [MALZEME MATERYEL], [MALZEME PARÇANO], [MALZEME ADI],");
                sb.AppendLine("       CASE WHEN [MALZEME GİRİŞ P.BİRİMİ] = 1 THEN 'TL' WHEN [MALZEME GİRİŞ P.BİRİMİ] = 2 THEN '€' WHEN [MALZEME GİRİŞ P.BİRİMİ] = 3 THEN '$'");
                sb.AppendLine("            WHEN [MALZEME GİRİŞ P.BİRİMİ] = 4 THEN 'JPY' WHEN [MALZEME GİRİŞ P.BİRİMİ] = 5 THEN 'CHF' WHEN [MALZEME GİRİŞ P.BİRİMİ] = 6 THEN 'GBP' END [MALZEME G.PARA BİRİMİ],");
                sb.AppendLine("       [MALZEME GİRİŞ B.FİYAT], [MALZEME GİRİŞ ADET], [MALZEME GİRİŞ ADET]*[MALZEME B.FİYAT] [MALZEME GİRİŞ T.FİYAT],");
                sb.AppendLine("       [MALZEME ÇIKIŞ ADET], [MALZEME ÇIKIŞ ADET]*[MALZEME B.FİYAT] [MALZEME ÇIKIŞ T.FİYAT],");
                sb.AppendLine("       [MALZEME STOK MIKTARI] AS [MALZEME STOK MİKTAR], [MALZEME MİN ADET], [MALZEME MAX ADET],");
                sb.AppendLine("       CASE WHEN [MALZEME STOK MIKTARI] > [MALZEME MAX ADET] THEN [MALZEME STOK MIKTARI] - [MALZEME MAX ADET] ELSE 0 END AS [STOK FAZLASI MİKTAR],");
                sb.AppendLine("       [MALZEME B.FİYAT] AS [MALZEME BİRİM FİYAT], [MALZEME STOK MIKTARI]*[MALZEME B.FİYAT] AS [MALZEME TOPLAM FİYAT],");
                sb.AppendLine("       CASE WHEN [MALZEME STOK MIKTARI] > [MALZEME MAX ADET] THEN ([MALZEME STOK MIKTARI]-[MALZEME MAX ADET])*[MALZEME B.FİYAT] ELSE 0 END AS [STOK FAZLASI TOPLAM FİYAT],");
                sb.AppendLine("       [MALZEME G.TARİH] AS [MALZEME GİRİŞ TARİH], [MALZEME Ç.TARİH] AS [MALZEME ÇIKIŞ TARİH], [MALZEME RAFNO], [MALZEME ANA GRUBU], [MALZEME GRUBU],");
                sb.AppendLine("       [MALZEME STOK SAY], [MALZEME SATINALMA KATEGORISI], [MALZEME NOTU], [MALZEME G.YILI], [MALZEME Ç.YILI]");
                sb.AppendLine("FROM (");
                sb.AppendLine("    SELECT m.MALZEME_ID, m.MALZEME_OUDBTNO AS [MALZEME OUDBTNO], m.MALZEME_TURU AS [MALZEME TURU], m.MALZEME_STOKMIKTARI AS [MALZEME STOK MIKTARI],");
                sb.AppendLine("           m.MALZEME_MATERYAL AS [MALZEME MATERYEL], m.MALZEME_PARCANO AS [MALZEME PARÇANO], m.MALZEME_ADI AS [MALZEME ADI],");
                sb.AppendLine("           (SELECT TOP 1 e.MALZEMEGIRIS_PARABIRIMI FROM TBL_LST_MALZEMEGIRIS e WHERE m.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY e.MALZEMEGIRIS_TARIH DESC) AS [MALZEME GİRİŞ P.BİRİMİ],");
                sb.AppendLine("           (SELECT TOP 1 e.MALZEMEGIRIS_BIRIMFIYAT FROM TBL_LST_MALZEMEGIRIS e WHERE m.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY e.MALZEMEGIRIS_TARIH DESC) AS [MALZEME GİRİŞ B.FİYAT],");
                sb.AppendLine("           m.MALZEME_MINADET AS [MALZEME MİN ADET],");

                // GİRİŞ / ÇIKIŞ ADET – stok varyantında yine tarih aralığına göre hesap olsun istiyorsan bırak;
                // değiştirmeyeceksen mevcut mantığına paralel kalsın:
                sb.AppendLine($"           (SELECT ISNULL(SUM(mg.MALZEMEGIRIS_ADET),0) FROM TBL_LST_MALZEMEGIRIS mg WHERE m.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID AND mg.MALZEMEGIRIS_TARIH >= '{baslangic}' AND mg.MALZEMEGIRIS_TARIH < DATEADD(day,1,'{bitis}')) AS [MALZEME GİRİŞ ADET],");
                sb.AppendLine($"           (SELECT ISNULL(SUM(mc.MALZEMECIKIS_ADET),0) FROM TBL_LST_MALZEMECIKIS mc WHERE m.MALZEME_ID = mc.MALZEMECIKIS_MALZEMELERID AND mc.MALZEMECIKIS_TARIHI >= '{baslangic}' AND mc.MALZEMECIKIS_TARIHI < DATEADD(day,1,'{bitis}')) AS [MALZEME ÇIKIŞ ADET],");

                sb.AppendLine("           m.MALZEME_MAXADET AS [MALZEME MAX ADET],");
                sb.AppendLine("           (SELECT TOP 1 e.MALZEMEGIRIS_SORGUBIRIMFIYAT FROM TBL_LST_MALZEMEGIRIS e WHERE m.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY e.MALZEMEGIRIS_TARIH DESC) AS [MALZEME B.FİYAT],");
                sb.AppendLine("           (SELECT TOP 1 CONVERT(varchar(10), e.MALZEMEGIRIS_TARIH, 121) FROM TBL_LST_MALZEMEGIRIS e WHERE m.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY e.MALZEMEGIRIS_TARIH DESC) AS [MALZEME G.TARİH],");
                sb.AppendLine("           (SELECT TOP 1 CONVERT(varchar(10), e.MALZEMECIKIS_TARIHI, 121) FROM TBL_LST_MALZEMECIKIS e WHERE m.MALZEME_ID = e.MALZEMECIKIS_MALZEMELERID ORDER BY e.MALZEMECIKIS_TARIHI DESC) AS [MALZEME Ç.TARİH],");
                sb.AppendLine("           m.MALZEME_RAFNO AS [MALZEME RAFNO],");

                sb.Append("           (SELECT d.MALZEMEANAGRUP_ADI FROM TBL_LST_MALZEMEANAGRUPLAR d WHERE m.MALZEME_ANAGRUBU = d.MALZEMEANAGRUP_ID");
                if (!string.IsNullOrEmpty(inList))
                    sb.AppendLine($" AND d.MALZEMEANAGRUP_ADI IN ({inList})");
                sb.AppendLine("           ) AS [MALZEME ANA GRUBU],");

                // >>> MALZEME GRUBU ADI
                sb.AppendLine("           (SELECT TOP 1 g.MALZEMEGRUP_ADI FROM TBL_LST_MALZEMEGRUPLAR g WHERE g.MALZEMEGRUP_ID = m.MALZEME_GRUBU) AS [MALZEME GRUBU],");

                sb.AppendLine("           m.MALZEME_STOKSAY AS [MALZEME STOK SAY],");
                sb.AppendLine("           (SELECT k.MALZEMEKATEGORI_ADI + ' (' + k.MALZEMEKATEGORI_KODU + ')' FROM TBL_LST_MALZEMEKATEGORILER k WHERE m.MALZEME_SATINALMAKATEGORI = k.MALZEMEKATEGORI_ID) AS [MALZEME SATINALMA KATEGORISI],");
                sb.AppendLine("           m.MALZEME_NOTU AS [MALZEME NOTU],");
                sb.AppendLine("           (SELECT TOP 1 YEAR(e.MALZEMEGIRIS_TARIH) FROM TBL_LST_MALZEMEGIRIS e WHERE m.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY e.MALZEMEGIRIS_TARIH DESC) AS [MALZEME G.YILI],");
                sb.AppendLine("           (SELECT TOP 1 YEAR(e.MALZEMECIKIS_TARIHI) FROM TBL_LST_MALZEMECIKIS e WHERE m.MALZEME_ID = e.MALZEMECIKIS_MALZEMELERID ORDER BY e.MALZEMECIKIS_TARIHI DESC) AS [MALZEME Ç.YILI]");
                sb.AppendLine("    FROM TBL_LST_MALZEMELER m WITH (NOLOCK)");
                sb.AppendLine(") SORGU");
                sb.AppendLine("WHERE ISNULL([MALZEME GİRİŞ ADET],0) > 0");
                if (!string.IsNullOrEmpty(inList))
                    sb.AppendLine("  AND [MALZEME ANA GRUBU] IS NOT NULL");
            }

            // 4) Çalıştır
            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlDepoDurum.DataSource = dtMalzemeler;

            // 5) Grid format/özetler (seninkiyle aynı)
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

        private void barButtonItemListele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            whereString = string.Empty;
            whereString2 = string.Empty;

            foreach (CheckedListBoxItem items in checkedComboBoxEditGrup.Properties.Items)
            {
                if (items.CheckState == CheckState.Checked)
                {
                    whereString += "'" + String.Format(items.Description.ToString()) + "'" + ",";
                }
            }

            if (whereString != string.Empty)
            {
                whereString = whereString.Substring(0, whereString.Length - 1);
            }

            if (whereString2 != string.Empty)
            {
                whereString2 = whereString2.Substring(0, whereString2.Length - 1);
            }

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
                        using (SqlDataAdapter sAdapter = new SqlDataAdapter(sCommand))
                        {
                            SqlDataReader dr = sCommand.ExecuteReader(CommandBehavior.CloseConnection);

                            while (dr.Read())
                            {
                                MyModel departmanlar = new MyModel();
                                departmanlar.MALZEMEGRUP_ID = Convert.ToInt32(dr[0]);
                                departmanlar.MALZEMEGRUP_ADI = dr[1].ToString();
                                //değiştirdiğim yer aşağıda
                                list.Add(departmanlar);
                                //değiştirdiğim yer yukarıda
                            }
                            conn.Close();
                        }
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
                        using (SqlDataAdapter sAdapter = new SqlDataAdapter(sCommand))
                        {
                            SqlDataReader dr = sCommand.ExecuteReader(CommandBehavior.CloseConnection);

                            while (dr.Read())
                            {
                                MyModel2 departmanlar2 = new MyModel2();
                                departmanlar2.MALZEME_DEPARTMANID = Convert.ToInt32(dr[0]);
                                departmanlar2.MALZEME_DEPARTMANADI = dr[1].ToString();
                                //değiştirdiğim yer aşağıda
                                list.Add(departmanlar2);
                                //değiştirdiğim yer yukarıda
                            }
                            conn.Close();
                        }
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
