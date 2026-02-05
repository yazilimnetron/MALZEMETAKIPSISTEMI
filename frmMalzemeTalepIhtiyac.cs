using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeTalepIhtiyac : Form
    {
        private bool _isInit;

        private bool HasMaksOrtYetki()
        {
            string sql = @"
            SELECT COUNT(*)
            FROM TBL_LST_MALZEMEYETKILER y WITH (NOLOCK)
            JOIN TBL_LST_MALZEMEMENULER m ON y.MALZEMEISTEMMENULER_ID = m.MALZEMEISTEMMENU_ID
            WHERE ISNULL(m.MALZEMEISTEMMENU_DURUM,0)=1
            AND m.MALZEMEISTEMMENU_ADI = N'Malzeme Talep Maks-Ort'
            AND y.MALZEMEISTEMKULLANICI_KID = " + clGenelTanim.DBToInt32(clGenelTanim.KullaniciKodu) + @"
            AND ISNULL(y.MALZEMEISTEMMENU_YETKI,0)=1";

            var dt = clSqlTanim.RunStoredProc(sql);
            return dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        private int LoadGlobalDurum()
        {
            string sql = @"
            SELECT TOP(1) ISNULL(y.MALZEMEISTEMMENU_SECIM,0) AS Secim
            FROM TBL_LST_MALZEMEMENULER m WITH (NOLOCK)
            JOIN TBL_LST_MALZEMEYETKILER y ON y.MALZEMEISTEMMENULER_ID = m.MALZEMEISTEMMENU_ID
            WHERE ISNULL(m.MALZEMEISTEMMENU_DURUM,0)=1
            AND m.MALZEMEISTEMMENU_ADI = N'Malzeme Talep Maks-Ort'";

            var dt = clSqlTanim.RunStoredProc(sql);
            if (dt != null && dt.Rows.Count > 0 && int.TryParse(Convert.ToString(dt.Rows[0]["Secim"]), out var idx))
                return Math.Max(0, Math.Min(1, idx));
            return 0;
        }

        private void SaveGlobalDurum(int index)
        {
            int safe = Math.Max(0, Math.Min(1, index));

            string sql = @"
            UPDATE y
            SET y.MALZEMEISTEMMENU_SECIM = " + safe + @"
            FROM dbo.TBL_LST_MALZEMEYETKILER y
            JOIN dbo.TBL_LST_MALZEMEMENULER m ON y.MALZEMEISTEMMENULER_ID = m.MALZEMEISTEMMENU_ID
            WHERE ISNULL(m.MALZEMEISTEMMENU_DURUM,0)=1
            AND m.MALZEMEISTEMMENU_ADI = N'Malzeme Talep Maks-Ort'";

            clSqlTanim.ExecuteNonQuery(sql);
        }

        public frmMalzemeTalepIhtiyac()
        {
            InitializeComponent();
            radioGroupDurum.EditValueChanged += radioGroupDurum_EditValueChanged;
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

        private void frmMalzemeOtomatikTalep_Load(object sender, EventArgs e)
        {
            _isInit = true;

            // 1) Global seçim (tüm kullanıcılar bunu görür)
            int idx = LoadGlobalDurum();
            idx = Math.Max(0, Math.Min(radioGroupDurum.Properties.Items.Count - 1, idx));
            radioGroupDurum.SelectedIndex = idx;

            // 2) Yetkisi olmayan sadece görür (değiştiremez)
            bool allowed = HasMaksOrtYetki();
            //radioGroupDurum.Properties.ReadOnly = !allowed;
            layoutControlItemDurum.Enabled = allowed;

            _isInit = false;

            // 3) Seçime göre listele
            if (radioGroupDurum.SelectedIndex == 0) InitForm(); else InitForm1();
        }

        public void InitForm()
        {
            string sql = @"
SELECT *
FROM (
    SELECT 
        s.MALZEME_ID,
        CASE 
            WHEN s.MALZEME_TURU = 1 THEN 'NLAG'
            WHEN s.MALZEME_TURU = 2 THEN 'UNBW'
            ELSE '<<Seçiniz>>'
        END AS [MALZEME TURU],
        s.MALZEME_MATERYAL AS [MALZEME MATERYEL],
        (SELECT TOP 1 CONVERT(VARCHAR(10), MALZEMETALEP_TARIHI, 121)
         FROM TBL_LST_MALZEMETALEP mt
         WHERE s.MALZEME_ID = mt.MALZEMETALEP_MALZEMELERID
         ORDER BY MALZEMETALEP_TARIHI DESC) AS [MALZEME T.TARIH],
        s.MALZEME_ADI AS [MALZEME ADI],
        ISNULL(s.MALZEME_MINADET,0) AS [MALZEME MIN ADET],
        ISNULL(s.MALZEME_MAXADET,0) AS [MALZEME MAX ADET],
        (ISNULL(g.GirislerToplam,0) - ISNULL(c.CikislarToplam,0)) AS [MALZEME STOK ADET],
        (ISNULL(s.MALZEME_MAXADET,0) - (ISNULL(g.GirislerToplam,0) - ISNULL(c.CikislarToplam,0))) AS [SİPARİŞ VERİLECEK],
        (SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0)
         FROM TBL_LST_MALZEMEGIRIS mg
         WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID
         ORDER BY MALZEMEGIRIS_TARIH DESC) AS [MALZEME BİRİM FİYAT],
        (ISNULL(s.MALZEME_MAXADET,0) - ISNULL((g.GirislerToplam - c.CikislarToplam),0)) *
        (SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0)
         FROM TBL_LST_MALZEMEGIRIS mg
         WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID
         ORDER BY MALZEMEGIRIS_TARIH DESC) AS [MALZEME TOPLAM FİYAT],
        (SELECT TOP 1 CONVERT(VARCHAR(10), MALZEMEGIRIS_TARIH, 121)
         FROM TBL_LST_MALZEMEGIRIS e
         WHERE s.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID
         ORDER BY MALZEMEGIRIS_TARIH DESC) AS [MALZEME G.TARİH],
        (SELECT TOP 1 CONVERT(VARCHAR(10), MALZEMECIKIS_TARIHI, 121)
         FROM TBL_LST_MALZEMECIKIS e
         WHERE s.MALZEME_ID = e.MALZEMECIKIS_MALZEMELERID
         ORDER BY MALZEMECIKIS_TARIHI DESC) AS [MALZEME Ç.TARİH],
        s.MALZEME_PARCANO AS [MALZEME PARÇANO],
        s.MALZEME_RAFNO AS [MALZEME RAFNO],
        s.MALZEME_GRUBU AS [MALZEME GRUBU],
        (SELECT TOP 1 
             CASE 
                WHEN mg.MALZEMEGIRIS_PARABIRIMI=1 THEN 'TL'
                WHEN mg.MALZEMEGIRIS_PARABIRIMI=2 THEN '€'
                WHEN mg.MALZEMEGIRIS_PARABIRIMI=3 THEN '$'
                WHEN mg.MALZEMEGIRIS_PARABIRIMI=4 THEN 'JPY'
                WHEN mg.MALZEMEGIRIS_PARABIRIMI=5 THEN 'CHF'
                WHEN mg.MALZEMEGIRIS_PARABIRIMI=6 THEN 'GBP'
             END
         FROM TBL_LST_MALZEMEGIRIS mg
         WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID
         ORDER BY MALZEMEGIRIS_TARIH DESC) AS [MALZEME G.PARA BİRİMİ],
        e.MALZEMEKATEGORI_ADI + ' (' + e.MALZEMEKATEGORI_KODU + ')' AS [MALZEME SATINALMA KATEGORISI]
    FROM TBL_LST_MALZEMELER s
    LEFT JOIN TBL_LST_MALZEMEKATEGORILER e ON s.MALZEME_SATINALMAKATEGORI = e.MALZEMEKATEGORI_ID
    LEFT OUTER JOIN (
        SELECT MALZEMEGIRIS_MALZEMELERID, SUM(MALZEMEGIRIS_ADET) AS GirislerToplam
        FROM TBL_LST_MALZEMEGIRIS
        GROUP BY MALZEMEGIRIS_MALZEMELERID
    ) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID
    LEFT OUTER JOIN (
        SELECT MALZEMECIKIS_MALZEMELERID, SUM(MALZEMECIKIS_ADET) AS CikislarToplam
        FROM TBL_LST_MALZEMECIKIS
        GROUP BY MALZEMECIKIS_MALZEMELERID
    ) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID
    WHERE NOT EXISTS (
        SELECT 1
        FROM TBL_LST_MALZEMETALEP t
        WHERE t.MALZEMETALEP_MALZEMELERID = s.MALZEME_ID
          AND t.MALZEMETALEP_DURUM = 0
    )
      AND (ISNULL(g.GirislerToplam,0) - ISNULL(c.CikislarToplam,0)) <= s.MALZEME_MINADET
) sorgu
WHERE [SİPARİŞ VERİLECEK] > 0
  AND [MALZEME G.TARİH] > '2016-01-01'
  AND [MALZEME Ç.TARİH] > '2016-01-01'
ORDER BY 4;";

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sql);
            gridControlMalzemeTalepler.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepler.Columns["MALZEME MATERYEL"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepler.Columns["MALZEME MATERYEL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepler.Columns["MALZEME MATERYEL"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepler.Columns["MALZEME G.TARİH"].Visible = false;
            this.gridViewMalzemeTalepler.Columns["MALZEME G.TARİH"].OptionsColumn.ShowInCustomizationForm = false;

            this.gridViewMalzemeTalepler.Columns["MALZEME Ç.TARİH"].Visible = false;
            this.gridViewMalzemeTalepler.Columns["MALZEME Ç.TARİH"].OptionsColumn.ShowInCustomizationForm = false;

            this.gridViewMalzemeTalepler.Columns["MALZEME_ID"].Visible = false;
            this.gridViewMalzemeTalepler.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewMalzemeTalepler, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepler.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepler.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepler.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.DisplayFormat = "TOPLAM FİYAT: {0:c2}";

            this.gridViewMalzemeTalepler.Columns["MALZEME STOK ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewMalzemeTalepler.Columns["MALZEME STOK ADET"].SummaryItem.DisplayFormat = "{0} Adet";

            this.gridViewMalzemeTalepler.BestFitColumns();
        }

        public void InitForm1()
        {
            string sql = @"
SELECT *
FROM (
    SELECT 
        s.MALZEME_ID,
        CASE 
            WHEN s.MALZEME_TURU = 1 THEN 'NLAG'
            WHEN s.MALZEME_TURU = 2 THEN 'UNBW'
            ELSE '<<Seçiniz>>'
        END AS [MALZEME TURU],
        s.MALZEME_MATERYAL AS [MALZEME MATERYEL],
        (SELECT TOP 1 CONVERT(VARCHAR(10), MALZEMETALEP_TARIHI, 121)
         FROM TBL_LST_MALZEMETALEP mt
         WHERE s.MALZEME_ID = mt.MALZEMETALEP_MALZEMELERID
         ORDER BY MALZEMETALEP_TARIHI DESC) AS [MALZEME T.TARIH],
        s.MALZEME_ADI AS [MALZEME ADI],
        ISNULL(s.MALZEME_MINADET,0) AS [MALZEME MIN ADET],
        ISNULL(s.MALZEME_MAXADET,0) AS [MALZEME MAX ADET],
        (ISNULL(g.GirislerToplam,0) - ISNULL(c.CikislarToplam,0)) AS [MALZEME STOK ADET],
        (((ISNULL(s.MALZEME_MAXADET,0) + ISNULL(s.MALZEME_MINADET,0)) / 2)
            - (ISNULL(g.GirislerToplam,0) - ISNULL(c.CikislarToplam,0))) AS [SİPARİŞ VERİLECEK],
        (SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0)
         FROM TBL_LST_MALZEMEGIRIS mg
         WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID
         ORDER BY MALZEMEGIRIS_TARIH DESC) AS [MALZEME BİRİM FİYAT],
        (((ISNULL(s.MALZEME_MAXADET,0) + ISNULL(s.MALZEME_MINADET,0)) / 2)
            - ISNULL((g.GirislerToplam - c.CikislarToplam),0)) *
        (SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0)
         FROM TBL_LST_MALZEMEGIRIS mg
         WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID
         ORDER BY MALZEMEGIRIS_TARIH DESC) AS [MALZEME TOPLAM FİYAT],
        (SELECT TOP 1 CONVERT(VARCHAR(10), MALZEMEGIRIS_TARIH, 121)
         FROM TBL_LST_MALZEMEGIRIS e
         WHERE s.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID
         ORDER BY MALZEMEGIRIS_TARIH DESC) AS [MALZEME G.TARİH],
        (SELECT TOP 1 CONVERT(VARCHAR(10), MALZEMECIKIS_TARIHI, 121)
         FROM TBL_LST_MALZEMECIKIS e
         WHERE s.MALZEME_ID = e.MALZEMECIKIS_MALZEMELERID
         ORDER BY MALZEMECIKIS_TARIHI DESC) AS [MALZEME Ç.TARİH],
        s.MALZEME_PARCANO AS [MALZEME PARÇANO],
        s.MALZEME_RAFNO AS [MALZEME RAFNO],
        s.MALZEME_GRUBU AS [MALZEME GRUBU],
        (SELECT TOP 1 
             CASE 
                WHEN mg.MALZEMEGIRIS_PARABIRIMI = 1 THEN 'TL'
                WHEN mg.MALZEMEGIRIS_PARABIRIMI = 2 THEN '€'
                WHEN mg.MALZEMEGIRIS_PARABIRIMI = 3 THEN '$'
                WHEN mg.MALZEMEGIRIS_PARABIRIMI = 4 THEN 'JPY'
                WHEN mg.MALZEMEGIRIS_PARABIRIMI = 5 THEN 'CHF'
                WHEN mg.MALZEMEGIRIS_PARABIRIMI = 6 THEN 'GBP'
             END
         FROM TBL_LST_MALZEMEGIRIS mg
         WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID
         ORDER BY MALZEMEGIRIS_TARIH DESC) AS [MALZEME G.PARA BİRİMİ],
        e.MALZEMEKATEGORI_ADI + ' (' + e.MALZEMEKATEGORI_KODU + ')' AS [MALZEME SATINALMA KATEGORISI]
    FROM TBL_LST_MALZEMELER s
    LEFT JOIN TBL_LST_MALZEMEKATEGORILER e ON s.MALZEME_SATINALMAKATEGORI = e.MALZEMEKATEGORI_ID
    LEFT OUTER JOIN (
        SELECT MALZEMEGIRIS_MALZEMELERID, SUM(MALZEMEGIRIS_ADET) AS GirislerToplam
        FROM TBL_LST_MALZEMEGIRIS
        GROUP BY MALZEMEGIRIS_MALZEMELERID
    ) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID
    LEFT OUTER JOIN (
        SELECT MALZEMECIKIS_MALZEMELERID, SUM(MALZEMECIKIS_ADET) AS CikislarToplam
        FROM TBL_LST_MALZEMECIKIS
        GROUP BY MALZEMECIKIS_MALZEMELERID
    ) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID
    WHERE NOT EXISTS (
        SELECT 1
        FROM TBL_LST_MALZEMETALEP t
        WHERE t.MALZEMETALEP_MALZEMELERID = s.MALZEME_ID
          AND t.MALZEMETALEP_DURUM = 0
    )
      AND (ISNULL(g.GirislerToplam,0) - ISNULL(c.CikislarToplam,0)) <= s.MALZEME_MINADET
) sorgu
WHERE [SİPARİŞ VERİLECEK] > 0
  AND [MALZEME G.TARİH] > '2016-01-01'
  AND [MALZEME Ç.TARİH] > '2016-01-01'
ORDER BY 4;";

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sql);
            gridControlMalzemeTalepler.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepler.Columns["MALZEME MATERYEL"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepler.Columns["MALZEME MATERYEL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepler.Columns["MALZEME MATERYEL"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepler.Columns["MALZEME G.TARİH"].Visible = false;
            this.gridViewMalzemeTalepler.Columns["MALZEME G.TARİH"].OptionsColumn.ShowInCustomizationForm = false;

            this.gridViewMalzemeTalepler.Columns["MALZEME Ç.TARİH"].Visible = false;
            this.gridViewMalzemeTalepler.Columns["MALZEME Ç.TARİH"].OptionsColumn.ShowInCustomizationForm = false;

            this.gridViewMalzemeTalepler.Columns["MALZEME_ID"].Visible = false;
            this.gridViewMalzemeTalepler.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewMalzemeTalepler, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepler.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepler.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepler.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepler.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.DisplayFormat = "TOPLAM FİYAT: {0:c2}";

            this.gridViewMalzemeTalepler.Columns["MALZEME STOK ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewMalzemeTalepler.Columns["MALZEME STOK ADET"].SummaryItem.DisplayFormat = "{0} Adet";

            this.gridViewMalzemeTalepler.BestFitColumns();
        }

        private void barButtonItemMalzemeTalepKapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void barButtonItemYazdir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");

            clGenelTanim.GridYazdir(gridControlMalzemeTalepler, "Malzeme Listesi");
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");

            clGenelTanim.GridExport(gridControlMalzemeTalepler, "Malzeme Listesi", this);
        }

        private void barButtonItemMalzemeTalepListele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (radioGroupDurum.SelectedIndex == 0)
            {
                InitForm();
            }
            if (radioGroupDurum.SelectedIndex == 1)
            {
                InitForm1();
            }
        }

        private void malzemeTalepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int focusedId = (int)gridViewMalzemeTalepler.GetFocusedRowCellValue("IsSelected");
            //if (focusedId>0)
            //{
            //var strRow = gridViewMalzemeTalepler.GetRowCellValue(gridViewMalzemeTalepler.FocusedRowHandle, gridViewMalzemeTalepler.Columns[0]).ToString();
            frmMalzemeTalepEkle u = new frmMalzemeTalepEkle();
            //u.nMALZEMELER_ID = clGenelTanim.DBToInt32(strRow.ToString());
            u.ShowDialog();
            //    }
            //    else { XtraMessageBox.Show("Talep yapmak istediğiniz kaydı seçmelisiniz !", "Bilgi ...", MessageBoxButtons.OK, MessageBoxIcon.Error); };
        }

        private void radioGroupDurum_EditValueChanged(object sender, EventArgs e)
        {
            if (_isInit) return;

            if (!HasMaksOrtYetki())
            {
                // Yetkisi yoksa ekrandaki değeri global olana geri döndür
                _isInit = true;
                radioGroupDurum.SelectedIndex = LoadGlobalDurum();
                _isInit = false;
                return;
            }

            // YETKİLİ TEK KULLANICI: Globali günceller → herkes görür
            SaveGlobalDurum(radioGroupDurum.SelectedIndex);

            // Anında uygula
            if (radioGroupDurum.SelectedIndex == 0) InitForm(); else InitForm1();
        }
    }
}
