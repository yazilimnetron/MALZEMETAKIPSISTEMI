using DevExpress.XtraEditors;
using MALZEMETAKIPSISTEMI;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmAnaGrupGuncelle : Form
    {
        public frmAnaGrupGuncelle()
        {
            InitializeComponent();
            layoutControlAnaGrup.LayoutKontrolleriniSifirla();
        }

        private void frmAnaGrupGuncelle_Load(object sender, EventArgs e)
        {
            comboBoxEditAnaGrupAdi.Doldur("SELECT MALZEMEANAGRUP_ID, MALZEMEANAGRUP_ADI AS 'MALZEME ANAGRUPADI' FROM TBL_LST_MALZEMEANAGRUPLAR (NOLOCK)  WHERE ISNULL (MALZEMEANAGRUP_DURUM,0)=1 ORDER BY MALZEMEANAGRUP_ADI", true);
            comboBoxEditAnaGrupAdi.SelectedIndex = 0;
        }

        private void simpleButtonKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonKaydet_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("AnaGrup Güncellensin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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
                int anaGrupId = clGenelTanim.DBToInt32(comboBoxEditAnaGrupAdi.SecilenDeger().Id.ToString());
                for (int i = 0; i < frmMalzemeTanimlama.gridViewMalzemeListesi.DataRowCount; i++)
                {
                    if (!frmMalzemeTanimlama.gridViewMalzemeListesi.IsRowSelected(i)) continue;
                    int malzemeId = clGenelTanim.DBToInt32(string.IsNullOrEmpty(frmMalzemeTanimlama.gridViewMalzemeListesi.GetRowCellValue(i, frmMalzemeTanimlama.gridViewMalzemeListesi.Columns[0]).ToString()) ? "0" : frmMalzemeTanimlama.gridViewMalzemeListesi.GetRowCellValue(i, frmMalzemeTanimlama.gridViewMalzemeListesi.Columns[0]).ToString());
                    clSqlTanim.ExecuteNonQuery(
                        "UPDATE TBL_LST_MALZEMELER SET MALZEME_ANAGRUBU=@grup WHERE MALZEME_ID=@id",
                        new[] { new SqlParameter("@grup", anaGrupId), new SqlParameter("@id", malzemeId) });
                }

                XtraMessageBox.Show("Malzeme AnaGrup Güncellendi...", "Bilgi...", MessageBoxButtons.OK, MessageBoxIcon.Information);


                frmMalzemeTanimlama.InitForm();

                this.Close();

            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
    }
}
