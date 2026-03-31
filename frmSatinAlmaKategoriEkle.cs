using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MALZEMETAKIPSISTEMI;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System.Data.SqlClient;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmSatinAlmaKategoriEkle : FrmBase
    {
        private int currentKategoriID = -1;
        private DataSet dsKategori = null;
        private bool yeniKategori = false;
        private List<KontrolVeriIliskisi> kontrolVeriIliskileriKategori = new List<KontrolVeriIliskisi>();
        public frmSatinAlmaKategoriEkle()
        {
            InitializeComponent();
        }

        private void KontrolVeriIliskileriniAyarla()
        {
            if (kontrolVeriIliskileriKategori.Count > 0)
                return;

            kontrolVeriIliskileriKategori.Add(new KontrolVeriIliskisi { control = textEditKategoriAdi, fieldName = "KATEGORİ ADI", IsRequired = true });
            kontrolVeriIliskileriKategori.Add(new KontrolVeriIliskisi { control = checkEditKategoriDurum, fieldName = "KULLANILIYOR", IsRequired = false });
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

        private void kategoriSec(int kategoriId)
        {
            this.yeniKategori = false;
            currentKategoriID = kategoriId;
            kategoriDetayListele(currentKategoriID);
        }

        private void kategoriDetayListele(int grupId)
        {
            StringBuilder sb = new StringBuilder();
            layoutControlSatinAlmaKategori.LayoutKontrolleriniSifirla();

            sb.Append("SELECT k.MALZEMEKATEGORI_ID, k.MALZEMEKATEGORI_KODU, k.MALZEMEKATEGORI_ADI AS 'KATEGORİ ADI', k.MALZEMEKATEGORIGRUP_ID, k.MALZEMEKATEGORI_DURUM AS 'KULLANILIYOR' ");
            sb.Append("FROM TBL_LST_MALZEMEKATEGORILER k (NOLOCK) ");
            sb.Append("LEFT JOIN TBL_LST_MALZEMEGRUPLAR g on k.MALZEMEKATEGORIGRUP_ID=g.MALZEMEGRUP_ID ");
            sb.Append("WHERE k.MALZEMEKATEGORI_ID=@katId");

            DataSet ds = clSqlTanim.RunStoredProcDS(sb.ToString(), "KD",
                new[] { new SqlParameter("@katId", grupId) });
            this.dsKategori = ds;

            kategoriBilgileriniDoldur(ds);
        }

        private void kategoriBilgileriniDoldur(DataSet ds)
        {
            DataTable dtGrup = ds.Tables[0];
            if (dtGrup != null && dtGrup.Rows.Count > 0)
            {
                textEditKategoriNo.Text = clGenelTanim.DBToString(dtGrup.Rows[0]["MALZEMEKATEGORI_ID"]);
                textEditKategoriKodu.Text = clGenelTanim.DBToString(dtGrup.Rows[0]["MALZEMEKATEGORI_KODU"]);
                textEditKategoriAdi.Text = clGenelTanim.DBToString(dtGrup.Rows[0]["KATEGORİ ADI"]);
                //comboBoxEditKategoriGrupEkle.DegerSec(clGenelTanim.DBToInt32(dtGrup.Rows[0]["MALZEMEKATEGORIGRUP_ID"]));
                checkEditKategoriDurum.Checked = clGenelTanim.DBToInt32(dtGrup.Rows[0]["KULLANILIYOR"]) == 1;
            }
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT k.MALZEMEKATEGORI_ID, k.MALZEMEKATEGORI_KODU AS 'KATEGORİ KODU', k.MALZEMEKATEGORI_ADI AS 'KATEGORİ ADI', k.MALZEMEKATEGORI_DURUM AS 'KULLANILIYOR' ");
            sb.Append("FROM TBL_LST_MALZEMEKATEGORILER k (NOLOCK) ");
            //sb.Append("LEFT JOIN TBL_LST_MALZEMEGRUPLAR g on k.MALZEMEKATEGORIGRUP_ID=g.MALZEMEGRUP_ID  ");
            sb.Append("ORDER BY 3");
            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlListe.DataSource = dtMalzemeler;

            //comboBoxEditKategoriGrupEkle.Doldur("SELECT MALZEMEGRUP_ID, MALZEMEGRUP_ADI AS 'MALZEME GRUPADI' FROM TBL_LST_MALZEMEGRUPLAR (NOLOCK)  WHERE ISNULL (MALZEMEGRUP_DURUM,0)=1 ORDER BY MALZEMEGRUP_ADI", true);
            //comboBoxEditKategoriGrupEkle.SelectedIndex = 0;

            this.gridViewListe.Columns["MALZEMEKATEGORI_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewListe.Columns["MALZEMEKATEGORI_ID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewListe.Columns["MALZEMEKATEGORI_ID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewListe.Columns["MALZEMEKATEGORI_ID"].Visible = false;

            this.gridViewListe.Columns["MALZEMEKATEGORI_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewListe, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewListe.BestFitColumns();

            this.KontrolVeriIliskileriniAyarla();
        }

        private void Kaydet()
        {
            if (GerekliAlanlarKontrolu(kontrolVeriIliskileriKategori))
            {
                XtraMessageBox.Show("Girilmesi zorunlu alanlar var", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtKategori = this.yeniKategori ? null : dsKategori.Tables[0];
            DataRow rowKategori = dtKategori == null ? null : dtKategori.Rows[0];

            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    if (rowKategori == null)
                    {
                        DataTable dt = clSqlTanim.RunStoredProc(
                            "INSERT INTO TBL_LST_MALZEMEKATEGORILER (MALZEMEKATEGORI_KODU, MALZEMEKATEGORI_ADI, MALZEMEKATEGORI_DURUM) VALUES (@kod, @adi, @durum); SELECT SCOPE_IDENTITY()",
                            new[] {
                                new SqlParameter("@kod",   textEditKategoriKodu.Text),
                                new SqlParameter("@adi",   textEditKategoriAdi.Text),
                                new SqlParameter("@durum", clGenelTanim.DBToInt32(checkEditKategoriDurum.Checked))
                            });
                        this.currentKategoriID = dt?.Rows.Count > 0 ? clGenelTanim.DBToInt32(dt.Rows[0][0]) : -1;
                    }
                    else
                    {
                        clSqlTanim.ExecuteNonQuery(
                            "UPDATE TBL_LST_MALZEMEKATEGORILER SET MALZEMEKATEGORI_KODU=@kod, MALZEMEKATEGORI_ADI=@adi, MALZEMEKATEGORI_DURUM=@durum WHERE MALZEMEKATEGORI_ID=@id",
                            new[] {
                                new SqlParameter("@kod",   textEditKategoriKodu.Text),
                                new SqlParameter("@adi",   textEditKategoriAdi.Text),
                                new SqlParameter("@durum", clGenelTanim.DBToInt32(checkEditKategoriDurum.Checked)),
                                new SqlParameter("@id",    clGenelTanim.DBToInt32(textEditKategoriNo.Text))
                            });
                    }

                    InitForm();
                    XtraMessageBox.Show("Kayıt İşlemi Başarılı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.simpleButtonKaydet.Enabled = false;
                    this.textEditKategoriAdi.Enabled = false;
                    textEditKategoriKodu.Enabled = false;
                    kategoriSec(currentKategoriID);
                }

                catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            }
        }

        private void simpleButtonYeniKayit_Click(object sender, EventArgs e)
        {
            this.yeniKategori = true;
            this.simpleButtonKaydet.Enabled = true;
            this.textEditKategoriAdi.Enabled = true;
            this.textEditKategoriKodu.Enabled = true;
            this.layoutControlSatinAlmaKategori.LayoutKontrolleriniSifirla();
        }

        private void simpleButtonKaydet_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        private void simpleButtonSil_Click(object sender, EventArgs e)
        {
            DataRow dr = dsKategori.Tables[0].Rows[0];
            string title = clGenelTanim.DBToString(dr["MALZEMEKATEGORI_ID"]) + " - " + clGenelTanim.DBToString(dr["KATEGORİ ADI"]) + " silinsin mi?";

            if (DialogResult.Yes == XtraMessageBox.Show(title, "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                clSqlTanim.ExecuteNonQuery(
                    "DELETE FROM TBL_LST_MALZEMEKATEGORILER WHERE MALZEMEKATEGORI_ID=@id",
                    new[] { new SqlParameter("@id", this.currentKategoriID) });

                InitForm();

                this.currentKategoriID = -1;
                this.simpleButtonKaydet.Enabled = false;
                kategoriSec(this.currentKategoriID);
            }
        }

        private void gridViewListe_DoubleClick(object sender, EventArgs e)
        {
            var item = gridViewListe.GetFocusedDataRow();
            if (item != null)
            {
                this.simpleButtonKaydet.Enabled = true;
                kategoriSec(clGenelTanim.DBToInt32(item["MALZEMEKATEGORI_ID"]));
                this.textEditKategoriAdi.Enabled = true;
                this.textEditKategoriKodu.Enabled = true;
            }
        }

        private void frmSatinAlmaKategoriEkle_Load(object sender, EventArgs e)
        {
            InitForm();
        }
    }
}
