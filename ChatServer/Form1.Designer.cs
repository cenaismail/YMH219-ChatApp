namespace ChatServer
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
            lblPortTitle = new Label();
            txtPort = new TextBox();
            btnStart = new Button();
            btnStop = new Button();
            lblStatus = new Label();
            lblClientsTitle = new Label();
            lstClients = new ListBox();
            lblLogTitle = new Label();
            lstLog = new ListBox();
            SuspendLayout();
            // 
            // lblPortTitle
            // 
            lblPortTitle.AutoSize = true;
            lblPortTitle.Location = new Point(15, 15);
            lblPortTitle.Name = "lblPortTitle";
            lblPortTitle.Size = new Size(42, 20);
            lblPortTitle.TabIndex = 0;
            lblPortTitle.Text = "Port :";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(70, 12);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(80, 27);
            txtPort.TabIndex = 1;
            txtPort.Text = "5000";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(165, 10);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(150, 32);
            btnStart.TabIndex = 2;
            btnStart.Text = "Sunucuyu Başlat";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(325, 10);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(100, 32);
            btnStop.TabIndex = 3;
            btnStop.Text = "Durdur";
            btnStop.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(15, 55);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(102, 20);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Sunucu Kapalı";
            // 
            // lblClientsTitle
            // 
            lblClientsTitle.AutoSize = true;
            lblClientsTitle.Location = new Point(15, 90);
            lblClientsTitle.Name = "lblClientsTitle";
            lblClientsTitle.Size = new Size(118, 20);
            lblClientsTitle.TabIndex = 5;
            lblClientsTitle.Text = "Bağlı istemciler :";
            // 
            // lstClients
            // 
            lstClients.FormattingEnabled = true;
            lstClients.Location = new Point(15, 115);
            lstClients.Name = "lstClients";
            lstClients.Size = new Size(220, 304);
            lstClients.TabIndex = 6;
            // 
            // lblLogTitle
            // 
            lblLogTitle.AutoSize = true;
            lblLogTitle.Location = new Point(250, 90);
            lblLogTitle.Name = "lblLogTitle";
            lblLogTitle.Size = new Size(111, 20);
            lblLogTitle.TabIndex = 7;
            lblLogTitle.Text = "Mesaj kayıtları :";
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.Location = new Point(250, 115);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(535, 304);
            lstLog.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lstLog);
            Controls.Add(lblLogTitle);
            Controls.Add(lstClients);
            Controls.Add(lblClientsTitle);
            Controls.Add(lblStatus);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(txtPort);
            Controls.Add(lblPortTitle);
            Name = "Form1";
            Text = "Chat Sunucusu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPortTitle;
        private TextBox txtPort;
        private Button btnStart;
        private Button btnStop;
        private Label lblStatus;
        private Label lblClientsTitle;
        private ListBox lstClients;
        private Label lblLogTitle;
        private ListBox lstLog;
    }
}
