namespace MALZEME_TAKIP_SISTEMI
{
    partial class frmMalzemeStokDurumlari
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMalzemeStokDurumlari));
            this.layoutControlStokDurumlari = new DevExpress.XtraLayout.LayoutControl();
            this.checkEditStokGoster = new DevExpress.XtraEditors.CheckEdit();
            this.gridControlStokDurum = new DevExpress.XtraGrid.GridControl();
            this.gridViewStokDurum = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButtonYazdir = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonListele = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupStokDurumlari = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemListele = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemYazdır = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemStokDurum = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlStokDurumlari)).BeginInit();
            this.layoutControlStokDurumlari.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditStokGoster.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStokDurum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStokDurum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupStokDurumlari)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemListele)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemYazdır)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStokDurum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlStokDurumlari
            // 
            this.layoutControlStokDurumlari.Controls.Add(this.checkEditStokGoster);
            this.layoutControlStokDurumlari.Controls.Add(this.gridControlStokDurum);
            this.layoutControlStokDurumlari.Controls.Add(this.simpleButtonYazdir);
            this.layoutControlStokDurumlari.Controls.Add(this.simpleButtonListele);
            this.layoutControlStokDurumlari.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlStokDurumlari.Location = new System.Drawing.Point(0, 0);
            this.layoutControlStokDurumlari.Name = "layoutControlStokDurumlari";
            this.layoutControlStokDurumlari.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(700, 352, 250, 350);
            this.layoutControlStokDurumlari.Root = this.layoutControlGroupStokDurumlari;
            this.layoutControlStokDurumlari.Size = new System.Drawing.Size(882, 510);
            this.layoutControlStokDurumlari.TabIndex = 0;
            this.layoutControlStokDurumlari.Text = "layoutControl1";
            // 
            // checkEditStokGoster
            // 
            this.checkEditStokGoster.Location = new System.Drawing.Point(12, 12);
            this.checkEditStokGoster.Name = "checkEditStokGoster";
            this.checkEditStokGoster.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkEditStokGoster.Properties.Appearance.Options.UseFont = true;
            this.checkEditStokGoster.Properties.AutoHeight = false;
            this.checkEditStokGoster.Properties.Caption = "Stok Sayısı Sıfır Olanları Dahil Et";
            this.checkEditStokGoster.Size = new System.Drawing.Size(162, 35);
            this.checkEditStokGoster.StyleController = this.layoutControlStokDurumlari;
            this.checkEditStokGoster.TabIndex = 9;
            // 
            // gridControlStokDurum
            // 
            this.gridControlStokDurum.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControlStokDurum.Location = new System.Drawing.Point(12, 51);
            this.gridControlStokDurum.MainView = this.gridViewStokDurum;
            this.gridControlStokDurum.Name = "gridControlStokDurum";
            this.gridControlStokDurum.Size = new System.Drawing.Size(858, 447);
            this.gridControlStokDurum.TabIndex = 8;
            this.gridControlStokDurum.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewStokDurum});
            // 
            // gridViewStokDurum
            // 
            this.gridViewStokDurum.GridControl = this.gridControlStokDurum;
            this.gridViewStokDurum.Name = "gridViewStokDurum";
            this.gridViewStokDurum.OptionsBehavior.Editable = false;
            this.gridViewStokDurum.OptionsBehavior.ReadOnly = true;
            this.gridViewStokDurum.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewStokDurum.OptionsView.ColumnAutoWidth = false;
            this.gridViewStokDurum.OptionsView.ShowAutoFilterRow = true;
            this.gridViewStokDurum.OptionsView.ShowFooter = true;
            this.gridViewStokDurum.OptionsView.ShowGroupPanel = false;
            // 
            // simpleButtonYazdir
            // 
            this.simpleButtonYazdir.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.simpleButtonYazdir.Appearance.Options.UseFont = true;
            this.simpleButtonYazdir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonYazdir.ImageOptions.Image")));
            this.simpleButtonYazdir.Location = new System.Drawing.Point(493, 12);
            this.simpleButtonYazdir.Name = "simpleButtonYazdir";
            this.simpleButtonYazdir.Size = new System.Drawing.Size(377, 35);
            this.simpleButtonYazdir.StyleController = this.layoutControlStokDurumlari;
            this.simpleButtonYazdir.TabIndex = 7;
            this.simpleButtonYazdir.Text = "Yazdır";
            this.simpleButtonYazdir.Click += new System.EventHandler(this.simpleButtonYazdir_Click);
            // 
            // simpleButtonListele
            // 
            this.simpleButtonListele.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.simpleButtonListele.Appearance.Options.UseFont = true;
            this.simpleButtonListele.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonListele.ImageOptions.Image")));
            this.simpleButtonListele.Location = new System.Drawing.Point(178, 12);
            this.simpleButtonListele.Name = "simpleButtonListele";
            this.simpleButtonListele.Size = new System.Drawing.Size(311, 35);
            this.simpleButtonListele.StyleController = this.layoutControlStokDurumlari;
            this.simpleButtonListele.TabIndex = 6;
            this.simpleButtonListele.Text = "Listele";
            this.simpleButtonListele.Click += new System.EventHandler(this.simpleButtonListele_Click);
            // 
            // layoutControlGroupStokDurumlari
            // 
            this.layoutControlGroupStokDurumlari.CustomizationFormText = "layoutControlGroupStokDurumlari";
            this.layoutControlGroupStokDurumlari.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupStokDurumlari.GroupBordersVisible = false;
            this.layoutControlGroupStokDurumlari.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemListele,
            this.layoutControlItemYazdır,
            this.layoutControlItemStokDurum,
            this.layoutControlItem1});
            this.layoutControlGroupStokDurumlari.Name = "layoutControlGroupStokDurumlari";
            this.layoutControlGroupStokDurumlari.Size = new System.Drawing.Size(882, 510);
            this.layoutControlGroupStokDurumlari.TextVisible = false;
            // 
            // layoutControlItemListele
            // 
            this.layoutControlItemListele.Control = this.simpleButtonListele;
            this.layoutControlItemListele.CustomizationFormText = "layoutControlItemListele";
            this.layoutControlItemListele.Location = new System.Drawing.Point(166, 0);
            this.layoutControlItemListele.MaxSize = new System.Drawing.Size(0, 39);
            this.layoutControlItemListele.MinSize = new System.Drawing.Size(106, 39);
            this.layoutControlItemListele.Name = "layoutControlItemListele";
            this.layoutControlItemListele.Size = new System.Drawing.Size(315, 39);
            this.layoutControlItemListele.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemListele.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemListele.TextVisible = false;
            // 
            // layoutControlItemYazdır
            // 
            this.layoutControlItemYazdır.Control = this.simpleButtonYazdir;
            this.layoutControlItemYazdır.CustomizationFormText = "layoutControlItemYazdır";
            this.layoutControlItemYazdır.Location = new System.Drawing.Point(481, 0);
            this.layoutControlItemYazdır.MaxSize = new System.Drawing.Size(0, 39);
            this.layoutControlItemYazdır.MinSize = new System.Drawing.Size(103, 39);
            this.layoutControlItemYazdır.Name = "layoutControlItemYazdır";
            this.layoutControlItemYazdır.Size = new System.Drawing.Size(381, 39);
            this.layoutControlItemYazdır.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemYazdır.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemYazdır.TextVisible = false;
            // 
            // layoutControlItemStokDurum
            // 
            this.layoutControlItemStokDurum.Control = this.gridControlStokDurum;
            this.layoutControlItemStokDurum.CustomizationFormText = "layoutControlItemStokDurum";
            this.layoutControlItemStokDurum.Location = new System.Drawing.Point(0, 39);
            this.layoutControlItemStokDurum.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItemStokDurum.Name = "layoutControlItemStokDurum";
            this.layoutControlItemStokDurum.Size = new System.Drawing.Size(862, 451);
            this.layoutControlItemStokDurum.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemStokDurum.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemStokDurum.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.checkEditStokGoster;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(92, 28);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(166, 39);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmMalzemeStokDurumlari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 510);
            this.Controls.Add(this.layoutControlStokDurumlari);
            this.Name = "frmMalzemeStokDurumlari";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stok Durumları";
            this.Load += new System.EventHandler(this.frmMalzemeStokDurumlari_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlStokDurumlari)).EndInit();
            this.layoutControlStokDurumlari.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditStokGoster.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStokDurum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStokDurum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupStokDurumlari)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemListele)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemYazdır)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStokDurum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlStokDurumlari;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupStokDurumlari;
        private DevExpress.XtraGrid.GridControl gridControlStokDurum;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewStokDurum;
        private DevExpress.XtraEditors.SimpleButton simpleButtonYazdir;
        private DevExpress.XtraEditors.SimpleButton simpleButtonListele;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemListele;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemYazdır;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStokDurum;
        private DevExpress.XtraEditors.CheckEdit checkEditStokGoster;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}