using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeTalepIstatistikleri : FrmBase
    {
        private int nMALZEMELER_TOPLAMSTOKMIKTARI = 0;

        public frmMalzemeTalepIstatistikleri()
        {
            InitializeComponent();
            layoutControlMalzemeTalepIstatiskleri.LayoutKontrolleriniSifirla();
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
            sb.AppendFormat("WHERE CAST(MALZEMEGIRIS_TARIH AS DATE) >= @basTarih ");
            sb.AppendFormat("AND CAST(MALZEMEGIRIS_TARIH AS DATE) <= @bitTarih ");
            sb.Append("GROUP BY  MALZEMEGIRIS_MALZEMELERID) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("JOIN (Select  MALZEMECIKIS_MALZEMELERID, isnull(Sum(MALZEMECIKIS_ADET),0) CikislarToplam FROM TBL_LST_MALZEMECIKIS ");
            sb.AppendFormat("WHERE CAST(MALZEMECIKIS_TARIHI AS DATE) >= @basTarih ");
            sb.AppendFormat("AND CAST(MALZEMECIKIS_TARIHI AS DATE) <= @bitTarih ");
            sb.Append("GROUP BY  MALZEMECIKIS_MALZEMELERID) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("WHERE rng=1 and rnc=1 ORDER BY 1 DESC");


            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString(), new[] {
                new SqlParameter("@basTarih", SqlDbType.Date) { Value = dateEditBaslangicTarih.DateTime.Date },
                new SqlParameter("@bitTarih", SqlDbType.Date) { Value = dateEditBitisTarih.DateTime.Date }
            });
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
        }

        public void InitForm3()
        {
        }

        public void InitForm4()
        {
        }

        public void InitForm5()
        {
        }

        public void InitForm6()
        {
        }

        public void InitForm7()
        {
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
        }

        private void frmMalzemeTalepIstatiskleri_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void barButtonItemGrafik_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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


                        decimal sorguToplamFiyatG = (decimal)clGenelTanim.DBToDecimal(item["MALZEME GİRİŞ BİRİM FİYAT"]) * clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text);
                        decimal birimFiyatG = (decimal)clGenelTanim.DBToDecimal(item["MALZEME GİRİŞ BİRİM FİYAT1"]) * clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text);
                        clSqlTanim.RunStoredProc(
                            "UPDATE TBL_LST_MALZEMEGIRIS SET MALZEMEGIRIS_ADET=@adet, MALZEMEGIRIS_SORGUTOPLAMFIYAT=@sorguToplam, MALZEMEGIRIS_BIRIMFIYAT=@birimFiyat WHERE MALZEMEGIRIS_ID=@id",
                            new[] {
                                new SqlParameter("@adet",        clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text)),
                                new SqlParameter("@sorguToplam", SqlDbType.Decimal) { Value = sorguToplamFiyatG },
                                new SqlParameter("@birimFiyat",  SqlDbType.Decimal) { Value = birimFiyatG },
                                new SqlParameter("@id",          clGenelTanim.DBToInt32(item["MALZEMEGIRIS_ID"]))
                            });

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

                        decimal sorguToplamFiyatC = (decimal)clGenelTanim.DBToDecimal(item["MALZEME ÇIKIŞ BİRİM FİYAT"]) * clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text);
                        decimal birimFiyatC = (decimal)clGenelTanim.DBToDecimal(item["MALZEME ÇIKIŞ BİRİM FİYAT1"]) * clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text);
                        clSqlTanim.RunStoredProc(
                            "UPDATE TBL_LST_MALZEMECIKIS SET MALZEMECIKIS_ADET=@adet, MALZEMECIKIS_SORGUTOPLAMFIYAT=@sorguToplam, MALZEMECIKIS_BIRIMFIYAT=@birimFiyat WHERE MALZEMECIKIS_ID=@id",
                            new[] {
                                new SqlParameter("@adet",        clGenelTanim.DBToInt32(frmAdet.textEditAdet.Text)),
                                new SqlParameter("@sorguToplam", SqlDbType.Decimal) { Value = sorguToplamFiyatC },
                                new SqlParameter("@birimFiyat",  SqlDbType.Decimal) { Value = birimFiyatC },
                                new SqlParameter("@id",          clGenelTanim.DBToInt32(item["MALZEMECIKIS_ID"]))
                            });

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

        private void xtraTabControlDepoHareketleri_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
        }
    }
}
