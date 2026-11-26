namespace ChatClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblServerTitle = new Label();
            txtServer = new TextBox();
            lblPortTitle = new Label();
            txtPort = new TextBox();
            lblUserTitle = new Label();
            txtUserName = new TextBox();
            btnConnect = new Button();
            btnDisconnect = new Button();
            lblStatus = new Label();
            lstMessages = new ListBox();
            txtMessage = new TextBox();
            btnSend = new Button();
            SuspendLayout();
            // 
            // lblServerTitle
            // 
            lblServerTitle.AutoSize = true;
            lblServerTitle.Location = new Point(15, 15);
            lblServerTitle.Name = "lblServerTitle";
            lblServerTitle.TabIndex = 0;
            lblServerTitle.Text = "Sunucu:";
            // 
            // txtServer
            // 
            txtServer.Location = new Point(80, 12);
            txtServer.Name = "txtServer";
            txtServer.Size = new Size(120, 27);
            txtServer.TabIndex = 1;
            txtServer.Text = "127.0.0.1";
            // 
            // lblPortTitle
            // 
            lblPortTitle.AutoSize = true;
            lblPortTitle.Location = new Point(215, 15);
            lblPortTitle.Name = "lblPortTitle";
            lblPortTitle.TabIndex = 2;
            lblPortTitle.Text = "Port:";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(260, 12);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(70, 27);
            txtPort.TabIndex = 3;
            txtPort.Text = "5000";
            // 
            // lblUserTitle
            // 
            lblUserTitle.AutoSize = true;
            lblUserTitle.Location = new Point(345, 15);
            lblUserTitle.Name = "lblUserTitle";
            lblUserTitle.TabIndex = 4;
            lblUserTitle.Text = "Kullanıcı adı:";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(450, 12);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(150, 27);
            txtUserName.TabIndex = 5;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(15, 50);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(120, 32);
            btnConnect.TabIndex = 6;
            btnConnect.Text = "Bağlan";
            btnConnect.UseVisualStyleBackColor = true;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(145, 50);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(140, 32);
            btnDisconnect.TabIndex = 7;
            btnDisconnect.Text = "Bağlantıyı Kes";
            btnDisconnect.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(300, 56);
            lblStatus.Name = "lblStatus";
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Bağlı değil";
            // 
            // lstMessages
            // 
            lstMessages.FormattingEnabled = true;
            lstMessages.HorizontalScrollbar = true;
            lstMessages.Location = new Point(15, 95);
            lstMessages.Name = "lstMessages";
            lstMessages.Size = new Size(770, 300);
            lstMessages.TabIndex = 9;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(15, 405);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(660, 27);
            txtMessage.TabIndex = 10;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(685, 403);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(100, 32);
            btnSend.TabIndex = 11;
            btnSend.Text = "Gönder";
            btnSend.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(lstMessages);
            Controls.Add(lblStatus);
            Controls.Add(btnDisconnect);
            Controls.Add(btnConnect);
            Controls.Add(txtUserName);
            Controls.Add(lblUserTitle);
            Controls.Add(txtPort);
            Controls.Add(lblPortTitle);
            Controls.Add(txtServer);
            Controls.Add(lblServerTitle);
            Name = "Form1";
            Text = "Chat İstemcisi";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblServerTitle;
        private TextBox txtServer;
        private Label lblPortTitle;
        private TextBox txtPort;
        private Label lblUserTitle;
        private TextBox txtUserName;
        private Button btnConnect;
        private Button btnDisconnect;
        private Label lblStatus;
        private ListBox lstMessages;
        private TextBox txtMessage;
        private Button btnSend;
    }
}
