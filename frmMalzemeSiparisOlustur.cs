using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeSiparisOlustur : FrmBase
    {
        private int currentDepartmanID = -1;
        bool siparisolusturBasildi = false;
        bool siparisgonderBasildi = false;
        bool siparissilBasildi = false;
        bool siparisvazgecBasildi = false;

        public frmMalzemeSiparisOlustur()
        {
            InitializeComponent();
        }

        public void InitForm()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT s.MALZEME_ID, s.MALZEME_MATERYAL AS 'MALZEME MATERYAL NO', s.MALZEME_ADI AS 'MALZEME ADI', s.MALZEME_PARCANO AS 'MALZEME PARÇA NO' , (isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) AS 'MALZEME STOK', d.MALZEMEANAGRUP_ID, d.MALZEMEANAGRUP_ADI AS 'MALZEME ANA GRUBU', s.MALZEME_GRUBU AS 'MALZEME GRUBU' ");
            sb.Append("FROM TBL_LST_MALZEMELER s (NOLOCK) JOIN TBL_LST_MALZEMEANAGRUPLAR d on s.MALZEME_ANAGRUBU=d.MALZEMEANAGRUP_ID ");
            sb.Append("LEFT OUTER JOIN (Select  MALZEMEGIRIS_MALZEMELERID, Sum(MALZEMEGIRIS_ADET)  GirislerToplam  FROM TBL_LST_MALZEMEGIRIS GROUP BY  MALZEMEGIRIS_MALZEMELERID ) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("LEFT OUTER JOIN (Select  MALZEMECIKIS_MALZEMELERID, Sum(MALZEMECIKIS_ADET)  CikislarToplam  FROM TBL_LST_MALZEMECIKIS GROUP BY  MALZEMECIKIS_MALZEMELERID ) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID ");
            sb.AppendFormat("WHERE (isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) >= 0 ");
            DataTable dtMalzemeIstemler = clSqlTanim.RunStoredProc(sb.ToString());
            gridControlMalzemeIstemListesi.DataSource = dtMalzemeIstemler;

            this.gridViewMalzemeIstemListesi.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeIstemListesi.Columns["MALZEME_ID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeIstemListesi.Columns["MALZEME_ID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeIstemListesi.Columns["MALZEME_ID"].Visible = false;
            this.gridViewMalzemeIstemListesi.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.gridViewMalzemeIstemListesi.Columns["MALZEMEANAGRUP_ID"].Visible = false;

            this.SetGridFont(gridViewMalzemeIstemListesi, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeIstemListesi.BestFitColumns();
        }

        public void InitForm2()
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append("SELECT a.MALZEMEISTEM_ID, a.MALZEMEISTEM_MALZEMELERID, a.MALZEMEISTEM_MATERYALNO AS 'MATERYAL NO', a.MALZEMEISTEM_OUDBTNO AS 'MALZEME NO', a.MALZEMEISTEM_ADI AS 'MALZEME ADI', a.MALZEMEISTEM_ADET AS 'TALEP ADET', b.MALZEME_DEPARTMANADI AS 'TALEP EDEN BÖLÜM', c.MALZEME_DEPARTMANADI AS 'MASRAF YERİ', a.MALZEMEISTEM_ISTEMTARIHI AS 'TALEP TARIHI', a.MALZEMEISTEM_MALZEMEDEPOISTEMID, a.MALZEMEISTEM_MALZEMEDEPARTMANISTEMID, ");
            sb.Append("ISNULL(g.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) AS 'MALZEME BİRİM FİYAT' , ISNULL(g.MALZEMEGIRIS_SORGUBIRIMFIYAT,0)*ISNULL(a.MALZEMEISTEM_ADET,0) AS 'MALZEME SİPARİŞ FİYAT' ");
            sb.Append("FROM TBL_LST_MALZEMEISTEM a (NOLOCK) ");
            sb.Append("JOIN TBL_LST_MALZEMEGIRIS g (NOLOCK) ON a.MALZEMEISTEM_MALZEMELERID= g.MALZEMEGIRIS_MALZEMELERID ");
            sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR b (NOLOCK) ON a.MALZEMEISTEM_MALZEMEDEPOISTEMID=b.MALZEME_DEPARTMANID ");
            sb.Append("JOIN TBL_LST_MALZEMEDEPARTMANLAR c (NOLOCK) ON a.MALZEMEISTEM_MALZEMEDEPARTMANISTEMID=c.MALZEME_DEPARTMANID ");
            sb.Append("WHERE a.MALZEMEISTEM_MALZEMEDEPOISTEMID=@istemId");
            int istemId = clGenelTanim.DBToInt32(string.IsNullOrEmpty(textEditMalzemeIstemKayitNo.Text) ? "0" : textEditMalzemeIstemKayitNo.Text);
            DataTable dtMalzemeIstemler = clSqlTanim.RunStoredProc(sb.ToString(), new[] { new SqlParameter("@istemId", istemId) });
            gridControlMalzemeIstemleri.DataSource = dtMalzemeIstemler;

            this.gridViewMalzemeIstemleri.Columns["MALZEMEISTEM_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeIstemleri.Columns["MALZEMEISTEM_ID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeIstemleri.Columns["MALZEMEISTEM_ID"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeIstemleri.Columns[3].Width = 500;
            this.gridViewMalzemeIstemleri.Columns[4].Width = 500;
            this.gridViewMalzemeIstemleri.Columns[6].Width = 300;
            this.gridViewMalzemeIstemleri.Columns[7].Width = 300;

            this.gridViewMalzemeIstemleri.Columns["MALZEMEISTEM_ID"].Visible = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEMEISTEM_MALZEMELERID"].Visible = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].Visible = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEMEISTEM_MALZEMEDEPARTMANISTEMID"].Visible = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEME NO"].Visible = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEME BİRİM FİYAT"].Visible = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEME SİPARİŞ FİYAT"].Visible = false;

            this.gridViewMalzemeIstemleri.Columns["MALZEMEISTEM_ID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEMEISTEM_MALZEMELERID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEMEISTEM_MALZEMEDEPOISTEMID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEMEISTEM_MALZEMEDEPARTMANISTEMID"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEME NO"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEME BİRİM FİYAT"].OptionsColumn.ShowInCustomizationForm = false;
            this.gridViewMalzemeIstemleri.Columns["MALZEME SİPARİŞ FİYAT"].OptionsColumn.ShowInCustomizationForm = false;


            this.SetGridFont(gridViewMalzemeIstemleri, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeIstemleri.BestFitColumns();
        }

        private void frmMalzemeIstem_Load(object sender, EventArgs e)
        {
            layoutControlMalzemeIstem.LayoutKontrolleriniSifirla();


            SetContainsFilter(gridViewMalzemeIstemListesi);
        }

        private void gridViewMalzemeIstem_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();//sanal sütun tanımladık
            col = gridViewMalzemeIstemListesi.Columns[0];//varolan sütünü sanala atadık.
            int[] i = gridViewMalzemeIstemListesi.GetSelectedRows();//seçili satırın numarasını getirdik
            frmMalzemeSiparisEkle u = new frmMalzemeSiparisEkle();
            u.nMALZEMELER_ID = Convert.ToInt32(gridViewMalzemeIstemListesi.GetRowCellValue(i[0], col));//seçili satır ve sütunun bilgisini aktardık.
            u.ShowDialog();
        }

        private void textEditMalzemeIstemAdi_EditValueChanged(object sender, EventArgs e)
        {
            this.gridViewMalzemeIstemListesi.ActiveFilterCriteria = new BinaryOperator(new OperandProperty("MALZEME ADI"), new OperandValue("%" + this.textEditMalzemeIstemAdi.Text + "%"), BinaryOperatorType.Like);
        }

        private void simpleButtonMalzemeIstemYeni_Click(object sender, EventArgs e)
        {
            siparisolusturBasildi = true;

            this.gridControlMalzemeIstemleri.Enabled = true;
            this.gridControlMalzemeIstemListesi.Enabled = true;
            this.dateEditMalzemeIstemTarih.Enabled = true;
            this.textEditMalzemeIstemAdi.Enabled = true;
            this.simpleButtonMalzemeIstemVazgec.Enabled = true;
            this.simpleButtonMalzemeIstemGonder.Enabled = true;
            this.simpleButtonMalzemeIstemYeni.Enabled = false;

            InitForm();
            InitForm2();

            try
            {
                DataTable dt = clSqlTanim.RunStoredProc(
                    "INSERT INTO TBL_LST_MALZEMEDEPOISTEM (MALZEMEDEPOISTEM_DEPARTMANID, MALZEMEDEPOISTEM_KULLANICIID, MALZEMEDEPOISTEM_ISTEMTARIHI) VALUES (@deptId, @kulId, @tarih); SELECT SCOPE_IDENTITY()",
                    new[] {
                        new SqlParameter("@deptId", clGenelTanim.currentMalzemeKullaniciDepartmanId),
                        new SqlParameter("@kulId",  clGenelTanim.KullaniciKodu),
                        new SqlParameter("@tarih",  dateEditMalzemeIstemTarih.DateTime)
                    });

                if (dt != null && dt.Rows.Count > 0)
                {
                    clGenelTanim.currentMalzemeDepoIstemID = clGenelTanim.DBToInt32(dt.Rows[0][0]);
                    textEditMalzemeIstemKayitNo.Text = clGenelTanim.currentMalzemeDepoIstemID.ToString();

                }
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }

        }

        private void simpleButtonMalzemeIstemGonder_Click(object sender, EventArgs e)
        {
            siparisgonderBasildi = true;

            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = gridControlMalzemeIstemleri.DataSource as DataTable;
            if (dt == null) return;

            if (gridViewMalzemeIstemleri.DataRowCount < 1)
            {
                XtraMessageBox.Show("Siparişi göndermek için en az bir tane seçmelisiniz !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DialogResult.Yes == XtraMessageBox.Show("Siparişinizi onaylıyor musunuz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    int malzemeDepoIstemId = clGenelTanim.DBToInt32(textEditMalzemeIstemKayitNo.Text);
                    bool hasAddedRow = false;
                    string istemTarih = Convert.ToDateTime(DateTime.Now.ToString()).Equals(clGenelTanim.dateNull)
                        ? null
                        : Convert.ToDateTime(dateEditMalzemeIstemTarih.DateTime).ToString("yyyy-MM-dd HH:mm");
                    const string insertIstem = "INSERT INTO TBL_LST_MALZEMEISTEM (MALZEMEISTEM_MALZEMELERID, MALZEMEISTEM_ADI, MALZEMEISTEM_ADET, MALZEMEISTEM_ISTEMTARIHI, MALZEMEISTEM_MALZEMEDEPOISTEMID, MALZEMEISTEM_MATERYALNO, MALZEMEISTEM_MALZEMEDEPARTMANISTEMID) VALUES (@malzemeId, @adi, @adet, @tarih, @depoIstemId, @materyalNo, @departmanIstemId)";

                    using (var conn = new SqlConnection(clSqlTanim.connectionString))
                    {
                        conn.Open();
                        using (var tran = conn.BeginTransaction())
                        {
                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if ((dr.RowState & DataRowState.Added) == DataRowState.Added)
                                    {
                                        hasAddedRow = true;
                                        using (var cmd = new SqlCommand(insertIstem, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@malzemeId", clGenelTanim.DBToInt32(dr["MALZEMEISTEM_MALZEMELERID"]));
                                            cmd.Parameters.AddWithValue("@adi", clGenelTanim.DBToString(dr["MALZEME ADI"]));
                                            cmd.Parameters.AddWithValue("@adet", clGenelTanim.DBToInt32(dr["TALEP ADET"]));
                                            cmd.Parameters.AddWithValue("@tarih", istemTarih == null ? (object)DBNull.Value : istemTarih);
                                            cmd.Parameters.AddWithValue("@depoIstemId", clGenelTanim.DBToInt32(dr["MALZEMEISTEM_MALZEMEDEPOISTEMID"]));
                                            cmd.Parameters.AddWithValue("@materyalNo", clGenelTanim.DBToString(dr["MATERYAL NO"]));
                                            cmd.Parameters.AddWithValue("@departmanIstemId", clGenelTanim.DBToInt32(dr["MALZEMEISTEM_MALZEMEDEPARTMANISTEMID"]));
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }

                                if (!hasAddedRow)
                                {
                                    XtraMessageBox.Show("Gönderilecek yeni sipariş satırı bulunamadı !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                var (cikisQuery, cikisParams) = BuildInsertMalzemeCikisKayitlari(malzemeDepoIstemId);
                                if (!string.IsNullOrEmpty(cikisQuery))
                                {
                                    using (var cmd = new SqlCommand(cikisQuery, conn, tran))
                                    {
                                        cmd.Parameters.AddRange(cikisParams);
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                tran.Commit();
                            }
                            catch
                            {
                                tran.Rollback();
                                throw;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                    return;
                }

                layoutControlMalzemeIstem.LayoutKontrolleriniSifirla();
                this.simpleButtonMalzemeIstemGonder.Enabled = false;
                this.simpleButtonMalzemeIstemYeni.Enabled = true;
                this.simpleButtonMalzemeIstemSil.Enabled = false;
                this.gridControlMalzemeIstemListesi.DataSource = null;
                this.gridControlMalzemeIstemleri.DataSource = null;
                //InitForm2();
            }
        }

        private void frmMalzemeTalep_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmGirisEkran frmGirisE = ((frmGirisEkran)Application.OpenForms["frmGirisEkran"]);
            //frmGirisE.pictureEdit1.BringToFront();
        }

        private void gridViewMalzemeIstemleri_DoubleClick(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Kaydı silmek istediğinizden emin misiniz?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var view = sender as GridView;

                view.DeleteRow(view.FocusedRowHandle);

                XtraMessageBox.Show("Kaydınız silinmiştir.");
            }

            if (gridViewMalzemeIstemleri.DataRowCount < 1)
            {
                simpleButtonMalzemeIstemVazgec.Enabled = true;
            }
        }

        private void simpleButtonMalzemeIstemSil_Click(object sender, EventArgs e)
        {
            siparissilBasildi = true;

            if (DialogResult.Yes == XtraMessageBox.Show(textEditMalzemeIstemKayitNo.Text.ToString() + " numaralı siparişinizi silmek üzeresiniz, onaylıyor musunuz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {

                try
                {
                    int silId = clGenelTanim.DBToInt32(textEditMalzemeIstemKayitNo.Text);
                    clSqlTanim.ExecuteNonQuery("DELETE FROM TBL_LST_MALZEMEDEPOISTEM WHERE MALZEMEDEPOISTEM_ID=@id", new[] { new SqlParameter("@id", silId) });
                    clSqlTanim.ExecuteNonQuery("DELETE FROM TBL_LST_MALZEMEISTEM WHERE MALZEMEISTEM_MALZEMEDEPOISTEMID=@id", new[] { new SqlParameter("@id", silId) });
                    XtraMessageBox.Show("Siparişiniz silinmiştir...");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }

                layoutControlMalzemeIstem.LayoutKontrolleriniSifirla();
                this.simpleButtonMalzemeIstemGonder.Enabled = false;
                this.gridControlMalzemeIstemleri.Enabled = false;
                this.gridControlMalzemeIstemListesi.Enabled = false;
                this.simpleButtonMalzemeIstemSil.Enabled = false;
                this.dateEditMalzemeIstemTarih.Enabled = false;
                this.textEditMalzemeIstemAdi.Enabled = false;
                this.simpleButtonMalzemeIstemVazgec.Enabled = false;
                this.simpleButtonMalzemeIstemYeni.Enabled = true;

                this.gridControlMalzemeIstemListesi.DataSource = null;
                this.gridControlMalzemeIstemleri.DataSource = null;
            }


            //InitForm();
            //InitForm2();
        }

        private void simpleButtonMalzemeIstemVazgec_Click(object sender, EventArgs e)
        {
            siparisvazgecBasildi = true;

            if (DialogResult.Yes == XtraMessageBox.Show("Sipariş oluşturmaktan vazgeçmek üzeresiniz, onaylıyor musunuz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {

                try
                {
                    int vazgecId = clGenelTanim.DBToInt32(textEditMalzemeIstemKayitNo.Text);
                    clSqlTanim.ExecuteNonQuery("DELETE FROM TBL_LST_MALZEMEDEPOISTEM WHERE MALZEMEDEPOISTEM_ID=@id", new[] { new SqlParameter("@id", vazgecId) });
                    XtraMessageBox.Show("Siparişinizden vazgeçildi ...");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }

            layoutControlMalzemeIstem.LayoutKontrolleriniSifirla();
            this.simpleButtonMalzemeIstemGonder.Enabled = false;
            this.gridControlMalzemeIstemleri.Enabled = false;
            this.gridControlMalzemeIstemListesi.Enabled = false;
            this.simpleButtonMalzemeIstemSil.Enabled = false;
            this.dateEditMalzemeIstemTarih.Enabled = false;
            this.textEditMalzemeIstemAdi.Enabled = false;
            this.simpleButtonMalzemeIstemVazgec.Enabled = false;
            this.simpleButtonMalzemeIstemYeni.Enabled = true;

            this.gridControlMalzemeIstemListesi.DataSource = null;
            this.gridControlMalzemeIstemleri.DataSource = null;
        }

        private void frmMalzemeSiparisOlustur_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataTable dt = gridControlMalzemeIstemleri.DataSource as DataTable;
            if (dt != null)
            {
                if (siparisgonderBasildi == false && siparissilBasildi == false && siparisvazgecBasildi == false)
                {
                    e.Cancel = true;
                    XtraMessageBox.Show("Devam eden işlemi bitirmeden çıkış yapamazsınız ...");
                }
            }
        }

        private void timerListele_Tick(object sender, EventArgs e)
        {
        }

        private (string sql, SqlParameter[] parameters) BuildInsertMalzemeCikisKayitlari(int malzemeDepoIstemId)
        {
            if (malzemeDepoIstemId <= 0)
                return (string.Empty, null);

            StringBuilder sbI = new StringBuilder(2048);
            sbI.Append("INSERT INTO TBL_LST_MALZEMECIKIS (MALZEMECIKIS_MALZEMELERID, MALZEMECIKIS_ADI, MALZEMECIKIS_ADET, MALZEMECIKIS_DEPARTMANID, MALZEMECIKIS_MALZEMEDEPOISTEM_ID, MALZEMECIKIS_TARIHI, MALZEMECIKIS_SORGUBIRIMFIYAT, MALZEMECIKIS_SORGUTOPLAMFIYAT, MALZEMECIKIS_PARABIRIMI) ");
            sbI.Append("SELECT i.MALZEMEISTEM_MALZEMELERID, i.MALZEMEISTEM_ADI, i.MALZEMEISTEM_ADET, i.MALZEMEISTEM_MALZEMEDEPARTMANISTEMID, i.MALZEMEISTEM_MALZEMEDEPOISTEMID, ");
            sbI.Append("@nowDate, ");
            sbI.Append("ISNULL(g.MALZEMEGIRIS_SORGUBIRIMFIYAT,0), ISNULL(g.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) * ISNULL(i.MALZEMEISTEM_ADET,0), ISNULL(g.MALZEMEGIRIS_PARABIRIMI,2) ");
            sbI.Append("FROM TBL_LST_MALZEMEISTEM i (NOLOCK) ");
            sbI.Append("OUTER APPLY (SELECT TOP 1 e.MALZEMEGIRIS_SORGUBIRIMFIYAT, e.MALZEMEGIRIS_PARABIRIMI ");
            sbI.Append("             FROM TBL_LST_MALZEMEGIRIS e (NOLOCK) ");
            sbI.Append("             WHERE e.MALZEMEGIRIS_MALZEMELERID = i.MALZEMEISTEM_MALZEMELERID ");
            sbI.Append("             ORDER BY e.MALZEMEGIRIS_TARIH DESC) g ");
            sbI.Append("WHERE i.MALZEMEISTEM_MALZEMEDEPOISTEMID = @depoIstemId ");
            sbI.Append("AND NOT EXISTS (");
            sbI.Append("SELECT 1 FROM TBL_LST_MALZEMECIKIS c (NOLOCK) ");
            sbI.Append("WHERE c.MALZEMECIKIS_MALZEMEDEPOISTEM_ID = i.MALZEMEISTEM_MALZEMEDEPOISTEMID ");
            sbI.Append("AND c.MALZEMECIKIS_MALZEMELERID = i.MALZEMEISTEM_MALZEMELERID)");

            var parameters = new[] {
                new SqlParameter("@nowDate",    DateTime.Now),
                new SqlParameter("@depoIstemId", malzemeDepoIstemId)
            };
            return (sbI.ToString(), parameters);
        }

        private void gridViewMalzemeIstemListesi_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int malzeme_StokMiktari = Convert.ToInt32(gridViewMalzemeIstemListesi.GetRowCellDisplayText(e.RowHandle, gridViewMalzemeIstemListesi.Columns[4]));

                if (malzeme_StokMiktari <= 0)
                {
                    e.Appearance.BackColor = Color.Red;
                }
            }
        }
    }
}

