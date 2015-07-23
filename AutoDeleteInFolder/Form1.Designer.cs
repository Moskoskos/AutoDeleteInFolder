namespace AutoDeleteInFolder
{
    partial class Form1
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalFiles = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOldest = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCurFiles = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCurSize = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCurOld = new System.Windows.Forms.TextBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(337, 49);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(83, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Monitored Folder";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(15, 52);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(312, 20);
            this.txtPath.TabIndex = 2;
            this.txtPath.Text = "Example: C:\\Users\\Admin\\Moves\\Recorded";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Maximum Amount of Files";
            // 
            // txtTotalFiles
            // 
            this.txtTotalFiles.Location = new System.Drawing.Point(15, 107);
            this.txtTotalFiles.Name = "txtTotalFiles";
            this.txtTotalFiles.Size = new System.Drawing.Size(63, 20);
            this.txtTotalFiles.TabIndex = 4;
            this.txtTotalFiles.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Maximum Size of Folder Allowed(GB)";
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(15, 165);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(63, 20);
            this.txtSize.TabIndex = 7;
            this.txtSize.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Delete Files Older Than (Days)";
            // 
            // txtOldest
            // 
            this.txtOldest.Location = new System.Drawing.Point(15, 220);
            this.txtOldest.Name = "txtOldest";
            this.txtOldest.Size = new System.Drawing.Size(63, 20);
            this.txtOldest.TabIndex = 10;
            this.txtOldest.Text = "0";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(12, 263);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(83, 23);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Current Files in Folder";
            // 
            // txtCurFiles
            // 
            this.txtCurFiles.Location = new System.Drawing.Point(220, 107);
            this.txtCurFiles.Name = "txtCurFiles";
            this.txtCurFiles.ReadOnly = true;
            this.txtCurFiles.Size = new System.Drawing.Size(62, 20);
            this.txtCurFiles.TabIndex = 13;
            this.txtCurFiles.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Current Size Allocated";
            // 
            // txtCurSize
            // 
            this.txtCurSize.Location = new System.Drawing.Point(220, 165);
            this.txtCurSize.Name = "txtCurSize";
            this.txtCurSize.ReadOnly = true;
            this.txtCurSize.Size = new System.Drawing.Size(62, 20);
            this.txtCurSize.TabIndex = 15;
            this.txtCurSize.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(217, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Oldest File";
            // 
            // txtCurOld
            // 
            this.txtCurOld.Location = new System.Drawing.Point(220, 220);
            this.txtCurOld.Name = "txtCurOld";
            this.txtCurOld.ReadOnly = true;
            this.txtCurOld.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtCurOld.Size = new System.Drawing.Size(200, 20);
            this.txtCurOld.TabIndex = 17;
            this.txtCurOld.Text = "0";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(217, 243);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(10, 13);
            this.lblAge.TabIndex = 18;
            this.lblAge.Text = " ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 318);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.txtCurOld);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCurSize);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCurFiles);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtOldest);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTotalFiles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowse);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOldest;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCurFiles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCurSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCurOld;
        private System.Windows.Forms.Label lblAge;
    }
}

