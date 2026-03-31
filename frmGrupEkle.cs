using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MALZEMETAKIPSISTEMI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmGrupEkle : FrmBase
    {
        private int currentGrupID = -1;
        private DataSet dsGrup = null;
        private bool yeniGrup = false;
        private List<KontrolVeriIliskisi> kontrolVeriIliskileriGrup = new List<KontrolVeriIliskisi>();
        public frmGrupEkle()
        {
            InitializeComponent();
        }

        private void KontrolVeriIliskileriniAyarla()
        {
            if (kontrolVeriIliskileriGrup.Count > 0)
                return;

            kontrolVeriIliskileriGrup.Add(new KontrolVeriIliskisi { control = textEditGrupAdi, fieldName = "GRUP ADI", IsRequired = true });
            kontrolVeriIliskileriGrup.Add(new KontrolVeriIliskisi { control = checkEditGrupDurum, fieldName = "KULLANILIYOR", IsRequired = false });
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

        private void grupSec(int grupId)
        {
            this.yeniGrup = false;
            currentGrupID = grupId;
            grupDetayListele(currentGrupID);
        }

        private void grupDetayListele(int grupId)
        {
            StringBuilder sb = new StringBuilder();
            layoutControlGrupEkle.LayoutKontrolleriniSifirla();

            sb.Append("SELECT g.MALZEMEGRUP_ID, g.MALZEMEGRUP_ADI AS 'GRUP ADI', ag.MALZEMEANAGRUP_ID, g.MALZEMEGRUP_DURUM AS 'KULLANILIYOR' ");
            sb.Append("FROM TBL_LST_MALZEMEGRUPLAR g (NOLOCK) ");
            sb.Append("JOIN TBL_LST_MALZEMEANAGRUPLAR ag on ag.MALZEMEANAGRUP_ID=g.MALZEMEANAGRUP_ID ");
            sb.Append("WHERE g.MALZEMEGRUP_ID=@grupId");

            DataSet ds = clSqlTanim.RunStoredProcDS(sb.ToString(), "GD",
                new[] { new System.Data.SqlClient.SqlParameter("@grupId", grupId) });
            this.dsGrup = ds;

            grupBilgileriniDoldur(ds);
        }

        private void grupBilgileriniDoldur(DataSet ds)
        {
            DataTable dtGrup = ds.Tables[0];
            if (dtGrup != null && dtGrup.Rows.Count > 0)
            {
                textEditGrupNo.Text = clGenelTanim.DBToString(dtGrup.Rows[0]["MALZEMEGRUP_ID"]);
                textEditGrupAdi.Text = clGenelTanim.DBToString(dtGrup.Rows[0]["GRUP ADI"]);
                comboBoxEditAnaGrupEkle.DegerSec(clGenelTanim.DBToInt32(dtGrup.Rows[0]["MALZEMEANAGRUP_ID"]));
                checkEditGrupDurum.Checked = clGenelTanim.DBToInt32(dtGrup.Rows[0]["KULLANILIYOR"]) == 1;
            }
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT MALZEMEGRUP_ID, MALZEMEGRUP_ADI AS 'GRUP ADI', ag.MALZEMEANAGRUP_ADI AS 'ANA GRUP ADI', MALZEMEGRUP_DURUM AS 'KULLANILIYOR' ");
            sb.Append("FROM TBL_LST_MALZEMEGRUPLAR g (NOLOCK) ");
            sb.Append("JOIN TBL_LST_MALZEMEANAGRUPLAR ag on ag.MALZEMEANAGRUP_ID=g.MALZEMEANAGRUP_ID  ");
            sb.Append("ORDER BY 2");
            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlListe.DataSource = dtMalzemeler;

            comboBoxEditAnaGrupEkle.Doldur("SELECT MALZEMEANAGRUP_ID, MALZEMEANAGRUP_ADI AS 'MALZEME ANAGRUPADI' FROM TBL_LST_MALZEMEANAGRUPLAR (NOLOCK)  WHERE ISNULL (MALZEMEANAGRUP_DURUM,0)=1 ORDER BY MALZEMEANAGRUP_ADI", true);
            comboBoxEditAnaGrupEkle.SelectedIndex = 0;

            this.gridViewListe.Columns["MALZEMEGRUP_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewListe.Columns["MALZEMEGRUP_ID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewListe.Columns["MALZEMEGRUP_ID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewListe.Columns["MALZEMEGRUP_ID"].Visible = false;

            this.gridViewListe.Columns["MALZEMEGRUP_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewListe, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewListe.BestFitColumns();

            this.KontrolVeriIliskileriniAyarla();
        }

        private void Kaydet()
        {
            if (GerekliAlanlarKontrolu(kontrolVeriIliskileriGrup))
            {
                XtraMessageBox.Show("Girilmesi zorunlu alanlar var", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtGrup = this.yeniGrup ? null : dsGrup.Tables[0];
            DataRow rowGrup = dtGrup == null ? null : dtGrup.Rows[0];

            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    if (rowGrup == null)
                    {
                        DataTable dt = clSqlTanim.RunStoredProc(
                            "INSERT INTO TBL_LST_MALZEMEGRUPLAR (MALZEMEGRUP_ADI, MALZEMEANAGRUP_ID, MALZEMEGRUP_DURUM) VALUES (@adi, @anaGrup, @durum); SELECT SCOPE_IDENTITY()",
                            new[] {
                                new System.Data.SqlClient.SqlParameter("@adi",    textEditGrupAdi.Text),
                                new System.Data.SqlClient.SqlParameter("@anaGrup", clGenelTanim.DBToInt32(comboBoxEditAnaGrupEkle.SecilenDeger().Id.ToString())),
                                new System.Data.SqlClient.SqlParameter("@durum",  clGenelTanim.DBToInt32(checkEditGrupDurum.Checked))
                            });
                        this.currentGrupID = dt != null && dt.Rows.Count > 0 ? clGenelTanim.DBToInt32(dt.Rows[0][0]) : -1;
                    }
                    else
                    {
                        clSqlTanim.ExecuteNonQuery(
                            "UPDATE TBL_LST_MALZEMEGRUPLAR SET MALZEMEGRUP_ADI=@adi, MALZEMEGRUP_DURUM=@durum, MALZEMEANAGRUP_ID=@anaGrup WHERE MALZEMEGRUP_ID=@id",
                            new[] {
                                new System.Data.SqlClient.SqlParameter("@adi",    textEditGrupAdi.Text),
                                new System.Data.SqlClient.SqlParameter("@durum",  clGenelTanim.DBToInt32(checkEditGrupDurum.Checked)),
                                new System.Data.SqlClient.SqlParameter("@anaGrup", clGenelTanim.DBToInt32(comboBoxEditAnaGrupEkle.SecilenDeger().Id.ToString())),
                                new System.Data.SqlClient.SqlParameter("@id",     clGenelTanim.DBToInt32(textEditGrupNo.Text))
                            });
                    }

                    InitForm();
                    XtraMessageBox.Show("Kayıt İşlemi Başarılı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.simpleButtonKaydet.Enabled = false;
                    this.textEditGrupAdi.Enabled = false;
                    grupSec(currentGrupID);
                }

                catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            }
        }

        private void simpleButtonYeniKayit_Click(object sender, EventArgs e)
        {
            this.yeniGrup = true;
            this.simpleButtonKaydet.Enabled = true;
            this.textEditGrupAdi.Enabled = true;
            this.layoutControlGrupEkle.LayoutKontrolleriniSifirla();
        }

        private void simpleButtonKaydet_Click(object sender, EventArgs e)
        {
            Kaydet();
        }


        private void simpleButtonSil_Click(object sender, EventArgs e)
        {
            DataRow dr = dsGrup.Tables[0].Rows[0];
            string title = clGenelTanim.DBToString(dr["MALZEMEGRUP_ID"]) + " - " + clGenelTanim.DBToString(dr["GRUP ADI"]) + " silinsin mi?";

            if (DialogResult.Yes == XtraMessageBox.Show(title, "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                clSqlTanim.ExecuteNonQuery(
                    "DELETE FROM TBL_LST_MALZEMEGRUPLAR WHERE MALZEMEGRUP_ID=@id",
                    new[] { new System.Data.SqlClient.SqlParameter("@id", this.currentGrupID) });

                InitForm();

                this.currentGrupID = -1;
                this.simpleButtonKaydet.Enabled = false;
                grupSec(this.currentGrupID);
            }
        }

        private void simpleButtonKapat_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void gridViewListe_DoubleClick(object sender, EventArgs e)
        {
            var item = gridViewListe.GetFocusedDataRow();
            if (item != null)
            {
                this.simpleButtonKaydet.Enabled = true;
                grupSec(clGenelTanim.DBToInt32(item["MALZEMEGRUP_ID"]));
                this.textEditGrupAdi.Enabled = true;
            }
        }

        private void frmGrupEkle_Load(object sender, EventArgs e)
        {
            InitForm();
        }
    }
}
