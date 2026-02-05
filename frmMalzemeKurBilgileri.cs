using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeKurBilgileri : Form
    {
        #region CONSTRUCTOR_0
        public frmMalzemeKurBilgileri()
        {
            InitializeComponent();
        }
        #endregion


        #region FORM_LOAD

        private void frmkur_Load(object sender, EventArgs e)
        {
            XML_VERILERI_CEK();

            InitForm();
        }

        private void InitForm()
        {
            StringBuilder sb = new StringBuilder(1024);
            sb.Append("SELECT TOP 5 MALZEMEDOVIZKUR_TARIH as 'TARÝH', MALZEMEDOVIZKUR_USDSATIS as 'Dolar', MALZEMEDOVIZKUR_EUROSATIS as 'Euro', MALZEMEDOVIZKUR_GBPSATIS as 'Sterlin', MALZEMEDOVIZKUR_JPYSATIS as 'Yen', MALZEMEDOVIZKUR_CHFSATIS as 'Frank', MALZEMEDOVIZKUR_RONSATIS as 'Ley' FROM TBL_LST_MALZEMEKURBILGILERI order by MALZEMEDOVIZKUR_ID desc ");
            DataSet ds = new DataSet();
            ds = clSqlTanim.RunStoredProcDS(sb.ToString(), "kurlar");
            dataGridView1.DataSource = ds.Tables[0];

            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        #endregion

        private void btnKurOnayla_Click(object sender, EventArgs e)
        {
            KUR_ONAYLA();
        }

        #region KUR_ONAYLA
        private void KUR_ONAYLA()
        {
            StringBuilder sb = new StringBuilder(1024);
            StringBuilder sbI = new StringBuilder(1024);
            StringBuilder sbU = new StringBuilder(1024);

            try
            {
                sb.Append("SELECT TOP 1 MALZEMEDOVIZKUR_TARIH FROM TBL_LST_MALZEMEKURBILGILERI WHERE CONVERT(VARCHAR(10), MALZEMEDOVIZKUR_TARIH, 121) = CONVERT(VARCHAR(10), '" + tarihKur.Value.ToString("yyyy-MM-dd") + "' , 121)");
                DataSet ds = new DataSet();
                ds = clSqlTanim.RunStoredProcDS(sb.ToString(), "kurlar");
                if (ds.Tables["kurlar"].Rows.Count == 0)
                {
                    sbI.Append("insert into TBL_LST_MALZEMEKURBILGILERI ( MALZEMEDOVIZKUR_TARIH, MALZEMEDOVIZKUR_USDSATIS, MALZEMEDOVIZKUR_EUROSATIS, MALZEMEDOVIZKUR_GBPSATIS, MALZEMEDOVIZKUR_JPYSATIS, MALZEMEDOVIZKUR_CHFSATIS, MALZEMEDOVIZKUR_RONSATIS ) select");
                    sbI.AppendFormat(" '{0}'", tarihKur.Value.ToString("yyyy-MM-dd"));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(txtKurDolar.Text.Replace(",", "."), 5, true));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(txtKurEuro.Text.Replace(",", "."), 5, true));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(txtKurGbp.Text.Replace(",", "."), 5, true));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(txtKurJpy.Text.Replace(",", "."), 5, true));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(txtKurChf.Text.Replace(",", "."), 5, true));
                    sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(txtKurRon.Text.Replace(",", "."), 5, true));


                    clSqlTanim.RunStoredProc(sbI.ToString());

                    MessageBox.Show("Bugünün Kur Bilgileri Ayarlanmýþtýr, Bugün Yapacaðýnýz Doviz Ýþlemleri Bu Bilgilere Göre Yapýlacaktýr!", "Malzeme Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ds.Tables["kurlar"].Rows.Count != 0)
                {
                    DialogResult secim = MessageBox.Show("Bu Tarih Önceden Girilmiþ. Yeni Bilgiler Bu Tarihin Üzerine Yazýlsýn mý?", "Malzeme Takip", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (secim == DialogResult.Yes)
                    {
                        sbU.Append("Update TBL_LST_MALZEMEKURBILGILERI set ");
                        sbU.AppendFormat("  MALZEMEDOVIZKUR_USDSATIS={0}", clGenelTanim.tosqlstring(txtKurDolar.Text.Replace(",", "."), 5, true));
                        sbU.AppendFormat(" ,MALZEMEDOVIZKUR_EUROSATIS={0}", clGenelTanim.tosqlstring(txtKurEuro.Text.Replace(",", "."), 5, true));
                        sbU.AppendFormat(" ,MALZEMEDOVIZKUR_GBPSATIS={0}", clGenelTanim.tosqlstring(txtKurGbp.Text.Replace(",", "."), 5, true));
                        sbU.AppendFormat(" ,MALZEMEDOVIZKUR_JPYSATIS={0}", clGenelTanim.tosqlstring(txtKurJpy.Text.Replace(",", "."), 5, true));
                        sbU.AppendFormat(" ,MALZEMEDOVIZKUR_RONSATIS={0}", clGenelTanim.tosqlstring(txtKurRon.Text.Replace(",", "."), 5, true));
                        sbU.AppendFormat(" ,MALZEMEDOVIZKUR_CHFSATIS={0}", clGenelTanim.tosqlstring(txtKurChf.Text.Replace(",", "."), 5, true));

                        sbU.AppendFormat(" where CONVERT(VARCHAR(10), MALZEMEDOVIZKUR_TARIH, 121)='{0}'", tarihKur.Value.ToString("yyyy-MM-dd"));

                        clSqlTanim.RunStoredProc(sbU.ToString());

                        MessageBox.Show("Kur Bilgileri Ayarlanmýþtýr, Yapacaðýnýz Doviz Ýþlemleri Bu Bilgilere Göre Yapýlacaktýr!", "Malzeme Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }

                InitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }//KUR_ONAYLA

        #endregion


        #region XML_VERILERI_CEK
        private void XML_VERILERI_CEK()
        {
            try
            {
                XmlDocument xmlVerisi = new XmlDocument();
                xmlVerisi.Load("https://www.tcmb.gov.tr/kurlar/today.xml");

                decimal dolar = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "USD")).InnerText.Replace('.', ','));
                decimal euro = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "EUR")).InnerText.Replace('.', ','));
                decimal gbp = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "GBP")).InnerText.Replace('.', ','));
                decimal jpy = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "JPY")).InnerText.Replace('.', ','));
                decimal chf = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "CHF")).InnerText.Replace('.', ','));
                decimal ron = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "RON")).InnerText.Replace('.', ','));

                txtKurDolar.Text = String.Format("{0:f2}", dolar);
                txtKurEuro.Text = String.Format("{0:f2}", euro);
                txtKurGbp.Text = String.Format("{0:f2}", gbp);
                txtKurJpy.Text = String.Format("{0:f2}", jpy);
                txtKurChf.Text = String.Format("{0:f2}", chf);
                txtKurRon.Text = String.Format("{0:f2}", ron);

            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region TEXT_PARASAL

        private void txtKurDolar_TextChanged(object sender, EventArgs e)
        {

            //if (txtKurDolar.Text == "") txtKurDolar.Text = "0";

            //decimal sayi = Convert.ToDecimal(txtKurDolar.Text);
            //txtKurDolar.Text = sayi.ToString("0:0.##");
            //txtKurDolar.SelectionStart = txtKurDolar.Text.Length;
        }

        private void txtKurDolar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar!=',')
            //{
            //    e.Handled = true;
            //}
        }
        //---------------------------------------
        private void txtKurEuro_TextChanged(object sender, EventArgs e)
        {
            //if (txtKurEuro.Text == "") txtKurEuro.Text = "0";

            //decimal sayi = Convert.ToDecimal(txtKurEuro.Text);
            //txtKurEuro.Text = sayi.ToString("#,###0");
            //txtKurEuro.SelectionStart = txtKurEuro.Text.Length;
        }

        private void txtKurEuro_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != ',')
            //{
            //    e.Handled = true;
            //}
            ////if (Char.IsDigit(e.KeyChar) == false && Char.IsControl(e.KeyChar) == false)
            ////    e.KeyChar = '\0';
        }
        private void txtKurJpy_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtKurChf_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        #endregion


        #region FORMU_KAPAT
        private void frmkur_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        #endregion

        private void btnKurKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }//FORM
}