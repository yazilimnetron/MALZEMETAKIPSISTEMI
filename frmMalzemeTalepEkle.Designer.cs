namespace MALZEMETAKIPSISTEMI
{
    partial class frmMalzemeTalepEkle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMalzemeTalepEkle));
            this.layoutControlMalzemeIstemBilgileri = new DevExpress.XtraLayout.LayoutControl();
            this.dateEditTalepTarih = new DevExpress.XtraEditors.DateEdit();
            this.textEditTalepNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroupMalzemeIstemBilgileri = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemTalepNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTalepTarih = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.simpleButtonTalepIptal = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonTalepKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItemTalepKaydet = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTalepIptal = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMalzemeIstemBilgileri)).BeginInit();
            this.layoutControlMalzemeIstemBilgileri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTalepTarih.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTalepTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTalepNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMalzemeIstemBilgileri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTalepNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTalepTarih)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTalepKaydet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTalepIptal)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMalzemeIstemBilgileri
            // 
            this.layoutControlMalzemeIstemBilgileri.Controls.Add(this.dateEditTalepTarih);
            this.layoutControlMalzemeIstemBilgileri.Controls.Add(this.simpleButtonTalepIptal);
            this.layoutControlMalzemeIstemBilgileri.Controls.Add(this.simpleButtonTalepKaydet);
            this.layoutControlMalzemeIstemBilgileri.Controls.Add(this.textEditTalepNo);
            this.layoutControlMalzemeIstemBilgileri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMalzemeIstemBilgileri.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMalzemeIstemBilgileri.Name = "layoutControlMalzemeIstemBilgileri";
            this.layoutControlMalzemeIstemBilgileri.Root = this.layoutControlGroupMalzemeIstemBilgileri;
            this.layoutControlMalzemeIstemBilgileri.Size = new System.Drawing.Size(402, 163);
            this.layoutControlMalzemeIstemBilgileri.TabIndex = 0;
            this.layoutControlMalzemeIstemBilgileri.Text = "layoutControl1";
            // 
            // dateEditTalepTarih
            // 
            this.dateEditTalepTarih.EditValue = null;
            this.dateEditTalepTarih.Location = new System.Drawing.Point(178, 60);
            this.dateEditTalepTarih.Name = "dateEditTalepTarih";
            this.dateEditTalepTarih.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateEditTalepTarih.Properties.Appearance.Options.UseFont = true;
            this.dateEditTalepTarih.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateEditTalepTarih.Properties.AppearanceDisabled.Options.UseFont = true;
            this.dateEditTalepTarih.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateEditTalepTarih.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dateEditTalepTarih.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateEditTalepTarih.Properties.AppearanceFocused.Options.UseFont = true;
            this.dateEditTalepTarih.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateEditTalepTarih.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.dateEditTalepTarih.Properties.AutoHeight = false;
            this.dateEditTalepTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTalepTarih.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTalepTarih.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEditTalepTarih.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditTalepTarih.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEditTalepTarih.Size = new System.Drawing.Size(212, 43);
            this.dateEditTalepTarih.StyleController = this.layoutControlMalzemeIstemBilgileri;
            this.dateEditTalepTarih.TabIndex = 14;
            // 
            // textEditTalepNo
            // 
            this.textEditTalepNo.Location = new System.Drawing.Point(178, 12);
            this.textEditTalepNo.Name = "textEditTalepNo";
            this.textEditTalepNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditTalepNo.Properties.Appearance.Options.UseFont = true;
            this.textEditTalepNo.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditTalepNo.Properties.AppearanceDisabled.Options.UseFont = true;
            this.textEditTalepNo.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditTalepNo.Properties.AppearanceFocused.Options.UseFont = true;
            this.textEditTalepNo.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.textEditTalepNo.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.textEditTalepNo.Properties.AutoHeight = false;
            this.textEditTalepNo.Size = new System.Drawing.Size(212, 44);
            this.textEditTalepNo.StyleController = this.layoutControlMalzemeIstemBilgileri;
            this.textEditTalepNo.TabIndex = 13;
            // 
            // layoutControlGroupMalzemeIstemBilgileri
            // 
            this.layoutControlGroupMalzemeIstemBilgileri.CustomizationFormText = "layoutControlGroupMalzemeIstemBilgileri";
            this.layoutControlGroupMalzemeIstemBilgileri.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMalzemeIstemBilgileri.GroupBordersVisible = false;
            this.layoutControlGroupMalzemeIstemBilgileri.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemTalepKaydet,
            this.layoutControlItemTalepIptal,
            this.layoutControlItemTalepNo,
            this.layoutControlItemTalepTarih});
            this.layoutControlGroupMalzemeIstemBilgileri.Name = "layoutControlGroupMalzemeIstemBilgileri";
            this.layoutControlGroupMalzemeIstemBilgileri.Size = new System.Drawing.Size(402, 163);
            this.layoutControlGroupMalzemeIstemBilgileri.TextVisible = false;
            // 
            // layoutControlItemTalepNo
            // 
            this.layoutControlItemTalepNo.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItemTalepNo.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemTalepNo.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItemTalepNo.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItemTalepNo.Control = this.textEditTalepNo;
            this.layoutControlItemTalepNo.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemTalepNo.MinSize = new System.Drawing.Size(279, 32);
            this.layoutControlItemTalepNo.Name = "layoutControlItemTalepNo";
            this.layoutControlItemTalepNo.Size = new System.Drawing.Size(382, 48);
            this.layoutControlItemTalepNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemTalepNo.Text = " Talep Numarası  :";
            this.layoutControlItemTalepNo.TextSize = new System.Drawing.Size(163, 22);
            // 
            // layoutControlItemTalepTarih
            // 
            this.layoutControlItemTalepTarih.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.layoutControlItemTalepTarih.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemTalepTarih.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItemTalepTarih.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItemTalepTarih.Control = this.dateEditTalepTarih;
            this.layoutControlItemTalepTarih.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemTalepTarih.MinSize = new System.Drawing.Size(279, 26);
            this.layoutControlItemTalepTarih.Name = "layoutControlItemTalepTarih";
            this.layoutControlItemTalepTarih.Size = new System.Drawing.Size(382, 47);
            this.layoutControlItemTalepTarih.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemTalepTarih.Text = " Talep Tarihi  :";
            this.layoutControlItemTalepTarih.TextSize = new System.Drawing.Size(163, 22);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // simpleButtonTalepIptal
            // 
            this.simpleButtonTalepIptal.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.simpleButtonTalepIptal.Appearance.Options.UseFont = true;
            this.simpleButtonTalepIptal.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonTalepIptal.ImageOptions.Image")));
            this.simpleButtonTalepIptal.Location = new System.Drawing.Point(203, 107);
            this.simpleButtonTalepIptal.Name = "simpleButtonTalepIptal";
            this.simpleButtonTalepIptal.Size = new System.Drawing.Size(187, 44);
            this.simpleButtonTalepIptal.StyleController = this.layoutControlMalzemeIstemBilgileri;
            this.simpleButtonTalepIptal.TabIndex = 9;
            this.simpleButtonTalepIptal.Text = "İPTAL";
            this.simpleButtonTalepIptal.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButtonTalepKaydet
            // 
            this.simpleButtonTalepKaydet.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.simpleButtonTalepKaydet.Appearance.Options.UseFont = true;
            this.simpleButtonTalepKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonTalepKaydet.ImageOptions.Image")));
            this.simpleButtonTalepKaydet.Location = new System.Drawing.Point(12, 107);
            this.simpleButtonTalepKaydet.Name = "simpleButtonTalepKaydet";
            this.simpleButtonTalepKaydet.Size = new System.Drawing.Size(187, 44);
            this.simpleButtonTalepKaydet.StyleController = this.layoutControlMalzemeIstemBilgileri;
            this.simpleButtonTalepKaydet.TabIndex = 8;
            this.simpleButtonTalepKaydet.Text = "EKLE";
            this.simpleButtonTalepKaydet.Click += new System.EventHandler(this.simpleButtonMalzemeIstemKaydet_Click);
            // 
            // layoutControlItemTalepKaydet
            // 
            this.layoutControlItemTalepKaydet.Control = this.simpleButtonTalepKaydet;
            this.layoutControlItemTalepKaydet.CustomizationFormText = "layoutControlItemMalzemeIstemKaydet";
            this.layoutControlItemTalepKaydet.Location = new System.Drawing.Point(0, 95);
            this.layoutControlItemTalepKaydet.MinSize = new System.Drawing.Size(7, 7);
            this.layoutControlItemTalepKaydet.Name = "layoutControlItemTalepKaydet";
            this.layoutControlItemTalepKaydet.Size = new System.Drawing.Size(191, 48);
            this.layoutControlItemTalepKaydet.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemTalepKaydet.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTalepKaydet.TextVisible = false;
            // 
            // layoutControlItemTalepIptal
            // 
            this.layoutControlItemTalepIptal.Control = this.simpleButtonTalepIptal;
            this.layoutControlItemTalepIptal.CustomizationFormText = "layoutControlItemMalzemeIstemIptal";
            this.layoutControlItemTalepIptal.Location = new System.Drawing.Point(191, 95);
            this.layoutControlItemTalepIptal.MinSize = new System.Drawing.Size(7, 7);
            this.layoutControlItemTalepIptal.Name = "layoutControlItemTalepIptal";
            this.layoutControlItemTalepIptal.Size = new System.Drawing.Size(191, 48);
            this.layoutControlItemTalepIptal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemTalepIptal.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTalepIptal.TextVisible = false;
            // 
            // frmMalzemeTalepEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 163);
            this.Controls.Add(this.layoutControlMalzemeIstemBilgileri);
            this.Name = "frmMalzemeTalepEkle";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Malzeme Talep Ekle";
            this.Load += new System.EventHandler(this.frmMalzemeIstemBilgileri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMalzemeIstemBilgileri)).EndInit();
            this.layoutControlMalzemeIstemBilgileri.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTalepTarih.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTalepTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTalepNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMalzemeIstemBilgileri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTalepNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTalepTarih)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTalepKaydet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTalepIptal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMalzemeIstemBilgileri;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMalzemeIstemBilgileri;
        private DevExpress.XtraEditors.SimpleButton simpleButtonTalepIptal;
        private DevExpress.XtraEditors.SimpleButton simpleButtonTalepKaydet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTalepKaydet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTalepIptal;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTalepNo;
        private DevExpress.XtraEditors.TextEdit textEditTalepNo;
        private DevExpress.XtraEditors.DateEdit dateEditTalepTarih;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTalepTarih;
    }
}