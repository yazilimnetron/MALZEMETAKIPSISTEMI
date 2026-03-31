using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using MALZEMETAKIPSISTEMI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeTanimlama : FrmBase
    {
        private DataSet dsMalzeme = null;
        private bool yeniMalzeme = false;
        private int currentMalzemeID = -1;

        decimal malzemeToplamFiyat = 0m;

        decimal malzemeUsdFiyat = 0m, malzemeEuroFiyat = 0m, malzemeJpyFiyat = 0m, malzemeChfFiyat = 0m, malzemeGbpFiyat = 0m, malzemeRonFiyat = 0m;

        decimal usdEuroOran = 0m, jpyEuroOran = 0m, chfEuroOran = 0m, gbpEuroOran = 0m, ronEuroOran = 0m;

        bool isImageDirty = false;

        string imgLoc = "";

        private bool HasMaksMinYetki()
        {
            string sql = @"
            SELECT COUNT(*)
            FROM TBL_LST_MALZEMEYETKILER Y (NOLOCK)
            JOIN TBL_LST_MALZEMEMENULER M 
            ON Y.MALZEMEISTEMMENULER_ID = M.MALZEMEISTEMMENU_ID
            WHERE ISNULL(M.MALZEMEISTEMMENU_DURUM,0)=1
            AND M.MALZEMEISTEMMENU_ADI = N'Malzeme Maks-Min'
            AND Y.MALZEMEISTEMKULLANICI_KID = " + clGenelTanim.DBToInt32(clGenelTanim.KullaniciKodu);

            DataTable dt = clSqlTanim.RunStoredProc(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count > 0;
            }
            return false;
        }

        private void UygulaMaksMinYetkisi()
        {
            bool allowed = HasMaksMinYetki();
            textEditMinAdet.Enabled = allowed;
            textEditMaxAdet.Enabled = allowed;
        }

        public frmMalzemeTanimlama()
        {
            InitializeComponent();
        }

        public void InitFormKur()
        {
            StringBuilder sb = new StringBuilder(1024);
            sb.Append("SELECT TOP 1 ISNULL(MALZEMEDOVIZKUR_USDSATIS,0), ISNULL(MALZEMEDOVIZKUR_EUROSATIS,0), ISNULL(MALZEMEDOVIZKUR_JPYSATIS,0), ISNULL(MALZEMEDOVIZKUR_GBPSATIS,0), ISNULL(MALZEMEDOVIZKUR_RONSATIS,0), ISNULL(MALZEMEDOVIZKUR_CHFSATIS,0) FROM TBL_LST_MALZEMEKURBILGILERI (NOLOCK) ORDER BY MALZEMEDOVIZKUR_ID DESC");
            DataTable table = clSqlTanim.RunStoredProc(sb.ToString());
            foreach (DataRow row in table.Rows)
            {
                malzemeUsdFiyat = Convert.ToDecimal(table.Rows[0][0]);
                malzemeEuroFiyat = Convert.ToDecimal(table.Rows[0][1]);
                malzemeJpyFiyat = Convert.ToDecimal(table.Rows[0][2]);
                malzemeGbpFiyat = Convert.ToDecimal(table.Rows[0][3]);
                malzemeRonFiyat = Convert.ToDecimal(table.Rows[0][4]);
                malzemeChfFiyat = Convert.ToDecimal(table.Rows[0][5]);


                usdEuroOran = ((int)((malzemeUsdFiyat / malzemeEuroFiyat) * 100)) / 100M;
                jpyEuroOran = ((int)((malzemeJpyFiyat / malzemeEuroFiyat) * 100)) / 10000M;
                chfEuroOran = ((int)((malzemeChfFiyat / malzemeEuroFiyat) * 100)) / 100M;
                gbpEuroOran = ((int)((malzemeGbpFiyat / malzemeEuroFiyat) * 100)) / 100M;
                ronEuroOran = ((int)((malzemeRonFiyat / malzemeEuroFiyat) * 100)) / 100M;
            }
        }
        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(1024);
            sb.Append(" SELECT s.MALZEME_ID, s.MALZEME_OUDBTNO AS 'MALZEME OUDBTNO',  case when s.MALZEME_TURU=1 then 'NLAG'  when s.MALZEME_TURU=2 then 'UNBW' else '<<Seçiniz>> '");
            sb.Append("end 'MALZEME TURU', convert(nvarchar, s.MALZEME_MATERYAL) AS 'MALZEME MATERYEL NO', s.MALZEME_PARCANO AS 'MALZEME PARÇA NO',");
            sb.Append("s.MALZEME_ADI AS 'MALZEME ADI', ");
            sb.Append("(SELECT TOP 1  case when mg.MALZEMEGIRIS_PARABIRIMI=1 then 'TL'  when mg.MALZEMEGIRIS_PARABIRIMI=2 then '€' when mg.MALZEMEGIRIS_PARABIRIMI=3 then '$' ");
            sb.Append("when mg.MALZEMEGIRIS_PARABIRIMI = 4 then 'JPY' when mg.MALZEMEGIRIS_PARABIRIMI = 5 then 'CHF' when mg.MALZEMEGIRIS_PARABIRIMI = 6 then 'GBP'end ");
            sb.Append(" FROM TBL_LST_MALZEMEGIRIS mg WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC) ");
            sb.Append("[MALZEME GIRIS P.BIRIMI], ");
            sb.Append("(SELECT TOP 1 e.MALZEMEGIRIS_BIRIMFIYAT FROM TBL_LST_MALZEMEGIRIS e WHERE s.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC ) [MALZEME GIRIS B.FIYAT], ");
            sb.Append("(isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) AS 'MALZEME STOK ADET', ISNULL(s.MALZEME_MINADET,0) AS 'MALZEME MIN ADET', ISNULL(s.MALZEME_MAXADET, 0) AS 'MALZEME MAX ADET', case when s.MALZEME_OLCUBIRIMI = 1 then 'ADET' when s.MALZEME_OLCUBIRIMI = 2 then 'METRE' end 'MALZEME ÖLÇÜ BIRIMI', s.MALZEME_RAFNO AS 'MALZEME RAF NO',");
            sb.Append(" d.MALZEMEANAGRUP_ID, d.MALZEMEANAGRUP_ADI AS 'MALZEME ANA GRUBU', f.MALZEMEGRUP_ADI AS 'MALZEME GRUBU', ");
            sb.Append("(SELECT TOP 1 convert(varchar(10), MALZEMEGIRIS_TARIH, 121) FROM TBL_LST_MALZEMEGIRIS e WHERE s.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC) AS 'MALZEME TARIHI', s.MALZEME_STOKSAY AS 'STOK SAYILSIN', e.MALZEMEKATEGORI_ADI + ' ' + '(' + e.MALZEMEKATEGORI_KODU + ')' AS 'MALZEME SATINALMA KATEGORISI', s.MALZEME_NOTU AS 'MALZEME NOTU' ");
            sb.Append("FROM TBL_LST_MALZEMELER s ");
            sb.Append("INNER JOIN TBL_LST_MALZEMEANAGRUPLAR d on s.MALZEME_ANAGRUBU=d.MALZEMEANAGRUP_ID ");
            sb.Append("LEFT JOIN TBL_LST_MALZEMEKATEGORILER e on s.MALZEME_SATINALMAKATEGORI=e.MALZEMEKATEGORI_ID ");
            sb.Append("LEFT JOIN TBL_LST_MALZEMEGRUPLAR f on s.MALZEME_GRUBU=f.MALZEMEGRUP_ID ");
            sb.Append("LEFT OUTER JOIN (Select  MALZEMEGIRIS_MALZEMELERID, Sum(MALZEMEGIRIS_ADET) GirislerToplam  FROM TBL_LST_MALZEMEGIRIS GROUP BY  MALZEMEGIRIS_MALZEMELERID ) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("LEFT OUTER JOIN (Select  MALZEMECIKIS_MALZEMELERID, Sum(MALZEMECIKIS_ADET) CikislarToplam  FROM TBL_LST_MALZEMECIKIS GROUP BY  MALZEMECIKIS_MALZEMELERID ) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("ORDER BY 1 DESC");

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlMalzemeListesi.DataSource = dtMalzemeler;

            this.gridViewMalzemeListesi.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeListesi.Columns["MALZEME MATERYEL NO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeListesi.Columns["MALZEME MATERYEL NO"].SummaryItem.DisplayFormat = "{0} Kayit";

            this.gridViewMalzemeListesi.Columns["MALZEME_ID"].Visible = false;
            this.gridViewMalzemeListesi.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.gridViewMalzemeListesi.Columns["MALZEME OUDBTNO"].Visible = false;

            this.gridViewMalzemeListesi.Columns["MALZEMEANAGRUP_ID"].Visible = false;

            this.SetGridFont(gridViewMalzemeListesi, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeListesi.Columns["MALZEME STOK ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewMalzemeListesi.Columns["MALZEME STOK ADET"].SummaryItem.DisplayFormat = "{0} Adet";
            this.gridViewMalzemeListesi.BestFitColumns();

            comboBoxEditMalzemeAnaGrubu.Doldur("SELECT MALZEMEANAGRUP_ID, MALZEMEANAGRUP_ADI AS 'MALZEME ANAGRUPADI' FROM TBL_LST_MALZEMEANAGRUPLAR (NOLOCK)  WHERE ISNULL (MALZEMEANAGRUP_DURUM,0)=1 ORDER BY MALZEMEANAGRUP_ADI", true);
            comboBoxEditMalzemeAnaGrubu.SelectedIndex = 0;

            comboBoxEditMalzemeGrubu.Doldur("SELECT MALZEMEGRUP_ID, MALZEMEGRUP_ADI AS 'MALZEME GRUPADI' FROM TBL_LST_MALZEMEGRUPLAR (NOLOCK)  WHERE ISNULL (MALZEMEGRUP_DURUM,0)=1 ORDER BY MALZEMEGRUP_ADI", true);
            comboBoxEditMalzemeGrubu.SelectedIndex = 0;

            comboBoxEditSatinAlmaKategori.Doldur("SELECT MALZEMEKATEGORI_ID, MALZEMEKATEGORI_ADI +' '+'(' + MALZEMEKATEGORI_KODU +')' AS 'MALZEME SATINALMA KATEGORISI' FROM TBL_LST_MALZEMEKATEGORILER (NOLOCK)  WHERE ISNULL (MALZEMEKATEGORI_DURUM,0)=1 ORDER BY MALZEMEKATEGORI_ADI", true);
            comboBoxEditSatinAlmaKategori.SelectedIndex = 0;

            DevCmbBoxItem[] itemsTur = { new DevCmbBoxItem(0, "<<Seçiniz>>"), new DevCmbBoxItem(1, "NLAG"), new DevCmbBoxItem(2, "UNBW") };
            comboBoxEditMalzemeTuru.Doldur(itemsTur);
            comboBoxEditMalzemeTuru.SelectedIndex = 0;

            DevCmbBoxItem[] itemsOlcu = { new DevCmbBoxItem(0, "<<Seçiniz>>"), new DevCmbBoxItem(1, "ADET"), new DevCmbBoxItem(2, "METRE") };
            comboBoxEditOlcuBirimi.Doldur(itemsOlcu);
            comboBoxEditOlcuBirimi.SelectedIndex = 0;

            DevCmbBoxItem[] items = { new DevCmbBoxItem(1, "TL"), new DevCmbBoxItem(2, "EURO"), new DevCmbBoxItem(3, "USD"), new DevCmbBoxItem(4, "JPY"), new DevCmbBoxItem(5, "CHF"), new DevCmbBoxItem(6, "GBP") };
            comboBoxEditParaBirimi.Doldur(items);
            comboBoxEditParaBirimi.SelectedIndex = 0;

            dateEditMalzemeTarih.DateTime = DateTime.Now;
        }

        private void YeniKayit()
        {
            this.isImageDirty = false;
            this.yeniMalzeme = true;
            this.barButtonItemKaydet.Enabled = true;
            this.textEditStokMiktari.ReadOnly = false;
            this.picEmp.Image = null;
            layoutControlMalzemeTanim.LayoutKontrolleriniSifirla();
        }
        private void barButtonItemYeniKayit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            YeniKayit();
        }

        private void Sil()
        {
            StringBuilder sb = new StringBuilder(1024);
            var item = gridViewMalzemeListesi.GetFocusedDataRow();
            if (item != null)
            {
                sb.Append("DELETE FROM TBL_LST_MALZEMELER ");
                sb.AppendFormat("WHERE MALZEME_ID={0}", clGenelTanim.DBToInt32(item["MALZEME_ID"]));
                clSqlTanim.RunStoredProc(sb.ToString());
                InitForm();
            }
        }

        private void barButtonItemSil_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr");

            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Sil();

                XtraMessageBox.Show("Kayıt Silindi ...");
            }
        }

        private void barButtonItemKapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğinizden emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void Kaydet()
        {
            DataTable dtMalzeme = this.yeniMalzeme ? null : dsMalzeme.Tables[0];
            DataRow rowMalzeme = dtMalzeme == null ? null : dtMalzeme.Rows[0];

            try
            {
                if (rowMalzeme == null)
                {
                    // INSERT into TBL_LST_MALZEMELER
                    string insertSql =
                        "INSERT INTO TBL_LST_MALZEMELER " +
                        "( MALZEME_TURU, MALZEME_ANAGRUBU, MALZEME_GRUBU, MALZEME_MATERYAL, MALZEME_PARCANO, MALZEME_ADI, " +
                        "  MALZEME_MINADET, MALZEME_MAXADET, MALZEME_RAFNO, MALZEME_TARIH, MALZEME_PARABIRIMI, MALZEME_OLCUBIRIMI, " +
                        "  MALZEME_STOKSAY, MALZEME_SATINALMAKATEGORI, MALZEME_NOTU ) " +
                        "VALUES " +
                        "( @turu, @anaGrubu, @grubu, @materyal, @parcaNo, @malzemeAdi, " +
                        "  @minAdet, @maxAdet, @rafNo, @tarih, @paraBirimi, @olcuBirimi, " +
                        "  @stokSay, @satinAlmaKategori, @notu ); " +
                        "SELECT SCOPE_IDENTITY()";

                    var insertParams = new[]
                    {
                        new SqlParameter("@turu",               clGenelTanim.DBToInt32(comboBoxEditMalzemeTuru.SecilenDeger().Id.ToString())),
                        new SqlParameter("@anaGrubu",           clGenelTanim.DBToInt32(comboBoxEditMalzemeAnaGrubu.SecilenDeger().Id.ToString())),
                        new SqlParameter("@grubu",              clGenelTanim.DBToInt32(comboBoxEditMalzemeGrubu.SecilenDeger().Id.ToString())),
                        new SqlParameter("@materyal",           clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMateryal.Text) ? "0" : textEditMateryal.Text)),
                        new SqlParameter("@parcaNo",            SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(textEditParcaNo.Text) ? (object)DBNull.Value : textEditParcaNo.Text },
                        new SqlParameter("@malzemeAdi",         SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(textEditMalzemeAdi.Text) ? (object)DBNull.Value : textEditMalzemeAdi.Text },
                        new SqlParameter("@minAdet",            clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMinAdet.Text) ? "0" : textEditMinAdet.Text)),
                        new SqlParameter("@maxAdet",            clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMaxAdet.Text) ? "0" : textEditMaxAdet.Text)),
                        new SqlParameter("@rafNo",              SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(textEditRafNo.Text) ? (object)DBNull.Value : textEditRafNo.Text },
                        new SqlParameter("@tarih",              SqlDbType.DateTime) { Value = dateEditMalzemeTarih.DateTime },
                        new SqlParameter("@paraBirimi",         clGenelTanim.DBToInt32(comboBoxEditParaBirimi.SecilenDeger().Id.ToString())),
                        new SqlParameter("@olcuBirimi",         clGenelTanim.DBToInt32(comboBoxEditOlcuBirimi.SecilenDeger().Id.ToString())),
                        new SqlParameter("@stokSay",            clGenelTanim.DBToInt32(checkEditStokSay.Checked)),
                        new SqlParameter("@satinAlmaKategori",  clGenelTanim.DBToInt32(comboBoxEditSatinAlmaKategori.SecilenDeger().Id.ToString())),
                        new SqlParameter("@notu",               SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(memoEditNot.Text) ? (object)DBNull.Value : memoEditNot.Text },
                    };

                    DataTable dt = clSqlTanim.RunStoredProc(insertSql, insertParams);
                    this.currentMalzemeID = clGenelTanim.DBToInt32(dt.Rows[0][0]);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Compute currency-converted unit/total prices
                        decimal birimFiyat = 0m, toplamFiyat = 0m;
                        decimal.TryParse(textEditBirimFiyat.Text.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out birimFiyat);
                        decimal.TryParse(textEditToplamFiyat.Text.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out toplamFiyat);

                        decimal sorgoBirimFiyat = birimFiyat, sorguToplamFiyat = toplamFiyat;
                        if (comboBoxEditParaBirimi.SelectedIndex == 0 && malzemeEuroFiyat != 0m)
                        {
                            sorgoBirimFiyat = birimFiyat / malzemeEuroFiyat;
                            sorguToplamFiyat = toplamFiyat / malzemeEuroFiyat;
                        }
                        else if (comboBoxEditParaBirimi.SelectedIndex == 1)
                        {
                            sorgoBirimFiyat = birimFiyat;
                            sorguToplamFiyat = toplamFiyat;
                        }
                        else if (comboBoxEditParaBirimi.SelectedIndex == 2)
                        {
                            sorgoBirimFiyat = birimFiyat * usdEuroOran;
                            sorguToplamFiyat = toplamFiyat * usdEuroOran;
                        }
                        else if (comboBoxEditParaBirimi.SelectedIndex == 3)
                        {
                            sorgoBirimFiyat = birimFiyat * jpyEuroOran;
                            sorguToplamFiyat = toplamFiyat * jpyEuroOran;
                        }
                        else if (comboBoxEditParaBirimi.SelectedIndex == 4)
                        {
                            sorgoBirimFiyat = birimFiyat * chfEuroOran;
                            sorguToplamFiyat = toplamFiyat * chfEuroOran;
                        }
                        else if (comboBoxEditParaBirimi.SelectedIndex == 5)
                        {
                            sorgoBirimFiyat = birimFiyat * gbpEuroOran;
                            sorguToplamFiyat = toplamFiyat * gbpEuroOran;
                        }

                        string girisInsertSql =
                            "IF NOT EXISTS (SELECT MALZEMEGIRIS_MALZEMELERID FROM TBL_LST_MALZEMEGIRIS WHERE MALZEMEGIRIS_MALZEMELERID=@malzemeId) " +
                            "INSERT INTO TBL_LST_MALZEMEGIRIS " +
                            "( MALZEMEGIRIS_MALZEMELERID, MALZEMEGIRIS_ADI, MALZEMEGIRIS_ADET, MALZEMEGIRIS_TARIH, " +
                            "  MALZEMEGIRIS_BIRIMFIYAT, MALZEMEGIRIS_TOPLAMFIYAT, MALZEMEGIRIS_PARABIRIMI, " +
                            "  MALZEMEGIRIS_SORGUBIRIMFIYAT, MALZEMEGIRIS_SORGUTOPLAMFIYAT, MALZEMEGIRIS_MALZEMEGRUPID ) " +
                            "VALUES " +
                            "( @malzemeId, @adi, @adet, @tarih, @birimFiyat, @toplamFiyat, @paraBirimi, @sorgoBirimFiyat, @sorguToplamFiyat, @grupId )";

                        var girisParams = new[]
                        {
                            new SqlParameter("@malzemeId",        this.currentMalzemeID),
                            new SqlParameter("@adi",              SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(textEditMalzemeAdi.Text) ? (object)DBNull.Value : textEditMalzemeAdi.Text },
                            new SqlParameter("@adet",             clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditStokMiktari.Text) ? "0" : textEditStokMiktari.Text)),
                            new SqlParameter("@tarih",            SqlDbType.DateTime) { Value = dateEditMalzemeTarih.DateTime },
                            new SqlParameter("@birimFiyat",       SqlDbType.Decimal) { Value = birimFiyat },
                            new SqlParameter("@toplamFiyat",      SqlDbType.Decimal) { Value = toplamFiyat },
                            new SqlParameter("@paraBirimi",       clGenelTanim.DBToInt32(comboBoxEditParaBirimi.SecilenDeger().Id.ToString())),
                            new SqlParameter("@sorgoBirimFiyat",  SqlDbType.Decimal) { Value = sorgoBirimFiyat },
                            new SqlParameter("@sorguToplamFiyat", SqlDbType.Decimal) { Value = sorguToplamFiyat },
                            new SqlParameter("@grupId",           clGenelTanim.DBToInt32(comboBoxEditMalzemeAnaGrubu.SecilenDeger().Id.ToString())),
                        };

                        clSqlTanim.RunStoredProc(girisInsertSql, girisParams);
                    }
                    else
                    {
                        this.currentMalzemeID = -1;
                    }
                }
                else
                {
                    // UPDATE TBL_LST_MALZEMELER
                    string updateSql =
                        "UPDATE TBL_LST_MALZEMELER SET " +
                        "  MALZEME_TURU=@turu, MALZEME_ANAGRUBU=@anaGrubu, MALZEME_GRUBU=@grubu, " +
                        "  MALZEME_MATERYAL=@materyal, MALZEME_PARCANO=@parcaNo, MALZEME_ADI=@malzemeAdi, " +
                        "  MALZEME_MINADET=@minAdet, MALZEME_MAXADET=@maxAdet, MALZEME_RAFNO=@rafNo, " +
                        "  MALZEME_TARIH=@tarih, MALZEME_OLCUBIRIMI=@olcuBirimi, MALZEME_PARABIRIMI=@paraBirimi, " +
                        "  MALZEME_SATINALMAKATEGORI=@satinAlmaKategori, MALZEME_STOKSAY=@stokSay, MALZEME_NOTU=@notu " +
                        "WHERE MALZEME_ID=@malzemeId";

                    var updateParams = new[]
                    {
                        new SqlParameter("@turu",               clGenelTanim.DBToInt32(comboBoxEditMalzemeTuru.SecilenDeger().Id.ToString())),
                        new SqlParameter("@anaGrubu",           clGenelTanim.DBToInt32(comboBoxEditMalzemeAnaGrubu.SecilenDeger().Id.ToString())),
                        new SqlParameter("@grubu",              clGenelTanim.DBToInt32(comboBoxEditMalzemeGrubu.SecilenDeger().Id.ToString())),
                        new SqlParameter("@materyal",           clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMateryal.Text) ? "0" : textEditMateryal.Text)),
                        new SqlParameter("@parcaNo",            SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(textEditParcaNo.Text) ? (object)DBNull.Value : textEditParcaNo.Text },
                        new SqlParameter("@malzemeAdi",         SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(textEditMalzemeAdi.Text) ? (object)DBNull.Value : textEditMalzemeAdi.Text },
                        new SqlParameter("@minAdet",            clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMinAdet.Text) ? "0" : textEditMinAdet.Text)),
                        new SqlParameter("@maxAdet",            clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMaxAdet.Text) ? "0" : textEditMaxAdet.Text)),
                        new SqlParameter("@rafNo",              SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(textEditRafNo.Text) ? (object)DBNull.Value : textEditRafNo.Text },
                        new SqlParameter("@tarih",              SqlDbType.DateTime) { Value = dateEditMalzemeTarih.DateTime },
                        new SqlParameter("@olcuBirimi",         clGenelTanim.DBToInt32(comboBoxEditOlcuBirimi.SecilenDeger().Id.ToString())),
                        new SqlParameter("@paraBirimi",         clGenelTanim.DBToInt32(comboBoxEditParaBirimi.SecilenDeger().Id.ToString())),
                        new SqlParameter("@satinAlmaKategori",  clGenelTanim.DBToInt32(comboBoxEditSatinAlmaKategori.SecilenDeger().Id.ToString())),
                        new SqlParameter("@stokSay",            clGenelTanim.DBToInt32(checkEditStokSay.Checked)),
                        new SqlParameter("@notu",               SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(memoEditNot.Text) ? (object)DBNull.Value : memoEditNot.Text },
                        new SqlParameter("@malzemeId",          this.currentMalzemeID),
                    };

                    clSqlTanim.RunStoredProc(updateSql, updateParams);
                }

                if (this.yeniMalzeme)
                {
                    this.yeniMalzeme = false;
                }

                KaydetResim();
                InitForm();
                malzemeSec(this.currentMalzemeID);
                XtraMessageBox.Show("İşlem Başarılı ...");
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }

        private bool KaydetResim()
        {
            if (this.isImageDirty)
            {
                SaveImage();
            }

            return this.isImageDirty;
        }

        private void ShowImage(int malzemeId)
        {
            try
            {
                byte[] img = null;

                using (var conn = new SqlConnection(clSqlTanim.connectionString))
                using (var cmd = new SqlCommand("SELECT MALZEME_RESIM FROM TBL_LST_MALZEMELER WHERE MALZEME_ID=@id", conn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = malzemeId;
                    conn.Open();

                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            img = (byte[])reader[0];
                        }
                    }
                }

                if (img == null)
                {
                    picEmp.Image = null;
                }
                else
                {
                    using (var ms = new MemoryStream(img))
                    {
                        picEmp.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }


        private void SaveImage()
        {
            try
            {
                if (this.picEmp.Image == null)
                {
                    clSqlTanim.ExecuteNonQuery(
                        "UPDATE TBL_LST_MALZEMELER SET MALZEME_RESIM=NULL WHERE MALZEME_ID=@id",
                        new[] { new SqlParameter("@id", this.currentMalzemeID) });
                }
                else
                {
                    byte[] imgBytes;
                    using (var fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read))
                    using (var br = new BinaryReader(fs))
                        imgBytes = br.ReadBytes((int)fs.Length);

                    using (var conn = new SqlConnection(clSqlTanim.connectionString))
                    using (var cmd = new SqlCommand("UPDATE TBL_LST_MALZEMELER SET MALZEME_RESIM=@Image WHERE MALZEME_ID=@id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Image", imgBytes));
                        cmd.Parameters.Add(new SqlParameter("@id", this.currentMalzemeID));
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void barButtonItemKaydet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr");

            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Kaydet();
                this.barButtonItemKaydet.Enabled = false;
                this.textEditStokMiktari.ReadOnly = true;
            }
        }

        private void frmMalzemeTanimlama_Load(object sender, EventArgs e)
        {
            InitFormKur();
            InitForm();
            SetContainsFilter(gridViewMalzemeListesi);
            UygulaMaksMinYetkisi();
        }

        private void barButtonItemYazdr_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            clGenelTanim.GridYazdir(gridControlMalzemeListesi, "Malzeme Listesi");
        }

        private void barButtonItemAktar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            clGenelTanim.GridExport(gridControlMalzemeListesi, "Malzeme Listesi", this);
        }

        private void gridViewMalzemeListesi_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int malzeme_StokMiktari = Convert.ToInt32(gridViewMalzemeListesi.GetRowCellDisplayText(e.RowHandle, gridViewMalzemeListesi.Columns[8]));
                int malzeme_MinMiktari = Convert.ToInt32(gridViewMalzemeListesi.GetRowCellDisplayText(e.RowHandle, gridViewMalzemeListesi.Columns[9]));

                if (malzeme_StokMiktari <= malzeme_MinMiktari)
                {
                    e.Appearance.BackColor = Color.Red;
                }
            }
        }

        private void textEditStokMiktarı_TextChanged(object sender, EventArgs e)
        {
            if (textEditStokMiktari.Text != string.Empty && textEditBirimFiyat.Text != string.Empty)
            {
                malzemeToplamFiyat = Convert.ToDecimal(textEditStokMiktari.Text) * Convert.ToDecimal(textEditBirimFiyat.Text); ;
                textEditToplamFiyat.Text = malzemeToplamFiyat.ToString();
            }
        }

        private void frmMalzemeTanimlama_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        void PreviewPrintableComponent(IPrintable component, UserLookAndFeel lookAndFeel)
        {
            using (DevExpress.XtraPrinting.PrintingSystemBase ps = new DevExpress.XtraPrinting.PrintingSystemBase())
            {
                ps.PageSettings.Landscape = true;
                DevExpress.XtraPrintingLinks.PrintableComponentLinkBase link = new PrintableComponentLinkBase(ps);
                link.Component = component;
                link.Margins.Right = 1;
                link.Margins.Left = 1;
                link.Margins.Top = 1;
                link.Margins.Bottom = 1;
                link.MinMargins = new System.Drawing.Printing.Margins(5, 5, 5, 5);
                link.PaperKind = System.Drawing.Printing.PaperKind.A4;
                link.Landscape = true;
                ps.Document.AutoFitToPagesWidth = 2;

                link.CreateDocument();
                DevExpress.XtraPrintingLinks.CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
                compositeLink.Links.AddRange(new object[] { link });
                compositeLink.CreateDocument();
            };
        }

        private void gridViewMalzemeListesi_Click(object sender, EventArgs e)
        {
            var item = gridViewMalzemeListesi.GetFocusedDataRow();
            if (item != null)
            {
                malzemeSec(clGenelTanim.DBToInt32(item["MALZEME_ID"]));
                this.textEditStokMiktari.ReadOnly = true;
                this.barButtonItemKaydet.Enabled = true;
            }
        }

        private void malzemeSec(int malzemeId)
        {
            yeniMalzeme = false;
            currentMalzemeID = malzemeId;
            malzemeDetayListele(currentMalzemeID);
        }

        private void malzemeDetayListele(int malzemeId)
        {
            layoutControlMalzemeTanim.LayoutKontrolleriniSifirla();

            this.ShowImage(malzemeId);

            DataSet ds = clSqlTanim.RunStoredProcDS(string.Format("sel_MalzemeDetayListele @malzemeId={0}", malzemeId), "MD");

            this.dsMalzeme = ds;

            MalzemeBilgileriniDoldur(ds);

            this.isImageDirty = false;
        }

        private void MalzemeBilgileriniDoldur(DataSet ds)
        {
            DataTable dtMalzeme = ds.Tables[0];
            DataTable dtMalzemeGiris = ds.Tables[1];

            if (dtMalzeme != null && dtMalzeme.Rows.Count > 0)
            {
                comboBoxEditMalzemeTuru.DegerSec(clGenelTanim.DBToInt32(dtMalzeme.Rows[0]["MALZEME_TURU"]));

                comboBoxEditMalzemeAnaGrubu.DegerSec(clGenelTanim.DBToInt32(dtMalzeme.Rows[0]["MALZEME_ANAGRUBU"]));
                comboBoxEditMalzemeGrubu.DegerSec(clGenelTanim.DBToInt32(dtMalzeme.Rows[0]["MALZEME_GRUBU"]));
                textEditMateryal.Text = clGenelTanim.DBToInt32(dtMalzeme.Rows[0]["MALZEME_MATERYAL"]).ToString();
                textEditParcaNo.Text = clGenelTanim.DBToString(dtMalzeme.Rows[0]["MALZEME_PARCANO"]);

                textEditMalzemeAdi.Text = clGenelTanim.DBToString(dtMalzeme.Rows[0]["MALZEME_ADI"]);

                textEditStokMiktari.Text = clGenelTanim.DBToInt32(dtMalzeme.Rows[0]["MALZEME_STOKMIKTARI"]).ToString();
                textEditMinAdet.Text = clGenelTanim.DBToInt32(dtMalzeme.Rows[0]["MALZEME_MINADET"]).ToString();
                textEditMaxAdet.Text = clGenelTanim.DBToInt32(dtMalzeme.Rows[0]["MALZEME_MAXADET"]).ToString();

                textEditRafNo.Text = clGenelTanim.DBToString(dtMalzeme.Rows[0]["MALZEME_RAFNO"]);
                comboBoxEditOlcuBirimi.DegerSec(clGenelTanim.DBToInt32(dtMalzeme.Rows[0]["MALZEME_OLCUBIRIMI"]));
                comboBoxEditParaBirimi.DegerSec(clGenelTanim.DBToInt32(dtMalzeme.Rows[0]["MALZEME_PARABIRIMI"]));


                checkEditStokSay.Checked = clGenelTanim.DBToInt32(dtMalzeme.Rows[0]["MALZEME_STOKSAY"]) == 1;

                comboBoxEditSatinAlmaKategori.DegerSec(clGenelTanim.DBToInt32(dtMalzeme.Rows[0]["MALZEME_SATINALMAKATEGORI"]));
                memoEditNot.Text = clGenelTanim.DBToString(dtMalzeme.Rows[0]["MALZEME_NOTU"]);
                //textEditBirimFiyat.Text = oldValue1;
                //textEditToplamFiyat.Text = oldValue2;
            }

            if (dtMalzemeGiris != null && dtMalzemeGiris.Rows.Count > 0)
            {
                textEditBirimFiyat.Text = clGenelTanim.DBToString(dtMalzemeGiris.Rows[0]["MALZEMEGIRIS_BIRIMFIYAT"]).ToString();
                textEditToplamFiyat.Text = clGenelTanim.DBToString(dtMalzemeGiris.Rows[0]["MALZEMEGIRIS_TOPLAMFIYAT"]).ToString();
                comboBoxEditParaBirimi.DegerSec(clGenelTanim.DBToInt32(dtMalzemeGiris.Rows[0]["MALZEMEGIRIS_PARABIRIMI"]));
                dateEditMalzemeTarih.DateTime = clGenelTanim.DBToDate(dtMalzemeGiris.Rows[0]["MALZEMEGIRIS_TARIH"]);
            }

        }

        private void malzemeGirisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = gridViewMalzemeListesi.FocusedRowHandle;
            int ID = 0, GRUPID = 0;
            if (index >= 0)
            {
                ID = Convert.ToInt32(gridViewMalzemeListesi.GetRowCellValue(index, "MALZEME_ID").ToString());
                GRUPID = Convert.ToInt32(gridViewMalzemeListesi.GetRowCellValue(index, "MALZEMEANAGRUP_ID").ToString());
            }
            frmMalzemeGirisEkle u = new frmMalzemeGirisEkle();
            u.MALZEMELER_ID = ID;
            u.MALZEMELER_GRUPID = GRUPID;
            u.ShowDialog();
        }

        private void malzemeCikisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = gridViewMalzemeListesi.FocusedRowHandle;
            int ID = 0, GRUPID = 0;
            if (index >= 0)
            {
                ID = Convert.ToInt32(gridViewMalzemeListesi.GetRowCellValue(index, "MALZEME_ID").ToString());
            }
            frmMalzemeCikisEkle u = new frmMalzemeCikisEkle();
            u.MALZEMELER_ID = ID;
            u.ShowDialog();
        }

        private void textEditMateryal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textEditStokMiktari_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textEditMinAdet_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textEditMaxAdet_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        string oldValue1;
        private void textEditBirimFiyat_TextChanged(object sender, EventArgs e)
        {
            if (textEditStokMiktari.Text != string.Empty && textEditBirimFiyat.Text != string.Empty)
            {
                malzemeToplamFiyat = Convert.ToDecimal(textEditStokMiktari.Text) * Convert.ToDecimal(textEditBirimFiyat.Text); ;
                textEditToplamFiyat.Text = malzemeToplamFiyat.ToString();

                if (oldValue1 != textEditBirimFiyat.Text)
                {
                    oldValue1 = textEditBirimFiyat.Text;
                }
            }
        }

        string oldValue2;
        private void textEditToplamFiyat_TextChanged(object sender, EventArgs e)
        {
            if (textEditStokMiktari.Text != string.Empty && textEditBirimFiyat.Text != string.Empty)
            {
                if (oldValue2 != textEditToplamFiyat.Text)
                {
                    oldValue2 = textEditToplamFiyat.Text;
                }
            }
        }

        private void barButtonItemKurBilgi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmMalzemeKurBilgileri frmKurBilgi = new frmMalzemeKurBilgileri();
            frmKurBilgi.Show();
        }

        private Bitmap createBarcode(string data)
        {
            Bitmap barCode = new Bitmap(1, 1);
            Font threeOfNine = new Font("Free 3 of 9", 45, FontStyle.Regular, GraphicsUnit.Point);
            Graphics graphics = Graphics.FromImage(barCode);
            SizeF dataSize = graphics.MeasureString(data, threeOfNine);
            barCode = new Bitmap(barCode, dataSize.ToSize());
            graphics = Graphics.FromImage(barCode);
            graphics.Clear(Color.White);
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            graphics.DrawString(data, threeOfNine, new SolidBrush(Color.Black), 0, 0);
            graphics.Flush();
            threeOfNine.Dispose();
            graphics.Dispose();
            return barCode;
        }

        private void barkodYazdirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridViewMalzemeListesi.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Lütfen yazdirmak için en az bir satir seçin.", "Uyari", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PrintDialog printDialog = new PrintDialog();
                PrintDocument document = new PrintDocument();
                ApplyBarcodeLabelSettings(document);
                printDialog.Document = document;

                if (printDialog.ShowDialog() != DialogResult.OK)
                    return;

                int[] selectedRows = gridViewMalzemeListesi.GetSelectedRows();
                int currentIndex = 0;

                document.PrintPage += (s, ev) =>
                {
                    ev.Graphics.PageUnit = GraphicsUnit.Millimeter;

                    DataRow row = gridViewMalzemeListesi.GetDataRow(selectedRows[currentIndex]);
                    if (row == null)
                    {
                        currentIndex++;
                        ev.HasMorePages = currentIndex < selectedRows.Length;
                        return;
                    }

                    StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
                    StringFormat leftFormat = new StringFormat { Alignment = StringAlignment.Near };
                    StringFormat rightColumnFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    string materyel = clGenelTanim.DBToString(row["MALZEME MATERYEL NO"]) ?? "";
                    string materyelRaw = materyel;

                    if (!string.IsNullOrEmpty(materyel))
                    {
                        for (int j = 3; j < materyel.Length; j += 4)
                        {
                            try { materyel = materyel.Insert(j, " "); }
                            catch { break; }
                        }
                    }

                    const float hundredthsInchToMm = 25.4f / 100f;
                    float pageWidth = ev.PageSettings.PaperSize.Width * hundredthsInchToMm;
                    float pageHeight = ev.PageSettings.PaperSize.Height * hundredthsInchToMm;

                    float marginX = 2f;
                    float marginY = 1f;
                    float contentWidth = Math.Max(1f, pageWidth - (marginX * 2));
                    float contentHeight = Math.Max(1f, pageHeight - (marginY * 2));

                    float rightColumnWidth = 15f;
                    float columnGap = 2f;
                    float materyalTextHeight = 3.5f;
                    float barcodeHeight = 11f;
                    float barcodeWidth = Math.Max(1f, contentWidth - rightColumnWidth - columnGap);
                    float barcodeY = marginY + materyalTextHeight + 2f;

                    if (!string.IsNullOrWhiteSpace(materyelRaw))
                    {
                        using (var barcodeImage = CreateCode128Bitmap(
                            materyelRaw,
                            (int)Math.Max(1f, barcodeWidth),
                            (int)Math.Max(1f, barcodeHeight)))
                        {
                            ev.Graphics.DrawImage(barcodeImage, marginX, barcodeY, barcodeWidth, barcodeHeight);
                        }
                    }

                    var rafNo = clGenelTanim.DBToString(row["MALZEME RAF NO"]) ?? "";
                    string[] rafBilgileri = rafNo.Split('-');

                    string strRa = rafBilgileri.Length > 0 ? rafBilgileri[0] : "";
                    string strRow = rafBilgileri.Length > 1 ? rafBilgileri[1] : "";
                    string strCo = rafBilgileri.Length > 2 ? rafBilgileri[2] : "";

                    float rightX = marginX + barcodeWidth + columnGap;
                    float rightRowHeight = barcodeHeight / 3f;
                    float rightY = barcodeY;
                    ev.Graphics.DrawString("Ra:", new Font("Arial", 7.5f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX, rightY, 5f, rightRowHeight), rightColumnFormat);
                    ev.Graphics.DrawString(strRa, new Font("Arial", 8f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX + 5f, rightY, rightColumnWidth - 5f, rightRowHeight), rightColumnFormat);
                    rightY += rightRowHeight;
                    ev.Graphics.DrawString("Ro:", new Font("Arial", 7.5f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX, rightY, 5f, rightRowHeight), rightColumnFormat);
                    ev.Graphics.DrawString(strRow, new Font("Arial", 8f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX + 5f, rightY, rightColumnWidth - 5f, rightRowHeight), rightColumnFormat);
                    rightY += rightRowHeight;
                    ev.Graphics.DrawString("Co:", new Font("Arial", 7.5f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX, rightY, 5f, rightRowHeight), rightColumnFormat);
                    ev.Graphics.DrawString(strCo, new Font("Arial", 8f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX + 5f, rightY, rightColumnWidth - 5f, rightRowHeight), rightColumnFormat);

                    using (var materyalFont = new Font("Arial", 15f, FontStyle.Bold))
                    {
                        DrawCenteredSpacedText(
                            ev.Graphics,
                            materyelRaw,
                            materyalFont,
                            Brushes.Black,
                            new RectangleF(marginX, marginY, barcodeWidth, materyalTextHeight),
                            1.6f);
                    }

                    float currentY = barcodeY + barcodeHeight + 1f;

                    ev.Graphics.DrawString(clGenelTanim.DBToString(row["MALZEME ADI"]),
                        new Font("Arial", 8f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(marginX, currentY, contentWidth, 3f), leftFormat);
                    currentY += 3f;

                    ev.Graphics.DrawString(clGenelTanim.DBToString(row["MALZEME PARÇA NO"]),
                        new Font("Arial", 7f, FontStyle.Regular),
                        Brushes.Black, new RectangleF(marginX, currentY, contentWidth, 3f), leftFormat);
                    currentY += 3f;
                    currentY += 1f;

                    float labelRowHeight = 3f;
                    float valueWidth = contentWidth * 0.22f;
                    float minLabelWidth = contentWidth * 0.12f;
                    float maxLabelWidth = contentWidth * 0.12f;
                    float fiyatLabelWidth = contentWidth * 0.145f;
                    float fiyatValueWidth = contentWidth * 0.255f;

                    DrawLabelAndValue(ev.Graphics, "Min:", clGenelTanim.DBToString(row["MALZEME MIN ADET"]),
                        marginX, currentY, minLabelWidth, valueWidth, labelRowHeight, leftFormat);
                    DrawLabelAndValue(ev.Graphics, "Max:", clGenelTanim.DBToString(row["MALZEME MAX ADET"]),
                        marginX + (contentWidth * 0.34f), currentY, maxLabelWidth, valueWidth, labelRowHeight, leftFormat);
                    DrawPriceWithCurrency(ev.Graphics,
                        "Fiyat:",
                        clGenelTanim.DBToString(row["MALZEME GIRIS P.BIRIMI"]),
                        clGenelTanim.DBToString(row["MALZEME GIRIS B.FIYAT"]),
                        marginX + (contentWidth * 0.62f),
                        currentY,
                        fiyatLabelWidth,
                        fiyatValueWidth,
                        labelRowHeight,
                        leftFormat);

                    currentIndex++;
                    ev.HasMorePages = currentIndex < selectedRows.Length;
                };

                document.Print();
            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show("Geçersiz yazici ayari: " + ex.Message, "Yazici Hatasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata olustu: " + ex.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DrawLabelAndValue(Graphics g, string label, string value,
                                       float x, float y, float labelWidth, float valueWidth,
                                       float height, StringFormat format)
        {

            using (var font = new Font("Arial", 8.5f, FontStyle.Bold))
            {
                g.DrawString(label, font, Brushes.Black,
                    new RectangleF(x, y, labelWidth, height), format);

                g.DrawString(value ?? "", font, Brushes.Black,
                    new RectangleF(x + labelWidth, y, valueWidth, height), format);
            }
        }

        private void DrawCenteredSpacedText(Graphics g, string text, Font font, Brush brush, RectangleF area, float charSpacing)
        {
            if (string.IsNullOrEmpty(text))
                return;

            float textWidth = 0f;
            for (int i = 0; i < text.Length; i++)
            {
                textWidth += g.MeasureString(text[i].ToString(), font).Width;
                if (i < text.Length - 1)
                    textWidth += charSpacing;
            }

            float x = area.X + Math.Max(0f, (area.Width - textWidth) / 2f);
            float y = area.Y;

            for (int i = 0; i < text.Length; i++)
            {
                string ch = text[i].ToString();
                float w = g.MeasureString(ch, font).Width;
                g.DrawString(ch, font, brush, new PointF(x, y));
                x += w + charSpacing;
            }
        }

        private void DrawPriceWithCurrency(Graphics g, string label, string currency, string price,
                                           float x, float y, float labelWidth, float valueWidth,
                                           float height, StringFormat format)
        {
            using (var font = new Font("Arial", 8.5f, FontStyle.Bold))
            {
                g.DrawString(label, font, Brushes.Black,
                    new RectangleF(x, y, labelWidth, height), format);

                string currencyText = currency ?? "";
                string priceText = price ?? "";
                float valueX = x + labelWidth;

                float currencyWidth = string.IsNullOrEmpty(currencyText) ? 0f : g.MeasureString(currencyText, font).Width;
                float priceWidth = string.IsNullOrEmpty(priceText) ? 0f : g.MeasureString(priceText, font).Width;

                float dynamicGap = 1f;

                float drawX = valueX;
                if (!string.IsNullOrEmpty(priceText))
                {
                    g.DrawString(priceText, font, Brushes.Black, new PointF(drawX, y));
                    drawX += priceWidth + (string.IsNullOrEmpty(currencyText) ? 0f : dynamicGap);
                }

                if (!string.IsNullOrEmpty(currencyText))
                {
                    g.DrawString(currencyText, font, Brushes.Black, new PointF(drawX, y));
                }
            }
        }


        private static System.Drawing.Bitmap CreateCode128Bitmap(string data, int width, int height)
        {
            int[] startB = { 2, 1, 1, 4, 1, 2 };
            int[] stop   = { 2, 3, 3, 1, 1, 1, 2 };
            int[][] P = new int[][] {
                new[]{2,1,2,2,2,2},new[]{2,2,2,1,2,2},new[]{2,2,2,2,2,1},new[]{1,2,1,2,2,3},
                new[]{1,2,1,3,2,2},new[]{1,3,1,2,2,2},new[]{1,2,2,2,1,3},new[]{1,2,2,3,1,2},
                new[]{1,3,2,2,1,2},new[]{2,2,1,2,1,3},new[]{2,2,1,3,1,2},new[]{2,3,1,2,1,2},
                new[]{1,1,2,2,3,2},new[]{1,2,2,1,3,2},new[]{1,2,2,2,3,1},new[]{1,1,3,2,2,2},
                new[]{1,2,3,1,2,2},new[]{1,2,3,2,2,1},new[]{2,2,3,2,1,1},new[]{2,2,1,1,3,2},
                new[]{2,2,1,2,3,1},new[]{2,1,3,2,1,2},new[]{2,2,3,1,1,2},new[]{3,1,2,1,3,1},
                new[]{3,1,1,2,2,2},new[]{3,2,1,1,2,2},new[]{3,2,1,2,2,1},new[]{3,1,2,2,1,2},
                new[]{3,2,2,1,1,2},new[]{3,2,2,2,1,1},new[]{2,1,2,1,2,3},new[]{2,1,2,3,2,1},
                new[]{2,3,2,1,2,1},new[]{1,1,1,3,2,3},new[]{1,3,1,1,2,3},new[]{1,3,1,3,2,1},
                new[]{1,1,2,3,1,3},new[]{1,3,2,1,1,3},new[]{1,3,2,3,1,1},new[]{2,1,1,3,1,3},
                new[]{2,3,1,1,1,3},new[]{2,3,1,3,1,1},new[]{1,1,2,1,3,3},new[]{1,1,2,3,3,1},
                new[]{1,3,2,1,3,1},new[]{1,1,3,1,2,3},new[]{1,1,3,3,2,1},new[]{1,3,3,1,2,1},
                new[]{3,1,3,1,2,1},new[]{2,1,1,3,3,1},new[]{2,3,1,1,3,1},new[]{2,1,3,1,1,3},
                new[]{2,1,3,3,1,1},new[]{2,1,3,1,3,1},new[]{3,1,1,1,2,3},new[]{3,1,1,3,2,1},
                new[]{3,3,1,1,2,1},new[]{3,1,2,1,1,3},new[]{3,1,2,3,1,1},new[]{3,3,2,1,1,1},
                new[]{3,1,4,1,1,1},new[]{2,2,1,4,1,1},new[]{4,3,1,1,1,1},new[]{1,1,1,2,2,4},
                new[]{1,1,1,4,2,2},new[]{1,2,1,1,2,4},new[]{1,2,1,4,2,1},new[]{1,4,1,1,2,2},
                new[]{1,4,1,2,2,1},new[]{1,1,2,2,1,4},new[]{1,1,2,4,1,2},new[]{1,2,2,1,1,4},
                new[]{1,2,2,4,1,1},new[]{1,4,2,1,1,2},new[]{1,4,2,2,1,1},new[]{2,4,1,2,1,1},
                new[]{2,2,1,1,1,4},new[]{4,1,3,1,1,1},new[]{2,4,1,1,1,2},new[]{1,3,4,1,1,1},
                new[]{1,1,1,2,4,2},new[]{1,2,1,1,4,2},new[]{1,2,1,2,4,1},new[]{1,1,4,2,1,2},
                new[]{1,2,4,1,1,2},new[]{1,2,4,2,1,1},new[]{4,1,1,2,1,2},new[]{4,2,1,1,1,2},
                new[]{4,2,1,2,1,1},new[]{2,1,2,1,4,1},new[]{2,1,4,1,2,1},new[]{4,1,2,1,2,1},
                new[]{1,1,1,1,4,3},new[]{1,1,1,3,4,1},new[]{1,3,1,1,4,1},new[]{1,1,4,1,1,3},
                new[]{1,1,4,3,1,1},new[]{4,1,1,1,1,3},new[]{4,1,1,3,1,1},new[]{1,1,3,1,4,1},
                new[]{1,1,4,1,3,1},new[]{3,1,1,1,4,1},new[]{4,1,1,1,3,1},new[]{2,1,1,4,1,2}
            };
            int startCode = 104;
            var chars = new System.Collections.Generic.List<int> { startCode };
            int check = startCode;
            for (int i = 0; i < data.Length; i++) { int v = (int)data[i] - 32; chars.Add(v); check += v * (i + 1); }
            chars.Add(check % 103);
            int totalUnits = 0;
            foreach (int u in startB) totalUnits += u;
            foreach (int c in chars) { if (c >= 0 && c < P.Length) foreach (int u in P[c]) totalUnits += u; }
            foreach (int u in stop) totalUnits += u;
            float scale = Math.Max(1f, (float)width / totalUnits);
            var bmp = new System.Drawing.Bitmap((int)(totalUnits * scale), height);
            using (var g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.White);
                float x = 0; bool black = true;
                System.Action<int[]> draw = (bars) => { foreach (int u in bars) { float w = u * scale; if (black) g.FillRectangle(System.Drawing.Brushes.Black, x, 0, w, height); x += w; black = !black; } };
                draw(startB);
                foreach (int c in chars) { if (c >= 0 && c < P.Length) draw(P[c]); }
                draw(stop);
            }
            return bmp;
        }        private void ApplyBarcodeLabelSettings(PrintDocument document)
        {
            const float mmToHundredthsInch = 100f / 25.4f;
            int widthMm = 70;
            int heightMm = 30;
            document.DefaultPageSettings.PaperSize = new PaperSize("BarcodeLabel70x30",
                (int)Math.Round(widthMm * mmToHundredthsInch),
                (int)Math.Round(heightMm * mmToHundredthsInch));
            document.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
        }


        private void comboBoxEditMalzemeAnaGrubu_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT MALZEMEGRUP_ID, MALZEMEGRUP_ADI FROM TBL_LST_MALZEMEGRUPLAR ");
            sb.AppendFormat("WHERE MALZEMEANAGRUP_ID={0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeAnaGrubu.SecilenDeger().Id.ToString()));

            comboBoxEditMalzemeGrubu.Doldur(sb.ToString(), true);
            comboBoxEditMalzemeGrubu.SelectedIndex = 0;
        }

        private void comboBoxEditMalzemeGrubu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //StringBuilder sb = new StringBuilder(512);
            //sb.Append("SELECT MALZEMEKATEGORI_ID, MALZEMEKATEGORI_ADI FROM TBL_LST_MALZEMEKATEGORILER ");
            //sb.AppendFormat("WHERE MALZEMEKATEGORIGRUP_ID={0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeGrubu.SecilenDeger().Id.ToString()));

            //comboBoxEditSatinAlmaKategori.Doldur(sb.ToString(), true);
            //comboBoxEditSatinAlmaKategori.SelectedIndex = 0;
        }

        private void malzemeAnaGrubuGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAnaGrupGuncelle u = new frmAnaGrupGuncelle();
            u.ShowDialog();
        }

        private void malzemeGrubuGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGrupGuncelle u = new frmGrupGuncelle();
            u.ShowDialog();
        }

        private void picEmp_DoubleClick(object sender, EventArgs e)
        {
            if (currentMalzemeID <= 0)
            {
                XtraMessageBox.Show("Önce bir malzeme seçin.");
                return;
            }

            using (var u = new frmMalzemeResim())
            {
                u.nMALZEMELER_ID = currentMalzemeID;
                u.StartPosition = FormStartPosition.CenterParent;
                u.ShowInTaskbar = false;
                u.ShowIcon = false;
                u.TopMost = true;
                u.ShowDialog(this);
            }
        }

        private void barButtonItemBarkod_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmMalzemeBarkodlama frmBarkod = new frmMalzemeBarkodlama();
            frmBarkod.Show();
        }

        private void pictureBoxMalzemeResim_DoubleClick(object sender, EventArgs e)
        {
            ///* Kullanici bu butona tikladiginda, OpenFileDialog kontrolümüz, dosya açma iletisim kutusunu açar. Kullanici bir dosya seçip OK tusunda bastiginda, Picture Box kontrolümüze seçilen resim dosyasi alinarak gösterilmesi saglanir. Daha sonra seçilen dosyanin tam adresi label kontrolümüze alinir ve resimAdresi degiskenimize atanir. */

            //if (ofdResim.ShowDialog() == DialogResult.OK)
            //{
            //    pictureBoxMalzemeResim.Image = System.Drawing.Image.FromFile(ofdResim.FileName); /* Drawing isim uzayinda yer alan Image sinifinin FromFile metodunu kullanarak belirtilen adresteki dosya PictureBox kontrolü içine çizilir. */

            //    resimAdresi = ofdResim.FileName.ToString();
            //}
        }

        private void pictureEditGenel_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureEditGenel_DoubleClick(object sender, EventArgs e)
        {
            //if (this.currentMalzemeID <= 0)
            //    return;

            //this.pictureEditGenel.LoadImage();
        }

        private void pictureEditGenel_MouseEnter(object sender, EventArgs e)
        {
            //pictureEditGenel.Height += 100;
            //pictureEditGenel.Width += 100;
        }

        float zoomSpeedFactor = 0.01f;
        private void pictureEditGenel_MouseHover(object sender, EventArgs e)
        {
            //pictureEditGenel.Properties.ZoomPercent += e.Delta * zoomSpeedFactor;
            //DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }

        private void barButtonItemResimSec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.isImageDirty = true;
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png";
                dlg.Title = "Materyal Resmi Seçiniz ...";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName.ToString();
                    picEmp.ImageLocation = imgLoc;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void textEditBirimFiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != ',')
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == (char)13) //Enter tusuna basildimi
                {

                    Single birimfiyat = Convert.ToSingle(textEditBirimFiyat.Text);
                    textEditBirimFiyat.Text = string.Format("{0:c}", double.Parse(textEditBirimFiyat.Text));
                    //Parabirimine dönüstür tekrar ayni text kutusuna aktar
                }
            }
            catch (Exception)
            {

                MessageBox.Show("BIRIM FIYAT GEÇERSIZ");
            }
        }
    }
}
