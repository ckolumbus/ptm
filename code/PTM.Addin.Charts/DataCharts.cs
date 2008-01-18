using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PTM.Framework;
using PTM.Framework.Infos;
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

            this.fromDateTimePicker.Value = DateTime.Today;
            this.toDateTimePicker.Value = DateTime.Today;

        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetReadyState();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            GetChartData((GenerateChartArguments) e.Argument);
            if (worker.CancellationPending)
                e.Cancel = true;
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
            SetWaitState();
            GenerateChartArguments args = new GenerateChartArguments();
            args.FromDate = this.fromDateTimePicker.Value;
            args.ToDate = this.toDateTimePicker.Value;
            args.SelectedChart = this.chartComboBox.SelectedIndex;
            if (worker.IsBusy) worker.CancelAsync();
            while (worker.IsBusy) Application.DoEvents();
            worker.RunWorkerAsync(args);
        }

        private void GetChartData(GenerateChartArguments arg)
        {
            //zg.GraphPane = new GraphPane();
            PointPairList workedTimeList = new PointPairList();
            PointPairList activeTimeList = new PointPairList();
            PointPairList inactiveTimeList = new PointPairList();

            DateTime curDate = this.fromDateTimePicker.Value.Date;
            DateTime toDate = this.toDateTimePicker.Value.Date;
            while (curDate <= toDate)
            {
                double xDate = new XDate(curDate);
                int workedTime = TasksSummaries.GetWorkedTime(curDate, curDate);
                if(workedTime!=0)
                {
                    int activeTime = TasksSummaries.GetActiveTime(curDate, curDate);
                    int inactiveTime = workedTime - activeTime;
                    workedTimeList.Add(xDate, workedTime / 3600);
                    activeTimeList.Add(xDate, activeTime / 3600);
                    inactiveTimeList.Add(xDate, inactiveTime / 3600);                   
                }
                curDate = curDate.AddDays(1);
            }

            zg.GraphPane.Title.Text = "Hrs. worked vs. Date";
            zg.GraphPane.XAxis.Title.Text = "Date";
            zg.GraphPane.XAxis.Scale.Format = "d";
            zg.GraphPane.YAxis.Title.Text = "Hrs.";
            zg.GraphPane.XAxis.Type = AxisType.DateAsOrdinal;
            zg.GraphPane.AddCurve("Worked time",
               workedTimeList, Color.Blue, SymbolType.Default);
            zg.GraphPane.AddCurve("Active time",
               activeTimeList, Color.Green, SymbolType.Default);
            zg.GraphPane.AddCurve("Inactive time",
               inactiveTimeList, Color.Red, SymbolType.Default);

            zg.AxisChange();
        }

        private void SetWaitState()
        {
            this.Status = "Retrieving data...";
            toDateTimePicker.Enabled = false;
            fromDateTimePicker.Enabled = false;
            this.generateButton.Enabled = false;
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
            private DateTime fromDate;
            private DateTime toDate;
            private int selectedChart;

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

            public int SelectedChart
            {
                get { return selectedChart; }
                set { selectedChart = value; }
            }
        }
    }
}
