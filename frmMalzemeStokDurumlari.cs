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
    public partial class frmMalzemeStokDurumlari : Form
    {
        public frmMalzemeStokDurumlari()
        {
            InitializeComponent();
        }

        void SetGridFont(GridView view, Font font)
        {
            foreach (AppearanceObject ap in view.Appearance)

                ap.Font = font;
        }

        private void frmMalzemeStokDurumlari_Load(object sender, EventArgs e)
        {
            layoutControlStokDurumlari.LayoutKontrolleriniSifirla();
            InitForm();
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("select MALZEME_ID, MALZEME_OUDBTNO as 'MALZEME OUDBTNO', convert(nvarchar,MALZEME_MATERYAL) as 'MALZEME MATERYAL', MALZEME_ADI as 'MALZEME ADI', ISNULL((g.GirislerToplam - c.CikislarToplam),0) as 'MALZEME STOK MİKTARI',  MALZEMEGRUP_ADI as 'MALZEME ANA GRUBU', MALZEME_GRUBU as 'MALZEME GRUBU' from TBL_LST_MALZEMELER (nolock) s ");
            sb.Append("LEFT OUTER JOIN (Select  MALZEMEGIRIS_MALZEMELERID, isnull(Sum(MALZEMEGIRIS_ADET),0) GirislerToplam FROM TBL_LST_MALZEMEGIRIS GROUP BY  MALZEMEGIRIS_MALZEMELERID) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("LEFT OUTER JOIN (Select  MALZEMECIKIS_MALZEMELERID, isnull(Sum(MALZEMECIKIS_ADET),0) CikislarToplam FROM TBL_LST_MALZEMECIKIS GROUP BY  MALZEMECIKIS_MALZEMELERID) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("INNER JOIN TBL_LST_MALZEMEANAGRUPLAR b on s.MALZEME_ANAGRUBU=b.MALZEMEANAGRUP_ID ");
            if (checkEditStokGoster.Checked == false)
            {
                sb.Append("where ISNULL((g.GirislerToplam - c.CikislarToplam),0) > 0  ");
            }
            sb.Append("ORDER BY s.MALZEME_ADI");

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlStokDurum.DataSource = dtMalzemeler;

            this.gridViewStokDurum.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewStokDurum.Columns["MALZEME ADI"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewStokDurum.Columns["MALZEME ADI"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewStokDurum.Columns["MALZEME_ID"].Visible = false;
            this.gridViewStokDurum.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.gridViewStokDurum.Columns["MALZEME OUDBTNO"].Visible = false;

            this.SetGridFont(gridViewStokDurum, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewStokDurum.BestFitColumns();

        }

        private void simpleButtonListele_Click(object sender, EventArgs e)
        {
            InitForm();
        }

        private void simpleButtonYazdir_Click(object sender, EventArgs e)
        {
            clGenelTanim.GridExport(gridControlStokDurum, "Malzeme Listesi", this);
        }
    }
}
