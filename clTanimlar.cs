using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{

    /// <summary>
    /// clYetkiDetay in yerine tbl_L_M yi kullanmayan kendi tablosu olan yetkilendirme 
    /// </summary>

    public class KontrolVeriIliskisi
    {
        public object control = null;
        public string fieldName = string.Empty;
        public bool IsRequired = false;
        public string dataType = null;
        public bool PersistToDatabase = true;
    }

    public enum MENU_ELEMANLARI
    {
        NO_MENU = -1,
        ISLEM_BEKLEYEN_HASTALAR = 56,
        ISTATISTIKLER = 320,
        MHRS_RANDEVU_EKLE = 376,
        MHRS_CETVEL = 210

    };

    public struct stID_KOD
    {
        public int ID;
        public string KOD;
    }
    public class IDKODEventArg
    {
        private List<stID_KOD> lstIDKOD = null;
        private int id = -1;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public List<stID_KOD> LstIDKOD
        {
            get { return lstIDKOD; }
        }
        public IDKODEventArg(List<stID_KOD> _lstIDKOD)
        {
            this.lstIDKOD = _lstIDKOD;
        }
        public IDKODEventArg(int _ID)
        {
            this.id = _ID;
        }

    }
    public class PersonelFilterEventArg
    {
        private string filter = string.Empty;

        public string Filter
        {
            get { return filter; }
            set { filter = value; }
        }

        public PersonelFilterEventArg(string _filter)
        {
            this.filter = _filter;
        }

    }
    public class AdresSecimiEventArg
    {
        private int adresKodu = 0;
        private int seviye = 1;
        private int icKapiNo = 0;
        private int adresTipi = 0;
        private string acikAdres = string.Empty;
        private int disKapiNo = 0;
        private string adresText = string.Empty;
        private string adresTipiText = string.Empty;
        private bool yeniAdres = true;
        private bool aktif = true;

        private int ekID = 0;
        private int etfAdresID = 0;


        public AdresSecimiEventArg()
        {

        }

        #region Properties
        public int AdresKodu
        {
            get { return adresKodu; }
            set { adresKodu = value; }
        }

        public int AdresSeviyesi
        {
            get { return seviye; }
            set { seviye = value; }
        }
        public string AdresText
        {
            get { return adresText; }
            set { adresText = value; }
        }

        public int IcKapiNo
        {
            get { return icKapiNo; }
            set { icKapiNo = value; }
        }


        public int DisKapiNo
        {
            get { return disKapiNo; }
            set { disKapiNo = value; }
        }


        public string AcikAdres
        {
            get { return acikAdres; }
            set { acikAdres = value; }
        }
        public int AdresTipi
        {
            get { return adresTipi; }
            set { adresTipi = value; }
        }
        public string AdresTipiText
        {
            get { return adresTipiText; }
            set { adresTipiText = value; }
        }

        public int EkID
        {
            get { return ekID; }
            set { ekID = value; }
        }
        public bool YeniAdres
        {
            get { return yeniAdres; }
            set { yeniAdres = value; }
        }
        public bool Aktif
        {
            get { return aktif; }
            set { aktif = value; }
        }
        public int EtfAdresID
        {
            get { return etfAdresID; }
            set { etfAdresID = value; }
        }

        #endregion

    }
    public class KodSecimiEventArg
    {
        private int id = 0;
        private string kodu = string.Empty;
        private string aciklama = string.Empty;

        public KodSecimiEventArg()
        {

        }

        #region Properties
        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        public string Kodu
        {
            get { return kodu; }
            set { kodu = value; }
        }
        public string Aciklama
        {
            get { return aciklama; }
            set { aciklama = value; }
        }


        #endregion
    }


    public class Turkce
    {
        public static string[] gunler = { "Pazar", "Pazartesi", "Salý", "Çarþamba", "Perþembe", "Cuma", "Cumartesi" };
        public static string[] aylar = { "Ocak", "Þubat", "Mart", "Nisan", "Mayýs", "Haziran", "Temmuz", "Aðustos", "Eylül", "Ekim", "Kasým", "Aralýk" };
    }
    public class clTaniBilgisi
    {
        public string Kodu;
        public int ICDID;
        public string taniAciklama;
    }
}
public class DevCmbBoxItem
{
    private int _id;
    private string _code;
    private string _text;
    private object _obj = null;

    #region properties
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    public string Text
    {
        get { return _text; }
        set { _text = value; }
    }
    public string Code
    {
        get { return _code; }
        set { _code = value; }
    }
    public object Obj
    {
        get { return _obj; }
        set { _obj = value; }
    }


    #endregion

    public DevCmbBoxItem(int id, string text)
    {
        this._id = id;
        this._text = text;
    }
    public DevCmbBoxItem(int id, string text, object obj)
        : this(id, text)
    {
        this._obj = obj;
    }
    public DevCmbBoxItem(int id, string text, string code)
        : this(id, text)
    {
        this._code = code;
    }
    public DevCmbBoxItem(int id, string text, string code, object obj)
        : this(id, text, code)
    {
        this._obj = obj;
    }
    public DevCmbBoxItem(string code, string text)
    {
        this._code = code;
        this._text = text;
    }
    public DevCmbBoxItem(string code, string text, object obj)
        : this(code, text)
    {
        this._obj = obj;
    }

    public override string ToString()
    {
        return _text;
    }
}

namespace MALZEME_TAKIP_SISTEMI.DevExpressExtentions
{
    using DevExpress.XtraEditors;

    public static class Extention
    {
        public static void GridYazdir(this DevExpress.XtraGrid.GridControl HangiGrid, string Baslik)
        {
            DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.Component = HangiGrid;
            link.Margins.Left = 15;
            link.Margins.Right = 15;
            link.Margins.Top = 40;
            link.Margins.Bottom = 15;
            string middleColumn = Baslik;
            DevExpress.XtraPrinting.PageHeaderFooter pageheader = link.PageHeaderFooter as DevExpress.XtraPrinting.PageHeaderFooter;
            pageheader.Header.Content.Clear();
            pageheader.Header.Content.AddRange(new string[] { null, middleColumn, null });
            pageheader.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Center;
            pageheader.Header.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 11, System.Drawing.FontStyle.Bold);
            link.CreateDocument();
            link.ShowPreview(); // yan yazdýrmaz için landspace
            link.Dispose();
        }

        public static DevCmbBoxItem SecilenDeger(this DevExpress.XtraEditors.ComboBoxEdit e)
        {
            DevCmbBoxItem item = new DevCmbBoxItem(-1, "-");
            if (e.SelectedIndex != -1 && e.SelectedItem is DevCmbBoxItem)
            {
                item = (DevCmbBoxItem)e.SelectedItem;
            }

            return item;
        }
        public static void DegerSec(this DevExpress.XtraEditors.ComboBoxEdit e, int id)
        {
            e.SelectedIndex = -1;
            foreach (object o in e.Properties.Items)
            {
                if (o is DevCmbBoxItem)
                {
                    DevCmbBoxItem cmbI = (DevCmbBoxItem)o;
                    if (cmbI.Id == id)
                    {
                        e.SelectedItem = cmbI;
                        break;
                    }
                }
            }

        }

        public static void DegerSec(this DevExpress.XtraEditors.ComboBoxEdit e, string kodu)
        {
            e.SelectedIndex = -1;

            foreach (object o in e.Properties.Items)
            {
                if (o is DevCmbBoxItem)
                {
                    DevCmbBoxItem cmbI = (DevCmbBoxItem)o;
                    if (cmbI.Code.Equals(kodu))
                    {
                        e.SelectedItem = cmbI;
                    }
                }
            }
        }

        public static void PanelKontrolleriniSifirla(this DevExpress.XtraEditors.PanelControl pc)
        {
            foreach (Control item in pc.Controls)
            {
                if (item.GetType() == typeof(DevExpress.XtraEditors.ComboBoxEdit))
                {
                    ((DevExpress.XtraEditors.ComboBoxEdit)item).SelectedIndex = 0;
                }
            }
        }
        public static void LayoutKontrolleriniSifirla(this DevExpress.XtraLayout.LayoutControl lc)
        {
            foreach (Control item in lc.Controls)
            {
                switch (item.GetType().ToString())
                {
                    case "DevExpress.XtraEditors.TextEdit":
                        ((DevExpress.XtraEditors.TextEdit)item).Text = string.Empty;
                        break;
                    case "DevExpress.XtraEditors.MemoEdit":
                        ((DevExpress.XtraEditors.MemoEdit)item).Text = string.Empty;
                        break;
                    case "DevExpress.XtraEditors.ComboBoxEdit":
                        ((DevExpress.XtraEditors.ComboBoxEdit)item).SelectedIndex = 0;
                        break;
                    case "DevExpress.XtraEditors.DateEdit":
                        ((DevExpress.XtraEditors.DateEdit)item).DateTime = DateTime.Now;
                        break;
                    case "DevExpress.XtraEditors.PictureEdit":
                        ((DevExpress.XtraEditors.PictureEdit)item).Image = null;
                        break;
                    case "DevExpress.XtraEditors.CheckEdit":
                        ((DevExpress.XtraEditors.CheckEdit)item).Checked = false;
                        break;
                    case "DevExpress.XtraEditors.CheckedListBoxControl":
                        ((DevExpress.XtraEditors.CheckedListBoxControl)item).ListBoxUnCheck();
                        break;
                    case "DevExpress.XtraEditors.RadioGroup":
                        ((DevExpress.XtraEditors.RadioGroup)item).SelectedIndex = 0;
                        break;
                    case "DevExpress.XtraEditors.LookUpEdit":
                        ((DevExpress.XtraEditors.LookUpEdit)item).SelectionStart = 0;
                        break;
                    case "DevExpress.XtraEditors.SpinEdit":
                        ((DevExpress.XtraEditors.SpinEdit)item).Value = 0;
                        break;
                        /*case "DevExpress.XtraGrid.GridControl":
                            ((DevExpress.XtraGrid.GridControl)item).DataSource = null;
                            break;
                        case "DevExpress.XtraVerticalGrid.VGridControl":
                            ((DevExpress.XtraVerticalGrid.VGridControl)item).DataSource = null;
                            break;*/
                }
            }
        }

        public static void Doldur(this DevExpress.XtraEditors.ComboBoxEdit cmb, DevCmbBoxItem[] items)
        {
            Doldur(cmb, items, false, string.Empty);
        }

        public static void Doldur(this DevExpress.XtraEditors.ComboBoxEdit cmb, DevCmbBoxItem[] items, bool firstEmpty)
        {
            Doldur(cmb, items, firstEmpty, string.Empty);
        }

        public static void Doldur(this DevExpress.XtraEditors.ComboBoxEdit cmb, DevCmbBoxItem[] items, bool firstEmpty, string emptyText)
        {
            cmb.Properties.Items.BeginUpdate();
            cmb.Properties.Items.Clear();

            try
            {
                if (firstEmpty)
                {
                    DevCmbBoxItem itm = new DevCmbBoxItem(-1, string.IsNullOrEmpty(emptyText) ? "<<Seçiniz>>" : emptyText);
                    cmb.Properties.Items.Add(itm);
                }


                foreach (DevCmbBoxItem it in items)
                {
                    cmb.Properties.Items.Add(it);
                }
            }
            finally
            {
                cmb.Properties.Items.EndUpdate();
            }
            cmb.SelectedIndex = -1;
        }
        public static void Bosalt(this DevExpress.XtraEditors.ComboBoxEdit cmb)
        {
            cmb.Properties.Items.BeginUpdate();
            cmb.Properties.Items.Clear();
            cmb.Properties.Items.EndUpdate();
        }

        public static void Doldur(this DevExpress.XtraEditors.ComboBoxEdit cmb, string query, bool firstEmpty)
        {
            Doldur(cmb, query, null, null, null, firstEmpty);
        }
        public static void Doldur(this DevExpress.XtraEditors.ComboBoxEdit cmb, string query, string idField, string textField, bool firstEmpty)
        {
            Doldur(cmb, query, idField, textField, null, firstEmpty);
        }
        public static void Doldur(this DevExpress.XtraEditors.ComboBoxEdit cmb, string query, string idField, string textField, string codeField, bool firstEmpty)
        {

            DataTable dt = clSqlTanim.RunStoredProc(query);
            cmb.Properties.Items.BeginUpdate();
            cmb.Properties.Items.Clear();
            try
            {
                if (firstEmpty)
                {
                    DevCmbBoxItem bosItem = new DevCmbBoxItem(-1, "<<Þeçiniz>>");
                    cmb.Properties.Items.Add(bosItem);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    DevCmbBoxItem item = null;
                    int id = string.IsNullOrEmpty(idField) ? clGenelTanim.DBToInt32(dr[0]) : clGenelTanim.DBToInt32(dr[idField]);
                    //string code = string.IsNullOrEmpty(codeField) ? clGenelTanim.DBToString(dr[2]) : clGenelTanim.DBToString(dr[codeField]);
                    string code = string.IsNullOrEmpty(codeField) ? null : clGenelTanim.DBToString(dr[codeField]);
                    string text = string.IsNullOrEmpty(textField) ? clGenelTanim.DBToString(dr[1]) : clGenelTanim.DBToString(dr[textField]);

                    item = new DevCmbBoxItem(id, text, code, dr);
                    cmb.Properties.Items.Add(item);
                }
            }
            finally
            {
                cmb.Properties.Items.EndUpdate();
            }
            cmb.SelectedIndex = -1;
        }
        /// <summary>
        /// verilen ComboBoxEdit kontrolunu aylar (ID,AY) (1..12,Ocak..Aralýk) ile doldurur. 
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="oncekiAyiSec">true önceki ayi seçer,false içinde bulunulan ayi seçer</param>
        /// <returns>ocak ayinda onceki ay seçilirse yilin da deðiþmesi gerektiðini belirtir</returns>
        public static bool DoldurAylar(this DevExpress.XtraEditors.ComboBoxEdit cmb, bool oncekiAyiSec)
        {
            bool yilAsimi = false;

            DataTable dt = clSqlTanim.RunStoredProc("select ID,AY from dbo.fn_AylarListe()");
            cmb.Properties.Items.BeginUpdate();
            cmb.Properties.Items.Clear();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DevCmbBoxItem item = null;
                    int id = clGenelTanim.DBToInt32(dr["ID"]);
                    string code = clGenelTanim.DBToString(dr["AY"]);
                    string text = clGenelTanim.DBToString(dr["AY"]);

                    item = new DevCmbBoxItem(id, text, code, dr);
                    cmb.Properties.Items.Add(item);
                }


                cmb.SelectedIndex = -1;

                int yil = DateTime.Now.Year;
                int ay = DateTime.Now.Month;

                if (oncekiAyiSec)
                {
                    int oncekiAy = DateTime.Now.Month - 1;
                    if (oncekiAy == 0)
                    {
                        oncekiAy = 12;
                        yil = yil - 1;
                        yilAsimi = true;
                    }
                    ay = oncekiAy;
                }
                cmb.DegerSec(ay);

            }
            finally
            {
                cmb.Properties.Items.EndUpdate();
            }

            return yilAsimi;
        }
        public static void Doldur(this DevExpress.XtraEditors.ComboBoxEdit cmb, DataTable dt, bool firstEmpty)
        {
            Doldur(cmb, dt, null, null, null, firstEmpty);
        }
        public static void Doldur(this DevExpress.XtraEditors.ComboBoxEdit cmb, DataTable dt, string idField, string textField, bool firstEmpty)
        {
            Doldur(cmb, dt, idField, textField, null, firstEmpty);
        }
        public static void Doldur(this DevExpress.XtraEditors.ComboBoxEdit cmb, DataTable dt, string idField, string textField, string codeField, bool firstEmpty)
        {
            cmb.Properties.Items.BeginUpdate();
            cmb.Properties.Items.Clear();
            try
            {
                if (firstEmpty)
                {
                    DevCmbBoxItem bosItem = new DevCmbBoxItem(-1, "<<Seçiniz>>");
                    cmb.Properties.Items.Add(bosItem);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    DevCmbBoxItem item = null;
                    int id = string.IsNullOrEmpty(idField) ? clGenelTanim.DBToInt32(dr[0]) : clGenelTanim.DBToInt32(dr[idField]);
                    string code = string.IsNullOrEmpty(codeField) ? null : clGenelTanim.DBToString(dr[codeField]);
                    string text = string.IsNullOrEmpty(textField) ? clGenelTanim.DBToString(dr[1]) : clGenelTanim.DBToString(dr[textField]);

                    item = new DevCmbBoxItem(id, text, code, dr);
                    cmb.Properties.Items.Add(item);
                }
            }
            finally
            {
                cmb.Properties.Items.EndUpdate();
            }
            cmb.SelectedIndex = -1;
        }

        public static void SecinizText(this DevExpress.XtraEditors.ComboBoxEdit cmb, string caption)
        {
            cmb.Properties.Items.BeginUpdate();
            if (cmb.Properties.Items.Count > 0 && cmb.Properties.Items[0] is DevCmbBoxItem)
            {

                DevCmbBoxItem item = (DevCmbBoxItem)cmb.Properties.Items[0];
                if (item.Id == -1)
                {
                    item.Text = caption;
                }
            }
        }

        #region DateEdit
        public static void DBToDateTime(this DevExpress.XtraEditors.DateEdit de, object dateValue, bool allowNull)
        {
            if (Convert.IsDBNull(dateValue))
            {
                if (allowNull)
                {
                    de.EditValue = null;
                }
                else
                {
                    de.DateTime = Convert.ToDateTime("1900/01/01");
                }
            }
            else
            {
                de.DateTime = Convert.ToDateTime(dateValue);
            }
        }


        #endregion
        #region BarEditItem RepositoryItemComboBox
        public static DevCmbBoxItem SecilenDeger(this DevExpress.XtraBars.BarEditItem e)
        {
            DevCmbBoxItem item = new DevCmbBoxItem(-1, "-");
            if (e.Edit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
            {
                item = (DevCmbBoxItem)e.EditValue;
            }
            return item;
        }
        public static void DegerSec(this DevExpress.XtraBars.BarEditItem e, int id)
        {
            if (e.Edit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
            {
                object val = ((RepositoryItemComboBox)e.Edit).Properties.Items[0];
                foreach (object o in ((RepositoryItemComboBox)e.Edit).Properties.Items)
                {
                    if (o is DevCmbBoxItem)
                    {
                        DevCmbBoxItem cmbI = (DevCmbBoxItem)o;
                        if (cmbI.Id == id)
                        {
                            val = cmbI;
                        }
                    }
                }
                e.EditValue = val;
            }
        }
        /// <summary>
        /// BarEditItem daki Combobox larý doldurmak için kullanýlýr
        /// </summary>
        /// <param name="e">BarEditItem</param>
        /// <param name="query">SQL query</param>
        /// <param name="firstEmpty">true ilk eleman Seciniz</param>
        public static void DoldurCombo(this DevExpress.XtraBars.BarEditItem e, string query, bool firstEmpty)
        {
            DataTable dt = clSqlTanim.RunStoredProc(query);

            if (e.Edit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
            {
                RepositoryItemComboBox rcombo = (RepositoryItemComboBox)e.Edit;
                try
                {
                    rcombo.Properties.Items.BeginUpdate();
                    rcombo.Properties.Items.Clear();
                    if (firstEmpty)
                    {
                        DevCmbBoxItem bosItem = new DevCmbBoxItem(-1, "<<Þeçiniz>>");
                        rcombo.Properties.Items.Add(bosItem);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        DevCmbBoxItem item = null;
                        int id = clGenelTanim.DBToInt32(dr[0]);

                        string text = clGenelTanim.DBToString(dr[1]);

                        item = new DevCmbBoxItem(id, text, dr);
                        rcombo.Properties.Items.Add(item);
                    }
                }
                finally
                {
                    rcombo.Properties.Items.EndUpdate();
                }
                e.EditValue = rcombo.Properties.Items[0];
            }
        }
        /// <summary>
        /// BarEditItem daki Combobox larý doldurmak için kullanýlýr
        /// </summary>
        /// <param name="e">BarEditItem</param>
        /// <param name="DevCmbBoxItem">Disarida olusturulmus item dizisi</DevCmbBoxItem>
        /// <param name="firstEmpty">true ilk eleman Seciniz</param>
        public static void DoldurCombo(this DevExpress.XtraBars.BarEditItem e, DevCmbBoxItem[] items, bool firstEmpty)
        {
            if (e.Edit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
            {
                RepositoryItemComboBox rcombo = (RepositoryItemComboBox)e.Edit;
                try
                {
                    rcombo.Properties.Items.BeginUpdate();
                    rcombo.Properties.Items.Clear();
                    if (firstEmpty)
                    {
                        DevCmbBoxItem bosItem = new DevCmbBoxItem(-1, "<<Þeçiniz>>");
                        rcombo.Properties.Items.Add(bosItem);
                    }
                    foreach (DevCmbBoxItem item in items)
                    {
                        rcombo.Properties.Items.Add(item);
                    }
                }
                finally
                {
                    rcombo.Properties.Items.EndUpdate();
                }
                e.EditValue = rcombo.Properties.Items[0];
            }
        }
        #endregion
        /// <summary>
        /// virgul yada baska bir simge ile ayrilmis sayilara göre eþleþen list box items lari checkler
        /// </summary>
        /// <param name="lst">listbox</param>
        /// <param name="strValue">aranacak deger</param>
        /// <param name="separator">ayirac</param>
        /// <returns>true ;ticklen en az bir kayit varsa</returns>
        public static bool ListBoxCheck(this DevExpress.XtraEditors.CheckedListBoxControl lst, string strValue, string separator)
        {
            bool result = false;

            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in lst.Items)
            {
                item.CheckState = CheckState.Unchecked;
            }

            string[] values = strValue.Split(separator.ToCharArray());
            foreach (string s in values)
            {
                if (!s.Equals(string.Empty))
                {
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in lst.Items)
                    {
                        int id = -1;
                        Int32.TryParse(s, out id);

                        if (item.Value is DevCmbBoxItem && ((DevCmbBoxItem)item.Value).Id == id && id > 0)
                        {
                            item.CheckState = CheckState.Checked;
                            result = true;
                            break;
                        }
                    }
                }
            }

            return result;
        }
        public static void ListBoxUnCheck(this DevExpress.XtraEditors.CheckedListBoxControl lst)
        {

            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in lst.Items)
            {
                item.CheckState = CheckState.Unchecked;
            }
        }
        /// <summary>
        /// X olan karakterler tiklenir
        /// </summary>
        /// <param name="lst">CheckedListBox</param>
        /// <param name="strValue"></param>
        /// <returns>true ;ticklen en az bir kayit varsa</returns>
        public static bool ListBoxCheck(this DevExpress.XtraEditors.CheckedListBoxControl lst, string strValue)
        {
            bool result = false;

            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in lst.Items)
            {
                item.CheckState = CheckState.Unchecked;
            }

            for (int i = 0; i < strValue.Length; i++)
            {

                if (strValue[i].ToString() == "X")
                {
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in lst.Items)
                    {
                        if (item.Value is DevCmbBoxItem)
                        {
                            DevCmbBoxItem ci = (DevCmbBoxItem)item.Value;
                            if (ci.Id == i)
                            {
                                item.CheckState = CheckState.Checked;
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static string ListBoxRead(this DevExpress.XtraEditors.CheckedListBoxControl lst, string separator)
        {
            string strDeger = string.Empty;
            bool bFirst = true;

            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in lst.CheckedItems)
            {
                if (item.Value is DevCmbBoxItem)
                {
                    DevCmbBoxItem ci = (DevCmbBoxItem)item.Value;

                    if (bFirst)
                    {
                        strDeger = ci.Id.ToString();
                        bFirst = false;
                    }
                    else
                    {
                        strDeger += separator + ci.Id.ToString();
                    }
                }
            }

            return strDeger;
        }
        public static void ListBoxFill(this DevExpress.XtraEditors.CheckedListBoxControl lst, string query, bool FirstEmpty)
        {
            lst.ListBoxFill(query, null, null, null, FirstEmpty);
        }
        public static void ListBoxFill(this DevExpress.XtraEditors.CheckedListBoxControl lst, string query, string idField, string codeField, string textField, bool FirstEmpty)
        {
            DataTable dt = clSqlTanim.RunStoredProc(query);
            lst.Items.BeginUpdate();
            lst.Items.Clear();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DevCmbBoxItem item = null;
                    int id = string.IsNullOrEmpty(idField) ? clGenelTanim.DBToInt32(dr[0]) : clGenelTanim.DBToInt32(dr[idField]);
                    string code = string.IsNullOrEmpty(codeField) ? null : clGenelTanim.DBToString(dr[codeField]);
                    string text = string.IsNullOrEmpty(textField) ? clGenelTanim.DBToString(dr[1]) : clGenelTanim.DBToString(dr[textField]);

                    item = new DevCmbBoxItem(id, text, code, dr);
                    lst.Items.Add(item);
                }
            }
            finally
            {
                lst.Items.EndUpdate();
            }
            lst.SelectedIndex = -1;
        }

        public static void TarihZamanGirisliYap(this DevExpress.XtraGrid.Columns.GridColumn gc)
        {
            if (gc == null)
                return;

            DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dte = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();

            dte.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            dte.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;

            dte.Mask.EditMask = "g";
            dte.Mask.UseMaskAsDisplayFormat = true;
            gc.ColumnEdit = dte;

        }
        public static void ZamanGirisliYap(this DevExpress.XtraGrid.Columns.GridColumn gc)
        {
            if (gc == null)
                return;

            DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit dte = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();

            dte.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dte.Mask.EditMask = "t";
            dte.Mask.UseMaskAsDisplayFormat = true;
            gc.ColumnEdit = dte;

        }
        public static void KomboGirisliYap(this DevExpress.XtraGrid.Columns.GridColumn gc, string query, string kodField, string displayField)
        {
            if (gc == null)
                return;
            //string query = "SELECT SNO KODU,Madde ADI from tbl_L_M where listeID=709";
            DataTable dt = clSqlTanim.RunStoredProc(query);

            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();

            lookup.DisplayMember = string.IsNullOrEmpty(displayField) ? "ADI" : displayField;
            lookup.ValueMember = string.IsNullOrEmpty(kodField) ? "KODU" : kodField;

            lookup.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(lookup.DisplayMember));
            lookup.DataSource = dt;

            gc.ColumnEdit = lookup;

        }
        public static void BandliGridViewKolonlariniAyarla(this DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bgv, string baslik,
            string[] kolonIsimleri)
        {
            var gridBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();

            gridBand.Caption = baslik;
            DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] bandedCols = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[kolonIsimleri.Length];
            int i = 0;

            foreach (string s in kolonIsimleri)
            {
                bandedCols[i] = (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn)bgv.Columns.AddField(kolonIsimleri[i]);
                bandedCols[i].OwnerBand = gridBand;
                bandedCols[i].Visible = true;
                i++;
            }

        }
        /// <summary>
        /// verilen columnu verilen kontrolun deðeriyle karþýlaþtýrýr (Devexpress kontrolleri)
        /// </summary>
        /// <param name="row">DataTable.row</param>
        /// <param name="alanAdi">Alan ADI</param>
        /// <param name="o">Kontrol ADI</param>
        /// <returns></returns>
        public static bool VeriDegistimi(this DataRow row, string alanAdi, object o)
        {
            bool result = false;

            if (row == null)
                return true;

            string controlTypeString = o.ToString();

            if (controlTypeString.Equals("DevExpress.XtraEditors.TextEdit"))
            {
                string dbTextValue = row[alanAdi] == DBNull.Value ? string.Empty : clGenelTanim.DBToString(row[alanAdi]);

                return !((TextEdit)o).Text.Equals(dbTextValue);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.MemoEdit"))
            {
                string dbTextValue = row[alanAdi] == DBNull.Value ? string.Empty : clGenelTanim.DBToString(row[alanAdi]);

                return !((MemoEdit)o).Text.Equals(dbTextValue);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.DateEdit"))
            {
                return !((DateEdit)o).DateTime.Equals(clGenelTanim.DBToDate(row[alanAdi]));
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.ComboBoxEdit"))
            {
                int deger = row[alanAdi] == System.DBNull.Value ? -1 : clGenelTanim.DBToInt32(row[alanAdi]);
                return ((ComboBoxEdit)o).SecilenDeger().Id != deger;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckEdit"))
            {
                return ((CheckEdit)o).Checked != (clGenelTanim.DBToInt32(row[alanAdi]) == 1);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckedListBoxControl"))
            {
                return ((CheckedListBoxControl)o).ListBoxRead(",") != (clGenelTanim.DBToString(row[alanAdi]));
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.RadioGroup"))
            {
                return ((RadioGroup)o).SelectedIndex != (clGenelTanim.DBToInt32(row[alanAdi]));
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.SpinEdit"))
            {
                return ((SpinEdit)o).Value != (clGenelTanim.DBToInt32(row[alanAdi]));
            }
            else if (controlTypeString.Equals("DevExpress.XtraBars.BarEditItem"))
            {
                DevExpress.XtraBars.BarEditItem barItem = (DevExpress.XtraBars.BarEditItem)o;

                if (barItem.Edit is RepositoryItemComboBox)
                {
                    int secilenDeger = barItem.SecilenDeger().Id;
                    int orjinalDeger = clGenelTanim.DBToInt32(row[alanAdi]);
                    if (orjinalDeger == 0 && secilenDeger <= 0)
                        return false;

                    return secilenDeger != orjinalDeger;
                }
            }

            return result;
        }

        public static bool VeriGirildimi(this object o)
        {
            bool result = false;

            if (o == null)
                return true;

            string controlTypeString = o.ToString();

            if (controlTypeString.Equals("DevExpress.XtraEditors.TextEdit"))
            {
                return !((TextEdit)o).Text.Trim().Equals(string.Empty);
            }
            if (controlTypeString.Equals("DevExpress.XtraEditors.MemoEdit"))
            {
                return !((MemoEdit)o).Text.Trim().Equals(string.Empty);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.DateEdit"))
            {
                return !((DateEdit)o).DateTime.Equals(clGenelTanim.dateNull);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.ComboBoxEdit"))
            {
                return ((ComboBoxEdit)o).SecilenDeger().Id > 0;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckEdit"))
            {
                return ((CheckEdit)o).CheckState != CheckState.Indeterminate;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckedListBoxControl"))
            {
                return ((CheckedListBoxControl)o).ListBoxRead(",") != string.Empty;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.RadioGroup"))
            {
                return ((RadioGroup)o).SelectedIndex >= 0;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.SpinEdit"))
            {
                return ((SpinEdit)o).Value > -1;
            }
            else if (controlTypeString.Equals("DevExpress.XtraBars.BarEditItem"))
            {
                DevExpress.XtraBars.BarEditItem barItem = (DevExpress.XtraBars.BarEditItem)o;

                if (barItem.Edit is RepositoryItemComboBox)
                {
                    return barItem.SecilenDeger().Id <= 0;
                }
            }
            return result;
        }
        public static void SetErrorProviderText(this DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider ep, object o, string text)
        {
            if (o == null)
                return;

            string controlTypeString = o.ToString();

            if (controlTypeString.Equals("DevExpress.XtraEditors.TextEdit"))
            {
                ep.SetError((TextEdit)o, text);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.DateEdit"))
            {
                ep.SetError((DateEdit)o, text);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.ComboBoxEdit"))
            {
                ep.SetError((ComboBoxEdit)o, text);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckEdit"))
            {
                ep.SetError((CheckEdit)o, text);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckedListBoxControl"))
            {
                ep.SetError((CheckedListBoxControl)o, text);

            }
            else if (controlTypeString.Equals("DevExpress.XtraBars.BarEditItem"))
            {
                //ep.SetError(((DevExpress.XtraBars.BarEditItem)o).Edit, text);
            }
        }
        public static void SetKontrolText(this object o, string text)
        {
            if (o == null)
                return;

            string controlTypeString = o.ToString();

            if (controlTypeString.Equals("DevExpress.XtraEditors.TextEdit"))
            {
                ((TextEdit)o).Text = text;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.DateEdit"))
            {
                ((DateEdit)o).EditValue = text;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.ComboBoxEdit"))
            {
                ((ComboBoxEdit)o).Text = text;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckEdit"))
            {
                ((CheckEdit)o).Checked = text.Equals("true", StringComparison.OrdinalIgnoreCase) || text.Equals("1");
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckedListBoxControl"))
            {
                //ep.SetError((CheckedListBoxControl)o, text);

            }
        }
        public static string GetKontrolName(this object o)
        {
            string name = string.Empty;
            if (o == null)
                return name;

            string controlTypeString = o.ToString();

            if (controlTypeString.Equals("DevExpress.XtraEditors.TextEdit"))
            {
                name = ((TextEdit)o).Name;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.DateEdit"))
            {
                name = ((DateEdit)o).Name;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.ComboBoxEdit"))
            {
                name = ((ComboBoxEdit)o).Name;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckEdit"))
            {
                name = ((CheckEdit)o).Name;
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckedListBoxControl"))
            {
                name = ((CheckedListBoxControl)o).Name;
            }
            else if (controlTypeString.Equals("DevExpress.XtraBars.BarEditItem"))
            {
                name = ((DevExpress.XtraBars.BarEditItem)o).Name;
            }
            return name;
        }
        public static string ToSQLString(this DateTime dateTime, bool showHourMinute)
        {
            string formattedDateTime = string.Empty;
            if (showHourMinute)
            {
                formattedDateTime = dateTime.Equals(clGenelTanim.dateNull) ? "NULL" : string.Format("'{0}'", dateTime.ToString("yyyy-MM-dd HH:mm"));
            }
            else
            {
                formattedDateTime = dateTime.Equals(clGenelTanim.dateNull) ? "NULL" : string.Format("'{0}'", dateTime.ToString("yyyy-MM-dd"));
            }

            return formattedDateTime;
        }
        public static void SetFrozenColumnColor(this DevExpress.XtraGrid.Views.Grid.GridView grv, System.Drawing.Color color)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn gc in grv.Columns)
            {
                if (gc.Fixed == DevExpress.XtraGrid.Columns.FixedStyle.Left)
                {
                    gc.AppearanceCell.BackColor = color;
                }
            }
        }

        public static void KolonlariTamGizle(this DevExpress.XtraGrid.Views.Grid.GridView grv, string[] colList)
        {
            foreach (string s in colList)
            {
                grv.Columns[s].Visible = false;
                grv.Columns[s].OptionsColumn.ShowInCustomizationForm = false;
            }
        }
        public static string KontrolDegeriniAl(this object control)
        {
            string resultText = string.Empty;
            string controlTypeString = control.ToString();

            if (controlTypeString.Equals("DevExpress.XtraEditors.TextEdit"))
            {
                resultText = clGenelTanim.TextToSQLWithNULL(((TextEdit)control).Text, 512, true);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.MemoEdit"))
            {
                resultText = clGenelTanim.TextToSQLWithNULL(((MemoEdit)control).Text, 512, true);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.ComboBoxEdit"))
            {
                resultText = ((ComboBoxEdit)control).SecilenDeger().Id >= 0 ? ((ComboBoxEdit)control).SecilenDeger().Id.ToString() : "NULL";
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.DateEdit"))
            {
                resultText = ((DateEdit)control).DateTime == null || ((DateEdit)control).DateTime == clGenelTanim.dateNull || ((DateEdit)control).DateTime == new DateTime(1, 1, 1) ? "NULL" : "'" + ((DateEdit)control).DateTime.ToString("yyyy-MM-dd HH:mm") + "'";
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckedListBoxControl"))
            {
                resultText = clGenelTanim.TextToSQLWithNULL(((CheckedListBoxControl)control).ListBoxRead(","), 512, true);
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckEdit"))
            {
                resultText = ((CheckEdit)control).Checked ? "1" : "0";
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.RadioGroup"))
            {
                resultText = ((RadioGroup)control).SelectedIndex >= 0 ? ((RadioGroup)control).SelectedIndex.ToString() : "NULL";
            }
            else if (controlTypeString.Equals("DevExpress.XtraEditors.SpinEdit"))
            {
                resultText = ((SpinEdit)control).Value > 0 ? ((SpinEdit)control).Value.ToString() : "0";
            }
            else if (controlTypeString.Equals("DevExpress.XtraBars.BarEditItem"))
            {
                DevExpress.XtraBars.BarEditItem barItem = (DevExpress.XtraBars.BarEditItem)control;

                if (barItem.Edit is RepositoryItemComboBox)
                {
                    resultText = barItem.SecilenDeger().Id <= 0 ? "NULL" : barItem.SecilenDeger().Id.ToString();
                }
            }

            return resultText;
        }
        public static string KontroldenInsertQueryOlustur(this List<KontrolVeriIliskisi> lstKV, string tableName, string[] ekAlanlar, string[] ekAlanDegerler)
        {
            StringBuilder sb = new StringBuilder(512);
            sb.AppendFormat("INSERT INTO {0} (", tableName);

            bool firstColumn = true;
            if (ekAlanlar != null && ekAlanlar.Length > 0)
            {
                foreach (string s in ekAlanlar)
                {
                    if (firstColumn)
                    {
                        sb.AppendFormat(" {0}", s);
                        firstColumn = false;
                    }
                    else
                    {
                        sb.AppendFormat(",{0}", s);
                    }
                }
            }

            foreach (KontrolVeriIliskisi kvi in lstKV)
            {
                if (firstColumn)
                {
                    firstColumn = false;
                    sb.AppendFormat(" {0}", kvi.fieldName);
                }
                else
                {
                    sb.AppendFormat(",{0}", kvi.fieldName);
                }
            }
            sb.Append(") select ");

            firstColumn = true;

            if (ekAlanDegerler != null && ekAlanDegerler.Length > 0)
            {
                foreach (string s in ekAlanDegerler)
                {
                    if (firstColumn)
                    {
                        sb.AppendFormat(" {0}", s);
                        firstColumn = false;
                    }
                    else
                    {
                        sb.AppendFormat(",{0}", s);
                    }
                }
            }

            foreach (KontrolVeriIliskisi kvi in lstKV)
            {
                if (firstColumn)
                {
                    firstColumn = false;
                    sb.Append(kvi.control.KontrolDegeriniAl());
                }
                else
                {
                    sb.AppendFormat(",{0}", kvi.control.KontrolDegeriniAl());
                }
            }

            return sb.ToString();
        }
        public static string KontroldenUpdateQueryOlustur(this List<KontrolVeriIliskisi> lstKV, DataRow dr, string tableName)
        {
            string resultText = string.Empty;


            bool firstColumn = true;
            StringBuilder sb = new StringBuilder(512);
            sb.AppendFormat("Update {0} set ", tableName);

            foreach (KontrolVeriIliskisi kvi in lstKV)
            {
                object control = kvi.control;
                string alanAdi = kvi.fieldName;

                if (dr.VeriDegistimi(alanAdi, control))
                {
                    string controlTypeString = control.ToString();

                    if (controlTypeString.Equals("DevExpress.XtraEditors.TextEdit"))
                    {
                        resultText = string.Format("{0}={1}", alanAdi, clGenelTanim.TextToSQLWithNULL(((TextEdit)control).Text, 512, true));
                    }
                    else if (controlTypeString.Equals("DevExpress.XtraEditors.MemoEdit"))
                    {
                        resultText = string.Format("{0}={1}", alanAdi, clGenelTanim.TextToSQLWithNULL(((MemoEdit)control).Text, 512, true));
                    }
                    else if (controlTypeString.Equals("DevExpress.XtraEditors.ComboBoxEdit"))
                    {
                        string idText = ((ComboBoxEdit)control).SecilenDeger().Id >= 0 ? ((ComboBoxEdit)control).SecilenDeger().Id.ToString() : "NULL";
                        resultText = string.Format("{0}={1}", alanAdi, idText);
                    }
                    else if (controlTypeString.Equals("DevExpress.XtraEditors.DateEdit"))
                    {
                        string dateText = ((DateEdit)control).DateTime == null || ((DateEdit)control).DateTime == clGenelTanim.dateNull || ((DateEdit)control).DateTime == new DateTime(1, 1, 1) ? "NULL" : "'" + ((DateEdit)control).DateTime.ToString("yyyy-MM-dd HH:mm") + "'";
                        resultText = string.Format("{0}={1}", alanAdi, dateText);
                    }
                    else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckedListBoxControl"))
                    {
                        string CheckedListText = clGenelTanim.TextToSQLWithNULL(((CheckedListBoxControl)control).ListBoxRead(","), 512, true);
                        resultText = string.Format("{0}={1}", alanAdi, CheckedListText);
                    }
                    else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckEdit"))
                    {
                        string CheckEditText = ((CheckEdit)control).Checked ? "1" : "0";
                        resultText = string.Format("{0}={1}", alanAdi, CheckEditText);
                    }
                    else if (controlTypeString.Equals("DevExpress.XtraEditors.RadioGroup"))
                    {
                        string RadioGroupText = ((RadioGroup)control).SelectedIndex >= 0 ? ((RadioGroup)control).SelectedIndex.ToString() : "NULL";
                        resultText = string.Format("{0}={1}", alanAdi, RadioGroupText);
                        //var secilen = ((RadioGroup)control).EditValue;
                    }
                    else if (controlTypeString.Equals("DevExpress.XtraEditors.SpinEdit"))
                    {
                        string SpinEditText = ((SpinEdit)control).Value > 0 ? ((SpinEdit)control).Value.ToString() : "0";
                        resultText = string.Format("{0}={1}", alanAdi, SpinEditText);
                    }
                    else if (controlTypeString.Equals("DevExpress.XtraBars.BarEditItem"))
                    {
                        DevExpress.XtraBars.BarEditItem barItem = (DevExpress.XtraBars.BarEditItem)control;

                        if (barItem.Edit is RepositoryItemComboBox)
                        {
                            string text = barItem.SecilenDeger().Id <= 0 ? "NULL" : barItem.SecilenDeger().Id.ToString();
                            resultText = string.Format("{0}={1}", alanAdi, text);
                        }
                    }

                    if (firstColumn)
                    {
                        firstColumn = false;
                    }
                    else
                    {
                        resultText = "," + resultText;
                    }

                    sb.Append(resultText);
                }
            }
            return sb.ToString();
        }

        public static string KontroldenQueryOlustur(DataRow dr, string alanAdi, object control, ref bool firstColumn)
        {
            string resultText = string.Empty;

            if (dr.VeriDegistimi(alanAdi, control))
            {
                string controlTypeString = control.ToString();

                if (controlTypeString.Equals("DevExpress.XtraEditors.TextEdit"))
                {
                    resultText = string.Format("{0}={1}", alanAdi, clGenelTanim.TextToSQLWithNULL(((TextEdit)control).Text, 512, true));
                }
                else if (controlTypeString.Equals("DevExpress.XtraEditors.MemoEdit"))
                {
                    resultText = string.Format("{0}={1}", alanAdi, clGenelTanim.TextToSQLWithNULL(((MemoEdit)control).Text, 512, true));
                }
                else if (controlTypeString.Equals("DevExpress.XtraEditors.ComboBoxEdit"))
                {
                    string idText = ((ComboBoxEdit)control).SecilenDeger().Id >= 0 ? ((ComboBoxEdit)control).SecilenDeger().Id.ToString() : "NULL";
                    resultText = string.Format("{0}={1}", alanAdi, idText);
                }
                else if (controlTypeString.Equals("DevExpress.XtraEditors.DateEdit"))
                {
                    string dateText = ((DateEdit)control).DateTime == null || ((DateEdit)control).DateTime == clGenelTanim.dateNull || ((DateEdit)control).DateTime == new DateTime(1, 1, 1) ? "NULL" : "'" + ((DateEdit)control).DateTime.ToString("yyyy-MM-dd HH:mm") + "'";
                    resultText = string.Format("{0}={1}", alanAdi, dateText);
                }
                else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckedListBoxControl"))
                {
                    string text = clGenelTanim.TextToSQLWithNULL(((CheckedListBoxControl)control).ListBoxRead(","), 512, true);
                    resultText = string.Format("{0}={1}", alanAdi, text);
                }
                else if (controlTypeString.Equals("DevExpress.XtraEditors.CheckEdit"))
                {
                    string text = ((CheckEdit)control).Checked ? "1" : "0";
                    resultText = string.Format("{0}={1}", alanAdi, text);
                }
                else if (controlTypeString.Equals("DevExpress.XtraEditors.RadioGroup"))
                {
                    string text = ((RadioGroup)control).SelectedIndex >= 0 ? ((RadioGroup)control).SelectedIndex.ToString() : "NULL";
                    resultText = string.Format("{0}={1}", alanAdi, text);
                }

                if (firstColumn)
                {
                    firstColumn = false;
                }
                else
                {
                    resultText = "," + resultText;
                }

                return resultText;
            }
            return resultText;
        }
    }
}