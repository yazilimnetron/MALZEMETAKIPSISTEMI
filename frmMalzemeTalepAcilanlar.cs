using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MALZEMETAKIPSISTEMI.DevExpressExtentions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class frmMalzemeTalepAcilanlar : FrmBase
    {
        public frmMalzemeTalepAcilanlar()
        {
            InitializeComponent();
            layoutControlMalzemeOtoTalepDetay.LayoutKontrolleriniSifirla();
        }

        private void frmMalzemeOtoTalepDetay_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void LoadList(int? durumFilter)
        {
            var sb = new StringBuilder(512);
            sb.Append("SELECT s.MALZEME_ID, case when isnull(t.MALZEMETALEP_DURUM,0)= 0 then 'T.Gelmedi'  when isnull(t.MALZEMETALEP_DURUM,0)= 1 then 'T.Geldi' else '<<Yok>>' end 'TALEP DURUM', ");
            sb.Append("t.MALZEMETALEP_NO AS 'TALEP NO', (ISNULL(t.MALZEMETALEP_ADET,0)) AS ' MALZEME SİPARİŞ ADET', CONVERT(VARCHAR(10),MALZEMETALEP_TARIHI ,121) AS 'TALEP TARİHİ' , ");
            sb.Append("case when s.MALZEME_TURU=1 then 'NLAG'  when s.MALZEME_TURU=2 then 'UNBW' else '<<Seçiniz>>' end 'MALZEME TURU', convert(nvarchar,s.MALZEME_MATERYAL) AS 'MALZEME MATERYEL NO', ");
            sb.Append("s.MALZEME_ADI AS 'MALZEME ADI',(SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS mg WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC ) AS 'MALZEME BİRİM FİYAT',");
            sb.Append("(SELECT TOP 1 ISNULL(mg.MALZEMEGIRIS_SORGUBIRIMFIYAT,0) FROM TBL_LST_MALZEMEGIRIS mg WHERE s.MALZEME_ID = mg.MALZEMEGIRIS_MALZEMELERID ORDER BY MALZEMEGIRIS_TARIH DESC )*(ISNULL(t.MALZEMETALEP_ADET,0)) AS 'MALZEME TOPLAM FİYAT' ,isnull(g.GirislerToplam,0) as 'MALZEME GİRİŞ ADET', isnull(c.CikislarToplam,0) as 'MALZEME ÇIKIŞ ADET', ");
            sb.Append("(isnull(g.GirislerToplam,0) - isnull(c.CikislarToplam,0)) AS 'MALZEME STOK ADET', ISNULL(s.MALZEME_MAXADET,0) AS 'MALZEME MAX ADET', ISNULL(s.MALZEME_MINADET,0) AS 'MALZEME MİN ADET', ");
            sb.Append("s.MALZEME_GRUBU AS 'MALZEME GRUBU', s.MALZEME_PARCANO AS 'MALZEME PARÇANO', e.MALZEMEKATEGORI_ADI +' '+'(' + e.MALZEMEKATEGORI_KODU +')' AS 'MALZEME SATINALMA KATEGORISI' ");
            sb.Append("FROM TBL_LST_MALZEMELER s ");
            sb.Append("LEFT OUTER JOIN ( Select  MALZEMEGIRIS_MALZEMELERID, Sum(MALZEMEGIRIS_ADET) GirislerToplam FROM TBL_LST_MALZEMEGIRIS GROUP BY  MALZEMEGIRIS_MALZEMELERID  ) g ON g.MALZEMEGIRIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("LEFT OUTER JOIN ( Select  MALZEMECIKIS_MALZEMELERID, Sum(MALZEMECIKIS_ADET) CikislarToplam FROM TBL_LST_MALZEMECIKIS GROUP BY  MALZEMECIKIS_MALZEMELERID  ) c ON c.MALZEMECIKIS_MALZEMELERID = s.MALZEME_ID ");
            sb.Append("JOIN TBL_LST_MALZEMETALEP t on t.MALZEMETALEP_MALZEMELERID=s.MALZEME_ID ");
            sb.Append("JOIN TBL_LST_MALZEMEANAGRUPLAR d on s.MALZEME_ANAGRUBU=d.MALZEMEANAGRUP_ID ");
            sb.Append("LEFT JOIN TBL_LST_MALZEMEKATEGORILER e on s.MALZEME_SATINALMAKATEGORI=e.MALZEMEKATEGORI_ID ");
            sb.Append("WHERE CONVERT(DATE, t.MALZEMETALEP_TARIHI) >= @basTarih AND CONVERT(DATE, t.MALZEMETALEP_TARIHI) <= @bitTarih ");
            if (durumFilter.HasValue)
                sb.Append($"AND isnull(t.MALZEMETALEP_DURUM,0)= {durumFilter.Value} ");
            sb.Append("ORDER BY t.MALZEMETALEP_TARIHI desc");

            DataTable dtMalzemeler = clSqlTanim.RunStoredProc(sb.ToString(), new[] {
                new SqlParameter("@basTarih", SqlDbType.Date) { Value = dateEditBaslangicTarih.DateTime.Date },
                new SqlParameter("@bitTarih", SqlDbType.Date) { Value = dateEditBitisTarih.DateTime.Date }
            });
            gridControlMalzemeTalepleri.DataSource = dtMalzemeler;

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;

            this.gridViewMalzemeTalepleri.Columns["TALEP NO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewMalzemeTalepleri.Columns["TALEP NO"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].Visible = false;
            this.gridViewMalzemeTalepleri.Columns["MALZEME_ID"].OptionsColumn.ShowInCustomizationForm = false;

            this.SetGridFont(gridViewMalzemeTalepleri, new Font("Tahoma", 10, FontStyle.Bold));

            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.Format = new System.Globalization.CultureInfo("de");
            this.gridViewMalzemeTalepleri.Columns["MALZEME BİRİM FİYAT"].DisplayFormat.FormatString = "{0:c2}";

            this.gridViewMalzemeTalepleri.Columns["MALZEME STOK ADET"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridViewMalzemeTalepleri.Columns["MALZEME STOK ADET"].SummaryItem.DisplayFormat = "{0} Adet";

            this.gridViewMalzemeTalepleri.BestFitColumns();
        }

        public void InitForm()  { LoadList(null); }
        public void InitForm1() { LoadList(1); }
        public void InitForm2() { LoadList(0); }
        private void barButtonItemMalzemeOtoTalepDetayListele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (radioGroupDurum.SelectedIndex == 0)
            {
                InitForm();
            }
            if (radioGroupDurum.SelectedIndex == 1)
            {
                InitForm1();
            }
            if (radioGroupDurum.SelectedIndex == 2)
            {
                InitForm2();
            }
        }

        private void barButtonItemMalzemeOtoTalepDetayKapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Çıkış yapmak istediğiniden eminmisiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void barButtonItemMalzemeOtoTalepYazdir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");

            clGenelTanim.GridYazdir(gridControlMalzemeTalepleri, "Malzeme Listesi");
        }

        private void barButtonItemMalzemeOtoTalepAktar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");

            clGenelTanim.GridExport(gridControlMalzemeTalepleri, "Malzeme Listesi", this);
        }

        private void gridViewMalzemeTalepleri_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    int malzeme_TalepMiktari = Convert.ToInt32(gridViewMalzemeTalepleri.GetRowCellDisplayText(e.RowHandle, gridViewMalzemeTalepleri.Columns[2]));

            //    if (malzeme_TalepMiktari > 0)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }
            //}
        }

        private void gridViewMalzemeTalepleri_DoubleClick(object sender, EventArgs e)
        {
        }

        private void malzemeTalepToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void radioGroupDurum_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radioGroupDurum.SelectedIndex)
            {
                case 0:
                    InitForm();
                    break;

                case 1:
                    InitForm1();
                    break;
                case 2:
                    InitForm2();
                    break;
            }
        }

        private void talepNoGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridViewMalzemeTalepleri.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("En az bir kayıt seçmelisiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // İlk seçili satır için dialog aç — kendi UPDATE'ini yapıyor
            var ilkRow = gridViewMalzemeTalepleri.GetDataRow(selectedRows[0]);
            string ilkTalepNo = gridViewMalzemeTalepleri.GetRowCellValue(selectedRows[0], "TALEP NO").ToString();
            frmMalzemeTalepGuncelle u = new frmMalzemeTalepGuncelle();
            u.nTalepNO  = ilkTalepNo;
            u.nMalzemeId = clGenelTanim.DBToInt32(ilkRow["MALZEME_ID"].ToString());
            u.ShowDialog();

            // Kullanıcı iptal ettiyse veya tek satırsa işlem bitti
            if (string.IsNullOrEmpty(u.YeniTalepNo) || selectedRows.Length == 1) return;

            // Kalan seçili satırları aynı yeni talep nosuyla güncelle
            for (int i = 1; i < selectedRows.Length; i++)
            {
                var row = gridViewMalzemeTalepleri.GetDataRow(selectedRows[i]);
                if (row == null) continue;
                clSqlTanim.ExecuteNonQuery(
                    "UPDATE TBL_LST_MALZEMETALEP SET MALZEMETALEP_NO=@yeniNo WHERE MALZEMETALEP_NO=@eskiNo",
                    new[] {
                        new SqlParameter("@yeniNo", u.YeniTalepNo),
                        new SqlParameter("@eskiNo", clGenelTanim.DBToString(row["TALEP NO"]))
                    });
            }

            InitForm();
        }



        private void Sil()
        {
            foreach (int rowHandle in gridViewMalzemeTalepleri.GetSelectedRows())
            {
                var item = gridViewMalzemeTalepleri.GetDataRow(rowHandle);
                if (item == null) continue;
                clSqlTanim.ExecuteNonQuery(
                    "DELETE FROM TBL_LST_MALZEMETALEP WHERE MALZEMETALEP_NO=@talepNo AND MALZEMETALEP_MALZEMELERID=@malzemeId",
                    new[] {
                        new SqlParameter("@talepNo",   clGenelTanim.DBToString(item["TALEP NO"])),
                        new SqlParameter("@malzemeId", clGenelTanim.DBToInt32(item["MALZEME_ID"].ToString()))
                    });
            }
            InitForm();
        }

        private void talepSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridViewMalzemeTalepleri.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show("En az bir kayıt seçmelisiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Sil();

                XtraMessageBox.Show("Kayıt Silindi ...");
            }
        }

        private void Tamamla()
        {
            foreach (int rowHandle in gridViewMalzemeTalepleri.GetSelectedRows())
            {
                var item = gridViewMalzemeTalepleri.GetDataRow(rowHandle);
                if (item == null) continue;
                clSqlTanim.ExecuteNonQuery(
                    "UPDATE TBL_LST_MALZEMETALEP SET MALZEMETALEP_DURUM=1 WHERE MALZEMETALEP_NO=@talepNo AND MALZEMETALEP_MALZEMELERID=@malzemeId",
                    new[] {
                        new SqlParameter("@talepNo",   clGenelTanim.DBToString(item["TALEP NO"])),
                        new SqlParameter("@malzemeId", clGenelTanim.DBToInt32(item["MALZEME_ID"].ToString()))
                    });
            }
            InitForm();
        }

        private void talepTamamlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridViewMalzemeTalepleri.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show("En az bir kayıt seçmelisiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.Yes == XtraMessageBox.Show("Değişiklikler Kaydedilsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Tamamla();

                XtraMessageBox.Show("İşlem Başarılı ...");
            }
        }
    }
}
