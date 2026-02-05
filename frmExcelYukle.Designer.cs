namespace MALZEME_TAKIP_SISTEMI
{
    partial class frmExcelYukle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExcelYukle));
            this.layoutExcelYukle = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonExcelYaz = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlExcel = new DevExpress.XtraGrid.GridControl();
            this.gridViewExcel = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButtonExcelOku = new DevExpress.XtraEditors.SimpleButton();
            this.textEditExcelOku = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlExcelYukle = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemExcelOku = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemExcelYolButton = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemExcelGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemExcelYaz = new DevExpress.XtraLayout.LayoutControlItem();
            this.openFileDialogExcel = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.layoutExcelYukle)).BeginInit();
            this.layoutExcelYukle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditExcelOku.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlExcelYukle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemExcelOku)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemExcelYolButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemExcelGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemExcelYaz)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutExcelYukle
            // 
            this.layoutExcelYukle.Controls.Add(this.simpleButtonExcelYaz);
            this.layoutExcelYukle.Controls.Add(this.gridControlExcel);
            this.layoutExcelYukle.Controls.Add(this.simpleButtonExcelOku);
            this.layoutExcelYukle.Controls.Add(this.textEditExcelOku);
            this.layoutExcelYukle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutExcelYukle.Location = new System.Drawing.Point(0, 0);
            this.layoutExcelYukle.Name = "layoutExcelYukle";
            this.layoutExcelYukle.Root = this.layoutControlExcelYukle;
            this.layoutExcelYukle.Size = new System.Drawing.Size(1284, 661);
            this.layoutExcelYukle.TabIndex = 0;
            this.layoutExcelYukle.Text = "layoutControl1";
            // 
            // simpleButtonExcelYaz
            // 
            this.simpleButtonExcelYaz.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.simpleButtonExcelYaz.Appearance.Options.UseFont = true;
            this.simpleButtonExcelYaz.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonExcelYaz.Image")));
            this.simpleButtonExcelYaz.Location = new System.Drawing.Point(12, 611);
            this.simpleButtonExcelYaz.Name = "simpleButtonExcelYaz";
            this.simpleButtonExcelYaz.Size = new System.Drawing.Size(1260, 38);
            this.simpleButtonExcelYaz.StyleController = this.layoutExcelYukle;
            this.simpleButtonExcelYaz.TabIndex = 7;
            this.simpleButtonExcelYaz.Text = "EXCEL YAZ";
            this.simpleButtonExcelYaz.Click += new System.EventHandler(this.simpleButtonExcelYaz_Click);
            // 
            // gridControlExcel
            // 
            this.gridControlExcel.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControlExcel.Location = new System.Drawing.Point(12, 112);
            this.gridControlExcel.MainView = this.gridViewExcel;
            this.gridControlExcel.Name = "gridControlExcel";
            this.gridControlExcel.Size = new System.Drawing.Size(1260, 495);
            this.gridControlExcel.TabIndex = 6;
            this.gridControlExcel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewExcel});
            // 
            // gridViewExcel
            // 
            this.gridViewExcel.GridControl = this.gridControlExcel;
            this.gridViewExcel.Name = "gridViewExcel";
            this.gridViewExcel.OptionsBehavior.Editable = false;
            this.gridViewExcel.OptionsBehavior.ReadOnly = true;
            this.gridViewExcel.OptionsView.ColumnAutoWidth = false;
            this.gridViewExcel.OptionsView.ShowAutoFilterRow = true;
            this.gridViewExcel.OptionsView.ShowFooter = true;
            this.gridViewExcel.OptionsView.ShowGroupPanel = false;
            this.gridViewExcel.PaintStyleName = "Office2003";
            // 
            // simpleButtonExcelOku
            // 
            this.simpleButtonExcelOku.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.simpleButtonExcelOku.Appearance.Options.UseFont = true;
            this.simpleButtonExcelOku.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonExcelOku.Image")));
            this.simpleButtonExcelOku.Location = new System.Drawing.Point(12, 70);
            this.simpleButtonExcelOku.Name = "simpleButtonExcelOku";
            this.simpleButtonExcelOku.Size = new System.Drawing.Size(1260, 38);
            this.simpleButtonExcelOku.StyleController = this.layoutExcelYukle;
            this.simpleButtonExcelOku.TabIndex = 5;
            this.simpleButtonExcelOku.Text = "DOSYA SEÇ";
            this.simpleButtonExcelOku.Click += new System.EventHandler(this.simpleButtonExcelOku_Click);
            // 
            // textEditExcelOku
            // 
            this.textEditExcelOku.Location = new System.Drawing.Point(113, 12);
            this.textEditExcelOku.Name = "textEditExcelOku";
            this.textEditExcelOku.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.textEditExcelOku.Properties.Appearance.Options.UseFont = true;
            this.textEditExcelOku.Properties.AutoHeight = false;
            this.textEditExcelOku.Size = new System.Drawing.Size(1159, 54);
            this.textEditExcelOku.StyleController = this.layoutExcelYukle;
            this.textEditExcelOku.TabIndex = 4;
            // 
            // layoutControlExcelYukle
            // 
            this.layoutControlExcelYukle.CustomizationFormText = "layoutControlExcelYukle";
            this.layoutControlExcelYukle.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlExcelYukle.GroupBordersVisible = false;
            this.layoutControlExcelYukle.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemExcelOku,
            this.layoutItemExcelYolButton,
            this.layoutItemExcelGrid,
            this.layoutItemExcelYaz});
            this.layoutControlExcelYukle.Location = new System.Drawing.Point(0, 0);
            this.layoutControlExcelYukle.Name = "layoutControlExcelYukle";
            this.layoutControlExcelYukle.Size = new System.Drawing.Size(1284, 661);
            this.layoutControlExcelYukle.Text = "layoutControlExcelYukle";
            this.layoutControlExcelYukle.TextVisible = false;
            // 
            // layoutItemExcelOku
            // 
            this.layoutItemExcelOku.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.layoutItemExcelOku.AppearanceItemCaption.Options.UseFont = true;
            this.layoutItemExcelOku.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutItemExcelOku.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutItemExcelOku.Control = this.textEditExcelOku;
            this.layoutItemExcelOku.CustomizationFormText = " DOSYA SEÇ";
            this.layoutItemExcelOku.Location = new System.Drawing.Point(0, 0);
            this.layoutItemExcelOku.MinSize = new System.Drawing.Size(151, 49);
            this.layoutItemExcelOku.Name = "layoutItemExcelOku";
            this.layoutItemExcelOku.Size = new System.Drawing.Size(1264, 58);
            this.layoutItemExcelOku.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutItemExcelOku.Text = " DOSYA SEÇ";
            this.layoutItemExcelOku.TextSize = new System.Drawing.Size(98, 19);
            // 
            // layoutItemExcelYolButton
            // 
            this.layoutItemExcelYolButton.Control = this.simpleButtonExcelOku;
            this.layoutItemExcelYolButton.CustomizationFormText = "layoutItemExcelYolButton";
            this.layoutItemExcelYolButton.Location = new System.Drawing.Point(0, 58);
            this.layoutItemExcelYolButton.Name = "layoutItemExcelYolButton";
            this.layoutItemExcelYolButton.Size = new System.Drawing.Size(1264, 42);
            this.layoutItemExcelYolButton.Text = "layoutItemExcelYolButton";
            this.layoutItemExcelYolButton.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemExcelYolButton.TextToControlDistance = 0;
            this.layoutItemExcelYolButton.TextVisible = false;
            // 
            // layoutItemExcelGrid
            // 
            this.layoutItemExcelGrid.Control = this.gridControlExcel;
            this.layoutItemExcelGrid.CustomizationFormText = "layoutItemExcelGrid";
            this.layoutItemExcelGrid.Location = new System.Drawing.Point(0, 100);
            this.layoutItemExcelGrid.Name = "layoutItemExcelGrid";
            this.layoutItemExcelGrid.Size = new System.Drawing.Size(1264, 499);
            this.layoutItemExcelGrid.Text = "layoutItemExcelGrid";
            this.layoutItemExcelGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemExcelGrid.TextToControlDistance = 0;
            this.layoutItemExcelGrid.TextVisible = false;
            // 
            // layoutItemExcelYaz
            // 
            this.layoutItemExcelYaz.Control = this.simpleButtonExcelYaz;
            this.layoutItemExcelYaz.CustomizationFormText = "layoutItemExcelYaz";
            this.layoutItemExcelYaz.Location = new System.Drawing.Point(0, 599);
            this.layoutItemExcelYaz.Name = "layoutItemExcelYaz";
            this.layoutItemExcelYaz.Size = new System.Drawing.Size(1264, 42);
            this.layoutItemExcelYaz.Text = "layoutItemExcelYaz";
            this.layoutItemExcelYaz.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemExcelYaz.TextToControlDistance = 0;
            this.layoutItemExcelYaz.TextVisible = false;
            // 
            // openFileDialogExcel
            // 
            this.openFileDialogExcel.FileName = "openFileDialogExcel";
            // 
            // frmExcelYukle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 661);
            this.Controls.Add(this.layoutExcelYukle);
            this.Name = "frmExcelYukle";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmExcelYukle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutExcelYukle)).EndInit();
            this.layoutExcelYukle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditExcelOku.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlExcelYukle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemExcelOku)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemExcelYolButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemExcelGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemExcelYaz)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutExcelYukle;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlExcelYukle;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExcelYaz;
        private DevExpress.XtraGrid.GridControl gridControlExcel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewExcel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExcelOku;
        private DevExpress.XtraEditors.TextEdit textEditExcelOku;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemExcelOku;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemExcelYolButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemExcelGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemExcelYaz;
        private System.Windows.Forms.OpenFileDialog openFileDialogExcel;
    }
}