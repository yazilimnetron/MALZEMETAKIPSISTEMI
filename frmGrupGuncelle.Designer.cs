
namespace MALZEMETAKIPSISTEMI
{
    partial class frmGrupGuncelle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGrupGuncelle));
            this.layoutControlGrup = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.comboBoxEditGrupAdi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItemGrupAdi = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItemKaydet = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonKapat = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItemKapat = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGrup)).BeginInit();
            this.layoutControlGrup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditGrupAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupAdi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKaydet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKapat)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGrup
            // 
            this.layoutControlGrup.Controls.Add(this.simpleButtonKapat);
            this.layoutControlGrup.Controls.Add(this.simpleButtonKaydet);
            this.layoutControlGrup.Controls.Add(this.comboBoxEditGrupAdi);
            this.layoutControlGrup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlGrup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGrup.Name = "layoutControlGrup";
            this.layoutControlGrup.Root = this.Root;
            this.layoutControlGrup.Size = new System.Drawing.Size(508, 123);
            this.layoutControlGrup.TabIndex = 0;
            this.layoutControlGrup.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGrupAdi,
            this.layoutControlItemKaydet,
            this.layoutControlItemKapat});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(508, 123);
            this.Root.TextVisible = false;
            // 
            // comboBoxEditGrupAdi
            // 
            this.comboBoxEditGrupAdi.Location = new System.Drawing.Point(105, 12);
            this.comboBoxEditGrupAdi.Name = "comboBoxEditGrupAdi";
            this.comboBoxEditGrupAdi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditGrupAdi.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditGrupAdi.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditGrupAdi.Properties.AppearanceDisabled.Options.UseFont = true;
            this.comboBoxEditGrupAdi.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditGrupAdi.Properties.AppearanceDropDown.Options.UseFont = true;
            this.comboBoxEditGrupAdi.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditGrupAdi.Properties.AppearanceFocused.Options.UseFont = true;
            this.comboBoxEditGrupAdi.Properties.AppearanceItemDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditGrupAdi.Properties.AppearanceItemDisabled.Options.UseFont = true;
            this.comboBoxEditGrupAdi.Properties.AppearanceItemHighlight.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditGrupAdi.Properties.AppearanceItemHighlight.Options.UseFont = true;
            this.comboBoxEditGrupAdi.Properties.AppearanceItemSelected.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditGrupAdi.Properties.AppearanceItemSelected.Options.UseFont = true;
            this.comboBoxEditGrupAdi.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.comboBoxEditGrupAdi.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.comboBoxEditGrupAdi.Properties.AutoHeight = false;
            this.comboBoxEditGrupAdi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditGrupAdi.Size = new System.Drawing.Size(391, 45);
            this.comboBoxEditGrupAdi.StyleController = this.layoutControlGrup;
            this.comboBoxEditGrupAdi.TabIndex = 4;
            // 
            // layoutControlItemGrupAdi
            // 
            this.layoutControlItemGrupAdi.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemGrupAdi.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemGrupAdi.Control = this.comboBoxEditGrupAdi;
            this.layoutControlItemGrupAdi.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGrupAdi.MinSize = new System.Drawing.Size(159, 26);
            this.layoutControlItemGrupAdi.Name = "layoutControlItemGrupAdi";
            this.layoutControlItemGrupAdi.Size = new System.Drawing.Size(488, 49);
            this.layoutControlItemGrupAdi.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemGrupAdi.Text = " Grup Adi  :";
            this.layoutControlItemGrupAdi.TextSize = new System.Drawing.Size(81, 18);
            // 
            // simpleButtonKaydet
            // 
            this.simpleButtonKaydet.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.simpleButtonKaydet.Appearance.Options.UseFont = true;
            this.simpleButtonKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonKaydet.ImageOptions.Image")));
            this.simpleButtonKaydet.Location = new System.Drawing.Point(12, 61);
            this.simpleButtonKaydet.Name = "simpleButtonKaydet";
            this.simpleButtonKaydet.Size = new System.Drawing.Size(241, 50);
            this.simpleButtonKaydet.StyleController = this.layoutControlGrup;
            this.simpleButtonKaydet.TabIndex = 5;
            this.simpleButtonKaydet.Text = "Kaydet";
            this.simpleButtonKaydet.Click += new System.EventHandler(this.simpleButtonKaydet_Click);
            // 
            // layoutControlItemKaydet
            // 
            this.layoutControlItemKaydet.Control = this.simpleButtonKaydet;
            this.layoutControlItemKaydet.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItemKaydet.MinSize = new System.Drawing.Size(91, 31);
            this.layoutControlItemKaydet.Name = "layoutControlItemKaydet";
            this.layoutControlItemKaydet.Size = new System.Drawing.Size(245, 54);
            this.layoutControlItemKaydet.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemKaydet.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemKaydet.TextVisible = false;
            // 
            // simpleButtonKapat
            // 
            this.simpleButtonKapat.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.simpleButtonKapat.Appearance.Options.UseFont = true;
            this.simpleButtonKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonKapat.ImageOptions.Image")));
            this.simpleButtonKapat.Location = new System.Drawing.Point(257, 61);
            this.simpleButtonKapat.Name = "simpleButtonKapat";
            this.simpleButtonKapat.Size = new System.Drawing.Size(239, 50);
            this.simpleButtonKapat.StyleController = this.layoutControlGrup;
            this.simpleButtonKapat.TabIndex = 6;
            this.simpleButtonKapat.Text = "Kapat";
            this.simpleButtonKapat.Click += new System.EventHandler(this.simpleButtonKapat_Click);
            // 
            // layoutControlItemKapat
            // 
            this.layoutControlItemKapat.Control = this.simpleButtonKapat;
            this.layoutControlItemKapat.Location = new System.Drawing.Point(245, 49);
            this.layoutControlItemKapat.MinSize = new System.Drawing.Size(91, 31);
            this.layoutControlItemKapat.Name = "layoutControlItemKapat";
            this.layoutControlItemKapat.Size = new System.Drawing.Size(243, 54);
            this.layoutControlItemKapat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemKapat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemKapat.TextVisible = false;
            // 
            // frmGrupGuncelle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 123);
            this.Controls.Add(this.layoutControlGrup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmGrupGuncelle";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Malzeme Grup Güncelle";
            this.Load += new System.EventHandler(this.frmGrupGuncelle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGrup)).EndInit();
            this.layoutControlGrup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditGrupAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrupAdi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKaydet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKapat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlGrup;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton simpleButtonKapat;
        private DevExpress.XtraEditors.SimpleButton simpleButtonKaydet;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditGrupAdi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrupAdi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKaydet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKapat;
    }
}