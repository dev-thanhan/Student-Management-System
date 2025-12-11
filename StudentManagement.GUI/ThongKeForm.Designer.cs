namespace StudentManagement.GUI
{
    partial class ThongKeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // ====================================================
            // ĐẶT KÍCH THƯỚC MỚI (1600 x 900)
            // ====================================================
            this.SuspendLayout(); 

            // Đặt kích thước: 1600 pixels (Chiều rộng) và 900 pixels (Chiều cao)
            this.ClientSize = new System.Drawing.Size(1600, 900); 
            
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "Thống kê Sinh viên";
            
            // Cấu hình vị trí khởi động Form ở giữa màn hình
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            // Tùy chọn: Đặt Form ở chế độ Maximize khi khởi động
            // this.WindowState = System.Windows.Forms.FormWindowState.Maximized; 

            this.ResumeLayout(false); 
        }

        #endregion
    }
}