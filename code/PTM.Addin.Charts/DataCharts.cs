using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PTM.Common;
using PTM.Framework;
using ZedGraph;

namespace PTM.Addin.Charts
{
    public partial class DataCharts : AddinTabPage
    {

        private BackgroundWorker worker;
        
        public DataCharts()
        {
            InitializeComponent();
            this.chartComboBox.SelectedIndex = 0;
            this.Text = "Charts";
            this.Status = "Ready";

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            zg.GraphPane.Title.Text = String.Empty;
            zg.GraphPane.XAxis.Title.Text = String.Empty;
            zg.GraphPane.YAxis.Title.Text = String.Empty;
            zg.GraphPane.IsFontsScaled = false;
            zg.GraphPane.IsPenWidthScaled = false;
            //zg.GraphPane.XAxis.ScaleFormatEvent += new Axis.ScaleFormatHandler(XScaleFormatEvent);
            this.fromDateTimePicker.Value = DateTime.Today;
            this.toDateTimePicker.Value = DateTime.Today;

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetChartData((GenerateChartArguments) e.Argument);
            if (worker.CancellationPending)
                e.Cancel = true;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetReadyState();
            if (e.Error != null)
            {
                Logger.WriteException(e.Error);
                MessageBox.Show(this, e.Error.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (e.Cancelled)
            {
                return;
            }
            ChartCollectedData((ChartInfo)e.Result);
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            CallGenerateChartAsync();           
        }

        private void CallGenerateChartAsync()
        {
            if(this.chartComboBox.SelectedIndex == 0)
            {
                MessageBox.Show(this, "Select a chart first.", this.ParentForm.Text, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.chartComboBox.Focus();
                return;
            }

            if (this.chartComboBox.SelectedIndex == 2)
            {
                //fix from date to the beggining of the week
                this.fromDateTimePicker.Value =
                    this.fromDateTimePicker.Value.AddDays(-(int)this.fromDateTimePicker.Value.DayOfWeek);
                //fix to date to the end of the week
                this.toDateTimePicker.Value =
                    this.toDateTimePicker.Value.AddDays(6 - (int)this.toDateTimePicker.Value.DayOfWeek);               
            }

            if (this.chartComboBox.SelectedIndex == 3)
            {
                //fix from date to the beggining of the month
                this.fromDateTimePicker.Value =
                    this.fromDateTimePicker.Value.AddDays(-this.fromDateTimePicker.Value.Day+1);
                //fix to date to the end of the month
                this.toDateTimePicker.Value = this.toDateTimePicker.Value.AddMonths(1).AddDays(-this.toDateTimePicker.Value.Day);
            }
            
            SetWaitState();
            if (worker.IsBusy) worker.CancelAsync();
            while (worker.IsBusy) Application.DoEvents();
            GenerateChartArguments arg = new GenerateChartArguments();
            arg.SelectedChart = this.chartComboBox.SelectedIndex;
            arg.FromDate = this.fromDateTimePicker.Value.Date;
            arg.ToDate = this.toDateTimePicker.Value.Date;
            worker.RunWorkerAsync(arg);
        }

        private ChartInfo GetChartData(GenerateChartArguments args)
        {
            if(args.SelectedChart == 1)
                return GetWorkedTimeVsDayData(args);
            else if (args.SelectedChart == 2)
                return GetWorkedTimeVsWeekData(args);
            else if (args.SelectedChart == 3)
                return GetWorkedTimeVsMonthData(args);

            throw new NotImplementedException("Selected chart is not implemented yet");
        }

        private void ChartCollectedData(ChartInfo chartInfo)
        {
            if (chartInfo.Arguments.SelectedChart == 1)
                ChartWorkedTimeVsDay(chartInfo);
            else if (chartInfo.Arguments.SelectedChart == 2)
                ChartWorkedTimeVsWeek(chartInfo);
            else if (chartInfo.Arguments.SelectedChart == 3)
                ChartWorkedTimeVsMonth(chartInfo);
        }


        #region Worked time vs days

        private ChartInfo GetWorkedTimeVsDayData(GenerateChartArguments args)
        {
            ChartPointsList workedTimeList = new ChartPointsList();
            ChartPointsList activeTimeList = new ChartPointsList();
            ChartPointsList inactiveTimeList = new ChartPointsList();

            DateTime curDate = args.FromDate;
            DateTime toDate = args.ToDate;
            while (curDate <= toDate)
            {
                if(worker.CancellationPending)
                    return null;
                int workedTime = TasksSummaries.GetWorkedTime(curDate, curDate);
                if(workedTime!=0)
                {
                    int activeTime = TasksSummaries.GetActiveTime(curDate, curDate);
                    int inactiveTime = workedTime - activeTime;
                    //double xDate = new XDate(curDate);
                    string date = curDate.ToShortDateString();
                    workedTimeList.Add(date, Convert.ToDouble(workedTime * 1.0/ 3600));
                    activeTimeList.Add(date, Convert.ToDouble(activeTime * 1.0 / 3600));
                    inactiveTimeList.Add(date, Convert.ToDouble(inactiveTime * 1.0 / 3600));
                }
                curDate = curDate.AddDays(1);
            }

            return new ChartInfo(args, new ChartPointsList[] { workedTimeList , activeTimeList, inactiveTimeList});            
        }

        private void ChartWorkedTimeVsDay(ChartInfo info)
        {
            zg.ZoomOutAll(zg.GraphPane);
            zg.GraphPane.Title.Text = "Hrs. worked vs. Day";
            zg.GraphPane.XAxis.Title.Text = "Day";
            zg.GraphPane.YAxis.Title.Text = "Hrs.";
            zg.GraphPane.XAxis.Type = AxisType.Text;
            zg.GraphPane.XAxis.Scale.FontSpec.Angle = 80;
            
            zg.GraphPane.XAxis.Scale.TextLabels = (string[]) info.PointsList[0].XAxisData.ToArray(typeof (string));
            zg.GraphPane.AddCurve("Worked time", null, (double[])info.PointsList[0].YAxisData.ToArray(typeof(double)), Color.Blue);
            zg.GraphPane.AddCurve("Active time", null, (double[])info.PointsList[1].YAxisData.ToArray(typeof(double)), Color.Green);
            zg.GraphPane.AddCurve("Inactive time", null, (double[])info.PointsList[2].YAxisData.ToArray(typeof(double)), Color.Red);

            zg.AxisChange();
            this.Refresh();
         }

        #endregion

        #region Worked time vs Week
        private ChartInfo GetWorkedTimeVsWeekData(GenerateChartArguments args)
        {
            ChartPointsList workedTimeList = new ChartPointsList();
            ChartPointsList activeTimeList = new ChartPointsList();
            ChartPointsList inactiveTimeList = new ChartPointsList();


            DateTime curDate = args.FromDate;
            DateTime toDate = args.ToDate;

            while (curDate <= toDate)
            {
                if (worker.CancellationPending)
                    return null;

                DateTime endcurDate = curDate.AddDays(7).AddSeconds(-1);
                int workedTime = TasksSummaries.GetWorkedTime(curDate, endcurDate);
                if (workedTime != 0)
                {
                    int activeTime = TasksSummaries.GetActiveTime(curDate, endcurDate);
                    int inactiveTime = workedTime - activeTime;
                    //XDate xDate = new XDate(curDate);
                    string week = curDate.ToShortDateString() + "-" + endcurDate.ToShortDateString();
                    workedTimeList.Add(week, Convert.ToDouble(workedTime * 1.0 / 3600));
                    activeTimeList.Add(week, Convert.ToDouble(activeTime * 1.0 / 3600));
                    inactiveTimeList.Add(week, Convert.ToDouble(inactiveTime * 1.0 / 3600));
                }
                curDate = curDate.AddDays(7);
            }
            return new ChartInfo(args, new ChartPointsList[] { workedTimeList, activeTimeList, inactiveTimeList });            
        }

        private void ChartWorkedTimeVsWeek(ChartInfo info)
        {
            zg.GraphPane.Title.Text = "Hrs. worked vs. Week";
            zg.GraphPane.XAxis.Title.Text = "Week";
            zg.GraphPane.YAxis.Title.Text = "Hrs.";
            zg.GraphPane.XAxis.Type = AxisType.Text;
            zg.GraphPane.XAxis.Scale.FontSpec.Angle = 80;

            zg.GraphPane.XAxis.Scale.TextLabels = (string[])info.PointsList[0].XAxisData.ToArray(typeof(string));
            zg.GraphPane.AddCurve("Worked time", null, (double[])info.PointsList[0].YAxisData.ToArray(typeof(double)), Color.Blue);
            zg.GraphPane.AddCurve("Active time", null, (double[])info.PointsList[1].YAxisData.ToArray(typeof(double)), Color.Green);
            zg.GraphPane.AddCurve("Inactive time", null, (double[])info.PointsList[2].YAxisData.ToArray(typeof(double)), Color.Red);

            zg.AxisChange();
            this.Refresh();
        }

        #endregion

        #region Worked time vs Month
        private ChartInfo GetWorkedTimeVsMonthData(GenerateChartArguments args)
        {
            ChartPointsList workedTimeList = new ChartPointsList();
            ChartPointsList activeTimeList = new ChartPointsList();
            ChartPointsList inactiveTimeList = new ChartPointsList();


            DateTime curDate = args.FromDate;
            DateTime toDate = args.ToDate;

            while (curDate <= toDate)
            {
                if (worker.CancellationPending)
                    return null;

                DateTime endcurDate = curDate.AddMonths(1).AddSeconds(-1);
                int workedTime = TasksSummaries.GetWorkedTime(curDate, endcurDate);
                if (workedTime != 0)
                {
                    int activeTime = TasksSummaries.GetActiveTime(curDate, endcurDate);
                    int inactiveTime = workedTime - activeTime;
                    //XDate xDate = new XDate(curDate);
                    string month = curDate.ToString("MMM-yyyy");
                    workedTimeList.Add(month, Convert.ToDouble(workedTime * 1.0 / 3600));
                    activeTimeList.Add(month, Convert.ToDouble(activeTime * 1.0 / 3600));
                    inactiveTimeList.Add(month, Convert.ToDouble(inactiveTime * 1.0 / 3600));
                }
                curDate = curDate.AddMonths(1);
            }
            return new ChartInfo(args, new ChartPointsList[] { workedTimeList, activeTimeList, inactiveTimeList });
        }

        private void ChartWorkedTimeVsMonth(ChartInfo info)
        {
            zg.GraphPane.Title.Text = "Hrs. worked vs. Month";
            zg.GraphPane.XAxis.Title.Text = "Month";
            zg.GraphPane.YAxis.Title.Text = "Hrs.";
            zg.GraphPane.XAxis.Type = AxisType.Text;
            zg.GraphPane.XAxis.Scale.FontSpec.Angle = 80;

            zg.GraphPane.XAxis.Scale.TextLabels = (string[])info.PointsList[0].XAxisData.ToArray(typeof(string));
            zg.GraphPane.AddCurve("Worked time", null, (double[])info.PointsList[0].YAxisData.ToArray(typeof(double)), Color.Blue);
            zg.GraphPane.AddCurve("Active time", null, (double[])info.PointsList[1].YAxisData.ToArray(typeof(double)), Color.Green);
            zg.GraphPane.AddCurve("Inactive time", null, (double[])info.PointsList[2].YAxisData.ToArray(typeof(double)), Color.Red);

            zg.AxisChange();
            this.Refresh();
        }
        #endregion


        private void SetWaitState()
        {
            this.Status = "Retrieving data...";
            zg.ZoomOutAll(zg.GraphPane);
            toDateTimePicker.Enabled = false;
            fromDateTimePicker.Enabled = false;
            this.generateButton.Enabled = false;
            this.chartComboBox.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            zg.GraphPane.CurveList.Clear();
            zg.Enabled = false;

            zg.GraphPane.Title.Text = String.Empty;
            zg.GraphPane.XAxis.Title.Text = String.Empty;
            zg.GraphPane.YAxis.Title.Text = String.Empty;
            
            this.Refresh();
            foreach (Control control in this.Controls)
            {
                control.Cursor = Cursors.WaitCursor;
            }
        }

        private void SetReadyState()
        {
            this.Status = "Ready";
            toDateTimePicker.Enabled = true;
            fromDateTimePicker.Enabled = true;
            this.chartComboBox.Enabled = true;
            this.generateButton.Enabled = true;
            this.Cursor = Cursors.Default;
            zg.Enabled = true;

            this.Refresh();
            foreach (Control control in this.Controls)
            {
                control.Cursor = Cursors.Default;
            }
        }


        private class GenerateChartArguments
        {
            private int selectedChart;
            private DateTime fromDate;
            private DateTime toDate;

            public int SelectedChart
            {
                get { return selectedChart; }
                set { selectedChart = value; }
            }

            public DateTime FromDate
            {
                get { return fromDate; }
                set { fromDate = value; }
            }

            public DateTime ToDate
            {
                get { return toDate; }
                set { toDate = value; }
            }
        }

        private class ChartInfo
        {
            public ChartInfo(GenerateChartArguments arguments, ChartPointsList[] pointsList)
            {
                this.arguments = arguments;
                this.pointsList = pointsList;
            }
            GenerateChartArguments arguments;
            private ChartPointsList[] pointsList;

            public GenerateChartArguments Arguments
            {
                get { return arguments; }
                set { arguments = value; }
            }

            public ChartPointsList[] PointsList
            {
                get { return pointsList; }
                set { pointsList = value; }
            }
        }


        private class ChartPointsList
        {
            private ArrayList xAxisData = new ArrayList();
            private ArrayList yAxisData = new ArrayList();

            public void Add(object x, object y)
            {
                this.xAxisData.Add(x);
                this.yAxisData.Add(y);
            }

            public ArrayList XAxisData
            {
                get { return xAxisData; }
                set { xAxisData = value; }
            }

            public ArrayList YAxisData
            {
                get { return yAxisData; }
                set { yAxisData = value; }
            }
        }


    }
}
