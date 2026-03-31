namespace MALZEMETAKIPSISTEMI
{
    partial class frmAdetDuzenle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdetDuzenle));
            this.layoutControlAdetDuzenle = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonKapat = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonTamam = new DevExpress.XtraEditors.SimpleButton();
            this.textEditAdet = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroupAdetDuzenle = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemAdetDuzenle = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAdetTamam = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAdetDuzenle)).BeginInit();
            this.layoutControlAdetDuzenle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAdet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAdetDuzenle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAdetDuzenle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAdetTamam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlAdetDuzenle
            // 
            this.layoutControlAdetDuzenle.Controls.Add(this.simpleButtonKapat);
            this.layoutControlAdetDuzenle.Controls.Add(this.simpleButtonTamam);
            this.layoutControlAdetDuzenle.Controls.Add(this.textEditAdet);
            this.layoutControlAdetDuzenle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlAdetDuzenle.Location = new System.Drawing.Point(0, 0);
            this.layoutControlAdetDuzenle.Name = "layoutControlAdetDuzenle";
            this.layoutControlAdetDuzenle.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(388, 192, 250, 350);
            this.layoutControlAdetDuzenle.Root = this.layoutControlGroupAdetDuzenle;
            this.layoutControlAdetDuzenle.Size = new System.Drawing.Size(376, 62);
            this.layoutControlAdetDuzenle.TabIndex = 0;
            this.layoutControlAdetDuzenle.Text = "layoutControl1";
            // 
            // simpleButtonKapat
            // 
            this.simpleButtonKapat.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleButtonKapat.Appearance.Options.UseFont = true;
            this.simpleButtonKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonKapat.ImageOptions.Image")));
            this.simpleButtonKapat.Location = new System.Drawing.Point(249, 12);
            this.simpleButtonKapat.Name = "simpleButtonKapat";
            this.simpleButtonKapat.Size = new System.Drawing.Size(115, 38);
            this.simpleButtonKapat.StyleController = this.layoutControlAdetDuzenle;
            this.simpleButtonKapat.TabIndex = 3;
            this.simpleButtonKapat.Text = "Kapat";
            this.simpleButtonKapat.Click += new System.EventHandler(this.simpleButtonKapat_Click);
            // 
            // simpleButtonTamam
            // 
            this.simpleButtonTamam.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleButtonTamam.Appearance.Options.UseFont = true;
            this.simpleButtonTamam.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonTamam.ImageOptions.Image")));
            this.simpleButtonTamam.Location = new System.Drawing.Point(127, 12);
            this.simpleButtonTamam.Name = "simpleButtonTamam";
            this.simpleButtonTamam.Size = new System.Drawing.Size(118, 38);
            this.simpleButtonTamam.StyleController = this.layoutControlAdetDuzenle;
            this.simpleButtonTamam.TabIndex = 2;
            this.simpleButtonTamam.Text = "Tamam";
            this.simpleButtonTamam.Click += new System.EventHandler(this.simpleButtonTamam_Click);
            // 
            // textEditAdet
            // 
            this.textEditAdet.Location = new System.Drawing.Point(83, 12);
            this.textEditAdet.Name = "textEditAdet";
            this.textEditAdet.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditAdet.Properties.Appearance.Options.UseFont = true;
            this.textEditAdet.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditAdet.Properties.AppearanceDisabled.Options.UseFont = true;
            this.textEditAdet.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditAdet.Properties.AppearanceFocused.Options.UseFont = true;
            this.textEditAdet.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditAdet.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.textEditAdet.Properties.AutoHeight = false;
            this.textEditAdet.Size = new System.Drawing.Size(40, 38);
            this.textEditAdet.StyleController = this.layoutControlAdetDuzenle;
            this.textEditAdet.TabIndex = 1;
            // 
            // layoutControlGroupAdetDuzenle
            // 
            this.layoutControlGroupAdetDuzenle.CustomizationFormText = "layoutControlGroupAdetDuzenle";
            this.layoutControlGroupAdetDuzenle.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupAdetDuzenle.GroupBordersVisible = false;
            this.layoutControlGroupAdetDuzenle.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemAdetDuzenle,
            this.layoutControlItemAdetTamam,
            this.layoutControlItem1});
            this.layoutControlGroupAdetDuzenle.Name = "Root";
            this.layoutControlGroupAdetDuzenle.Size = new System.Drawing.Size(376, 62);
            this.layoutControlGroupAdetDuzenle.TextVisible = false;
            // 
            // layoutControlItemAdetDuzenle
            // 
            this.layoutControlItemAdetDuzenle.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemAdetDuzenle.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemAdetDuzenle.Control = this.textEditAdet;
            this.layoutControlItemAdetDuzenle.CustomizationFormText = "  Adet  :";
            this.layoutControlItemAdetDuzenle.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemAdetDuzenle.MinSize = new System.Drawing.Size(1, 1);
            this.layoutControlItemAdetDuzenle.Name = "layoutControlItemAdetDuzenle";
            this.layoutControlItemAdetDuzenle.Size = new System.Drawing.Size(115, 42);
            this.layoutControlItemAdetDuzenle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemAdetDuzenle.Text = "  Adet  :";
            this.layoutControlItemAdetDuzenle.TextSize = new System.Drawing.Size(68, 22);
            // 
            // layoutControlItemAdetTamam
            // 
            this.layoutControlItemAdetTamam.Control = this.simpleButtonTamam;
            this.layoutControlItemAdetTamam.CustomizationFormText = "layoutControlItemAdetTamam";
            this.layoutControlItemAdetTamam.Location = new System.Drawing.Point(115, 0);
            this.layoutControlItemAdetTamam.MinSize = new System.Drawing.Size(1, 1);
            this.layoutControlItemAdetTamam.Name = "layoutControlItemAdetTamam";
            this.layoutControlItemAdetTamam.Size = new System.Drawing.Size(122, 42);
            this.layoutControlItemAdetTamam.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemAdetTamam.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemAdetTamam.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButtonKapat;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(237, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(1, 1);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(119, 42);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // frmAdetDuzenle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 62);
            this.Controls.Add(this.layoutControlAdetDuzenle);
            this.KeyPreview = true;
            this.Name = "frmAdetDuzenle";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adet Düzenle";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAdetDuzenle_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAdetDuzenle)).EndInit();
            this.layoutControlAdetDuzenle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditAdet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAdetDuzenle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAdetDuzenle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAdetTamam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlAdetDuzenle;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupAdetDuzenle;
        private DevExpress.XtraEditors.SimpleButton simpleButtonTamam;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAdetDuzenle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAdetTamam;
        public DevExpress.XtraEditors.TextEdit textEditAdet;
        private DevExpress.XtraEditors.SimpleButton simpleButtonKapat;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}