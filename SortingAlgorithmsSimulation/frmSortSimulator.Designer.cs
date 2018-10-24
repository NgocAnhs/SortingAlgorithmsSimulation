namespace SortingAlgorithmsSimulation
{
    partial class frmSortSimulator
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCreateArr = new System.Windows.Forms.Button();
            this.txtNumArr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboSortAlgo = new System.Windows.Forms.ComboBox();
            this.btnSort = new System.Windows.Forms.Button();
            this.pnlSimulator = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.bgwSimuSort = new System.ComponentModel.BackgroundWorker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSortInfo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCreateArr);
            this.groupBox1.Controls.Add(this.txtNumArr);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 186);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tạo Mảng";
            // 
            // btnCreateArr
            // 
            this.btnCreateArr.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnCreateArr.Location = new System.Drawing.Point(153, 121);
            this.btnCreateArr.Name = "btnCreateArr";
            this.btnCreateArr.Size = new System.Drawing.Size(143, 42);
            this.btnCreateArr.TabIndex = 2;
            this.btnCreateArr.Text = "Khởi tạo";
            this.btnCreateArr.UseVisualStyleBackColor = true;
            this.btnCreateArr.Click += new System.EventHandler(this.btnCreateArr_Click);
            // 
            // txtNumArr
            // 
            this.txtNumArr.Location = new System.Drawing.Point(133, 57);
            this.txtNumArr.Name = "txtNumArr";
            this.txtNumArr.Size = new System.Drawing.Size(206, 38);
            this.txtNumArr.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 62);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số phần tử mảng:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboSortAlgo);
            this.groupBox2.Controls.Add(this.btnSort);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(402, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 186);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thuật toán";
            // 
            // cboSortAlgo
            // 
            this.cboSortAlgo.FormattingEnabled = true;
            this.cboSortAlgo.Items.AddRange(new object[] {
            "Interchange Sort",
            "Insertion Sort",
            "Selection Sort",
            "Bubble Sort",
            "Heap Sort",
            "Quick Sort (first pivot)",
            "Merge Sort",
            "Shell Sort"});
            this.cboSortAlgo.Location = new System.Drawing.Point(53, 57);
            this.cboSortAlgo.Name = "cboSortAlgo";
            this.cboSortAlgo.Size = new System.Drawing.Size(284, 39);
            this.cboSortAlgo.TabIndex = 7;
            // 
            // btnSort
            // 
            this.btnSort.Enabled = false;
            this.btnSort.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnSort.Location = new System.Drawing.Point(108, 121);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(143, 42);
            this.btnSort.TabIndex = 6;
            this.btnSort.Text = "Sắp xếp";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // pnlSimulator
            // 
            this.pnlSimulator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSimulator.AutoScroll = true;
            this.pnlSimulator.BackColor = System.Drawing.Color.White;
            this.pnlSimulator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSimulator.Location = new System.Drawing.Point(12, 265);
            this.pnlSimulator.Name = "pnlSimulator";
            this.pnlSimulator.Size = new System.Drawing.Size(982, 439);
            this.pnlSimulator.TabIndex = 4;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(8, 57);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(185, 56);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // bgwSimuSort
            // 
            this.bgwSimuSort.WorkerReportsProgress = true;
            this.bgwSimuSort.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSimuSort_DoWork);
            this.bgwSimuSort.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwSimuSort_ProgressChanged);
            this.bgwSimuSort.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwSimuSort_RunWorkerCompleted);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.trackBar1);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(792, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(201, 186);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Điều Khiển";
            // 
            // lblSortInfo
            // 
            this.lblSortInfo.Location = new System.Drawing.Point(422, 207);
            this.lblSortInfo.Name = "lblSortInfo";
            this.lblSortInfo.Size = new System.Drawing.Size(160, 47);
            this.lblSortInfo.TabIndex = 0;
            this.lblSortInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSortSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 721);
            this.Controls.Add(this.lblSortInfo);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pnlSimulator);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmSortSimulator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mô phỏng thuật toán sắp xếp";
            this.Load += new System.EventHandler(this.frmSortSimulator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnlSimulator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateArr;
        private System.Windows.Forms.TextBox txtNumArr;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ComboBox cboSortAlgo;
        private System.ComponentModel.BackgroundWorker bgwSimuSort;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblSortInfo;
    }
}

