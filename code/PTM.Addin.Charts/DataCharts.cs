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

        private ArrayList parentTasksList = new ArrayList();

        public DataCharts()
        {
            InitializeComponent();
            this.chartComboBox.SelectedIndex = 0;
            this.Text = "Charts";
            this.Status = "Ready";

            //Task parentTask;
            //parentTask = Tasks.RootTask;
            //parentTasksList.Add(parentTask);
            //this.parentTaskComboBox.DisplayMember = "Description";
            //this.parentTaskComboBox.ValueMember = "Id";
            //this.parentTaskComboBox.DataSource = parentTasksList;
            //parentTaskComboBox.SelectedIndex = 0;

            this.fromDateTimePicker.Value = DateTime.Today;
            this.toDateTimePicker.Value = DateTime.Today;

        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            zg.GraphPane = new GraphPane();
            PointPairList workedTimeList = new PointPairList();
            PointPairList activeTimeList = new PointPairList();
            PointPairList inactiveTimeList = new PointPairList();

            DateTime curDate = this.fromDateTimePicker.Value.Date;
            DateTime toDate = this.toDateTimePicker.Value.Date;
            while (curDate <= toDate)
			{
                double xDate = new XDate(curDate);
                int workedTime = TasksSummaries.GetWorkedTime(curDate, curDate);
                int activeTime = TasksSummaries.GetActiveTime(curDate, curDate);
			    int inactiveTime = workedTime - activeTime;
                workedTimeList.Add(xDate, workedTime / 3600);
                activeTimeList.Add(xDate, activeTime / 3600);
                inactiveTimeList.Add(xDate, inactiveTime / 3600);
    		    curDate = curDate.AddDays(1);
			}

            zg.GraphPane.Title.Text = "Hrs. worked vs. Date";
            zg.GraphPane.XAxis.Title.Text = "Date";
            zg.GraphPane.YAxis.Title.Text = "Hrs.";
            zg.GraphPane.XAxis.Type = AxisType.Date;
            zg.GraphPane.AddCurve("Worked time",
               workedTimeList, Color.Blue, SymbolType.Default);
            zg.GraphPane.AddCurve("Active time",
               activeTimeList, Color.Green, SymbolType.Default);
            zg.GraphPane.AddCurve("Inactive time",
               inactiveTimeList, Color.Red, SymbolType.Default); 

            zg.AxisChange();
        }


    }
}
