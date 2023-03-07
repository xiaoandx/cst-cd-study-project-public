namespace SocketDemo
{
    partial class SocketServer
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
            this.labelMessagePrompt = new System.Windows.Forms.Label();
            this.labelServerIP = new System.Windows.Forms.Label();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.labelProt = new System.Windows.Forms.Label();
            this.textBoxProt = new System.Windows.Forms.TextBox();
            this.buttonRunServer = new System.Windows.Forms.Button();
            this.buttonRunClient = new System.Windows.Forms.Button();
            this.labelSendMessage = new System.Windows.Forms.Label();
            this.textBoxSendMessage = new System.Windows.Forms.TextBox();
            this.buttonSendMessage = new System.Windows.Forms.Button();
            this.labelSelectClient = new System.Windows.Forms.Label();
            this.comboBoxSelectCilent = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonMassMessage = new System.Windows.Forms.Button();
            this.buttonSaveChatToFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelMessagePrompt
            // 
            this.labelMessagePrompt.AutoSize = true;
            this.labelMessagePrompt.Location = new System.Drawing.Point(21, 20);
            this.labelMessagePrompt.Name = "labelMessagePrompt";
            this.labelMessagePrompt.Size = new System.Drawing.Size(67, 15);
            this.labelMessagePrompt.TabIndex = 0;
            this.labelMessagePrompt.Text = "消息提示";
            // 
            // labelServerIP
            // 
            this.labelServerIP.AutoSize = true;
            this.labelServerIP.Location = new System.Drawing.Point(596, 29);
            this.labelServerIP.Name = "labelServerIP";
            this.labelServerIP.Size = new System.Drawing.Size(76, 15);
            this.labelServerIP.TabIndex = 1;
            this.labelServerIP.Text = "服务器IP:";
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.Location = new System.Drawing.Point(670, 25);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.ReadOnly = true;
            this.textBoxServerIP.Size = new System.Drawing.Size(100, 25);
            this.textBoxServerIP.TabIndex = 2;
            this.textBoxServerIP.Text = "127.0.0.1";
            this.textBoxServerIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelProt
            // 
            this.labelProt.AutoSize = true;
            this.labelProt.Location = new System.Drawing.Point(596, 74);
            this.labelProt.Name = "labelProt";
            this.labelProt.Size = new System.Drawing.Size(76, 15);
            this.labelProt.TabIndex = 3;
            this.labelProt.Text = "端 口 号:";
            // 
            // textBoxProt
            // 
            this.textBoxProt.Location = new System.Drawing.Point(670, 71);
            this.textBoxProt.Name = "textBoxProt";
            this.textBoxProt.Size = new System.Drawing.Size(100, 25);
            this.textBoxProt.TabIndex = 4;
            this.textBoxProt.Text = "8009";
            this.textBoxProt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonRunServer
            // 
            this.buttonRunServer.Location = new System.Drawing.Point(804, 24);
            this.buttonRunServer.Name = "buttonRunServer";
            this.buttonRunServer.Size = new System.Drawing.Size(101, 26);
            this.buttonRunServer.TabIndex = 5;
            this.buttonRunServer.Text = "启动服务端";
            this.buttonRunServer.UseVisualStyleBackColor = true;
            this.buttonRunServer.Click += new System.EventHandler(this.buttonRunServer_Click);
            // 
            // buttonRunClient
            // 
            this.buttonRunClient.Location = new System.Drawing.Point(804, 70);
            this.buttonRunClient.Name = "buttonRunClient";
            this.buttonRunClient.Size = new System.Drawing.Size(101, 26);
            this.buttonRunClient.TabIndex = 6;
            this.buttonRunClient.Text = "启动客户端";
            this.buttonRunClient.UseVisualStyleBackColor = true;
            this.buttonRunClient.Click += new System.EventHandler(this.buttonRunClient_Click);
            // 
            // labelSendMessage
            // 
            this.labelSendMessage.AutoSize = true;
            this.labelSendMessage.Location = new System.Drawing.Point(596, 160);
            this.labelSendMessage.Name = "labelSendMessage";
            this.labelSendMessage.Size = new System.Drawing.Size(82, 15);
            this.labelSendMessage.TabIndex = 7;
            this.labelSendMessage.Text = "发送消息框";
            // 
            // textBoxSendMessage
            // 
            this.textBoxSendMessage.Location = new System.Drawing.Point(599, 188);
            this.textBoxSendMessage.Multiline = true;
            this.textBoxSendMessage.Name = "textBoxSendMessage";
            this.textBoxSendMessage.Size = new System.Drawing.Size(306, 151);
            this.textBoxSendMessage.TabIndex = 8;
            // 
            // buttonSendMessage
            // 
            this.buttonSendMessage.Location = new System.Drawing.Point(812, 359);
            this.buttonSendMessage.Name = "buttonSendMessage";
            this.buttonSendMessage.Size = new System.Drawing.Size(93, 26);
            this.buttonSendMessage.TabIndex = 9;
            this.buttonSendMessage.Text = "发送消息";
            this.buttonSendMessage.UseVisualStyleBackColor = true;
            this.buttonSendMessage.Click += new System.EventHandler(this.buttonSendMessage_Click);
            // 
            // labelSelectClient
            // 
            this.labelSelectClient.AutoSize = true;
            this.labelSelectClient.Location = new System.Drawing.Point(596, 121);
            this.labelSelectClient.Name = "labelSelectClient";
            this.labelSelectClient.Size = new System.Drawing.Size(82, 15);
            this.labelSelectClient.TabIndex = 10;
            this.labelSelectClient.Text = "选择客户端";
            // 
            // comboBoxSelectCilent
            // 
            this.comboBoxSelectCilent.FormattingEnabled = true;
            this.comboBoxSelectCilent.Location = new System.Drawing.Point(684, 117);
            this.comboBoxSelectCilent.Name = "comboBoxSelectCilent";
            this.comboBoxSelectCilent.Size = new System.Drawing.Size(221, 23);
            this.comboBoxSelectCilent.TabIndex = 11;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTime,
            this.columnHeaderContent});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(24, 50);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(550, 289);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "时间";
            this.columnHeaderTime.Width = 200;
            // 
            // columnHeaderContent
            // 
            this.columnHeaderContent.Text = "消息";
            this.columnHeaderContent.Width = 500;
            // 
            // buttonMassMessage
            // 
            this.buttonMassMessage.Location = new System.Drawing.Point(599, 359);
            this.buttonMassMessage.Name = "buttonMassMessage";
            this.buttonMassMessage.Size = new System.Drawing.Size(93, 26);
            this.buttonMassMessage.TabIndex = 13;
            this.buttonMassMessage.Text = "群发消息";
            this.buttonMassMessage.UseVisualStyleBackColor = true;
            this.buttonMassMessage.Click += new System.EventHandler(this.buttonMassMessage_Click);
            // 
            // buttonSaveChatToFile
            // 
            this.buttonSaveChatToFile.Location = new System.Drawing.Point(24, 359);
            this.buttonSaveChatToFile.Name = "buttonSaveChatToFile";
            this.buttonSaveChatToFile.Size = new System.Drawing.Size(128, 26);
            this.buttonSaveChatToFile.TabIndex = 14;
            this.buttonSaveChatToFile.Text = "保存聊天文件";
            this.buttonSaveChatToFile.UseVisualStyleBackColor = true;
            this.buttonSaveChatToFile.Click += new System.EventHandler(this.buttonSaveChatToFile_Click);
            // 
            // SocketServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 404);
            this.Controls.Add(this.buttonSaveChatToFile);
            this.Controls.Add(this.buttonMassMessage);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.comboBoxSelectCilent);
            this.Controls.Add(this.labelSelectClient);
            this.Controls.Add(this.buttonSendMessage);
            this.Controls.Add(this.textBoxSendMessage);
            this.Controls.Add(this.labelSendMessage);
            this.Controls.Add(this.buttonRunClient);
            this.Controls.Add(this.buttonRunServer);
            this.Controls.Add(this.textBoxProt);
            this.Controls.Add(this.labelProt);
            this.Controls.Add(this.textBoxServerIP);
            this.Controls.Add(this.labelServerIP);
            this.Controls.Add(this.labelMessagePrompt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SocketServer";
            this.Text = "Socket网络服务端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMessagePrompt;
        private System.Windows.Forms.Label labelServerIP;
        private System.Windows.Forms.TextBox textBoxServerIP;
        private System.Windows.Forms.Label labelProt;
        private System.Windows.Forms.TextBox textBoxProt;
        private System.Windows.Forms.Button buttonRunServer;
        private System.Windows.Forms.Button buttonRunClient;
        private System.Windows.Forms.Label labelSendMessage;
        private System.Windows.Forms.TextBox textBoxSendMessage;
        private System.Windows.Forms.Button buttonSendMessage;
        private System.Windows.Forms.Label labelSelectClient;
        private System.Windows.Forms.ComboBox comboBoxSelectCilent;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.ColumnHeader columnHeaderContent;
        private System.Windows.Forms.Button buttonMassMessage;
        private System.Windows.Forms.Button buttonSaveChatToFile;
    }
}

