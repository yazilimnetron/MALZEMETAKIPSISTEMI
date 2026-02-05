using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MALZEME_TAKIP_SISTEMI
{
    public partial class frmGrafik : Form
    {
        frmMalzemeTalepIstatistikleri frmMalzemeT = ((frmMalzemeTalepIstatistikleri)Application.OpenForms["frmMalzemeTalepIstatistikleri"]);
        public frmGrafik()
        {
            InitializeComponent();
        }


        private DataTable CreateChartData()
        {
            DataTable table = new DataTable("Table1");
            table.Columns.Add("Argument", typeof(string));
            table.Columns.Add("Value", typeof(decimal));


            StringBuilder sb = new StringBuilder(1024);
            sb.Append("SELECT MALZEMEGIRIS_ADI, MALZEMEGIRIS_TOPLAMFIYAT ");
            sb.Append("FROM TBL_LST_MALZEMEGIRIS (NOLOCK) ");
            // sb.AppendFormat("WHERE CONVERT(VARCHAR(10),MALZEMEGIRIS_TARIH ,121)>={0}", "'" + Convert.ToDateTime(frmMalzemeT.dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            // sb.AppendFormat("AND CONVERT(VARCHAR(10),MALZEMEGIRIS_TARIH ,121)<={0}", "'" + Convert.ToDateTime(frmMalzemeT.dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            sb.Append("union all ");
            sb.Append("SELECT 'TOPLAM', SUM(0)  ");
            sb.Append("FROM TBL_LST_MALZEMEGIRIS (NOLOCK) ");
            // sb.AppendFormat("WHERE CONVERT(VARCHAR(10),MALZEMEGIRIS_TARIH ,121)>={0}", "'" + Convert.ToDateTime(frmMalzemeT.dateEditBaslangicTarih.DateTime).ToString("yyyy-MM-dd") + "' ");
            // sb.AppendFormat("AND CONVERT(VARCHAR(10),MALZEMEGIRIS_TARIH ,121)<={0}", "'" + Convert.ToDateTime(frmMalzemeT.dateEditBitisTarih.DateTime).ToString("yyyy-MM-dd") + "' ");

            DataTable table1 = clSqlTanim.RunStoredProc(sb.ToString());

            DataRow row = null;
            foreach (DataRow dr in table1.Rows)
            {
                row = table.NewRow();
                row["Argument"] = clGenelTanim.DBToString(dr["MALZEMEGIRIS_ADI"]);
                Decimal dblValue = decimal.Parse(dr["MALZEMEGIRIS_TOPLAMFIYAT"].ToString());
                row["Value"] = dblValue;
                table.Rows.Add(row);
            }

            return table;
        }

        private void frmGrafik_Load(object sender, EventArgs e)
        {
            Series series1 = new Series("Malzeme Listesi", ViewType.Pie3D);
            chartControl1.Series.Add(series1);
            series1.DataSource = CreateChartData();

            series1.ArgumentScaleType = ScaleType.Qualitative;
            series1.ArgumentDataMember = "Argument";
            series1.ValueScaleType = ScaleType.Numerical;
            series1.ValueDataMembers.AddRange(new string[] { "Value" });



            // Adjust the view-type-specific options of the series.
            ((Pie3DSeriesView)series1.View).Depth = 30;
            ((Pie3DSeriesView)series1.View).ExplodedPoints.Add(series1.Points[0]);
            ((Pie3DSeriesView)series1.View).ExplodedDistancePercentage = 30;
            ((Pie3DSeriesView)series1.View).ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                DataFilterCondition.GreaterThanOrEqual, 9));
            ((Pie3DSeriesView)series1.View).ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.NotEqual, "Others"));
            ((Pie3DSeriesView)series1.View).ExplodeMode = PieExplodeMode.All;
            ((Pie3DSeriesView)series1.View).ExplodedDistancePercentage = 2;
            //((Pie3DSeriesView)series1.View).RuntimeExploding = true;
            //((Pie3DSeriesView)series1.View).HeightToWidthRatio = 2.25;

            // Access the diagram's options.
            ((SimpleDiagram3D)chartControl1.Diagram).RotationType = RotationType.UseAngles;
            ((SimpleDiagram3D)chartControl1.Diagram).RotationAngleX = -35;

            // Add a title to the chart and hide the legend.
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Liste";
            chartControl1.Titles.Add(chartTitle1);
            chartControl1.Legend.Visible = true;


            series1.LegendTextPattern = "{A}: {V:c2}"; ///// {VP:p0}

            //Pie3DSeriesView myView = (Pie3DSeriesView)series1.View;
            //// Show a title for the series.
            //myView.Titles.Add(new SeriesTitle());
            //myView.Titles[0].Text = series1.Name;

            //// Specify a data filter to explode points.
            //myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
            //    DataFilterCondition.GreaterThanOrEqual, 9));
            //myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
            //    DataFilterCondition.NotEqual, "Others"));
            //myView.ExplodeMode = PieExplodeMode.UseFilters;
            //myView.ExplodedDistancePercentage = 2;
            //myView.RuntimeExploding = true;
            //myView.HeightToWidthRatio = 2.25;

            //////    try
            //////    {
            //////        ChartControl pieChart = new ChartControl();

            //////        // Create a pie series.
            //////        Series series1 = new Series("Malzeme Listesi", ViewType.Pie);

            //////           // Add the series to the chart.
            //////            pieChart.Series.Add(series1);

            //////            series1.DataSource = CreateChartData();

            //////            series1.ArgumentScaleType = ScaleType.Qualitative;
            //////            series1.ArgumentDataMember = "Argument";
            //////            series1.ValueScaleType = ScaleType.Numerical;
            //////            series1.ValueDataMembers.AddRange(new string[] { "Value" });

            //////        //pieChart.SeriesTemplate.SummaryFunction = "SUM([Value])";
            //////        //pieChart.SeriesTemplate.LegendTextPattern = "{A}: {V:c2}";

            //////        // Format the the series labels.
            //////        //PiePointOptions piePointOptions = series1.Label.PointOptions as PiePointOptions;
            //////        //piePointOptions.PercentOptions.ValueAsPercent = false;
            //////        //piePointOptions.ValueNumericOptions.Precision = 2;
            //////        //piePointOptions.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Currency;

            //////double sum = 0;
            //////for (int i = 0; i < series1.Points.Count; i++)
            //////{
            //////    sum = sum + series1.Points[i].Values[0];
            //////}

            //////        series1.LegendTextPattern = "Summary:" + sum.ToString();

            //////        series1.LegendTextPattern = "{A}: {V:c2}"; ///// {VP:p0}

            //////        //// Make the series point labels display both arguments and values.
            //////        //((PiePointOptions)series1.Label.PointOptions).PointView = PointView.Values;

            //////        //// Make the series points' values to be displayed as percents.
            //////        //((PiePointOptions)series1.Label.PointOptions).PercentOptions.ValueAsPercent = true;
            //////        //((PiePointOptions)series1.Label.PointOptions).ValueNumericOptions.Format = NumericFormat.Percent;
            //////        //((PiePointOptions)series1.Label.PointOptions).ValueNumericOptions.Precision = 2;

            //////        // Adjust the position of series labels. 
            //////        ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;

            //////        // Detect overlapping of series labels.
            //////        ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            //////        // Access the view-type-specific options of the series.
            //////        PieSeriesView myView = (PieSeriesView)series1.View;

            //////        // Show a title for the series.
            //////        myView.Titles.Add(new SeriesTitle());
            //////        myView.Titles[0].Text = series1.Name;

            //////        // Specify a data filter to explode points.
            //////        myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
            //////            DataFilterCondition.GreaterThanOrEqual, 9));
            //////        myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
            //////            DataFilterCondition.NotEqual, "Others"));
            //////        myView.ExplodeMode = PieExplodeMode.UseFilters;
            //////        myView.ExplodedDistancePercentage = 2;
            //////        myView.RuntimeExploding = true;
            //////        myView.HeightToWidthRatio = 2.25;

            //////        // Hide the legend (if necessary).
            //////        pieChart.Legend.Visible = true;

            //////        // Add the chart to the form.
            //////        pieChart.Dock = DockStyle.Fill;
            //////        this.Controls.Add(pieChart);
            //////    }
            //////    catch (Exception ex) { MessageBox.Show(ex.Message); };
        }

        private void chartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if (e.SeriesPoint.Argument == "TOPLAM")
            {
                double sum = 0;
                for (int i = 0; i < e.Series.Points.Count; i++)
                {
                    sum = sum + e.Series.Points[i].Values[0];
                }
                e.LegendText = "TOPLAM : " + sum.ToString();
            }
        }

        private void chartControl1_CustomDrawSeriesPoint_1(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if (e.SeriesPoint.Argument == "TOPLAM")
            {
                double sum = 0;
                for (int i = 0; i < e.Series.Points.Count; i++)
                {
                    sum = sum + e.Series.Points[i].Values[0];
                }
                e.LegendText = "TOPLAM : " + sum.ToString();
            }
        }

    }
}
