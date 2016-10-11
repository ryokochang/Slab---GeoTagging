namespace Slab___GeoTagging
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
            this.label_log = new System.Windows.Forms.Label();
            this.button_log = new System.Windows.Forms.Button();
            this.textBox_log = new System.Windows.Forms.TextBox();
            this.label_img = new System.Windows.Forms.Label();
            this.textBox_img = new System.Windows.Forms.TextBox();
            this.button_img = new System.Windows.Forms.Button();
            this.textBox_output = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button_sync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_log
            // 
            this.label_log.AutoSize = true;
            this.label_log.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_log.Location = new System.Drawing.Point(13, 28);
            this.label_log.Name = "label_log";
            this.label_log.Size = new System.Drawing.Size(76, 17);
            this.label_log.TabIndex = 0;
            this.label_log.Text = "Log File txt";
            // 
            // button_log
            // 
            this.button_log.Location = new System.Drawing.Point(569, 27);
            this.button_log.Name = "button_log";
            this.button_log.Size = new System.Drawing.Size(75, 23);
            this.button_log.TabIndex = 1;
            this.button_log.Text = "Browse";
            this.button_log.UseVisualStyleBackColor = true;
            this.button_log.Click += new System.EventHandler(this.button_log_Click);
            // 
            // textBox_log
            // 
            this.textBox_log.Location = new System.Drawing.Point(95, 27);
            this.textBox_log.Name = "textBox_log";
            this.textBox_log.Size = new System.Drawing.Size(468, 20);
            this.textBox_log.TabIndex = 2;
            // 
            // label_img
            // 
            this.label_img.AutoSize = true;
            this.label_img.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_img.Location = new System.Drawing.Point(13, 68);
            this.label_img.Name = "label_img";
            this.label_img.Size = new System.Drawing.Size(74, 17);
            this.label_img.TabIndex = 3;
            this.label_img.Text = "Img Folder";
            // 
            // textBox_img
            // 
            this.textBox_img.Location = new System.Drawing.Point(95, 70);
            this.textBox_img.Name = "textBox_img";
            this.textBox_img.Size = new System.Drawing.Size(468, 20);
            this.textBox_img.TabIndex = 4;
            // 
            // button_img
            // 
            this.button_img.Location = new System.Drawing.Point(569, 68);
            this.button_img.Name = "button_img";
            this.button_img.Size = new System.Drawing.Size(75, 23);
            this.button_img.TabIndex = 5;
            this.button_img.Text = "Browse";
            this.button_img.UseVisualStyleBackColor = true;
            this.button_img.Click += new System.EventHandler(this.button_img_Click);
            // 
            // textBox_output
            // 
            this.textBox_output.Location = new System.Drawing.Point(12, 143);
            this.textBox_output.Multiline = true;
            this.textBox_output.Name = "textBox_output";
            this.textBox_output.ReadOnly = true;
            this.textBox_output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_output.Size = new System.Drawing.Size(628, 248);
            this.textBox_output.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button_sync
            // 
            this.button_sync.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_sync.Location = new System.Drawing.Point(257, 103);
            this.button_sync.Name = "button_sync";
            this.button_sync.Size = new System.Drawing.Size(118, 31);
            this.button_sync.TabIndex = 7;
            this.button_sync.Text = "Sync";
            this.button_sync.UseVisualStyleBackColor = true;
            this.button_sync.Click += new System.EventHandler(this.button_sync_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 403);
            this.Controls.Add(this.button_sync);
            this.Controls.Add(this.textBox_output);
            this.Controls.Add(this.button_img);
            this.Controls.Add(this.textBox_img);
            this.Controls.Add(this.label_img);
            this.Controls.Add(this.textBox_log);
            this.Controls.Add(this.button_log);
            this.Controls.Add(this.label_log);
            this.Name = "Form1";
            this.Text = "Santos Lab - Geotagging";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_log;
        private System.Windows.Forms.Button button_log;
        private System.Windows.Forms.TextBox textBox_log;
        private System.Windows.Forms.Label label_img;
        private System.Windows.Forms.TextBox textBox_img;
        private System.Windows.Forms.Button button_img;
        private System.Windows.Forms.TextBox textBox_output;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_sync;
    }
}

