using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmExcelYukle : DevExpress.XtraEditors.XtraForm
    {
        Excel.Application ExcelUygulama;
        Excel.Workbook ExcelProje;
        Excel.Worksheet ExcelSayfa;
        Excel.Range ExcelRange;

        int rowCnt = 0;
        int columnCnt = 0;
        int nullCnt = 0;
        public frmExcelYukle()
        {
            InitializeComponent();
        }

        void SetGridFont(GridView view, Font font)
        {
            foreach (AppearanceObject ap in view.Appearance)

                ap.Font = font;
        }

        private void simpleButtonExcelOku_Click(object sender, EventArgs e)
        {
            openFileDialogExcel.ShowDialog();
            textEditExcelOku.Text = openFileDialogExcel.FileName;
            string dosya_yolu = openFileDialogExcel.FileName;

            try
            {
                ExcelUygulama = new Excel.Application();
                ExcelProje = ExcelUygulama.Workbooks.Open(dosya_yolu);
                ExcelSayfa = (Excel.Worksheet)ExcelProje.Worksheets.get_Item(1);
                ExcelRange = ExcelSayfa.UsedRange;
                ExcelSayfa = (Excel.Worksheet)ExcelUygulama.ActiveSheet;

                ExcelUygulama.Visible = false;
                ExcelUygulama.AlertBeforeOverwriting = false;

                rowCnt = ExcelRange.Rows.Count - 1;
                columnCnt = ExcelRange.Columns.Count;

                /* for (int i = 1; i < rowCnt; i++)
                {
                    for (int a = 1; a < columnCnt; a++)
                    {
                        object hucre = ExcelSayfa.Cells[i, a];
                        Excel.Range bolge = ExcelSayfa.get_Range(hucre, hucre);

                        if (bolge.Value2 == null) //ALP if (string.IsNullOrEmpty(((Microsoft.Office.Interop.Excel.Range)ExcelSayfa.Cells[1, 1]).Text.ToString()))                  
                        {
                            nullCnt = nullCnt + 1;
                        }
                    }
                }*/

                //if (nullCnt > 0) XtraMessageBox.Show(nullCnt + " Adet Excel' de Boş Hücre Var.Lütfen Kontrol Ediniz !!!");
                //ALP if (nullCnt == 0)
                //ALP {
                var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; data source={0};Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'", dosya_yolu);
                /*string SpreadSheetName = "";
                DataTable ExcelSheets = connectionString.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                SpreadSheetName = ExcelSheets.Rows[0]["TABLE_NAME"].ToString();*/
                var adapter = new OleDbDataAdapter("select * from [Malzeme Listesi$]", connectionString);
                var ds = new DataSet();
                string tableName = "excelData";
                adapter.Fill(ds, tableName);
                DataTable data = ds.Tables[tableName];
                gridControlExcel.DataSource = data;
                ExcelBilgileriniListele();
                //ALP }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void ExcelBilgileriniListele()
        {
            this.gridViewExcel.Columns["Sıra No"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridViewExcel.Columns["Sıra No"].SummaryItem.DisplayFormat = "{0} Kayıt";

            this.SetGridFont(gridViewExcel, new Font("Tahoma", 10, FontStyle.Bold));
            this.gridViewExcel.OptionsView.ShowGroupPanel = false;
            this.gridViewExcel.OptionsView.ColumnAutoWidth = false;
            this.gridViewExcel.BestFitColumns();
        }

        private void simpleButtonExcelYaz_Click(object sender, EventArgs e)
        {
            if (gridViewExcel.RowCount > 0)
            {
                if (gridViewExcel.GetSelectedRows().Count() > 0)
                {
                    for (int i = 0; i < gridViewExcel.RowCount; i++)
                    {
                        StringBuilder sbI = new StringBuilder();

                        Int32 MALZEME_SIRANO = clGenelTanim.DBToInt32(string.IsNullOrEmpty(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[0]).ToString()) ? "0" : gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[0]).ToString());
                        //String MALZEME_OUDBTNO = clGenelTanim.DBToString(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[1]).ToString());
                        String MALZEME_TURU = clGenelTanim.DBToString(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[2]).ToString());
                        Int32 MALZEME_MATERYAL = clGenelTanim.DBToInt32(string.IsNullOrEmpty(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[3]).ToString()) ? "0" : gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[3]).ToString()); //gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[3]).ToString());
                        String MALZEME_PARCANO = clGenelTanim.DBToString(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[4]).ToString());
                        String MALZEME_ADI = clGenelTanim.DBToString(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[5]).ToString());
                        Int32 MALZEME_STOKMIKTARI = clGenelTanim.DBToInt32(string.IsNullOrEmpty(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[6]).ToString()) ? "0" : gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[6]).ToString());
                        Int32 MALZEME_MINADET = clGenelTanim.DBToInt32(string.IsNullOrEmpty(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[7]).ToString()) ? "0" : gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[7]).ToString());
                        Int32 MALZEME_MAXADET = clGenelTanim.DBToInt32(string.IsNullOrEmpty(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[8]).ToString()) ? "0" : gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[8]).ToString());
                        String MALZEME_RAFNO = clGenelTanim.DBToString(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[9]).ToString());
                        String MALZEME_GRUBU = clGenelTanim.DBToString(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[10]).ToString());

                        sbI.Append("insert into TBL_LST_MALZEMELER ( MALZEME_SIRANO, MALZEME_TURU, MALZEME_MATERYAL, MALZEME_PARCANO, MALZEME_ADI, MALZEME_STOKMIKTARI, MALZEME_MINADET, MALZEME_MAXADET,  MALZEME_RAFNO, MALZEME_GRUBU ) select");
                        sbI.AppendFormat("  {0}", clGenelTanim.DBToInt32(MALZEME_SIRANO.ToString()));
                        //sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(MALZEME_OUDBTNO.ToString(), 50, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(MALZEME_TURU.ToString(), 10, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(MALZEME_MATERYAL.ToString()));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(MALZEME_PARCANO.ToString(), 10, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(MALZEME_ADI.ToString(), 500, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(MALZEME_STOKMIKTARI.ToString()));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(MALZEME_MINADET.ToString()));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(MALZEME_MAXADET.ToString()));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(MALZEME_RAFNO.ToString(), 50, true));
                        sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(MALZEME_GRUBU.ToString(), 50, true));


                        //Int32 MALZEME_SIRANO = clGenelTanim.DBToInt32(string.IsNullOrEmpty(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[0]).ToString()) ? "0" : gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[0]).ToString());
                        //String MALZEMEGIRIS_ADI = clGenelTanim.DBToString(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[1]).ToString());
                        //Int32 MALZEMEGIRIS_ADET = clGenelTanim.DBToInt32(string.IsNullOrEmpty(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[2]).ToString()) ? "0" : gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[2]).ToString());
                        //Decimal MALZEME_BIRIMFIYAT = Convert.ToDecimal(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[3]).ToString());
                        //Decimal MALZEME_TOPLAMFIYAT = Convert.ToDecimal(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[4]).ToString());

                        //sbI.Append("insert into TBL_LST_MALZEMEGIRIS (MALZEMEGIRIS_SIRANO, MALZEMEGIRIS_ADI, MALZEMEGIRIS_ADET, MALZEMEGIRIS_BIRIMFIYAT, MALZEMEGIRIS_TOPLAMFIYAT) select");
                        //sbI.AppendFormat("  {0}", clGenelTanim.DBToInt32(MALZEME_SIRANO.ToString()));
                        //sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(MALZEMEGIRIS_ADI.ToString(), 500, true));
                        //sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(MALZEMEGIRIS_ADET.ToString()));
                        //sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(MALZEME_BIRIMFIYAT.ToString().Replace(',', '.'), 10, true));
                        //sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(MALZEME_TOPLAMFIYAT.ToString().Replace(',', '.'), 10, true));


                        //Int32 MATERYEL_NO = clGenelTanim.DBToInt32(string.IsNullOrEmpty(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[0]).ToString()) ? "0" : gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[0]).ToString());
                        //String MATERYEL_ADI = clGenelTanim.DBToString(gridViewExcel.GetRowCellValue(i, gridViewExcel.Columns[1]).ToString());
                        //Int32 MATERYEL_DURUM = 1;

                        //sbI.Append("insert into TBL_LST_DISPLAYMATERYELLER (MATERYEL_NO, MATERYEL_ADI, MATERYEL_DURUM) select");
                        //sbI.AppendFormat("  {0}", clGenelTanim.DBToInt32(MATERYEL_NO.ToString()));
                        //sbI.AppendFormat(" ,{0}", clGenelTanim.tosqlstring(MATERYEL_ADI.ToString(), 500, true));
                        //sbI.AppendFormat(" ,{0}", clGenelTanim.DBToInt32(MATERYEL_DURUM.ToString()));

                        clSqlTanim.RunStoredProc(sbI.ToString());
                    }
                }
            }
        }

        private void frmExcelYukle_Load(object sender, EventArgs e)
        {
            /*foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName == "EXCEL")
                {
                    p.Kill();
                }
            }*/
        }

    }
}