using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MALZEME_TAKIP_SISTEMI.DevExpressExtentions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmAnaGrupEkle : Form
    {
        private int currentGrupID = -1;
        private DataSet dsGrup = null;
        private bool yeniGrup = false;
        private List<KontrolVeriIliskisi> kontrolVeriIliskileriGrup = new List<KontrolVeriIliskisi>();
        public frmAnaGrupEkle()
        {
            InitializeComponent();
        }

        void SetGridFont(GridView view, Font font)
        {
            foreach (AppearanceObject ap in view.Appearance)

                ap.Font = font;
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

            StringBuilder sbI = new StringBuilder(512);
            StringBuilder sbU = new StringBuilder(512);

            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    if (rowGrup == null)
                    {
                        sbI.Append("insert into TBL_LST_MALZEMEANAGRUPLAR ( MALZEMEANAGRUP_ADI, MALZEMEANAGRUP_DURUM ) select");
                        sbI.AppendFormat("  {0}", clGenelTanim.tosqlstring(textEditGrupAdi.Text.ToString(), 50, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(checkEditGrupDurum.Checked));
                        string insertQuery = sbI.ToString() + "\r\nSELECT @@IDENTITY";
                        DataTable dt = clSqlTanim.RunStoredProc(insertQuery);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            this.currentGrupID = clGenelTanim.DBToInt32(dt.Rows[0][0]);
                        }
                        else
                        {
                            this.currentGrupID = -1;
                        }
                    }
                    else
                    {
                        sbU.Append("update TBL_LST_MALZEMEANAGRUPLAR set ");
                        sbU.AppendFormat("  MALZEMEANAGRUP_ADI={0}", clGenelTanim.tosqlstring(textEditGrupAdi.Text.ToString(), 50, true));
                        sbU.AppendFormat(" ,MALZEMEANAGRUP_DURUM={0}", clGenelTanim.DBToInt32(checkEditGrupDurum.Checked));
                        sbU.AppendFormat(" where MALZEMEANAGRUP_ID={0}", clGenelTanim.DBToInt32(textEditGrupNo.Text.ToString()));
                        clSqlTanim.RunStoredProc(sbU.ToString());
                    }

                    if (sbI.Length > 50 || sbU.Length > 50)
                    {
                        InitForm();
                        XtraMessageBox.Show("Kayıt İşlemi Başarılı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.simpleButtonGrupKaydet.Enabled = false;
                        this.textEditGrupAdi.Enabled = false;
                        grupSec(currentGrupID);

                    }
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
            //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            //frmGirisE.pictureEdit1.BringToFront();
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
            sb.AppendFormat("WHERE MALZEMEGRUP_ID={0}", grupId.ToString());

            DataSet ds = clSqlTanim.RunStoredProcDS(sb.ToString(), "GD");
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
                StringBuilder sbD = new StringBuilder(300);
                sbD.AppendFormat("delete from TBL_LST_MALZEMEANAGRUPLAR where MALZEMEANAGRUP_ID={0}", this.currentGrupID);

                clSqlTanim.ExecuteNonQuery(sbD.ToString());

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

                //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
                //frmGirisE.pictureEdit1.BringToFront();
            }
        }
    }
}
