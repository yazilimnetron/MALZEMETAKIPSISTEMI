using DevExpress.XtraEditors;
using MALZEME_TAKIP_SISTEMI;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeNotlarYeni : Form
    {

        public int currentNotID = -1;
        private DataSet dsNot = null;
        public bool yeniNot = false;

        private List<KontrolVeriIliskisi> kontrolVeriIliskileriKullanici = new List<KontrolVeriIliskisi>();
        public frmMalzemeNotlarYeni()
        {
            InitializeComponent();
        }

        private void KontrolVeriIliskileriniAyarla()
        {
            if (kontrolVeriIliskileriKullanici.Count > 0)
                return;

            kontrolVeriIliskileriKullanici.Add(new KontrolVeriIliskisi { control = textEditNotBaslik, fieldName = "NOT BAŞLIK", IsRequired = true });
            kontrolVeriIliskileriKullanici.Add(new KontrolVeriIliskisi { control = richEditControlNotDetay, fieldName = "NOT DETAY", IsRequired = true });
        }

        private bool GerekliAlanlarKontrolu(List<KontrolVeriIliskisi> lstKV)
        {
            bool gerekliAlanGirilmemis = false;
            foreach (KontrolVeriIliskisi kv in lstKV)
            {
                if (kv.IsRequired)
                {
                    if (!kv.control.VeriGirildimi())
                    {
                        dxErrorProvider1.SetErrorProviderText(kv.control, "Bu alan gereklidir");
                        gerekliAlanGirilmemis = true;
                    }
                    else
                    {
                        dxErrorProvider1.SetErrorProviderText(kv.control, string.Empty);
                    }
                }

            }

            return gerekliAlanGirilmemis;
        }

        private void Kaydet()
        {
            if (GerekliAlanlarKontrolu(kontrolVeriIliskileriKullanici))
            {
                XtraMessageBox.Show("Girilmesi zorunlu alanlar var", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmMalzemeNotlar frmNotlar = ((frmMalzemeNotlar)Application.OpenForms["frmMalzemeNotlar"]);


            DataTable dtNot = this.yeniNot ? null : dsNot.Tables[0];
            DataRow rowNot = dtNot == null ? null : dtNot.Rows[0];

            StringBuilder sbI = new StringBuilder(512);
            StringBuilder sbU = new StringBuilder(512);

            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    if (rowNot == null)
                    {
                        sbI.Append("insert into TBL_LST_MALZEMENOTLAR ( NOT_BASLIK, NOT_DETAY, NOT_TARIH ) select");
                        sbI.AppendFormat("  {0}", clGenelTanim.tosqlstring(textEditNotBaslik.Text.ToString(), 500, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(richEditControlNotDetay.Text.ToString(), 5000, true));
                        sbI.AppendFormat(" ,'{0}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                        DataTable dt = clSqlTanim.RunStoredProc(sbI.ToString());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            this.currentNotID = clGenelTanim.DBToInt32(dt.Rows[0][0]);
                        }
                        else
                        {
                            this.currentNotID = -1;
                        }

                    }
                    else
                    {
                        sbU.Append("update TBL_LST_MALZEMENOTLAR set ");
                        sbU.AppendFormat("  NOT_BASLIK={0}", clGenelTanim.tosqlstring(textEditNotBaslik.Text.ToString(), 500, true));
                        sbU.AppendFormat(" ,NOT_DETAY={0}", clGenelTanim.tosqlstring(richEditControlNotDetay.Text.ToString(), 5000, true));
                        sbU.AppendFormat(" ,NOT_TARIH='{0}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        sbU.AppendFormat("  where NOT_ID={0}", clGenelTanim.DBToString(textEditNotID.Text.ToString()));

                        clSqlTanim.RunStoredProc(sbU.ToString());
                    }

                    if (sbI.Length > 50 || sbU.Length > 50)
                    {
                        XtraMessageBox.Show("Kayıt İşlemi Başarılı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmNotlar.InitForm();
                        this.Close();
                    }
                }
                catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            }
        }

        private void simpleButtonKapat_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void simpleButtonKaydet_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        void bilgileriGetir()
        {
            StringBuilder sb = new StringBuilder(1024);

            try
            {
                //sb.Append("SELECT NOT_ID, NOT_BASLIK, NOT_DETAY FROM TBL_LST_MALZEMENOTLAR ");
                //sb.Append("WHERE NOT_ID = " + currentNotID.ToString());

                DataSet ds = clSqlTanim.RunStoredProcDS(string.Format("SELECT NOT_ID, NOT_BASLIK, NOT_DETAY FROM TBL_LST_MALZEMENOTLAR WHERE NOT_ID ={0}", currentNotID.ToString()), "MN");

                this.dsNot = ds;

                NotBilgileriniDoldur(ds);

                //DataTable dt = clSqlTanim.RunStoredProc(sb.ToString());

                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    textEditNotID.Text = clGenelTanim.DBToString(dt.Rows[0]["NOT_ID"]);
                //    textEditNotBaslik.Text = clGenelTanim.DBToString(dt.Rows[0]["NOT_BASLIK"]).ToString();
                //    richEditControlNotDetay.Text = clGenelTanim.DBToString(dt.Rows[0]["NOT_DETAY"]);
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void NotBilgileriniDoldur(DataSet ds)
        {
            DataTable dt = ds.Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                textEditNotID.Text = clGenelTanim.DBToString(dt.Rows[0]["NOT_ID"]);
                textEditNotBaslik.Text = clGenelTanim.DBToString(dt.Rows[0]["NOT_BASLIK"]).ToString();
                richEditControlNotDetay.Text = clGenelTanim.DBToString(dt.Rows[0]["NOT_DETAY"]);
            }
        }

        private void frmMalzemeNotlarYeni_Load(object sender, EventArgs e)
        {
            bilgileriGetir();
        }
    }
}
