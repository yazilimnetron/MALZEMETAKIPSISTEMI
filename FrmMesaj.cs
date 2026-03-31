using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MALZEMETAKIPSISTEMI
{
    public partial class FrmMesaj : DevExpress.XtraEditors.XtraForm
    {
        public FrmMesaj()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            label2.Text = "Yes";
            this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (label2.Text=="Yes")
            {
                DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Dispose();
            }
            else if (label2.Text=="No")
            {
                DialogResult = System.Windows.Forms.DialogResult.No;
                this.Dispose();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            label2.Text = "No";
            this.Close();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}