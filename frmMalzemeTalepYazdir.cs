using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class MalzemeTalepYazdir : DevExpress.XtraEditors.XtraForm
    {
        private int MALZEMETALEPCIKISDEPO_ID = 0;
        public MalzemeTalepYazdir()
        {
            InitializeComponent();
        }

        public MalzemeTalepYazdir(int MALZEMEDEPOISTEM_ID)
        {
            InitializeComponent();
            MALZEMETALEPCIKISDEPO_ID = MALZEMEDEPOISTEM_ID;
        }

        public int SatirSayisi = 0;
        DataTable dt = null;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                int i = 0;

                //ÇİZİM BAŞLANGICI
                Font myFont = new Font("Calibri", 7);
                SolidBrush sbrush = new SolidBrush(Color.Black);
                Pen myPen = new Pen(Color.Black);

                e.Graphics.DrawString("Düzenlenme Tarihi: " + DateTime.Now.ToLongDateString() + "   " + DateTime.Now.ToLongTimeString(), myFont, sbrush, 50, 25);

                e.Graphics.DrawLine(myPen, 50, 45, 770, 45); // 1. Kalem, 2. X, 3. Y Koordinatı, 4. Uzunluk, 5. BitişX 

                myFont = new Font("Calibri", 15, FontStyle.Bold);
                e.Graphics.DrawString("Malzeme Talep Listesi", myFont, sbrush, 350, 65);
                e.Graphics.DrawLine(myPen, 50, 95, 770, 95);

                myFont = new Font("Calibri", 10, FontStyle.Bold);
                e.Graphics.DrawString("MALZEME ADI", myFont, sbrush, 50, 110);
                e.Graphics.DrawString("MALZEME CIKIŞ ADET", myFont, sbrush, 300, 110);
                e.Graphics.DrawString("MALZEME CIKIŞ TARIHI", myFont, sbrush, 550, 110);

                e.Graphics.DrawLine(myPen, 50, 125, 770, 125);

                int y = 150;

                myFont = new Font("Calibri", 10);

                StringBuilder sbS = new StringBuilder(1024);
                sbS.Append("SELECT TBL_LST_MALZEMEISTEM.MALZEMEISTEM_ADI AS MALZEMECIKIS_ADI, TBL_LST_MALZEMECIKIS.MALZEMECIKIS_ADET AS MALZEMECIKIS_ADET, TBL_LST_MALZEMEISTEM.MALZEMEISTEM_ISTEMTARIHI AS MALZEMECIKIS_TARIHI, MALZEMEISTEM_MALZEMEDEPARTMANISTEMID ");
                sbS.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
                sbS.Append("JOIN TBL_LST_MALZEMECIKIS (NOLOCK) ON TBL_LST_MALZEMECIKIS.MALZEMECIKIS_MALZEMELERID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMELERID ");
                sbS.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
                sbS.Append("WHERE TBL_LST_MALZEMEISTEM.MALZEMEISTEM_DURUM=1 AND TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID=@depoId AND TBL_LST_MALZEMECIKIS.MALZEMECIKIS_DEPARTMAN=@depoId ");

                DataTable dt = clSqlTanim.RunStoredProc(sbS.ToString(), new[] {
                    new SqlParameter("@depoId", MALZEMETALEPCIKISDEPO_ID)
                });

                foreach (DataRow dr in dt.Rows)
                {
                    e.Graphics.DrawString(dr["MALZEMECIKIS_ADI"].ToString(), myFont, sbrush, 50, y);
                    e.Graphics.DrawString(dr["MALZEMECIKIS_ADET"].ToString(), myFont, sbrush, 300, y);
                    e.Graphics.DrawString(dr["MALZEMECIKIS_TARIHI"].ToString(), myFont, sbrush, 550, y);

                    //e.Graphics.DrawString(dr["MALZEME_DEPARTMANADI"].ToString(), myFont, sbrush, 600, y*5);
                    //e.Graphics.DrawString(dr["MALZEMEDEPOISTEM_IHTIYACDEPARTMAN"].ToString(), myFont, sbrush, 600, y*5);

                    //e.Graphics.DrawString(Convert.ToDouble(oku["UrunFiyat"]).ToString("c"), myFont, sbrush, 700, y);

                    y += 20;

                    i += 1;


                    //yeni sayfaya geçme kontrolü
                    if (y > 1000)
                    {
                        e.Graphics.DrawString("(Devamı -->)", myFont, sbrush, 700, y + 50);
                        y = 50;
                        break; //burada yazdırma sınırına ulaştığımız için while döngüsünden çıkıyoruz
                        //çıktığımızda whil baştan başlıyor i değişkeni değer almaya devam ediyor
                        //yazdırma yeni sayfada başlamış oluyor
                    }
                }

                //çoklu sayfa kontrolü
                if (i < SatirSayisi)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                    i = 0;
                }


                StringFormat myStringFormat = new StringFormat();
                myStringFormat.Alignment = StringAlignment.Far;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Yazdir_Load(object sender, EventArgs e)
        {

        }
    }
}