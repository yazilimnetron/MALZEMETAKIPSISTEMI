using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
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
using ZXing;
using ZXing.Common;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeTanimlama : Form
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
            sb.Append("[MALZEME GİRİŞ P.BİRİMİ], ");
            sb.Append("(SELECT TOP 1 e.MALZEMEGIRIS_BIRIMFIYAT FROM TBL_LST_MALZEMEGIRIS e WHERE s.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC ) [MALZEME GİRİŞ B.FİYAT], ");
            sb.Append("(isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) AS 'MALZEME STOK ADET', ISNULL(s.MALZEME_MINADET,0) AS 'MALZEME MİN ADET', ISNULL(s.MALZEME_MAXADET, 0) AS 'MALZEME MAX ADET', case when s.MALZEME_OLCUBIRIMI = 1 then 'ADET' when s.MALZEME_OLCUBIRIMI = 2 then 'METRE' end 'MALZEME ÖLÇÜ BİRİMİ', s.MALZEME_RAFNO AS 'MALZEME RAF NO',");
            sb.Append(" d.MALZEMEANAGRUP_ID, d.MALZEMEANAGRUP_ADI AS 'MALZEME ANA GRUBU', f.MALZEMEGRUP_ADI AS 'MALZEME GRUBU', ");
            sb.Append("(SELECT TOP 1 convert(varchar(10), MALZEMEGIRIS_TARIH, 121) FROM TBL_LST_MALZEMEGIRIS e WHERE s.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC) AS 'MALZEME TARİHİ', s.MALZEME_STOKSAY AS 'STOK SAYILSIN', e.MALZEMEKATEGORI_ADI + ' ' + '(' + e.MALZEMEKATEGORI_KODU + ')' AS 'MALZEME SATINALMA KATEGORISI', s.MALZEME_NOTU AS 'MALZEME NOTU' ");
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
            this.gridViewMalzemeListesi.Columns["MALZEME MATERYEL NO"].SummaryItem.DisplayFormat = "{0} Kayıt";

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
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void Kaydet()
        {
            StringBuilder sbI = new StringBuilder(1024);
            StringBuilder sbII = new StringBuilder(1024);
            StringBuilder sbU = new StringBuilder(1024);
            StringBuilder sbUU = new StringBuilder(1024);

            DataTable dtMalzeme = this.yeniMalzeme ? null : dsMalzeme.Tables[0];
            DataRow rowMalzeme = dtMalzeme == null ? null : dtMalzeme.Rows[0];

            try
            {
                if (rowMalzeme == null)
                {
                    sbI.Append("insert into TBL_LST_MALZEMELER ( MALZEME_TURU, MALZEME_ANAGRUBU, MALZEME_GRUBU, MALZEME_MATERYAL, MALZEME_PARCANO, MALZEME_ADI, MALZEME_MINADET, MALZEME_MAXADET,  MALZEME_RAFNO, MALZEME_TARIH, MALZEME_PARABIRIMI, MALZEME_OLCUBIRIMI, MALZEME_STOKSAY, MALZEME_SATINALMAKATEGORI, MALZEME_NOTU ) select");
                    sbI.AppendFormat("  {0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeTuru.SecilenDeger().Id.ToString()));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeAnaGrubu.SecilenDeger().Id.ToString()));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeGrubu.SecilenDeger().Id.ToString()));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMateryal.Text.ToString()) ? "0" : textEditMateryal.Text.ToString()));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(textEditParcaNo.Text.ToString(), 50, true));
                    sbI.AppendFormat(" ,N{0}", clGenelTanim.tosqlstring(textEditMalzemeAdi.Text.ToString(), 500, true));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMinAdet.Text.ToString()) ? "0" : textEditMinAdet.Text.ToString()));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMaxAdet.Text.ToString()) ? "0" : textEditMaxAdet.Text.ToString()));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(textEditRafNo.Text.ToString(), 50, true));
                    sbI.AppendFormat(" ,{0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditMalzemeTarih.DateTime).ToString("yyyy-MM-dd HH:mm") + "'");
                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(comboBoxEditParaBirimi.SecilenDeger().Id.ToString()));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(comboBoxEditOlcuBirimi.SecilenDeger().Id.ToString()));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(checkEditStokSay.Checked));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(comboBoxEditSatinAlmaKategori.SecilenDeger().Id.ToString()));
                    sbI.AppendFormat(" ,N{0}", clGenelTanim.tosqlstring(memoEditNot.Text.ToString(), 300, true));
                    string insertQuery = sbI.ToString() + "\r\nSELECT @@IDENTITY";

                    DataTable dt = clSqlTanim.RunStoredProc(insertQuery);
                    this.currentMalzemeID = clGenelTanim.DBToInt32(dt.Rows[0][0]);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sbII.AppendFormat("if not exists (select MALZEMEGIRIS_MALZEMELERID from TBL_LST_MALZEMEGIRIS where MALZEMEGIRIS_MALZEMELERID={0})", this.currentMalzemeID.ToString());
                        sbII.AppendLine();
                        sbII.Append("insert into TBL_LST_MALZEMEGIRIS ( MALZEMEGIRIS_MALZEMELERID, MALZEMEGIRIS_ADI, MALZEMEGIRIS_ADET, MALZEMEGIRIS_TARIH, MALZEMEGIRIS_BIRIMFIYAT, MALZEMEGIRIS_TOPLAMFIYAT, MALZEMEGIRIS_PARABIRIMI, MALZEMEGIRIS_SORGUBIRIMFIYAT, MALZEMEGIRIS_SORGUTOPLAMFIYAT, MALZEMEGIRIS_MALZEMEGRUPID ) select");
                        sbII.AppendFormat("  {0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(this.currentMalzemeID.ToString()) ? "0" : this.currentMalzemeID.ToString()));
                        sbII.AppendFormat(" ,N{0}", clGenelTanim.tosqlstring(textEditMalzemeAdi.Text.ToString(), 500, true));
                        sbII.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditStokMiktari.Text.ToString()) ? "0" : textEditStokMiktari.Text.ToString())));
                        sbII.AppendFormat(" ,{0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditMalzemeTarih.DateTime).ToString("yyyy-MM-dd HH:mm") + "'");
                        sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(textEditBirimFiyat.Text.ToString().Replace(',', '.'), 10, true));
                        sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(textEditToplamFiyat.Text.ToString().Replace(',', '.'), 10, true));
                        sbII.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(comboBoxEditParaBirimi.SecilenDeger().Id.ToString()));
                        if (comboBoxEditParaBirimi.SelectedIndex == 0)
                        {
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditBirimFiyat.Text) / malzemeEuroFiyat).ToString().Replace(',', '.'), 10, true));
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditToplamFiyat.Text) / malzemeEuroFiyat).ToString().Replace(',', '.'), 10, true));
                        }
                        if (comboBoxEditParaBirimi.SelectedIndex == 1)
                        {
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(textEditBirimFiyat.Text.ToString().Replace(',', '.'), 10, true));
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(textEditToplamFiyat.Text.ToString().Replace(',', '.'), 10, true));
                        }
                        if (comboBoxEditParaBirimi.SelectedIndex == 2)
                        {
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditBirimFiyat.Text) * usdEuroOran).ToString().Replace(',', '.'), 10, true));
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditToplamFiyat.Text) * usdEuroOran).ToString().Replace(',', '.'), 10, true));
                        }
                        if (comboBoxEditParaBirimi.SelectedIndex == 3)
                        {
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditBirimFiyat.Text) * jpyEuroOran).ToString().Replace(',', '.'), 10, true));
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditToplamFiyat.Text) * jpyEuroOran).ToString().Replace(',', '.'), 10, true));
                        }
                        if (comboBoxEditParaBirimi.SelectedIndex == 4)
                        {
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditBirimFiyat.Text) * chfEuroOran).ToString().Replace(',', '.'), 10, true));
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditToplamFiyat.Text) * chfEuroOran).ToString().Replace(',', '.'), 10, true));
                        }
                        if (comboBoxEditParaBirimi.SelectedIndex == 5)
                        {
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditBirimFiyat.Text) * gbpEuroOran).ToString().Replace(',', '.'), 10, true));
                            sbII.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditToplamFiyat.Text) * gbpEuroOran).ToString().Replace(',', '.'), 10, true));
                        }
                        sbII.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeAnaGrubu.SecilenDeger().Id.ToString()));

                        clSqlTanim.RunStoredProc(sbII.ToString());
                        //}

                    }
                    else
                    {
                        this.currentMalzemeID = -1;
                    }
                }
                else
                {
                    sbU.AppendFormat("update TBL_LST_MALZEMELER set ");
                    sbU.AppendFormat("  MALZEME_TURU={0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeTuru.SecilenDeger().Id.ToString()));
                    sbU.AppendFormat(" ,MALZEME_ANAGRUBU={0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeAnaGrubu.SecilenDeger().Id.ToString()));
                    sbU.AppendFormat(" ,MALZEME_GRUBU={0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeGrubu.SecilenDeger().Id.ToString()));
                    sbU.AppendFormat(" ,MALZEME_MATERYAL={0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMateryal.Text.ToString()) ? "0" : textEditMateryal.Text.ToString()));
                    sbU.AppendFormat(" ,MALZEME_PARCANO={0}", clGenelTanim.tosqlstring(textEditParcaNo.Text.ToString(), 50, true));
                    sbU.AppendFormat(" ,MALZEME_ADI=N{0}", clGenelTanim.tosqlstring(textEditMalzemeAdi.Text.ToString(), 500, true));
                    sbU.AppendFormat(" ,MALZEME_MINADET={0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMinAdet.Text.ToString()) ? "0" : textEditMinAdet.Text.ToString()));
                    sbU.AppendFormat(" ,MALZEME_MAXADET={0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMaxAdet.Text.ToString()) ? "0" : textEditMaxAdet.Text.ToString()));
                    sbU.AppendFormat(" ,MALZEME_RAFNO={0}", clGenelTanim.tosqlstring(textEditRafNo.Text.ToString(), 50, true));
                    sbU.AppendFormat(" ,MALZEME_TARIH={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditMalzemeTarih.DateTime).ToString("yyyy-MM-dd HH:mm") + "'");
                    sbU.AppendFormat(" ,MALZEME_OLCUBIRIMI={0}", clGenelTanim.DBToInt32(comboBoxEditOlcuBirimi.SecilenDeger().Id.ToString()));
                    sbU.AppendFormat(" ,MALZEME_PARABIRIMI={0}", clGenelTanim.DBToInt32(comboBoxEditParaBirimi.SecilenDeger().Id.ToString()));
                    sbU.AppendFormat(" ,MALZEME_SATINALMAKATEGORI={0}", clGenelTanim.DBToInt32(comboBoxEditSatinAlmaKategori.SecilenDeger().Id.ToString()));
                    sbU.AppendFormat(" ,MALZEME_STOKSAY={0}", clGenelTanim.DBToInt32(checkEditStokSay.Checked));
                    sbU.AppendFormat(" ,MALZEME_NOTU=N{0}", clGenelTanim.tosqlstring(memoEditNot.Text.ToString(), 300, true));
                    sbU.AppendFormat(" where MALZEME_ID={0}", this.currentMalzemeID);

                    clSqlTanim.RunStoredProc(sbU.ToString());

                }

                if (this.yeniMalzeme)
                {
                    this.yeniMalzeme = false;
                }

                KaydetResim();

                InitForm();

                malzemeSec(this.currentMalzemeID);

                XtraMessageBox.Show("işlem Başarılı ...");

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

        string query = "";
        SqlCommand cmd;
        SqlConnection conn;
        byte[] img;
        private void ShowImage(int malzemeId)
        {
            try
            {
                img = null;

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
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);

                if (this.picEmp.Image == null)
                {
                    query = string.Format("Update TBL_LST_MALZEMELER set MALZEME_RESIM=null where MALZEME_ID={0}", this.currentMalzemeID);
                    clSqlTanim.ExecuteNonQuery(query);

                }
                else
                {
                    conn = new SqlConnection(clSqlTanim.connectionString);
                    query = string.Format("Update TBL_LST_MALZEMELER set MALZEME_RESIM=@Image where MALZEME_ID={0}", this.currentMalzemeID);
                    if (conn.State != ConnectionState.Open) conn.Open();
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add(new SqlParameter("@Image", img));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
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
            //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            //frmGirisE.pictureEdit1.BringToFront();
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
                    MessageBox.Show("Lütfen yazdırmak için en az bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    float marginX = 3f;
                    float marginY = 3f;
                    float contentWidth = Math.Max(1f, pageWidth - (marginX * 2));
                    float contentHeight = Math.Max(1f, pageHeight - (marginY * 2));

                    float rightColumnWidth = 16f;
                    float columnGap = 2f;
                    float barcodeHeight = contentHeight * 0.45f;
                    float barcodeWidth = Math.Max(1f, contentWidth - rightColumnWidth - columnGap);

                    var barcodeWriter = new BarcodeWriter
                    {
                        Format = BarcodeFormat.CODE_128,
                        Options = new EncodingOptions
                        {
                            Height = (int)Math.Max(1f, barcodeHeight),
                            Width = (int)Math.Max(1f, barcodeWidth),
                            Margin = 0,
                            PureBarcode = true
                        }
                    };

                    if (!string.IsNullOrWhiteSpace(materyelRaw))
                    {
                        var barcodeImage = barcodeWriter.Write(materyelRaw);
                        ev.Graphics.DrawImage(barcodeImage, marginX, marginY, barcodeWidth, barcodeHeight);
                    }

                    var rafNo = clGenelTanim.DBToString(row["MALZEME RAF NO"]) ?? "";
                    string[] rafBilgileri = rafNo.Split('-');

                    string strRa = rafBilgileri.Length > 0 ? rafBilgileri[0] : "";
                    string strRow = rafBilgileri.Length > 1 ? rafBilgileri[1] : "";
                    string strCo = rafBilgileri.Length > 2 ? rafBilgileri[2] : "";

                    float rightX = marginX + barcodeWidth + columnGap;
                    float rightY = marginY + 2f;
                    float rightRowHeight = 6f;
                    ev.Graphics.DrawString("Ra:", new Font("Arial", 7.5f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX, rightY, rightColumnWidth, rightRowHeight), leftFormat);
                    ev.Graphics.DrawString(strRa, new Font("Arial", 8f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX + 6f, rightY, rightColumnWidth - 6f, rightRowHeight), leftFormat);
                    rightY += rightRowHeight;
                    ev.Graphics.DrawString("Ro:", new Font("Arial", 7.5f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX, rightY, rightColumnWidth, rightRowHeight), leftFormat);
                    ev.Graphics.DrawString(strRow, new Font("Arial", 8f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX + 6f, rightY, rightColumnWidth - 6f, rightRowHeight), leftFormat);
                    rightY += rightRowHeight;
                    ev.Graphics.DrawString("Co:", new Font("Arial", 7.5f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX, rightY, rightColumnWidth, rightRowHeight), leftFormat);
                    ev.Graphics.DrawString(strCo, new Font("Arial", 8f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(rightX + 6f, rightY, rightColumnWidth - 6f, rightRowHeight), leftFormat);

                    float currentY = marginY + barcodeHeight + 1f;

                    ev.Graphics.DrawString(materyelRaw, new Font("Arial", 8.5f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(marginX, currentY, contentWidth, 4f), centerFormat);

                    currentY += 4.5f;
                    ev.Graphics.DrawLine(Pens.Gray, marginX, currentY, marginX + contentWidth, currentY);
                    currentY += 1.5f;

                    ev.Graphics.DrawString(clGenelTanim.DBToString(row["MALZEME ADI"]),
                        new Font("Arial", 8f, FontStyle.Bold),
                        Brushes.Black, new RectangleF(marginX, currentY, contentWidth, 4f), leftFormat);
                    currentY += 4.5f;

                    ev.Graphics.DrawString(clGenelTanim.DBToString(row["MALZEME PARÇA NO"]),
                        new Font("Arial", 7f, FontStyle.Regular),
                        Brushes.Black, new RectangleF(marginX, currentY, contentWidth, 4f), leftFormat);
                    currentY += 4.5f;

                    float labelRowHeight = 4f;
                    float labelWidth = contentWidth * 0.2f;
                    float valueWidth = contentWidth * 0.25f;

                    DrawLabelAndValue(ev.Graphics, "Min:", clGenelTanim.DBToString(row["MALZEME MİN ADET"]),
                        marginX, currentY, labelWidth, valueWidth, labelRowHeight, leftFormat);
                    DrawLabelAndValue(ev.Graphics, "Max:", clGenelTanim.DBToString(row["MALZEME MAX ADET"]),
                        marginX + (contentWidth * 0.36f), currentY, labelWidth, valueWidth, labelRowHeight, leftFormat);
                    DrawLabelAndValue(ev.Graphics, "Fiyat:", clGenelTanim.DBToString(row["MALZEME GİRİŞ B.FİYAT"]),
                        marginX + (contentWidth * 0.7f), currentY, labelWidth, valueWidth, labelRowHeight, leftFormat);

                    string fiyatCinsi = clGenelTanim.DBToString(row["MALZEME GİRİŞ P.BİRİMİ"]);
                    if (!string.IsNullOrEmpty(fiyatCinsi))
                    {
                        ev.Graphics.DrawString(fiyatCinsi, new Font("Arial", 7.5f, FontStyle.Bold),
                            Brushes.Black,
                            new RectangleF(marginX + (contentWidth * 0.9f), currentY, contentWidth * 0.1f, labelRowHeight),
                            leftFormat);
                    }

                    currentIndex++;
                    ev.HasMorePages = currentIndex < selectedRows.Length;
                };

                document.Print();
            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show("Geçersiz yazıcı ayarı: " + ex.Message, "Yazıcı Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata",
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

        private void ApplyBarcodeLabelSettings(PrintDocument document)
        {
            const float mmToHundredthsInch = 100f / 25.4f;
            int widthMm = 90;
            int heightMm = 60;
            document.DefaultPageSettings.PaperSize = new PaperSize("BarcodeLabel90x60",
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
            ///* Kullanici bu butona tikladiginda, OpenFileDialog kontrolümüz, dosya açma iletisim kutusunu açar. Kullanici bir dosya seçip OK tusunda bastiginda, Picture Box kontrolümüze seçilen resim dosyasi alinarak gösterilmesi sağlanır. Daha sonra seçilen dosyanin tam adresi label kontrolümüze alınır ve resimAdresi degiskenimize atanır. */

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
                else if (e.KeyChar == (char)13) //Enter tuşuna basıldımı
                {

                    Single birimfiyat = Convert.ToSingle(textEditBirimFiyat.Text);
                    textEditBirimFiyat.Text = string.Format("{0:c}", double.Parse(textEditBirimFiyat.Text));
                    //Parabirimine dönüştür tekrar aynı text kutusuna aktar
                }
            }
            catch (Exception)
            {

                MessageBox.Show("BİRİM FİYAT GEÇERSİZ");
            }
        }
    }
}
