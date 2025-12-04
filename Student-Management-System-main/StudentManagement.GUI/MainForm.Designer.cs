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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSinhVien = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewNav = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSinhVien = new System.Windows.Forms.TabPage();
            this.tabPageThongKe = new System.Windows.Forms.TabPage();
            this.sinhVienListControl = new SinhVienListControl();
            this.thongKeControl = new ThongKeControl();
            this.lblTitle = new System.Windows.Forms.Label();

            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSinhVien.SuspendLayout();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";

            // fileToolStripMenuItem
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tsmiSinhVien,
                this.tsmiExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";

            // tsmiSinhVien
            this.tsmiSinhVien.Name = "tsmiSinhVien";
            this.tsmiSinhVien.Size = new System.Drawing.Size(152, 22);
            this.tsmiSinhVien.Text = "Sinh Viên";
            this.tsmiSinhVien.Click += new System.EventHandler(this.tsmiSinhVien_Click);

            // tsmiExit
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(152, 22);
            this.tsmiExit.Text = "Thoát";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);

            // splitContainer1
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // left panel for navigation
            this.splitContainer1.Panel1.Controls.Add(this.treeViewNav);
            // right panel for main content
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 550);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.TabIndex = 1;

            // treeViewNav
            this.treeViewNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewNav.Location = new System.Drawing.Point(0, 0);
            this.treeViewNav.Name = "treeViewNav";
            this.treeViewNav.Size = new System.Drawing.Size(220, 550);
            this.treeViewNav.TabIndex = 0;
            this.treeViewNav.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewNav_AfterSelect);

            // tabControl1
            this.tabControl1.Controls.Add(this.tabPageSinhVien);
            this.tabControl1.Controls.Add(this.tabPageThongKe);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.TabIndex = 1;

            // tabPageSinhVien
            this.tabPageSinhVien.Controls.Add(this.sinhVienListControl);
            this.tabPageSinhVien.Location = new System.Drawing.Point(4, 24);
            this.tabPageSinhVien.Name = "tabPageSinhVien";
            this.tabPageSinhVien.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSinhVien.Size = new System.Drawing.Size(772, 522);
            this.tabPageSinhVien.TabIndex = 0;
            this.tabPageSinhVien.Text = "Quản lý Sinh Viên";
            this.tabPageSinhVien.UseVisualStyleBackColor = true;

            // tabPageThongKe
            this.tabPageThongKe.Controls.Add(this.thongKeControl);
            this.tabPageThongKe.Location = new System.Drawing.Point(4, 24);
            this.tabPageThongKe.Name = "tabPageThongKe";
            this.tabPageThongKe.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageThongKe.Size = new System.Drawing.Size(772, 522);
            this.tabPageThongKe.TabIndex = 1;
            this.tabPageThongKe.Text = "Thống kê";
            this.tabPageThongKe.UseVisualStyleBackColor = true;

            // sinhVienListControl
            this.sinhVienListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sinhVienListControl.Location = new System.Drawing.Point(3, 3);
            this.sinhVienListControl.Name = "sinhVienListControl";
            this.sinhVienListControl.Size = new System.Drawing.Size(766, 516);
            this.sinhVienListControl.TabIndex = 0;

            // thongKeControl
            this.thongKeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thongKeControl.Location = new System.Drawing.Point(3, 3);
            this.thongKeControl.Name = "thongKeControl";
            this.thongKeControl.Size = new System.Drawing.Size(766, 516);
            this.thongKeControl.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Hệ Thống Quản Lý Sinh Viên";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Quản Lý Sinh Viên";
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPageSinhVien.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
