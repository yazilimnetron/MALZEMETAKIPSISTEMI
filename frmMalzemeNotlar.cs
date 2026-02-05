using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MALZEME_TAKIP_SISTEMI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeNotlar : Form
    {
        //private int currentNotlarID = -1;
        //private DataSet dsNotlar = null;
        //private bool yeniNotlar = false;
        //private int Not_ID = 0;
        private int MALZEMENOT_ID = 0;
        public frmMalzemeNotlar()
        {
            InitializeComponent();
        }

        void SetGridFont(GridView view, Font font)
        {
            foreach (AppearanceObject ap in view.Appearance)

                ap.Font = font;
        }

        private void simpleButtonKaydet_Click(object sender, EventArgs e)
        {
            frmMalzemeNotlarYeni u = new frmMalzemeNotlarYeni();
            u.yeniNot = true;
            u.ShowDialog();
            //StringBuilder sb = new StringBuilder(5012);
            //if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            //{
            //    try
            //    {
            //        sb.Append("exec NotlarEkleGuncelle ");
            //        sb.Append("@NOT_AD=" + clGenelTanim.tosqlstring(richEditControlNotlar.Text.ToString(), 50000, true));
            //        clSqlTanim.RunStoredProc(sb.ToString());

            //        if (sb.Length > 50)
            //        {
            //            InitForm();
            //            XtraMessageBox.Show("Kayıt İşlemi Başarılı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }

            //    catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            //}
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
                StringBuilder sb = new StringBuilder(1024);
                var item = gridViewNotListe.GetFocusedDataRow();
                if (item != null)
                {
                    sb.Append("DELETE FROM TBL_LST_MALZEMENOTLAR ");
                    sb.AppendFormat("WHERE NOT_ID={0}", clGenelTanim.DBToInt32(item["NOT_ID"]));
                    clSqlTanim.RunStoredProc(sb.ToString());
                    InitForm();
                    XtraMessageBox.Show("Kayıt Silindi ...");
                }

            }
        }

        private void MALZEMENOTID(int MALZEMENOT_ID)
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("Select NOT_DETAY from TBL_LST_MALZEMENOTLAR ");
            sb.AppendFormat("Where NOT_ID={0}", MALZEMENOT_ID.ToString());

            DataTable dt = clSqlTanim.RunStoredProc(sb.ToString());
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
