using DevExpress.XtraEditors;
using MALZEME_TAKIP_SISTEMI;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
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
    public partial class frmMalzemeTalepGuncelle : Form
    {
        public string nTalepNO = "";
        public frmMalzemeTalepGuncelle()
        {
            InitializeComponent();
            layoutControlTalep.LayoutKontrolleriniSifirla();
            this.ActiveControl = textEditTalepNo;
        }

        void bilgileriGetir()
        {
            try
            {
                DataTable dt = clSqlTanim.RunStoredProc("Select MALZEMETALEP_NO from TBL_LST_MALZEMETALEP t Where t.MALZEMETALEP_NO = " + clGenelTanim.tosqlstring(nTalepNO.ToString(), 100, true));
                foreach (DataRow dr in dt.Rows)
                {
                    textEditTalepNo.Text = dr["MALZEMETALEP_NO"].ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void frmMalzemeTalepGuncelle_Load(object sender, EventArgs e)
        {
            bilgileriGetir();
        }

        private void simpleButtonKapat_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        frmMalzemeTalepAcilanlar frmTalepler = ((frmMalzemeTalepAcilanlar)Application.OpenForms["frmMalzemeTalepAcilanlar"]);
        private void simpleButtonKaydet_Click(object sender, EventArgs e)
        {
            StringBuilder sbU = new StringBuilder(512);
            try
            {
                sbU.AppendFormat("update TBL_LST_MALZEMETALEP set ");
                sbU.AppendFormat(" MALZEMETALEP_NO={0}", clGenelTanim.tosqlstring(textEditTalepNo.Text.ToString(), 100, true));
                sbU.AppendFormat(" where MALZEMETALEP_NO={0}", clGenelTanim.tosqlstring(nTalepNO.ToString(), 100, true));

                clSqlTanim.RunStoredProc(sbU.ToString());

                XtraMessageBox.Show("Talep Numrası Güncellendi...", "Bilgi...", MessageBoxButtons.OK, MessageBoxIcon.Information);


                frmTalepler.InitForm();

                this.Close();
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
    }
}
