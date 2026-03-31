
namespace MALZEMETAKIPSISTEMI
{
    partial class frmMalzemeNotlarYeni
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMalzemeNotlarYeni));
            this.layoutControlYeniNot = new DevExpress.XtraLayout.LayoutControl();
            this.textEditNotID = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonKapat = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.richEditControlNotDetay = new DevExpress.XtraRichEdit.RichEditControl();
            this.textEditNotBaslik = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemNotDetay = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBaslik = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemKaydet = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemKapat = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemNotID = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlYeniNot)).BeginInit();
            this.layoutControlYeniNot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNotID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNotBaslik.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNotDetay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBaslik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKaydet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKapat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNotID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlYeniNot
            // 
            this.layoutControlYeniNot.Controls.Add(this.textEditNotID);
            this.layoutControlYeniNot.Controls.Add(this.simpleButtonKapat);
            this.layoutControlYeniNot.Controls.Add(this.simpleButtonKaydet);
            this.layoutControlYeniNot.Controls.Add(this.richEditControlNotDetay);
            this.layoutControlYeniNot.Controls.Add(this.textEditNotBaslik);
            this.layoutControlYeniNot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlYeniNot.Location = new System.Drawing.Point(0, 0);
            this.layoutControlYeniNot.Name = "layoutControlYeniNot";
            this.layoutControlYeniNot.Root = this.Root;
            this.layoutControlYeniNot.Size = new System.Drawing.Size(800, 450);
            this.layoutControlYeniNot.TabIndex = 0;
            this.layoutControlYeniNot.Text = "layoutControl1";
            // 
            // textEditNotID
            // 
            this.textEditNotID.Location = new System.Drawing.Point(122, 12);
            this.textEditNotID.Name = "textEditNotID";
            this.textEditNotID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textEditNotID.Properties.Appearance.Options.UseFont = true;
            this.textEditNotID.Properties.AutoHeight = false;
            this.textEditNotID.Size = new System.Drawing.Size(666, 38);
            this.textEditNotID.StyleController = this.layoutControlYeniNot;
            this.textEditNotID.TabIndex = 8;
            // 
            // simpleButtonKapat
            // 
            this.simpleButtonKapat.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleButtonKapat.Appearance.Options.UseFont = true;
            this.simpleButtonKapat.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleButtonKapat.AppearancePressed.Options.UseFont = true;
            this.simpleButtonKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonKapat.ImageOptions.Image")));
            this.simpleButtonKapat.Location = new System.Drawing.Point(402, 401);
            this.simpleButtonKapat.Name = "simpleButtonKapat";
            this.simpleButtonKapat.Size = new System.Drawing.Size(386, 37);
            this.simpleButtonKapat.StyleController = this.layoutControlYeniNot;
            this.simpleButtonKapat.TabIndex = 7;
            this.simpleButtonKapat.Text = "Kapat";
            this.simpleButtonKapat.Click += new System.EventHandler(this.simpleButtonKapat_Click);
            // 
            // simpleButtonKaydet
            // 
            this.simpleButtonKaydet.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleButtonKaydet.Appearance.Options.UseFont = true;
            this.simpleButtonKaydet.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleButtonKaydet.AppearancePressed.Options.UseFont = true;
            this.simpleButtonKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonKaydet.ImageOptions.Image")));
            this.simpleButtonKaydet.Location = new System.Drawing.Point(12, 401);
            this.simpleButtonKaydet.Name = "simpleButtonKaydet";
            this.simpleButtonKaydet.Size = new System.Drawing.Size(386, 37);
            this.simpleButtonKaydet.StyleController = this.layoutControlYeniNot;
            this.simpleButtonKaydet.TabIndex = 6;
            this.simpleButtonKaydet.Text = "Kaydet";
            this.simpleButtonKaydet.Click += new System.EventHandler(this.simpleButtonKaydet_Click);
            // 
            // richEditControlNotDetay
            // 
            this.richEditControlNotDetay.Appearance.Text.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.richEditControlNotDetay.Appearance.Text.Options.UseFont = true;
            this.richEditControlNotDetay.Location = new System.Drawing.Point(122, 95);
            this.richEditControlNotDetay.Name = "richEditControlNotDetay";
            this.richEditControlNotDetay.Size = new System.Drawing.Size(666, 302);
            this.richEditControlNotDetay.TabIndex = 5;
            // 
            // textEditNotBaslik
            // 
            this.textEditNotBaslik.Location = new System.Drawing.Point(122, 54);
            this.textEditNotBaslik.Name = "textEditNotBaslik";
            this.textEditNotBaslik.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditNotBaslik.Properties.Appearance.Options.UseFont = true;
            this.textEditNotBaslik.Properties.AutoHeight = false;
            this.textEditNotBaslik.Size = new System.Drawing.Size(666, 37);
            this.textEditNotBaslik.StyleController = this.layoutControlYeniNot;
            this.textEditNotBaslik.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemNotDetay,
            this.layoutControlItemBaslik,
            this.layoutControlItemKaydet,
            this.layoutControlItemKapat,
            this.layoutControlItemNotID});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(800, 450);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemNotDetay
            // 
            this.layoutControlItemNotDetay.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemNotDetay.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemNotDetay.Control = this.richEditControlNotDetay;
            this.layoutControlItemNotDetay.Location = new System.Drawing.Point(0, 83);
            this.layoutControlItemNotDetay.MinSize = new System.Drawing.Size(224, 24);
            this.layoutControlItemNotDetay.Name = "layoutControlItemNotDetay";
            this.layoutControlItemNotDetay.Size = new System.Drawing.Size(780, 306);
            this.layoutControlItemNotDetay.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemNotDetay.Text = " Not Detay :";
            this.layoutControlItemNotDetay.TextSize = new System.Drawing.Size(107, 22);
            // 
            // layoutControlItemBaslik
            // 
            this.layoutControlItemBaslik.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemBaslik.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemBaslik.Control = this.textEditNotBaslik;
            this.layoutControlItemBaslik.Location = new System.Drawing.Point(0, 42);
            this.layoutControlItemBaslik.MinSize = new System.Drawing.Size(186, 41);
            this.layoutControlItemBaslik.Name = "layoutControlItemBaslik";
            this.layoutControlItemBaslik.Size = new System.Drawing.Size(780, 41);
            this.layoutControlItemBaslik.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBaslik.Text = " Not Başlık :";
            this.layoutControlItemBaslik.TextSize = new System.Drawing.Size(107, 22);
            // 
            // layoutControlItemKaydet
            // 
            this.layoutControlItemKaydet.Control = this.simpleButtonKaydet;
            this.layoutControlItemKaydet.Location = new System.Drawing.Point(0, 389);
            this.layoutControlItemKaydet.MinSize = new System.Drawing.Size(211, 41);
            this.layoutControlItemKaydet.Name = "layoutControlItemKaydet";
            this.layoutControlItemKaydet.Size = new System.Drawing.Size(390, 41);
            this.layoutControlItemKaydet.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemKaydet.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemKaydet.TextVisible = false;
            // 
            // layoutControlItemKapat
            // 
            this.layoutControlItemKapat.Control = this.simpleButtonKapat;
            this.layoutControlItemKapat.Location = new System.Drawing.Point(390, 389);
            this.layoutControlItemKapat.MinSize = new System.Drawing.Size(211, 41);
            this.layoutControlItemKapat.Name = "layoutControlItemKapat";
            this.layoutControlItemKapat.Size = new System.Drawing.Size(390, 41);
            this.layoutControlItemKapat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemKapat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemKapat.TextVisible = false;
            // 
            // layoutControlItemNotID
            // 
            this.layoutControlItemNotID.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.layoutControlItemNotID.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemNotID.Control = this.textEditNotID;
            this.layoutControlItemNotID.Enabled = false;
            this.layoutControlItemNotID.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemNotID.MinSize = new System.Drawing.Size(185, 31);
            this.layoutControlItemNotID.Name = "layoutControlItemNotID";
            this.layoutControlItemNotID.Size = new System.Drawing.Size(780, 42);
            this.layoutControlItemNotID.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemNotID.Text = " Not ID:";
            this.layoutControlItemNotID.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemNotID.TextSize = new System.Drawing.Size(107, 22);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // frmMalzemeNotlarYeni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.layoutControlYeniNot);
            this.Name = "frmMalzemeNotlarYeni";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yeni Not Ekle";
            this.Load += new System.EventHandler(this.frmMalzemeNotlarYeni_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlYeniNot)).EndInit();
            this.layoutControlYeniNot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditNotID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNotBaslik.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNotDetay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBaslik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKaydet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKapat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNotID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlYeniNot;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit textEditNotBaslik;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBaslik;
        private DevExpress.XtraRichEdit.RichEditControl richEditControlNotDetay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemNotDetay;
        private DevExpress.XtraEditors.SimpleButton simpleButtonKapat;
        private DevExpress.XtraEditors.SimpleButton simpleButtonKaydet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKaydet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKapat;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.TextEdit textEditNotID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemNotID;
    }
}