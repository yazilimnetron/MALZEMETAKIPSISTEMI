using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmKullaniciEkle : FrmBase
    {
        private int currentKullaniciID = -1;
        private DataSet dsKullanici = null;
        private bool yeniKullanici = false;

        private List<KontrolVeriIliskisi> kontrolVeriIliskileriKullanici = new List<KontrolVeriIliskisi>();
        public frmKullaniciEkle()
        {
            InitializeComponent();
        }

        private void KontrolVeriIliskileriniAyarla()
        {
            if (kontrolVeriIliskileriKullanici.Count > 0)
                return;

            kontrolVeriIliskileriKullanici.Add(new KontrolVeriIliskisi { control = textEditKullaniciKodu, fieldName = "KULLANICI KODU", IsRequired = true });
            kontrolVeriIliskileriKullanici.Add(new KontrolVeriIliskisi { control = textEditKullaniciAdi, fieldName = "KULLANICI ADI", IsRequired = true });
            kontrolVeriIliskileriKullanici.Add(new KontrolVeriIliskisi { control = textEditKullaniciSifre, fieldName = "KULLANICI ŞİFRE", IsRequired = true });
            kontrolVeriIliskileriKullanici.Add(new KontrolVeriIliskisi { control = checkEditKullaniciDurum, fieldName = "KULLANILIYOR", IsRequired = false });
            kontrolVeriIliskileriKullanici.Add(new KontrolVeriIliskisi { control = checkEditKullaniciOperator, fieldName = "OPERATOR", IsRequired = false });
            kontrolVeriIliskileriKullanici.Add(new KontrolVeriIliskisi { control = checkEditKullaniciSiparisKapat, fieldName = "SIPARIS KAPAT", IsRequired = false });
            kontrolVeriIliskileriKullanici.Add(new KontrolVeriIliskisi { control = comboBoxEditBolumu, fieldName = "BOLUMU", IsRequired = false });
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

        private void simpleButtonKullaniciKaydet_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        private void kullaniciSec(int kullaniciId)
        {
            this.yeniKullanici = false;
            currentKullaniciID = kullaniciId;
            departmanDetayListele(currentKullaniciID);
        }

        private void departmanDetayListele(int kullaniciId)
        {
            StringBuilder sb = new StringBuilder();

            // layoutControlKullaniciEkle.LayoutKontrolleriniSifirla();

            sb.Append("SELECT MALZEMEKULLANICI_ID , MALZEMEKULLANICI_KODU AS 'KULLANICI KODU', MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', MALZEMEKULLANICI_DEPARTMAN AS 'BOLUMU',");
            sb.Append("MALZEMEKULLANICI_SIFRE AS 'KULLANICI ŞİFRE', MALZEMEKULLANICI_DURUM AS 'KULLANILIYOR', MALZEMEKULLANICI_OPERATOR AS 'OPERATOR', MALZEMEKULLANICI_SIPARISKAPAT AS 'SIPARIS KAPAT', MALZEMEKULLANICI_YONETICI AS 'YONETICI' ");
            sb.Append("FROM TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ");
            sb.Append("WHERE MALZEMEKULLANICI_ID=@id");

            var parameters = new[] { new SqlParameter("@id", kullaniciId) };
            DataSet ds = clSqlTanim.RunStoredProcDS(sb.ToString(), "KS", parameters);
            this.dsKullanici = ds;

            KullaniciBilgileriniDoldur(ds);
        }

        private void KullaniciBilgileriniDoldur(DataSet ds)
        {
            DataTable dtKullanici = ds.Tables[0];
            if (dtKullanici != null && dtKullanici.Rows.Count > 0)
            {
                textEditKullaniciNo.Text = clGenelTanim.DBToString(dtKullanici.Rows[0]["MALZEMEKULLANICI_ID"]);
                textEditKullaniciKodu.Text = clGenelTanim.DBToString(dtKullanici.Rows[0]["KULLANICI KODU"]);
                textEditKullaniciAdi.Text = clGenelTanim.DBToString(dtKullanici.Rows[0]["KULLANICI ADI"]);
                textEditKullaniciSifre.Text = clGenelTanim.DBToString(dtKullanici.Rows[0]["KULLANICI ŞİFRE"]);
                checkEditKullaniciDurum.Checked = clGenelTanim.DBToInt32(dtKullanici.Rows[0]["KULLANILIYOR"]) == 1;
                checkEditKullaniciOperator.Checked = clGenelTanim.DBToInt32(dtKullanici.Rows[0]["OPERATOR"]) == 1;
                checkEditKullaniciSiparisKapat.Checked = clGenelTanim.DBToInt32(dtKullanici.Rows[0]["SIPARIS KAPAT"]) == 1;
                checkEditKullaniciYonetici.Checked = clGenelTanim.DBToInt32(dtKullanici.Rows[0]["YONETICI"]) == 1;
                comboBoxEditBolumu.DegerSec(clGenelTanim.DBToInt32(dtKullanici.Rows[0]["BOLUMU"]));
                //checkEditDokumaOrgu.Checked = clGenelTanim.DBToInt32(dtKullanici.Rows[0]["DOKUMA ÖRGÜ OPERATÖR"]) == 1;
            }
        }

        private void Kaydet()
        {
            if (GerekliAlanlarKontrolu(kontrolVeriIliskileriKullanici))
            {
                XtraMessageBox.Show("Girilmesi zorunlu alanlar var", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtKullanici = this.yeniKullanici ? null : dsKullanici.Tables[0];
            DataRow rowDepartman = dtKullanici == null ? null : dtKullanici.Rows[0];

            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    if (rowDepartman == null)
                    {
                        string insertSql = "INSERT INTO TBL_LST_MALZEMEKULLANICILAR " +
                            "(MALZEMEKULLANICI_KODU, MALZEMEKULLANICI_ADI, MALZEMEKULLANICI_SIFRE, " +
                            "MALZEMEKULLANICI_DURUM, MALZEMEKULLANICI_OPERATOR, MALZEMEKULLANICI_SIPARISKAPAT, " +
                            "MALZEMEKULLANICI_YONETICI, MALZEMEKULLANICI_DEPARTMAN) " +
                            "VALUES (@kodu, @adi, @sifre, @durum, @operator, @siparisKapat, @yonetici, @departman); " +
                            "SELECT SCOPE_IDENTITY()";
                        var insertParams = new SqlParameter[]
                        {
                            new SqlParameter("@kodu", textEditKullaniciKodu.Text),
                            new SqlParameter("@adi", textEditKullaniciAdi.Text),
                            new SqlParameter("@sifre", textEditKullaniciSifre.Text),
                            new SqlParameter("@durum", clGenelTanim.DBToInt32(checkEditKullaniciDurum.Checked)),
                            new SqlParameter("@operator", clGenelTanim.DBToInt32(checkEditKullaniciOperator.Checked)),
                            new SqlParameter("@siparisKapat", clGenelTanim.DBToInt32(checkEditKullaniciSiparisKapat.Checked)),
                            new SqlParameter("@yonetici", clGenelTanim.DBToInt32(checkEditKullaniciYonetici.Checked)),
                            new SqlParameter("@departman", clGenelTanim.DBToInt32(comboBoxEditBolumu.SecilenDeger().Id.ToString())),
                        };
                        DataTable dt = clSqlTanim.RunStoredProc(insertSql, insertParams);
                        this.currentKullaniciID = (dt != null && dt.Rows.Count > 0)
                            ? clGenelTanim.DBToInt32(dt.Rows[0][0])
                            : -1;
                    }
                    else
                    {
                        string updateSql = "UPDATE TBL_LST_MALZEMEKULLANICILAR SET " +
                            "MALZEMEKULLANICI_KODU=@kodu, MALZEMEKULLANICI_ADI=@adi, MALZEMEKULLANICI_SIFRE=@sifre, " +
                            "MALZEMEKULLANICI_DURUM=@durum, MALZEMEKULLANICI_OPERATOR=@operator, " +
                            "MALZEMEKULLANICI_SIPARISKAPAT=@siparisKapat, MALZEMEKULLANICI_YONETICI=@yonetici, " +
                            "MALZEMEKULLANICI_DEPARTMAN=@departman " +
                            "WHERE MALZEMEKULLANICI_ID=@id";
                        var updateParams = new SqlParameter[]
                        {
                            new SqlParameter("@kodu", textEditKullaniciKodu.Text),
                            new SqlParameter("@adi", textEditKullaniciAdi.Text),
                            new SqlParameter("@sifre", textEditKullaniciSifre.Text),
                            new SqlParameter("@durum", clGenelTanim.DBToInt32(checkEditKullaniciDurum.Checked)),
                            new SqlParameter("@operator", clGenelTanim.DBToInt32(checkEditKullaniciOperator.Checked)),
                            new SqlParameter("@siparisKapat", clGenelTanim.DBToInt32(checkEditKullaniciSiparisKapat.Checked)),
                            new SqlParameter("@yonetici", clGenelTanim.DBToInt32(checkEditKullaniciYonetici.Checked)),
                            new SqlParameter("@departman", clGenelTanim.DBToInt32(comboBoxEditBolumu.SecilenDeger().Id.ToString())),
                            new SqlParameter("@id", clGenelTanim.DBToInt32(textEditKullaniciNo.Text)),
                        };
                        clSqlTanim.ExecuteNonQuery(updateSql, updateParams);
                    }

                    InitForm();
                    XtraMessageBox.Show("Kayıt İşlemi Başarılı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.simpleButtonKullaniciKaydet.Enabled = false;
                    this.textEditKullaniciKodu.Enabled = false;
                    this.textEditKullaniciSifre.Enabled = false;
                    this.textEditKullaniciAdi.Enabled = false;
                    this.comboBoxEditBolumu.Enabled = false;
                    kullaniciSec(this.currentKullaniciID);
                }
                catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            }
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT MALZEMEKULLANICI_ID , MALZEMEKULLANICI_KODU AS 'KULLANICI KODU', MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', MALZEME_DEPARTMANADI AS 'BÖLÜMÜ',");
            sb.Append("MALZEMEKULLANICI_SIFRE AS 'KULLANICI ŞİFRE', MALZEMEKULLANICI_DURUM AS 'KULLANILIYOR', MALZEMEKULLANICI_OPERATOR AS 'OPERATOR', MALZEMEKULLANICI_SIPARISKAPAT AS 'SIPARIS KAPAT', MALZEMEKULLANICI_YONETICI AS 'YONETICI' ");
            sb.Append("FROM TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ");
            sb.Append("LEFT JOIN TBL_LST_MALZEMEDEPARTMANLAR ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_DEPARTMAN=TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID ");
            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlKullaniciListe.DataSource = dtMalzemeler;

            this.gridViewKullaniciListe.Columns["MALZEMEKULLANICI_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewKullaniciListe.Columns["MALZEMEKULLANICI_ID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewKullaniciListe.Columns["MALZEMEKULLANICI_ID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewKullaniciListe.Columns["MALZEMEKULLANICI_ID"].Visible = false;

            this.gridViewKullaniciListe.Columns["MALZEMEKULLANICI_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewKullaniciListe, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewKullaniciListe.BestFitColumns();

            this.KontrolVeriIliskileriniAyarla();
        }

        private void simpleButtonKullaniciYeniKayit_Click(object sender, EventArgs e)
        {
            this.yeniKullanici = true;
            this.simpleButtonKullaniciKaydet.Enabled = true;
            this.textEditKullaniciKodu.Enabled = true;
            this.textEditKullaniciSifre.Enabled = true;
            this.textEditKullaniciAdi.Enabled = true;
            this.comboBoxEditBolumu.Enabled = true;
            layoutControlKullaniciEkle.LayoutKontrolleriniSifirla();
        }

        private void frmKullaniciEkle_Load(object sender, EventArgs e)
        {
            comboBoxEditBolumu.Doldur("SELECT MALZEME_DEPARTMANID, MALZEME_DEPARTMANADI AS 'MALZEMEISTEM DEPARTMANADI' FROM TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ORDER BY MALZEME_DEPARTMANADI", true);
            comboBoxEditBolumu.SelectedIndex = 0;

            InitForm();
        }

        private void frmKullaniciEkle_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            //frmGirisE.pictureEdit1.BringToFront();
        }

        private void simpleButtonKullaniciKapat_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();

                //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
                //frmGirisE.pictureEdit1.BringToFront();
            }
        }

        private void simpleButtonKullaniciSil_Click(object sender, EventArgs e)
        {
            DataRow dr = dsKullanici.Tables[0].Rows[0];
            string title = clGenelTanim.DBToString(dr["KULLANICI KODU"]) + " - " + clGenelTanim.DBToString(dr["KULLANICI ADI"]) + " silinsin mi?";

            if (DialogResult.Yes == XtraMessageBox.Show(title, "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                var deleteParams = new[] { new SqlParameter("@id", this.currentKullaniciID) };
                clSqlTanim.ExecuteNonQuery("DELETE FROM TBL_LST_MALZEMEKULLANICILAR WHERE MALZEMEKULLANICI_ID=@id", deleteParams);

                InitForm();

                this.currentKullaniciID = -1;
                this.simpleButtonKullaniciKaydet.Enabled = false;
                kullaniciSec(this.currentKullaniciID);
            }
        }

        private void gridViewKullaniciListe_DoubleClick(object sender, EventArgs e)
        {
            var item = gridViewKullaniciListe.GetFocusedDataRow();
            if (item != null)
            {
                this.simpleButtonKullaniciKaydet.Enabled = true;
                this.textEditKullaniciAdi.Enabled = true;
                this.textEditKullaniciSifre.Enabled = true;
                this.comboBoxEditBolumu.Enabled = true;
                kullaniciSec(clGenelTanim.DBToInt32(item["MALZEMEKULLANICI_ID"]));
            }
        }
    }
}
