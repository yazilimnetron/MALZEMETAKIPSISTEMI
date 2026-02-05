using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeTalepIstatistikleri : Form
    {
        private int nMALZEMELER_TOPLAMSTOKMIKTARI = 0;

        string whereString = string.Empty;
        string whereString2 = string.Empty;

        public frmMalzemeTalepIstatistikleri()
        {
            InitializeComponent();
            layoutControlMalzemeTalepIstatiskleri.LayoutKontrolleriniSifirla();
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

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT MALZEMEDEPOISTEM_ID, MALZEME_DEPARTMANADI AS 'MALZEME TALEP DEPARTMANADI', MALZEMEDEPOISTEM_IHTIYACDEPARTMAN AS 'MALZEME TALEP IHTIYACDEPARTMAN', MALZEMEISTEM_ADI AS 'MALZEME TALEP ADI' , MALZEMEISTEM_ADET AS 'MALZEME TALEP ADET', MALZEMEISTEM_ISTEMTARIHI AS 'MALZEME TALEP TARIHI', ");
            sb.Append("CASE WHEN ISNULL(MALZEMEISTEM_DURUM,0)=0 THEN 'Onaysız' ELSE 'Onaylı' END 'MALZEME TALEP DURUM' ");
            sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
            sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM ON TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID ");
            sb.Append("JOIN TBL_LST_MALZEMEISTEMDEPARTMANLAR ON TBL_LST_MALZEMEISTEMDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());

            this.gridControlDepoHareket.DataSource = dtMalzemeler;

            this.gridViewDepoHareket.Columns["MALZEMEDEPOISTEM_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewDepoHareket.Columns["MALZEMEDEPOISTEM_ID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewDepoHareket.Columns["MALZEMEDEPOISTEM_ID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewDepoHareket.Columns["MALZEMEDEPOISTEM_ID"].Visible = false;

            this.gridViewDepoHareket.Columns["MALZEMEDEPOISTEM_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewDepoHareket, new Font("Tahoma", 10, FontStyle.Bold));


            this.gridViewDepoHareket.BestFitColumns();
        }

        public void InitForm1()
        {
            StringBuilder sb = new StringBuilder(512);

            sb.Append("SELECT s.MALZEME_ID, case when s.MALZEME_TURU=1 then 'NLAG'  when s.MALZEME_TURU=2 then 'UNBW' else '<<Seçiniz>>' end 'MALZEME TURU', convert(nvarchar,s.MALZEME_MATERYAL) AS 'MALZEME MATERYEL', s.MALZEME_PARCANO AS 'MALZEME PARÇANO', s.MALZEME_ADI AS 'MALZEME ADI', ");
            sb.Append("ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) AS 'MALZEME BİRİM FİYAT', ");
            sb.Append("ISNULL(GirislerToplam,0) AS 'MALZEME GİRİŞ ADET', ISNULL(CikislarToplam,0) AS 'MALZEME ÇIKIŞ ADET', ");
            sb.Append("(ISNULL(GirislerToplam,0)*ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0)) AS 'MALZEME GİRİŞ TOPLAM FİYAT', ");
            sb.Append("(ISNULL(CikislarToplam,0))*ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) AS 'MALZEME ÇIKIŞ TOPLAM FİYAT', ");
            sb.Append("mg.MALZEMEGIRIS_TARIH AS 'MALZEME GİRİŞ TARİH', mc.MALZEMECIKIS_TARIHI AS 'MALZEME ÇIKIŞ TARİH', ");
            sb.Append("s.MALZEME_RAFNO AS 'MALZEME RAFNO' ");
            sb.Append("FROM TBL_LST_MALZEMELER s ");
            sb.Append("JOIN (SELECT MALZEMEGIRIS_MALZEMELERID, MALZEMEGIRIS_SORGUBIRIMFIYAT, MALZEMEGIRIS_PARABIRIMI, MALZEMEGIRIS_BIRIMFIYAT, MALZEMEGIRIS_TARIH, ROW_NUMBER() OVER (PARTITION BY MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC) AS rng FROM TBL_LST_MALZEMEGIRIS WITH (NOLOCK)) mg ON mg.MALZEMEGIRIS_MALZEMELERID=s.MALZEME_ID ");
            sb.Append("JOIN (SELECT MALZEMECIKIS_MALZEMELERID, MALZEMECIKIS_SORGUBIRIMFIYAT, MALZEMECIKIS_PARABIRIMI, MALZEMECIKIS_BIRIMFIYAT, MALZEMECIKIS_TARIHI, ROW_NUMBER() OVER (PARTITION BY MALZEMECIKIS_MALZEMELERID ORDER BY MALZEMECIKIS_TARIHI DESC) AS rnc FROM TBL_LST_MALZEMECIKIS WITH (NOLOCK)) mc ON mc.MALZEMECIKIS_MALZEMELERID=s.MALZEME_ID ");
            sb.Append("JOIN (Select  MALZEMEGIRIS_MALZEMELERID, isnull(Sum(MALZEMEGIRIS_ADET),0) GirislerToplam FROM TBL_LST_MALZEMEGIRIS ");
            sb.AppendFormat("WHERE CONVERT(VARCHAR(10),MALZEMEGIRIS_TARIH ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.AppendFormat("AND CONVERT(VARCHAR(10),MALZEMEGIRIS_TARIH ,121)<={0} ", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.Append("GROUP BY  MALZEMEGIRIS_MALZEMELERID) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("JOIN (Select  MALZEMECIKIS_MALZEMELERID, isnull(Sum(MALZEMECIKIS_ADET),0) CikislarToplam FROM TBL_LST_MALZEMECIKIS ");
            sb.AppendFormat("WHERE CONVERT(VARCHAR(10),MALZEMECIKIS_TARIHI ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.AppendFormat("AND CONVERT(VARCHAR(10),MALZEMECIKIS_TARIHI ,121)<={0} ", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.Append("GROUP BY  MALZEMECIKIS_MALZEMELERID) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("WHERE rng=1 and rnc=1 ORDER BY 1 DESC");


            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            this.gridControlGirisHareket.DataSource = dtMalzemeler;

            this.gridViewGirisHareket.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewGirisHareket.Columns["MALZEME GİRİŞ ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewGirisHareket.Columns["MALZEME GİRİŞ ADET"].SummaryItem.DisplayFormat = "{0} Adet";

            this.gridViewGirisHareket.Columns["MALZEME ÇIKIŞ ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewGirisHareket.Columns["MALZEME ÇIKIŞ ADET"].SummaryItem.DisplayFormat = "{0} Adet";


            this.gridViewGirisHareket.Columns["MALZEME_ID"].Visible = false;

            this.gridViewGirisHareket.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewGirisHareket, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewGirisHareket.Columns["MALZEME ADI"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewGirisHareket.Columns["MALZEME ADI"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewGirisHareket.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewGirisHareket.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewGirisHareket.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewGirisHareket.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewGirisHareket.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewGirisHareket.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewGirisHareket.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewGirisHareket.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            this.gridViewGirisHareket.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].SummaryItem.DisplayFormat = "G.TOPLAM FİYAT: {0:c2}";

            this.gridViewGirisHareket.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewGirisHareket.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            this.gridViewGirisHareket.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.DisplayFormat = "Ç.TOPLAM FİYAT: {0:c2}";


            this.gridViewGirisHareket.BestFitColumns();
        }

        public void InitForm2()
        {
            //StringBuilder sb = new StringBuilder(512);
            //sb.Append("SELECT a.MALZEMECIKIS_ID, a.MALZEMECIKIS_MALZEMELERID, convert(nvarchar,b.MALZEME_MATERYAL) AS 'MALZEME MATERYAL NO', b.MALZEME_PARCANO AS 'MALZEME PARÇA NO', ");
            //sb.Append("a.MALZEMECIKIS_ADI AS 'MALZEME ADI', ISNULL(a.MALZEMECIKIS_ADET,0) AS 'MALZEME ÇIKIŞ ADET', ");
            //sb.Append("ISNULL(a.MALZEMECIKIS_SORGUBIRIMFIYAT,0) AS 'MALZEME ÇIKIŞ BİRİM FİYAT', ISNULL(a.MALZEMECIKIS_BIRIMFIYAT,0) AS 'MALZEME ÇIKIŞ BİRİM FİYAT1', ISNULL(a.MALZEMECIKIS_SORGUTOPLAMFIYAT,0) AS 'MALZEME ÇIKIŞ TOPLAM FİYAT', ");
            //sb.Append("a.MALZEMECIKIS_TARIHI AS 'MALZEME ÇIKIŞ TARİH', c.MALZEMEGRUP_ADI AS 'MALZEME ANA GRUBU', b.MALZEME_GRUBU AS 'MALZEME GRUBU' ");
            //sb.Append("FROM TBL_LST_MALZEMELER (nolock)  b, TBL_LST_MALZEMECIKIS (nolock)  a , TBL_LST_MALZEMEGRUPLAR (nolock) c ");
            //sb.AppendFormat("WHERE b.MALZEME_ID=a.MALZEMECIKIS_MALZEMELERID and b.MALZEME_ANAGRUBU= c.MALZEMEGRUP_ID and CONVERT(VARCHAR(10),a.MALZEMECIKIS_TARIHI ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            //sb.AppendFormat(" AND CONVERT(VARCHAR(10),a.MALZEMECIKIS_TARIHI ,121)<={0} ", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            ////if (checkedComboBoxEditGrup.Properties.GetItems().GetCheckedValues().Count > 0) sb.AppendFormat(" AND b.MALZEME_ANAGRUBU in ({0}) ", whereString.ToString());

            //DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            //gridControlCikisHareket.DataSource = dtMalzemeler;

            //this.gridViewCikisHareket.Columns["MALZEMECIKIS_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            //this.gridViewCikisHareket.Columns["MALZEME ADI"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //this.gridViewCikisHareket.Columns["MALZEME ADI"].SummaryItem.DisplayFormat = "{0} Kayıt";

            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.DisplayFormat = "TOPLAM FİYAT: {0:c2}";

            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ ADET"].SummaryItem.DisplayFormat = "{0} Adet";

            //this.gridViewCikisHareket.Columns["MALZEMECIKIS_ID"].Visible = false;
            //this.gridViewCikisHareket.Columns["MALZEMECIKIS_MALZEMELERID"].Visible = false;
            //this.gridViewCikisHareket.Columns["MALZEME ÇIKIŞ BİRİM FİYAT1"].Visible = false;

            //this.gridViewCikisHareket.Columns["MALZEMECIKIS_ID"].OptionsColumn.ShowInCustomizationForm = false;
            //this.gridViewCikisHareket.Columns["MALZEMECIKIS_MALZEMELERID"].OptionsColumn.ShowInCustomizationForm = false;

            //this.SetGridFont(gridViewCikisHareket, new Font("Tahoma", 10, FontStyle.Bold));

            //this.gridViewCikisHareket.BestFitColumns();
        }

        public void InitForm3()
        {
            //StringBuilder sb = new StringBuilder(512);
            //sb.Append("SELECT a.MALZEME_ID, a.MALZEME_OUDBTNO AS 'MALZEME OUDBTNO', case when a.MALZEME_TURU=1 then 'NLAG'  when a.MALZEME_TURU=2 then 'UNBW' else '<<Seçiniz>>' end 'MALZEME TURU', convert(nvarchar,a.MALZEME_MATERYAL) AS 'MALZEME MATERYEL', ");
            //sb.Append("a.MALZEME_PARCANO AS 'MALZEME PARÇANO', a.MALZEME_ADI AS 'MALZEME ADI', ISNULL(a.MALZEME_STOKMIKTARI,0) AS 'MALZEME STOK ADET', ISNULL(a.MALZEME_MINADET,0) AS 'MALZEME MİN ADET', ");
            //sb.Append("ISNULL(a.MALZEME_MAXADET,0) AS 'MALZEME MAX ADET', ISNULL(b.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) AS 'MALZEME BİRİM FİYAT', ISNULL(b.MALZEMEGIRIS_SORGUBIRIMFIYAT,0)*ISNULL(a.MALZEME_STOKMIKTARI,0) AS 'MALZEME TOPLAM FİYAT', ");
            //sb.Append("a.MALZEME_RAFNO AS 'MALZEME RAFNO', d.MALZEMEGRUP_ADI AS 'MALZEME ANA GRUBU', a.MALZEME_GRUBU AS 'MALZEME GRUBU' ");
            //sb.Append("FROM TBL_LST_MALZEMELER a ");
            //sb.Append("JOIN TBL_LST_MALZEMEGRUPLAR d on a.MALZEME_ANAGRUBU=d.MALZEMEGRUP_ID ");
            //sb.Append("JOIN (SELECT  MALZEMEGIRIS_MALZEMELERID, MALZEMEGIRIS_SORGUBIRIMFIYAT ");
            //sb.Append("FROM TBL_LST_MALZEMEGIRIS (NOLOCK) WHERE MALZEMEGIRIS_ID IN (select MAX(MALZEMEGIRIS_ID) from TBL_LST_MALZEMEGIRIS GROUP BY MALZEMEGIRIS_MALZEMELERID) GROUP BY MALZEMEGIRIS_MALZEMELERID, MALZEMEGIRIS_SORGUBIRIMFIYAT ) b on a.MALZEME_ID=b.MALZEMEGIRIS_MALZEMELERID ");
            //if (checkedComboBoxEditGrup.Properties.GetItems().GetCheckedValues().Count > 0) sb.AppendFormat(" AND a.MALZEME_ANAGRUBU in ({0}) ", whereString.ToString());

            //DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            //gridControlSorgulama.DataSource = dtMalzemeler;

            //this.gridViewSorgulama.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            //this.gridViewSorgulama.Columns["MALZEME ADI"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //this.gridViewSorgulama.Columns["MALZEME ADI"].SummaryItem.DisplayFormat = "{0} Kayıt";

            //this.gridViewSorgulama.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //this.gridViewSorgulama.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewSorgulama.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            //this.gridViewSorgulama.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //this.gridViewSorgulama.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewSorgulama.Columns["MALZEME TOPLAM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            //this.gridViewSorgulama.Columns["MALZEME STOK ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //this.gridViewSorgulama.Columns["MALZEME STOK ADET"].SummaryItem.DisplayFormat = "{0} Adet";


            //this.gridViewSorgulama.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //this.gridViewSorgulama.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewSorgulama.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.DisplayFormat = "Toplam Fiyat : {0:c2}";

            //this.gridViewSorgulama.Columns["MALZEME_ID"].Visible = false;
            //this.gridViewSorgulama.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;


            //this.gridViewSorgulama.Columns["MALZEME OUDBTNO"].Visible = false;

            //this.SetGridFont(gridViewSorgulama, new Font("Tahoma", 10, FontStyle.Bold));

            //this.gridViewSorgulama.BestFitColumns();
        }

        public void InitForm4()
        {
            //StringBuilder sb = new StringBuilder(512);
            //sb.Append("select a.MALZEME_ID, a.MALZEME_ADI as 'MALZEME ADI', a.MALZEME_OUDBTNO AS 'MALZEME OUDBTNO', a.MALZEME_PARCANO as 'MALZEME PARÇA NO', convert(nvarchar,a.MALZEME_MATERYAL) as 'MALZEME MATERYAL NO' , d.MALZEMEGRUP_ADI AS 'MALZEME ANA GRUBU', a.MALZEME_GRUBU as 'MALZEME GRUBU', isnull(e.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) AS 'MALZEME BİRİM FİYAT', isnull(b.MALZEMEGIRIS_ADET,0) as 'MALZEME GİRİŞ ADET', isnull(e.MALZEMEGIRIS_SORGUBIRIMFIYAT,0)*isnull(b.MALZEMEGIRIS_ADET,0) AS 'MALZEME GİRİŞ TOPLAM FİYAT', isnull(c.MALZEMECIKIS_ADET,0) as 'MALZEME ÇIKIŞ ADET', isnull(e.MALZEMEGIRIS_SORGUBIRIMFIYAT,0)*isnull(c.MALZEMECIKIS_ADET,0) AS 'MALZEME ÇIKIŞ TOPLAM FİYAT' ");
            //sb.Append("from TBL_LST_MALZEMELER a (nolock) ");
            //sb.Append("join TBL_LST_MALZEMEGRUPLAR d on a.MALZEME_ANAGRUBU=d.MALZEMEGRUP_ID ");
            //if (checkedComboBoxEditGrup.Properties.GetItems().GetCheckedValues().Count > 0) sb.AppendFormat(" AND a.MALZEME_ANAGRUBU in ({0}) ", whereString.ToString());
            //sb.Append("left join (SELECT  MALZEMEGIRIS_MALZEMELERID, MALZEMEGIRIS_SORGUBIRIMFIYAT ");
            //sb.Append("FROM TBL_LST_MALZEMEGIRIS (NOLOCK) WHERE MALZEMEGIRIS_ID IN (select MAX(MALZEMEGIRIS_ID) from TBL_LST_MALZEMEGIRIS GROUP BY MALZEMEGIRIS_MALZEMELERID) GROUP BY MALZEMEGIRIS_MALZEMELERID, MALZEMEGIRIS_SORGUBIRIMFIYAT ) e on a.MALZEME_ID=e.MALZEMEGIRIS_MALZEMELERID ");
            //sb.Append("left join (select MALZEMEGIRIS_MALZEMELERID, sum(isnull(MALZEMEGIRIS_ADET,0)) MALZEMEGIRIS_ADET  from TBL_LST_MALZEMEGIRIS (nolock) ");
            //sb.AppendFormat("where CONVERT(VARCHAR(10),MALZEMEGIRIS_TARIH ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            //sb.AppendFormat("AND CONVERT(VARCHAR(10),MALZEMEGIRIS_TARIH ,121)<={0}", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            //sb.Append(" group by MALZEMEGIRIS_MALZEMELERID ) b on a.MALZEME_ID=b.MALZEMEGIRIS_MALZEMELERID ");
            //sb.Append("left join (select MALZEMECIKIS_MALZEMELERID, sum(isnull(MALZEMECIKIS_ADET,0)) MALZEMECIKIS_ADET  from TBL_LST_MALZEMECIKIS (nolock) ");
            //sb.AppendFormat("where CONVERT(VARCHAR(10),MALZEMECIKIS_TARIHI ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            //sb.AppendFormat("AND CONVERT(VARCHAR(10),MALZEMECIKIS_TARIHI ,121)<={0}", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            //sb.Append("group by MALZEMECIKIS_MALZEMELERID ) c on a.MALZEME_ID=c.MALZEMECIKIS_MALZEMELERID ");
            //sb.Append("group by a.MALZEME_ID, a.MALZEME_ADI, a.MALZEME_OUDBTNO, a.MALZEME_PARCANO, a.MALZEME_MATERYAL, a.MALZEME_GRUBU, b.MALZEMEGIRIS_ADET, c.MALZEMECIKIS_ADET, d.MALZEMEGRUP_ADI, e.MALZEMEGIRIS_SORGUBIRIMFIYAT, a.MALZEME_STOKMIKTARI ");

            //DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            //gridControlMalzemeKullanim.DataSource = dtMalzemeler;

            //this.gridViewMalzemeKullanim.Columns["MALZEME ADI"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //this.gridViewMalzemeKullanim.Columns["MALZEME ADI"].SummaryItem.DisplayFormat = "{0} Kayıt";

            //this.gridViewMalzemeKullanim.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //this.gridViewMalzemeKullanim.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewMalzemeKullanim.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            //this.gridViewMalzemeKullanim.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //this.gridViewMalzemeKullanim.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewMalzemeKullanim.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            //this.gridViewMalzemeKullanim.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //this.gridViewMalzemeKullanim.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewMalzemeKullanim.Columns["MALZEME GİRİŞ TOPLAM FİYAT"].SummaryItem.DisplayFormat = "Toplam Fiyat : {0:c2} ";

            //this.gridViewMalzemeKullanim.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //this.gridViewMalzemeKullanim.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewMalzemeKullanim.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.DisplayFormat = "Toplam Fiyat : {0:c2}";

            //this.gridViewMalzemeKullanim.Columns["MALZEME_ID"].Visible = false;
            //this.gridViewMalzemeKullanim.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;

            //this.gridViewMalzemeKullanim.Columns["MALZEME OUDBTNO"].Visible = false;

            //this.SetGridFont(gridViewMalzemeKullanim, new Font("Tahoma", 10, FontStyle.Bold));

            //this.gridViewMalzemeKullanim.BestFitColumns();
        }

        public void InitForm5()
        {
            //StringBuilder sb = new StringBuilder(512);
            //sb.Append("select a.MALZEME_ID, a.MALZEME_OUDBTNO as 'MALZEME OUDBTNO', a.MALZEME_GRUBU as 'MALZEME GRUBU' , convert(nvarchar,a.MALZEME_MATERYAL) as 'MALZEME MATERYAL NO', a.MALZEME_PARCANO as 'MALZEME PARÇA NO', a.MALZEME_ADI as 'MALZEME ADI', a.MALZEME_STOKMIKTARI as 'MALZEME STOK MİKTARI', b.MALZEMEGIRIS_TARIH as 'MALZEME GİRİŞ TARİHİ', isnull(b.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) as 'MALZEME BİRİM FİYAT' , isnull(b.MALZEMEGIRIS_SORGUTOPLAMFIYAT,0) as 'MALZEME TOPLAM FİYAT' ");
            //sb.Append("from TBL_LST_MALZEMELER a (nolock) ");
            //sb.Append("left join TBL_LST_MALZEMEGIRIS (nolock) b on a.MALZEME_ID=b.MALZEMEGIRIS_MALZEMELERID ");
            //if (checkedComboBoxEditGrup.Properties.GetItems().GetCheckedValues().Count > 0) sb.AppendFormat(" AND a.MALZEME_ANAGRUBU in ({0}) ", whereString.ToString());
            //sb.Append("left join TBL_LST_MALZEMECIKIS (nolock) c on a.MALZEME_ID=c.MALZEMECIKIS_MALZEMELERID ");
            //sb.AppendFormat("where (b.MALZEMEGIRIS_ID is null or c.MALZEMECIKIS_ID is null) and CONVERT(VARCHAR(10),b.MALZEMEGIRIS_TARIH ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            //sb.AppendFormat(" AND CONVERT(VARCHAR(10),b.MALZEMEGIRIS_TARIH ,121)<={0}", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");

            //DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            //gridControlMalzemeHareket.DataSource = dtMalzemeler;

            //this.gridViewMalzemeHareket.Columns["MALZEME ADI"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //this.gridViewMalzemeHareket.Columns["MALZEME ADI"].SummaryItem.DisplayFormat = "{0} Kayıt";

            //this.gridViewMalzemeHareket.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //this.gridViewMalzemeHareket.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewMalzemeHareket.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            //this.gridViewMalzemeHareket.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //this.gridViewMalzemeHareket.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewMalzemeHareket.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.DisplayFormat = "Toplam Fiyat : {0:c2}";

            //this.gridViewMalzemeHareket.Columns["MALZEME_ID"].Visible = false;
            //this.gridViewMalzemeHareket.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;

            //this.gridViewMalzemeHareket.Columns["MALZEME OUDBTNO"].Visible = false;

            //this.SetGridFont(gridViewMalzemeHareket, new Font("Tahoma", 10, FontStyle.Bold));

            //this.gridViewMalzemeHareket.BestFitColumns();
        }

        public void InitForm6()
        {
            //StringBuilder sb = new StringBuilder(512);
            //sb.Append("select a.MALZEME_ID, a.MALZEME_OUDBTNO as 'MALZEME OUDBTNO', a.MALZEME_GRUBU as 'MALZEME GRUBU' , convert(nvarchar,a.MALZEME_MATERYAL) as 'MALZEME MATERYAL NO', a.MALZEME_PARCANO as 'MALZEME PARÇA NO', a.MALZEME_ADI as 'MALZEME ADI', a.MALZEME_STOKMIKTARI as 'MALZEME STOK MİKTARI', b.MALZEMEGIRIS_TARIH as 'MALZEME GİRİŞ TARİHİ', isnull(b.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) as 'MALZEME BİRİM FİYAT' , isnull(b.MALZEMEGIRIS_SORGUTOPLAMFIYAT,0) as 'MALZEME TOPLAM FİYAT' ");
            //sb.Append("from TBL_LST_MALZEMELER a (nolock) ");
            //sb.Append("left join TBL_LST_MALZEMEGIRIS (nolock) b on a.MALZEME_ID=b.MALZEMEGIRIS_MALZEMELERID ");
            //if (checkedComboBoxEditGrup.Properties.GetItems().GetCheckedValues().Count > 0) sb.AppendFormat(" AND a.MALZEME_ANAGRUBU in ({0}) ", whereString.ToString());
            //sb.Append("left join TBL_LST_MALZEMECIKIS (nolock) c on a.MALZEME_ID=c.MALZEMECIKIS_MALZEMELERID ");
            //sb.AppendFormat("where (b.MALZEMEGIRIS_ID is not null or c.MALZEMECIKIS_ID is not null) and CONVERT(VARCHAR(10),b.MALZEMEGIRIS_TARIH ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            //sb.AppendFormat(" AND CONVERT(VARCHAR(10),b.MALZEMEGIRIS_TARIH ,121)<={0}", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");

            //DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            //gridControlMalzemeHareket2.DataSource = dtMalzemeler;

            //this.gridViewMalzemeHareket2.Columns["MALZEME ADI"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //this.gridViewMalzemeHareket2.Columns["MALZEME ADI"].SummaryItem.DisplayFormat = "{0} Kayıt";

            //this.gridViewMalzemeHareket2.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //this.gridViewMalzemeHareket2.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewMalzemeHareket2.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            //this.gridViewMalzemeHareket2.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //this.gridViewMalzemeHareket2.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewMalzemeHareket2.Columns["MALZEME TOPLAM FİYAT"].SummaryItem.DisplayFormat = "Toplam Fiyat : {0:c2}";

            //this.gridViewMalzemeHareket2.Columns["MALZEME_ID"].Visible = false;
            //this.gridViewMalzemeHareket2.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;

            //this.gridViewMalzemeHareket2.Columns["MALZEME OUDBTNO"].Visible = false;

            //this.SetGridFont(gridViewMalzemeHareket2, new Font("Tahoma", 10, FontStyle.Bold));

            //this.gridViewMalzemeHareket2.BestFitColumns();
        }

        public void InitForm7()
        {
            //StringBuilder sb = new StringBuilder(512);
            //sb.Append("SELECT '' AS 'SİPARİŞ NO', c.MALZEME_DEPARTMANADI AS 'MASRAF YERİ', '' AS 'SİPARİŞ DURUM', '' AS 'SİPARİŞ TARİHİ', '' AS 'ONAY TARİHİ', '' AS 'TESLİM TARİHİ', '' AS 'TESLİM ALAN', ");
            //sb.Append("convert(nvarchar,b.MALZEME_MATERYAL) AS 'MALZEME MATERYAL NO', a.MALZEMECIKIS_ADI AS 'MALZEME ADI',a.MALZEMECIKIS_ADET AS 'MALZEME ÇIKIŞ ADET', ");
            //sb.Append("a.MALZEMECIKIS_SORGUBIRIMFIYAT AS 'MALZEME ÇIKIŞ BİRİM FİYAT',a.MALZEMECIKIS_SORGUTOPLAMFIYAT AS 'MALZEME ÇIKIŞ TOPLAM FİYAT',a.MALZEMECIKIS_TARIHI AS 'MALZEME ÇIKIŞ TARİH' ");
            //sb.Append("FROM TBL_LST_MALZEMELER (nolock)  b, TBL_LST_MALZEMECIKIS (nolock)  a , TBL_LST_MALZEMEDEPARTMANLAR (nolock) c  ");
            //sb.Append("WHERE b.MALZEME_ID=a.MALZEMECIKIS_MALZEMELERID AND a.MALZEMECIKIS_DEPARTMANID=c.MALZEME_DEPARTMANID ");
            //if (checkedComboBoxEditDepartman.Properties.GetItems().GetCheckedValues().Count > 0) sb.AppendFormat(" AND a.MALZEMECIKIS_DEPARTMANID in ({0}) ", whereString2.ToString());
            //sb.AppendFormat(" AND CONVERT(VARCHAR(10),a.MALZEMECIKIS_TARIHI ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            //sb.AppendFormat(" AND CONVERT(VARCHAR(10),a.MALZEMECIKIS_TARIHI ,121)<={0}", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            //sb.AppendLine("");
            //sb.AppendLine("union all");
            //sb.Append("SELECT MALZEMEDEPOISTEM_ID AS 'SİPARİŞ NO', e.MALZEME_DEPARTMANADI AS 'MASRAF YERİ', Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylandı' ");
            //sb.Append("when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM',MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ', MALZEMEDEPOISTEM_ONAYTARIHI AS 'ONAY TARİHİ', ");
            //sb.Append("MALZEMEDEPOISTEM_TESLIMTARIHI AS 'TESLİM TARİHİ', d.MALZEMEKULLANICI_ADI AS 'TESLİM ALAN',convert(nvarchar,MALZEMEISTEM_MATERYALNO) AS 'MALZEME MATERYAL NO', ");
            //sb.Append("MALZEMEISTEM_ADI 'MALZEME ADI', MALZEMECIKIS_ADET AS 'MALZEME ÇIKIŞ ADET', MALZEMECIKIS_SORGUBIRIMFIYAT AS 'MALZEME ÇIKIŞ BİRİM FİYAT', MALZEMECIKIS_SORGUTOPLAMFIYAT 'MALZEME ÇIKIŞ TOPLAM FİYAT', c.MALZEMECIKIS_TARIHI AS 'MALZEME ÇIKIŞ TARİH' ");
            //sb.Append("FROM TBL_LST_MALZEMEDEPOISTEM a (NOLOCK)");
            //sb.Append("JOIN TBL_LST_MALZEMEISTEM b (nolock) on a.MALZEMEDEPOISTEM_ID = b.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
            //sb.Append("JOIN TBL_LST_MALZEMECIKIS c (nolock) on b.MALZEMEISTEM_MALZEMELERID = c.MALZEMECIKIS_MALZEMELERID AND b.MALZEMEISTEM_MALZEMEDEPOISTEMID= c.MALZEMECIKIS_MALZEMEDEPOISTEM_ID ");
            //sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR d (nolock) on d.MALZEMEKULLANICI_ID = a.MALZEMEDEPOISTEM_TESLIMKULLANICIID ");
            //sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR e (nolock) on e.MALZEME_DEPARTMANID=c.MALZEMECIKIS_DEPARTMANID ");
            //if (checkedComboBoxEditDepartman.Properties.GetItems().GetCheckedValues().Count > 0) sb.AppendFormat(" AND c.MALZEMECIKIS_DEPARTMANID in ({0}) ", whereString2.ToString());
            //sb.AppendFormat(" AND CONVERT(VARCHAR(10),c.MALZEMECIKIS_TARIHI ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            //sb.AppendFormat(" AND CONVERT(VARCHAR(10),c.MALZEMECIKIS_TARIHI ,121)<={0}", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");

            //DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            //gridControlCikisHareket2.DataSource = dtMalzemeler;

            //this.gridViewCikisHareket2.Columns["MALZEME ADI"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //this.gridViewCikisHareket2.Columns["MALZEME ADI"].SummaryItem.DisplayFormat = "{0} Kayıt";

            //this.gridViewCikisHareket2.Columns["MALZEME ÇIKIŞ BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //this.gridViewCikisHareket2.Columns["MALZEME ÇIKIŞ BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewCikisHareket2.Columns["MALZEME ÇIKIŞ BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            //this.gridViewCikisHareket2.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //this.gridViewCikisHareket2.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            //this.gridViewCikisHareket2.Columns["MALZEME ÇIKIŞ TOPLAM FİYAT"].SummaryItem.DisplayFormat = "Toplam Fiyat : {0:c2}";


            //this.SetGridFont(gridViewCikisHareket2, new Font("Tahoma", 10, FontStyle.Bold));

            //this.gridViewCikisHareket2.BestFitColumns();
        }

        private void barButtonItemYazdir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");

            DevExpress.XtraBars.BarButtonItem bi = (DevExpress.XtraBars.BarButtonItem)e.Item;
            string uzanti = string.Empty;

            if (bi == barButtonItemYazdir)
            {
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageDepoHareket)
                {
                    this.gridControlDepoHareket.ShowPrintPreview();
                }

                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageGirisHareketleri)
                {
                    this.gridControlGirisHareket.ShowPrintPreview();
                }

                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri)
                {
                    this.gridControlCikisHareket.ShowPrintPreview();
                }

                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageSorgulama)
                {
                    this.gridControlSorgulama.ShowPrintPreview();
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeKullanim)
                {
                    this.gridControlMalzemeKullanim.ShowPrintPreview();
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeHareket)
                {
                    this.gridControlMalzemeHareket.ShowPrintPreview();
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeHareket2)
                {
                    this.gridControlMalzemeHareket2.ShowPrintPreview();
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri2)
                {
                    this.gridControlCikisHareket2.ShowPrintPreview();
                }

            }
            else if (bi == barButtonItemExcel)
            {
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageDepoHareket)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlDepoHareket, saveFileDialogMalzemeIstatiskleri, "xls");
                }

                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageGirisHareketleri)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlGirisHareket, saveFileDialogMalzemeIstatiskleri, "xls");
                }

                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlCikisHareket, saveFileDialogMalzemeIstatiskleri, "xls");
                }

                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageSorgulama)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlSorgulama, saveFileDialogMalzemeIstatiskleri, "xls");
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeKullanim)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlMalzemeKullanim, saveFileDialogMalzemeIstatiskleri, "xls");
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeHareket)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlMalzemeHareket, saveFileDialogMalzemeIstatiskleri, "xls");
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeHareket2)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlMalzemeHareket2, saveFileDialogMalzemeIstatiskleri, "xls");
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri2)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlCikisHareket2, saveFileDialogMalzemeIstatiskleri, "xls");
                }
            }
            else if (bi == barButtonItemPdf)
            {
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageDepoHareket)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlDepoHareket, saveFileDialogMalzemeIstatiskleri, "pdf");
                }

                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageGirisHareketleri)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlGirisHareket, saveFileDialogMalzemeIstatiskleri, "pdf");
                }

                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlCikisHareket, saveFileDialogMalzemeIstatiskleri, "pdf");
                }

                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageSorgulama)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlSorgulama, saveFileDialogMalzemeIstatiskleri, "pdf");
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeKullanim)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlMalzemeKullanim, saveFileDialogMalzemeIstatiskleri, "pdf");
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeHareket)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlMalzemeHareket, saveFileDialogMalzemeIstatiskleri, "pdf");
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeHareket2)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlMalzemeHareket2, saveFileDialogMalzemeIstatiskleri, "pdf");
                }
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri2)
                {
                    clGenelTanim.OpenSaveDlg(this.gridControlCikisHareket2, saveFileDialogMalzemeIstatiskleri, "pdf");
                }
            }
        }

        private void barButtonItemKapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            frmGirisE.pictureEdit1.BringToFront();

            this.Close();
        }

        private void barButtonItemListele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            whereString = string.Empty;
            whereString2 = string.Empty;

            //foreach (CheckedListBoxItem items in checkedComboBoxEditGrup.Properties.Items)
            //{
            //    if (items.CheckState == CheckState.Checked)
            //    {
            //        whereString += String.Format(items.Value.ToString()) + ",";
            //    }
            //}

            //if (whereString != string.Empty)
            //{
            //    whereString = whereString.Substring(0, whereString.Length - 1);
            //}

            //foreach (CheckedListBoxItem items in checkedComboBoxEditDepartman.Properties.Items)
            //{
            //    if (items.CheckState == CheckState.Checked)
            //    {
            //        whereString2 += String.Format(items.Value.ToString()) + ",";
            //    }
            //}

            //if (whereString2 != string.Empty)
            //{
            //    whereString2 = whereString2.Substring(0, whereString2.Length - 1);
            //}

            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageDepoHareket)
            {
                InitForm();
            }

            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageGirisHareketleri)
            {
                InitForm1();
                SetContainsFilter(gridViewGirisHareket);
            }

            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri)
            {
                InitForm2();
                SetContainsFilter(gridViewCikisHareket);
            }

            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageSorgulama)
            {
                InitForm3();
                SetContainsFilter(gridViewSorgulama);
            }

            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeKullanim)
            {
                InitForm4();
                SetContainsFilter(gridViewMalzemeKullanim);
            }
            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeHareket)
            {
                InitForm5();
                SetContainsFilter(gridViewMalzemeHareket);
            }
            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeHareket2)
            {
                InitForm6();
                SetContainsFilter(gridViewMalzemeHareket2);
            }
            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri2)
            {
                InitForm7();
                SetContainsFilter(gridViewCikisHareket2);
            }

        }

        private void frmMalzemeTalepIstatiskleri_Load(object sender, EventArgs e)
        {
            //var controller = new MyController();
            //checkedComboBoxEditGrup.Properties.DataSource = controller.GetItems();
            //checkedComboBoxEditGrup.Properties.ValueMember = "MALZEMEGRUP_ID";
            //checkedComboBoxEditGrup.Properties.DisplayMember = "MALZEMEGRUP_ADI";

            //var controller2 = new MyController2();
            //checkedComboBoxEditDepartman.Properties.DataSource = controller2.GetItems();
            //checkedComboBoxEditDepartman.Properties.ValueMember = "MALZEME_DEPARTMANID";
            //checkedComboBoxEditDepartman.Properties.DisplayMember = "MALZEME_DEPARTMANADI";
        }

        private void frmMalzemeTalepIstatiskleri_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            //frmGirisE.pictureEdit1.BringToFront();
        }

        private void barButtonItemGrafik_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmGrafik frmGraf = new frmGrafik();
            //frmGraf.Show();
        }

        private void comboBoxEditSorgulamaAnaGrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageSorgulama)
            {
                InitForm3();
            }

            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeKullanim)
            {
                InitForm4();
            }

            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri)
            {
                InitForm2();
            }

            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageGirisHareketleri)
            {
                InitForm1();
            }

            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeHareket)
            {
                InitForm5();
            }
            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageMalzemeHareket2)
            {
                InitForm6();
            }
            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri2)
            {
                InitForm7();
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sbD = new StringBuilder(512);
            StringBuilder sbU = new StringBuilder(512);

            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageGirisHareketleri)
            {
                var item = gridViewGirisHareket.GetFocusedDataRow();
                if (item != null)
                {
                    sbD.Append("DELETE FROM TBL_LST_MALZEMEGIRIS ");
                    sbD.AppendFormat("WHERE MALZEMEGIRIS_ID={0}", clGenelTanim.DBToInt32(item["MALZEMEGIRIS_ID"]));
                    clSqlTanim.RunStoredProc(sbD.ToString());

                    sbU.Append("update TBL_LST_MALZEMELER set ");
                    sbU.AppendFormat(" MALZEME_STOKMIKTARI=MALZEME_STOKMIKTARI - {0} ", clGenelTanim.DBToInt32(item["MALZEME GİRİŞ ADET"]));
                    sbU.AppendFormat(" where MALZEME_ID={0}", clGenelTanim.DBToInt32(item["MALZEMEGIRIS_MALZEMELERID"]).ToString());

                    clSqlTanim.RunStoredProc(sbU.ToString());

                    InitForm1();
                }
            }

            if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri)
            {
                var item = gridViewCikisHareket.GetFocusedDataRow();
                if (item != null)
                {
                    sbD.Append("DELETE FROM TBL_LST_MALZEMECIKIS ");
                    sbD.AppendFormat("WHERE MALZEMECIKIS_ID={0}", clGenelTanim.DBToInt32(item["MALZEMECIKIS_ID"]));
                    clSqlTanim.RunStoredProc(sbD.ToString());

                    sbU.Append("update TBL_LST_MALZEMELER set ");
                    sbU.AppendFormat(" MALZEME_STOKMIKTARI=MALZEME_STOKMIKTARI + {0} ", clGenelTanim.DBToInt32(item["MALZEME ÇIKIŞ ADET"]));
                    sbU.AppendFormat(" where MALZEME_ID={0}", clGenelTanim.DBToInt32(item["MALZEMECIKIS_MALZEMELERID"]).ToString());

                    clSqlTanim.RunStoredProc(sbU.ToString());

                    InitForm2();
                }
            }
        }

        private void miktarDüzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sbD = new StringBuilder(512);
            StringBuilder sbU = new StringBuilder(512);

            frmAdetDuzenle frmAdet = new frmAdetDuzenle();
            frmAdet.ShowDialog();

            if (frmAdet.Tamam == true)
            {
                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageGirisHareketleri)
                {
                    var item = gridViewGirisHareket.GetFocusedDataRow();
                    if (item != null)
                    {
                        nMALZEMELER_TOPLAMSTOKMIKTARI = clGenelTanim.DBToInt32(item["MALZEME GİRİŞ ADET"]);
                        nMALZEMELER_TOPLAMSTOKMIKTARI = nMALZEMELER_TOPLAMSTOKMIKTARI - clGenelTanim.DBToInt32(string.IsNullOrEmpty(frmAdet.textEditAdet.Text.ToString()) ? "0" : frmAdet.textEditAdet.Text.ToString());


                        sbD.Append("Update TBL_LST_MALZEMEGIRIS set ");
                        sbD.AppendFormat(" MALZEMEGIRIS_ADET={0} ", clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text));
                        sbD.AppendFormat(" ,MALZEMEGIRIS_SORGUTOPLAMFIYAT={0} ", clGenelTanim.tosqlstring((clGenelTanim.DBToDecimal(item["MALZEME GİRİŞ BİRİM FİYAT"]) * clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text)).ToString().Replace(',', '.'), 10, true));
                        sbD.AppendFormat(" ,MALZEMEGIRIS_BIRIMFIYAT={0} ", clGenelTanim.tosqlstring((clGenelTanim.DBToDecimal(item["MALZEME GİRİŞ BİRİM FİYAT1"]) * clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text)).ToString().Replace(',', '.'), 10, true));
                        sbD.AppendFormat(" Where MALZEMEGIRIS_ID={0}", clGenelTanim.DBToInt32(item["MALZEMEGIRIS_ID"]));
                        clSqlTanim.RunStoredProc(sbD.ToString());

                        sbU.Append("Update TBL_LST_MALZEMELER set ");
                        sbU.AppendFormat(" MALZEME_STOKMIKTARI=MALZEME_STOKMIKTARI - {0} ", nMALZEMELER_TOPLAMSTOKMIKTARI.ToString());
                        sbU.AppendFormat(" Where MALZEME_ID={0}", clGenelTanim.DBToInt32(item["MALZEMEGIRIS_MALZEMELERID"]).ToString());

                        clSqlTanim.RunStoredProc(sbU.ToString());

                        InitForm1();
                    }
                }

                if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri)
                {
                    var item = gridViewCikisHareket.GetFocusedDataRow();
                    if (item != null)
                    {
                        nMALZEMELER_TOPLAMSTOKMIKTARI = clGenelTanim.DBToInt32(item["MALZEME ÇIKIŞ ADET"]);
                        nMALZEMELER_TOPLAMSTOKMIKTARI = nMALZEMELER_TOPLAMSTOKMIKTARI - clGenelTanim.DBToInt32(string.IsNullOrEmpty(frmAdet.textEditAdet.Text.ToString()) ? "0" : frmAdet.textEditAdet.Text.ToString());

                        sbD.Append("Update TBL_LST_MALZEMECIKIS set ");
                        sbD.AppendFormat(" MALZEMECIKIS_ADET={0} ", clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text));
                        sbD.AppendFormat(" ,MALZEMECIKIS_SORGUTOPLAMFIYAT={0} ", clGenelTanim.tosqlstring((clGenelTanim.DBToDecimal(item["MALZEME ÇIKIŞ BİRİM FİYAT"]) * clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text)).ToString().Replace(',', '.'), 10, true));
                        sbD.AppendFormat(" ,MALZEMECIKIS_BIRIMFIYAT={0} ", clGenelTanim.tosqlstring((clGenelTanim.DBToDecimal(item["MALZEME ÇIKIŞ BİRİM FİYAT1"]) * clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text)).ToString().Replace(',', '.'), 10, true));
                        sbD.AppendFormat("WHERE MALZEMECIKIS_ID={0}", clGenelTanim.DBToInt32(item["MALZEMECIKIS_ID"]));
                        clSqlTanim.RunStoredProc(sbD.ToString());

                        sbU.Append("update TBL_LST_MALZEMELER set ");
                        sbU.AppendFormat(" MALZEME_STOKMIKTARI=MALZEME_STOKMIKTARI + {0} ", nMALZEMELER_TOPLAMSTOKMIKTARI.ToString());
                        sbU.AppendFormat(" where MALZEME_ID={0}", clGenelTanim.DBToInt32(item["MALZEMECIKIS_MALZEMELERID"]).ToString());

                        clSqlTanim.RunStoredProc(sbU.ToString());

                        InitForm2();
                    }
                }
            }
        }

        public class MyModel
        {
            public int MALZEMEGRUP_ID { get; set; }
            public string MALZEMEGRUP_ADI { get; set; }
            //public string Version { get; set; }

            public string FullId { get { return String.Format("{0}{1}", MALZEMEGRUP_ID, MALZEMEGRUP_ADI); } }
        }

        public class MyModel2
        {
            public int MALZEME_DEPARTMANID { get; set; }
            public string MALZEME_DEPARTMANADI { get; set; }
            //public string Version { get; set; }

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

        private void xtraTabControlDepoHareketleri_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //if (xtraTabControlDepoHareketleri.SelectedTabPage == xtraTabPageCikisHareketleri2)
            //{
            //    layoutControlItemGrup.Enabled = false;
            //    layoutControlItemDepartman.Enabled = true;
            //}
            //else
            //{
            //    layoutControlItemGrup.Enabled = true; layoutControlItemDepartman.Enabled = false; }
        }
    }
}
