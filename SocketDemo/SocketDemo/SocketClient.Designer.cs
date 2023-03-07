namespace SocketDemo
{
    partial class SocketClient
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
            this.labMessagePompt = new System.Windows.Forms.Label();
            this.labServerIP = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxProt = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.buttonConnectServer = new System.Windows.Forms.Button();
            this.labelSendMessage = new System.Windows.Forms.Label();
            this.textBoxSendMessgae = new System.Windows.Forms.TextBox();
            this.buttonSendMessage = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.content = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonClientSaveChatFile = new System.Windows.Forms.Button();
            this.labelCurrentIP = new System.Windows.Forms.Label();
            this.labelIPProt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labMessagePompt
            // 
            this.labMessagePompt.AutoSize = true;
            this.labMessagePompt.Location = new System.Drawing.Point(24, 39);
            this.labMessagePompt.Name = "labMessagePompt";
            this.labMessagePompt.Size = new System.Drawing.Size(67, 15);
            this.labMessagePompt.TabIndex = 0;
            this.labMessagePompt.Text = "消息提示";
            // 
            // labServerIP
            // 
            this.labServerIP.AutoSize = true;
            this.labServerIP.Location = new System.Drawing.Point(700, 48);
            this.labServerIP.Name = "labServerIP";
            this.labServerIP.Size = new System.Drawing.Size(76, 15);
            this.labServerIP.TabIndex = 2;
            this.labServerIP.Text = "服务器IP:";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(782, 41);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(110, 25);
            this.textBoxIP.TabIndex = 3;
            this.textBoxIP.Text = "127.0.0.1";
            this.textBoxIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxProt
            // 
            this.textBoxProt.Location = new System.Drawing.Point(782, 84);
            this.textBoxProt.Name = "textBoxProt";
            this.textBoxProt.Size = new System.Drawing.Size(110, 25);
            this.textBoxProt.TabIndex = 5;
            this.textBoxProt.Text = "8009";
            this.textBoxProt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(700, 91);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(76, 15);
            this.labelPort.TabIndex = 4;
            this.labelPort.Text = "端 口 号:";
            // 
            // buttonConnectServer
            // 
            this.buttonConnectServer.Location = new System.Drawing.Point(782, 130);
            this.buttonConnectServer.Name = "buttonConnectServer";
            this.buttonConnectServer.Size = new System.Drawing.Size(110, 35);
            this.buttonConnectServer.TabIndex = 6;
            this.buttonConnectServer.Text = "连接服务器";
            this.buttonConnectServer.UseVisualStyleBackColor = true;
            this.buttonConnectServer.Click += new System.EventHandler(this.buttonConnectServer_Click);
            // 
            // labelSendMessage
            // 
            this.labelSendMessage.AutoSize = true;
            this.labelSendMessage.Location = new System.Drawing.Point(700, 178);
            this.labelSendMessage.Name = "labelSendMessage";
            this.labelSendMessage.Size = new System.Drawing.Size(67, 15);
            this.labelSendMessage.TabIndex = 7;
            this.labelSendMessage.Text = "发送消息";
            // 
            // textBoxSendMessgae
            // 
            this.textBoxSendMessgae.Location = new System.Drawing.Point(703, 205);
            this.textBoxSendMessgae.Multiline = true;
            this.textBoxSendMessgae.Name = "textBoxSendMessgae";
            this.textBoxSendMessgae.Size = new System.Drawing.Size(189, 104);
            this.textBoxSendMessgae.TabIndex = 8;
            // 
            // buttonSendMessage
            // 
            this.buttonSendMessage.Location = new System.Drawing.Point(782, 333);
            this.buttonSendMessage.Name = "buttonSendMessage";
            this.buttonSendMessage.Size = new System.Drawing.Size(110, 35);
            this.buttonSendMessage.TabIndex = 9;
            this.buttonSendMessage.Text = "发送消息";
            this.buttonSendMessage.UseVisualStyleBackColor = true;
            this.buttonSendMessage.Click += new System.EventHandler(this.buttonSendMessage_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.time,
            this.content});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(27, 58);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(643, 251);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // time
            // 
            this.time.Text = "时间";
            this.time.Width = 200;
            // 
            // content
            // 
            this.content.Text = "内容";
            this.content.Width = 500;
            // 
            // buttonClientSaveChatFile
            // 
            this.buttonClientSaveChatFile.Location = new System.Drawing.Point(27, 333);
            this.buttonClientSaveChatFile.Name = "buttonClientSaveChatFile";
            this.buttonClientSaveChatFile.Size = new System.Drawing.Size(123, 35);
            this.buttonClientSaveChatFile.TabIndex = 11;
            this.buttonClientSaveChatFile.Text = "保存聊天记录";
            this.buttonClientSaveChatFile.UseVisualStyleBackColor = true;
            this.buttonClientSaveChatFile.Click += new System.EventHandler(this.buttonClientSaveChatFile_Click);
            // 
            // labelCurrentIP
            // 
            this.labelCurrentIP.AutoSize = true;
            this.labelCurrentIP.Location = new System.Drawing.Point(253, 343);
            this.labelCurrentIP.Name = "labelCurrentIP";
            this.labelCurrentIP.Size = new System.Drawing.Size(143, 15);
            this.labelCurrentIP.TabIndex = 12;
            this.labelCurrentIP.Text = "当前本端IP与端口：";
            // 
            // labelIPProt
            // 
            this.labelIPProt.AutoSize = true;
            this.labelIPProt.Location = new System.Drawing.Point(394, 342);
            this.labelIPProt.Name = "labelIPProt";
            this.labelIPProt.Size = new System.Drawing.Size(0, 15);
            this.labelIPProt.TabIndex = 13;
            // 
            // SocketClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 411);
            this.Controls.Add(this.labelIPProt);
            this.Controls.Add(this.labelCurrentIP);
            this.Controls.Add(this.buttonClientSaveChatFile);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonSendMessage);
            this.Controls.Add(this.textBoxSendMessgae);
            this.Controls.Add(this.labelSendMessage);
            this.Controls.Add(this.buttonConnectServer);
            this.Controls.Add(this.textBoxProt);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.labServerIP);
            this.Controls.Add(this.labMessagePompt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SocketClient";
            this.Text = "Socket网络客户端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labMessagePompt;
        private System.Windows.Forms.Label labServerIP;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxProt;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Button buttonConnectServer;
        private System.Windows.Forms.Label labelSendMessage;
        private System.Windows.Forms.TextBox textBoxSendMessgae;
        private System.Windows.Forms.Button buttonSendMessage;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ColumnHeader content;
        private System.Windows.Forms.Button buttonClientSaveChatFile;
        private System.Windows.Forms.Label labelCurrentIP;
        private System.Windows.Forms.Label labelIPProt;
    }
}