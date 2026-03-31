
namespace MALZEMETAKIPSISTEMI
{
    partial class frmMalzemeNotlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMalzemeNotlar));
            this.layoutControlNotlar = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlNotListe = new DevExpress.XtraGrid.GridControl();
            this.gridViewNotListe = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.richEditControlNotlar = new DevExpress.XtraRichEdit.RichEditControl();
            this.simpleButtonKapat = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonSil = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemKaydet = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSil = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemKapat = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemNotlar = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemNotListe = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlNotlar)).BeginInit();
            this.layoutControlNotlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNotListe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNotListe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKaydet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKapat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNotlar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNotListe)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlNotlar
            // 
            this.layoutControlNotlar.Controls.Add(this.gridControlNotListe);
            this.layoutControlNotlar.Controls.Add(this.richEditControlNotlar);
            this.layoutControlNotlar.Controls.Add(this.simpleButtonKapat);
            this.layoutControlNotlar.Controls.Add(this.simpleButtonSil);
            this.layoutControlNotlar.Controls.Add(this.simpleButtonKaydet);
            this.layoutControlNotlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlNotlar.Location = new System.Drawing.Point(0, 0);
            this.layoutControlNotlar.Name = "layoutControlNotlar";
            this.layoutControlNotlar.Root = this.Root;
            this.layoutControlNotlar.Size = new System.Drawing.Size(782, 553);
            this.layoutControlNotlar.TabIndex = 0;
            this.layoutControlNotlar.Text = "layoutControl1";
            // 
            // gridControlNotListe
            // 
            this.gridControlNotListe.Location = new System.Drawing.Point(12, 12);
            this.gridControlNotListe.MainView = this.gridViewNotListe;
            this.gridControlNotListe.Name = "gridControlNotListe";
            this.gridControlNotListe.Size = new System.Drawing.Size(294, 489);
            this.gridControlNotListe.TabIndex = 9;
            this.gridControlNotListe.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNotListe});
            // 
            // gridViewNotListe
            // 
            this.gridViewNotListe.GridControl = this.gridControlNotListe;
            this.gridViewNotListe.Name = "gridViewNotListe";
            this.gridViewNotListe.OptionsBehavior.Editable = false;
            this.gridViewNotListe.OptionsBehavior.ReadOnly = true;
            this.gridViewNotListe.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewNotListe.OptionsPrint.AutoWidth = false;
            this.gridViewNotListe.OptionsView.ShowAutoFilterRow = true;
            this.gridViewNotListe.OptionsView.ShowFooter = true;
            this.gridViewNotListe.OptionsView.ShowGroupPanel = false;
            this.gridViewNotListe.Click += new System.EventHandler(this.gridViewNotListe_Click);
            this.gridViewNotListe.DoubleClick += new System.EventHandler(this.gridViewNotListe_DoubleClick);
            // 
            // richEditControlNotlar
            // 
            this.richEditControlNotlar.Appearance.Text.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.richEditControlNotlar.Appearance.Text.Options.UseFont = true;
            this.richEditControlNotlar.Enabled = false;
            this.richEditControlNotlar.Location = new System.Drawing.Point(310, 12);
            this.richEditControlNotlar.Name = "richEditControlNotlar";
            this.richEditControlNotlar.Size = new System.Drawing.Size(460, 489);
            this.richEditControlNotlar.TabIndex = 8;
            // 
            // simpleButtonKapat
            // 
            this.simpleButtonKapat.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButtonKapat.Appearance.Options.UseFont = true;
            this.simpleButtonKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonKapat.ImageOptions.Image")));
            this.simpleButtonKapat.Location = new System.Drawing.Point(522, 505);
            this.simpleButtonKapat.Name = "simpleButtonKapat";
            this.simpleButtonKapat.Size = new System.Drawing.Size(248, 36);
            this.simpleButtonKapat.StyleController = this.layoutControlNotlar;
            this.simpleButtonKapat.TabIndex = 7;
            this.simpleButtonKapat.Text = "Kapat";
            this.simpleButtonKapat.Click += new System.EventHandler(this.simpleButtonKapat_Click);
            // 
            // simpleButtonSil
            // 
            this.simpleButtonSil.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButtonSil.Appearance.Options.UseFont = true;
            this.simpleButtonSil.Enabled = false;
            this.simpleButtonSil.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonSil.ImageOptions.Image")));
            this.simpleButtonSil.Location = new System.Drawing.Point(267, 505);
            this.simpleButtonSil.Name = "simpleButtonSil";
            this.simpleButtonSil.Size = new System.Drawing.Size(251, 36);
            this.simpleButtonSil.StyleController = this.layoutControlNotlar;
            this.simpleButtonSil.TabIndex = 6;
            this.simpleButtonSil.Text = "Sil";
            this.simpleButtonSil.Click += new System.EventHandler(this.simpleButtonSil_Click);
            // 
            // simpleButtonKaydet
            // 
            this.simpleButtonKaydet.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButtonKaydet.Appearance.Options.UseFont = true;
            this.simpleButtonKaydet.Enabled = false;
            this.simpleButtonKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonKaydet.ImageOptions.Image")));
            this.simpleButtonKaydet.Location = new System.Drawing.Point(12, 505);
            this.simpleButtonKaydet.Name = "simpleButtonKaydet";
            this.simpleButtonKaydet.Size = new System.Drawing.Size(251, 36);
            this.simpleButtonKaydet.StyleController = this.layoutControlNotlar;
            this.simpleButtonKaydet.TabIndex = 5;
            this.simpleButtonKaydet.Text = "Yeni Kayıt";
            this.simpleButtonKaydet.Click += new System.EventHandler(this.simpleButtonKaydet_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemKaydet,
            this.layoutControlItemSil,
            this.layoutControlItemKapat,
            this.layoutControlItemNotlar,
            this.layoutControlItemNotListe});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(782, 553);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemKaydet
            // 
            this.layoutControlItemKaydet.Control = this.simpleButtonKaydet;
            this.layoutControlItemKaydet.Location = new System.Drawing.Point(0, 493);
            this.layoutControlItemKaydet.Name = "layoutControlItemKaydet";
            this.layoutControlItemKaydet.Size = new System.Drawing.Size(255, 40);
            this.layoutControlItemKaydet.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemKaydet.TextVisible = false;
            // 
            // layoutControlItemSil
            // 
            this.layoutControlItemSil.Control = this.simpleButtonSil;
            this.layoutControlItemSil.Location = new System.Drawing.Point(255, 493);
            this.layoutControlItemSil.Name = "layoutControlItemSil";
            this.layoutControlItemSil.Size = new System.Drawing.Size(255, 40);
            this.layoutControlItemSil.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSil.TextVisible = false;
            // 
            // layoutControlItemKapat
            // 
            this.layoutControlItemKapat.Control = this.simpleButtonKapat;
            this.layoutControlItemKapat.Location = new System.Drawing.Point(510, 493);
            this.layoutControlItemKapat.Name = "layoutControlItemKapat";
            this.layoutControlItemKapat.Size = new System.Drawing.Size(252, 40);
            this.layoutControlItemKapat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemKapat.TextVisible = false;
            // 
            // layoutControlItemNotlar
            // 
            this.layoutControlItemNotlar.Control = this.richEditControlNotlar;
            this.layoutControlItemNotlar.Location = new System.Drawing.Point(298, 0);
            this.layoutControlItemNotlar.Name = "layoutControlItemNotlar";
            this.layoutControlItemNotlar.Size = new System.Drawing.Size(464, 493);
            this.layoutControlItemNotlar.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemNotlar.TextVisible = false;
            // 
            // layoutControlItemNotListe
            // 
            this.layoutControlItemNotListe.Control = this.gridControlNotListe;
            this.layoutControlItemNotListe.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemNotListe.Name = "layoutControlItemNotListe";
            this.layoutControlItemNotListe.Size = new System.Drawing.Size(298, 493);
            this.layoutControlItemNotListe.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemNotListe.TextVisible = false;
            // 
            // frmMalzemeNotlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.layoutControlNotlar);
            this.Name = "frmMalzemeNotlar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Program Not Ekle";
            this.Load += new System.EventHandler(this.frmNotlar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlNotlar)).EndInit();
            this.layoutControlNotlar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNotListe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNotListe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKaydet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKapat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNotlar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNotListe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlNotlar;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton simpleButtonKapat;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSil;
        private DevExpress.XtraEditors.SimpleButton simpleButtonKaydet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKaydet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSil;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKapat;
        private DevExpress.XtraRichEdit.RichEditControl richEditControlNotlar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemNotlar;
        private DevExpress.XtraGrid.GridControl gridControlNotListe;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNotListe;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemNotListe;
    }
}