namespace StudentManagement.GUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSinhVien;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSinhVien;
        private System.Windows.Forms.TabPage tabPageThongKe;
        private System.Windows.Forms.Label lblTitle;
        private SinhVienListControl sinhVienListControl;
        private ThongKeControl thongKeControl;
        private System.Windows.Forms.TreeView treeViewNav;

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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            tsmiSinhVien = new ToolStripMenuItem();
            tsmiExit = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            treeViewNav = new TreeView();
            tabControl1 = new TabControl();
            tabPageSinhVien = new TabPage();
            sinhVienListControl = new SinhVienListControl();
            tabPageThongKe = new TabPage();
            thongKeControl = new ThongKeControl();
            lblTitle = new Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageSinhVien.SuspendLayout();
            tabPageThongKe.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(1143, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmiSinhVien, tsmiExit });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // tsmiSinhVien
            // 
            tsmiSinhVien.Name = "tsmiSinhVien";
            tsmiSinhVien.Size = new Size(153, 26);
            tsmiSinhVien.Text = "Sinh Viên";
            tsmiSinhVien.Click += tsmiSinhVien_Click;
            // 
            // tsmiExit
            // 
            tsmiExit.Name = "tsmiExit";
            tsmiExit.Size = new Size(153, 26);
            tsmiExit.Text = "Thoát";
            tsmiExit.Click += tsmiExit_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(0, 36);
            splitContainer1.Margin = new Padding(3, 4, 3, 4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeViewNav);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Size = new Size(1143, 733);
            splitContainer1.SplitterDistance = 251;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 1;
            // 
            // treeViewNav
            // 
            treeViewNav.Dock = DockStyle.Fill;
            treeViewNav.Location = new Point(0, 0);
            treeViewNav.Margin = new Padding(3, 4, 3, 4);
            treeViewNav.Name = "treeViewNav";
            treeViewNav.Size = new Size(251, 733);
            treeViewNav.TabIndex = 0;
            treeViewNav.AfterSelect += treeViewNav_AfterSelect;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageSinhVien);
            tabControl1.Controls.Add(tabPageThongKe);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(3, 4, 3, 4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(887, 733);
            tabControl1.TabIndex = 1;
            // 
            // tabPageSinhVien
            // 
            tabPageSinhVien.Controls.Add(sinhVienListControl);
            tabPageSinhVien.Location = new Point(4, 29);
            tabPageSinhVien.Margin = new Padding(3, 4, 3, 4);
            tabPageSinhVien.Name = "tabPageSinhVien";
            tabPageSinhVien.Padding = new Padding(3, 4, 3, 4);
            tabPageSinhVien.Size = new Size(879, 700);
            tabPageSinhVien.TabIndex = 0;
            tabPageSinhVien.Text = "Quản lý Sinh Viên";
            tabPageSinhVien.UseVisualStyleBackColor = true;
            // 
            // sinhVienListControl
            // 
            sinhVienListControl.Dock = DockStyle.Fill;
            sinhVienListControl.Font = new Font("Segoe UI", 9F);
            sinhVienListControl.Location = new Point(3, 4);
            sinhVienListControl.Margin = new Padding(3, 5, 3, 5);
            sinhVienListControl.Name = "sinhVienListControl";
            sinhVienListControl.Size = new Size(873, 692);
            sinhVienListControl.TabIndex = 0;
            sinhVienListControl.Load += sinhVienListControl_Load;
            // 
            // tabPageThongKe
            // 
            tabPageThongKe.Controls.Add(thongKeControl);
            tabPageThongKe.Location = new Point(4, 29);
            tabPageThongKe.Margin = new Padding(3, 4, 3, 4);
            tabPageThongKe.Name = "tabPageThongKe";
            tabPageThongKe.Padding = new Padding(3, 4, 3, 4);
            tabPageThongKe.Size = new Size(880, 700);
            tabPageThongKe.TabIndex = 1;
            tabPageThongKe.Text = "Thống kê";
            tabPageThongKe.UseVisualStyleBackColor = true;
            // 
            // thongKeControl
            // 
            thongKeControl.Dock = DockStyle.Fill;
            thongKeControl.Font = new Font("Segoe UI", 9F);
            thongKeControl.Location = new Point(3, 4);
            thongKeControl.Margin = new Padding(3, 5, 3, 5);
            thongKeControl.Name = "thongKeControl";
            thongKeControl.Size = new Size(874, 692);
            thongKeControl.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(250, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Hệ Thống Quản Lý Sinh Viên";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 800);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 9F);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Quản Lý Sinh Viên";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPageSinhVien.ResumeLayout(false);
            tabPageThongKe.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
