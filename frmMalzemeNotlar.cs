using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeNotlar : FrmBase
    {
        private int MALZEMENOT_ID = 0;
        public frmMalzemeNotlar()
        {
            InitializeComponent();
        }

        private void simpleButtonKaydet_Click(object sender, EventArgs e)
        {
            frmMalzemeNotlarYeni u = new frmMalzemeNotlarYeni();
            u.yeniNot = true;
            u.ShowDialog();
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(5012);
            sb.Append("Select NOT_ID, NOT_BASLIK as 'NOT BAŞLIK' from TBL_LST_MALZEMENOTLAR");
            DataTable dtNotlar = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlNotListe.DataSource = dtNotlar;

            this.gridViewNotListe.Columns["NOT_ID"].Visible = false;
            this.gridViewNotListe.Columns["NOT_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewNotListe, new Font("Tahoma", 10, FontStyle.Bold));
        }

        private void frmNotlar_Load(object sender, EventArgs e)
        {
            if (clGenelTanim.currentYoneticiMi == 1)
            {
                simpleButtonKaydet.Enabled = true;
                simpleButtonSil.Enabled = true;
            }
                InitForm();
        }

        private void simpleButtonKapat_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void simpleButtonSil_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Kayıt Silinsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                var item = gridViewNotListe.GetFocusedDataRow();
                if (item != null)
                {
                    clSqlTanim.ExecuteNonQuery(
                        "DELETE FROM TBL_LST_MALZEMENOTLAR WHERE NOT_ID=@id",
                        new[] { new SqlParameter("@id", clGenelTanim.DBToInt32(item["NOT_ID"])) });
                    InitForm();
                    XtraMessageBox.Show("Kayıt Silindi ...");
                }

            }
        }

        private void MALZEMENOTID(int MALZEMENOT_ID)
        {
            DataTable dt = clSqlTanim.RunStoredProc(
                "SELECT NOT_DETAY FROM TBL_LST_MALZEMENOTLAR WHERE NOT_ID=@id",
                new[] { new SqlParameter("@id", MALZEMENOT_ID) });
            if (dt != null && dt.Rows.Count > 0)
            {
                richEditControlNotlar.Text = clGenelTanim.DBToString(dt.Rows[0]["NOT_DETAY"]);
            }
        }

        private void gridViewNotListe_Click(object sender, EventArgs e)
        {
            var item = gridViewNotListe.GetFocusedDataRow();
            if (item != null)
            {
                MALZEMENOT_ID = clGenelTanim.DBToInt32(item["NOT_ID"]);

                MALZEMENOTID(this.MALZEMENOT_ID);
            }
        }

        int index = 0, ID = 0;
        private void gridViewNotListe_DoubleClick(object sender, EventArgs e)
        {
            if (clGenelTanim.currentYoneticiMi == 1)
            {
                index = gridViewNotListe.FocusedRowHandle;
                if (index >= 0)
                {
                    ID = Convert.ToInt32(gridViewNotListe.GetRowCellValue(index, "NOT_ID").ToString());
                }
                frmMalzemeNotlarYeni u = new frmMalzemeNotlarYeni();
                u.currentNotID = ID;
                u.yeniNot = false;
                u.ShowDialog();
            }
        }
    }
}
