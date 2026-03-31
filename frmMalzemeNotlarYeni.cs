using DevExpress.XtraEditors;
using MALZEMETAKIPSISTEMI;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    if (rowNot == null)
                    {
                        string insertSql = "INSERT INTO TBL_LST_MALZEMENOTLAR (NOT_BASLIK, NOT_DETAY, NOT_TARIH) " +
                            "VALUES (@baslik, @detay, @tarih); SELECT SCOPE_IDENTITY()";
                        var insertParams = new SqlParameter[]
                        {
                            new SqlParameter("@baslik", textEditNotBaslik.Text),
                            new SqlParameter("@detay", richEditControlNotDetay.Text),
                            new SqlParameter("@tarih", DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                        };
                        DataTable dt = clSqlTanim.RunStoredProc(insertSql, insertParams);
                        this.currentNotID = (dt != null && dt.Rows.Count > 0)
                            ? clGenelTanim.DBToInt32(dt.Rows[0][0])
                            : -1;
                    }
                    else
                    {
                        string updateSql = "UPDATE TBL_LST_MALZEMENOTLAR SET " +
                            "NOT_BASLIK=@baslik, NOT_DETAY=@detay, NOT_TARIH=@tarih " +
                            "WHERE NOT_ID=@id";
                        var updateParams = new SqlParameter[]
                        {
                            new SqlParameter("@baslik", textEditNotBaslik.Text),
                            new SqlParameter("@detay", richEditControlNotDetay.Text),
                            new SqlParameter("@tarih", DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                            new SqlParameter("@id", clGenelTanim.DBToInt32(textEditNotID.Text)),
                        };
                        clSqlTanim.ExecuteNonQuery(updateSql, updateParams);
                    }

                    XtraMessageBox.Show("Kayıt İşlemi Başarılı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmNotlar?.InitForm();
                    this.Close();
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
            try
            {
                DataSet ds = clSqlTanim.RunStoredProcDS(
                    "SELECT NOT_ID, NOT_BASLIK, NOT_DETAY FROM TBL_LST_MALZEMENOTLAR WHERE NOT_ID=@id", "MN",
                    new[] { new SqlParameter("@id", currentNotID) });

                this.dsNot = ds;

                NotBilgileriniDoldur(ds);
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
