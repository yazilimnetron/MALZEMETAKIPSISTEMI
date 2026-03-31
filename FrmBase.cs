using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace MALZEMETAKIPSISTEMI
{
    public class FrmBase : Form
    {
        protected void SetGridFont(GridView view, Font font)
        {
            foreach (DevExpress.Utils.AppearanceObject ap in view.Appearance)
                ap.Font = font;
        }

        protected void SetContainsFilter(GridView view)
        {
            foreach (GridColumn col in view.Columns)
                col.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
        }
    }
}
