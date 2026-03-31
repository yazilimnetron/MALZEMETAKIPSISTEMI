using System;
using System.Data;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeBarkodlama : DevExpress.XtraEditors.XtraForm
    {
        int nMalzemeId = 0;
        public frmMalzemeBarkodlama()
        {
            InitializeComponent();
        }

        private void simpleButtonGiris_Click(object sender, EventArgs e)
        {
            if (textEditMalzemeMateryalNo.Text != string.Empty && textEditMalzemeAdi.Text != string.Empty)
            {
                frmMalzemeGirisEkle u = new frmMalzemeGirisEkle();
                u.MALZEMELER_ID = nMalzemeId;
                u.ShowDialog();
            }
        }

        private void simpleButtonCikis_Click(object sender, EventArgs e)
        {
            if (textEditMalzemeMateryalNo.Text != string.Empty && textEditMalzemeAdi.Text != string.Empty)
            {
                frmMalzemeCikisEkle u = new frmMalzemeCikisEkle();
                u.MALZEMELER_ID = nMalzemeId;
                u.ShowDialog();
            }
        }

        private void frmMalzemeBarkodlama_Load(object sender, EventArgs e)
        {
        }

        private void simpleButtonOku_Click(object sender, EventArgs e)
        {
        }

        private void textEditMalzemeMateryalNo_TextChanged(object sender, EventArgs e)
        {
            if (textEditMalzemeMateryalNo.Text.Length == 8)
            {
                int materyalNo = clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMalzemeMateryalNo.Text) ? "0" : textEditMalzemeMateryalNo.Text);
                DataTable dt = clSqlTanim.RunStoredProc(
                    "SELECT MALZEME_ADI, MALZEME_ID, MALZEME_STOKMIKTARI FROM TBL_LST_MALZEMELER (NOLOCK) WHERE MALZEME_MATERYAL=@materyal",
                    new[] { new System.Data.SqlClient.SqlParameter("@materyal", materyalNo) });

                if (dt != null && dt.Rows.Count == 1)
                {
                    textEditMalzemeAdi.Text = dt.Rows[0][0].ToString();
                    nMalzemeId = clGenelTanim.DBToInt32(dt.Rows[0][1].ToString());
                    textEditMalzemeAdet.Text = dt.Rows[0][2].ToString();
                }
            }
        }

        private void frmMalzemeBarkodlama_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
