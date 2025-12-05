namespace StudentManagement.GUI
{
    partial class KhoaListControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvKhoa = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).BeginInit();
            SuspendLayout();
            // 
            // dgvKhoa
            // 
            dgvKhoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhoa.Dock = DockStyle.Fill;
            dgvKhoa.Location = new Point(0, 0);
            dgvKhoa.Name = "dgvKhoa";
            dgvKhoa.RowHeadersWidth = 51;
            dgvKhoa.Size = new Size(1312, 657);
            dgvKhoa.TabIndex = 0;
            // 
            // KhoaListControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvKhoa);
            Name = "KhoaListControl";
            Size = new Size(1312, 657);
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvKhoa;
    }
}
