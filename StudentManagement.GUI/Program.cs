using System;
using System.Windows.Forms;
using StudentManagement.GUI; // Đảm bảo gọi đúng namespace chứa LoginForm và MainForm

namespace StudentManagement.GUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Cấu hình khởi tạo ứng dụng (Tùy thuộc vào .NET Framework/Core)
            // Nếu dùng .NET Core:
            // ApplicationConfiguration.Initialize(); 
            
            // Nếu dùng .NET Framework (mã nguồn cũ hơn):
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // 1. Khởi tạo Form Đăng nhập
            using (LoginForm loginForm = new LoginForm())
            {
                // 2. Hiển thị Form Đăng nhập
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // 3. Nếu đăng nhập thành công, chạy Form Chính (MainForm)
                    Application.Run(new MainForm());
                }
                else
                {
                    // 4. Nếu người dùng thoát khỏi LoginForm (DialogResult khác OK), chương trình dừng
                    return; 
                }
            }
        }
    }
}