namespace MALZEMETAKIPSISTEMI
{
    partial class frmAnaGrupEkle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnaGrupEkle));
            this.layoutControlAnaGrup = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlGruplar = new DevExpress.XtraGrid.GridControl();
            this.gridViewGruplar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButtonGrupKapat = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonGrupSil = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonGrupKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonGrupYeniKayit = new DevExpress.XtraEditors.SimpleButton();
            this.checkEditGrupDurum = new DevExpress.XtraEditors.CheckEdit();
            this.textEditGrupNo = new DevExpress.XtraEditors.TextEdit();
            this.textEditGrupAdi = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroupAnaGrup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGrupAdi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrupNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrupDurum = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrupYeniKayit = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrupKaydet = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrupSil = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrupKapat = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrupListesi = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAnaGrup)).BeginInit();
            this.layoutControlAnaGrup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGruplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGruplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditGrupDurum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGrupNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGrupAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAnaGrup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupAdi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupDurum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupYeniKayit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupKaydet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupSil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupKapat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupListesi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlAnaGrup
            // 
            this.layoutControlAnaGrup.Controls.Add(this.gridControlGruplar);
            this.layoutControlAnaGrup.Controls.Add(this.simpleButtonGrupKapat);
            this.layoutControlAnaGrup.Controls.Add(this.simpleButtonGrupSil);
            this.layoutControlAnaGrup.Controls.Add(this.simpleButtonGrupKaydet);
            this.layoutControlAnaGrup.Controls.Add(this.simpleButtonGrupYeniKayit);
            this.layoutControlAnaGrup.Controls.Add(this.checkEditGrupDurum);
            this.layoutControlAnaGrup.Controls.Add(this.textEditGrupNo);
            this.layoutControlAnaGrup.Controls.Add(this.textEditGrupAdi);
            this.layoutControlAnaGrup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlAnaGrup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlAnaGrup.Name = "layoutControlAnaGrup";
            this.layoutControlAnaGrup.Root = this.layoutControlGroupAnaGrup;
            this.layoutControlAnaGrup.Size = new System.Drawing.Size(782, 553);
            this.layoutControlAnaGrup.TabIndex = 0;
            this.layoutControlAnaGrup.Text = "layoutControl1";
            // 
            // gridControlGruplar
            // 
            this.gridControlGruplar.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControlGruplar.Location = new System.Drawing.Point(12, 149);
            this.gridControlGruplar.MainView = this.gridViewGruplar;
            this.gridControlGruplar.Name = "gridControlGruplar";
            this.gridControlGruplar.Size = new System.Drawing.Size(758, 392);
            this.gridControlGruplar.TabIndex = 11;
            this.gridControlGruplar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGruplar});
            // 
            // gridViewGruplar
            // 
            this.gridViewGruplar.GridControl = this.gridControlGruplar;
            this.gridViewGruplar.Name = "gridViewGruplar";
            this.gridViewGruplar.OptionsBehavior.Editable = false;
            this.gridViewGruplar.OptionsBehavior.ReadOnly = true;
            this.gridViewGruplar.OptionsView.ColumnAutoWidth = false;
            this.gridViewGruplar.OptionsView.ShowAutoFilterRow = true;
            this.gridViewGruplar.OptionsView.ShowFooter = true;
            this.gridViewGruplar.OptionsView.ShowGroupPanel = false;
            this.gridViewGruplar.DoubleClick += new System.EventHandler(this.gridViewGruplar_DoubleClick);
            // 
            // simpleButtonGrupKapat
            // 
            this.simpleButtonGrupKapat.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleButtonGrupKapat.Appearance.Options.UseFont = true;
            this.simpleButtonGrupKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonGrupKapat.ImageOptions.Image")));
            this.simpleButtonGrupKapat.Location = new System.Drawing.Point(573, 100);
            this.simpleButtonGrupKapat.Name = "simpleButtonGrupKapat";
            this.simpleButtonGrupKapat.Size = new System.Drawing.Size(197, 45);
            this.simpleButtonGrupKapat.StyleController = this.layoutControlAnaGrup;
            this.simpleButtonGrupKapat.TabIndex = 10;
            this.simpleButtonGrupKapat.Text = "KAPAT";
            this.simpleButtonGrupKapat.Click += new System.EventHandler(this.simpleButtonGrupKapat_Click);
            // 
            // simpleButtonGrupSil
            // 
            this.simpleButtonGrupSil.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleButtonGrupSil.Appearance.Options.UseFont = true;
            this.simpleButtonGrupSil.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonGrupSil.ImageOptions.Image")));
            this.simpleButtonGrupSil.Location = new System.Drawing.Point(386, 100);
            this.simpleButtonGrupSil.Name = "simpleButtonGrupSil";
            this.simpleButtonGrupSil.Size = new System.Drawing.Size(183, 45);
            this.simpleButtonGrupSil.StyleController = this.layoutControlAnaGrup;
            this.simpleButtonGrupSil.TabIndex = 9;
            this.simpleButtonGrupSil.Text = "SİL";
            this.simpleButtonGrupSil.Click += new System.EventHandler(this.simpleButtonGrupSil_Click);
            // 
            // simpleButtonGrupKaydet
            // 
            this.simpleButtonGrupKaydet.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleButtonGrupKaydet.Appearance.Options.UseFont = true;
            this.simpleButtonGrupKaydet.Enabled = false;
            this.simpleButtonGrupKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonGrupKaydet.ImageOptions.Image")));
            this.simpleButtonGrupKaydet.Location = new System.Drawing.Point(199, 100);
            this.simpleButtonGrupKaydet.Name = "simpleButtonGrupKaydet";
            this.simpleButtonGrupKaydet.Size = new System.Drawing.Size(183, 45);
            this.simpleButtonGrupKaydet.StyleController = this.layoutControlAnaGrup;
            this.simpleButtonGrupKaydet.TabIndex = 8;
            this.simpleButtonGrupKaydet.Text = "KAYDET";
            this.simpleButtonGrupKaydet.Click += new System.EventHandler(this.simpleButtonGrupKaydet_Click);
            // 
            // simpleButtonGrupYeniKayit
            // 
            this.simpleButtonGrupYeniKayit.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleButtonGrupYeniKayit.Appearance.Options.UseFont = true;
            this.simpleButtonGrupYeniKayit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonGrupYeniKayit.ImageOptions.Image")));
            this.simpleButtonGrupYeniKayit.Location = new System.Drawing.Point(12, 100);
            this.simpleButtonGrupYeniKayit.Name = "simpleButtonGrupYeniKayit";
            this.simpleButtonGrupYeniKayit.Size = new System.Drawing.Size(183, 45);
            this.simpleButtonGrupYeniKayit.StyleController = this.layoutControlAnaGrup;
            this.simpleButtonGrupYeniKayit.TabIndex = 7;
            this.simpleButtonGrupYeniKayit.Text = "YENİ KAYIT";
            this.simpleButtonGrupYeniKayit.Click += new System.EventHandler(this.simpleButtonGrupYeniKayit_Click);
            // 
            // checkEditGrupDurum
            // 
            this.checkEditGrupDurum.Location = new System.Drawing.Point(205, 56);
            this.checkEditGrupDurum.Name = "checkEditGrupDurum";
            this.checkEditGrupDurum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.checkEditGrupDurum.Properties.Appearance.Options.UseFont = true;
            this.checkEditGrupDurum.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.checkEditGrupDurum.Properties.AppearanceDisabled.Options.UseFont = true;
            this.checkEditGrupDurum.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.checkEditGrupDurum.Properties.AppearanceFocused.Options.UseFont = true;
            this.checkEditGrupDurum.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.checkEditGrupDurum.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.checkEditGrupDurum.Properties.AutoHeight = false;
            this.checkEditGrupDurum.Properties.Caption = " KULLANILIYOR";
            this.checkEditGrupDurum.Size = new System.Drawing.Size(565, 40);
            this.checkEditGrupDurum.StyleController = this.layoutControlAnaGrup;
            this.checkEditGrupDurum.TabIndex = 6;
            // 
            // textEditGrupNo
            // 
            this.textEditGrupNo.Enabled = false;
            this.textEditGrupNo.Location = new System.Drawing.Point(586, 12);
            this.textEditGrupNo.Name = "textEditGrupNo";
            this.textEditGrupNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditGrupNo.Properties.Appearance.Options.UseFont = true;
            this.textEditGrupNo.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditGrupNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.textEditGrupNo.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditGrupNo.Properties.AppearanceDisabled.Options.UseFont = true;
            this.textEditGrupNo.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.textEditGrupNo.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.textEditGrupNo.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditGrupNo.Properties.AppearanceFocused.Options.UseFont = true;
            this.textEditGrupNo.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.textEditGrupNo.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.textEditGrupNo.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditGrupNo.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.textEditGrupNo.Properties.AutoHeight = false;
            this.textEditGrupNo.Properties.ReadOnly = true;
            this.textEditGrupNo.Size = new System.Drawing.Size(184, 40);
            this.textEditGrupNo.StyleController = this.layoutControlAnaGrup;
            this.textEditGrupNo.TabIndex = 5;
            // 
            // textEditGrupAdi
            // 
            this.textEditGrupAdi.Enabled = false;
            this.textEditGrupAdi.Location = new System.Drawing.Point(205, 12);
            this.textEditGrupAdi.Name = "textEditGrupAdi";
            this.textEditGrupAdi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditGrupAdi.Properties.Appearance.Options.UseFont = true;
            this.textEditGrupAdi.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditGrupAdi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.textEditGrupAdi.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditGrupAdi.Properties.AppearanceDisabled.Options.UseFont = true;
            this.textEditGrupAdi.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.textEditGrupAdi.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.textEditGrupAdi.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditGrupAdi.Properties.AppearanceFocused.Options.UseFont = true;
            this.textEditGrupAdi.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.textEditGrupAdi.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.textEditGrupAdi.Properties.AutoHeight = false;
            this.textEditGrupAdi.Size = new System.Drawing.Size(184, 40);
            this.textEditGrupAdi.StyleController = this.layoutControlAnaGrup;
            this.textEditGrupAdi.TabIndex = 4;
            // 
            // layoutControlGroupAnaGrup
            // 
            this.layoutControlGroupAnaGrup.CustomizationFormText = "layoutControlGroupAnaGrup";
            this.layoutControlGroupAnaGrup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupAnaGrup.GroupBordersVisible = false;
            this.layoutControlGroupAnaGrup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGrupAdi,
            this.layoutControlItemGrupNo,
            this.layoutControlItemGrupDurum,
            this.layoutControlItemGrupYeniKayit,
            this.layoutControlItemGrupKaydet,
            this.layoutControlItemGrupSil,
            this.layoutControlItemGrupKapat,
            this.layoutControlItemGrupListesi});
            this.layoutControlGroupAnaGrup.Name = "Root";
            this.layoutControlGroupAnaGrup.Size = new System.Drawing.Size(782, 553);
            this.layoutControlGroupAnaGrup.TextVisible = false;
            // 
            // layoutControlItemGrupAdi
            // 
            this.layoutControlItemGrupAdi.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemGrupAdi.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemGrupAdi.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItemGrupAdi.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItemGrupAdi.Control = this.textEditGrupAdi;
            this.layoutControlItemGrupAdi.CustomizationFormText = "ANA GRUP ADI  :";
            this.layoutControlItemGrupAdi.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGrupAdi.MaxSize = new System.Drawing.Size(0, 44);
            this.layoutControlItemGrupAdi.MinSize = new System.Drawing.Size(166, 44);
            this.layoutControlItemGrupAdi.Name = "layoutControlItemGrupAdi";
            this.layoutControlItemGrupAdi.Size = new System.Drawing.Size(381, 44);
            this.layoutControlItemGrupAdi.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemGrupAdi.Text = " ANA GRUP ADI  :";
            this.layoutControlItemGrupAdi.TextSize = new System.Drawing.Size(190, 22);
            // 
            // layoutControlItemGrupNo
            // 
            this.layoutControlItemGrupNo.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemGrupNo.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemGrupNo.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItemGrupNo.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItemGrupNo.Control = this.textEditGrupNo;
            this.layoutControlItemGrupNo.CustomizationFormText = " ANA GRUP NO  :";
            this.layoutControlItemGrupNo.Location = new System.Drawing.Point(381, 0);
            this.layoutControlItemGrupNo.MinSize = new System.Drawing.Size(166, 26);
            this.layoutControlItemGrupNo.Name = "layoutControlItemGrupNo";
            this.layoutControlItemGrupNo.Size = new System.Drawing.Size(381, 44);
            this.layoutControlItemGrupNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemGrupNo.Text = " ANA GRUP NO  :";
            this.layoutControlItemGrupNo.TextSize = new System.Drawing.Size(190, 22);
            // 
            // layoutControlItemGrupDurum
            // 
            this.layoutControlItemGrupDurum.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemGrupDurum.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemGrupDurum.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItemGrupDurum.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItemGrupDurum.Control = this.checkEditGrupDurum;
            this.layoutControlItemGrupDurum.CustomizationFormText = " ANA GRUP DURUM  :";
            this.layoutControlItemGrupDurum.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItemGrupDurum.MaxSize = new System.Drawing.Size(0, 44);
            this.layoutControlItemGrupDurum.MinSize = new System.Drawing.Size(278, 44);
            this.layoutControlItemGrupDurum.Name = "layoutControlItemGrupDurum";
            this.layoutControlItemGrupDurum.Size = new System.Drawing.Size(762, 44);
            this.layoutControlItemGrupDurum.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemGrupDurum.Text = " ANA GRUP DURUM  :";
            this.layoutControlItemGrupDurum.TextSize = new System.Drawing.Size(190, 22);
            // 
            // layoutControlItemGrupYeniKayit
            // 
            this.layoutControlItemGrupYeniKayit.Control = this.simpleButtonGrupYeniKayit;
            this.layoutControlItemGrupYeniKayit.CustomizationFormText = "layoutControlItemGrupYeniKayit";
            this.layoutControlItemGrupYeniKayit.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlItemGrupYeniKayit.Location = new System.Drawing.Point(0, 88);
            this.layoutControlItemGrupYeniKayit.MaxSize = new System.Drawing.Size(0, 49);
            this.layoutControlItemGrupYeniKayit.MinSize = new System.Drawing.Size(95, 49);
            this.layoutControlItemGrupYeniKayit.Name = "layoutControlItemGrupYeniKayit";
            this.layoutControlItemGrupYeniKayit.Size = new System.Drawing.Size(187, 49);
            this.layoutControlItemGrupYeniKayit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemGrupYeniKayit.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrupYeniKayit.TextVisible = false;
            // 
            // layoutControlItemGrupKaydet
            // 
            this.layoutControlItemGrupKaydet.Control = this.simpleButtonGrupKaydet;
            this.layoutControlItemGrupKaydet.CustomizationFormText = "layoutControlItemGrupKaydet";
            this.layoutControlItemGrupKaydet.Location = new System.Drawing.Point(187, 88);
            this.layoutControlItemGrupKaydet.MaxSize = new System.Drawing.Size(0, 49);
            this.layoutControlItemGrupKaydet.MinSize = new System.Drawing.Size(95, 49);
            this.layoutControlItemGrupKaydet.Name = "layoutControlItemGrupKaydet";
            this.layoutControlItemGrupKaydet.Size = new System.Drawing.Size(187, 49);
            this.layoutControlItemGrupKaydet.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemGrupKaydet.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrupKaydet.TextVisible = false;
            // 
            // layoutControlItemGrupSil
            // 
            this.layoutControlItemGrupSil.Control = this.simpleButtonGrupSil;
            this.layoutControlItemGrupSil.CustomizationFormText = "layoutControlItemGrupSil";
            this.layoutControlItemGrupSil.Location = new System.Drawing.Point(374, 88);
            this.layoutControlItemGrupSil.MaxSize = new System.Drawing.Size(0, 49);
            this.layoutControlItemGrupSil.MinSize = new System.Drawing.Size(95, 49);
            this.layoutControlItemGrupSil.Name = "layoutControlItemGrupSil";
            this.layoutControlItemGrupSil.Size = new System.Drawing.Size(187, 49);
            this.layoutControlItemGrupSil.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemGrupSil.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrupSil.TextVisible = false;
            // 
            // layoutControlItemGrupKapat
            // 
            this.layoutControlItemGrupKapat.Control = this.simpleButtonGrupKapat;
            this.layoutControlItemGrupKapat.CustomizationFormText = "layoutControlItemGrupKapat";
            this.layoutControlItemGrupKapat.Location = new System.Drawing.Point(561, 88);
            this.layoutControlItemGrupKapat.MaxSize = new System.Drawing.Size(0, 49);
            this.layoutControlItemGrupKapat.MinSize = new System.Drawing.Size(95, 49);
            this.layoutControlItemGrupKapat.Name = "layoutControlItemGrupKapat";
            this.layoutControlItemGrupKapat.Size = new System.Drawing.Size(201, 49);
            this.layoutControlItemGrupKapat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemGrupKapat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrupKapat.TextVisible = false;
            // 
            // layoutControlItemGrupListesi
            // 
            this.layoutControlItemGrupListesi.Control = this.gridControlGruplar;
            this.layoutControlItemGrupListesi.CustomizationFormText = "layoutControlItemGrupListesi";
            this.layoutControlItemGrupListesi.Location = new System.Drawing.Point(0, 137);
            this.layoutControlItemGrupListesi.MinSize = new System.Drawing.Size(255, 24);
            this.layoutControlItemGrupListesi.Name = "layoutControlItemGrupListesi";
            this.layoutControlItemGrupListesi.Size = new System.Drawing.Size(762, 396);
            this.layoutControlItemGrupListesi.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemGrupListesi.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrupListesi.TextVisible = false;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // frmAnaGrupEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.layoutControlAnaGrup);
            this.Name = "frmAnaGrupEkle";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ana Grup Ekle";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAnaGrupEkle_FormClosed);
            this.Load += new System.EventHandler(this.frmAnaGrupEkle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAnaGrup)).EndInit();
            this.layoutControlAnaGrup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGruplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGruplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditGrupDurum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGrupNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGrupAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAnaGrup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupAdi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupDurum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupYeniKayit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupKaydet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupSil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupKapat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupListesi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlAnaGrup;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupAnaGrup;
        private DevExpress.XtraEditors.TextEdit textEditGrupAdi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrupAdi;
        private DevExpress.XtraEditors.TextEdit textEditGrupNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrupNo;
        private DevExpress.XtraEditors.CheckEdit checkEditGrupDurum;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrupDurum;
        private DevExpress.XtraEditors.SimpleButton simpleButtonGrupKapat;
        private DevExpress.XtraEditors.SimpleButton simpleButtonGrupSil;
        private DevExpress.XtraEditors.SimpleButton simpleButtonGrupKaydet;
        private DevExpress.XtraEditors.SimpleButton simpleButtonGrupYeniKayit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrupYeniKayit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrupKaydet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrupSil;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrupKapat;
        private DevExpress.XtraGrid.GridControl gridControlGruplar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGruplar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrupListesi;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}