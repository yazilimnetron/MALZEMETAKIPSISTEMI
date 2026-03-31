using DevExpress.XtraEditors;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeTalepEkle : Form
    {
        public frmMalzemeTalepEkle()
        {
            InitializeComponent();
            layoutControlMalzemeIstemBilgileri.LayoutKontrolleriniSifirla();
        }

        private void frmMalzemeIstemBilgileri_Load(object sender, EventArgs e)
        {
        }

        private void simpleButtonMalzemeIstemKaydet_Click(object sender, EventArgs e)
        {
            var frmTalepIhtiyac = Application.OpenForms["frmMalzemeTalepIhtiyac"] as frmMalzemeTalepIhtiyac;
            if (frmTalepIhtiyac == null) return;

            int seciliSatir = frmTalepIhtiyac.gridViewMalzemeTalepler.GetSelectedRows().Length;
            if (seciliSatir == 0)
            {
                XtraMessageBox.Show("En az bir malzeme seçmelisiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.Yes == XtraMessageBox.Show("Sipariş Eklensin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Kaydet();
            }
            this.Close();
        }

        //frmMalzemeTalepAcilanlar frmMalzemeTalep = ((frmMalzemeTalepAcilanlar)Application.OpenForms["frmMalzemeTalepDetay"]);

        private void Kaydet()
        {
            var frmTalepIhtiyac = Application.OpenForms["frmMalzemeTalepIhtiyac"] as frmMalzemeTalepIhtiyac;
            if (frmTalepIhtiyac == null) return;

            try
            {
                for (int i = 0; i < frmTalepIhtiyac.gridViewMalzemeTalepler.DataRowCount; i++)
                {
                    if (!frmTalepIhtiyac.gridViewMalzemeTalepler.IsRowSelected(i)) continue;

                    var gv = frmTalepIhtiyac.gridViewMalzemeTalepler;
                    int malzemeId  = clGenelTanim.DBToInt32(gv.GetRowCellValue(i, "MALZEME_ID")?.ToString() ?? "0");
                    string adi     = gv.GetRowCellValue(i, "MALZEME ADI")?.ToString() ?? "";
                    int materyalNo = clGenelTanim.DBToInt32(gv.GetRowCellValue(i, "MALZEME MATERYEL")?.ToString() ?? "0");
                    int adet       = clGenelTanim.DBToInt32(gv.GetRowCellValue(i, "SİPARİŞ VERİLECEK")?.ToString() ?? "0");

                    clSqlTanim.ExecuteNonQuery(
                        "INSERT INTO TBL_LST_MALZEMETALEP (MALZEMETALEP_MALZEMELERID, MALZEMETALEP_ADI, MALZEMETALEP_MATERYALNO, MALZEMETALEP_ADET, MALZEMETALEP_TARIHI, MALZEMETALEP_NO, MALZEMETALEP_DURUM) VALUES (@id, @adi, @materyal, @adet, @tarih, @talepNo, @durum)",
                        new[] {
                            new SqlParameter("@id",      malzemeId),
                            new SqlParameter("@adi",     adi),
                            new SqlParameter("@materyal", materyalNo),
                            new SqlParameter("@adet",    adet),
                            new SqlParameter("@tarih",   dateEditTalepTarih.DateTime),
                            new SqlParameter("@talepNo", textEditTalepNo.Text),
                            new SqlParameter("@durum",   (object)0)
                        });
                }

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
