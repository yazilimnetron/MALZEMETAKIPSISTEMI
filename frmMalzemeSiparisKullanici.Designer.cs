namespace MALZEME_TAKIP_SISTEMI
{
    partial class frmMalzemeSiparisKullanici
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMalzemeSiparisKullanici));
            this.layoutControlKullanici = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonIptal = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonTamam = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEditKullanici = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemKullanici = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTamam = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemIptal = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlKullanici)).BeginInit();
            this.layoutControlKullanici.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditKullanici.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKullanici)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTamam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIptal)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlKullanici
            // 
            this.layoutControlKullanici.Controls.Add(this.simpleButtonIptal);
            this.layoutControlKullanici.Controls.Add(this.simpleButtonTamam);
            this.layoutControlKullanici.Controls.Add(this.comboBoxEditKullanici);
            this.layoutControlKullanici.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlKullanici.Location = new System.Drawing.Point(0, 0);
            this.layoutControlKullanici.Name = "layoutControlKullanici";
            this.layoutControlKullanici.Root = this.Root;
            this.layoutControlKullanici.Size = new System.Drawing.Size(531, 114);
            this.layoutControlKullanici.TabIndex = 0;
            this.layoutControlKullanici.Text = "layoutControl1";
            // 
            // simpleButtonIptal
            // 
            this.simpleButtonIptal.Appearance.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.simpleButtonIptal.Appearance.Options.UseFont = true;
            this.simpleButtonIptal.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonIptal.ImageOptions.Image")));
            this.simpleButtonIptal.Location = new System.Drawing.Point(267, 64);
            this.simpleButtonIptal.Name = "simpleButtonIptal";
            this.simpleButtonIptal.Size = new System.Drawing.Size(252, 38);
            this.simpleButtonIptal.StyleController = this.layoutControlKullanici;
            this.simpleButtonIptal.TabIndex = 6;
            this.simpleButtonIptal.Text = "İptal";
            this.simpleButtonIptal.Click += new System.EventHandler(this.simpleButtonIptal_Click);
            // 
            // simpleButtonTamam
            // 
            this.simpleButtonTamam.Appearance.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.simpleButtonTamam.Appearance.Options.UseFont = true;
            this.simpleButtonTamam.AppearanceDisabled.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.simpleButtonTamam.AppearanceDisabled.Options.UseFont = true;
            this.simpleButtonTamam.AppearanceHovered.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.simpleButtonTamam.AppearanceHovered.Options.UseFont = true;
            this.simpleButtonTamam.AppearancePressed.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.simpleButtonTamam.AppearancePressed.Options.UseFont = true;
            this.simpleButtonTamam.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonTamam.ImageOptions.Image")));
            this.simpleButtonTamam.Location = new System.Drawing.Point(12, 64);
            this.simpleButtonTamam.Name = "simpleButtonTamam";
            this.simpleButtonTamam.Size = new System.Drawing.Size(251, 38);
            this.simpleButtonTamam.StyleController = this.layoutControlKullanici;
            this.simpleButtonTamam.TabIndex = 5;
            this.simpleButtonTamam.Text = "Tamam";
            this.simpleButtonTamam.Click += new System.EventHandler(this.simpleButtonTamam_Click);
            // 
            // comboBoxEditKullanici
            // 
            this.comboBoxEditKullanici.Location = new System.Drawing.Point(253, 12);
            this.comboBoxEditKullanici.Name = "comboBoxEditKullanici";
            this.comboBoxEditKullanici.Properties.Appearance.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditKullanici.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditKullanici.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditKullanici.Properties.AppearanceDisabled.Options.UseFont = true;
            this.comboBoxEditKullanici.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditKullanici.Properties.AppearanceDropDown.Options.UseFont = true;
            this.comboBoxEditKullanici.Properties.AppearanceFocused.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditKullanici.Properties.AppearanceFocused.Options.UseFont = true;
            this.comboBoxEditKullanici.Properties.AppearanceItemDisabled.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditKullanici.Properties.AppearanceItemDisabled.Options.UseFont = true;
            this.comboBoxEditKullanici.Properties.AppearanceItemHighlight.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditKullanici.Properties.AppearanceItemHighlight.Options.UseFont = true;
            this.comboBoxEditKullanici.Properties.AppearanceItemSelected.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditKullanici.Properties.AppearanceItemSelected.Options.UseFont = true;
            this.comboBoxEditKullanici.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditKullanici.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.comboBoxEditKullanici.Properties.AutoHeight = false;
            this.comboBoxEditKullanici.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditKullanici.Size = new System.Drawing.Size(266, 48);
            this.comboBoxEditKullanici.StyleController = this.layoutControlKullanici;
            this.comboBoxEditKullanici.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemKullanici,
            this.layoutControlItemTamam,
            this.layoutControlItemIptal});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(531, 114);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemKullanici
            // 
            this.layoutControlItemKullanici.AppearanceItemCaption.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemKullanici.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemKullanici.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItemKullanici.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItemKullanici.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItemKullanici.Control = this.comboBoxEditKullanici;
            this.layoutControlItemKullanici.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemKullanici.MinSize = new System.Drawing.Size(154, 26);
            this.layoutControlItemKullanici.Name = "layoutControlItemKullanici";
            this.layoutControlItemKullanici.Size = new System.Drawing.Size(511, 52);
            this.layoutControlItemKullanici.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemKullanici.Text = " Kullanıcı Adı  :";
            this.layoutControlItemKullanici.TextSize = new System.Drawing.Size(238, 27);
            // 
            // layoutControlItemTamam
            // 
            this.layoutControlItemTamam.Control = this.simpleButtonTamam;
            this.layoutControlItemTamam.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItemTamam.Name = "layoutControlItemTamam";
            this.layoutControlItemTamam.Size = new System.Drawing.Size(255, 42);
            this.layoutControlItemTamam.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTamam.TextVisible = false;
            // 
            // layoutControlItemIptal
            // 
            this.layoutControlItemIptal.Control = this.simpleButtonIptal;
            this.layoutControlItemIptal.Location = new System.Drawing.Point(255, 52);
            this.layoutControlItemIptal.Name = "layoutControlItemIptal";
            this.layoutControlItemIptal.Size = new System.Drawing.Size(256, 42);
            this.layoutControlItemIptal.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemIptal.TextVisible = false;
            // 
            // frmMalzemeSiparisKullanici
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 114);
            this.Controls.Add(this.layoutControlKullanici);
            this.Name = "frmMalzemeSiparisKullanici";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kullanıcı Seçiniz ...";
            this.Load += new System.EventHandler(this.frmMalzemeSiparisKullanici_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlKullanici)).EndInit();
            this.layoutControlKullanici.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditKullanici.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKullanici)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTamam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIptal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlKullanici;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton simpleButtonIptal;
        private DevExpress.XtraEditors.SimpleButton simpleButtonTamam;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditKullanici;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKullanici;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTamam;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIptal;
    }
}