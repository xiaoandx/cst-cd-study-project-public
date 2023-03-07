///-------------------------------------------------------------------------------------------------
/// Copyright (c) 2023 WEI.ZHOU. All rights reserved.
/// The following code is only used for learning and communication, not for illegal and
/// commercial use.
/// If the code is used, no consent is required, but the author has nothing to do with any problems
/// and consequences.
/// In case of code problems, feedback can be made through the following email address.
/// 
///                        <wei.zhou@ccssttcn.com>
///-------------------------------------------------------------------------------------------------    
namespace ExcelINTOCreateTableSQL
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtExcelFilePathURLSet = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnClearExcelFilePath = new System.Windows.Forms.Button();
            this.rbtnDBTypeSQLServer = new System.Windows.Forms.RadioButton();
            this.rbtnDBTypeOracle = new System.Windows.Forms.RadioButton();
            this.groupBoxDBType = new System.Windows.Forms.GroupBox();
            this.butConversion = new System.Windows.Forms.Button();
            this.textCreateSQLStatement = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.butCopySQL = new System.Windows.Forms.Button();
            this.groupBoxDBType.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtExcelFilePathURLSet
            // 
            this.txtExcelFilePathURLSet.Location = new System.Drawing.Point(28, 38);
            this.txtExcelFilePathURLSet.Multiline = true;
            this.txtExcelFilePathURLSet.Name = "txtExcelFilePathURLSet";
            this.txtExcelFilePathURLSet.Size = new System.Drawing.Size(552, 119);
            this.txtExcelFilePathURLSet.TabIndex = 0;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(610, 38);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(118, 38);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "选择转换文件";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnClearExcelFilePath
            // 
            this.btnClearExcelFilePath.Location = new System.Drawing.Point(610, 119);
            this.btnClearExcelFilePath.Name = "btnClearExcelFilePath";
            this.btnClearExcelFilePath.Size = new System.Drawing.Size(118, 38);
            this.btnClearExcelFilePath.TabIndex = 2;
            this.btnClearExcelFilePath.Text = "清除选择文件";
            this.btnClearExcelFilePath.UseVisualStyleBackColor = true;
            this.btnClearExcelFilePath.Click += new System.EventHandler(this.btnClearExcelFilePath_Click);
            // 
            // rbtnDBTypeSQLServer
            // 
            this.rbtnDBTypeSQLServer.AutoSize = true;
            this.rbtnDBTypeSQLServer.Checked = true;
            this.rbtnDBTypeSQLServer.Location = new System.Drawing.Point(6, 24);
            this.rbtnDBTypeSQLServer.Name = "rbtnDBTypeSQLServer";
            this.rbtnDBTypeSQLServer.Size = new System.Drawing.Size(100, 19);
            this.rbtnDBTypeSQLServer.TabIndex = 3;
            this.rbtnDBTypeSQLServer.TabStop = true;
            this.rbtnDBTypeSQLServer.Text = "SQLServer";
            this.rbtnDBTypeSQLServer.UseVisualStyleBackColor = true;
            // 
            // rbtnDBTypeOracle
            // 
            this.rbtnDBTypeOracle.AutoSize = true;
            this.rbtnDBTypeOracle.Location = new System.Drawing.Point(106, 24);
            this.rbtnDBTypeOracle.Name = "rbtnDBTypeOracle";
            this.rbtnDBTypeOracle.Size = new System.Drawing.Size(76, 19);
            this.rbtnDBTypeOracle.TabIndex = 4;
            this.rbtnDBTypeOracle.TabStop = true;
            this.rbtnDBTypeOracle.Text = "Oracle";
            this.rbtnDBTypeOracle.UseVisualStyleBackColor = true;
            // 
            // groupBoxDBType
            // 
            this.groupBoxDBType.Controls.Add(this.rbtnDBTypeSQLServer);
            this.groupBoxDBType.Controls.Add(this.rbtnDBTypeOracle);
            this.groupBoxDBType.Location = new System.Drawing.Point(28, 175);
            this.groupBoxDBType.Name = "groupBoxDBType";
            this.groupBoxDBType.Size = new System.Drawing.Size(188, 50);
            this.groupBoxDBType.TabIndex = 5;
            this.groupBoxDBType.TabStop = false;
            this.groupBoxDBType.Text = "选择数据库";
            // 
            // butConversion
            // 
            this.butConversion.Location = new System.Drawing.Point(28, 250);
            this.butConversion.Name = "butConversion";
            this.butConversion.Size = new System.Drawing.Size(118, 38);
            this.butConversion.TabIndex = 6;
            this.butConversion.Text = "开始转换";
            this.butConversion.UseVisualStyleBackColor = true;
            this.butConversion.Click += new System.EventHandler(this.butConversion_Click);
            // 
            // textCreateSQLStatement
            // 
            this.textCreateSQLStatement.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textCreateSQLStatement.ForeColor = System.Drawing.Color.Red;
            this.textCreateSQLStatement.Location = new System.Drawing.Point(260, 175);
            this.textCreateSQLStatement.Multiline = true;
            this.textCreateSQLStatement.Name = "textCreateSQLStatement";
            this.textCreateSQLStatement.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textCreateSQLStatement.Size = new System.Drawing.Size(468, 295);
            this.textCreateSQLStatement.TabIndex = 7;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // butCopySQL
            // 
            this.butCopySQL.Location = new System.Drawing.Point(28, 312);
            this.butCopySQL.Name = "butCopySQL";
            this.butCopySQL.Size = new System.Drawing.Size(118, 38);
            this.butCopySQL.TabIndex = 8;
            this.butCopySQL.Text = "复制SQL";
            this.butCopySQL.UseVisualStyleBackColor = true;
            this.butCopySQL.Click += new System.EventHandler(this.butCopySQL_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 486);
            this.Controls.Add(this.butCopySQL);
            this.Controls.Add(this.textCreateSQLStatement);
            this.Controls.Add(this.butConversion);
            this.Controls.Add(this.groupBoxDBType);
            this.Controls.Add(this.btnClearExcelFilePath);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtExcelFilePathURLSet);
            this.Name = "MainForm";
            this.Text = "ExcelINTOCreateTableSQL";
            this.groupBoxDBType.ResumeLayout(false);
            this.groupBoxDBType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtExcelFilePathURLSet;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnClearExcelFilePath;
        private System.Windows.Forms.RadioButton rbtnDBTypeSQLServer;
        private System.Windows.Forms.RadioButton rbtnDBTypeOracle;
        private System.Windows.Forms.GroupBox groupBoxDBType;
        private System.Windows.Forms.Button butConversion;
        private System.Windows.Forms.TextBox textCreateSQLStatement;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button butCopySQL;
    }
}

