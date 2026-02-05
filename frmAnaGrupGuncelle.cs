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
            StringBuilder sbU = new StringBuilder(512);

            frmMalzemeTanimlama frmMalzemeTanimlama = ((frmMalzemeTanimlama)Application.OpenForms["frmMalzemeTanimlama"]);

            try
            {
                for (int i = 0; i < frmMalzemeTanimlama.gridViewMalzemeListesi.DataRowCount; i++)
                {
                    if (frmMalzemeTanimlama.gridViewMalzemeListesi.IsRowSelected(i))
                    {
                        sbU.AppendFormat("update TBL_LST_MALZEMELER set ");
                        sbU.AppendFormat("MALZEME_ANAGRUBU={0}", clGenelTanim.DBToInt32(comboBoxEditAnaGrupAdi.SecilenDeger().Id.ToString()));
                        sbU.AppendFormat(" where MALZEME_ID={0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(frmMalzemeTanimlama.gridViewMalzemeListesi.GetRowCellValue(i, frmMalzemeTanimlama.gridViewMalzemeListesi.Columns[0]).ToString()) ? "0" : frmMalzemeTanimlama.gridViewMalzemeListesi.GetRowCellValue(i, frmMalzemeTanimlama.gridViewMalzemeListesi.Columns[0]).ToString()));
                        sbU.Append(Environment.NewLine);
                    }
                }

                clSqlTanim.RunStoredProc(sbU.ToString());

                XtraMessageBox.Show("Malzeme AnaGrup Güncellendi...", "Bilgi...", MessageBoxButtons.OK, MessageBoxIcon.Information);


                frmMalzemeTanimlama.InitForm();

                this.Close();

            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
    }
}
