namespace MALZEMETAKIPSISTEMI
{
    partial class frmYetkiEkle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmYetkiEkle));
            this.layoutControlMalzemeTalepYetkiler = new DevExpress.XtraLayout.LayoutControl();
            this.trvTumYetkilerKullanici = new System.Windows.Forms.TreeView();
            this.trvTumYetkiler = new System.Windows.Forms.TreeView();
            this.comboBoxEditKullaniciAdi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.barManagerMalzemeTalepYetkiler = new DevExpress.XtraBars.BarManager(this.components);
            this.barMalzemeTalepYetkiler = new DevExpress.XtraBars.Bar();
            this.barStaticItemYetkiİslemleri = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItemYetkilerYeniKayıt = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemYetkilerKaydet = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemYetkilerKapat = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControlGroupMalzemeTalepYetkiler = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemKullanici = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTumYetkiler = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMalzemeTalepYetkiler)).BeginInit();
            this.layoutControlMalzemeTalepYetkiler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditKullaniciAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMalzemeTalepYetkiler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMalzemeTalepYetkiler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKullanici)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTumYetkiler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMalzemeTalepYetkiler
            // 
            this.layoutControlMalzemeTalepYetkiler.Controls.Add(this.trvTumYetkilerKullanici);
            this.layoutControlMalzemeTalepYetkiler.Controls.Add(this.trvTumYetkiler);
            this.layoutControlMalzemeTalepYetkiler.Controls.Add(this.comboBoxEditKullaniciAdi);
            this.layoutControlMalzemeTalepYetkiler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMalzemeTalepYetkiler.Location = new System.Drawing.Point(0, 48);
            this.layoutControlMalzemeTalepYetkiler.Name = "layoutControlMalzemeTalepYetkiler";
            this.layoutControlMalzemeTalepYetkiler.Root = this.layoutControlGroupMalzemeTalepYetkiler;
            this.layoutControlMalzemeTalepYetkiler.Size = new System.Drawing.Size(801, 643);
            this.layoutControlMalzemeTalepYetkiler.TabIndex = 0;
            this.layoutControlMalzemeTalepYetkiler.Text = "layoutControl1";
            // 
            // trvTumYetkilerKullanici
            // 
            this.trvTumYetkilerKullanici.Location = new System.Drawing.Point(402, 76);
            this.trvTumYetkilerKullanici.Name = "trvTumYetkilerKullanici";
            this.trvTumYetkilerKullanici.Size = new System.Drawing.Size(387, 555);
            this.trvTumYetkilerKullanici.TabIndex = 6;
            this.trvTumYetkilerKullanici.DoubleClick += new System.EventHandler(this.trvTumYetkilerKullanici_DoubleClick);
            // 
            // trvTumYetkiler
            // 
            this.trvTumYetkiler.Location = new System.Drawing.Point(12, 76);
            this.trvTumYetkiler.Name = "trvTumYetkiler";
            this.trvTumYetkiler.Size = new System.Drawing.Size(386, 555);
            this.trvTumYetkiler.TabIndex = 5;
            this.trvTumYetkiler.DoubleClick += new System.EventHandler(this.trvTumYetkiler_DoubleClick);
            // 
            // comboBoxEditKullaniciAdi
            // 
            this.comboBoxEditKullaniciAdi.Location = new System.Drawing.Point(178, 12);
            this.comboBoxEditKullaniciAdi.MenuManager = this.barManagerMalzemeTalepYetkiler;
            this.comboBoxEditKullaniciAdi.Name = "comboBoxEditKullaniciAdi";
            this.comboBoxEditKullaniciAdi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditKullaniciAdi.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditKullaniciAdi.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditKullaniciAdi.Properties.AppearanceDropDown.Options.UseFont = true;
            this.comboBoxEditKullaniciAdi.Properties.AutoHeight = false;
            this.comboBoxEditKullaniciAdi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditKullaniciAdi.Size = new System.Drawing.Size(611, 35);
            this.comboBoxEditKullaniciAdi.StyleController = this.layoutControlMalzemeTalepYetkiler;
            this.comboBoxEditKullaniciAdi.TabIndex = 4;
            this.comboBoxEditKullaniciAdi.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // barManagerMalzemeTalepYetkiler
            // 
            this.barManagerMalzemeTalepYetkiler.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMalzemeTalepYetkiler});
            this.barManagerMalzemeTalepYetkiler.DockControls.Add(this.barDockControlTop);
            this.barManagerMalzemeTalepYetkiler.DockControls.Add(this.barDockControlBottom);
            this.barManagerMalzemeTalepYetkiler.DockControls.Add(this.barDockControlLeft);
            this.barManagerMalzemeTalepYetkiler.DockControls.Add(this.barDockControlRight);
            this.barManagerMalzemeTalepYetkiler.Form = this;
            this.barManagerMalzemeTalepYetkiler.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItemYetkiİslemleri,
            this.barButtonItemYetkilerYeniKayıt,
            this.barButtonItemYetkilerKaydet,
            this.barButtonItemYetkilerKapat});
            this.barManagerMalzemeTalepYetkiler.MainMenu = this.barMalzemeTalepYetkiler;
            this.barManagerMalzemeTalepYetkiler.MaxItemId = 4;
            // 
            // barMalzemeTalepYetkiler
            // 
            this.barMalzemeTalepYetkiler.BarName = "Main menu";
            this.barMalzemeTalepYetkiler.DockCol = 0;
            this.barMalzemeTalepYetkiler.DockRow = 0;
            this.barMalzemeTalepYetkiler.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMalzemeTalepYetkiler.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemYetkiİslemleri),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemYetkilerKapat, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barMalzemeTalepYetkiler.OptionsBar.MultiLine = true;
            this.barMalzemeTalepYetkiler.OptionsBar.UseWholeRow = true;
            this.barMalzemeTalepYetkiler.Text = "Main menu";
            // 
            // barStaticItemYetkiİslemleri
            // 
            this.barStaticItemYetkiİslemleri.Caption = "Yetki İşlemleri";
            this.barStaticItemYetkiİslemleri.Id = 0;
            this.barStaticItemYetkiİslemleri.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barStaticItemYetkiİslemleri.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItemYetkiİslemleri.Name = "barStaticItemYetkiİslemleri";
            this.barStaticItemYetkiİslemleri.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barButtonItemYetkilerYeniKayıt
            // 
            this.barButtonItemYetkilerYeniKayıt.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemYetkilerYeniKayıt.Caption = "Yeni Kayıt";
            this.barButtonItemYetkilerYeniKayıt.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemYetkilerYeniKayıt.Glyph")));
            this.barButtonItemYetkilerYeniKayıt.Id = 1;
            this.barButtonItemYetkilerYeniKayıt.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barButtonItemYetkilerYeniKayıt.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemYetkilerYeniKayıt.Name = "barButtonItemYetkilerYeniKayıt";
            this.barButtonItemYetkilerYeniKayıt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemYetkilerYeniKayıt_ItemClick);
            // 
            // barButtonItemYetkilerKaydet
            // 
            this.barButtonItemYetkilerKaydet.Caption = "Kaydet";
            this.barButtonItemYetkilerKaydet.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemYetkilerKaydet.Glyph")));
            this.barButtonItemYetkilerKaydet.Id = 2;
            this.barButtonItemYetkilerKaydet.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barButtonItemYetkilerKaydet.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemYetkilerKaydet.Name = "barButtonItemYetkilerKaydet";
            this.barButtonItemYetkilerKaydet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemYetkilerKaydet_ItemClick);
            // 
            // barButtonItemYetkilerKapat
            // 
            this.barButtonItemYetkilerKapat.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemYetkilerKapat.Caption = "Kapat";
            this.barButtonItemYetkilerKapat.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemYetkilerKapat.Glyph")));
            this.barButtonItemYetkilerKapat.Id = 3;
            this.barButtonItemYetkilerKapat.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.barButtonItemYetkilerKapat.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemYetkilerKapat.Name = "barButtonItemYetkilerKapat";
            this.barButtonItemYetkilerKapat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemYetkilerKapat_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(801, 48);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 691);
            this.barDockControlBottom.Size = new System.Drawing.Size(801, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 48);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 643);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(801, 48);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 643);
            // 
            // layoutControlGroupMalzemeTalepYetkiler
            // 
            this.layoutControlGroupMalzemeTalepYetkiler.CustomizationFormText = "layoutControlGroupMalzemeTalepYetkiler";
            this.layoutControlGroupMalzemeTalepYetkiler.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMalzemeTalepYetkiler.GroupBordersVisible = false;
            this.layoutControlGroupMalzemeTalepYetkiler.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemKullanici,
            this.layoutControlItemTumYetkiler,
            this.layoutControlItem3});
            this.layoutControlGroupMalzemeTalepYetkiler.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMalzemeTalepYetkiler.Name = "layoutControlGroupMalzemeTalepYetkiler";
            this.layoutControlGroupMalzemeTalepYetkiler.Size = new System.Drawing.Size(801, 643);
            this.layoutControlGroupMalzemeTalepYetkiler.Text = "layoutControlGroupMalzemeTalepYetkiler";
            this.layoutControlGroupMalzemeTalepYetkiler.TextVisible = false;
            // 
            // layoutControlItemKullanici
            // 
            this.layoutControlItemKullanici.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemKullanici.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemKullanici.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItemKullanici.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItemKullanici.Control = this.comboBoxEditKullaniciAdi;
            this.layoutControlItemKullanici.CustomizationFormText = " Kullanıcı Adı  :";
            this.layoutControlItemKullanici.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemKullanici.MaxSize = new System.Drawing.Size(0, 39);
            this.layoutControlItemKullanici.MinSize = new System.Drawing.Size(166, 39);
            this.layoutControlItemKullanici.Name = "layoutControlItemKullanici";
            this.layoutControlItemKullanici.Size = new System.Drawing.Size(781, 39);
            this.layoutControlItemKullanici.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemKullanici.Text = " Kullanıcı Adı  :";
            this.layoutControlItemKullanici.TextSize = new System.Drawing.Size(163, 21);
            // 
            // layoutControlItemTumYetkiler
            // 
            this.layoutControlItemTumYetkiler.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemTumYetkiler.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemTumYetkiler.Control = this.trvTumYetkiler;
            this.layoutControlItemTumYetkiler.CustomizationFormText = " Tüm Yetkiler";
            this.layoutControlItemTumYetkiler.Location = new System.Drawing.Point(0, 39);
            this.layoutControlItemTumYetkiler.MinSize = new System.Drawing.Size(280, 49);
            this.layoutControlItemTumYetkiler.Name = "layoutControlItemTumYetkiler";
            this.layoutControlItemTumYetkiler.Size = new System.Drawing.Size(390, 584);
            this.layoutControlItemTumYetkiler.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemTumYetkiler.Text = " Tüm Yetkiler";
            this.layoutControlItemTumYetkiler.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemTumYetkiler.TextSize = new System.Drawing.Size(163, 22);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.trvTumYetkilerKullanici;
            this.layoutControlItem3.CustomizationFormText = " Kullanıcı Yetkileri";
            this.layoutControlItem3.Location = new System.Drawing.Point(390, 39);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(131, 43);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(391, 584);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = " Kullanıcı Yetkileri";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(163, 22);
            // 
            // frmYetkiEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 691);
            this.Controls.Add(this.layoutControlMalzemeTalepYetkiler);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmYetkiEkle";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yetki Ekle";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmYetkiEkle_FormClosed);
            this.Load += new System.EventHandler(this.frmYetkiEkle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMalzemeTalepYetkiler)).EndInit();
            this.layoutControlMalzemeTalepYetkiler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditKullaniciAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMalzemeTalepYetkiler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMalzemeTalepYetkiler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKullanici)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTumYetkiler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMalzemeTalepYetkiler;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMalzemeTalepYetkiler;
        private DevExpress.XtraBars.BarManager barManagerMalzemeTalepYetkiler;
        private DevExpress.XtraBars.Bar barMalzemeTalepYetkiler;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem barStaticItemYetkiİslemleri;
        private DevExpress.XtraBars.BarButtonItem barButtonItemYetkilerYeniKayıt;
        private DevExpress.XtraBars.BarButtonItem barButtonItemYetkilerKaydet;
        private DevExpress.XtraBars.BarButtonItem barButtonItemYetkilerKapat;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditKullaniciAdi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKullanici;
        private System.Windows.Forms.TreeView trvTumYetkilerKullanici;
        private System.Windows.Forms.TreeView trvTumYetkiler;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTumYetkiler;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}