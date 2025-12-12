namespace StudentManagement.GUI
{
    partial class ThongKeControl
    {
        private System.ComponentModel.IContainer components = null;
        
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLop;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGioiTinh;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNamSinh;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGPA;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControlThongKe;
        private System.Windows.Forms.TabPage tpLop;
        private System.Windows.Forms.TabPage tpGioiTinh;
        private System.Windows.Forms.TabPage tpNamSinh;
        private System.Windows.Forms.TabPage tpGPA;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ======================================================================
            // KHAI BÁO CÁC BIẾN CẤU HÌNH (ĐÃ SỬA LỖI CS0103)
            // ======================================================================
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            
            // Khởi tạo controls
            this.tabControlThongKe = new System.Windows.Forms.TabControl();
            this.tpLop = new System.Windows.Forms.TabPage();
            this.tpGioiTinh = new System.Windows.Forms.TabPage();
            this.tpNamSinh = new System.Windows.Forms.TabPage();
            this.tpGPA = new System.Windows.Forms.TabPage();
            this.chartLop = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartGioiTinh = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartNamSinh = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartGPA = new System.Windows.Forms.DataVisualization.Charting.Chart();
            
            // Setup
            this.tabControlThongKe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGioiTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNamSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGPA)).BeginInit();
            this.SuspendLayout();

            // 
            // tabControlThongKe (Container chính)
            // 
            this.tabControlThongKe.Controls.Add(this.tpLop);
            this.tabControlThongKe.Controls.Add(this.tpGioiTinh);
            this.tabControlThongKe.Controls.Add(this.tpNamSinh);
            this.tabControlThongKe.Controls.Add(this.tpGPA);
            this.tabControlThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlThongKe.Location = new System.Drawing.Point(0, 0);
            this.tabControlThongKe.Name = "tabControlThongKe";
            this.tabControlThongKe.SelectedIndex = 0;
            this.tabControlThongKe.Size = new System.Drawing.Size(1200, 800);
            this.tabControlThongKe.TabIndex = 0;

            // 
            // tpLop (Tab 1: Thống kê theo Lớp)
            // 
            this.tpLop.Controls.Add(this.chartLop);
            this.tpLop.Location = new System.Drawing.Point(4, 29);
            this.tpLop.Name = "tpLop";
            this.tpLop.Padding = new System.Windows.Forms.Padding(3);
            this.tpLop.Size = new System.Drawing.Size(1192, 767);
            this.tpLop.TabIndex = 0;
            this.tpLop.Text = "Theo Lớp";
            this.tpLop.UseVisualStyleBackColor = true;

            // 
            // tpGioiTinh (Tab 2: Thống kê theo Giới tính)
            // 
            this.tpGioiTinh.Controls.Add(this.chartGioiTinh);
            this.tpGioiTinh.Location = new System.Drawing.Point(4, 29);
            this.tpGioiTinh.Name = "tpGioiTinh";
            this.tpGioiTinh.Padding = new System.Windows.Forms.Padding(3);
            this.tpGioiTinh.Size = new System.Drawing.Size(1192, 767);
            this.tpGioiTinh.TabIndex = 1;
            this.tpGioiTinh.Text = "Theo Giới tính";
            this.tpGioiTinh.UseVisualStyleBackColor = true;

            // 
            // tpNamSinh (Tab 3: Thống kê theo Năm sinh)
            // 
            this.tpNamSinh.Controls.Add(this.chartNamSinh);
            this.tpNamSinh.Location = new System.Drawing.Point(4, 29);
            this.tpNamSinh.Name = "tpNamSinh";
            this.tpNamSinh.Padding = new System.Windows.Forms.Padding(3);
            this.tpNamSinh.Size = new System.Drawing.Size(1192, 767);
            this.tpNamSinh.TabIndex = 2;
            this.tpNamSinh.Text = "Theo Năm sinh";
            this.tpNamSinh.UseVisualStyleBackColor = true;

            // 
            // tpGPA (Tab 4: Thống kê theo GPA)
            // 
            this.tpGPA.Controls.Add(this.chartGPA);
            this.tpGPA.Location = new System.Drawing.Point(4, 29);
            this.tpGPA.Name = "tpGPA";
            this.tpGPA.Padding = new System.Windows.Forms.Padding(3);
            this.tpGPA.Size = new System.Drawing.Size(1192, 767);
            this.tpGPA.TabIndex = 3;
            this.tpGPA.Text = "Theo GPA";
            this.tpGPA.UseVisualStyleBackColor = true;
            
            // --- CẤU HÌNH CÁC CHARTS --- (Bên trong TabPages)
            
            // Cấu hình chartLop
            chartArea1.Name = "ChartAreaLop";
            this.chartLop.ChartAreas.Add(chartArea1);
            this.chartLop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartLop.Legends.Add(legend1);
            this.chartLop.Name = "chartLop";
            this.chartLop.TabIndex = 0;

            // Cấu hình chartGioiTinh
            chartArea2.Name = "ChartAreaGioiTinh";
            this.chartGioiTinh.ChartAreas.Add(chartArea2);
            this.chartGioiTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartGioiTinh.Legends.Add(legend2);
            this.chartGioiTinh.Name = "chartGioiTinh";
            this.chartGioiTinh.TabIndex = 1;

            // Cấu hình chartNamSinh
            chartArea3.Name = "ChartAreaNamSinh";
            this.chartNamSinh.ChartAreas.Add(chartArea3);
            this.chartNamSinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartNamSinh.Legends.Add(legend3);
            this.chartNamSinh.Name = "chartNamSinh";
            this.chartNamSinh.TabIndex = 2;

            // Cấu hình chartGPA
            chartArea4.Name = "ChartAreaGPA";
            this.chartGPA.ChartAreas.Add(chartArea4);
            this.chartGPA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartGPA.Legends.Add(legend4);
            this.chartGPA.Name = "chartGPA";
            this.chartGPA.TabIndex = 3;

            // 
            // ThongKeControl (Container chính)
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlThongKe); // Chỉ thêm TabControl
            this.Name = "ThongKeControl";
            this.Size = new System.Drawing.Size(1200, 800); // Kích thước lớn hơn
            
            // End Init
            ((System.ComponentModel.ISupportInitialize)(this.chartLop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGioiTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNamSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGPA)).EndInit();
            this.tabControlThongKe.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}