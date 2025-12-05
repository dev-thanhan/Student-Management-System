using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Linq;
using StudentManagement.BLL;

namespace StudentManagement.GUI
{
    public partial class ThongKeControl : UserControl
    {
        private readonly SinhVienBLL _svBLL = new SinhVienBLL();
        private readonly LopBLL _lopBLL = new LopBLL();

        public ThongKeControl()
        {
            InitializeComponent();
        }

        public void LoadChart()
        {
            try
            {
                // Get all students
                var students = _svBLL.GetAllStudents();
                
                // Group by class
                var groupedByClass = students.GroupBy(s => s.MaLop)
                    .Select(g => new { Class = g.Key, Count = g.Count() })
                    .ToList();

                // Setup chart
                chart1.Series.Clear();
                var series = chart1.Series.Add("Số lượng sinh viên");
                series.ChartType = SeriesChartType.Column;

                foreach (var item in groupedByClass)
                {
                    series.Points.AddXY(item.Class, item.Count);
                }

                chart1.ChartAreas[0].AxisX.Title = "Lớp học";
                chart1.ChartAreas[0].AxisY.Title = "Số lượng";
                chart1.Titles.Add("Thống kê sinh viên theo lớp");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê: " + ex.Message, "Lỗi");
            }
        }
    }
}
