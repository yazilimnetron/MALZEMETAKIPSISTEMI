using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmAnaGrupEkle : FrmBase
    {
        private int currentGrupID = -1;
        private DataSet dsGrup = null;
        private bool yeniGrup = false;
        private List<KontrolVeriIliskisi> kontrolVeriIliskileriGrup = new List<KontrolVeriIliskisi>();
        public frmAnaGrupEkle()
        {
            InitializeComponent();
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
                            "INSERT INTO TBL_LST_MALZEMEANAGRUPLAR (MALZEMEANAGRUP_ADI, MALZEMEANAGRUP_DURUM) VALUES (@adi, @durum); SELECT SCOPE_IDENTITY()",
                            new[] {
                                new System.Data.SqlClient.SqlParameter("@adi",   textEditGrupAdi.Text),
                                new System.Data.SqlClient.SqlParameter("@durum", clGenelTanim.DBToInt32(checkEditGrupDurum.Checked))
                            });
                        this.currentGrupID = dt != null && dt.Rows.Count > 0 ? clGenelTanim.DBToInt32(dt.Rows[0][0]) : -1;
                    }
                    else
                    {
                        clSqlTanim.ExecuteNonQuery(
                            "UPDATE TBL_LST_MALZEMEANAGRUPLAR SET MALZEMEANAGRUP_ADI=@adi, MALZEMEANAGRUP_DURUM=@durum WHERE MALZEMEANAGRUP_ID=@id",
                            new[] {
                                new System.Data.SqlClient.SqlParameter("@adi",   textEditGrupAdi.Text),
                                new System.Data.SqlClient.SqlParameter("@durum", clGenelTanim.DBToInt32(checkEditGrupDurum.Checked)),
                                new System.Data.SqlClient.SqlParameter("@id",    clGenelTanim.DBToInt32(textEditGrupNo.Text))
                            });
                    }

                    InitForm();
                    XtraMessageBox.Show("Kayıt İşlemi Başarılı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.simpleButtonGrupKaydet.Enabled = false;
                    this.textEditGrupAdi.Enabled = false;
                    grupSec(currentGrupID);
                }

                catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            }
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT MALZEMEANAGRUP_ID, MALZEMEANAGRUP_ADI AS 'GRUP ADI', MALZEMEANAGRUP_DURUM AS 'KULLANILIYOR'");
            sb.Append("FROM TBL_LST_MALZEMEANAGRUPLAR (NOLOCK) ");
            sb.Append("ORDER BY 2");
            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlGruplar.DataSource = dtMalzemeler;

            this.gridViewGruplar.Columns["MALZEMEANAGRUP_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewGruplar.Columns["MALZEMEANAGRUP_ID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewGruplar.Columns["MALZEMEANAGRUP_ID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewGruplar.Columns["MALZEMEANAGRUP_ID"].Visible = false;

            this.gridViewGruplar.Columns["MALZEMEANAGRUP_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewGruplar, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewGruplar.BestFitColumns();

            this.KontrolVeriIliskileriniAyarla();
        }

        private void frmAnaGrupEkle_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void simpleButtonGrupYeniKayit_Click(object sender, EventArgs e)
        {
            this.yeniGrup = true;
            this.simpleButtonGrupKaydet.Enabled = true;
            this.textEditGrupAdi.Enabled = true;
            this.layoutControlAnaGrup.LayoutKontrolleriniSifirla();
        }

        private void simpleButtonGrupKaydet_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        private void frmAnaGrupEkle_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void KontrolVeriIliskileriniAyarla()
        {
            if (kontrolVeriIliskileriGrup.Count > 0)
                return;

            kontrolVeriIliskileriGrup.Add(new KontrolVeriIliskisi { control = textEditGrupAdi, fieldName = "DEPARTMAN ADI", IsRequired = true });
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

        private void grupSec(int departmanId)
        {
            this.yeniGrup = false;
            currentGrupID = departmanId;
            grupDetayListele(currentGrupID);
        }

        private void grupDetayListele(int grupId)
        {
            StringBuilder sb = new StringBuilder();
            layoutControlAnaGrup.LayoutKontrolleriniSifirla();

            sb.Append("SELECT MALZEMEANAGRUP_ID, MALZEMEANAGRUP_ADI AS 'GRUP ADI', MALZEMEANAGRUP_DURUM AS 'KULLANILIYOR'");
            sb.Append("FROM TBL_LST_MALZEMEANAGRUPLAR (NOLOCK) ");
            sb.Append("WHERE MALZEMEANAGRUP_ID=@grupId");

            DataSet ds = clSqlTanim.RunStoredProcDS(sb.ToString(), "GD",
                new[] { new System.Data.SqlClient.SqlParameter("@grupId", grupId) });
            this.dsGrup = ds;

            grupBilgileriniDoldur(ds);
        }

        private void grupBilgileriniDoldur(DataSet ds)
        {
            DataTable dtDepartman = ds.Tables[0];
            if (dtDepartman != null && dtDepartman.Rows.Count > 0)
            {
                textEditGrupNo.Text = clGenelTanim.DBToString(dtDepartman.Rows[0]["MALZEMEANAGRUP_ID"]);
                textEditGrupAdi.Text = clGenelTanim.DBToString(dtDepartman.Rows[0]["GRUP ADI"]);
                checkEditGrupDurum.Checked = clGenelTanim.DBToInt32(dtDepartman.Rows[0]["KULLANILIYOR"]) == 1;
            }
        }

        private void gridViewGruplar_DoubleClick(object sender, EventArgs e)
        {
            var item = gridViewGruplar.GetFocusedDataRow();
            if (item != null)
            {
                this.simpleButtonGrupKaydet.Enabled = true;
                grupSec(clGenelTanim.DBToInt32(item["MALZEMEANAGRUP_ID"]));
                this.textEditGrupAdi.Enabled = true;
            }
        }

        private void simpleButtonGrupSil_Click(object sender, EventArgs e)
        {
            DataRow dr = dsGrup.Tables[0].Rows[0];
            string title = clGenelTanim.DBToString(dr["MALZEMEANAGRUP_ID"]) + " - " + clGenelTanim.DBToString(dr["GRUP ADI"]) + " silinsin mi?";

            if (DialogResult.Yes == XtraMessageBox.Show(title, "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                clSqlTanim.ExecuteNonQuery(
                    "DELETE FROM TBL_LST_MALZEMEANAGRUPLAR WHERE MALZEMEANAGRUP_ID=@id",
                    new[] { new System.Data.SqlClient.SqlParameter("@id", this.currentGrupID) });

                InitForm();

                this.currentGrupID = -1;
                this.simpleButtonGrupKaydet.Enabled = false;
                grupSec(this.currentGrupID);
            }
        }

        private void simpleButtonGrupKapat_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }
    }
}
