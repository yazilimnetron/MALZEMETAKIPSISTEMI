using DevExpress.XtraEditors;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeTalepEkle : Form
    {
        public int nMALZEMELER_ID = 0;
        public string nTALEPLER_ID = "";
        public int nMALZEMELER_STOKMIKTARI = 0;

        StringBuilder sbI = new StringBuilder(512);
        public frmMalzemeTalepEkle()
        {
            InitializeComponent();
            layoutControlMalzemeIstemBilgileri.LayoutKontrolleriniSifirla();
        }

        void bilgileriGetir()
        {
            try
            {
                //if (string.IsNullOrEmpty(textEditTalepAdedi.Text))
                //{
                //    textEditTalepAdedi.Text = "0";
                //}

                //this.ShowImage(nMALZEMELER_ID);

                sbI.Append("Select m.MALZEME_ADI, m.MALZEME_MATERYAL, t.MALZEMETALEP_ADET, t.MALZEMETALEP_NO, isnull(CONVERT(varchar(50),t.MALZEMETALEP_TARIHI,103), CONVERT(varchar(50),GETDATE(),103)) as  MALZEMETALEP_TARIHI from TBL_LST_MALZEMELER m ");
                sbI.Append("left join TBL_LST_MALZEMETALEP t on m.MALZEME_ID=t.MALZEMETALEP_MALZEMELERID ");
                sbI.AppendFormat("Where m.MALZEME_ID ={0} ", nMALZEMELER_ID.ToString());
                if (nTALEPLER_ID.Length > 1) sbI.AppendFormat(" and t.MALZEMETALEP_NO={0} ", clGenelTanim.tosqlstring(nTALEPLER_ID.ToString(), 100, true));
                DataTable dt = clSqlTanim.RunStoredProc(sbI.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    //textEditTalepMateryalNo.Text = clGenelTanim.DBToInt32(dr["MALZEME_MATERYAL"]).ToString();
                    //textEditTalepAdi.Text = clGenelTanim.DBToString(dr["MALZEME_ADI"]).ToString();
                    //textEditTalepAdedi.Text = clGenelTanim.DBToInt32(dr["MALZEMETALEP_ADET"]).ToString();
                    textEditTalepNo.Text = clGenelTanim.DBToString(dr["MALZEMETALEP_NO"]).ToString();
                    dateEditTalepTarih.DateTime = clGenelTanim.DBToDate(dr["MALZEMETALEP_TARIHI"]);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void frmMalzemeIstemBilgileri_Load(object sender, EventArgs e)
        {
            //bilgileriGetir();
        }

        private void simpleButtonMalzemeIstemKaydet_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Sipariş Eklensin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Kaydet();
            }
            this.Close();
        }

        //frmMalzemeTalepAcilanlar frmMalzemeTalep = ((frmMalzemeTalepAcilanlar)Application.OpenForms["frmMalzemeTalepDetay"]);

        private void Kaydet()
        {
            StringBuilder sbI = new StringBuilder(512);

            frmMalzemeTalepIhtiyac frmTalepIhtiyac = ((frmMalzemeTalepIhtiyac)Application.OpenForms["frmMalzemeTalepIhtiyac"]);

            try
            {
                for (int i = 0; i < frmTalepIhtiyac.gridViewMalzemeTalepler.DataRowCount; i++)
                {
                    if (frmTalepIhtiyac.gridViewMalzemeTalepler.IsRowSelected(i))
                    {
                        sbI.Append("insert into TBL_LST_MALZEMETALEP ( MALZEMETALEP_MALZEMELERID, MALZEMETALEP_ADI, MALZEMETALEP_MATERYALNO ,MALZEMETALEP_ADET, MALZEMETALEP_TARIHI, MALZEMETALEP_NO, MALZEMETALEP_DURUM ) select");

                        sbI.AppendFormat("  {0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(frmTalepIhtiyac.gridViewMalzemeTalepler.GetRowCellValue(i, frmTalepIhtiyac.gridViewMalzemeTalepler.Columns[0]).ToString()) ? "0" : frmTalepIhtiyac.gridViewMalzemeTalepler.GetRowCellValue(i, frmTalepIhtiyac.gridViewMalzemeTalepler.Columns[0]).ToString()));
                        sbI.AppendFormat(" ,N{0}", clGenelTanim.tosqlstring(frmTalepIhtiyac.gridViewMalzemeTalepler.GetRowCellValue(i, frmTalepIhtiyac.gridViewMalzemeTalepler.Columns[3]).ToString(), 500, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(frmTalepIhtiyac.gridViewMalzemeTalepler.GetRowCellValue(i, frmTalepIhtiyac.gridViewMalzemeTalepler.Columns[2]).ToString()) ? "0" : frmTalepIhtiyac.gridViewMalzemeTalepler.GetRowCellValue(i, frmTalepIhtiyac.gridViewMalzemeTalepler.Columns[2]).ToString()));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(frmTalepIhtiyac.gridViewMalzemeTalepler.GetRowCellValue(i, frmTalepIhtiyac.gridViewMalzemeTalepler.Columns[7]).ToString()) ? "0" : frmTalepIhtiyac.gridViewMalzemeTalepler.GetRowCellValue(i, frmTalepIhtiyac.gridViewMalzemeTalepler.Columns[7]).ToString()));
                        sbI.AppendFormat(" ,{0}", Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull) ? "NULL" : "'" + Convert.ToDateTime(dateEditTalepTarih.DateTime).ToString("yyyy-MM-dd HH:mm") + "'");
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(textEditTalepNo.Text.ToString(), 100, true));
                        sbI.AppendFormat(" ,{0}", 0);
                        sbI.Append(Environment.NewLine);
                    }

                }

                clSqlTanim.RunStoredProc(sbI.ToString());

                XtraMessageBox.Show("Talep Numrası Eklendi...", "Bilgi...", MessageBoxButtons.OK, MessageBoxIcon.Information);


                frmTalepIhtiyac.InitForm();

                this.Close();

            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textEditMalzemeIstemAdedi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
