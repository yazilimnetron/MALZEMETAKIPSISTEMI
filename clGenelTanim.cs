using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;

namespace MALZEME_TAKIP_SISTEMI
{
    public class clGenelTanim
    {
        public static SqlDataReader dr;
        public static int KullaniciKodu = -1;
        public static int currentYoneticiMi = -1;
        public static int currentMalzemeKullanıcıDepartmanId = -1;
        public static string currentMalzemeKullanıcıDepartmanAdi = "";
        public static string strHostName = "";
        public static DateTime dateNull = new DateTime(1900, 1, 1);
        public static int currentMalzemeDepoIstemID = -1;


        public static DateTime DBToDate(object obj)
        {
            if (Convert.IsDBNull(obj) == true) return Convert.ToDateTime("1900/01/01");
            if (Convert.ToDateTime(obj).Year < 1900) return Convert.ToDateTime("1900/01/01");
            return Convert.ToDateTime(obj);
        }

        public static double DBToDecimal(object obj)
        {
            if (Convert.IsDBNull(obj) == true) return 0;
            return Convert.ToDouble(obj);
        }

        public static int DBToInt32(object obj)
        {
            if (Convert.IsDBNull(obj) == true) return 0;
            return Convert.ToInt32(obj);
        }

        public static Int64 DBToInt64(object obj)
        {
            if (Convert.IsDBNull(obj) == true) return 0;
            return Convert.ToInt64(obj);
        }

        public static string DBToString(object obj)
        {
            if (Convert.IsDBNull(obj) == true) return "";
            return Convert.ToString(obj);
        }

        public static bool DBToBool(object obj)
        {
            if (Convert.IsDBNull(obj) == true) return false;
            return Convert.ToBoolean(obj);
        }

        public static string tosqlstring(object obj, int len, bool withNull)
        {
            string str;
            if (Convert.IsDBNull(obj) == true)
            {
                if (withNull)
                    return "NULL";
                else
                    str = "";
            }
            else
            {
                str = Convert.ToString(obj);
            }

            str = str.Replace(@"'", @"''");
            if (len < str.Length)
            {
                str = str.Substring(0, len);
            }
            return @"'" + str + @"'";
        }

        public static string TextToSQLWithNULL(string text, int maxlen, bool trim)
        {
            if (string.IsNullOrEmpty(text))
                return "NULL";
            string s = text;

            if (trim)
            {
                s = text.Trim();
                if (string.IsNullOrEmpty(s))
                    return null;
            }

            string s1 = s.Replace("'", "''");
            string result = s1;


            if (maxlen > 0 && s1.Length > maxlen)
            {
                result = s1.Substring(0, maxlen);
            }

            return "'" + result + "'";
        }

        public static void OpenSaveDlg(DevExpress.XtraGrid.GridControl grid, SaveFileDialog dlg, string type)
        {
            dlg.FileName = string.Empty;

            if (type.Equals("pdf"))
            {
                dlg.Filter = "Pdf dosyalari|*." + type;

            }
            else if (type.Equals("xls"))
            {
                dlg.Filter = "Excel dosyalari|*." + type;
            }
            else if (type.Equals("html"))
            {
                dlg.Filter = "Web dosyalari|*." + type;
            }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (type.Equals("pdf"))
                {
                    grid.ExportToPdf(dlg.FileName);
                }
                else if (type.Equals("xls"))
                {
                    grid.ExportToXls(dlg.FileName);
                }
                else if (type.Equals("html"))
                {
                    grid.ExportToHtml(dlg.FileName);
                }
            }
        }

        public static void GridYazdir(DevExpress.XtraGrid.GridControl HangiGrid, string Baslik)
        {
            DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.Landscape = true;
            link.Component = HangiGrid;
            link.Margins.Left = 2;
            link.Margins.Right = 2;
            link.Margins.Top = 5;
            link.Margins.Bottom = 5;
            link.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            string middleColumn = Baslik;
            DevExpress.XtraPrinting.PageHeaderFooter pageheader = link.PageHeaderFooter as DevExpress.XtraPrinting.PageHeaderFooter;
            pageheader.Header.Content.Clear();
            pageheader.Header.Content.AddRange(new string[] { null, middleColumn, null });
            pageheader.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Center;
            pageheader.Header.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 11, System.Drawing.FontStyle.Bold);
            link.CreateDocument();
            link.ShowPreview();
            link.Dispose();
        }

        public static void GridViewExport(DevExpress.XtraGrid.Views.Grid.GridView HangiGrid, string DosyaAdi, Form UstFormHangisi)
        {
            SaveFileDialog dialog = new SaveFileDialog() { Filter = "Excel 2003 (.xls)|*.xls|Excel 2007 (.xlsx)|*.xlsx|Pdf Dosyası (.pdf)|*.pdf|Html Dosyası (.html)|*.html|Mht Dosyası (.mht)|*.mht|Rtf - Word (.rtf)|*.rtf|Csv Dosyası (.csv)|*.csv|Metin Dosyası (.txt)|*.txt", FileName = DosyaAdi };
            DialogResult secim = dialog.ShowDialog();
            if (secim == DialogResult.OK)
            {
                switch (dialog.FilterIndex)
                {
                    case 1:
                        HangiGrid.BestFitColumns();
                        HangiGrid.ExportToXls(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 2:
                        HangiGrid.ExportToXlsx(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 3:
                        HangiGrid.BestFitColumns();
                        HangiGrid.ExportToPdf(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 4:
                        HangiGrid.ExportToHtml(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 5:
                        HangiGrid.ExportToMht(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 6:
                        HangiGrid.ExportToRtf(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 7:
                        HangiGrid.ExportToCsv(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 8:
                        HangiGrid.ExportToText(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                }
            }
            else
            {
                Mesajor("İşlem Eptal Edildi, Tekrar Deneyiniz.");
                dialog.Dispose();
            }
        }
        public static void GridExport(DevExpress.XtraGrid.GridControl HangiGrid, string DosyaAdi, Form UstFormHangisi)
        {
            SaveFileDialog dialog = new SaveFileDialog() { Filter = "Excel 2003 (.xls)|*.xls|Excel 2007 (.xlsx)|*.xlsx|Pdf Dosyası (.pdf)|*.pdf|Html Dosyası (.html)|*.html|Mht Dosyası (.mht)|*.mht|Rtf - Word (.rtf)|*.rtf|Csv Dosyası (.csv)|*.csv|Metin Dosyası (.txt)|*.txt", FileName = DosyaAdi };
            DialogResult secim = dialog.ShowDialog();
            if (secim == DialogResult.OK)
            {
                switch (dialog.FilterIndex)
                {
                    case 1:
                        HangiGrid.ExportToXls(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 2:
                        HangiGrid.ExportToXlsx(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 3:
                        HangiGrid.ExportToPdf(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 4:
                        HangiGrid.ExportToHtml(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 5:
                        HangiGrid.ExportToMht(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 6:
                        HangiGrid.ExportToRtf(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 7:
                        HangiGrid.ExportToCsv(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                    case 8:
                        HangiGrid.ExportToText(dialog.FileName);
                        System.Diagnostics.Process.Start(dialog.FileName);
                        dialog.Dispose();
                        break;
                }
            }
            else
            {
                Mesajor("İşlem Eptal Edildi, Tekrar Deneyiniz.");
                dialog.Dispose();
            }
        }

        public static void Mesajor(string Mesajınız)
        {
            //FrmMesaj frm = new FrmMesaj();
            //frm.label1.Text = Mesajınız;
            //frm.simpleButton1.Visible = false;
            //frm.simpleButton2.Visible = false;
            //frm.timer1.Interval = 1500;
            //frm.timer1.Start();
            //if (Form.ActiveForm.IsMdiContainer == true)
            //{ frm.MdiParent = Form.ActiveForm; }
            //else
            //{ frm.MdiParent = Form.ActiveForm.MdiParent; }
            //frm.Show();
        }

        public static bool WriteFilmImagetoSQL(string Action, int Index, byte[] picbyte)
        {
            try
            {
                SqlConnection conn = new SqlConnection(clSqlTanim.connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_Platform_FilmResim", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.VarChar, 1));
                cmd.Parameters.Add(new SqlParameter("@FilmKodu", SqlDbType.Int, 0));
                cmd.Parameters.Add(new SqlParameter("@ResimDosya", SqlDbType.Image, 2147483647));

                cmd.Parameters["@Action"].Value = Action;
                cmd.Parameters["@FilmKodu"].Value = Index;
                cmd.Parameters["@ResimDosya"].Value = picbyte;
                dr = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool WriteMuzikImagetoSQL(string Action, int Index, byte[] picbyte)
        {
            try
            {
                SqlConnection conn = new SqlConnection(clSqlTanim.connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_Platform_MuzikResim", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.VarChar, 1));
                cmd.Parameters.Add(new SqlParameter("@MuzikKodu", SqlDbType.Int, 0));
                cmd.Parameters.Add(new SqlParameter("@ResimDosya", SqlDbType.Image, 2147483647));

                cmd.Parameters["@Action"].Value = Action;
                cmd.Parameters["@MuzikKodu"].Value = Index;
                cmd.Parameters["@ResimDosya"].Value = picbyte;
                dr = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool WriteMalzemeImagetoSQL(string Action, int Index)
        {
            try
            {
                SqlConnection conn = new SqlConnection(clSqlTanim.connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("up_Malzeme_ResimKaydet", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.VarChar, 1));
                cmd.Parameters.Add(new SqlParameter("@MalzemeId", SqlDbType.Int, 0));
                cmd.Parameters.Add(new SqlParameter("@MalzemeResimDosya", SqlDbType.Image));

                cmd.Parameters["@Action"].Value = Action;
                cmd.Parameters["@MalzemeId"].Value = Index;
                dr = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        //public static Image ShowImage(byte[] oImgByteArray)
        //{
        //    try
        //    {
        //        MemoryStream stream = new MemoryStream();
        //        stream.Write(oImgByteArray, 0, oImgByteArray.Length);
        //        System.Drawing.Image x = System.Drawing.Image.FromStream(stream);
        //        return x;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //}

    }


}


