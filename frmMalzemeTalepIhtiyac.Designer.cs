namespace MALZEMETAKIPSISTEMI
{
    partial class frmMalzemeTalepIhtiyac
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMalzemeTalepIhtiyac));
            this.layoutControlMalzemeOtomatikTalep = new DevExpress.XtraLayout.LayoutControl();
            this.radioGroupDurum = new DevExpress.XtraEditors.RadioGroup();
            this.barManagerMalzemeTalepleri = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barStaticItemMalzemeTalepleri = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItemMalzemeTalepListele = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemMalzemeTalepYazdir = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemYazdir = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemMalzemeTalepKapat = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControlMalzemeTalepler = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStripMalzemeTalep = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.malzemeTalepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewMalzemeTalepler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroupMalzemeTalepIhtiyac = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemMalzemeTalepler = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDurum = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMalzemeOtomatikTalep)).BeginInit();
            this.layoutControlMalzemeOtomatikTalep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupDurum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMalzemeTalepleri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMalzemeTalepler)).BeginInit();
            this.contextMenuStripMalzemeTalep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMalzemeTalepler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMalzemeTalepIhtiyac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMalzemeTalepler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDurum)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMalzemeOtomatikTalep
            // 
            this.layoutControlMalzemeOtomatikTalep.Controls.Add(this.radioGroupDurum);
            this.layoutControlMalzemeOtomatikTalep.Controls.Add(this.gridControlMalzemeTalepler);
            this.layoutControlMalzemeOtomatikTalep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMalzemeOtomatikTalep.Location = new System.Drawing.Point(0, 50);
            this.layoutControlMalzemeOtomatikTalep.Name = "layoutControlMalzemeOtomatikTalep";
            this.layoutControlMalzemeOtomatikTalep.Root = this.layoutControlGroupMalzemeTalepIhtiyac;
            this.layoutControlMalzemeOtomatikTalep.Size = new System.Drawing.Size(782, 683);
            this.layoutControlMalzemeOtomatikTalep.TabIndex = 0;
            // 
            // radioGroupDurum
            // 
            this.radioGroupDurum.Location = new System.Drawing.Point(153, 12);
            this.radioGroupDurum.MenuManager = this.barManagerMalzemeTalepleri;
            this.radioGroupDurum.Name = "radioGroupDurum";
            this.radioGroupDurum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.radioGroupDurum.Properties.Appearance.Options.UseFont = true;
            this.radioGroupDurum.Properties.Columns = 2;
            this.radioGroupDurum.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Maksimuma Göre Çalış"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Ortalamaya Göre Çalış")});
            this.radioGroupDurum.Size = new System.Drawing.Size(617, 40);
            this.radioGroupDurum.StyleController = this.layoutControlMalzemeOtomatikTalep;
            this.radioGroupDurum.TabIndex = 5;
            this.radioGroupDurum.EditValueChanged += new System.EventHandler(this.radioGroupDurum_EditValueChanged);
            // 
            // barManagerMalzemeTalepleri
            // 
            this.barManagerMalzemeTalepleri.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
            this.barManagerMalzemeTalepleri.DockControls.Add(this.barDockControlTop);
            this.barManagerMalzemeTalepleri.DockControls.Add(this.barDockControlBottom);
            this.barManagerMalzemeTalepleri.DockControls.Add(this.barDockControlLeft);
            this.barManagerMalzemeTalepleri.DockControls.Add(this.barDockControlRight);
            this.barManagerMalzemeTalepleri.Form = this;
            this.barManagerMalzemeTalepleri.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItemMalzemeTalepleri,
            this.barButtonItemMalzemeTalepListele,
            this.barButtonItemMalzemeTalepKapat,
            this.barSubItemMalzemeTalepYazdir,
            this.barButtonItemYazdir,
            this.barButtonItem2});
            this.barManagerMalzemeTalepleri.MainMenu = this.bar2;
            this.barManagerMalzemeTalepleri.MaxItemId = 6;
            this.barManagerMalzemeTalepleri.StatusBar = this.bar3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemMalzemeTalepleri),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemMalzemeTalepListele, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItemMalzemeTalepYazdir, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemMalzemeTalepKapat, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barStaticItemMalzemeTalepleri
            // 
            this.barStaticItemMalzemeTalepleri.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.barStaticItemMalzemeTalepleri.Caption = "Malzeme Talep İhtiyaç";
            this.barStaticItemMalzemeTalepleri.Id = 0;
            this.barStaticItemMalzemeTalepleri.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.barStaticItemMalzemeTalepleri.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItemMalzemeTalepleri.Name = "barStaticItemMalzemeTalepleri";
            // 
            // barButtonItemMalzemeTalepListele
            // 
            this.barButtonItemMalzemeTalepListele.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemMalzemeTalepListele.Caption = "Listele";
            this.barButtonItemMalzemeTalepListele.Id = 1;
            this.barButtonItemMalzemeTalepListele.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemMalzemeTalepListele.ImageOptions.Image")));
            this.barButtonItemMalzemeTalepListele.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.barButtonItemMalzemeTalepListele.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemMalzemeTalepListele.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.barButtonItemMalzemeTalepListele.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.barButtonItemMalzemeTalepListele.Name = "barButtonItemMalzemeTalepListele";
            this.barButtonItemMalzemeTalepListele.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemMalzemeTalepListele_ItemClick);
            // 
            // barSubItemMalzemeTalepYazdir
            // 
            this.barSubItemMalzemeTalepYazdir.Caption = "Yazdır";
            this.barSubItemMalzemeTalepYazdir.Id = 3;
            this.barSubItemMalzemeTalepYazdir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barSubItemMalzemeTalepYazdir.ImageOptions.Image")));
            this.barSubItemMalzemeTalepYazdir.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.barSubItemMalzemeTalepYazdir.ItemAppearance.Normal.Options.UseFont = true;
            this.barSubItemMalzemeTalepYazdir.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemYazdir),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.barSubItemMalzemeTalepYazdir.Name = "barSubItemMalzemeTalepYazdir";
            // 
            // barButtonItemYazdir
            // 
            this.barButtonItemYazdir.Caption = "Yazdır";
            this.barButtonItemYazdir.Id = 4;
            this.barButtonItemYazdir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemYazdir.ImageOptions.Image")));
            this.barButtonItemYazdir.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.barButtonItemYazdir.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemYazdir.Name = "barButtonItemYazdir";
            this.barButtonItemYazdir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemYazdir_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Aktar";
            this.barButtonItem2.Id = 5;
            this.barButtonItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.Image")));
            this.barButtonItem2.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.barButtonItem2.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItemMalzemeTalepKapat
            // 
            this.barButtonItemMalzemeTalepKapat.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemMalzemeTalepKapat.Caption = "Kapat";
            this.barButtonItemMalzemeTalepKapat.Id = 2;
            this.barButtonItemMalzemeTalepKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemMalzemeTalepKapat.ImageOptions.Image")));
            this.barButtonItemMalzemeTalepKapat.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.barButtonItemMalzemeTalepKapat.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemMalzemeTalepKapat.Name = "barButtonItemMalzemeTalepKapat";
            this.barButtonItemMalzemeTalepKapat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemMalzemeTalepKapat_ItemClick);
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
            this.barDockControlTop.Manager = this.barManagerMalzemeTalepleri;
            this.barDockControlTop.Size = new System.Drawing.Size(782, 50);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 733);
            this.barDockControlBottom.Manager = this.barManagerMalzemeTalepleri;
            this.barDockControlBottom.Size = new System.Drawing.Size(782, 20);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 50);
            this.barDockControlLeft.Manager = this.barManagerMalzemeTalepleri;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 683);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(782, 50);
            this.barDockControlRight.Manager = this.barManagerMalzemeTalepleri;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 683);
            // 
            // gridControlMalzemeTalepler
            // 
            this.gridControlMalzemeTalepler.ContextMenuStrip = this.contextMenuStripMalzemeTalep;
            this.gridControlMalzemeTalepler.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControlMalzemeTalepler.Location = new System.Drawing.Point(12, 56);
            this.gridControlMalzemeTalepler.MainView = this.gridViewMalzemeTalepler;
            this.gridControlMalzemeTalepler.MenuManager = this.barManagerMalzemeTalepleri;
            this.gridControlMalzemeTalepler.Name = "gridControlMalzemeTalepler";
            this.gridControlMalzemeTalepler.Size = new System.Drawing.Size(758, 615);
            this.gridControlMalzemeTalepler.TabIndex = 4;
            this.gridControlMalzemeTalepler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMalzemeTalepler});
            // 
            // contextMenuStripMalzemeTalep
            // 
            this.contextMenuStripMalzemeTalep.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.contextMenuStripMalzemeTalep.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripMalzemeTalep.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.malzemeTalepToolStripMenuItem});
            this.contextMenuStripMalzemeTalep.Name = "contextMenuStripMalzemeTanimlama";
            this.contextMenuStripMalzemeTalep.Size = new System.Drawing.Size(260, 34);
            // 
            // malzemeTalepToolStripMenuItem
            // 
            this.malzemeTalepToolStripMenuItem.AccessibleName = "";
            this.malzemeTalepToolStripMenuItem.Name = "malzemeTalepToolStripMenuItem";
            this.malzemeTalepToolStripMenuItem.Size = new System.Drawing.Size(259, 30);
            this.malzemeTalepToolStripMenuItem.Text = "Malzeme Talep Giriş";
            this.malzemeTalepToolStripMenuItem.Click += new System.EventHandler(this.malzemeTalepToolStripMenuItem_Click);
            // 
            // gridViewMalzemeTalepler
            // 
            this.gridViewMalzemeTalepler.GridControl = this.gridControlMalzemeTalepler;
            this.gridViewMalzemeTalepler.Name = "gridViewMalzemeTalepler";
            this.gridViewMalzemeTalepler.OptionsBehavior.Editable = false;
            this.gridViewMalzemeTalepler.OptionsBehavior.ReadOnly = true;
            this.gridViewMalzemeTalepler.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewMalzemeTalepler.OptionsPrint.AutoWidth = false;
            this.gridViewMalzemeTalepler.OptionsSelection.MultiSelect = true;
            this.gridViewMalzemeTalepler.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewMalzemeTalepler.OptionsView.ColumnAutoWidth = false;
            this.gridViewMalzemeTalepler.OptionsView.ShowAutoFilterRow = true;
            this.gridViewMalzemeTalepler.OptionsView.ShowFooter = true;
            this.gridViewMalzemeTalepler.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroupMalzemeTalepIhtiyac
            // 
            this.layoutControlGroupMalzemeTalepIhtiyac.CustomizationFormText = "layoutControlGroupMalzemeOtomatikTalep";
            this.layoutControlGroupMalzemeTalepIhtiyac.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMalzemeTalepIhtiyac.GroupBordersVisible = false;
            this.layoutControlGroupMalzemeTalepIhtiyac.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemMalzemeTalepler,
            this.layoutControlItemDurum});
            this.layoutControlGroupMalzemeTalepIhtiyac.Name = "layoutControlGroupMalzemeTalepIhtiyac";
            this.layoutControlGroupMalzemeTalepIhtiyac.Size = new System.Drawing.Size(782, 683);
            this.layoutControlGroupMalzemeTalepIhtiyac.TextVisible = false;
            // 
            // layoutControlItemMalzemeTalepler
            // 
            this.layoutControlItemMalzemeTalepler.Control = this.gridControlMalzemeTalepler;
            this.layoutControlItemMalzemeTalepler.CustomizationFormText = "layoutControlItemMalzemeTalepler";
            this.layoutControlItemMalzemeTalepler.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItemMalzemeTalepler.MinSize = new System.Drawing.Size(307, 24);
            this.layoutControlItemMalzemeTalepler.Name = "layoutControlItemMalzemeTalepler";
            this.layoutControlItemMalzemeTalepler.Size = new System.Drawing.Size(762, 619);
            this.layoutControlItemMalzemeTalepler.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemMalzemeTalepler.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemMalzemeTalepler.TextVisible = false;
            // 
            // layoutControlItemDurum
            // 
            this.layoutControlItemDurum.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemDurum.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemDurum.Control = this.radioGroupDurum;
            this.layoutControlItemDurum.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemDurum.MinSize = new System.Drawing.Size(177, 20);
            this.layoutControlItemDurum.Name = "layoutControlItemDurum";
            this.layoutControlItemDurum.Size = new System.Drawing.Size(762, 44);
            this.layoutControlItemDurum.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemDurum.Text = " Çalışma Durumu  :";
            this.layoutControlItemDurum.TextSize = new System.Drawing.Size(138, 18);
            // 
            // frmMalzemeTalepIhtiyac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.layoutControlMalzemeOtomatikTalep);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmMalzemeTalepIhtiyac";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Malzeme Talep İhtiyaç";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMalzemeTalepIhtiyac_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMalzemeOtomatikTalep)).EndInit();
            this.layoutControlMalzemeOtomatikTalep.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupDurum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMalzemeTalepleri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMalzemeTalepler)).EndInit();
            this.contextMenuStripMalzemeTalep.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMalzemeTalepler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMalzemeTalepIhtiyac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMalzemeTalepler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDurum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMalzemeOtomatikTalep;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMalzemeTalepIhtiyac;
        private DevExpress.XtraBars.BarManager barManagerMalzemeTalepleri;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarStaticItem barStaticItemMalzemeTalepleri;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMalzemeTalepListele;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMalzemeTalepKapat;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMalzemeTalepler;
        private DevExpress.XtraBars.BarSubItem barSubItemMalzemeTalepYazdir;
        private DevExpress.XtraBars.BarButtonItem barButtonItemYazdir;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMalzemeTalep;
        private System.Windows.Forms.ToolStripMenuItem malzemeTalepToolStripMenuItem;
        public DevExpress.XtraGrid.GridControl gridControlMalzemeTalepler;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewMalzemeTalepler;
        private DevExpress.XtraEditors.RadioGroup radioGroupDurum;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDurum;
    }
}