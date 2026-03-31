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
    public partial class frmDepartmanEkle : FrmBase
    {
        private int currentDepartmanID = -1;
        private DataSet dsDepartman = null;
        private bool yeniDepartman = false;
        private List<KontrolVeriIliskisi> kontrolVeriIliskileriDepartman = new List<KontrolVeriIliskisi>();
        public frmDepartmanEkle()
        {
            InitializeComponent();
        }

        private void Kaydet()
        {
            if (GerekliAlanlarKontrolu(kontrolVeriIliskileriDepartman))
            {
                XtraMessageBox.Show("Girilmesi zorunlu alanlar var", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtDepartman = this.yeniDepartman ? null : dsDepartman.Tables[0];
            DataRow rowDepartman = dtDepartman == null ? null : dtDepartman.Rows[0];

            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    if (rowDepartman == null)
                    {
                        DataTable dt = clSqlTanim.RunStoredProc(
                            "INSERT INTO TBL_LST_MALZEMEDEPARTMANLAR (MALZEME_DEPARTMANADI, MALZEME_DEPARTMANDURUM) VALUES (@adi, @durum); SELECT SCOPE_IDENTITY()",
                            new[] {
                                new System.Data.SqlClient.SqlParameter("@adi",   textEditDepartmanAdi.Text),
                                new System.Data.SqlClient.SqlParameter("@durum", clGenelTanim.DBToInt32(checkEditDepartmanDurum.Checked))
                            });
                        this.currentDepartmanID = dt != null && dt.Rows.Count > 0 ? clGenelTanim.DBToInt32(dt.Rows[0][0]) : -1;
                    }
                    else
                    {
                        clSqlTanim.ExecuteNonQuery(
                            "UPDATE TBL_LST_MALZEMEDEPARTMANLAR SET MALZEME_DEPARTMANADI=@adi, MALZEME_DEPARTMANDURUM=@durum WHERE MALZEME_DEPARTMANID=@id",
                            new[] {
                                new System.Data.SqlClient.SqlParameter("@adi",   textEditDepartmanAdi.Text),
                                new System.Data.SqlClient.SqlParameter("@durum", clGenelTanim.DBToInt32(checkEditDepartmanDurum.Checked)),
                                new System.Data.SqlClient.SqlParameter("@id",    clGenelTanim.DBToInt32(textEditDepartmanId.Text))
                            });
                    }

                    InitForm();
                    XtraMessageBox.Show("Kayıt İşlemi Başarılı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.simpleButtonDepartmanKaydet.Enabled = false;
                    this.textEditDepartmanAdi.Enabled = false;
                    departmanSec(currentDepartmanID);
                }

                catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            }
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT MALZEME_DEPARTMANID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', MALZEME_DEPARTMANDURUM AS 'KULLANILIYOR'");
            sb.Append("FROM TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ");
            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlDepartmanlar.DataSource = dtMalzemeler;

            this.gridViewDepartmanlar.Columns["MALZEME_DEPARTMANID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewDepartmanlar.Columns["MALZEME_DEPARTMANID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewDepartmanlar.Columns["MALZEME_DEPARTMANID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewDepartmanlar.Columns["MALZEME_DEPARTMANID"].Visible = false;

            this.gridViewDepartmanlar.Columns["MALZEME_DEPARTMANID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewDepartmanlar, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewDepartmanlar.BestFitColumns();

            this.KontrolVeriIliskileriniAyarla();
        }

        private void frmDepartmanEkle_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void simpleButtonDepartmanYeniKayit_Click(object sender, EventArgs e)
        {
            this.yeniDepartman = true;
            this.simpleButtonDepartmanKaydet.Enabled = true;
            this.textEditDepartmanAdi.Enabled = true;
            this.layoutControlDepartmanEkle.LayoutKontrolleriniSifirla();
        }

        private void simpleButtonDepartmanKaydet_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        private void frmDepartmanEkle_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void KontrolVeriIliskileriniAyarla()
        {
            if (kontrolVeriIliskileriDepartman.Count > 0)
                return;

            kontrolVeriIliskileriDepartman.Add(new KontrolVeriIliskisi { control = textEditDepartmanAdi, fieldName = "DEPARTMAN ADI", IsRequired = true });
            kontrolVeriIliskileriDepartman.Add(new KontrolVeriIliskisi { control = checkEditDepartmanDurum, fieldName = "KULLANILIYOR", IsRequired = false });
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

        private void departmanSec(int departmanId)
        {
            this.yeniDepartman = false;
            currentDepartmanID = departmanId;
            departmanDetayListele(currentDepartmanID);
        }

        private void departmanDetayListele(int departmanId)
        {
            StringBuilder sb = new StringBuilder();
            layoutControlDepartmanEkle.LayoutKontrolleriniSifirla();

            sb.Append("SELECT MALZEME_DEPARTMANID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', MALZEME_DEPARTMANDURUM AS 'KULLANILIYOR'");
            sb.Append("FROM TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ");
            sb.Append("WHERE MALZEME_DEPARTMANID=@deptId");

            DataSet ds = clSqlTanim.RunStoredProcDS(sb.ToString(), "DB",
                new[] { new System.Data.SqlClient.SqlParameter("@deptId", departmanId) });
            this.dsDepartman = ds;

            DepartmanBilgileriniDoldur(ds);
        }

        private void DepartmanBilgileriniDoldur(DataSet ds)
        {
            DataTable dtDepartman = ds.Tables[0];
            if (dtDepartman != null && dtDepartman.Rows.Count > 0)
            {
                textEditDepartmanId.Text = clGenelTanim.DBToString(dtDepartman.Rows[0]["MALZEME_DEPARTMANID"]);
                textEditDepartmanAdi.Text = clGenelTanim.DBToString(dtDepartman.Rows[0]["DEPARTMAN ADI"]);
                checkEditDepartmanDurum.Checked = clGenelTanim.DBToInt32(dtDepartman.Rows[0]["KULLANILIYOR"]) == 1;

            }
        }

        private void gridViewDepartmanlar_DoubleClick(object sender, EventArgs e)
        {
            var item = gridViewDepartmanlar.GetFocusedDataRow();
            if (item != null)
            {
                this.simpleButtonDepartmanKaydet.Enabled = true;
                departmanSec(clGenelTanim.DBToInt32(item["MALZEME_DEPARTMANID"]));
                this.textEditDepartmanAdi.Enabled = true;
            }
        }

        private void simpleButtonDepartmanKapat_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void simpleButtonDepartmanSil_Click(object sender, EventArgs e)
        {

            DataRow dr = dsDepartman.Tables[0].Rows[0];
            string title = clGenelTanim.DBToString(dr["MALZEME_DEPARTMANID"]) + " - " + clGenelTanim.DBToString(dr["DEPARTMAN ADI"]) + " silinsin mi?";

            if (DialogResult.Yes == XtraMessageBox.Show(title, "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                clSqlTanim.ExecuteNonQuery(
                    "DELETE FROM TBL_LST_MALZEMEDEPARTMANLAR WHERE MALZEME_DEPARTMANID=@id",
                    new[] { new System.Data.SqlClient.SqlParameter("@id", this.currentDepartmanID) });

                InitForm();

                this.currentDepartmanID = -1;
                this.simpleButtonDepartmanKaydet.Enabled = false;
                departmanSec(this.currentDepartmanID);
            }
        }
    }
}
