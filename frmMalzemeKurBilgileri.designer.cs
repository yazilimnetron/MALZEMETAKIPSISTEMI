namespace MALZEMETAKIPSISTEMI
{
    partial class frmMalzemeKurBilgileri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMalzemeKurBilgileri));
            this.btnKurKapat = new System.Windows.Forms.Button();
            this.txtKurEuro = new System.Windows.Forms.TextBox();
            this.txtKurDolar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnKurOnayla = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tarihKur = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlKur = new System.Windows.Forms.Panel();
            this.pictureKurlar = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.grupBilgileriGir = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKurRon = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKurChf = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtKurJpy = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtKurGbp = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlKur.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureKurlar)).BeginInit();
            this.grupBilgileriGir.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnKurKapat
            // 
            this.btnKurKapat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnKurKapat.Image = ((System.Drawing.Image)(resources.GetObject("btnKurKapat.Image")));
            this.btnKurKapat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKurKapat.Location = new System.Drawing.Point(340, 483);
            this.btnKurKapat.Margin = new System.Windows.Forms.Padding(4);
            this.btnKurKapat.Name = "btnKurKapat";
            this.btnKurKapat.Size = new System.Drawing.Size(143, 47);
            this.btnKurKapat.TabIndex = 4;
            this.btnKurKapat.Text = "   Kapat";
            this.btnKurKapat.UseVisualStyleBackColor = true;
            this.btnKurKapat.Click += new System.EventHandler(this.btnKurKapat_Click);
            // 
            // txtKurEuro
            // 
            this.txtKurEuro.Location = new System.Drawing.Point(203, 90);
            this.txtKurEuro.Margin = new System.Windows.Forms.Padding(4);
            this.txtKurEuro.MaxLength = 23;
            this.txtKurEuro.Name = "txtKurEuro";
            this.txtKurEuro.Size = new System.Drawing.Size(201, 22);
            this.txtKurEuro.TabIndex = 2;
            this.txtKurEuro.Text = "0";
            this.txtKurEuro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKurEuro.TextChanged += new System.EventHandler(this.txtKurEuro_TextChanged);
            this.txtKurEuro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKurEuro_KeyPress);
            // 
            // txtKurDolar
            // 
            this.txtKurDolar.Location = new System.Drawing.Point(203, 60);
            this.txtKurDolar.Margin = new System.Windows.Forms.Padding(4);
            this.txtKurDolar.MaxLength = 23;
            this.txtKurDolar.Name = "txtKurDolar";
            this.txtKurDolar.Size = new System.Drawing.Size(201, 22);
            this.txtKurDolar.TabIndex = 1;
            this.txtKurDolar.Text = "0";
            this.txtKurDolar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKurDolar.TextChanged += new System.EventHandler(this.txtKurDolar_TextChanged);
            this.txtKurDolar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKurDolar_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(406, 94);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 16);
            this.label5.TabIndex = 40;
            this.label5.Text = "TL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(406, 63);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 16);
            this.label4.TabIndex = 39;
            this.label4.Text = "TL";
            // 
            // btnKurOnayla
            // 
            this.btnKurOnayla.Image = ((System.Drawing.Image)(resources.GetObject("btnKurOnayla.Image")));
            this.btnKurOnayla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKurOnayla.Location = new System.Drawing.Point(189, 483);
            this.btnKurOnayla.Margin = new System.Windows.Forms.Padding(4);
            this.btnKurOnayla.Name = "btnKurOnayla";
            this.btnKurOnayla.Size = new System.Drawing.Size(143, 47);
            this.btnKurOnayla.TabIndex = 3;
            this.btnKurOnayla.Text = "     Onayla";
            this.btnKurOnayla.UseVisualStyleBackColor = true;
            this.btnKurOnayla.Click += new System.EventHandler(this.btnKurOnayla_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 93);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 16);
            this.label3.TabIndex = 35;
            this.label3.Text = "Euro";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 34;
            this.label2.Text = "Dolar";
            // 
            // tarihKur
            // 
            this.tarihKur.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.tarihKur.Location = new System.Drawing.Point(204, 23);
            this.tarihKur.Margin = new System.Windows.Forms.Padding(4);
            this.tarihKur.Name = "tarihKur";
            this.tarihKur.Size = new System.Drawing.Size(201, 22);
            this.tarihKur.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "Tarih";
            // 
            // pnlKur
            // 
            this.pnlKur.BackColor = System.Drawing.Color.White;
            this.pnlKur.Controls.Add(this.pictureKurlar);
            this.pnlKur.Controls.Add(this.label18);
            this.pnlKur.Location = new System.Drawing.Point(0, 0);
            this.pnlKur.Margin = new System.Windows.Forms.Padding(4);
            this.pnlKur.Name = "pnlKur";
            this.pnlKur.Size = new System.Drawing.Size(647, 86);
            this.pnlKur.TabIndex = 31;
            // 
            // pictureKurlar
            // 
            this.pictureKurlar.Image = ((System.Drawing.Image)(resources.GetObject("pictureKurlar.Image")));
            this.pictureKurlar.Location = new System.Drawing.Point(13, 4);
            this.pictureKurlar.Margin = new System.Windows.Forms.Padding(4);
            this.pictureKurlar.Name = "pictureKurlar";
            this.pictureKurlar.Size = new System.Drawing.Size(113, 80);
            this.pictureKurlar.TabIndex = 1;
            this.pictureKurlar.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label18.Location = new System.Drawing.Point(235, 27);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(194, 40);
            this.label18.TabIndex = 0;
            this.label18.Text = "Kur Bilgileri";
            // 
            // grupBilgileriGir
            // 
            this.grupBilgileriGir.Controls.Add(this.dataGridView1);
            this.grupBilgileriGir.Controls.Add(this.label6);
            this.grupBilgileriGir.Controls.Add(this.txtKurRon);
            this.grupBilgileriGir.Controls.Add(this.label7);
            this.grupBilgileriGir.Controls.Add(this.txtKurChf);
            this.grupBilgileriGir.Controls.Add(this.label8);
            this.grupBilgileriGir.Controls.Add(this.txtKurJpy);
            this.grupBilgileriGir.Controls.Add(this.label9);
            this.grupBilgileriGir.Controls.Add(this.label10);
            this.grupBilgileriGir.Controls.Add(this.label11);
            this.grupBilgileriGir.Controls.Add(this.label12);
            this.grupBilgileriGir.Controls.Add(this.txtKurGbp);
            this.grupBilgileriGir.Controls.Add(this.label19);
            this.grupBilgileriGir.Controls.Add(this.tarihKur);
            this.grupBilgileriGir.Controls.Add(this.label1);
            this.grupBilgileriGir.Controls.Add(this.txtKurEuro);
            this.grupBilgileriGir.Controls.Add(this.label2);
            this.grupBilgileriGir.Controls.Add(this.txtKurDolar);
            this.grupBilgileriGir.Controls.Add(this.label3);
            this.grupBilgileriGir.Controls.Add(this.label5);
            this.grupBilgileriGir.Controls.Add(this.label4);
            this.grupBilgileriGir.Location = new System.Drawing.Point(16, 100);
            this.grupBilgileriGir.Margin = new System.Windows.Forms.Padding(4);
            this.grupBilgileriGir.Name = "grupBilgileriGir";
            this.grupBilgileriGir.Padding = new System.Windows.Forms.Padding(4);
            this.grupBilgileriGir.Size = new System.Drawing.Size(631, 375);
            this.grupBilgileriGir.TabIndex = 0;
            this.grupBilgileriGir.TabStop = false;
            this.grupBilgileriGir.Text = "Bilgileri Giriniz";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 238);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(617, 130);
            this.dataGridView1.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(118, 212);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 16);
            this.label6.TabIndex = 53;
            this.label6.Text = "Rumen Leyi";
            // 
            // txtKurRon
            // 
            this.txtKurRon.Location = new System.Drawing.Point(203, 209);
            this.txtKurRon.Margin = new System.Windows.Forms.Padding(4);
            this.txtKurRon.MaxLength = 23;
            this.txtKurRon.Name = "txtKurRon";
            this.txtKurRon.Size = new System.Drawing.Size(201, 22);
            this.txtKurRon.TabIndex = 52;
            this.txtKurRon.Text = "0";
            this.txtKurRon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(406, 212);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 16);
            this.label7.TabIndex = 54;
            this.label7.Text = "TL";
            // 
            // txtKurChf
            // 
            this.txtKurChf.Location = new System.Drawing.Point(203, 179);
            this.txtKurChf.Margin = new System.Windows.Forms.Padding(4);
            this.txtKurChf.MaxLength = 23;
            this.txtKurChf.Name = "txtKurChf";
            this.txtKurChf.Size = new System.Drawing.Size(201, 22);
            this.txtKurChf.TabIndex = 47;
            this.txtKurChf.Text = "0";
            this.txtKurChf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(121, 152);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 16);
            this.label8.TabIndex = 48;
            this.label8.Text = "Japon Yeni";
            // 
            // txtKurJpy
            // 
            this.txtKurJpy.Location = new System.Drawing.Point(203, 149);
            this.txtKurJpy.Margin = new System.Windows.Forms.Padding(4);
            this.txtKurJpy.MaxLength = 23;
            this.txtKurJpy.Name = "txtKurJpy";
            this.txtKurJpy.Size = new System.Drawing.Size(201, 22);
            this.txtKurJpy.TabIndex = 46;
            this.txtKurJpy.Text = "0";
            this.txtKurJpy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(109, 183);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 16);
            this.label9.TabIndex = 49;
            this.label9.Text = "İsviçre Frangı";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(406, 183);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 16);
            this.label10.TabIndex = 51;
            this.label10.Text = "TL";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(406, 152);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 16);
            this.label11.TabIndex = 50;
            this.label11.Text = "TL";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(152, 123);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 16);
            this.label12.TabIndex = 43;
            this.label12.Text = "Sterlin";
            // 
            // txtKurGbp
            // 
            this.txtKurGbp.Location = new System.Drawing.Point(203, 120);
            this.txtKurGbp.Margin = new System.Windows.Forms.Padding(4);
            this.txtKurGbp.MaxLength = 23;
            this.txtKurGbp.Name = "txtKurGbp";
            this.txtKurGbp.Size = new System.Drawing.Size(201, 22);
            this.txtKurGbp.TabIndex = 41;
            this.txtKurGbp.Text = "0";
            this.txtKurGbp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKurGbp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKurJpy_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(406, 123);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(23, 16);
            this.label19.TabIndex = 45;
            this.label19.Text = "TL";
            // 
            // frmMalzemeKurBilgileri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnKurKapat;
            this.ClientSize = new System.Drawing.Size(660, 537);
            this.Controls.Add(this.grupBilgileriGir);
            this.Controls.Add(this.btnKurKapat);
            this.Controls.Add(this.btnKurOnayla);
            this.Controls.Add(this.pnlKur);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMalzemeKurBilgileri";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Döviz Kurları";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmkur_FormClosing);
            this.Load += new System.EventHandler(this.frmkur_Load);
            this.pnlKur.ResumeLayout(false);
            this.pnlKur.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureKurlar)).EndInit();
            this.grupBilgileriGir.ResumeLayout(false);
            this.grupBilgileriGir.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnKurKapat;
        private System.Windows.Forms.TextBox txtKurEuro;
        private System.Windows.Forms.TextBox txtKurDolar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnKurOnayla;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker tarihKur;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlKur;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureKurlar;
        private System.Windows.Forms.GroupBox grupBilgileriGir;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtKurGbp;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtKurRon;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtKurChf;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtKurJpy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}