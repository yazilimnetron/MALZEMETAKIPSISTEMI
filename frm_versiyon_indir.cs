using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frm_versiyon_indir : Form
    {
        private Downloader downloader;
        public frm_versiyon_indir()
        {
            InitializeComponent();

            downloader = new Downloader(progressBar1);
        }

        public class Downloader
        {
            private ProgressBar progressBar;
            private string hedef = Application.StartupPath + "\\malzeme_takip";
            private string kaynak = "http://www.netronyazilim.com//malzeme_takip/MALZEMETAKIPSISTEMI.exe";

            public Downloader(ProgressBar progressBar)
            {
                this.progressBar = progressBar;
                this.progressBar.Minimum = 0;
                this.progressBar.Maximum = 100;
                this.progressBar.Value = 0;
            }

            public async Task DownloadFileAsync()
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadProgressChanged += Client_DownloadProgressChanged;
                        client.DownloadFileCompleted += Client_DownloadFileCompleted;

                        await client.DownloadFileTaskAsync(new Uri(kaynak), hedef);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("�ndirme hatas�: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
            {
                progressBar.Invoke((MethodInvoker)delegate
                {
                    progressBar.Value = e.ProgressPercentage;
                });
            }

            string tar;
            private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
            {
                if (e.Error != null)
                {
                    MessageBox.Show("�ndirme tamamlanamad�: " + e.Error.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    DateTime tarih = DateTime.Now;
                    tar = tarih.ToString("yyyy-MM-dd hh-mm");

                    File.Move(Application.StartupPath + "\\MALZEMETAKIPSISTEMI.exe", Application.StartupPath + "\\old_malzemetakip" + tar + ".exe");
                    File.Move(Application.StartupPath + "\\malzeme_takip", Application.StartupPath + "\\MALZEMETAKIPSISTEMI.exe");

                    MessageBox.Show("Dosya indirme tamamland� !!!");
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\MALZEMETAKIPSISTEMI.exe");
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("G�ncelleme hatas�: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private async void frm_versiyon_indir_Load(object sender, EventArgs e)
        {
            await downloader.DownloadFileAsync();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        }


    }
}
