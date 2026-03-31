using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmGiris : Form
    {
        DataSet ds = new DataSet();
        public frmGiris()
        {
            InitializeComponent();
        }

        private void simpleButtonGiris_Click(object sender, EventArgs e)
        {
            try
            {
                ds = clSqlTanim.RunStoredProcDS(
                    "exec sel_Giris @kullaniciAdi, @sifre",
                    "ds",
                    new[] {
                        new SqlParameter("@kullaniciAdi", textBoxKullaniciAdi.Text),
                        new SqlParameter("@sifre",        textBoxKullaniciSifre.Text)
                    });
                if (ds == null) return;
                if (ds.Tables.Count < 1) return;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clGenelTanim.KullaniciKodu = clGenelTanim.DBToInt32(dr["KID"]);
                    clGenelTanim.strHostName = clGenelTanim.DBToString(dr["HOSTNAME"]);
                    clGenelTanim.currentYoneticiMi = clGenelTanim.DBToInt32(dr["YONETICIMI"]);
                    clGenelTanim.currentMalzemeKullaniciDepartmanId = clGenelTanim.DBToInt32(dr["MALZEMEKULLANICI_DEPARTMANID"]);
                    clGenelTanim.currentMalzemeKullaniciDepartmanAdi = clGenelTanim.DBToString(dr["MALZEMEKULLANICI_DEPARTMANADI"]);

                    frmGirisEkran formgiris = new frmGirisEkran();
                    formgiris.Show();
                    this.Hide();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void frmGiris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButtonGiris_Click(new object(), new System.EventArgs());
            }
        }

        private void simpleButtonCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmGiris_Load(object sender, EventArgs e)
        {
            try
            {
                CheckDemo();

                if (CheckUpdate())
                {
                    MessageBox.Show("Yeni güncelleme bulundu, indirme başlıyor...", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frm_versiyon_indir frm = new frm_versiyon_indir();
                    frm.ShowDialog();
                    this.Close();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        string ilkyazi;
        public void CheckDemo()
        {
            try
            {
                // dosyayı oku  
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(string.Concat("http://www.netronyazilim.com/malzeme_takip/programbaslangictarihi.txt"));
                StreamReader sr = new StreamReader(stream);
                ilkyazi = (sr.ReadToEnd()).ToString();
                sr.Close();


                if (ilkyazi.ToString() != null && ilkyazi.ToString() != "")
                {
                    // boş değilse bitiş tarihini alıyoruz
                    Stream streamBitis = client.OpenRead(string.Concat("http://www.netronyazilim.com/malzeme_takip/programbitistarihi.txt"));
                    StreamReader srBitis = new StreamReader(streamBitis);
                    DateTime bitistarihi = Convert.ToDateTime((srBitis.ReadToEnd()).ToString());

                    Stream streamBaslangic = client.OpenRead(string.Concat("http://www.netronyazilim.com/malzeme_takip/programbaslangictarihi.txt"));
                    StreamReader srBaslangic = new StreamReader(streamBaslangic);
                    DateTime ilkkayittarihi = Convert.ToDateTime((srBaslangic.ReadToEnd()).ToString());
                    srBaslangic.Close();

                    DateTime bugun = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    TimeSpan kacgunkaldi = bitistarihi - bugun;
                    string k = kacgunkaldi.ToString();
                    string b = k.Substring(0, 2);

                    if (Convert.ToInt32(b) <= 0)
                    {
                        //MessageBox.Show("Kullanım süresi bitmiştir. \n  Orjinal yazılım için irtibata geçiniz." +
                        //                      "\n ", "");
                        Application.Exit();
                    }
                    else
                    {
                        //MessageBox.Show(" " + kacgunkaldi.Days.ToString() + "  gün kaldı." +
                        //            "\n" +
                        //            "\n ", "");
                    }
                    srBitis.Close();
                }
                else
                {
                    //// boş sa ilk çalıştırılma tarihini girdik
                    //StreamWriter sw = new StreamWriter(@"..//..//programbaslangictarihi.txt");
                    //sw.Write(DateTime.Now.ToShortDateString());
                    //sw.Close();


                    //// son bitiş tarihinide giriyoruz
                    //StreamWriter swbitis = new StreamWriter(@"..//..//programbitistarihi.txt");
                    //swbitis.Write(DateTime.Now.AddDays(10).ToShortDateString());
                    //swbitis.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckUpdate()
        {
            bool ret;
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(string.Concat("http://www.netronyazilim.com/malzeme_takip/check.php?v=" + Program.versionCode));
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();
                if (content == "UPDATE")
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret = false;
            }
            return ret;
        }
        public static void updateMe()
        {
            Application.Run(new frm_versiyon_indir());
        }
    }
}
