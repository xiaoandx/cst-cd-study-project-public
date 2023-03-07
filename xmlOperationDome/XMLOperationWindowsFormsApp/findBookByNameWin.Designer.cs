namespace XMLOperationWindowsFormsApp
{
    partial class findBookByNameWin
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
            this.inputBookName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.findButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputBookName
            // 
            this.inputBookName.Location = new System.Drawing.Point(179, 29);
            this.inputBookName.Name = "inputBookName";
            this.inputBookName.Size = new System.Drawing.Size(111, 25);
            this.inputBookName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "输入Book标签的name：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(318, 29);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(120, 25);
            this.findButton.TabIndex = 2;
            this.findButton.Text = "点击查看详情";
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            //this.findButton.UseVisualStyleBackColor = true;
            // 
            // findBookByNameWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 78);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputBookName);
            this.Name = "findBookByNameWin";
            this.Text = "查询指定book:name的详细内容";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.TextBox inputBookName;
    }
}