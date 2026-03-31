using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeResim : Form
    {
        public int nMALZEMELER_ID = 0;

        private bool _dragging = false;
        private int _xPos, _yPos;
        private MouseWheelZoomFilter _wheelFilter;

        public frmMalzemeResim()
        {
            InitializeComponent();

            // --- Form ayarları ---
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.KeyPreview = true;
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); };
            this.Load += frmMalzemeResim_Load;
            this.FormClosed += frmMalzemeResim_FormClosed;
            this.Resize += (s, e) => CenterPictureBox();

            // --- PictureBox ayarları ---
            pictureBox1.Dock = DockStyle.None;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.BackColor = Color.Black;
            pictureBox1.Cursor = Cursors.Hand;

            // Pan (sürükle)
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;

            // Çift tıklama › fit-to-window
            pictureBox1.DoubleClick += (s, e) => FitToWindow();

            // Global mouse wheel filtresi
            _wheelFilter = new MouseWheelZoomFilter(pictureBox1, this);
            Application.AddMessageFilter(_wheelFilter);
        }

        private void frmMalzemeResim_Load(object sender, EventArgs e)
        {
            ShowImage(nMALZEMELER_ID);
            FitToWindow(); // açılışta pencereye sığdır
        }

        private void frmMalzemeResim_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_wheelFilter != null)
            {
                Application.RemoveMessageFilter(_wheelFilter);
                _wheelFilter = null;
            }
        }

        // ------------------- PAN -------------------
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _dragging = true;
            _xPos = e.X; _yPos = e.Y;
            pictureBox1.Cursor = Cursors.SizeAll;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            var c = (Control)sender;
            c.Left += e.X - _xPos;
            c.Top += e.Y - _yPos;
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
            pictureBox1.Cursor = Cursors.Hand;
        }

        // ------------------- ORTALAMA -------------------
        private void CenterPictureBox()
        {
            if (pictureBox1.Parent == null || pictureBox1.Image == null) return;
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
        }

        // ------------------- FIT-TO-WINDOW -------------------
        private void FitToWindow()
        {
            if (pictureBox1.Image == null) return;

            // Pencereye oranlı sığdırma
            float scale = Math.Min(
                (float)this.ClientSize.Width / pictureBox1.Image.Width,
                (float)this.ClientSize.Height / pictureBox1.Image.Height);

            int newW = Math.Max(1, (int)(pictureBox1.Image.Width * scale));
            int newH = Math.Max(1, (int)(pictureBox1.Image.Height * scale));

            pictureBox1.Width = newW;
            pictureBox1.Height = newH;
            CenterPictureBox();

            // Wheel filtresine başlangıç zoom’unu bildir
            _wheelFilter?.SetZoom(scale);
        }

        // ------------------- RESİM YÜKLEME -------------------
        private void ShowImage(int malzemeId)
        {
            try
            {
                byte[] img = null;

                using (var conn = new SqlConnection(clSqlTanim.connectionString))
                using (var cmd = new SqlCommand(
                    "SELECT MALZEME_RESIM FROM TBL_LST_MALZEMELER WHERE MALZEME_ID=@id", conn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = malzemeId;
                    conn.Open();
                    var o = cmd.ExecuteScalar();
                    if (o != null && o != DBNull.Value)
                        img = (byte[])o;
                }

                if (img == null || img.Length == 0)
                {
                    pictureBox1.Image = null;
                    return;
                }

                using (var ms = new MemoryStream(img))
                using (var tmp = Image.FromStream(ms))
                    pictureBox1.Image = new Bitmap(tmp);

                // Görüntüyü pencereye sığdır
                FitToWindow();
            }
            catch (Exception ex)
            {
                pictureBox1.Image = null;
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------- MOUSE WHEEL FİLTRESİ -------------------
        private class MouseWheelZoomFilter : IMessageFilter
        {
            private readonly PictureBox _pic;
            private readonly Form _form;
            private const float MinZoom = 0.1f;   // %10
            private const float MaxZoom = 10.0f;  // %1000
            private const int WM_MOUSEWHEEL = 0x020A;
            private float _zoomFactor = 1.0f;

            public MouseWheelZoomFilter(PictureBox pic, Form form)
            {
                _pic = pic;
                _form = form;
            }

            public void SetZoom(float z)
            {
                _zoomFactor = Math.Max(MinZoom, Math.Min(MaxZoom, z));
            }

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg != WM_MOUSEWHEEL || _pic == null || _pic.IsDisposed) return false;
                if (_pic.Image == null) return false;

                // İmleç resmin üzerindeyse zoom yap
                var cursor = Cursor.Position;
                var rect = _pic.RectangleToScreen(_pic.ClientRectangle);
                if (!rect.Contains(cursor)) return false;

                int delta = (short)((m.WParam.ToInt64() >> 16) & 0xffff);
                float step = (delta > 0) ? 1.10f : 0.90f;

                _zoomFactor = Math.Max(MinZoom, Math.Min(MaxZoom, _zoomFactor * step));

                int newW = Math.Max(1, (int)(_pic.Image.Width * _zoomFactor));
                int newH = Math.Max(1, (int)(_pic.Image.Height * _zoomFactor));

                _pic.Width = newW;
                _pic.Height = newH;

                // Ortalamayı koru
                _pic.Left = (_form.ClientSize.Width - newW) / 2;
                _pic.Top = (_form.ClientSize.Height - newH) / 2;

                return true; // olayı tükettik
            }
        }
    }
}