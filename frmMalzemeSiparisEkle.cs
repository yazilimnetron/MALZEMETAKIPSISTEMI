using DevExpress.XtraEditors;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeSiparisEkle : Form
    {

        public int nMALZEMELER_ID = 0;
        public int nMALZEMELER_STOKMIKTARI = 0;

        public frmMalzemeSiparisEkle()
        {
            InitializeComponent();
            layoutControlMalzemeIstemBilgileri.LayoutKontrolleriniSifirla();
            this.ActiveControl = textEditMalzemeIstemAdedi;
        }

        void bilgileriGetir()
        {
            try
            {
                comboBoxEditMalzemeIstemDepartman.Doldur("SELECT MALZEME_DEPARTMANID, MALZEME_DEPARTMANADI AS 'MALZEME DEPARTMANADI' FROM TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK)  WHERE ISNULL (MALZEME_DEPARTMANDURUM,0)=1 ORDER BY MALZEME_DEPARTMANADI", false);
                comboBoxEditMalzemeIstemDepartman.DegerSec(clGenelTanim.DBToInt32(clGenelTanim.currentMalzemeKullaniciDepartmanId.ToString()));

                if (string.IsNullOrEmpty(textEditMalzemeIstemAdedi.Text))
                {
                    textEditMalzemeIstemAdedi.Text = "0";
                }
                this.ShowImage(nMALZEMELER_ID);

                DataTable dt = clSqlTanim.RunStoredProc(
                    "Select s.MALZEME_MATERYAL, s.MALZEME_TURU, s.MALZEME_GRUBU, s.MALZEME_ADI, (isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) AS MALZEME_STOKMIKTARI, s.MALZEME_RAFNO, " +
                    "(SELECT TOP 1 e.MALZEMEGIRIS_SORGUBIRIMFIYAT FROM TBL_LST_MALZEMEGIRIS e WHERE s.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC ) as 'MALZEME BİRİM FİYAT'" +
                    " From TBL_LST_MALZEMELER s " +
                    "LEFT OUTER JOIN (Select  MALZEMEGIRIS_MALZEMELERID, Sum(MALZEMEGIRIS_ADET)  GirislerToplam FROM TBL_LST_MALZEMEGIRIS GROUP BY  MALZEMEGIRIS_MALZEMELERID) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID " +
                    "LEFT OUTER JOIN (Select  MALZEMECIKIS_MALZEMELERID, Sum(MALZEMECIKIS_ADET)  CikislarToplam FROM TBL_LST_MALZEMECIKIS GROUP BY  MALZEMECIKIS_MALZEMELERID ) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID " +
                    "Where s.MALZEME_ID = @id",
                    new[] { new SqlParameter("@id", nMALZEMELER_ID) });
                foreach (DataRow dr in dt.Rows)
                {
                    textEditMateryalNo.Text = dr["MALZEME_MATERYAL"].ToString();
                    textEditMalzemeIstemAdi.Text = dr["MALZEME_ADI"].ToString();
                    nMALZEMELER_STOKMIKTARI = Convert.ToInt32(dr["MALZEME_STOKMIKTARI"].ToString());
                    textEditMalzemeIstemFiyat.Text = Convert.ToDecimal(dr["MALZEME BİRİM FİYAT"]).ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void frmMalzemeIstemBilgileri_Load(object sender, EventArgs e)
        {
            bilgileriGetir();
        }

        private void simpleButtonMalzemeIstemKaydet_Click(object sender, EventArgs e)
        {
            if (nMALZEMELER_STOKMIKTARI < Convert.ToInt32(textEditMalzemeIstemAdedi.Text))
            {
                XtraMessageBox.Show("STOK MİKTARI YETERLİ DEĞİL !!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Convert.ToInt32(textEditMalzemeIstemAdedi.Text) < 1)
            {
                XtraMessageBox.Show("ADET MİKTARI 1 ' DEN KÜÇÜK OLAMAZ !!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textEditMalzemeIstemAdedi.Text == string.Empty)
            {
                XtraMessageBox.Show("STOK MİKTARI GÖRMELİSİNİZ !!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DialogResult.Yes == XtraMessageBox.Show("Sipariş Eklensin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Kaydet();
            }
            this.Close();
        }

        private void Kaydet()
        {
            var frmIstemler = Application.OpenForms["frmMalzemeSiparisOlustur"] as frmMalzemeSiparisOlustur;
            if (frmIstemler == null) return;

            try
            {
                DataTable dt = frmIstemler.gridControlMalzemeIstemleri.DataSource as DataTable;
                if (dt == null) return;
                DataRow dr = dt.NewRow();
                dr["MALZEMEISTEM_MALZEMELERID"] = clGenelTanim.DBToInt32(string.IsNullOrEmpty(nMALZEMELER_ID.ToString()) ? "0" : nMALZEMELER_ID.ToString());
                dr["MALZEME ADI"] = clGenelTanim.DBToString(textEditMalzemeIstemAdi.Text.ToString());
                dr["TALEP ADET"] = clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMalzemeIstemAdedi.Text.ToString()) ? "0" : textEditMalzemeIstemAdedi.Text.ToString());
                dr["TALEP TARIHI"] = frmIstemler.dateEditMalzemeIstemTarih.DateTime.ToString("yyyy-MM-dd HH:mm");
                dr["MATERYAL NO"] = clGenelTanim.DBToString(textEditMateryalNo.Text.ToString());
                dr["MALZEMEISTEM_MALZEMEDEPOISTEMID"] = clGenelTanim.DBToInt32(clGenelTanim.currentMalzemeDepoIstemID.ToString());
                dr["MALZEMEISTEM_MALZEMEDEPARTMANISTEMID"] = clGenelTanim.DBToInt32(comboBoxEditMalzemeIstemDepartman.SecilenDeger().Id.ToString());
                dr["TALEP EDEN BÖLÜM"] = clGenelTanim.DBToString(clGenelTanim.currentMalzemeKullaniciDepartmanAdi.ToString());
                dr["MASRAF YERİ"] = clGenelTanim.DBToString(comboBoxEditMalzemeIstemDepartman.SecilenDeger().Text.ToString());

                dt.Rows.InsertAt(dr, 0);

                frmIstemler.gridControlMalzemeIstemleri.DataSource = dt;

                if (frmIstemler.gridControlMalzemeIstemleri.DataSource != null)
                {
                    frmIstemler.simpleButtonMalzemeIstemVazgec.Enabled = false;
                }

                if (frmIstemler.gridControlMalzemeIstemleri.DataSource != null)
                {
                    frmIstemler.simpleButtonMalzemeIstemSil.Enabled = true;
                }


            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowImage(int malzemeId)
        {
            try
            {
                byte[] img = null;
                using (var conn = new SqlConnection(clSqlTanim.connectionString))
                using (var cmd = new SqlCommand("SELECT MALZEME_RESIM FROM TBL_LST_MALZEMELER WHERE MALZEME_ID=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", malzemeId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && !Convert.IsDBNull(reader[0]))
                            img = (byte[])reader[0];
                    }
                }
                if (img == null)
                    pictureEditGenel.Image = null;
                else
                {
                    using (var ms = new MemoryStream(img))
                        pictureEditGenel.Image = new Bitmap(Image.FromStream(ms));
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void textEditMalzemeIstemAdedi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textEditMalzemeIstemAdedi_Validating(object sender, CancelEventArgs e)
        {
        }

        private void textEditMalzemeIstemAdedi_TextChanged(object sender, EventArgs e)
        {
        }

        private void pictureEditGenel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureEditGenel_DoubleClick(object sender, EventArgs e)
        {
            frmMalzemeResim u = new frmMalzemeResim();
            u.nMALZEMELER_ID = Convert.ToInt32(this.nMALZEMELER_ID.ToString());
            u.ShowDialog();
        }

        private void pictureEditGenel_ZoomPercentChanged(object sender, EventArgs e)
        {
        }
    }
}
