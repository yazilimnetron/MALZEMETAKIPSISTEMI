using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeCikisEkle : FrmBase
    {
        private bool yeniMalzemeCikis = true;

        public int MALZEMELER_ID = 0, MALZEMECIKIS_DEPARTMANID = 0, MALZEMECIKIS_TESLIMKULLANICIID=0; //nMALZEMELER_STOKMIKTARI = 0;

        decimal malzemeToplamFiyat = 0m;

        decimal malzemeUsdFiyat = 0m, malzemeEuroFiyat = 0m, malzemeJpyFiyat = 0m, malzemeChfFiyat = 0m, malzemeGbpFiyat = 0m, malzemeRonFiyat = 0m;

        decimal usdEuroOran = 0m, jpyEuroOran = 0m, chfEuroOran = 0m, gbpEuroOran = 0m, ronEuroOran = 0m;
        public frmMalzemeCikisEkle()
        {
            InitializeComponent();
            layoutControlMalzemeCikis.LayoutKontrolleriniSifirla();
        }

        void bilgileriGetir()
        {
            try
            {
                comboBoxEditMalzemeCikisYeri.Doldur("SELECT MALZEME_DEPARTMANID, MALZEME_DEPARTMANADI AS 'MALZEME DEPARTMANADI' FROM TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK)  WHERE ISNULL (MALZEME_DEPARTMANDURUM,0)=1 ORDER BY MALZEME_DEPARTMANADI", true);
                comboBoxEditMalzemeCikisYeri.SelectedIndex = 0;

                comboBoxEditMalzemeCikisKullanici.Doldur("select MALZEMEKULLANICI_ID, MALZEMEKULLANICI_ADI from TBL_LST_MALZEMEKULLANICILAR where isnull(MALZEMEKULLANICI_DURUM,0) =1 and isnull(MALZEMEKULLANICI_SIPARISKAPAT,0)=1 order by 2", true);
                comboBoxEditMalzemeCikisKullanici.SelectedIndex = 0;



                if (string.IsNullOrEmpty(textEditMalzemeCikisAdedi.Text))
                {
                    textEditMalzemeCikisAdedi.Text = "0";
                }

                //StringBuilder sb = new StringBuilder();
                //sb.Append("Select top 1 TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_ID, TBL_LST_MALZEMELER.MALZEME_ADI," +
                //          "TBL_LST_MALZEMELER.MALZEME_STOKMIKTARI, TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_BIRIMFIYAT," +
                //          "TBL_LST_MALZEMELER.MALZEME_PARABIRIMI ");
                //sb.Append("from TBL_LST_MALZEMELER, TBL_LST_MALZEMEGIRIS ");
                //sb.Append("where TBL_LST_MALZEMELER.MALZEME_ID=TBL_LST_MALZEMEGIRIS.MALZEMEGIRIS_MALZEMELERID ");
                //sb.Append("and TBL_LST_MALZEMELER.MALZEME_ID = " + nMALZEMELER_ID.ToString());
                //sb.Append(" order by 1 desc");
                //DataTable dt = clSqlTanim.RunStoredProc(sb.ToString());

                DataTable dt = clSqlTanim.RunStoredProc(
                    "SELECT TOP 1 mg.MALZEMEGIRIS_ADI, mg.MALZEMEGIRIS_BIRIMFIYAT, mg.MALZEMEGIRIS_PARABIRIMI " +
                    "FROM TBL_LST_MALZEMEGIRIS mg WHERE mg.MALZEMEGIRIS_MALZEMELERID=@malzemeId ORDER BY MALZEMEGIRIS_TARIH DESC",
                    new[] { new SqlParameter("@malzemeId", MALZEMELER_ID) });
                foreach (DataRow dr in dt.Rows)
                {
                    textEditMalzemeCikisAdi.Text = dr["MALZEMEGIRIS_ADI"].ToString();
                    textEditMalzemeCikisBirimFiyat.Text = dr["MALZEMEGIRIS_BIRIMFIYAT"].ToString();
                    comboBoxEditCikisParaBirimi.DegerSec(clGenelTanim.DBToInt32(dr["MALZEMEGIRIS_PARABIRIMI"]));
                    //nMALZEMELER_STOKMIKTARI = Convert.ToInt32(dr["MALZEME_STOKMIKTARI"].ToString());
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gridViewListe_DoubleClick(object sender, EventArgs e)
        {
            yeniMalzemeCikis = false;
            var item = gridViewCikisListe.GetFocusedDataRow();

            if (item != null)
            {
                StringBuilder sb = new StringBuilder(1024);
                sb.Append("Select TBL_LST_MALZEMECIKIS.MALZEMECIKIS_ID, TBL_LST_MALZEMECIKIS.MALZEMECIKIS_ADI as 'MALZEME ADI', TBL_LST_MALZEMECIKIS.MALZEMECIKIS_ADET as 'Adet', ");
                sb.Append("TBL_LST_MALZEMECIKIS.MALZEMECIKIS_TARIHI as 'Tarih', TBL_LST_MALZEMECIKIS.MALZEMECIKIS_SORGUBIRIMFIYAT as 'B.Fiyat', TBL_LST_MALZEMECIKIS.MALZEMECIKIS_SORGUTOPLAMFIYAT as 'T.Fiyat', ");
                sb.Append("TBL_LST_MALZEMECIKIS.MALZEMECIKIS_PARABIRIMI as 'P.BİRİMİ', TBL_LST_MALZEMECIKIS.MALZEMECIKIS_DEPARTMANID,TBL_LST_MALZEMECIKIS.MALZEMECIKIS_TESLIMKULLANICIID ");
                sb.Append("from TBL_LST_MALZEMECIKIS ");
                sb.Append("where TBL_LST_MALZEMECIKIS.MALZEMECIKIS_ID=@id");
                DataTable dt = clSqlTanim.RunStoredProc(sb.ToString(), new[] { new SqlParameter("@id", clGenelTanim.DBToInt32(item["MALZEMECIKIS_ID"])) });
                if (dt != null && dt.Rows.Count > 0)
                {
                    textEditMalzemeCikisAdi.Text = clGenelTanim.DBToString(dt.Rows[0]["MALZEME ADI"]);
                    textEditMalzemeCikisAdedi.Text = clGenelTanim.DBToInt32(dt.Rows[0]["Adet"]).ToString();
                    dateEditMalzemeCikisTarih.DateTime = clGenelTanim.DBToDate(dt.Rows[0]["Tarih"]);
                    textEditMalzemeCikisBirimFiyat.Text = clGenelTanim.DBToString(dt.Rows[0]["B.Fiyat"]).ToString();
                    textEditMalzemeCikisToplamFiyat.Text = clGenelTanim.DBToString(dt.Rows[0]["T.Fiyat"]).ToString();
                    comboBoxEditCikisParaBirimi.DegerSec(clGenelTanim.DBToInt32(dt.Rows[0]["P.BİRİMİ"]));
                    comboBoxEditMalzemeCikisYeri.DegerSec(clGenelTanim.DBToInt32(dt.Rows[0]["MALZEMECIKIS_DEPARTMANID"]));
                    comboBoxEditMalzemeCikisKullanici.DegerSec(clGenelTanim.DBToInt32(dt.Rows[0]["MALZEMECIKIS_TESLIMKULLANICIID"]));

                }
            }
        }

        private void InitFormListe()
        {

            StringBuilder sb = new StringBuilder(1024);
            if (checkEditMalzemeTumKayit.Checked == false)
                sb.Append("Select top 7 ");
            if (checkEditMalzemeTumKayit.Checked == true)
                sb.Append("Select ");
            sb.Append("TBL_LST_MALZEMECIKIS.MALZEMECIKIS_ID, TBL_LST_MALZEMECIKIS.MALZEMECIKIS_ADI as 'MALZEME ADI', TBL_LST_MALZEMECIKIS.MALZEMECIKIS_ADET as 'Adet', ");
            sb.Append("TBL_LST_MALZEMECIKIS.MALZEMECIKIS_TARIHI as 'Tarih', TBL_LST_MALZEMECIKIS.MALZEMECIKIS_SORGUBIRIMFIYAT as 'B.Fiyat', TBL_LST_MALZEMECIKIS.MALZEMECIKIS_SORGUTOPLAMFIYAT as 'T.Fiyat', ");
            sb.Append("case when TBL_LST_MALZEMECIKIS.MALZEMECIKIS_PARABIRIMI=1 then 'TL' ");
            sb.Append("when TBL_LST_MALZEMECIKIS.MALZEMECIKIS_PARABIRIMI=2 then '€' ");
            sb.Append("when TBL_LST_MALZEMECIKIS.MALZEMECIKIS_PARABIRIMI=3 then '$' ");
            sb.Append("when TBL_LST_MALZEMECIKIS.MALZEMECIKIS_PARABIRIMI=4 then 'JPY' ");
            sb.Append("when TBL_LST_MALZEMECIKIS.MALZEMECIKIS_PARABIRIMI=5 then 'CHF' ");
            sb.Append("when TBL_LST_MALZEMECIKIS.MALZEMECIKIS_PARABIRIMI=6 then 'GBP' end 'P.BİRİMİ', ");
            sb.Append("TBL_LST_MALZEMECIKIS.MALZEMECIKIS_DEPARTMANID ");
            sb.Append("from TBL_LST_MALZEMECIKIS ");
            sb.Append("where TBL_LST_MALZEMECIKIS.MALZEMECIKIS_MALZEMELERID=@malzemeId");
            sb.Append(" order by MALZEMECIKIS_TARIHI desc ");
            DataTable dt = clSqlTanim.RunStoredProc(sb.ToString(), new[] { new SqlParameter("@malzemeId", MALZEMELER_ID) });

            gridControlCikisListe.DataSource = dt;
            this.SetGridFont(gridViewCikisListe, new Font("Tahoma", 10, FontStyle.Bold));


            this.gridViewCikisListe.Columns["Adet"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewCikisListe.Columns["Adet"].SummaryItem.DisplayFormat = "{0} Adet";
            this.gridViewCikisListe.Columns["MALZEMECIKIS_ID"].Visible = false;
            this.gridViewCikisListe.Columns["MALZEMECIKIS_DEPARTMANID"].Visible = false;

            this.gridViewCikisListe.BestFitColumns();
        }

        private void checkEditMalzemeTumKayit_CheckedChanged(object sender, EventArgs e)
        {
            InitFormListe();
        }

        private void simpleButtonMalzemeCikisIptal_Click(object sender, EventArgs e)
        {
            if (yeniMalzemeCikis == false)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("Kayıtlar Silinsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    var item = gridViewCikisListe.GetFocusedDataRow();
                    if (item != null)
                    {
                        clSqlTanim.ExecuteNonQuery(
                            "DELETE FROM TBL_LST_MALZEMECIKIS WHERE MALZEMECIKIS_ID=@id",
                            new[] { new SqlParameter("@id", clGenelTanim.DBToInt32(item["MALZEMECIKIS_ID"])) });
                    }
                    XtraMessageBox.Show("Kayıt Silindi ...");

                    (Application.OpenForms["frmMalzemeTanimlama"] as frmMalzemeTanimlama)?.InitForm();
                    InitFormListe();

                }
            }
            else { XtraMessageBox.Show("Silmek istediğiniz kaydı seçmelisiniz !", "Bilgi ...", MessageBoxButtons.OK, MessageBoxIcon.Error); };

        }

        private void simpleButtonMalzemeCikisYazdir_Click(object sender, EventArgs e)
        {
            MALZEMECIKIS_DEPARTMANID = clGenelTanim.DBToInt32(comboBoxEditMalzemeCikisYeri.SecilenDeger().Id.ToString());
            MALZEMECIKIS_TESLIMKULLANICIID = clGenelTanim.DBToInt32(comboBoxEditMalzemeCikisKullanici.SecilenDeger().Id.ToString());

            XtraReport3 rapor = new XtraReport3();
            Parameter param1 = new Parameter();
            Parameter param2 = new Parameter();

            param1.Name = "MALZEMECIKIS_TESLIMKULLANICIID";
            param1.Type = typeof(System.Int32);
            param1.Value = MALZEMECIKIS_TESLIMKULLANICIID.ToString();
            param1.Visible = false;

            param2.Name = "MALZEMECIKIS_DEPARTMANID";
            param2.Type = typeof(System.Int32);
            param2.Value = MALZEMECIKIS_DEPARTMANID.ToString();
            param2.Visible = false;

            rapor.Parameters.Add(param1);
            rapor.Parameters.Add(param2);

            rapor.FilterString = "[MALZEMECIKIS_TESLIMKULLANICIID]=[Parameters.MALZEMECIKIS_TESLIMKULLANICIID] AND [MALZEMECIKIS_DEPARTMANID]=[Parameters.MALZEMECIKIS_DEPARTMANID] ";
            rapor.RequestParameters = false;

            ReportPrintTool pt = new ReportPrintTool(rapor);
            pt.AutoShowParametersPanel = false;
            rapor.ShowPreview();
        }

        private void simpleButtonMalzemeCikisKaydet_Click(object sender, EventArgs e)
        {
            if (comboBoxEditMalzemeCikisYeri.SelectedIndex < 1)
            {
                XtraMessageBox.Show("MASRAF YERİ SEÇMELİSİNİZ !!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtStok = clSqlTanim.RunStoredProc(
                "SELECT ISNULL(SUM(g.MALZEMEGIRIS_ADET),0) - ISNULL(SUM(c.MALZEMECIKIS_ADET),0) AS STOK " +
                "FROM TBL_LST_MALZEMELER m " +
                "LEFT JOIN TBL_LST_MALZEMEGIRIS g ON g.MALZEMEGIRIS_MALZEMELERID = m.MALZEME_ID " +
                "LEFT JOIN TBL_LST_MALZEMECIKIS c ON c.MALZEMECIKIS_MALZEMELERID = m.MALZEME_ID " +
                "WHERE m.MALZEME_ID = @malzemeId " +
                "GROUP BY m.MALZEME_ID",
                new[] { new SqlParameter("@malzemeId", MALZEMELER_ID) });

            int mevcutStok = (dtStok != null && dtStok.Rows.Count > 0)
                ? clGenelTanim.DBToInt32(dtStok.Rows[0]["STOK"])
                : 0;

            if (mevcutStok < Convert.ToInt32(textEditMalzemeCikisAdedi.Text))
            {
                XtraMessageBox.Show(
                    $"STOK MİKTARI YETERLİ DEĞİL! Mevcut stok: {mevcutStok} adet.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Convert.ToInt32(textEditMalzemeCikisAdedi.Text) < 1)
            {
                XtraMessageBox.Show("ADET MİKTARI 1 ' DEN KÜÇÜK OLAMAZ !!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textEditMalzemeCikisAdedi.Text == string.Empty)
            {
                XtraMessageBox.Show("STOK MİKTARI GİRMELİSİNİZ !!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Kaydet();
        }

        private void InitFormKur()
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
            comboBoxEditCikisParaBirimi.Doldur(items);
            comboBoxEditCikisParaBirimi.SelectedIndex = 0;
        }

        private void frmMalzemeCikisEkle_Load(object sender, EventArgs e)
        {
            InitFormKur();

            bilgileriGetir();

            InitFormListe();

            //this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);

            //// no larger than screen size
            //this.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            //this.AutoSize = true;
            //this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }



        private void Kaydet()
        {
            var item = gridViewCikisListe.GetFocusedDataRow();

            try
            {
                decimal birimFiyat = Convert.ToDecimal(textEditMalzemeCikisBirimFiyat.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                decimal toplamFiyat = Convert.ToDecimal(textEditMalzemeCikisToplamFiyat.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                int adet = clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMalzemeCikisAdedi.Text) ? "0" : textEditMalzemeCikisAdedi.Text);

                decimal sorguBirimFiyat;
                switch (comboBoxEditCikisParaBirimi.SelectedIndex)
                {
                    case 0: sorguBirimFiyat = malzemeEuroFiyat > 0 ? birimFiyat / malzemeEuroFiyat : birimFiyat; break;
                    case 1: sorguBirimFiyat = birimFiyat; break;
                    case 2: sorguBirimFiyat = birimFiyat * usdEuroOran; break;
                    case 3: sorguBirimFiyat = birimFiyat * jpyEuroOran; break;
                    case 4: sorguBirimFiyat = birimFiyat * chfEuroOran; break;
                    case 5: sorguBirimFiyat = birimFiyat * gbpEuroOran; break;
                    default: sorguBirimFiyat = birimFiyat; break;
                }

                if (yeniMalzemeCikis == true)
                {
                    string insertSql = "INSERT INTO TBL_LST_MALZEMECIKIS " +
                        "(MALZEMECIKIS_MALZEMELERID, MALZEMECIKIS_ADI, MALZEMECIKIS_ADET, MALZEMECIKIS_DEPARTMANID, " +
                        "MALZEMECIKIS_TARIHI, MALZEMECIKIS_BIRIMFIYAT, MALZEMECIKIS_TOPLAMFIYAT, MALZEMECIKIS_PARABIRIMI, " +
                        "MALZEMECIKIS_SORGUBIRIMFIYAT, MALZEMECIKIS_SORGUTOPLAMFIYAT, MALZEMECIKIS_TESLIMKULLANICIID) " +
                        "VALUES (@malzemeId, @cikisAdi, @adet, @departmanId, @tarih, @birimFiyat, @toplamFiyat, " +
                        "@paraBirimi, @sorguBirimFiyat, @sorguToplamFiyat, @teslimKullanici)";
                    var insertParams = new SqlParameter[]
                    {
                        new SqlParameter("@malzemeId", MALZEMELER_ID),
                        new SqlParameter("@cikisAdi", textEditMalzemeCikisAdi.Text),
                        new SqlParameter("@adet", adet),
                        new SqlParameter("@departmanId", clGenelTanim.DBToInt32(comboBoxEditMalzemeCikisYeri.SecilenDeger().Id.ToString())),
                        new SqlParameter("@tarih", Convert.ToDateTime(dateEditMalzemeCikisTarih.DateTime).ToString("yyyy-MM-dd HH:mm")),
                        new SqlParameter("@birimFiyat", birimFiyat.ToString("0.####", CultureInfo.InvariantCulture)),
                        new SqlParameter("@toplamFiyat", toplamFiyat.ToString("0.####", CultureInfo.InvariantCulture)),
                        new SqlParameter("@paraBirimi", clGenelTanim.DBToInt32(comboBoxEditCikisParaBirimi.SecilenDeger().Id.ToString())),
                        new SqlParameter("@sorguBirimFiyat", sorguBirimFiyat.ToString("0.####", CultureInfo.InvariantCulture)),
                        new SqlParameter("@sorguToplamFiyat", (sorguBirimFiyat * adet).ToString("0.####", CultureInfo.InvariantCulture)),
                        new SqlParameter("@teslimKullanici", clGenelTanim.DBToInt32(comboBoxEditMalzemeCikisKullanici.SecilenDeger().Id.ToString())),
                    };
                    clSqlTanim.ExecuteNonQuery(insertSql, insertParams);
                    XtraMessageBox.Show("Çıkış Eklendi...", "Bilgi...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string updateSql = "UPDATE TBL_LST_MALZEMECIKIS SET " +
                        "MALZEMECIKIS_ADET=@adet, MALZEMECIKIS_TARIHI=@tarih, MALZEMECIKIS_BIRIMFIYAT=@birimFiyat, " +
                        "MALZEMECIKIS_TOPLAMFIYAT=@toplamFiyat, MALZEMECIKIS_PARABIRIMI=@paraBirimi, " +
                        "MALZEMECIKIS_DEPARTMANID=@departmanId, MALZEMECIKIS_TESLIMKULLANICIID=@teslimKullanici " +
                        "WHERE MALZEMECIKIS_ID=@id";
                    var updateParams = new SqlParameter[]
                    {
                        new SqlParameter("@adet", adet),
                        new SqlParameter("@tarih", Convert.ToDateTime(dateEditMalzemeCikisTarih.DateTime).ToString("yyyy-MM-dd HH:mm")),
                        new SqlParameter("@birimFiyat", birimFiyat.ToString("0.####", CultureInfo.InvariantCulture)),
                        new SqlParameter("@toplamFiyat", toplamFiyat.ToString("0.####", CultureInfo.InvariantCulture)),
                        new SqlParameter("@paraBirimi", clGenelTanim.DBToInt32(comboBoxEditCikisParaBirimi.SecilenDeger().Id.ToString())),
                        new SqlParameter("@departmanId", clGenelTanim.DBToInt32(comboBoxEditMalzemeCikisYeri.SecilenDeger().Id.ToString())),
                        new SqlParameter("@teslimKullanici", clGenelTanim.DBToInt32(comboBoxEditMalzemeCikisKullanici.SecilenDeger().Id.ToString())),
                        new SqlParameter("@id", clGenelTanim.DBToInt32(item["MALZEMECIKIS_ID"])),
                    };
                    clSqlTanim.ExecuteNonQuery(updateSql, updateParams);
                    XtraMessageBox.Show("Çıkış Güncellendi...", "Bilgi...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                (Application.OpenForms["frmMalzemeTanimlama"] as frmMalzemeTanimlama)?.InitForm();
                InitFormListe();
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message, "BİLGİ ...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void textEditMalzemeCikisAdedi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }



        private void textEditMalzemeCikisAdedi_TextChanged(object sender, EventArgs e)
        {
            if (textEditMalzemeCikisAdedi.Text != string.Empty && textEditMalzemeCikisBirimFiyat.Text != string.Empty)
            {
                malzemeToplamFiyat = Convert.ToDecimal(textEditMalzemeCikisAdedi.Text) * Convert.ToDecimal(textEditMalzemeCikisBirimFiyat.Text); ;
                textEditMalzemeCikisToplamFiyat.Text = malzemeToplamFiyat.ToString();
            }
        }

        private void textEditMalzemeCikisBirimFiyat_TextChanged(object sender, EventArgs e)
        {
            if (textEditMalzemeCikisAdedi.Text != string.Empty && textEditMalzemeCikisBirimFiyat.Text != string.Empty)
            {
                malzemeToplamFiyat = Convert.ToDecimal(textEditMalzemeCikisAdedi.Text) * Convert.ToDecimal(textEditMalzemeCikisBirimFiyat.Text); ;
                textEditMalzemeCikisToplamFiyat.Text = malzemeToplamFiyat.ToString();
            }
        }

        private void textEditMalzemeCikisBirimFiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

    }
}
