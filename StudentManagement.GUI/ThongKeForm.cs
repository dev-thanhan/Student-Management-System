using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement.GUI
{
    public partial class ThongKeForm : Form
    {
        // Khai báo Control
        private ThongKeControl thongKeControl;

        public ThongKeForm()
        {
            InitializeComponent();
            
            // --- Thiết lập thuộc tính Form ---
            this.Text = "Thống kê Sinh viên theo Lớp";
            
            // CHỈNH SỬA TẠI ĐÂY: Tăng kích thước lên 1600x900 (gấp đôi kích thước cũ)
            this.Size = new Size(1600, 900); // <--- ĐÃ SỬA KÍCH THƯỚC GHI ĐÈ
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimizeBox = true;
            this.MaximizeBox = true;
            
            // 1. Khởi tạo Control Thống kê
            thongKeControl = new ThongKeControl();
            
            // 2. Thiết lập Dock để lấp đầy Form
            thongKeControl.Dock = DockStyle.Fill;
            
            // 3. Thêm Control vào Form
            this.Controls.Add(thongKeControl);
            
            // Đăng ký sự kiện Load để tải biểu đồ
            this.Load += ThongKeForm_Load;
        }

        private void ThongKeForm_Load(object sender, EventArgs e)
        {
            // QUAN TRỌNG: Gọi hàm tải dữ liệu ngay khi Form hiển thị
            thongKeControl.LoadChart();
        }
        
        // Bạn có thể thêm các hàm xử lý sự kiện khác tại đây
    }
}