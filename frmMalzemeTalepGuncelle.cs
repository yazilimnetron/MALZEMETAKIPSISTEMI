using DevExpress.XtraEditors;
using MALZEMETAKIPSISTEMI;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeTalepGuncelle : Form
    {
        public string nTalepNO = "";
        public int nMalzemeId = 0;
        public string YeniTalepNo { get; private set; } = "";
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
                DataTable dt = clSqlTanim.RunStoredProc(
                    "SELECT MALZEMETALEP_NO FROM TBL_LST_MALZEMETALEP t WHERE t.MALZEMETALEP_NO=@talepNo",
                    new[] { new SqlParameter("@talepNo", nTalepNO) });
                if (dt == null) return;
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

        private void simpleButtonKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                YeniTalepNo = textEditTalepNo.Text;

                clSqlTanim.ExecuteNonQuery(
                    "UPDATE TBL_LST_MALZEMETALEP SET MALZEMETALEP_NO=@yeniNo WHERE MALZEMETALEP_NO=@eskiNo AND MALZEMETALEP_MALZEMELERID=@malzemeId",
                    new[] {
                        new SqlParameter("@yeniNo",    YeniTalepNo),
                        new SqlParameter("@eskiNo",    nTalepNO),
                        new SqlParameter("@malzemeId", nMalzemeId)
                    });

                XtraMessageBox.Show("Talep Numrası Güncellendi...", "Bilgi...", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var frmTalepler = Application.OpenForms["frmMalzemeTalepAcilanlar"] as frmMalzemeTalepAcilanlar;
                frmTalepler?.InitForm();

                this.Close();
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
    }
}
