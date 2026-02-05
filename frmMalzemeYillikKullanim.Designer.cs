namespace MALZEME_TAKIP_SISTEMI
{
    partial class frmMalzemeYillikKullanim
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMalzemeYillikKullanim));
            this.layoutControlYillikKulanim = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlListe = new DevExpress.XtraGrid.GridControl();
            this.gridViewListe = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButtonYazdir = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonHesapla = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditBitisTarih = new DevExpress.XtraEditors.DateEdit();
            this.dateEditBaslangicTarih = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroupYillikKullanim = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemBaslangicTarih = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBitisTarih = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemHesapla = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemYazdir = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemListe = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlYillikKulanim)).BeginInit();
            this.layoutControlYillikKulanim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewListe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitisTarih.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitisTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBaslangicTarih.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBaslangicTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupYillikKullanim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBaslangicTarih)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBitisTarih)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHesapla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemYazdir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemListe)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlYillikKulanim
            // 
            this.layoutControlYillikKulanim.Controls.Add(this.gridControlListe);
            this.layoutControlYillikKulanim.Controls.Add(this.simpleButtonYazdir);
            this.layoutControlYillikKulanim.Controls.Add(this.simpleButtonHesapla);
            this.layoutControlYillikKulanim.Controls.Add(this.dateEditBitisTarih);
            this.layoutControlYillikKulanim.Controls.Add(this.dateEditBaslangicTarih);
            this.layoutControlYillikKulanim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlYillikKulanim.Location = new System.Drawing.Point(0, 0);
            this.layoutControlYillikKulanim.Name = "layoutControlYillikKulanim";
            this.layoutControlYillikKulanim.Root = this.layoutControlGroupYillikKullanim;
            this.layoutControlYillikKulanim.Size = new System.Drawing.Size(1336, 753);
            this.layoutControlYillikKulanim.TabIndex = 0;
            this.layoutControlYillikKulanim.Text = "layoutControl1";
            // 
            // gridControlListe
            // 
            this.gridControlListe.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControlListe.Location = new System.Drawing.Point(12, 51);
            this.gridControlListe.MainView = this.gridViewListe;
            this.gridControlListe.Name = "gridControlListe";
            this.gridControlListe.Size = new System.Drawing.Size(1312, 690);
            this.gridControlListe.TabIndex = 9;
            this.gridControlListe.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewListe});
            // 
            // gridViewListe
            // 
            this.gridViewListe.GridControl = this.gridControlListe;
            this.gridViewListe.Name = "gridViewListe";
            this.gridViewListe.OptionsBehavior.Editable = false;
            this.gridViewListe.OptionsBehavior.ReadOnly = true;
            this.gridViewListe.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewListe.OptionsView.ColumnAutoWidth = false;
            this.gridViewListe.OptionsView.ShowAutoFilterRow = true;
            this.gridViewListe.OptionsView.ShowFooter = true;
            this.gridViewListe.OptionsView.ShowGroupPanel = false;
            // 
            // simpleButtonYazdir
            // 
            this.simpleButtonYazdir.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.simpleButtonYazdir.Appearance.Options.UseFont = true;
            this.simpleButtonYazdir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonYazdir.ImageOptions.Image")));
            this.simpleButtonYazdir.Location = new System.Drawing.Point(942, 12);
            this.simpleButtonYazdir.Name = "simpleButtonYazdir";
            this.simpleButtonYazdir.Size = new System.Drawing.Size(382, 35);
            this.simpleButtonYazdir.StyleController = this.layoutControlYillikKulanim;
            this.simpleButtonYazdir.TabIndex = 8;
            this.simpleButtonYazdir.Text = "Yazdır";
            this.simpleButtonYazdir.Click += new System.EventHandler(this.simpleButtonYazdir_Click);
            // 
            // simpleButtonHesapla
            // 
            this.simpleButtonHesapla.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.simpleButtonHesapla.Appearance.Options.UseFont = true;
            this.simpleButtonHesapla.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonHesapla.ImageOptions.Image")));
            this.simpleButtonHesapla.Location = new System.Drawing.Point(557, 12);
            this.simpleButtonHesapla.Name = "simpleButtonHesapla";
            this.simpleButtonHesapla.Size = new System.Drawing.Size(381, 35);
            this.simpleButtonHesapla.StyleController = this.layoutControlYillikKulanim;
            this.simpleButtonHesapla.TabIndex = 7;
            this.simpleButtonHesapla.Text = "Hesapla";
            this.simpleButtonHesapla.Click += new System.EventHandler(this.simpleButtonHesapla_Click);
            // 
            // dateEditBitisTarih
            // 
            this.dateEditBitisTarih.EditValue = null;
            this.dateEditBitisTarih.Location = new System.Drawing.Point(418, 12);
            this.dateEditBitisTarih.Name = "dateEditBitisTarih";
            this.dateEditBitisTarih.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBitisTarih.Properties.Appearance.Options.UseFont = true;
            this.dateEditBitisTarih.Properties.AppearanceCalendar.DayCellHighlighted.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBitisTarih.Properties.AppearanceCalendar.DayCellHighlighted.Options.UseFont = true;
            this.dateEditBitisTarih.Properties.AppearanceCalendar.Header.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBitisTarih.Properties.AppearanceCalendar.Header.Options.UseFont = true;
            this.dateEditBitisTarih.Properties.AppearanceCalendar.HeaderHighlighted.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBitisTarih.Properties.AppearanceCalendar.HeaderHighlighted.Options.UseFont = true;
            this.dateEditBitisTarih.Properties.AppearanceCalendar.WeekNumber.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBitisTarih.Properties.AppearanceCalendar.WeekNumber.Options.UseFont = true;
            this.dateEditBitisTarih.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBitisTarih.Properties.AppearanceDisabled.Options.UseFont = true;
            this.dateEditBitisTarih.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBitisTarih.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dateEditBitisTarih.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBitisTarih.Properties.AppearanceFocused.Options.UseFont = true;
            this.dateEditBitisTarih.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBitisTarih.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.dateEditBitisTarih.Properties.AutoHeight = false;
            this.dateEditBitisTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBitisTarih.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBitisTarih.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEditBitisTarih.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditBitisTarih.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateEditBitisTarih.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditBitisTarih.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEditBitisTarih.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditBitisTarih.Size = new System.Drawing.Size(135, 35);
            this.dateEditBitisTarih.StyleController = this.layoutControlYillikKulanim;
            this.dateEditBitisTarih.TabIndex = 5;
            // 
            // dateEditBaslangicTarih
            // 
            this.dateEditBaslangicTarih.EditValue = null;
            this.dateEditBaslangicTarih.Location = new System.Drawing.Point(166, 12);
            this.dateEditBaslangicTarih.Name = "dateEditBaslangicTarih";
            this.dateEditBaslangicTarih.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBaslangicTarih.Properties.Appearance.Options.UseFont = true;
            this.dateEditBaslangicTarih.Properties.AppearanceCalendar.DayCellHighlighted.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBaslangicTarih.Properties.AppearanceCalendar.DayCellHighlighted.Options.UseFont = true;
            this.dateEditBaslangicTarih.Properties.AppearanceCalendar.Header.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBaslangicTarih.Properties.AppearanceCalendar.Header.Options.UseFont = true;
            this.dateEditBaslangicTarih.Properties.AppearanceCalendar.HeaderHighlighted.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBaslangicTarih.Properties.AppearanceCalendar.HeaderHighlighted.Options.UseFont = true;
            this.dateEditBaslangicTarih.Properties.AppearanceCalendar.WeekNumber.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBaslangicTarih.Properties.AppearanceCalendar.WeekNumber.Options.UseFont = true;
            this.dateEditBaslangicTarih.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBaslangicTarih.Properties.AppearanceDisabled.Options.UseFont = true;
            this.dateEditBaslangicTarih.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBaslangicTarih.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dateEditBaslangicTarih.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBaslangicTarih.Properties.AppearanceFocused.Options.UseFont = true;
            this.dateEditBaslangicTarih.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBaslangicTarih.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.dateEditBaslangicTarih.Properties.AutoHeight = false;
            this.dateEditBaslangicTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBaslangicTarih.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBaslangicTarih.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEditBaslangicTarih.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditBaslangicTarih.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEditBaslangicTarih.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditBaslangicTarih.Size = new System.Drawing.Size(136, 35);
            this.dateEditBaslangicTarih.StyleController = this.layoutControlYillikKulanim;
            this.dateEditBaslangicTarih.TabIndex = 4;
            // 
            // layoutControlGroupYillikKullanim
            // 
            this.layoutControlGroupYillikKullanim.CustomizationFormText = "layoutControlGroupYillikKullanim";
            this.layoutControlGroupYillikKullanim.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupYillikKullanim.GroupBordersVisible = false;
            this.layoutControlGroupYillikKullanim.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemBaslangicTarih,
            this.layoutControlItemBitisTarih,
            this.layoutControlItemHesapla,
            this.layoutControlItemYazdir,
            this.layoutControlItemListe});
            this.layoutControlGroupYillikKullanim.Name = "layoutControlGroupYillikKullanim";
            this.layoutControlGroupYillikKullanim.Size = new System.Drawing.Size(1336, 753);
            this.layoutControlGroupYillikKullanim.TextVisible = false;
            // 
            // layoutControlItemBaslangicTarih
            // 
            this.layoutControlItemBaslangicTarih.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemBaslangicTarih.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemBaslangicTarih.Control = this.dateEditBaslangicTarih;
            this.layoutControlItemBaslangicTarih.CustomizationFormText = " Başlangıç Tarih  :";
            this.layoutControlItemBaslangicTarih.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemBaslangicTarih.MaxSize = new System.Drawing.Size(0, 39);
            this.layoutControlItemBaslangicTarih.MinSize = new System.Drawing.Size(166, 39);
            this.layoutControlItemBaslangicTarih.Name = "layoutControlItemBaslangicTarih";
            this.layoutControlItemBaslangicTarih.Size = new System.Drawing.Size(294, 39);
            this.layoutControlItemBaslangicTarih.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBaslangicTarih.Text = " Başlangıç Tarih  :";
            this.layoutControlItemBaslangicTarih.TextSize = new System.Drawing.Size(151, 21);
            // 
            // layoutControlItemBitisTarih
            // 
            this.layoutControlItemBitisTarih.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemBitisTarih.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemBitisTarih.Control = this.dateEditBitisTarih;
            this.layoutControlItemBitisTarih.CustomizationFormText = " Bitiş Tarih  :";
            this.layoutControlItemBitisTarih.Location = new System.Drawing.Point(294, 0);
            this.layoutControlItemBitisTarih.MaxSize = new System.Drawing.Size(0, 39);
            this.layoutControlItemBitisTarih.MinSize = new System.Drawing.Size(166, 39);
            this.layoutControlItemBitisTarih.Name = "layoutControlItemBitisTarih";
            this.layoutControlItemBitisTarih.Size = new System.Drawing.Size(251, 39);
            this.layoutControlItemBitisTarih.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBitisTarih.Text = " Bitiş Tarih  :";
            this.layoutControlItemBitisTarih.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItemBitisTarih.TextSize = new System.Drawing.Size(107, 21);
            this.layoutControlItemBitisTarih.TextToControlDistance = 5;
            // 
            // layoutControlItemHesapla
            // 
            this.layoutControlItemHesapla.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemHesapla.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemHesapla.Control = this.simpleButtonHesapla;
            this.layoutControlItemHesapla.CustomizationFormText = "layoutControlItemHesapla";
            this.layoutControlItemHesapla.Location = new System.Drawing.Point(545, 0);
            this.layoutControlItemHesapla.MaxSize = new System.Drawing.Size(0, 39);
            this.layoutControlItemHesapla.MinSize = new System.Drawing.Size(58, 39);
            this.layoutControlItemHesapla.Name = "layoutControlItemHesapla";
            this.layoutControlItemHesapla.Size = new System.Drawing.Size(385, 39);
            this.layoutControlItemHesapla.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemHesapla.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItemHesapla.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemHesapla.TextToControlDistance = 0;
            this.layoutControlItemHesapla.TextVisible = false;
            // 
            // layoutControlItemYazdir
            // 
            this.layoutControlItemYazdir.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemYazdir.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemYazdir.Control = this.simpleButtonYazdir;
            this.layoutControlItemYazdir.CustomizationFormText = "layoutControlItemYazdir";
            this.layoutControlItemYazdir.Location = new System.Drawing.Point(930, 0);
            this.layoutControlItemYazdir.MaxSize = new System.Drawing.Size(0, 39);
            this.layoutControlItemYazdir.MinSize = new System.Drawing.Size(176, 39);
            this.layoutControlItemYazdir.Name = "layoutControlItemYazdir";
            this.layoutControlItemYazdir.Size = new System.Drawing.Size(386, 39);
            this.layoutControlItemYazdir.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemYazdir.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemYazdir.TextVisible = false;
            // 
            // layoutControlItemListe
            // 
            this.layoutControlItemListe.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemListe.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemListe.Control = this.gridControlListe;
            this.layoutControlItemListe.CustomizationFormText = "layoutControlItemListe";
            this.layoutControlItemListe.Location = new System.Drawing.Point(0, 39);
            this.layoutControlItemListe.MinSize = new System.Drawing.Size(216, 24);
            this.layoutControlItemListe.Name = "layoutControlItemListe";
            this.layoutControlItemListe.Size = new System.Drawing.Size(1316, 694);
            this.layoutControlItemListe.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemListe.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemListe.TextVisible = false;
            // 
            // frmMalzemeYillikKullanim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 753);
            this.Controls.Add(this.layoutControlYillikKulanim);
            this.Name = "frmMalzemeYillikKullanim";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Malzeme Yıllık Kullanım";
            this.Load += new System.EventHandler(this.frmMalzemeYillikKullanim_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlYillikKulanim)).EndInit();
            this.layoutControlYillikKulanim.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewListe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitisTarih.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitisTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBaslangicTarih.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBaslangicTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupYillikKullanim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBaslangicTarih)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBitisTarih)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHesapla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemYazdir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemListe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlYillikKulanim;
        private DevExpress.XtraGrid.GridControl gridControlListe;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewListe;
        private DevExpress.XtraEditors.SimpleButton simpleButtonYazdir;
        private DevExpress.XtraEditors.SimpleButton simpleButtonHesapla;
        private DevExpress.XtraEditors.DateEdit dateEditBitisTarih;
        private DevExpress.XtraEditors.DateEdit dateEditBaslangicTarih;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupYillikKullanim;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBaslangicTarih;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBitisTarih;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemHesapla;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemYazdir;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemListe;
    }
}