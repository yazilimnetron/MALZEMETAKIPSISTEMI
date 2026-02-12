using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeSiparisOnayi : Form
    {
        private int MALZEMEDEPOISTEM_ID = 0;
        public int malzemesiparisKullanici = 0;
        private string cikisyapilmayan = "0";
        string malzeme_DepoDurum = "";
        int MALZEMEISTEM_ID = 0;
        int MALZEMEISTEM_MALZEMELERID = 0;

        bool onaylandi = false;
        decimal MALZEMEGIRIS_BIRIMFIYAT = 0m;
        decimal MALZEMESIPARIS_ADET = 0;
        decimal MALZEMESIPARIS_ASILADET = 0;
        decimal MALZEMESTOK_ADET = 0;
        decimal MALZEMEGIRIS_SIPARISFIYAT = 0m;
        decimal MALZEMESTOK_GERCEKADET = 0m;
        public frmMalzemeSiparisOnayi()
        {
            InitializeComponent();
            layoutControlMalzemeTalepIslemleri.LayoutKontrolleriniSifirla();
        }

        void SetGridFont(GridView view, Font font)
        {
            foreach (AppearanceObject ap in view.Appearance)

                ap.Font = font;
        }
        public void gridTemizle()
        {
            gridControlMalzemeTalepDepartman.DataSource = null;
            gridControlMalzemeTalepleri.DataSource = null;
            malzeme_DepoDurum = string.Empty;
            SetOnayRedButtonsByDurum();
        }

        private void SetOnayRedButtonsByDurum()
        {
            bool isOnaysiz = string.Equals(malzeme_DepoDurum, "Onaysız", StringComparison.OrdinalIgnoreCase);
            this.simpleButtonMalzemeSiparisOnay.Enabled = isOnaysiz;
            this.simpleButtonMalzemeSiparisRed.Enabled = isOnaysiz;
        }

        public void InitForm()
        {
            gridTemizle();
            StringBuilder sb = new StringBuilder(512);

            if (clGenelTanim.currentYoneticiMi != 1)
            {
                sb.Append("SELECT MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', ");
                sb.Append("Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylı' when MALZEMEDEPOISTEM_DURUM is null then 'Onaysız' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ' ");
                sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
                sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ID=TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_KULLANICIID ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ON TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");
                sb.AppendFormat("WHERE TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID={0} and CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)>={1}", clGenelTanim.currentMalzemeKullanıcıDepartmanId, Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.AppendFormat("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)<={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.Append("GROUP BY MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI, MALZEMEKULLANICI_ADI, MALZEMEDEPOISTEM_DURUM, MALZEMEDEPOISTEM_ISTEMTARIHI ");
                sb.Append("ORDER BY 1 DESC");
            }
            else
            {
                sb.Append("SELECT MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', ");
                sb.Append("Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylı' when MALZEMEDEPOISTEM_DURUM is null then 'Onaysız' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ' ");
                sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
                sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ID=TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_KULLANICIID ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ON TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");
                sb.AppendFormat("WHERE CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)>={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.AppendFormat("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)<={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.Append("GROUP BY MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI, MALZEMEKULLANICI_ADI, MALZEMEDEPOISTEM_DURUM, MALZEMEDEPOISTEM_ISTEMTARIHI ");
                sb.Append("ORDER BY 1 DESC");
            }

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlMalzemeTalepDepartman.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].Visible = false;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewMalzemeTalepDepartman, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepDepartman.BestFitColumns();

            //if (clGenelTanim.currentYoneticiMi == 1)
            //{
            //    this.simpleButtonMalzemeSiparisOnay.Enabled = true;
            //    this.simpleButtonMalzemeSiparisRed.Enabled = true;
            //}
        }

        public void InitForm1()
        {
            gridTemizle();

            StringBuilder sb = new StringBuilder(512);


            if (clGenelTanim.currentYoneticiMi != 1)
            {
                sb.Append("SELECT MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', ");
                sb.Append("Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylı' when MALZEMEDEPOISTEM_DURUM is null then 'Onaysız' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ' ");
                sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
                sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ID=TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_KULLANICIID ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ON TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");
                sb.AppendFormat("WHERE TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DURUM=1 and TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID={0} and CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)>={1}", clGenelTanim.currentMalzemeKullanıcıDepartmanId, Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.AppendFormat("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)<={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.Append("GROUP BY MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI, MALZEMEKULLANICI_ADI, MALZEMEDEPOISTEM_DURUM, MALZEMEDEPOISTEM_ISTEMTARIHI ");
                sb.Append("ORDER BY 1 DESC");
            }
            else
            {
                sb.Append("SELECT MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', ");
                sb.Append("Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylı' when MALZEMEDEPOISTEM_DURUM is null then 'Onaysız' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ' ");
                sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
                sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ID=TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_KULLANICIID ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ON TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");
                sb.AppendFormat("WHERE TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DURUM=1 and CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)>={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.AppendFormat("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)<={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.Append("GROUP BY MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI, MALZEMEKULLANICI_ADI, MALZEMEDEPOISTEM_DURUM, MALZEMEDEPOISTEM_ISTEMTARIHI ");
                sb.Append("ORDER BY 1 DESC");
            }

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlMalzemeTalepDepartman.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].Visible = false;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewMalzemeTalepDepartman, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepDepartman.BestFitColumns();

            //if (clGenelTanim.currentYoneticiMi == 1)
            //{
            //    this.simpleButtonMalzemeSiparisOnay.Enabled = true;
            //    this.simpleButtonMalzemeSiparisRed.Enabled = true;
            //}
        }

        public void InitForm2()
        {
            gridTemizle();

            StringBuilder sb = new StringBuilder(512);

            if (clGenelTanim.currentYoneticiMi != 1)
            {
                sb.Append("SELECT MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', ");
                sb.Append("Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylı' when MALZEMEDEPOISTEM_DURUM is null then 'Onaysız' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ' ");
                sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
                sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ID=TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_KULLANICIID ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ON TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");
                sb.AppendFormat("WHERE TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DURUM is null and TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID={0} and CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)>={1}", clGenelTanim.currentMalzemeKullanıcıDepartmanId, Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.AppendFormat("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)<={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.Append("GROUP BY MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI, MALZEMEKULLANICI_ADI, MALZEMEDEPOISTEM_DURUM, MALZEMEDEPOISTEM_ISTEMTARIHI ");
                sb.Append("ORDER BY 1 DESC");
            }
            else
            {
                sb.Append("SELECT MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', ");
                sb.Append("Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylı' when MALZEMEDEPOISTEM_DURUM is null then 'Onaysız' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ' ");
                sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
                sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ID=TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_KULLANICIID ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ON TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");
                sb.AppendFormat("WHERE TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DURUM is null and CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)>={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.AppendFormat("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)<={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.Append("GROUP BY MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI, MALZEMEKULLANICI_ADI, MALZEMEDEPOISTEM_DURUM, MALZEMEDEPOISTEM_ISTEMTARIHI ");
                sb.Append("ORDER BY 1 DESC");
            }


            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlMalzemeTalepDepartman.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].Visible = false;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewMalzemeTalepDepartman, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepDepartman.BestFitColumns();

            //if (clGenelTanim.currentYoneticiMi == 1)
            //{
            //    this.simpleButtonMalzemeSiparisOnay.Enabled = true;
            //    this.simpleButtonMalzemeSiparisRed.Enabled = true;
            //}
        }

        public void InitForm3()
        {
            gridTemizle();

            StringBuilder sb = new StringBuilder(512);

            if (clGenelTanim.currentYoneticiMi != 1)
            {
                sb.Append("SELECT MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', ");
                sb.Append("Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylı' when MALZEMEDEPOISTEM_DURUM is null then 'Onaysız' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ' ");
                sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
                sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ID=TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_KULLANICIID ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ON TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");
                sb.AppendFormat("WHERE TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DURUM=2 and TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID={0} and CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)>={1}", clGenelTanim.currentMalzemeKullanıcıDepartmanId, Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.AppendFormat("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)<={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.Append("GROUP BY MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI, MALZEMEKULLANICI_ADI, MALZEMEDEPOISTEM_DURUM, MALZEMEDEPOISTEM_ISTEMTARIHI ");
                sb.Append("ORDER BY 1 DESC");
            }
            else
            {
                sb.Append("SELECT MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', ");
                sb.Append("Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylı' when MALZEMEDEPOISTEM_DURUM is null then 'Onaysız' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ' ");
                sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
                sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ID=TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_KULLANICIID ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ON TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");
                sb.AppendFormat("WHERE TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DURUM=2 and CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)>={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.AppendFormat("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)<={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.Append("GROUP BY MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI, MALZEMEKULLANICI_ADI, MALZEMEDEPOISTEM_DURUM, MALZEMEDEPOISTEM_ISTEMTARIHI ");
                sb.Append("ORDER BY 1 DESC");
            }


            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlMalzemeTalepDepartman.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].Visible = false;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewMalzemeTalepDepartman, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepDepartman.BestFitColumns();

            //if (clGenelTanim.currentYoneticiMi == 1)
            //{
            //    this.simpleButtonMalzemeSiparisOnay.Enabled = true;
            //    this.simpleButtonMalzemeSiparisRed.Enabled = true;
            //}
        }

        public void InitForm4()
        {
            gridTemizle();

            StringBuilder sb = new StringBuilder(512);

            if (clGenelTanim.currentYoneticiMi != 1)
            {
                sb.Append("SELECT MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', ");
                sb.Append("Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylı' when MALZEMEDEPOISTEM_DURUM is null then 'Onaysız' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ' ");
                sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
                sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ID=TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_KULLANICIID ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ON TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");
                sb.AppendFormat("WHERE TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DURUM=3 and TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID={0} and CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)>={1}", clGenelTanim.currentMalzemeKullanıcıDepartmanId, Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.AppendFormat("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)<={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.Append("GROUP BY MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI, MALZEMEKULLANICI_ADI, MALZEMEDEPOISTEM_DURUM, MALZEMEDEPOISTEM_ISTEMTARIHI ");
                sb.Append("ORDER BY 1 DESC");
            }
            else
            {
                sb.Append("SELECT MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', ");
                sb.Append("Case when MALZEMEDEPOISTEM_DURUM =2 then 'Reddedildi' when MALZEMEDEPOISTEM_DURUM =1 then 'Onaylı' when MALZEMEDEPOISTEM_DURUM is null then 'Onaysız' when MALZEMEDEPOISTEM_DURUM =3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ' ");
                sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
                sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ID=TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_KULLANICIID ");
                sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ON TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");
                sb.AppendFormat("WHERE TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DURUM=3 and CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)>={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.AppendFormat("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI ,121)<={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
                sb.Append("GROUP BY MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI, MALZEMEKULLANICI_ADI, MALZEMEDEPOISTEM_DURUM, MALZEMEDEPOISTEM_ISTEMTARIHI ");
                sb.Append("ORDER BY 1 DESC");
            }


            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlMalzemeTalepDepartman.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].Visible = false;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewMalzemeTalepDepartman, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepDepartman.BestFitColumns();

            //if (clGenelTanim.currentYoneticiMi == 1)
            //{
            //    this.simpleButtonMalzemeSiparisOnay.Enabled = true;
            //    this.simpleButtonMalzemeSiparisRed.Enabled = true;
            //}
        }
        private void frmMalzemeTalepIslemleri_Load(object sender, EventArgs e)
        {
            if (radioGroupMalzemeTalepCikis.SelectedIndex == 0) //Tümü
            {
                InitForm();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 1) //Onaylılar
            {
                InitForm1();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 2) //Onaysızlar
            {
                InitForm2();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 3) // Onaylanmayanlar
            {
                InitForm3();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 4) // Teslim Edilenler
            {
                InitForm4();
            }

        }

        private void MALZEMEDEPOISTEMID(int MALZEMEDEPOISTEM_ID)
        {
            StringBuilder sb3 = new StringBuilder(512);
            sb3.Append("SELECT a.MALZEMEISTEM_ID, b.MALZEME_ID, a.MALZEMEISTEM_MALZEMEDEPOISTEMID, ");
            sb3.Append("b.MALZEME_ADI AS 'MALZEME ADI', a.MALZEMEISTEM_MATERYALNO AS 'MATERYAL NO', b.MALZEME_OUDBTNO AS 'MALZEME OUDBTNO', d.MALZEME_DEPARTMANADI AS 'MALZEME DEPARTMANI', (isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) AS 'MALZEME STOKMIKTARI', ");
            sb3.Append("a.MALZEMEISTEM_ADET AS 'MALZEME SİPARİŞ ADET', ");
            sb3.Append("(SELECT TOP 1 ISNULL(e.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS e WHERE b.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC ) as 'MALZEME BİRİM FİYAT', ");
            sb3.Append("b.MALZEME_MINADET AS 'MALZEME MİN ADET', b.MALZEME_MAXADET AS 'MALZEME MAX ADET', ");
            sb3.Append("(SELECT TOP 1 ISNULL(e.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS e WHERE b.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC )*ISNULL(a.MALZEMEISTEM_ADET,0) AS 'MALZEME SİPARİŞ FİYAT', ");
            sb3.Append("b.MALZEME_RAFNO AS 'MALZEME RAF NO', ISNULL(a.MALZEMEISTEM_DURUM,1) AS SEC ");
            sb3.Append("FROM TBL_LST_MALZEMEISTEM a (NOLOCK) ");
            sb3.Append("INNER JOIN TBL_LST_MALZEMELER b (NOLOCK) ON b.MALZEME_ID=a.MALZEMEISTEM_MALZEMELERID ");
            sb3.Append("INNER JOIN TBL_LST_MALZEMEDEPARTMANLAR d (NOLOCK) ON a.MALZEMEISTEM_MALZEMEDEPARTMANISTEMID=d.MALZEME_DEPARTMANID ");
            sb3.Append("LEFT OUTER JOIN (Select  MALZEMEGIRIS_MALZEMELERID, Sum(MALZEMEGIRIS_ADET)  GirislerToplam FROM TBL_LST_MALZEMEGIRIS GROUP BY  MALZEMEGIRIS_MALZEMELERID) g ON g.MALZEMEGIRIS_MALZEMELERID = b.MALZEME_ID ");
            sb3.Append("LEFT OUTER JOIN (Select  MALZEMECIKIS_MALZEMELERID, Sum(MALZEMECIKIS_ADET)  CikislarToplam FROM TBL_LST_MALZEMECIKIS GROUP BY  MALZEMECIKIS_MALZEMELERID) c ON c.MALZEMECIKIS_MALZEMELERID = b.MALZEME_ID ");
            sb3.AppendFormat("WHERE a.MALZEMEISTEM_MALZEMEDEPOISTEMID={0}", MALZEMEDEPOISTEM_ID.ToString());
            DataTable dtMalzemeler3 = clSqlTanim.RunStoredProc(sb3.ToString());
            gridControlMalzemeTalepleri.DataSource = dtMalzemeler3;

            RepositoryItemCheckEdit chkMalzemeDurum = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit() { ValueUnchecked = 0, ValueChecked = 1, ValueGrayed = "" };
            this.gridViewMalzemeTalepleri.Columns["SEC"].ColumnEdit = chkMalzemeDurum;

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_ID"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEME OUDBTNO"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEME MAX ADET"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEME MİN ADET"].Visible = false;

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_ID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsColumn.ShowInCustomizationForm = false;

            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].DisplayFormat.FormatString = "{0:c2}";


            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].SummaryItem.DisplayFormat = "TOPLAM FİYAT: {0:C2}";

            var ri = new RepositoryItemTextEdit();
            ri.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            ri.Mask.EditMask = "([0-9]{1,3}[.]?[0-9]{0,3})";
            ri.Mask.UseMaskAsDisplayFormat = true;
            gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ ADET"].ColumnEdit = ri;

            this.SetGridFont(gridViewMalzemeTalepleri, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepleri.BestFitColumns();
        }

        private void gridViewMalzemeTalepleri_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.GetTextCaption() == "SEC")
            {
                // SEC değişimi kullanıcı seçimidir; DB güncellemesi yalnızca Onay/Red butonlarında yapılır.
                return;
            }

            //if (e.Column.GetTextCaption() == "MALZEME SİPARİŞ ADET")
            //{

            //    MALZEMESIPARIS_ASILADET = Convert.ToDecimal(gridViewMalzemeTalepleri.GetFocusedRowCellValue(gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ ADET"]).ToString());

            //    //MALZEMEISTEM_ID = Convert.ToInt32(gridViewMalzemeTalepleri.GetRowCellValue(e.RowHandle, gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_ID"]).ToString());

            //    //if (onaylandi == true && MALZEMESIPARIS_ADET == 0)
            //    //{
            //    //    StringBuilder sbU = new StringBuilder();
            //    //    sbU.Append("Update TBL_LST_MALZEMEISTEM set ");
            //    //    sbU.AppendFormat(" MALZEMEISTEM_ADET={0}", e.Value.ToString());
            //    //    sbU.AppendFormat(" where MALZEMEISTEM_ID={0}", MALZEMEISTEM_ID.ToString());
            //    //    clSqlTanim.RunStoredProc(sbU.ToString());
            //    //}
            //}
        }

        private void gridViewMalzemeTalepleri_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            StringBuilder sbU = new StringBuilder();
            StringBuilder sbUU = new StringBuilder();

            if (e.Column.FieldName != "MALZEME SİPARİŞ ADET") return;

            MALZEMEISTEM_MALZEMELERID = Convert.ToInt32(gridViewMalzemeTalepleri.GetFocusedRowCellValue(gridViewMalzemeTalepleri.Columns["MALZEME_ID"]).ToString());
            MALZEMEISTEM_ID = Convert.ToInt32(gridViewMalzemeTalepleri.GetFocusedRowCellValue(gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_ID"]).ToString());
            MALZEMEGIRIS_BIRIMFIYAT = Convert.ToDecimal(gridViewMalzemeTalepleri.GetFocusedRowCellValue(gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"]).ToString());
            MALZEMESIPARIS_ADET = Convert.ToDecimal(string.IsNullOrEmpty(gridViewMalzemeTalepleri.GetFocusedRowCellValue(gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ ADET"]).ToString()) ? "0" : gridViewMalzemeTalepleri.GetFocusedRowCellValue(gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ ADET"]).ToString());
            MALZEMESTOK_ADET = Convert.ToDecimal(gridViewMalzemeTalepleri.GetFocusedRowCellValue(gridViewMalzemeTalepleri.Columns["MALZEME STOKMIKTARI"]).ToString());// + MALZEMESIPARIS_ASILADET;
            //MALZEMESTOK_ADET = MALZEMESTOK_GERCEKADET - MALZEMESIPARIS_ADET;
            //MALZEMEGIRIS_SIPARISFIYAT = 0;

            //if (MALZEMESIPARIS_ASILADET != MALZEMESIPARIS_ADET)
            //{
            //    //decimal MALZEMESIPARIS_ADET1 = 0;
            //    //MALZEMESIPARIS_ADET1 = MALZEMESIPARIS_ASILADET - MALZEMESIPARIS_ADET;
            //    sbUU.Append("UPDATE TBL_LST_MALZEMELER SET MALZEME_STOKMIKTARI = ");
            //    sbUU.Append(clGenelTanim.DBToInt32(MALZEMESTOK_ADET));
            //    sbUU.AppendFormat(" WHERE MALZEME_ID={0}", clGenelTanim.DBToInt32(MALZEMEISTEM_MALZEMELERID));
            //    clSqlTanim.RunStoredProc(sbUU.ToString());

            //    //StringBuilder sb1 = new StringBuilder(1024);
            //    //sb1.Append("SELECT MALZEME_STOKMIKTARI FROM TBL_LST_MALZEMELER ");
            //    //sb1.AppendFormat(" WHERE MALZEME_ID={0}", clGenelTanim.DBToInt32(MALZEMEISTEM_MALZEMELERID));
            //    //DataTable table = clSqlTanim.RunStoredProc(sb1.ToString());
            //    //if (table != null && table.Rows.Count == 1)
            //    //{
            //    //    MALZEMESTOK_ADET= Convert.ToInt32(table.Rows[0][0]);
            //    //}

            //    this.gridViewMalzemeTalepleri.SetFocusedRowCellValue(gridViewMalzemeTalepleri.Columns["MALZEME STOKMIKTARI"], MALZEMESTOK_ADET);
            //}

            if (MALZEMESIPARIS_ADET == 0)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("Malzemenin talep adedini 0 olarak değiştirdiniz. Bu malzeme teslim edilmeyecektir. Onaylıyor musunuz !", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    onaylandi = true;
                else
                {
                    this.gridViewMalzemeTalepleri.CellValueChanged -= this.gridViewMalzemeTalepleri_CellValueChanged;
                    try
                    {
                        this.gridViewMalzemeTalepleri.SetRowCellValue(gridViewMalzemeTalepleri.FocusedRowHandle, gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ ADET"], MALZEMESIPARIS_ASILADET);

                    }
                    finally
                    {
                        this.gridViewMalzemeTalepleri.CellValueChanged += this.gridViewMalzemeTalepleri_CellValueChanged;
                    }
                }
            }

            if (MALZEMESTOK_ADET < MALZEMESIPARIS_ADET)
            {
                XtraMessageBox.Show("SİPARİŞ MİKTARI STOK MİKTARINDAN FAZLA OLAMAZ !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.simpleButtonMalzemeSiparisOnay.Enabled = false;
                this.simpleButtonMalzemeSiparisRed.Enabled = false;
            }
            else
            {
                if (onaylandi == false || MALZEMESIPARIS_ADET > 0)
                {
                    sbU.Append("Update TBL_LST_MALZEMEISTEM set ");
                    sbU.AppendFormat(" MALZEMEISTEM_ADET={0}", MALZEMESIPARIS_ADET.ToString());
                    sbU.AppendFormat(" where MALZEMEISTEM_ID={0}", MALZEMEISTEM_ID.ToString());
                    clSqlTanim.RunStoredProc(sbU.ToString());
                }

                if (onaylandi == true && MALZEMESIPARIS_ADET == 0)
                {

                    sbU.Append("Update TBL_LST_MALZEMEISTEM set ");
                    sbU.AppendFormat(" MALZEMEISTEM_ADET={0}", e.Value.ToString());
                    sbU.AppendFormat(" where MALZEMEISTEM_ID={0}", MALZEMEISTEM_ID.ToString());
                    clSqlTanim.RunStoredProc(sbU.ToString());
                }

                //this.simpleButtonMalzemeSiparisOnay.Enabled = true;
                //this.simpleButtonMalzemeSiparisRed.Enabled = true;

                //MALZEMEGIRIS_SIPARISFIYAT = MALZEMEGIRIS_BIRIMFIYAT * MALZEMESIPARIS_ADET;

                //this.gridViewMalzemeTalepleri.SetFocusedRowCellValue(gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"], MALZEMEGIRIS_SIPARISFIYAT);

                //this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
                //this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].SummaryItem.DisplayFormat = "TOPLAM FİYAT: {0:c2}";
            }

        }


        private void simpleButtonMalzemeTalepOnayla_Click(object sender, EventArgs e)
        {
            if (clGenelTanim.currentYoneticiMi != 1)
            {
                XtraMessageBox.Show("Bu işlem için yönetici yetkisi gereklidir.", "Yetki", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gridViewMalzemeTalepleri.RowCount < 1)
            {
                XtraMessageBox.Show("Onaylanacak sipariş satırı bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.Yes == XtraMessageBox.Show("Sipariş Onaylansın mı?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    int depoIstemId = clGenelTanim.DBToInt32(gridViewMalzemeTalepleri.GetRowCellValue(gridViewMalzemeTalepleri.FocusedRowHandle, "MALZEMEISTEM_MALZEMEDEPOISTEMID"));
                    if (depoIstemId <= 0)
                    {
                        XtraMessageBox.Show("Geçerli sipariş başlığı bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int onaylananAdet = 0;
                    int reddedilenAdet = 0;
                    StringBuilder sbTx = new StringBuilder(4096);
                    sbTx.AppendLine("BEGIN TRY");
                    sbTx.AppendLine("BEGIN TRAN");

                    for (int i = 0; i < gridViewMalzemeTalepleri.RowCount; i++)
                    {
                        int deger = clGenelTanim.DBToInt32(gridViewMalzemeTalepleri.GetDataRow(i)["SEC"]);
                        int malzemeId = clGenelTanim.DBToInt32(gridViewMalzemeTalepleri.GetDataRow(i)["MALZEME_ID"]);
                        int siparisDepoIstemId = clGenelTanim.DBToInt32(gridViewMalzemeTalepleri.GetDataRow(i)["MALZEMEISTEM_MALZEMEDEPOISTEMID"]);
                        int satirDurum = deger == 1 ? 1 : 2;
                        if (satirDurum == 1)
                        {
                            onaylananAdet++;
                        }
                        else
                        {
                            reddedilenAdet++;
                        }

                        sbTx.Append("UPDATE TBL_LST_MALZEMEISTEM SET MALZEMEISTEM_DURUM = ");
                        sbTx.Append(satirDurum);
                        sbTx.AppendFormat(" WHERE MALZEMEISTEM_MALZEMELERID={0} AND MALZEMEISTEM_MALZEMEDEPOISTEMID={1};", malzemeId, siparisDepoIstemId);
                        sbTx.AppendLine();
                    }

                    int depoDurum = onaylananAdet > 0 ? 1 : 2;
                    sbTx.AppendFormat("UPDATE TBL_LST_MALZEMEDEPOISTEM SET MALZEMEDEPOISTEM_DURUM = {0}, MALZEMEDEPOISTEM_ONAYTARIHI='{1}', MALZEMEDEPOISTEM_ONAYKULLANICIID={2} WHERE MALZEMEDEPOISTEM_ID={3};",
                        depoDurum,
                        Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm"),
                        clGenelTanim.KullaniciKodu.ToString(),
                        depoIstemId);
                    sbTx.AppendLine();
                    sbTx.AppendLine("COMMIT TRAN");
                    sbTx.AppendLine("END TRY");
                    sbTx.AppendLine("BEGIN CATCH");
                    sbTx.AppendLine("IF @@TRANCOUNT > 0 ROLLBACK TRAN");
                    sbTx.AppendLine("THROW");
                    sbTx.AppendLine("END CATCH");

                    clSqlTanim.RunStoredProc(sbTx.ToString());
                    XtraMessageBox.Show(reddedilenAdet > 0 && onaylananAdet == 0 ? "Sipariş Reddedildi ..." : "Sipariş Onaylandı ...");

                    if (radioGroupMalzemeTalepCikis.SelectedIndex == 0) //Tümü
                    {
                        InitForm();
                    }

                    if (radioGroupMalzemeTalepCikis.SelectedIndex == 1) //Onaylılar
                    {
                        InitForm1();
                    }

                    if (radioGroupMalzemeTalepCikis.SelectedIndex == 2) //Onaysızlar
                    {
                        InitForm2();
                    }

                    if (radioGroupMalzemeTalepCikis.SelectedIndex == 3) // Onaylanmayanlar
                    {
                        InitForm3();
                    }

                    if (radioGroupMalzemeTalepCikis.SelectedIndex == 4) // Teslim Edilenler
                    {
                        InitForm4();
                    }

                }

                catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            }
        }

        private void simpleButtonMalzemeTalepReddet_Click(object sender, EventArgs e)
        {
            //StringBuilder sbU = new StringBuilder(512);

            //var item = gridViewMalzemeTalepDepartman.GetFocusedDataRow();

            //try
            //{
            //    if (item != null)
            //    {
            //        sbU.Append("Update TBL_LST_MALZEMEDEPOISTEM set ");
            //        sbU.AppendFormat(" MALZEMEDEPOISTEM_DURUM={0}", 2);
            //        sbU.AppendFormat(" where MALZEMEDEPOISTEM_ID={0}", clGenelTanim.DBToInt32(item["MALZEMEISTEM_MALZEMEDEPOISTEMID"]));
            //        clSqlTanim.RunStoredProc(sbU.ToString());

            //        for (int i = 0; i < gridViewMalzemeTalepleri.RowCount; i++)
            //        {
            //            StringBuilder sbUU = new StringBuilder(512);
            //            sbUU.Append("UPDATE TBL_LST_MALZEMEISTEM SET MALZEMEISTEM_DURUM = 2 ");
            //            sbUU.AppendFormat(" WHERE MALZEMEISTEM_MALZEMELERID={0}", clGenelTanim.DBToInt32(gridViewMalzemeTalepleri.GetDataRow(i)["MALZEME_ID"].ToString()));
            //            clSqlTanim.RunStoredProc(sbUU.ToString());
            //        }

            //        XtraMessageBox.Show("Talep Reddetildi...");
            //    }

            //    if (radioGroupMalzemeTalepCikis.SelectedIndex == 0) //Tümü
            //    {
            //        InitForm();
            //    }

            //    if (radioGroupMalzemeTalepCikis.SelectedIndex == 1) //Onaylılar
            //    {
            //        InitForm1();
            //    }

            //    if (radioGroupMalzemeTalepCikis.SelectedIndex == 2) //Onaysızlar
            //    {
            //        InitForm2();
            //    }

            //    if (radioGroupMalzemeTalepCikis.SelectedIndex == 3) // Onaylanmayanlar
            //    {
            //        InitForm3();
            //    }

            //    if (radioGroupMalzemeTalepCikis.SelectedIndex == 4) // Teslim Edilenler
            //    {
            //        InitForm4();
            //    }
            //}

            //catch (Exception ex) { XtraMessageBox.Show(ex.Message); }

        }

        private void dateEditTalepTarihi_EditValueChanged(object sender, EventArgs e)
        {
            if (radioGroupMalzemeTalepCikis.SelectedIndex == 0) //Tümü
            {
                InitForm();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 1) //Onaylılar
            {
                InitForm1();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 2) //Onaysızlar
            {
                InitForm2();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 3) // Onaylanmayanlar
            {
                InitForm3();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 4) // Teslim Edilenler
            {
                InitForm4();
            }
        }

        private void radioGroupMalzemeTalepCikis_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 0) //Tümü
            {
                InitForm();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 1) //Onaylılar
            {
                InitForm1();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 2) //Onaysızlar
            {
                InitForm2();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 3) // Onaylanmayanlar
            {
                InitForm3();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 4) // Teslim Edilenler
            {
                InitForm4();
            }
        }

        private void gridViewMalzemeTalepDepartman_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string malzeme_DepoDurum = clGenelTanim.DBToString(gridViewMalzemeTalepDepartman.GetRowCellDisplayText(e.RowHandle, gridViewMalzemeTalepDepartman.Columns[3]));

                if (malzeme_DepoDurum == "Teslim Edildi")
                {
                    e.Appearance.BackColor = Color.Red;
                    //this.simpleButtonMalzemeSiparisOnay.Enabled = false;
                    //this.simpleButtonMalzemeSiparisRed.Enabled = false;

                    //MALZEMEDEPOISTEM_DURUM = 2 then 'Reddedildi' 
                    //    when MALZEMEDEPOISTEM_DURUM = 1 then 'Onaylandı'  
                    //    when MALZEMEDEPOISTEM_DURUM = 3 then 'Teslim Edildi'
                }

                //if (malzeme_DepoDurum == "Reddedildi")
                //{
                //    this.simpleButtonMalzemeSiparisOnay.Enabled = false;
                //    this.simpleButtonMalzemeSiparisRed.Enabled = false;

                //    //MALZEMEDEPOISTEM_DURUM = 2 then 'Reddedildi' 
                //    //    when MALZEMEDEPOISTEM_DURUM = 1 then 'Onaylandı'  
                //    //    when MALZEMEDEPOISTEM_DURUM = 3 then 'Teslim Edildi'
                //}

                //if (malzeme_DepoDurum == "Onaylandı")
                //{
                //    this.simpleButtonMalzemeSiparisOnay.Enabled = false;
                //    this.simpleButtonMalzemeSiparisRed.Enabled = false;

                //    //MALZEMEDEPOISTEM_DURUM = 2 then 'Reddedildi' 
                //    //    when MALZEMEDEPOISTEM_DURUM = 1 then 'Onaylandı'  
                //    //    when MALZEMEDEPOISTEM_DURUM = 3 then 'Teslim Edildi'
                //}
            }
        }

        private void frmMalzemeTalepIslemleri_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            //frmGirisE.pictureEdit1.BringToFront();
        }

        private void simpleButtonListele_Click(object sender, EventArgs e)
        {
            if (radioGroupMalzemeTalepCikis.SelectedIndex == 0) //Tümü
            {
                InitForm();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 1) //Onaylılar
            {
                InitForm1();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 2) //Onaysızlar
            {
                InitForm2();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 3) // Onaylanmayanlar
            {
                InitForm3();
            }

            if (radioGroupMalzemeTalepCikis.SelectedIndex == 4) // Teslim Edilenler
            {
                InitForm4();
            }
        }

        private void gridViewMalzemeTalepDepartman_DoubleClick(object sender, EventArgs e)
        {
            var item = gridViewMalzemeTalepDepartman.GetFocusedDataRow();

            if (item != null)
            {
                MALZEMEDEPOISTEM_ID = clGenelTanim.DBToInt32(item["MALZEMEISTEM_MALZEMEDEPOISTEMID"]);
                malzeme_DepoDurum = clGenelTanim.DBToString(item["SİPARİŞ DURUM"]);

                MALZEMEDEPOISTEMID(this.MALZEMEDEPOISTEM_ID);
                SetOnayRedButtonsByDurum();

            }
        }

        private void gridViewMalzemeTalepleri_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (clGenelTanim.currentYoneticiMi != 1)
            {
                e.Cancel = true;
                return;
            }

            if (malzeme_DepoDurum == "Onaysız")
            {
                GridView view = sender as GridView;

                if (view.FocusedColumn.FieldName == "MALZEME SİPARİŞ ADET" || view.FocusedColumn.FieldName == "SEC")
                {
                    view.Columns["MALZEME SİPARİŞ ADET"].OptionsColumn.AllowEdit = true;
                    view.Columns["SEC"].OptionsColumn.AllowEdit = true;
                }
                else { e.Cancel = true; }

            }
            else { e.Cancel = true; } 

        }

        private void simpleButtonMalzemeSiparisRed_Click(object sender, EventArgs e)
        {
            if (clGenelTanim.currentYoneticiMi != 1)
            {
                XtraMessageBox.Show("Bu işlem için yönetici yetkisi gereklidir.", "Yetki", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = gridViewMalzemeTalepDepartman.GetFocusedDataRow();
            if (DialogResult.Yes == XtraMessageBox.Show("Sipariş İptal Edilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    if (item != null)
                    {
                        int depoIstemId = clGenelTanim.DBToInt32(item["MALZEMEISTEM_MALZEMEDEPOISTEMID"]);
                        StringBuilder sbTx = new StringBuilder(1024);
                        sbTx.AppendLine("BEGIN TRY");
                        sbTx.AppendLine("BEGIN TRAN");
                        sbTx.AppendFormat("UPDATE TBL_LST_MALZEMEDEPOISTEM SET MALZEMEDEPOISTEM_DURUM={0} WHERE MALZEMEDEPOISTEM_ID={1};", 2, depoIstemId);
                        sbTx.AppendLine();
                        sbTx.AppendFormat("UPDATE TBL_LST_MALZEMEISTEM SET MALZEMEISTEM_DURUM={0} WHERE MALZEMEISTEM_MALZEMEDEPOISTEMID={1};", 2, depoIstemId);
                        sbTx.AppendLine();
                        sbTx.AppendFormat("DELETE FROM TBL_LST_MALZEMECIKIS WHERE MALZEMECIKIS_MALZEMEDEPOISTEM_ID={0};", depoIstemId);
                        sbTx.AppendLine();
                        sbTx.AppendLine("COMMIT TRAN");
                        sbTx.AppendLine("END TRY");
                        sbTx.AppendLine("BEGIN CATCH");
                        sbTx.AppendLine("IF @@TRANCOUNT > 0 ROLLBACK TRAN");
                        sbTx.AppendLine("THROW");
                        sbTx.AppendLine("END CATCH");

                        clSqlTanim.RunStoredProc(sbTx.ToString());

                        XtraMessageBox.Show("Sipariş Reddetildi...");
                    }

                    if (radioGroupMalzemeTalepCikis.SelectedIndex == 0) //Tümü
                    {
                        InitForm();
                    }

                    if (radioGroupMalzemeTalepCikis.SelectedIndex == 1) //Onaylılar
                    {
                        InitForm1();
                    }

                    if (radioGroupMalzemeTalepCikis.SelectedIndex == 2) //Onaysızlar
                    {
                        InitForm2();
                    }

                    if (radioGroupMalzemeTalepCikis.SelectedIndex == 3) // Onaylanmayanlar
                    {
                        InitForm3();
                    }

                    if (radioGroupMalzemeTalepCikis.SelectedIndex == 4) // Teslim Edilenler
                    {
                        InitForm4();
                    }
                }

                catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            }
        }
    }
}
