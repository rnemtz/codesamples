namespace SparkEjs
{
    partial class Main
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
            this.openSparkFile = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.folderSparkFiles = new System.Windows.Forms.FolderBrowserDialog();
            this.lblFolderPath = new System.Windows.Forms.Label();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openSparkFile
            // 
            this.openSparkFile.FileName = "openSparkFile";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(6, 30);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(297, 122);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Select Spark Files Folder";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(6, 30);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(297, 122);
            this.btnTranslate.TabIndex = 1;
            this.btnTranslate.Text = "Transform";
            this.btnTranslate.UseVisualStyleBackColor = true;
            // 
            // lblFolderPath
            // 
            this.lblFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolderPath.Location = new System.Drawing.Point(327, 30);
            this.lblFolderPath.Name = "lblFolderPath";
            this.lblFolderPath.Size = new System.Drawing.Size(856, 97);
            this.lblFolderPath.TabIndex = 2;
            this.lblFolderPath.Text = "Test";
            // 
            // lblFileCount
            // 
            this.lblFileCount.Location = new System.Drawing.Point(328, 127);
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(855, 34);
            this.lblFileCount.TabIndex = 3;
            this.lblFileCount.Text = "Found {0} Spark Files";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFileCount);
            this.groupBox1.Controls.Add(this.lblFolderPath);
            this.groupBox1.Controls.Add(this.btnOpenFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1201, 173);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTranslate);
            this.groupBox2.Location = new System.Drawing.Point(12, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1201, 170);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 809);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "Spark to EJS";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openSparkFile;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.FolderBrowserDialog folderSparkFiles;
        private System.Windows.Forms.Label lblFolderPath;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

