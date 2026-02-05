using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using MALZEMETAKIPSISTEMI;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeGirisEkle : Form
    {
        public int MALZEMELER_ID = 0, MALZEMELER_GRUPID = 0, MALZEMEGIRIS_TESLIMKULLANICIID=0; //, nMALZEMELER_STOKMIKTARI = 0;
        
        private bool yeniMalzemeGiris = true;

        decimal malzemeToplamFiyat = 0m;

        decimal malzemeUsdFiyat = 0m, malzemeEuroFiyat = 0m, malzemeJpyFiyat = 0m, malzemeChfFiyat = 0m, malzemeGbpFiyat = 0m, malzemeRonFiyat = 0m;

        decimal usdEuroOran = 0m, jpyEuroOran = 0m, chfEuroOran = 0m, gbpEuroOran = 0m, ronEuroOran = 0m;

        public frmMalzemeGirisEkle()
        {
            InitializeComponent();
            layoutControlMalzemeCikisBilgileri.LayoutKontrolleriniSifirla();
        }

        void SetGridFont(GridView view, Font font)
        {
            foreach (AppearanceObject ap in view.Appearance)

                ap.Font = font;
        }

        public void InitFormKur()
        {
            StringBuilder sb = new StringBuilder(1024);
            sb.Append("SELECT TOP 1 ISNULL(MALZEMEDOVIZKUR_USDSATIS,0), ISNULL(MALZEMEDOVIZKUR_EUROSATIS,0), ISNULL(MALZEMEDOVIZKUR_JPYSATIS,0), ISNULL(MALZEMEDOVIZKUR_GBPSATIS,0), ISNULL(MALZEMEDOVIZKUR_RONSATIS,0), ISNULL(MALZEMEDOVIZKUR_CHFSATIS,0)  FROM TBL_LST_MALZEMEKURBILGILERI (NOLOCK) ORDER BY MALZEMEDOVIZKUR_ID DESC");
            DataTable table = clSqlTanim.RunStoredProc(sb.ToString());
            if (table != null && table.Rows.Count == 1)
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

            DevCmbBoxItem[] items = { new DevCmbBoxItem(1, "TL"), new DevCmbBoxItem(2, "EURO"), new DevCmbBoxItem(3, "USD"), new DevCmbBoxItem(4, "JPY"), new DevCmbBoxItem(5, "CHF"), new DevCmbBoxItem(6, "GBP") };
            comboBoxEditMalzemeGirisParaBirimi.Doldur(items);
            comboBoxEditMalzemeGirisParaBirimi.SelectedIndex = 0;
        }

        void bilgileriGetir()
        {

            StringBuilder sb = new StringBuilder(1024);

            try
            {
                comboBoxEditMalzemeGirisYeri.Doldur("SELECT MALZEMEANAGRUP_ID, MALZEMEANAGRUP_ADI AS 'MALZEME DEPARTMANADI' FROM TBL_LST_MALZEMEANAGRUPLAR (NOLOCK)  WHERE ISNULL (MALZEMEANAGRUP_DURUM,0)=1 ORDER BY 2", false);
                comboBoxEditMalzemeGirisYeri.DegerSec(clGenelTanim.DBToInt32(MALZEMELER_GRUPID.ToString()));

                comboBoxEditMalzemeGirisKullanici.Doldur("select MALZEMEKULLANICI_ID, MALZEMEKULLANICI_ADI from TBL_LST_MALZEMEKULLANICILAR where isnull(MALZEMEKULLANICI_DURUM,0) =1 and isnull(MALZEMEKULLANICI_SIPARISKAPAT,0)=1 order by 2", true);
                comboBoxEditMalzemeGirisKullanici.SelectedIndex = 0;

                //comboBoxEditMalzemeGirisYeri.Text = nMALZEMELER_GRUPID.ToString();          
                //comboBoxEditMalzemeGirisYeri.SelectedIndex = 0;               

                sb.Append("SELECT TOP 1 MALZEMEGIRIS_ADI, MALZEMEGIRIS_PARABIRIMI FROM TBL_LST_MALZEMEGIRIS ");
                sb.Append("WHERE MALZEMEGIRIS_MALZEMELERID = " + MALZEMELER_ID.ToString());
                sb.Append(" ORDER BY MALZEMEGIRIS_TARIH DESC");
                DataTable dt = clSqlTanim.RunStoredProc(sb.ToString());
                //DataTable dt = clSqlTanim.RunStoredProc("SELECT TOP 1 MALZEMEGIRIS_ADI, MALZEMEGIRIS_PARABIRIMI FROM TBL_LST_MALZEMEGIRIS WHERE MALZEMEGIRIS_MALZEMELERID= " + nMALZEMELER_ID.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    textEditMalzemeGirisAdi.Text = dr["MALZEMEGIRIS_ADI"].ToString();
                    //nMALZEMELER_STOKMIKTARI = clGenelTanim.DBToInt32(dr["MALZEME_STOKMIKTARI"]);
                    comboBoxEditMalzemeGirisParaBirimi.DegerSec(clGenelTanim.DBToInt32(dr["MALZEMEGIRIS_PARABIRIMI"]));
                }

                if (string.IsNullOrEmpty(textEditMalzemeGirisAdedi.Text))
                {
                    textEditMalzemeGirisAdedi.Text = "0";
                }

                if (string.IsNullOrEmpty(textEditMalzemeGirisBirimFiyat.Text))
                {
                    textEditMalzemeGirisBirimFiyat.Text = "0";
                }

                if (string.IsNullOrEmpty(textEditMalzemeGirisToplamFiyat.Text))
                {
                    textEditMalzemeGirisToplamFiyat.Text = "0";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }

        private void checkEditMalzemeTumKayit_CheckedChanged(object sender, EventArgs e)
        {
            InitFormListe();
        }

        private void gridViewGirisListe_DoubleClick(object sender, EventArgs e)
        {
            yeniMalzemeGiris = false;
            var item = gridViewGirisListe.GetFocusedDataRow();

            if (item != null)
            {
                StringBuilder sb = new StringBuilder(1024);
                sb.Append("Select TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_ID, TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_ADI as 'MALZEME ADI', TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_ADET as 'Adet',");
                sb.Append("TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_TARIH as 'Tarih', TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_BIRIMFIYAT as 'B.Fiyat', TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_TOPLAMFIYAT as 'T.Fiyat', ");
                sb.Append("TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_PARABIRIMI as 'P.BİRİMİ', ");
                sb.Append("TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_MALZEMEGRUPID, TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_TESLIMKULLANICIID ");
                sb.Append("from TBL_LST_MALZEMEGIRIS ");
                sb.Append("where TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_ID = " + clGenelTanim.DBToInt32(item["MALZEMEGIRIS_ID"]));
                DataTable dt = clSqlTanim.RunStoredProc(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    textEditMalzemeGirisAdi.Text = clGenelTanim.DBToString(dt.Rows[0]["MALZEME ADI"]);
                    textEditMalzemeGirisAdedi.Text = clGenelTanim.DBToInt32(dt.Rows[0]["Adet"]).ToString();
                    dateEditMalzemeGirisTarih.DateTime = clGenelTanim.DBToDate(dt.Rows[0]["Tarih"]);
                    textEditMalzemeGirisBirimFiyat.Text = clGenelTanim.DBToString(dt.Rows[0]["B.Fiyat"]).ToString();
                    textEditMalzemeGirisToplamFiyat.Text = clGenelTanim.DBToString(dt.Rows[0]["T.Fiyat"]).ToString();
                    comboBoxEditMalzemeGirisParaBirimi.DegerSec(clGenelTanim.DBToInt32(dt.Rows[0]["P.BİRİMİ"]));
                    comboBoxEditMalzemeGirisYeri.DegerSec(clGenelTanim.DBToInt32(dt.Rows[0]["MALZEMEGIRIS_MALZEMEGRUPID"]));
                    comboBoxEditMalzemeGirisKullanici.DegerSec(clGenelTanim.DBToInt32(dt.Rows[0]["MALZEMEGIRIS_TESLIMKULLANICIID"]));
                }
            }
        }

        private void simpleButtonMalzemeGirisYazdir_Click(object sender, EventArgs e)
        {
            MALZEMELER_GRUPID = clGenelTanim.DBToInt32(comboBoxEditMalzemeGirisYeri.SecilenDeger().Id.ToString());
            MALZEMEGIRIS_TESLIMKULLANICIID = clGenelTanim.DBToInt32(comboBoxEditMalzemeGirisKullanici.SecilenDeger().Id.ToString());

            XtraReport1 rapor = new XtraReport1();
            Parameter param1 = new Parameter();
            Parameter param2 = new Parameter();

            param1.Name = "MALZEMEGIRIS_TESLIMKULLANICIID";
            param1.Type = typeof(System.Int32);
            param1.Value = MALZEMEGIRIS_TESLIMKULLANICIID.ToString();
            param1.Visible = false;

            param2.Name = "MALZEMEGIRIS_MALZEMEGRUPID";
            param2.Type = typeof(System.Int32);
            param2.Value = MALZEMELER_GRUPID.ToString();
            param2.Visible = false;

            rapor.Parameters.Add(param1);
            rapor.Parameters.Add(param2);

            rapor.FilterString = "[MALZEMEGIRIS_TESLIMKULLANICIID]=[Parameters.MALZEMEGIRIS_TESLIMKULLANICIID] AND [MALZEMEGIRIS_MALZEMEGRUPID]=[Parameters.MALZEMEGIRIS_MALZEMEGRUPID] ";
            rapor.RequestParameters = false;

            ReportPrintTool pt = new ReportPrintTool(rapor);
            pt.AutoShowParametersPanel = false;
            rapor.ShowPreview();
        }

        private void InitFormListe()
        {
            StringBuilder sb = new StringBuilder(1024);
            if (checkEditMalzemeTumKayit.Checked == false)
                sb.Append("Select top 7 ");
            if (checkEditMalzemeTumKayit.Checked == true)
                sb.Append("Select ");
            sb.Append("TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_ID, TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_ADI as 'MALZEME ADI', TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_ADET as 'Adet',");
            sb.Append("TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_TARIH as 'Tarih', TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_BIRIMFIYAT as 'B.Fiyat', TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_TOPLAMFIYAT as 'T.Fiyat', ");
            sb.Append("case when TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_PARABIRIMI=1 then 'TL' ");
            sb.Append("when TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_PARABIRIMI=2 then '€' ");
            sb.Append("when TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_PARABIRIMI=3 then '$' ");
            sb.Append("when TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_PARABIRIMI=4 then 'JPY' ");
            sb.Append("when TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_PARABIRIMI=5 then 'CHF' ");
            sb.Append("when TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_PARABIRIMI=6 then 'GBP' end 'P.BİRİMİ', ");
            sb.Append("TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_MALZEMEGRUPID ");
            sb.Append("from TBL_LST_MALZEMEGIRIS ");
            sb.Append("where TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_MALZEMELERID = " + MALZEMELER_ID.ToString());
            sb.Append(" order by MALZEMEGIRIS_TARIH desc ");
            DataTable dt = clSqlTanim.RunStoredProc(sb.ToString());

            gridControlGirisListe.DataSource = dt;
            this.SetGridFont(gridViewGirisListe, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewGirisListe.Columns["Adet"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewGirisListe.Columns["Adet"].SummaryItem.DisplayFormat = "{0} Adet";
            this.gridViewGirisListe.Columns["MALZEMEGIRIS_ID"].Visible = false;
            this.gridViewGirisListe.Columns["MALZEMEGIRIS_MALZEMEGRUPID"].Visible = false;

            this.gridViewGirisListe.BestFitColumns();
        }

        private void frmMalzemeGirisBilgileri_Load(object sender, EventArgs e)
        {
            InitFormKur();

            InitFormListe();

            bilgileriGetir();
        }

        private void simpleButtonMalzemeGirisKaydet_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        frmMalzemeTanimlama frmMalzemeler = ((frmMalzemeTanimlama)Application.OpenForms["frmMalzemeTanimlama"]);

        private void Kaydet()
        {
            StringBuilder sbI = new StringBuilder(1024);
            StringBuilder sbU = new StringBuilder(1024);
            var item = gridViewGirisListe.GetFocusedDataRow();

            try
            {
                if (yeniMalzemeGiris == true)
                {
                    sbI.Append("insert into TBL_LST_MALZEMEGIRIS ( MALZEMEGIRIS_MALZEMELERID, MALZEMEGIRIS_ADI, MALZEMEGIRIS_ADET, MALZEMEGIRIS_TARIH, MALZEMEGIRIS_BIRIMFIYAT, MALZEMEGIRIS_TOPLAMFIYAT, MALZEMEGIRIS_PARABIRIMI, MALZEMEGIRIS_SORGUBIRIMFIYAT, MALZEMEGIRIS_SORGUTOPLAMFIYAT, MALZEMEGIRIS_MALZEMEGRUPID, MALZEMEGIRIS_TESLIMKULLANICIID ) select");
                    sbI.AppendFormat("  {0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(MALZEMELER_ID.ToString()) ? "0" : MALZEMELER_ID.ToString()));
                    sbI.AppendFormat(" ,N{0}", clGenelTanim.tosqlstring(textEditMalzemeGirisAdi.Text.ToString(), 500, true));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMalzemeGirisAdedi.Text.ToString()) ? "0" : textEditMalzemeGirisAdedi.Text.ToString()));
                    sbI.AppendFormat(" ,{0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditMalzemeGirisTarih.DateTime).ToString("yyyy-MM-dd HH:mm") + "'");
                    sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(textEditMalzemeGirisBirimFiyat.Text.ToString().Replace(',', '.'), 10, true));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(textEditMalzemeGirisToplamFiyat.Text.ToString().Replace(',', '.'), 10, true));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeGirisParaBirimi.SecilenDeger().Id.ToString()));
                    if (comboBoxEditMalzemeGirisParaBirimi.SelectedIndex == 0)
                    {
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text) / malzemeEuroFiyat).ToString().Replace(',', '.'), 10, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text) / malzemeEuroFiyat).ToString().Replace(',', '.'), 10, true));
                    }
                    if (comboBoxEditMalzemeGirisParaBirimi.SelectedIndex == 1)
                    {
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(textEditMalzemeGirisBirimFiyat.Text.ToString().Replace(',', '.'), 10, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(textEditMalzemeGirisBirimFiyat.Text.ToString().Replace(',', '.'), 10, true));
                    }
                    if (comboBoxEditMalzemeGirisParaBirimi.SelectedIndex == 2)
                    {
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text) * usdEuroOran).ToString().Replace(',', '.'), 10, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text) * usdEuroOran).ToString().Replace(',', '.'), 10, true));
                    }
                    if (comboBoxEditMalzemeGirisParaBirimi.SelectedIndex == 3)
                    {
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text) * jpyEuroOran).ToString().Replace(',', '.'), 10, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text) * jpyEuroOran).ToString().Replace(',', '.'), 10, true));
                    }
                    if (comboBoxEditMalzemeGirisParaBirimi.SelectedIndex == 4)
                    {
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text) * chfEuroOran).ToString().Replace(',', '.'), 10, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text) * chfEuroOran).ToString().Replace(',', '.'), 10, true));
                    }
                    if (comboBoxEditMalzemeGirisParaBirimi.SelectedIndex == 5)
                    {
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text) * gbpEuroOran).ToString().Replace(',', '.'), 10, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text) * gbpEuroOran).ToString().Replace(',', '.'), 10, true));
                    }

                    sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeGirisYeri.SecilenDeger().Id.ToString()));
                    sbI.AppendFormat(", {0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeGirisKullanici.SecilenDeger().Id.ToString()));

                    clSqlTanim.RunStoredProc(sbI.ToString());

                    XtraMessageBox.Show("Giriş Eklendi...", "Bilgi...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    sbU.AppendFormat("update TBL_LST_MALZEMEGIRIS set ");
                    sbU.AppendFormat("  MALZEMEGIRIS_ADET={0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMalzemeGirisAdedi.Text.ToString()) ? "0" : textEditMalzemeGirisAdedi.Text.ToString()));
                    sbU.AppendFormat(" ,MALZEMEGIRIS_TARIH={0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditMalzemeGirisTarih.DateTime).ToString("yyyy-MM-dd HH:mm") + "'");
                    sbU.AppendFormat(" ,MALZEMEGIRIS_BIRIMFIYAT={0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text)).ToString().Replace(',', '.'), 10, true));
                    sbU.AppendFormat(" ,MALZEMEGIRIS_TOPLAMFIYAT={0}", clGenelTanim.tosqlstring((Convert.ToDecimal(textEditMalzemeGirisToplamFiyat.Text)).ToString().Replace(',', '.'), 10, true));
                    sbU.AppendFormat(" ,MALZEMEGIRIS_PARABIRIMI={0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeGirisParaBirimi.SecilenDeger().Id.ToString()));
                    sbU.AppendFormat(" ,MALZEMEGIRIS_MALZEMEGRUPID={0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeGirisYeri.SecilenDeger().Id.ToString()));
                    sbU.AppendFormat(" ,MALZEMEGIRIS_TESLIMKULLANICIID={0}", clGenelTanim.DBToInt32(comboBoxEditMalzemeGirisKullanici.SecilenDeger().Id.ToString()));
                    sbU.AppendFormat("  where MALZEMEGIRIS_ID={0}", clGenelTanim.DBToInt32(item["MALZEMEGIRIS_ID"]));
                    clSqlTanim.RunStoredProc(sbU.ToString());

                    XtraMessageBox.Show("Giriş GÜncellendi...", "Bilgi...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                frmMalzemeler.InitForm();
                InitFormListe();
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }

        private void textEditMalzemeGirisFiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != ',')
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == (char)13) //Enter tuşuna basıldımı
                {

                    Single birimfiyat = Convert.ToSingle(textEditMalzemeGirisBirimFiyat.Text);
                    textEditMalzemeGirisBirimFiyat.Text = string.Format("{0:c}", double.Parse(textEditMalzemeGirisBirimFiyat.Text));
                    //Parabirimine dönüştür tekrar aynı text kutusuna aktar
                }
            }
            catch (Exception)
            {

                MessageBox.Show("BİRİM FİYAT GEÇERSİZ");
            }
        }

        private void textEditMalzemeGirisAdedi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textEditMalzemeGirisAdedi_TextChanged(object sender, EventArgs e)
        {
            if (textEditMalzemeGirisAdedi.Text != string.Empty && textEditMalzemeGirisBirimFiyat.Text != string.Empty)
            {
                malzemeToplamFiyat = Convert.ToDecimal(textEditMalzemeGirisAdedi.Text) * Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text);
                textEditMalzemeGirisToplamFiyat.Text = malzemeToplamFiyat.ToString();
            }
        }

        private void textEditMalzemeGirisFiyat_TextChanged(object sender, EventArgs e)
        {
            if (textEditMalzemeGirisAdedi.Text != string.Empty && textEditMalzemeGirisBirimFiyat.Text != string.Empty)
            {
                malzemeToplamFiyat = Convert.ToDecimal(textEditMalzemeGirisAdedi.Text) * Convert.ToDecimal(textEditMalzemeGirisBirimFiyat.Text); ;
                textEditMalzemeGirisToplamFiyat.Text = malzemeToplamFiyat.ToString();
            }
        }

        private void simpleButtonMalzemeGirisIptal_Click(object sender, EventArgs e)
        {
            if (yeniMalzemeGiris == false)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("Kayıtlar Silinsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    StringBuilder sb = new StringBuilder(1024);
                    var item = gridViewGirisListe.GetFocusedDataRow();
                    if (item != null)
                    {
                        sb.Append("DELETE FROM TBL_LST_MALZEMEGIRIS ");
                        sb.AppendFormat("WHERE MALZEMEGIRIS_ID={0}", clGenelTanim.DBToInt32(item["MALZEMEGIRIS_ID"]));
                        clSqlTanim.RunStoredProc(sb.ToString());
                    }
                    XtraMessageBox.Show("Kayıt Silindi ...");
                    frmMalzemeler.InitForm();
                    InitFormListe();
                }
            }
            else { XtraMessageBox.Show("Silmek istediğiniz kaydı seçmelisiniz !", "Bilgi ...", MessageBoxButtons.OK, MessageBoxIcon.Error); };
        }
    }
}