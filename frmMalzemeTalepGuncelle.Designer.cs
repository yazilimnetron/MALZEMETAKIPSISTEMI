
namespace MALZEMETAKIPSISTEMI
{
    partial class frmMalzemeTalepGuncelle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMalzemeTalepGuncelle));
            this.layoutControlTalep = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonKapat = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.textEditTalepNo = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemTalepNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemKaydet = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemKapat = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTalep)).BeginInit();
            this.layoutControlTalep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTalepNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTalepNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKaydet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKapat)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlTalep
            // 
            this.layoutControlTalep.Controls.Add(this.simpleButtonKapat);
            this.layoutControlTalep.Controls.Add(this.simpleButtonKaydet);
            this.layoutControlTalep.Controls.Add(this.textEditTalepNo);
            this.layoutControlTalep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlTalep.Location = new System.Drawing.Point(0, 0);
            this.layoutControlTalep.Name = "layoutControlTalep";
            this.layoutControlTalep.Root = this.Root;
            this.layoutControlTalep.Size = new System.Drawing.Size(442, 123);
            this.layoutControlTalep.TabIndex = 0;
            this.layoutControlTalep.Text = "layoutControl1";
            // 
            // simpleButtonKapat
            // 
            this.simpleButtonKapat.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButtonKapat.Appearance.Options.UseFont = true;
            this.simpleButtonKapat.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButtonKapat.AppearanceDisabled.Options.UseFont = true;
            this.simpleButtonKapat.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButtonKapat.AppearanceHovered.Options.UseFont = true;
            this.simpleButtonKapat.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButtonKapat.AppearancePressed.Options.UseFont = true;
            this.simpleButtonKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonKapat.ImageOptions.Image")));
            this.simpleButtonKapat.Location = new System.Drawing.Point(223, 63);
            this.simpleButtonKapat.Name = "simpleButtonKapat";
            this.simpleButtonKapat.Size = new System.Drawing.Size(207, 48);
            this.simpleButtonKapat.StyleController = this.layoutControlTalep;
            this.simpleButtonKapat.TabIndex = 6;
            this.simpleButtonKapat.Text = "Kapat";
            this.simpleButtonKapat.Click += new System.EventHandler(this.simpleButtonKapat_Click);
            // 
            // simpleButtonKaydet
            // 
            this.simpleButtonKaydet.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButtonKaydet.Appearance.Options.UseFont = true;
            this.simpleButtonKaydet.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButtonKaydet.AppearanceDisabled.Options.UseFont = true;
            this.simpleButtonKaydet.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButtonKaydet.AppearanceHovered.Options.UseFont = true;
            this.simpleButtonKaydet.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButtonKaydet.AppearancePressed.Options.UseFont = true;
            this.simpleButtonKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonKaydet.ImageOptions.Image")));
            this.simpleButtonKaydet.Location = new System.Drawing.Point(12, 63);
            this.simpleButtonKaydet.Name = "simpleButtonKaydet";
            this.simpleButtonKaydet.Size = new System.Drawing.Size(207, 48);
            this.simpleButtonKaydet.StyleController = this.layoutControlTalep;
            this.simpleButtonKaydet.TabIndex = 5;
            this.simpleButtonKaydet.Text = "Kaydet";
            this.simpleButtonKaydet.Click += new System.EventHandler(this.simpleButtonKaydet_Click);
            // 
            // textEditTalepNo
            // 
            this.textEditTalepNo.Location = new System.Drawing.Point(111, 12);
            this.textEditTalepNo.Name = "textEditTalepNo";
            this.textEditTalepNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textEditTalepNo.Properties.Appearance.Options.UseFont = true;
            this.textEditTalepNo.Properties.AutoHeight = false;
            this.textEditTalepNo.Size = new System.Drawing.Size(319, 47);
            this.textEditTalepNo.StyleController = this.layoutControlTalep;
            this.textEditTalepNo.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemTalepNo,
            this.layoutControlItemKaydet,
            this.layoutControlItemKapat});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(442, 123);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemTalepNo
            // 
            this.layoutControlItemTalepNo.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.layoutControlItemTalepNo.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemTalepNo.Control = this.textEditTalepNo;
            this.layoutControlItemTalepNo.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemTalepNo.MinSize = new System.Drawing.Size(174, 26);
            this.layoutControlItemTalepNo.Name = "layoutControlItemTalepNo";
            this.layoutControlItemTalepNo.Size = new System.Drawing.Size(422, 51);
            this.layoutControlItemTalepNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemTalepNo.Text = " Talep No :";
            this.layoutControlItemTalepNo.TextSize = new System.Drawing.Size(96, 22);
            // 
            // layoutControlItemKaydet
            // 
            this.layoutControlItemKaydet.Control = this.simpleButtonKaydet;
            this.layoutControlItemKaydet.Location = new System.Drawing.Point(0, 51);
            this.layoutControlItemKaydet.MinSize = new System.Drawing.Size(211, 41);
            this.layoutControlItemKaydet.Name = "layoutControlItemKaydet";
            this.layoutControlItemKaydet.Size = new System.Drawing.Size(211, 52);
            this.layoutControlItemKaydet.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemKaydet.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemKaydet.TextVisible = false;
            // 
            // layoutControlItemKapat
            // 
            this.layoutControlItemKapat.Control = this.simpleButtonKapat;
            this.layoutControlItemKapat.Location = new System.Drawing.Point(211, 51);
            this.layoutControlItemKapat.MinSize = new System.Drawing.Size(211, 41);
            this.layoutControlItemKapat.Name = "layoutControlItemKapat";
            this.layoutControlItemKapat.Size = new System.Drawing.Size(211, 52);
            this.layoutControlItemKapat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemKapat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemKapat.TextVisible = false;
            // 
            // frmMalzemeTalepGuncelle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 123);
            this.Controls.Add(this.layoutControlTalep);
            this.Name = "frmMalzemeTalepGuncelle";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Malzeme Talep No Güncelle";
            this.Load += new System.EventHandler(this.frmMalzemeTalepGuncelle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTalep)).EndInit();
            this.layoutControlTalep.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditTalepNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTalepNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKaydet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKapat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlTalep;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit textEditTalepNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTalepNo;
        private DevExpress.XtraEditors.SimpleButton simpleButtonKapat;
        private DevExpress.XtraEditors.SimpleButton simpleButtonKaydet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKaydet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKapat;
    }
}