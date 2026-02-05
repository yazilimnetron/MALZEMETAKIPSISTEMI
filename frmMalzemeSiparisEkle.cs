using DevExpress.XtraEditors;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeSiparisEkle : Form
    {

        public int nMALZEMELER_ID = 0;
        public int nMALZEMELER_STOKMIKTARI = 0;

        //private int zoom = 4;
        //private bool zoomedIn = false;
        //private double originalZoomValue = 100;
        //private int index = 0;

        public frmMalzemeSiparisEkle()
        {
            InitializeComponent();
            layoutControlMalzemeIstemBilgileri.LayoutKontrolleriniSifirla();
            this.ActiveControl = textEditMalzemeIstemAdedi;
        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (keyData == Keys.Escape)
        //        Close();
        //    if (keyData == Keys.Right || keyData == Keys.Space || keyData == Keys.Up || keyData == Keys.Left || keyData == Keys.Down)
        //    {
        //        this.ShowImage(nMALZEMELER_ID);
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
        //private void UpdateZoomPercent()
        //{
        //    double max = Math.Min(((double) pictureEditGenel.ClientSize.Width) / pictureEditGenel.Image.Width, ((double)pictureEditGenel.ClientSize.Height) / pictureEditGenel.Image.Height);
        //    pictureEditGenel.Properties.ZoomPercent = ValidateZoom(max * 100);
        //    originalZoomValue = pictureEditGenel.Properties.ZoomPercent;
        //}

        //double minZoom = 25;
        //double maxZoom = 150;

        //private double ValidateZoom(double zoom)
        //{
        //    if (zoom < minZoom)
        //        return minZoom;

        //    if (zoom > maxZoom)
        //        return maxZoom;

        //    return zoom;
        //}

        void bilgileriGetir()
        {
            try
            {
                comboBoxEditMalzemeIstemDepartman.Doldur("SELECT MALZEME_DEPARTMANID, MALZEME_DEPARTMANADI AS 'MALZEME DEPARTMANADI' FROM TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK)  WHERE ISNULL (MALZEME_DEPARTMANDURUM,0)=1 ORDER BY MALZEME_DEPARTMANADI", false);
                comboBoxEditMalzemeIstemDepartman.DegerSec(clGenelTanim.DBToInt32(clGenelTanim.currentMalzemeKullanıcıDepartmanId.ToString()));

                if (string.IsNullOrEmpty(textEditMalzemeIstemAdedi.Text))
                {
                    textEditMalzemeIstemAdedi.Text = "0";
                }
                this.ShowImage(nMALZEMELER_ID);

                DataTable dt = clSqlTanim.RunStoredProc("Select s.MALZEME_MATERYAL, s.MALZEME_TURU, s.MALZEME_GRUBU, s.MALZEME_ADI, (isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) AS MALZEME_STOKMIKTARI, s.MALZEME_RAFNO, " +
                    "(SELECT TOP 1 e.MALZEMEGIRIS_SORGUBIRIMFIYAT FROM TBL_LST_MALZEMEGIRIS e WHERE s.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC ) as 'MALZEME BİRİM FİYAT'" +
                    " From TBL_LST_MALZEMELER s " +
                    "LEFT OUTER JOIN (Select  MALZEMEGIRIS_MALZEMELERID, Sum(MALZEMEGIRIS_ADET)  GirislerToplam FROM TBL_LST_MALZEMEGIRIS GROUP BY  MALZEMEGIRIS_MALZEMELERID) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID " +
                    "LEFT OUTER JOIN (Select  MALZEMECIKIS_MALZEMELERID, Sum(MALZEMECIKIS_ADET)  CikislarToplam FROM TBL_LST_MALZEMECIKIS GROUP BY  MALZEMECIKIS_MALZEMELERID ) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID " +
                    "Where s.MALZEME_ID = " + nMALZEMELER_ID.ToString());
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
            //textEditMalzemeIstemAdedi.IsModified = true;
            //textEditMalzemeIstemAdedi.DoValidate();

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
                XtraMessageBox.Show("STOK MİKTARI GİRMELİSİNİZ !!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            frmMalzemeSiparisOlustur frmIstemler = ((frmMalzemeSiparisOlustur)Application.OpenForms["frmMalzemeSiparisOlustur"]);

            try
            {
                //string date1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                //DateTime dateAsil = DateTime.Parse(date1, System.Globalization.CultureInfo.InvariantCulture); 

                DataTable dt = frmIstemler.gridControlMalzemeIstemleri.DataSource as DataTable;
                DataRow dr = dt.NewRow();
                dr["MALZEMEISTEM_MALZEMELERID"] = clGenelTanim.DBToInt32(string.IsNullOrEmpty(nMALZEMELER_ID.ToString()) ? "0" : nMALZEMELER_ID.ToString());
                dr["MALZEME ADI"] = clGenelTanim.DBToString(textEditMalzemeIstemAdi.Text.ToString());
                dr["TALEP ADET"] = clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMalzemeIstemAdedi.Text.ToString()) ? "0" : textEditMalzemeIstemAdedi.Text.ToString());
                dr["TALEP TARIHI"] = frmIstemler.dateEditMalzemeIstemTarih.DateTime.ToString("yyyy-MM-dd HH:mm");
                dr["MATERYAL NO"] = clGenelTanim.DBToString(textEditMateryalNo.Text.ToString());
                dr["MALZEMEISTEM_MALZEMEDEPOISTEMID"] = clGenelTanim.DBToInt32(clGenelTanim.currentMalzemeDepoIstemID.ToString());
                dr["MALZEMEISTEM_MALZEMEDEPARTMANISTEMID"] = clGenelTanim.DBToInt32(comboBoxEditMalzemeIstemDepartman.SecilenDeger().Id.ToString());
                dr["TALEP EDEN BÖLÜM"] = clGenelTanim.DBToString(clGenelTanim.currentMalzemeKullanıcıDepartmanAdi.ToString());
                dr["MASRAF YERİ"] = clGenelTanim.DBToString(comboBoxEditMalzemeIstemDepartman.SecilenDeger().Text.ToString());
                //dr["MALZEME BİRİM FİYAT"] = clGenelTanim.DBToDecimal(textEditMalzemeIstemFiyat.Text.ToString());

                dt.Rows.InsertAt(dr, 0);

                ((frmMalzemeSiparisOlustur)Application.OpenForms["frmMalzemeSiparisOlustur"]).gridControlMalzemeIstemleri.DataSource = dt;

                if (((frmMalzemeSiparisOlustur)Application.OpenForms["frmMalzemeSiparisOlustur"]).gridControlMalzemeIstemleri.DataSource != null)
                {
                    ((frmMalzemeSiparisOlustur)Application.OpenForms["frmMalzemeSiparisOlustur"]).simpleButtonMalzemeIstemVazgec.Enabled = false;
                }

                if (((frmMalzemeSiparisOlustur)Application.OpenForms["frmMalzemeSiparisOlustur"]).gridControlMalzemeIstemleri.DataSource != null)
                {
                    ((frmMalzemeSiparisOlustur)Application.OpenForms["frmMalzemeSiparisOlustur"]).simpleButtonMalzemeIstemSil.Enabled = true;
                }


            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string query = "";
        SqlCommand cmd;
        SqlConnection conn;
        byte[] img;
        private void ShowImage(int malzemeId)
        {
            try
            {
                img = null;
                conn = new SqlConnection(clSqlTanim.connectionString);
                query = string.Format("select MALZEME_RESIM from TBL_LST_MALZEMELER where MALZEME_ID={0}", malzemeId);
                if (conn.State != ConnectionState.Open) conn.Open();
                cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    if (!Convert.IsDBNull(reader[0]))
                    {
                        img = (byte[])(reader[0]) ?? null;
                    }
                    if (img == null)
                        pictureEditGenel.Image = null;
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureEditGenel.Image = Image.FromStream(ms);
                    }

                }
                else
                {
                    XtraMessageBox.Show("Kayıt Bulunumadı ...");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void textEditMalzemeIstemAdedi_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsNumber(e.textEditMalzemeIstemAdedi, e.Text.Length - 1))//sadece rakam girilmesine ve backspace tuşuna izin veriyor.
            //{
            //    e.Handled = true;
            //}


            //if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))//sadece rakam girilmesine ve backspace tuşuna izin veriyor.
            //{

            //}
            //else
            //{
            //    textEditMalzemeIstemAdedi.ErrorText = "The name should only contain alphanumeric characters";
            //    textEditMalzemeIstemAdedi.ErrorImageOptions.ImageUri.Uri = "SpellCheckAsYouType;Size16x16";
            //}


            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            //Regex regex = new Regex("^[0-9]*$");

            //if (!regex.IsMatch(textEditMalzemeIstemAdedi.Text))
            //{

            //    textEditMalzemeIstemAdedi.ErrorText = "The name should only contain alphanumeric characters";

            //    textEditMalzemeIstemAdedi.ErrorImageOptions.ImageUri.Uri = "SpellCheckAsYouType;Size16x16";

            //    return;

            //}
        }

        private void textEditMalzemeIstemAdedi_Validating(object sender, CancelEventArgs e)
        {
            //TextEdit textEdit = sender as TextEdit;

            //string editValue = textEdit.EditValue as string;

            //Regex regex = new Regex("^[0-9]*$");

            //if (!regex.IsMatch(editValue))
            //{

            //    textEdit.ErrorText = "The name should only contain alphanumeric characters";

            //    textEdit.ErrorImageOptions.ImageUri.Uri = "SpellCheckAsYouType;Size16x16";

            //    e.Cancel = true;

            //}
        }

        private void textEditMalzemeIstemAdedi_TextChanged(object sender, EventArgs e)
        {
            //if (System.Text.RegularExpressions.Regex.IsMatch(textEditMalzemeIstemAdedi.Text, "[^0-9]"))
            //{
            //    //XtraMessageBox.Show("SADECE SAYI GİRİNİZ !!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    string degisken = textEditMalzemeIstemAdedi.Text.Substring(0, textEditMalzemeIstemAdedi.Text.Length - 1);
            //    textEditMalzemeIstemAdedi.Text = degisken.ToString();

            //    //textEditMalzemeIstemAdedi.Text = textEditMalzemeIstemAdedi.Text.Remove(textEditMalzemeIstemAdedi.Text.Length - 1);
            //}
        }

        private void pictureEditGenel_Paint(object sender, PaintEventArgs e)
        {
            //PictureEdit edit = sender as PictureEdit;
            //PictureEditViewInfo viewInfo = edit.GetViewInfo() as PictureEditViewInfo;
            //e.Graphics.DrawRectangle(Pens.Black, new Rectangle(
            //    viewInfo.picturest,
            //    viewInfo.PictureStartY,
            //    viewInfo.PictureRect.Width - (viewInfo.PictureStartX - viewInfo.PictureRect.X) * 2,
            //    viewInfo.PictureRect.Height - (viewInfo.PictureStartY - viewInfo.PictureRect.Y) * 2));
        }

        private void pictureEditGenel_DoubleClick(object sender, EventArgs e)
        {

            frmMalzemeResim u = new frmMalzemeResim();
            u.nMALZEMELER_ID = Convert.ToInt32(this.nMALZEMELER_ID.ToString());//seçili satır ve sütunun bilgisini aktardık.
            u.ShowDialog();
            //PictureEdit pe = sender as PictureEdit;
            //if (pe == null)
            //    return;


            //if (zoomedIn)
            //{
            //    pe.Properties.ZoomPercent = originalZoomValue;
            //    pe.Refresh();
            //    zoomedIn = false;
            //}
            //else
            //{
            //    Point mousePosition = pe.PointToClient(Control.MousePosition);
            //    Point centerPoint = pe.ViewportToImage(new Point(pe.Width / 2, pe.Height / 2));
            //    Point currentPoint = pe.ViewportToImage(mousePosition);
            //    int xOffset = (currentPoint.X - centerPoint.X) * zoom;
            //    int yOffset = (currentPoint.Y - centerPoint.Y) * zoom;
            //    pe.Properties.ZoomPercent = zoom * 100;
            //    pe.HorizontalScrollPosition += xOffset;
            //    pe.VerticalScrollPosition += yOffset;
            //    pe.Refresh();
            //    zoomedIn = true;
            //}
        }

        private void pictureEditGenel_ZoomPercentChanged(object sender, EventArgs e)
        {
            //PictureEdit edit = sender as PictureEdit;
            //edit.Properties.ZoomPercent = ValidateZoom(edit.Properties.ZoomPercent);
        }
    }
}
