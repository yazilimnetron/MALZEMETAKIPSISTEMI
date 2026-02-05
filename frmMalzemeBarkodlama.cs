using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;



namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmMalzemeBarkodlama : DevExpress.XtraEditors.XtraForm
    {
        //private static readonly List<BarcodeFormat> Fmts = new List<BarcodeFormat> { BarcodeFormat.All_1D };
        //private TouchlessMgr _touch;
        private const int _previewWidth = 900;
        private const int _previewHeight = 600;
        int nMalzemeId = 0;
        public frmMalzemeBarkodlama()
        {
            InitializeComponent();
            //_touch = new TouchlessMgr();
        }

        private void StartCamera()
        {
            //_touch.RefreshCameraList();
            //if (_touch.Cameras.Count == 0)
            //{
            //    XtraMessageBox.Show("Web Cam Bulunmadı ...");
            //    return;
            //}

            //_touch.CurrentCamera = _touch.Cameras[0];
            //_touch.CurrentCamera.CaptureWidth = _previewWidth;
            //_touch.CurrentCamera.CaptureWidth = _previewHeight;
            //_touch.CurrentCamera.OnImageCaptured += OnImageCaptured;
        }

        private void StopCamera()
        {
            //if (_touch.CurrentCamera != null)
            //{
            //    _touch.CurrentCamera.OnImageCaptured -= OnImageCaptured;
            //    _touch.CurrentCamera.Dispose();
            //    _touch.CurrentCamera = null;
            //}
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

        //Bitmap bitmap;
        private void frmMalzemeBarkodlama_Load(object sender, EventArgs e)
        {
            //StartCamera();
        }

        //private void OnImageCaptured(object sender, CameraEventArgs args)
        //{
        //    //////Bitmap bitmap = args.Image;
        //    //////this.Invoke((MethodInvoker)delegate
        //    //////{
        //    //////    pictureBox1.Image = bitmap;
        //    //////    ReadBarcode(bitmap);
        //    //////});

        //    //bitmap = args.Image;

        //    //pictureBox1.Image = bitmap; 
        //}

        private void ReadBarcode(Bitmap bitmap)
        {
            //ZXing.BarcodeReader reader = new ZXing.BarcodeReader
            //{
            //    AutoRotate = true,
            //    TryInverted = true,
            //    Options =
            //    {
            //        PossibleFormats = Fmts,
            //        TryHarder = true,
            //        ReturnCodabarStartEnd = true,
            //        PureBarcode = false
            //    }
            //};

            //Result result = reader.Decode(bitmap);
            //if (result != null)
            //{
            //    textEditMalzemeMateryalNo.Text = result.Text;
            //}
            //else { textEditMalzemeMateryalNo.Text = "Barkod Bulunamadı !!!"; }

            //////Stopwatch sw = Stopwatch.StartNew();
            //////sw.Start();
            //////Result results = reader.Decode(bitmap);
            //////sw.Stop();

            //////textEditMalzemeMateryalNo.Text = string.Empty;
            //////textEditMalzemeAdi.Text = string.Empty;

            //////if (results != null)
            //////{
            //////    textEditMalzemeMateryalNo.Text = results.Text;
            //////}
            //////else { textEditMalzemeMateryalNo.Text = "Barkod Bulunamadı !!!"; }
        }

        private void simpleButtonOku_Click(object sender, EventArgs e)
        {
            //ReadBarcode(bitmap);
        }

        private void textEditMalzemeMateryalNo_TextChanged(object sender, EventArgs e)
        {
            if (textEditMalzemeMateryalNo.Text.Length == 8)
            {
                //StopCamera();

                StringBuilder SbS = new StringBuilder(1024);
                SbS.AppendFormat("Select MALZEME_ADI, MALZEME_ID, MALZEME_STOKMIKTARI from TBL_LST_MALZEMELER (nolock) where MALZEME_MATERYAL ={0}", clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMalzemeMateryalNo.Text.ToString()) ? "0" : textEditMalzemeMateryalNo.Text.ToString()));
                DataTable dt = clSqlTanim.RunStoredProc(SbS.ToString());

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
            //StopCamera();
        }
    }
}