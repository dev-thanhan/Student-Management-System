using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using StudentManagement.BLL;
using StudentManagement.DTO;
using System.Drawing; 

namespace StudentManagement.GUI
{
    public partial class ThongKeControl : UserControl
    {
        private readonly SinhVienBLL _svBLL = new SinhVienBLL();
        private readonly LopBLL _lopBLL = new LopBLL(); 
        
        // <<< BỎ CÁC KHAI BÁO CHART LẶP LẠI Ở ĐÂY >>>
        // private Chart chartLop;
        // private Chart chartGioiTinh;
        // ...
        
        public ThongKeControl()
        {
            InitializeComponent();
            this.Load += ThongKeControl_Load;
            
            // <<< BỎ PHƯƠNG THỨC SetupLayout() Ở ĐÂY >>>
            // SetupLayout(); 
        }

        private void ThongKeControl_Load(object sender, EventArgs e)
        {
            LoadChart();
        }

        public void LoadChart()
        {
            try
            {
                // Gọi các phương thức tải dữ liệu và vẽ biểu đồ
                LoadChartLop();
                LoadChartGioiTinh();
                LoadChartNamSinh();
                LoadChartGPA();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê: Vui lòng kiểm tra kết nối CSDL và các hàm BLL/DAL. Chi tiết lỗi: " + ex.Message, "Lỗi");
            }
        }
        
        // HÀM 1: Thống kê theo Lớp
        private void LoadChartLop()
        {
            var students = _svBLL.GetAllStudents();
            var groupedByClass = students.GroupBy(s => s.MaLop)
                .Select(g => new { Class = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            chartLop.Series.Clear();
            chartLop.Titles.Clear();
            chartLop.Titles.Add("Thống kê sinh viên theo Lớp");
            
            var series = chartLop.Series.Add("Số lượng sinh viên");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;

            foreach (var item in groupedByClass)
            {
                series.Points.AddXY(item.Class ?? "Không rõ", item.Count);
            }
            chartLop.ChartAreas[0].AxisX.Title = "Lớp học";
            chartLop.ChartAreas[0].AxisY.Title = "Số lượng";
        }
        
        // HÀM 2: Thống kê theo Giới tính
        private void LoadChartGioiTinh()
        {
            var data = _svBLL.ThongKeTheoGioiTinh(); 

            chartGioiTinh.Series.Clear();
            chartGioiTinh.Titles.Clear();
            chartGioiTinh.Titles.Add("Thống kê sinh viên theo Giới tính");
            
            var series = chartGioiTinh.Series.Add("Giới tính");
            series.ChartType = SeriesChartType.Pie; 
            series.IsValueShownAsLabel = true;

            foreach (var item in data)
            {
                series.Points.AddXY(item.Key, item.Value);
                series.Points.Last().Label = item.Key + " (" + item.Value.ToString() + ")";
            }
        }

        // HÀM 3: Thống kê theo Năm sinh
        private void LoadChartNamSinh()
        {
            var data = _svBLL.ThongKeTheoNamSinh();

            chartNamSinh.Series.Clear();
            chartNamSinh.Titles.Clear();
            chartNamSinh.Titles.Add("Thống kê sinh viên theo Năm sinh");
            
            var series = chartNamSinh.Series.Add("Năm sinh");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;

            foreach (var item in data.OrderBy(x => x.Key))
            {
                series.Points.AddXY(item.Key.ToString(), item.Value);
            }
            chartNamSinh.ChartAreas[0].AxisX.Title = "Năm sinh";
            chartNamSinh.ChartAreas[0].AxisY.Title = "Số lượng";
        }

        // HÀM 4: Thống kê theo GPA/Mức điểm
        private void LoadChartGPA()
        {
            var data = _svBLL.ThongKeTheoGPA();

            chartGPA.Series.Clear();
            chartGPA.Titles.Clear();
            chartGPA.Titles.Add("Thống kê sinh viên theo Mức GPA");
            
            var series = chartGPA.Series.Add("Mức GPA");
            series.ChartType = SeriesChartType.Bar;
            series.IsValueShownAsLabel = true;

            foreach (var item in data)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
            chartGPA.ChartAreas[0].AxisX.Title = "Mức điểm";
            chartGPA.ChartAreas[0].AxisY.Title = "Số lượng";
        }
    }
}