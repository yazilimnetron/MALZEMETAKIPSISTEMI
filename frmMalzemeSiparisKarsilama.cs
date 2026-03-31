using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Data.SqlClient;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeSiparisKarsilama : FrmBase
    {
        public int MALZEMEDEPOISTEM_ID = 0, MALZEMEDEPARTMAN_ID = 0, MALZEME_ID = 0, MALZEMEISTEM_ID = 0;
        public int malzemesiparisKullanici = 0;
        string malzeme_DepoDurum = "";
        public frmMalzemeSiparisKarsilama()
        {
            InitializeComponent();
            layoutControlMalzemeTalepIslemleri.LayoutKontrolleriniSifirla();
        }

        public void gridTemizle()
        {
            gridControlMalzemeTalepDepartman.DataSource = null;
            gridControlMalzemeTalepleri.DataSource = null;
        }

        private static readonly string[] StatusFilters = new[]
        {
            "isnull(MALZEMEDEPOISTEM_DURUM,0) <> 2",
            "isnull(MALZEMEDEPOISTEM_DURUM,0) = 1",
            "isnull(MALZEMEDEPOISTEM_DURUM,0) = 0",
            "isnull(MALZEMEDEPOISTEM_DURUM,0) = 3",
            "isnull(MALZEMEDEPOISTEM_DURUM,0) NOT IN (2, 3)",
        };

        private void RefreshList()
        {
            int idx = radioGroupMalzemeTalepCikis.SelectedIndex;
            if (idx >= 0 && idx < StatusFilters.Length)
                LoadOrderList(StatusFilters[idx]);
        }

        public void InitForm() => LoadOrderList(StatusFilters[0]);

        public void LoadOrderList(string statusFilter)
        {
            gridTemizle();

            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI AS 'DEPARTMAN ADI', TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID, TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ADI AS 'KULLANICI ADI', ");
            sb.Append("Case When isnull(MALZEMEDEPOISTEM_DURUM,0)=0 then 'Onaysız' when isnull(MALZEMEDEPOISTEM_DURUM,0)=1 then 'Onaylı' when isnull(MALZEMEDEPOISTEM_DURUM,0) =2 then 'Reddedildi' when isnull(MALZEMEDEPOISTEM_DURUM,0)=3 then 'Teslim Edildi' end 'SİPARİŞ DURUM', MALZEMEDEPOISTEM_ISTEMTARIHI AS 'SİPARİŞ TARİHİ', MALZEMEDEPOISTEM_ONAYTARIHI AS 'ONAY TARİHİ' ");
            sb.Append("FROM TBL_LST_MALZEMEISTEM (NOLOCK) ");
            sb.Append("JOIN TBL_LST_MALZEMEDEPOISTEM (NOLOCK) ON TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ID=TBL_LST_MALZEMEISTEM.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
            sb.Append("JOIN TBL_LST_MALZEMEKULLANICILAR (NOLOCK) ON TBL_LST_MALZEMEKULLANICILAR.MALZEMEKULLANICI_ID=TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_KULLANICIID ");
            sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR (NOLOCK) ON TBL_LST_MALZEMEDEPARTMANLAR.MALZEME_DEPARTMANID = TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_DEPARTMANID ");
            sb.AppendFormat("WHERE {0} ", statusFilter);

            SqlParameter[] parameters = null;
            if (!checkEditTumListe.Checked)
            {
                sb.Append("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI,121) >= @basTarih ");
                sb.Append("AND CONVERT(varchar(10),TBL_LST_MALZEMEDEPOISTEM.MALZEMEDEPOISTEM_ISTEMTARIHI,121) <= @bitTarih ");
                parameters = new[]
                {
                    new SqlParameter("@basTarih", Convert.ToDateTime(dateEditBasTarih.DateTime).ToString("yyyy-MM-dd")),
                    new SqlParameter("@bitTarih", Convert.ToDateTime(dateEditBitTarih.DateTime).ToString("yyyy-MM-dd")),
                };
            }

            sb.Append("GROUP BY MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEME_DEPARTMANADI, MALZEMEKULLANICI_ADI, MALZEMEDEPOISTEM_DURUM, MALZEMEDEPOISTEM_ISTEMTARIHI, MALZEMEDEPOISTEM_DEPARTMANID, MALZEMEDEPOISTEM_ONAYTARIHI ");
            sb.Append("ORDER BY 1 DESC");

            DataTable dtMalzemeler = parameters != null
                ? clSqlTanim.RunStoredProc(sb.ToString(), parameters)
                : clSqlTanim.RunStoredProc(sb.ToString());
            gridControlMalzemeTalepDepartman.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].SummaryItem.DisplayFormat = "{0} Kayıt";
            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].Visible = false;
            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEDEPOISTEM_DEPARTMANID"].Visible = false;
            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeTalepDepartman.Columns["MALZEMEDEPOISTEM_DEPARTMANID"].OptionsColumn.ShowInCustomizationForm = false;
            this.SetGridFont(gridViewMalzemeTalepDepartman, new Font("Tahoma", 10, FontStyle.Bold));
            this.gridViewMalzemeTalepDepartman.BestFitColumns();
        }

        private void frmMalzemeTalepIslemleri_Load(object sender, EventArgs e)
        {
            timer1.Interval = 60 * 60 * 100;
            timer1.Start();

            RefreshList();

        }

        private void MALZEMEDEPOISTEMID(int MALZEMEDEPOISTEM_ID)
        {
            StringBuilder sb3 = new StringBuilder(512);
            sb3.Append("SELECT a.MALZEMEISTEM_ID, b.MALZEME_ID, a.MALZEMEISTEM_MALZEMEDEPOISTEMID, a.MALZEMEISTEM_MALZEMEDEPARTMANISTEMID, ");
            sb3.Append("b.MALZEME_ADI AS 'MALZEME ADI', a.MALZEMEISTEM_MATERYALNO AS 'MATERYAL NO', b.MALZEME_OUDBTNO AS 'MALZEME OUDBTNO', d.MALZEME_DEPARTMANADI, (isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) AS 'MALZEME STOKMIKTARI', ");
            sb3.Append("a.MALZEMEISTEM_ADET AS 'MALZEME TALEP ADET', b.MALZEME_MINADET AS 'MALZEME MİN ADET', b.MALZEME_MAXADET AS 'MALZEME MAX ADET', b.MALZEME_RAFNO AS 'MALZEME RAF NO', ");
            sb3.Append("(SELECT TOP 1 ISNULL(e.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS e WHERE b.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC ) as 'MALZEME BİRİM FİYAT', ");
            sb3.Append("(SELECT TOP 1 ISNULL(e.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS e WHERE b.MALZEME_ID = e.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC )*isnull(a.MALZEMEISTEM_ADET,0) AS 'MALZEME SİPARİŞ FİYAT', ");
            sb3.Append("ISNULL(a.MALZEMEISTEM_DURUM,1) AS SEC ");
            sb3.Append("FROM TBL_LST_MALZEMEISTEM a (NOLOCK) ");
            sb3.Append("INNER JOIN TBL_LST_MALZEMELER b (NOLOCK) ON b.MALZEME_ID=a.MALZEMEISTEM_MALZEMELERID ");
            sb3.Append("INNER JOIN TBL_LST_MALZEMEDEPARTMANLAR d (NOLOCK) ON a.MALZEMEISTEM_MALZEMEDEPARTMANISTEMID=d.MALZEME_DEPARTMANID ");
            sb3.Append("LEFT OUTER JOIN (Select  MALZEMEGIRIS_MALZEMELERID, Sum(MALZEMEGIRIS_ADET) GirislerToplam FROM TBL_LST_MALZEMEGIRIS GROUP BY  MALZEMEGIRIS_MALZEMELERID) g ON g.MALZEMEGIRIS_MALZEMELERID = b.MALZEME_ID ");
            sb3.Append("LEFT OUTER JOIN (Select  MALZEMECIKIS_MALZEMELERID, Sum(MALZEMECIKIS_ADET) CikislarToplam FROM TBL_LST_MALZEMECIKIS GROUP BY  MALZEMECIKIS_MALZEMELERID) c ON c.MALZEMECIKIS_MALZEMELERID = b.MALZEME_ID ");
            sb3.Append("WHERE ISNULL(a.MALZEMEISTEM_DURUM,0) <>2 and a.MALZEMEISTEM_MALZEMEDEPOISTEMID=@istemId");
            DataTable dtMalzemeler3 = clSqlTanim.RunStoredProc(sb3.ToString(), new[] { new SqlParameter("@istemId", MALZEMEDEPOISTEM_ID) });
            gridControlMalzemeTalepleri.DataSource = dtMalzemeler3;

            RepositoryItemCheckEdit chkMalzemeDurum = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit() { ValueUnchecked = 0, ValueChecked = 1, ValueGrayed = "" };
            this.gridViewMalzemeTalepleri.Columns["SEC"].ColumnEdit = chkMalzemeDurum;

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_ID"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_MALZEMEDEPARTMANISTEMID"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEME OUDBTNO"].Visible = false;

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_ID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_MALZEMEDEPARTMANISTEMID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEME OUDBTNO"].OptionsColumn.ShowInCustomizationForm = false;

            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].DisplayFormat.FormatString = "{0:c2}";


            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].SummaryItem.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepleri.Columns["MALZEME SİPARİŞ FİYAT"].SummaryItem.DisplayFormat = "TOPLAM FİYAT: {0:c2}";



            this.SetGridFont(gridViewMalzemeTalepleri, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepleri.BestFitColumns();
        }

        private void gridViewMalzemeTalepleri_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.GetTextCaption() == "SEC")
            {
                MALZEMEISTEM_ID = Convert.ToInt32(gridViewMalzemeTalepleri.GetRowCellValue(e.RowHandle, gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_ID"]).ToString());
                clSqlTanim.ExecuteNonQuery(
                    "UPDATE TBL_LST_MALZEMEISTEM SET MALZEMEISTEM_DURUM=@durum WHERE MALZEMEISTEM_ID=@id",
                    new[] { new SqlParameter("@durum", Convert.ToInt32(e.Value)), new SqlParameter("@id", MALZEMEISTEM_ID) });
            }

            if (e.Column.GetTextCaption() == "MALZEME TALEP ADET")
            {
                MALZEMEISTEM_ID = Convert.ToInt32(gridViewMalzemeTalepleri.GetRowCellValue(e.RowHandle, gridViewMalzemeTalepleri.Columns["MALZEMEISTEM_ID"]).ToString());
                clSqlTanim.ExecuteNonQuery(
                    "UPDATE TBL_LST_MALZEMEISTEM SET MALZEMEISTEM_ADET=@adet WHERE MALZEMEISTEM_ID=@id",
                    new[] { new SqlParameter("@adet", Convert.ToInt32(e.Value)), new SqlParameter("@id", MALZEMEISTEM_ID) });
            }

        }

        private void simpleButtonMalzemeTalepOnayla_Click(object sender, EventArgs e)
        {
            int deger = 0;

            if (gridViewMalzemeTalepleri.DataRowCount <= 0)
            {
                XtraMessageBox.Show("Teslim edilecek malzeme Listesi bulunamadı !!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmMalzemeSiparisKullanici frmKullanici = new frmMalzemeSiparisKullanici();
            frmKullanici.ShowDialog();

            if (DialogResult.Yes == XtraMessageBox.Show("Sipariş Teslim Edilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    bool hasTeslimSatiri = false;
                    using (var conn = new System.Data.SqlClient.SqlConnection(clSqlTanim.connectionString))
                    {
                        conn.Open();
                        using (var tx = conn.BeginTransaction())
                        try
                        {
                            for (int i = 0; i < gridViewMalzemeTalepleri.RowCount; i++)
                            {
                                deger = Convert.ToInt32(gridViewMalzemeTalepleri.GetDataRow(i)["SEC"].ToString());
                                if (deger != 1) continue;
                                hasTeslimSatiri = true;
                                using (var cmd = new System.Data.SqlClient.SqlCommand(
                                    "UPDATE TBL_LST_MALZEMEISTEM SET MALZEMEISTEM_DURUM=3 WHERE MALZEMEISTEM_MALZEMELERID=@mId AND MALZEMEISTEM_MALZEMEDEPOISTEMID=@dId",
                                    conn, tx))
                                {
                                    cmd.Parameters.AddWithValue("@mId", clGenelTanim.DBToInt32(gridViewMalzemeTalepleri.GetDataRow(i)["MALZEME_ID"]));
                                    cmd.Parameters.AddWithValue("@dId", clGenelTanim.DBToInt32(gridViewMalzemeTalepleri.GetDataRow(i)["MALZEMEISTEM_MALZEMEDEPOISTEMID"]));
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            if (!hasTeslimSatiri)
                            {
                                tx.Rollback();
                                XtraMessageBox.Show("Teslim için en az bir satır seçmelisiniz (SEC=1).", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            using (var cmd2 = new System.Data.SqlClient.SqlCommand(
                                "UPDATE TBL_LST_MALZEMEDEPOISTEM SET MALZEMEDEPOISTEM_DURUM=@d, MALZEMEDEPOISTEM_TESLIMKULLANICIID=@k, MALZEMEDEPOISTEM_TESLIMTARIHI=@t WHERE MALZEMEDEPOISTEM_ID=@id",
                                conn, tx))
                            {
                                cmd2.Parameters.AddWithValue("@d",  3);
                                cmd2.Parameters.AddWithValue("@k",  this.malzemesiparisKullanici);
                                cmd2.Parameters.AddWithValue("@t",  DateTime.Now);
                                cmd2.Parameters.AddWithValue("@id", MALZEMEDEPOISTEM_ID);
                                cmd2.ExecuteNonQuery();
                            }
                            tx.Commit();
                        }
                        catch { tx.Rollback(); throw; }
                    }

                    XtraMessageBox.Show("Sipariş Onaylandı ...");
                    RefreshList();
                }

                catch (Exception ex) { XtraMessageBox.Show(ex.Message, "BİLGİ ...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void simpleButtonMalzemeTalepYazdir_Click(object sender, EventArgs e)
        {
            XtraReport2 rapor = new XtraReport2();
            Parameter param1 = new Parameter();
            //Parameter param2 = new Parameter();

            param1.Name = "MALZEMEDEPOISTEM_ID";
            param1.Type = typeof(System.Int32);
            param1.Value = MALZEMEDEPOISTEM_ID.ToString();
            param1.Visible = false;

            //param1.Name = "MALZEMEISTEM_MALZEMEDEPOISTEMID";
            //param1.Type = typeof(System.Int32);
            //param1.Value = MALZEMEDEPOISTEM_ID.ToString();
            //param1.Visible = false;

            //param2.Name = "MALZEMEDEPOISTEM_ID";
            //param2.Type = typeof(System.Int32);
            //param2.Value = MALZEMEDEPOISTEM_ID.ToString();
            //param2.Visible = false;


            rapor.Parameters.Add(param1);
            //rapor.Parameters.Add(param2);

            rapor.FilterString = "[MALZEMEDEPOISTEM_ID]=[Parameters.MALZEMEDEPOISTEM_ID]";
            //rapor.FilterString = "[MALZEMEISTEM_MALZEMEDEPOISTEMID]=[Parameters.MALZEMEISTEM_MALZEMEDEPOISTEMID] AND [MALZEMEDEPOISTEM_ID]=[Parameters.MALZEMEDEPOISTEM_ID] ";
            rapor.RequestParameters = false;

            ReportPrintTool pt = new ReportPrintTool(rapor);
            pt.AutoShowParametersPanel = false;
            rapor.ShowPreview();
        }

        private void simpleButtonMalzemeTalepReddet_Click(object sender, EventArgs e)
        {
            var item = gridViewMalzemeTalepDepartman.GetFocusedDataRow();

            try
            {
                if (item != null)
                {
                    int depoIstemId = clGenelTanim.DBToInt32(item["MALZEMEISTEM_MALZEMEDEPOISTEMID"]);
                    using (var conn = new System.Data.SqlClient.SqlConnection(clSqlTanim.connectionString))
                    {
                        conn.Open();
                        using (var tx = conn.BeginTransaction())
                        try
                        {
                            using (var c1 = new System.Data.SqlClient.SqlCommand("UPDATE TBL_LST_MALZEMEDEPOISTEM SET MALZEMEDEPOISTEM_DURUM=@d WHERE MALZEMEDEPOISTEM_ID=@id", conn, tx))
                            { c1.Parameters.AddWithValue("@d", 2); c1.Parameters.AddWithValue("@id", depoIstemId); c1.ExecuteNonQuery(); }
                            using (var c2 = new System.Data.SqlClient.SqlCommand("UPDATE TBL_LST_MALZEMEISTEM SET MALZEMEISTEM_DURUM=@d WHERE MALZEMEISTEM_MALZEMEDEPOISTEMID=@id", conn, tx))
                            { c2.Parameters.AddWithValue("@d", 2); c2.Parameters.AddWithValue("@id", depoIstemId); c2.ExecuteNonQuery(); }
                            using (var c3 = new System.Data.SqlClient.SqlCommand("DELETE FROM TBL_LST_MALZEMECIKIS WHERE MALZEMECIKIS_MALZEMEDEPOISTEM_ID=@id", conn, tx))
                            { c3.Parameters.AddWithValue("@id", depoIstemId); c3.ExecuteNonQuery(); }
                            tx.Commit();
                        }
                        catch { tx.Rollback(); throw; }
                    }

                    XtraMessageBox.Show("Sipariş Reddetildi...");
                }

                RefreshList();
            }

            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }

        }

        private void dateEditTalepTarihi_EditValueChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void radioGroupMalzemeTalepCikis_SelectedIndexChanged(object sender, EventArgs e)
        {

            RefreshList();
        }

        private void gridViewMalzemeTalepDepartman_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string malzeme_DepoDurum = clGenelTanim.DBToString(gridViewMalzemeTalepDepartman.GetRowCellDisplayText(e.RowHandle, gridViewMalzemeTalepDepartman.Columns[4]));

                if (malzeme_DepoDurum == "Teslim Edildi")
                {
                    e.Appearance.BackColor = Color.Red;

                }
            }
        }

        private void gridViewMalzemeTalepleri_ShowingEditor(object sender, CancelEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (view.FocusedColumn.FieldName != "MALZEME TALEP ADET")
            //    e.Cancel = true;
        }

        private void checkEditTumListe_CheckedChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void gridViewMalzemeTalepDepartman_DoubleClick(object sender, EventArgs e)
        {
            var item = gridViewMalzemeTalepDepartman.GetFocusedDataRow();
            if (item != null)
            {
                malzeme_DepoDurum = clGenelTanim.DBToString(item["SİPARİŞ DURUM"]);
                MALZEMEDEPOISTEM_ID = clGenelTanim.DBToInt32(item["MALZEMEISTEM_MALZEMEDEPOISTEMID"]);
                MALZEMEDEPARTMAN_ID = clGenelTanim.DBToInt32(item["MALZEMEDEPOISTEM_DEPARTMANID"]);
                MALZEMEDEPOISTEMID(this.MALZEMEDEPOISTEM_ID);

                if (malzeme_DepoDurum == "Teslim Edildi" || malzeme_DepoDurum == "Reddedildi")
                {
                    this.simpleButtonMalzemeTalepOnayla.Enabled = false;
                    this.simpleButtonMalzemeTalepYazdir.Enabled = true;
                }
                else
                {
                    this.simpleButtonMalzemeTalepOnayla.Enabled = true;
                    this.simpleButtonMalzemeTalepYazdir.Enabled = true;
                }
            }
        }

        private void frmMalzemeTalepIslemleri_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            //frmGirisE.pictureEdit1.BringToFront();
        }

        private void simpleButtonListele_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        public void InıtForm5()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("Select COUNT(*) adet from TBL_LST_MALZEMEDEPOISTEM where MALZEMEDEPOISTEM_DURUM not in ('3','2')");
            DataTable dtMalzemeler3 = clSqlTanim.RunStoredProcDependency(sb.ToString());

        }

    }
}
