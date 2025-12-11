using System.Drawing;
using System.Windows.Forms;

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
        
        // Chỉ giữ lại TreeView để làm Menu điều hướng
        private System.Windows.Forms.TreeView treeViewNav;
        
        // ❌ ĐÃ LOẠI BỎ KHAI BÁO TabControl, TabPages và ThongKeControl

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            tsmiSinhVien = new ToolStripMenuItem();
            tsmiExit = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            treeViewNav = new TreeView();
            
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            this.SuspendLayout();

            // MenuStrip
            menuStrip1.Items.AddRange(new ToolStripItem[] {
                fileToolStripMenuItem
            });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(800, 24);
            menuStrip1.TabIndex = 0;
            
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                tsmiSinhVien,
                tsmiExit
            });
            
            tsmiSinhVien.Text = "Sinh Viên";
            tsmiSinhVien.Click += tsmiSinhVien_Click;

            tsmiExit.Text = "Thoát";
            tsmiExit.Click += tsmiExit_Click;

            // SplitContainer
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 24); // Dưới MenuStrip
            splitContainer1.Name = "splitContainer1";
            
            // Panel 1 (Left - Menu)
            splitContainer1.Panel1.Controls.Add(treeViewNav);
            splitContainer1.Panel1MinSize = 150;
            
            // Panel 2 (Right - Content Area)
            // Thiết lập Panel 2 thu nhỏ và Panel 1 cố định (khắc phục lỗi bố cục)
            splitContainer1.SplitterDistance = 250; 
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true; 
            splitContainer1.Panel2MinSize = 0; 
            
            splitContainer1.Size = new System.Drawing.Size(800, 426);
            splitContainer1.TabIndex = 1;

            // TreeView
            treeViewNav.Dock = DockStyle.Fill;
            treeViewNav.Location = new System.Drawing.Point(0, 0);
            treeViewNav.Name = "treeViewNav";
            treeViewNav.Size = new System.Drawing.Size(250, 402); 
            treeViewNav.TabIndex = 0;
            treeViewNav.AfterSelect += treeViewNav_AfterSelect;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(splitContainer1);
            this.Controls.Add(menuStrip1);
            
            this.MainMenuStrip = menuStrip1;
            this.Name = "MainForm";
            this.Text = "Quản lý Sinh viên";
            this.WindowState = FormWindowState.Maximized;
            
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}