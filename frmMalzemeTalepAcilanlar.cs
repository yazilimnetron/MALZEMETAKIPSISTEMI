using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using MALZEMETAKIPSISTEMI;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeTalepAcilanlar : Form
    {
        public frmMalzemeTalepAcilanlar()
        {
            InitializeComponent();
            layoutControlMalzemeOtoTalepDetay.LayoutKontrolleriniSifirla();
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

        private void frmMalzemeOtoTalepDetay_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT s.MALZEME_ID, case when isnull(t.MALZEMETALEP_DURUM,0)= 0 then 'T.Gelmedi'  when isnull(t.MALZEMETALEP_DURUM,0)= 1 then 'T.Geldi' else '<<Yok>>' end 'TALEP DURUM', ");
            sb.Append("t.MALZEMETALEP_NO AS 'TALEP NO', (ISNULL(t.MALZEMETALEP_ADET,0)) AS ' MALZEME SİPARİŞ ADET', CONVERT(VARCHAR(10),MALZEMETALEP_TARIHI ,121) AS 'TALEP TARİHİ' , ");
            sb.Append("case when s.MALZEME_TURU=1 then 'NLAG'  when s.MALZEME_TURU=2 then 'UNBW' else '<<Seçiniz>>' end 'MALZEME TURU', convert(nvarchar,s.MALZEME_MATERYAL) AS 'MALZEME MATERYEL NO', ");
            sb.Append("s.MALZEME_ADI AS 'MALZEME ADI',(SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS mg WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC ) AS 'MALZEME BİRİM FİYAT',");
            sb.Append("(SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS mg WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC )*(ISNULL(t.MALZEMETALEP_ADET,0)) AS 'MALZEME TOPLAM FİYAT' ,isnull(g.GirislerToplam,0) as 'MALZEME GİRİŞ ADET', isnull(c.CikislarToplam,0) as 'MALZEME ÇIKIŞ ADET', ");
            sb.Append("(isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) AS 'MALZEME STOK ADET', ISNULL(s.MALZEME_MAXADET,0) AS 'MALZEME MAX ADET', ISNULL(s.MALZEME_MINADET,0) AS 'MALZEME MİN ADET', ");
            sb.Append("s.MALZEME_GRUBU AS 'MALZEME GRUBU', s.MALZEME_PARCANO AS 'MALZEME PARÇANO', e.MALZEMEKATEGORI_ADI +' '+'(' + e.MALZEMEKATEGORI_KODU +')' AS 'MALZEME SATINALMA KATEGORISI' ");
            sb.Append("FROM TBL_LST_MALZEMELER s ");
            sb.Append("LEFT OUTER JOIN ( Select  MALZEMEGIRIS_MALZEMELERID, Sum(MALZEMEGIRIS_ADET) GirislerToplam FROM TBL_LST_MALZEMEGIRIS GROUP BY  MALZEMEGIRIS_MALZEMELERID  ) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("LEFT OUTER JOIN ( Select  MALZEMECIKIS_MALZEMELERID, Sum(MALZEMECIKIS_ADET) CikislarToplam FROM TBL_LST_MALZEMECIKIS GROUP BY  MALZEMECIKIS_MALZEMELERID  ) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("JOIN TBL_LST_MALZEMETALEP t on t.MALZEMETALEP_MALZEMELERID=s.MALZEME_ID ");
            sb.Append("JOIN TBL_LST_MALZEMEANAGRUPLAR d on s.MALZEME_ANAGRUBU=d.MALZEMEANAGRUP_ID ");
            sb.Append("LEFT JOIN TBL_LST_MALZEMEKATEGORILER e on s.MALZEME_SATINALMAKATEGORI=e.MALZEMEKATEGORI_ID ");
            sb.AppendFormat("WHERE CONVERT(VARCHAR(10),t.MALZEMETALEP_TARIHI ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.AppendFormat("AND CONVERT(VARCHAR(10),t.MALZEMETALEP_TARIHI ,121)<={0} ", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            //sb.Append("AND isnull(t.MALZEMETALEP_DURUM,0)= 0 ");
            sb.Append("ORDER BY t.MALZEMETALEP_TARIHI desc");

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlMalzemeTalepleri.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepleri.Columns["TALEP NO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepleri.Columns["TALEP NO"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;


            this.SetGridFont(gridViewMalzemeTalepleri, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewMalzemeTalepleri.Columns["MALZEME STOK ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewMalzemeTalepleri.Columns["MALZEME STOK ADET"].SummaryItem.DisplayFormat = "{0} Adet";

            this.gridViewMalzemeTalepleri.BestFitColumns();
        }

        public void InitForm1()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT s.MALZEME_ID, case when isnull(t.MALZEMETALEP_DURUM,0)= 0 then 'T.Gelmedi'  when isnull(t.MALZEMETALEP_DURUM,0)= 1 then 'T.Geldi' else '<<Yok>>' end 'TALEP DURUM', ");
            sb.Append("t.MALZEMETALEP_NO AS 'TALEP NO', (ISNULL(t.MALZEMETALEP_ADET,0)) AS ' MALZEME SİPARİŞ ADET', CONVERT(VARCHAR(10),MALZEMETALEP_TARIHI ,121) AS 'TALEP TARİHİ' , ");
            sb.Append("case when s.MALZEME_TURU=1 then 'NLAG'  when s.MALZEME_TURU=2 then 'UNBW' else '<<Seçiniz>>' end 'MALZEME TURU', convert(nvarchar,s.MALZEME_MATERYAL) AS 'MALZEME MATERYEL NO', ");
            sb.Append("s.MALZEME_ADI AS 'MALZEME ADI',(SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS mg WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC ) AS 'MALZEME BİRİM FİYAT',");
            sb.Append("(SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS mg WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC )*(ISNULL(t.MALZEMETALEP_ADET,0)) AS 'MALZEME TOPLAM FİYAT' ,isnull(g.GirislerToplam,0) as 'MALZEME GİRİŞ ADET', isnull(c.CikislarToplam,0) as 'MALZEME ÇIKIŞ ADET', ");
            sb.Append("(isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) AS 'MALZEME STOK ADET', ISNULL(s.MALZEME_MAXADET,0) AS 'MALZEME MAX ADET', ISNULL(s.MALZEME_MINADET,0) AS 'MALZEME MİN ADET', ");
            sb.Append("s.MALZEME_GRUBU AS 'MALZEME GRUBU', s.MALZEME_PARCANO AS 'MALZEME PARÇANO', e.MALZEMEKATEGORI_ADI +' '+'(' + e.MALZEMEKATEGORI_KODU +')' AS 'MALZEME SATINALMA KATEGORISI' ");
            sb.Append("FROM TBL_LST_MALZEMELER s ");
            sb.Append("LEFT OUTER JOIN ( Select  MALZEMEGIRIS_MALZEMELERID, Sum(MALZEMEGIRIS_ADET) GirislerToplam FROM TBL_LST_MALZEMEGIRIS GROUP BY  MALZEMEGIRIS_MALZEMELERID  ) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("LEFT OUTER JOIN ( Select  MALZEMECIKIS_MALZEMELERID, Sum(MALZEMECIKIS_ADET) CikislarToplam FROM TBL_LST_MALZEMECIKIS GROUP BY  MALZEMECIKIS_MALZEMELERID  ) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("JOIN TBL_LST_MALZEMETALEP t on t.MALZEMETALEP_MALZEMELERID=s.MALZEME_ID ");
            sb.Append("JOIN TBL_LST_MALZEMEANAGRUPLAR d on s.MALZEME_ANAGRUBU=d.MALZEMEANAGRUP_ID ");
            sb.Append("LEFT JOIN TBL_LST_MALZEMEKATEGORILER e on s.MALZEME_SATINALMAKATEGORI=e.MALZEMEKATEGORI_ID ");
            sb.AppendFormat("WHERE CONVERT(VARCHAR(10),t.MALZEMETALEP_TARIHI ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.AppendFormat("AND CONVERT(VARCHAR(10),t.MALZEMETALEP_TARIHI ,121)<={0} ", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.Append("AND isnull(t.MALZEMETALEP_DURUM,0)= 1 ");
            sb.Append("ORDER BY t.MALZEMETALEP_TARIHI desc");

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlMalzemeTalepleri.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepleri.Columns["TALEP NO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepleri.Columns["TALEP NO"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;


            this.SetGridFont(gridViewMalzemeTalepleri, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewMalzemeTalepleri.Columns["MALZEME STOK ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewMalzemeTalepleri.Columns["MALZEME STOK ADET"].SummaryItem.DisplayFormat = "{0} Adet";

            this.gridViewMalzemeTalepleri.BestFitColumns();
        }

        public void InitForm2()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT s.MALZEME_ID, case when isnull(t.MALZEMETALEP_DURUM,0)= 0 then 'T.Gelmedi'  when isnull(t.MALZEMETALEP_DURUM,0)= 1 then 'T.Geldi' else '<<Yok>>' end 'TALEP DURUM', ");
            sb.Append("t.MALZEMETALEP_NO AS 'TALEP NO', (ISNULL(t.MALZEMETALEP_ADET,0)) AS ' MALZEME SİPARİŞ ADET', CONVERT(VARCHAR(10),MALZEMETALEP_TARIHI ,121) AS 'TALEP TARİHİ' , ");
            sb.Append("case when s.MALZEME_TURU=1 then 'NLAG'  when s.MALZEME_TURU=2 then 'UNBW' else '<<Seçiniz>>' end 'MALZEME TURU', convert(nvarchar,s.MALZEME_MATERYAL) AS 'MALZEME MATERYEL NO', ");
            sb.Append("s.MALZEME_ADI AS 'MALZEME ADI',(SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS mg WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC ) AS 'MALZEME BİRİM FİYAT',");
            sb.Append("(SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS mg WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC )*(ISNULL(t.MALZEMETALEP_ADET,0)) AS 'MALZEME TOPLAM FİYAT' ,isnull(g.GirislerToplam,0) as 'MALZEME GİRİŞ ADET', isnull(c.CikislarToplam,0) as 'MALZEME ÇIKIŞ ADET', ");
            sb.Append("(isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) AS 'MALZEME STOK ADET', ISNULL(s.MALZEME_MAXADET,0) AS 'MALZEME MAX ADET', ISNULL(s.MALZEME_MINADET,0) AS 'MALZEME MİN ADET', ");
            sb.Append("s.MALZEME_GRUBU AS 'MALZEME GRUBU', s.MALZEME_PARCANO AS 'MALZEME PARÇANO', e.MALZEMEKATEGORI_ADI +' '+'(' + e.MALZEMEKATEGORI_KODU +')' AS 'MALZEME SATINALMA KATEGORISI' ");
            sb.Append("FROM TBL_LST_MALZEMELER s ");
            sb.Append("LEFT OUTER JOIN ( Select  MALZEMEGIRIS_MALZEMELERID, Sum(MALZEMEGIRIS_ADET) GirislerToplam FROM TBL_LST_MALZEMEGIRIS GROUP BY  MALZEMEGIRIS_MALZEMELERID  ) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("LEFT OUTER JOIN ( Select  MALZEMECIKIS_MALZEMELERID, Sum(MALZEMECIKIS_ADET) CikislarToplam FROM TBL_LST_MALZEMECIKIS GROUP BY  MALZEMECIKIS_MALZEMELERID  ) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("JOIN TBL_LST_MALZEMETALEP t on t.MALZEMETALEP_MALZEMELERID=s.MALZEME_ID ");
            sb.Append("JOIN TBL_LST_MALZEMEANAGRUPLAR d on s.MALZEME_ANAGRUBU=d.MALZEMEANAGRUP_ID ");
            sb.Append("LEFT JOIN TBL_LST_MALZEMEKATEGORILER e on s.MALZEME_SATINALMAKATEGORI=e.MALZEMEKATEGORI_ID ");
            sb.AppendFormat("WHERE CONVERT(VARCHAR(10),t.MALZEMETALEP_TARIHI ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.AppendFormat("AND CONVERT(VARCHAR(10),t.MALZEMETALEP_TARIHI ,121)<={0} ", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.Append("AND isnull(t.MALZEMETALEP_DURUM,0)= 0 ");
            sb.Append("ORDER BY t.MALZEMETALEP_TARIHI desc");

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlMalzemeTalepleri.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepleri.Columns["TALEP NO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepleri.Columns["TALEP NO"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;


            this.SetGridFont(gridViewMalzemeTalepleri, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewMalzemeTalepleri.Columns["MALZEME STOK ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewMalzemeTalepleri.Columns["MALZEME STOK ADET"].SummaryItem.DisplayFormat = "{0} Adet";

            this.gridViewMalzemeTalepleri.BestFitColumns();
        }
        private void barButtonItemMalzemeOtoTalepDetayListele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (radioGroupDurum.SelectedIndex == 0)
            {
                InitForm();
            }
            if (radioGroupDurum.SelectedIndex == 1)
            {
                InitForm1();
            }
            if (radioGroupDurum.SelectedIndex == 2)
            {
                InitForm2();
            }
        }

        private void barButtonItemMalzemeOtoTalepDetayKapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void barButtonItemMalzemeOtoTalepYazdir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");

            clGenelTanim.GridYazdir(gridControlMalzemeTalepleri, "Malzeme Listesi");
        }

        private void barButtonItemMalzemeOtoTalepAktar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");

            clGenelTanim.GridExport(gridControlMalzemeTalepleri, "Malzeme Listesi", this);
        }

        private void gridViewMalzemeTalepleri_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    int malzeme_TalepMiktari = Convert.ToInt32(gridViewMalzemeTalepleri.GetRowCellDisplayText(e.RowHandle, gridViewMalzemeTalepleri.Columns[2]));

            //    if (malzeme_TalepMiktari > 0)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }
            //}
        }

        private void gridViewMalzemeTalepleri_DoubleClick(object sender, EventArgs e)
        {
            //DevExpress.XtraGrid.Columns.GridColumn col = gridViewMalzemeTalepleri.Columns[0];//varolan sütünü sanala atadık.
            //DevExpress.XtraGrid.Columns.GridColumn col1 = new DevExpress.XtraGrid.Columns.GridColumn();//sanal sütun tanımladık
            //col = gridViewMalzemeTalepleri.Columns[0];//varolan sütünü sanala atadık.
            //col1 = gridViewMalzemeTalepleri.Columns[1];//varolan sütünü sanala atadık.
            //int[] i = gridViewMalzemeTalepleri.GetSelectedRows();//seçili satırın numarasını getirdik
            //frmMalzemeTalepEkle u = new frmMalzemeTalepEkle();
            //u.nMALZEMELER_ID = clGenelTanim.DBToInt32(gridViewMalzemeTalepleri.GetRowCellValue(i[0], col));//seçili satır ve sütunun bilgisini aktardık.
            //u.nTALEPLER_ID = clGenelTanim.DBToString(gridViewMalzemeTalepleri.GetRowCellValue(i[0], col1));//seçili satır ve sütunun bilgisini aktardık.

            //u.ShowDialog();
        }

        private void malzemeTalepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DevExpress.XtraGrid.Columns.GridColumn col = gridViewMalzemeTalepleri.Columns[0];//varolan sütünü sanala atadık.
            //DevExpress.XtraGrid.Columns.GridColumn col1 = new DevExpress.XtraGrid.Columns.GridColumn();//sanal sütun tanımladık
            //col = gridViewMalzemeTalepleri.Columns[0];//varolan sütünü sanala atadık.
            //col1 = gridViewMalzemeTalepleri.Columns[1];//varolan sütünü sanala atadık.
            //int[] i = gridViewMalzemeTalepleri.GetSelectedRows();//seçili satırın numarasını getirdik
            //frmMalzemeTalepEkle u = new frmMalzemeTalepEkle();
            //u.nMALZEMELER_ID = clGenelTanim.DBToInt32(gridViewMalzemeTalepleri.GetRowCellValue(i[0], col));//seçili satır ve sütunun bilgisini aktardık.
            //u.nTALEPLER_ID = clGenelTanim.DBToString(gridViewMalzemeTalepleri.GetRowCellValue(i[0], col1));//seçili satır ve sütunun bilgisini aktardık.

            //u.ShowDialog();
        }

        private void radioGroupDurum_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radioGroupDurum.SelectedIndex)
            {
                case 0:
                    InitForm();
                    break;

                case 1:
                    InitForm1();
                    break;
                case 2:
                    InitForm2();
                    break;
            }
        }

        private void talepNoGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = gridViewMalzemeTalepleri.FocusedRowHandle;
            string TalepNO = "";
            if (index >= 0)
            {
                TalepNO = gridViewMalzemeTalepleri.GetRowCellValue(index, "TALEP NO").ToString();
            }
            frmMalzemeTalepGuncelle u = new frmMalzemeTalepGuncelle();
            u.nTalepNO = TalepNO;
            u.ShowDialog();
        }



        private void Sil()
        {
            StringBuilder sb = new StringBuilder(1024);
            var item = gridViewMalzemeTalepleri.GetFocusedDataRow();
            if (item != null)
            {
                sb.Append("DELETE FROM TBL_LST_MALZEMETALEP ");
                sb.AppendFormat("WHERE MALZEMETALEP_NO='{0}' AND MALZEMETALEP_MALZEMELERID='{1}'", clGenelTanim.DBToString(item["TALEP NO"]), item["MALZEME_ID"]);
                clSqlTanim.RunStoredProc(sb.ToString());
                InitForm();
            }
        }

        private void talepSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Sil();

                XtraMessageBox.Show("Kayıt Silindi ...");
            }
        }

        private void Tamamla()
        {
            StringBuilder sb = new StringBuilder(1024);
            var item = gridViewMalzemeTalepleri.GetFocusedDataRow();
            if (item != null)
            {
                sb.Append("Update TBL_LST_MALZEMETALEP set MALZEMETALEP_DURUM=1");
                sb.AppendFormat("WHERE MALZEMETALEP_NO='{0}'", clGenelTanim.DBToString(item["TALEP NO"]));
                clSqlTanim.RunStoredProc(sb.ToString());
                InitForm();
            }
        }

        private void talepTamamlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Tamamla();

                XtraMessageBox.Show("İşlem Başarılı ...");
            }
        }
    }
}
