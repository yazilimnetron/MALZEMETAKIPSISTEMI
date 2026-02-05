namespace MALZEME_TAKIP_SISTEMI
{
    partial class frmMalzemeSiparisIstatistikleri
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMalzemeSiparisIstatistikleri));
            this.barManagerMalzemeSiparisIstatistikleri = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barStaticItemSiparisİstatistik = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItemListele = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemYazdir = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemExcel = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPdf = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemKapat = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSubItemYazdir = new DevExpress.XtraBars.BarButtonItem();
            this.layoutControlRaporlama = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlSiparisIstatistik = new DevExpress.XtraGrid.GridControl();
            this.gridViewSiparisIstatistik = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dateEditBitTarih = new DevExpress.XtraEditors.DateEdit();
            this.dateEditBasTarih = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemBasTarih = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBitTarih = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.saveFileDialogMalzemeRapor = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMalzemeSiparisIstatistikleri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRaporlama)).BeginInit();
            this.layoutControlRaporlama.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSiparisIstatistik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSiparisIstatistik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBasTarih)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBitTarih)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManagerMalzemeSiparisIstatistikleri
            // 
            this.barManagerMalzemeSiparisIstatistikleri.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
            this.barManagerMalzemeSiparisIstatistikleri.DockControls.Add(this.barDockControlTop);
            this.barManagerMalzemeSiparisIstatistikleri.DockControls.Add(this.barDockControlBottom);
            this.barManagerMalzemeSiparisIstatistikleri.DockControls.Add(this.barDockControlLeft);
            this.barManagerMalzemeSiparisIstatistikleri.DockControls.Add(this.barDockControlRight);
            this.barManagerMalzemeSiparisIstatistikleri.Form = this;
            this.barManagerMalzemeSiparisIstatistikleri.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItemSiparisİstatistik,
            this.barButtonItemListele,
            this.barSubItemYazdir,
            this.barButtonItemKapat,
            this.barSubItem1,
            this.barButtonItemYazdir,
            this.barButtonItemExcel,
            this.barButtonItemPdf});
            this.barManagerMalzemeSiparisIstatistikleri.MainMenu = this.bar2;
            this.barManagerMalzemeSiparisIstatistikleri.MaxItemId = 8;
            this.barManagerMalzemeSiparisIstatistikleri.StatusBar = this.bar3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(367, 179);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemSiparisİstatistik),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemListele, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemKapat, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barStaticItemSiparisİstatistik
            // 
            this.barStaticItemSiparisİstatistik.Caption = "Sipariş İstatistikleri";
            this.barStaticItemSiparisİstatistik.Id = 0;
            this.barStaticItemSiparisİstatistik.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barStaticItemSiparisİstatistik.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItemSiparisİstatistik.Name = "barStaticItemSiparisİstatistik";
            // 
            // barButtonItemListele
            // 
            this.barButtonItemListele.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemListele.Caption = "Listele";
            this.barButtonItemListele.Id = 1;
            this.barButtonItemListele.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemListele.ImageOptions.Image")));
            this.barButtonItemListele.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barButtonItemListele.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemListele.Name = "barButtonItemListele";
            this.barButtonItemListele.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemListele_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barSubItem1.Caption = "Yazdır";
            this.barSubItem1.Id = 4;
            this.barSubItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barSubItem1.ImageOptions.Image")));
            this.barSubItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barSubItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemYazdir),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemExcel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemPdf)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItemYazdir
            // 
            this.barButtonItemYazdir.Caption = "Yazdır";
            this.barButtonItemYazdir.Id = 5;
            this.barButtonItemYazdir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemYazdir.ImageOptions.Image")));
            this.barButtonItemYazdir.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemYazdir.ImageOptions.LargeImage")));
            this.barButtonItemYazdir.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barButtonItemYazdir.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemYazdir.Name = "barButtonItemYazdir";
            this.barButtonItemYazdir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemYazdir1_ItemClick);
            // 
            // barButtonItemExcel
            // 
            this.barButtonItemExcel.Caption = "Excel";
            this.barButtonItemExcel.Id = 6;
            this.barButtonItemExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemExcel.ImageOptions.Image")));
            this.barButtonItemExcel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemExcel.ImageOptions.LargeImage")));
            this.barButtonItemExcel.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barButtonItemExcel.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemExcel.Name = "barButtonItemExcel";
            this.barButtonItemExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemExcel_ItemClick);
            // 
            // barButtonItemPdf
            // 
            this.barButtonItemPdf.Caption = "Pdf";
            this.barButtonItemPdf.Id = 7;
            this.barButtonItemPdf.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemPdf.ImageOptions.Image")));
            this.barButtonItemPdf.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemPdf.ImageOptions.LargeImage")));
            this.barButtonItemPdf.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barButtonItemPdf.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemPdf.Name = "barButtonItemPdf";
            // 
            // barButtonItemKapat
            // 
            this.barButtonItemKapat.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemKapat.Caption = "Kapat";
            this.barButtonItemKapat.Id = 3;
            this.barButtonItemKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemKapat.ImageOptions.Image")));
            this.barButtonItemKapat.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barButtonItemKapat.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemKapat.Name = "barButtonItemKapat";
            this.barButtonItemKapat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemKapat_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManagerMalzemeSiparisIstatistikleri;
            this.barDockControlTop.Size = new System.Drawing.Size(1227, 50);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 533);
            this.barDockControlBottom.Manager = this.barManagerMalzemeSiparisIstatistikleri;
            this.barDockControlBottom.Size = new System.Drawing.Size(1227, 20);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 50);
            this.barDockControlLeft.Manager = this.barManagerMalzemeSiparisIstatistikleri;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 483);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1227, 50);
            this.barDockControlRight.Manager = this.barManagerMalzemeSiparisIstatistikleri;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 483);
            // 
            // barSubItemYazdir
            // 
            this.barSubItemYazdir.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barSubItemYazdir.Caption = "Yazdır";
            this.barSubItemYazdir.Id = 2;
            this.barSubItemYazdir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barSubItemYazdir.ImageOptions.Image")));
            this.barSubItemYazdir.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barSubItemYazdir.ImageOptions.LargeImage")));
            this.barSubItemYazdir.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barSubItemYazdir.ItemAppearance.Normal.Options.UseFont = true;
            this.barSubItemYazdir.Name = "barSubItemYazdir";
            // 
            // layoutControlRaporlama
            // 
            this.layoutControlRaporlama.Controls.Add(this.gridControlSiparisIstatistik);
            this.layoutControlRaporlama.Controls.Add(this.dateEditBitTarih);
            this.layoutControlRaporlama.Controls.Add(this.dateEditBasTarih);
            this.layoutControlRaporlama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlRaporlama.Location = new System.Drawing.Point(0, 50);
            this.layoutControlRaporlama.Name = "layoutControlRaporlama";
            this.layoutControlRaporlama.Root = this.Root;
            this.layoutControlRaporlama.Size = new System.Drawing.Size(1227, 483);
            this.layoutControlRaporlama.TabIndex = 4;
            this.layoutControlRaporlama.Text = "layoutControl1";
            // 
            // gridControlSiparisIstatistik
            // 
            this.gridControlSiparisIstatistik.Location = new System.Drawing.Point(12, 52);
            this.gridControlSiparisIstatistik.MainView = this.gridViewSiparisIstatistik;
            this.gridControlSiparisIstatistik.MenuManager = this.barManagerMalzemeSiparisIstatistikleri;
            this.gridControlSiparisIstatistik.Name = "gridControlSiparisIstatistik";
            this.gridControlSiparisIstatistik.Size = new System.Drawing.Size(1203, 419);
            this.gridControlSiparisIstatistik.TabIndex = 6;
            this.gridControlSiparisIstatistik.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSiparisIstatistik});
            // 
            // gridViewSiparisIstatistik
            // 
            this.gridViewSiparisIstatistik.GridControl = this.gridControlSiparisIstatistik;
            this.gridViewSiparisIstatistik.Name = "gridViewSiparisIstatistik";
            this.gridViewSiparisIstatistik.OptionsBehavior.Editable = false;
            this.gridViewSiparisIstatistik.OptionsBehavior.ReadOnly = true;
            this.gridViewSiparisIstatistik.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewSiparisIstatistik.OptionsView.ColumnAutoWidth = false;
            this.gridViewSiparisIstatistik.OptionsView.ShowAutoFilterRow = true;
            this.gridViewSiparisIstatistik.OptionsView.ShowFooter = true;
            this.gridViewSiparisIstatistik.OptionsView.ShowGroupPanel = false;
            // 
            // dateEditBitTarih
            // 
            this.dateEditBitTarih.EditValue = null;
            this.dateEditBitTarih.Location = new System.Drawing.Point(505, 12);
            this.dateEditBitTarih.MenuManager = this.barManagerMalzemeSiparisIstatistikleri;
            this.dateEditBitTarih.Name = "dateEditBitTarih";
            this.dateEditBitTarih.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBitTarih.Properties.Appearance.Options.UseFont = true;
            this.dateEditBitTarih.Properties.AutoHeight = false;
            this.dateEditBitTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBitTarih.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBitTarih.Size = new System.Drawing.Size(171, 36);
            this.dateEditBitTarih.StyleController = this.layoutControlRaporlama;
            this.dateEditBitTarih.TabIndex = 5;
            // 
            // dateEditBasTarih
            // 
            this.dateEditBasTarih.EditValue = null;
            this.dateEditBasTarih.Location = new System.Drawing.Point(171, 12);
            this.dateEditBasTarih.MenuManager = this.barManagerMalzemeSiparisIstatistikleri;
            this.dateEditBasTarih.Name = "dateEditBasTarih";
            this.dateEditBasTarih.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBasTarih.Properties.Appearance.Options.UseFont = true;
            this.dateEditBasTarih.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBasTarih.Properties.AppearanceDisabled.Options.UseFont = true;
            this.dateEditBasTarih.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBasTarih.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dateEditBasTarih.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBasTarih.Properties.AppearanceFocused.Options.UseFont = true;
            this.dateEditBasTarih.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.dateEditBasTarih.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.dateEditBasTarih.Properties.AutoHeight = false;
            this.dateEditBasTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBasTarih.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBasTarih.Size = new System.Drawing.Size(171, 36);
            this.dateEditBasTarih.StyleController = this.layoutControlRaporlama;
            this.dateEditBasTarih.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemBasTarih,
            this.layoutControlItemBitTarih,
            this.emptySpaceItem2,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1227, 483);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemBasTarih
            // 
            this.layoutControlItemBasTarih.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemBasTarih.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemBasTarih.Control = this.dateEditBasTarih;
            this.layoutControlItemBasTarih.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemBasTarih.MinSize = new System.Drawing.Size(226, 26);
            this.layoutControlItemBasTarih.Name = "layoutControlItemBasTarih";
            this.layoutControlItemBasTarih.Size = new System.Drawing.Size(334, 40);
            this.layoutControlItemBasTarih.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBasTarih.Text = " Başlangıç Tarihi  :";
            this.layoutControlItemBasTarih.TextSize = new System.Drawing.Size(156, 21);
            // 
            // layoutControlItemBitTarih
            // 
            this.layoutControlItemBitTarih.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemBitTarih.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemBitTarih.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItemBitTarih.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItemBitTarih.Control = this.dateEditBitTarih;
            this.layoutControlItemBitTarih.Location = new System.Drawing.Point(334, 0);
            this.layoutControlItemBitTarih.MinSize = new System.Drawing.Size(221, 26);
            this.layoutControlItemBitTarih.Name = "layoutControlItemBitTarih";
            this.layoutControlItemBitTarih.Size = new System.Drawing.Size(334, 40);
            this.layoutControlItemBitTarih.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBitTarih.Text = " Bitiş Tarihi  :";
            this.layoutControlItemBitTarih.TextSize = new System.Drawing.Size(156, 21);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(668, 0);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(539, 40);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlSiparisIstatistik;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1207, 423);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmMalzemeSiparisIstatistikleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 553);
            this.Controls.Add(this.layoutControlRaporlama);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmMalzemeSiparisIstatistikleri";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sipariş İstatistikleri";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMalzemeSiparisIstatistikleri_FormClosed);
            this.Load += new System.EventHandler(this.frmMalzemeSiparisIstatistikleri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMalzemeSiparisIstatistikleri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRaporlama)).EndInit();
            this.layoutControlRaporlama.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSiparisIstatistik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSiparisIstatistik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBasTarih)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBitTarih)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagerMalzemeSiparisIstatistikleri;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem barStaticItemSiparisİstatistik;
        private DevExpress.XtraBars.BarButtonItem barButtonItemListele;
        private DevExpress.XtraBars.BarButtonItem barSubItemYazdir;
        private DevExpress.XtraBars.BarButtonItem barButtonItemKapat;
        private DevExpress.XtraLayout.LayoutControl layoutControlRaporlama;
        private DevExpress.XtraEditors.DateEdit dateEditBitTarih;
        private DevExpress.XtraEditors.DateEdit dateEditBasTarih;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBasTarih;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBitTarih;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.GridControl gridControlSiparisIstatistik;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSiparisIstatistik;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemYazdir;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExcel;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPdf;
        private System.Windows.Forms.SaveFileDialog saveFileDialogMalzemeRapor;
    }
}