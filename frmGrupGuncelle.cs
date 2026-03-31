using DevExpress.XtraEditors;
using MALZEMETAKIPSISTEMI;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmGrupGuncelle : Form
    {
        public frmGrupGuncelle()
        {
            InitializeComponent();
            layoutControlGrup.LayoutKontrolleriniSifirla();
        }

        private void frmGrupGuncelle_Load(object sender, EventArgs e)
        {
            comboBoxEditGrupAdi.Doldur("SELECT MALZEMEGRUP_ID, MALZEMEGRUP_ADI AS 'MALZEME GRUPADI' FROM TBL_LST_MALZEMEGRUPLAR (NOLOCK)  WHERE ISNULL (MALZEMEGRUP_DURUM,0)=1 ORDER BY MALZEMEGRUP_ADI", true);
            comboBoxEditGrupAdi.SelectedIndex = 0;
        }

        private void simpleButtonKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonKaydet_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Grup Güncellensin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Kaydet();
            }
            this.Close();
        }

        private void Kaydet()
        {
            var frmMalzemeTanimlama = Application.OpenForms["frmMalzemeTanimlama"] as frmMalzemeTanimlama;
            if (frmMalzemeTanimlama == null) return;

            try
            {
                int grupId = clGenelTanim.DBToInt32(comboBoxEditGrupAdi.SecilenDeger().Id.ToString());
                for (int i = 0; i < frmMalzemeTanimlama.gridViewMalzemeListesi.DataRowCount; i++)
                {
                    if (!frmMalzemeTanimlama.gridViewMalzemeListesi.IsRowSelected(i)) continue;
                    int malzemeId = clGenelTanim.DBToInt32(string.IsNullOrEmpty(frmMalzemeTanimlama.gridViewMalzemeListesi.GetRowCellValue(i, frmMalzemeTanimlama.gridViewMalzemeListesi.Columns[0]).ToString()) ? "0" : frmMalzemeTanimlama.gridViewMalzemeListesi.GetRowCellValue(i, frmMalzemeTanimlama.gridViewMalzemeListesi.Columns[0]).ToString());
                    clSqlTanim.ExecuteNonQuery(
                        "UPDATE TBL_LST_MALZEMELER SET MALZEME_GRUBU=@grup WHERE MALZEME_ID=@id",
                        new[] { new SqlParameter("@grup", grupId), new SqlParameter("@id", malzemeId) });
                }

                XtraMessageBox.Show("Malzeme Grup Güncellendi...", "Bilgi...", MessageBoxButtons.OK, MessageBoxIcon.Information);


                frmMalzemeTanimlama.InitForm();

                this.Close();

            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
    }
}
