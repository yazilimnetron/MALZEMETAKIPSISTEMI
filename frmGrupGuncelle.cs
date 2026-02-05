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
            StringBuilder sbU = new StringBuilder(512);

            frmMalzemeTanimlama frmMalzemeTanimlama = ((frmMalzemeTanimlama)Application.OpenForms["frmMalzemeTanimlama"]);

            try
            {
                for (int i = 0; i < frmMalzemeTanimlama.gridViewMalzemeListesi.DataRowCount; i++)
                {
                    if (frmMalzemeTanimlama.gridViewMalzemeListesi.IsRowSelected(i))
                    {
                        sbU.AppendFormat("update TBL_LST_MALZEMELER set ");
                        sbU.AppendFormat(" MALZEME_GRUBU={0}", clGenelTanim.DBToInt32(comboBoxEditGrupAdi.SecilenDeger().Id.ToString())); 
                        sbU.AppendFormat(" where MALZEME_ID={0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(frmMalzemeTanimlama.gridViewMalzemeListesi.GetRowCellValue(i, frmMalzemeTanimlama.gridViewMalzemeListesi.Columns[0]).ToString()) ? "0" : frmMalzemeTanimlama.gridViewMalzemeListesi.GetRowCellValue(i, frmMalzemeTanimlama.gridViewMalzemeListesi.Columns[0]).ToString()));
                        sbU.Append(Environment.NewLine);
                    }
                }

                clSqlTanim.RunStoredProc(sbU.ToString());

                XtraMessageBox.Show("Malzeme Grup Güncellendi...", "Bilgi...", MessageBoxButtons.OK, MessageBoxIcon.Information);


                frmMalzemeTanimlama.InitForm();

                this.Close();

            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
    }
}
