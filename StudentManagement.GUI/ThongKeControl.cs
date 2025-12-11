using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Linq;
using StudentManagement.BLL;
using StudentManagement.DTO; // Đảm bảo DTO được tham chiếu

namespace StudentManagement.GUI
{
    public partial class ThongKeControl : UserControl
    {
        private readonly SinhVienBLL _svBLL = new SinhVienBLL();
        private readonly LopBLL _lopBLL = new LopBLL();

        public ThongKeControl()
        {
            InitializeComponent();
            
            // THÊM: Đăng ký sự kiện Load để đảm bảo biểu đồ được tải khi control hiển thị.
            this.Load += ThongKeControl_Load;
        }
        
        // HÀM MỚI: Xử lý sự kiện Load của UserControl
        private void ThongKeControl_Load(object sender, EventArgs e)
        {
            LoadChart();
        }

        public void LoadChart()
        {
            try
            {
                // Kiểm tra ChartArea đã tồn tại
                if (chart1.ChartAreas.Count == 0)
                {
                     chart1.ChartAreas.Add(new ChartArea("MainArea"));
                }
                
                // Get all students
                // LƯU Ý: Nếu lỗi vẫn xảy ra, lỗi nằm trong _svBLL.GetAllStudents()
                var students = _svBLL.GetAllStudents();
                
                // Group by class
                var groupedByClass = students.GroupBy(s => s.MaLop)
                    .Select(g => new { Class = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count) // Sắp xếp theo số lượng giảm dần
                    .ToList();
                
                // --- SETUP CHART ---
                
                chart1.Series.Clear();
                chart1.Titles.Clear();

                var series = chart1.Series.Add("Số lượng sinh viên");
                series.ChartType = SeriesChartType.Column;
                series.IsValueShownAsLabel = true; // Hiển thị giá trị trên cột

                foreach (var item in groupedByClass)
                {
                    // Lớp 'null' (chưa được gán) sẽ được hiển thị là "Không rõ"
                    series.Points.AddXY(item.Class ?? "Không rõ", item.Count);
                }

                // Cấu hình trục và tiêu đề
                chart1.ChartAreas[0].AxisX.Title = "Lớp học";
                chart1.ChartAreas[0].AxisY.Title = "Số lượng";
                chart1.Titles.Add("Thống kê sinh viên theo lớp");
            }
            catch (Exception ex)
            {
                // Thông báo lỗi rõ ràng nếu BLL/DAL gặp vấn đề
                MessageBox.Show("Lỗi khi tải thống kê: Vui lòng kiểm tra kết nối CSDL và hàm GetAllStudents() trong BLL/DAL. Chi tiết lỗi: " + ex.Message, "Lỗi");
            }
        }
    }
}
