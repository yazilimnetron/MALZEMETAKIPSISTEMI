using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeYillikKullanim : Form
    {
        public frmMalzemeYillikKullanim()
        {
            InitializeComponent();
        }

        void SetGridFont(GridView view, Font font)
        {
            foreach (AppearanceObject ap in view.Appearance)

                ap.Font = font;
        }

        private void frmMalzemeYillikKullanim_Load(object sender, EventArgs e)
        {
            layoutControlYillikKulanim.LayoutKontrolleriniSifirla();
            InitForm();
        }
        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT a.MALZEME_ID, a.MALZEME_OUDBTNO AS 'MALZEME OUDBTNO', ");
            sb.Append(" case when a.MALZEME_TURU=1 then 'NLAG'  when a.MALZEME_TURU=2 then 'UNBW' else '<<Seçiniz>>' end 'MALZEME TURU', ");
            sb.Append("convert(nvarchar,a.MALZEME_MATERYAL) AS 'MALZEME MATERYEL', ");
            sb.Append("a.MALZEME_PARCANO AS 'MALZEME PARÇANO', a.MALZEME_ADI AS 'MALZEME ADI', ISNULL(a.MALZEME_STOKMIKTARI,0) AS 'MALZEME STOK ADET', ISNULL(b.MALZEMEGIRIS_BIRIMFIYAT,0) AS 'MALZEME BİRİM FİYAT', ");
            sb.Append("case when b.MALZEMEGIRIS_PARABIRIMI=1 then 'TL' ");
            sb.Append("when b.MALZEMEGIRIS_PARABIRIMI=2 then '€' ");
            sb.Append("when b.MALZEMEGIRIS_PARABIRIMI=3 then '$' ");
            sb.Append("when b.MALZEMEGIRIS_PARABIRIMI=4 then 'Jpy' ");
            sb.Append("when b.MALZEMEGIRIS_PARABIRIMI=5 then 'Chf' end 'FİYAT CİNSİ', ");
            sb.Append("ISNULL(b.MALZEMEGIRIS_BIRIMFIYAT,0)*ISNULL(a.MALZEME_STOKMIKTARI,0) AS 'MALZEME TOPLAM FİYAT', ");
            sb.Append("a.MALZEME_RAFNO AS 'MALZEME RAFNO', a.MALZEME_GRUBU AS 'MALZEME GRUBU', c.MALZEME_CIKIS_ADET AS 'TOPLAM KULLANIM ADETİ', Convert(int,ROUND((c.MALZEME_CIKIS_ADET/12)*2, 0)) AS 'MİNUMUM MİKTAR', Convert(int,ROUND((c.MALZEME_CIKIS_ADET/12)*4,0)) AS 'MAXİMUM MİKTAR' ");
            sb.Append("FROM TBL_LST_MALZEMELER a ");
            sb.Append("JOIN (SELECT  MALZEMEGIRIS_MALZEMELERID, MALZEMEGIRIS_BIRIMFIYAT, MALZEMEGIRIS_PARABIRIMI FROM TBL_LST_MALZEMEGIRIS (NOLOCK) WHERE MALZEMEGIRIS_ID IN (select MAX(MALZEMEGIRIS_ID) from TBL_LST_MALZEMEGIRIS GROUP BY MALZEMEGIRIS_MALZEMELERID) GROUP BY MALZEMEGIRIS_MALZEMELERID, MALZEMEGIRIS_BIRIMFIYAT, MALZEMEGIRIS_PARABIRIMI ) b on a.MALZEME_ID=b.MALZEMEGIRIS_MALZEMELERID ");
            sb.Append("JOIN (SELECT MALZEMECIKIS_MALZEMELERID,CONVERT(DECIMAL(8,0),SUM(MALZEMECIKIS_ADET)) AS MALZEME_CIKIS_ADET FROM TBL_LST_MALZEMECIKIS(NOLOCK) ");
            sb.AppendFormat("WHERE CONVERT(VARCHAR(10),MALZEMECIKIS_TARIHI ,121)>={0}", "'" + Convert.ToDateTime(dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.AppendFormat("AND CONVERT(VARCHAR(10),MALZEMECIKIS_TARIHI ,121)<={0} ", "'" + Convert.ToDateTime(dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.Append("GROUP BY MALZEMECIKIS_MALZEMELERID ) c on a.MALZEME_ID=c.MALZEMECIKIS_MALZEMELERID ");
            sb.Append("ORDER BY a.MALZEME_ADI");

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlListe.DataSource = dtMalzemeler;

            this.gridViewListe.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewListe.Columns["MALZEME OUDBTNO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewListe.Columns["MALZEME OUDBTNO"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewListe.Columns["MALZEME_ID"].Visible = false;
            this.gridViewListe.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.gridViewListe.Columns["MALZEME OUDBTNO"].Visible = false;

            this.SetGridFont(gridViewListe, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewListe.Columns["MALZEME STOK ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewListe.Columns["MALZEME STOK ADET"].SummaryItem.DisplayFormat = "{0} Adet";
            this.gridViewListe.BestFitColumns();

        }

        private void simpleButtonHesapla_Click(object sender, EventArgs e)
        {
            InitForm();
        }

        private void simpleButtonYazdir_Click(object sender, EventArgs e)
        {
            clGenelTanim.GridExport(gridControlListe, "Malzeme Listesi", this);
        }
    }
}
