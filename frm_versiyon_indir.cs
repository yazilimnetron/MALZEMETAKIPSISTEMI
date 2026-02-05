using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
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
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;

                    await client.DownloadFileTaskAsync(new Uri(kaynak), hedef);
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
                DateTime tarih = DateTime.Now;
                tar = tarih.ToString("yyyy-MM-dd hh-mm");


                File.Move(Application.StartupPath + "\\MALZEMETAKIPSISTEMI.exe", Application.StartupPath + "\\old_malzemetakip" + tar + ".exe");
                File.Move(Application.StartupPath + "\\malzeme_takip", Application.StartupPath + "\\MALZEMETAKIPSISTEMI.exe");

                Application.Exit();

                System.Diagnostics.Process.Start(Application.StartupPath + "\\MALZEMETAKIPSISTEMI.exe");

                MessageBox.Show("Dosya indirme tamamlandı !!!");

            }
        }



        //private string hedef = Application.StartupPath + "\\malzeme_takip";
        //private string kaynak = "http://www.netronyazilim.com//malzeme_takip/MALZEME_TAKIP_SISTEMI.exe";

        //private string DosyaAdi;
        private async void frm_versiyon_indir_Load(object sender, EventArgs e)
        {
            await downloader.DownloadFileAsync();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //WebClient webClient = new WebClient();
            //webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            //webClient.DownloadFileAsync(new Uri(kaynak), hedef);

            //string DosyaAdiUrlAdresi = kaynak;
            //int karaktersayisi = DosyaAdiUrlAdresi.LastIndexOf('/');
            //DosyaAdi = DosyaAdiUrlAdresi.Remove(0, karaktersayisi + 1);

        }

        //private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    progressBar1.Value = e.ProgressPercentage;
        //    ilerlemedurum_label.Text = " (%" + e.ProgressPercentage.ToString() + ")";
        //}


        //private void Completed(object sender, AsyncCompletedEventArgs e)
        //{
        //    ilerlemedurum_label.Text = "Tamamlandı"; // Yükleme bitti

        //    string tar;
        //    DateTime tarih = DateTime.Now;
        //    tar = tarih.ToString("yyyy-MM-dd hh-mm");


        //    File.Move(Application.StartupPath + "\\MALZEME_TAKIP_SISTEMI.exe", Application.StartupPath + "\\old_malzemetakip" + tar + ".exe");
        //    File.Move(Application.StartupPath + "\\malzeme_takip", Application.StartupPath + "\\MALZEME_TAKIP_SISTEMI.exe");

        //    timer2.Stop();
        //    label1.Visible = true;

        //    Application.Exit();

        //    System.Diagnostics.Process.Start(Application.StartupPath + "\\MALZEME_TAKIP_SISTEMI.exe");
        //}

        private void timer2_Tick(object sender, EventArgs e)
        {
            //label1.Visible = label1.Visible ? false : true;
        }


    }
}
