using System.Net.Sockets;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        private readonly ChatConnection _connection = new ChatConnection();

        public Form1()
        {
            InitializeComponent();

            // Bağlantıdan gelen olayları dinle
            _connection.MessageReceived += AddMessage;
            _connection.Disconnected += OnDisconnected;

            // Buton ve klavye olayları
            btnConnect.Click += BtnConnect_Click;
            btnDisconnect.Click += (s, e) => _connection.Disconnect();
            btnSend.Click += BtnSend_Click;
            txtMessage.KeyDown += TxtMessage_KeyDown;
            FormClosing += (s, e) => _connection.Disconnect();

            UpdateUi();
        }

        private async void BtnConnect_Click(object? sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            if (userName.Length == 0)
            {
                MessageBox.Show("Lütfen bir kullanıcı adı girin.", "Eksik bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return;
            }

            if (!int.TryParse(txtPort.Text, out int port) || port < 1024 || port > 65535)
            {
                MessageBox.Show("Port 1024 ile 65535 arasında bir sayı olmalı.", "Geçersiz port",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnConnect.Enabled = false;
            lblStatus.Text = "Bağlanıyor...";
            lblStatus.ForeColor = Color.DarkOrange;

            try
            {
                await _connection.ConnectAsync(txtServer.Text.Trim(), port, userName);
                txtMessage.Focus();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Sunucuya bağlanılamadı. Sunucunun çalıştığından, adres ve portun doğru olduğundan emin olun.\n\n" + ex.Message,
                    "Bağlantı hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateUi();
        }

        private async void BtnSend_Click(object? sender, EventArgs e)
        {
            await SendCurrentMessageAsync();
        }

        /// <summary>Mesaj kutusunda Enter'a basılınca mesajı gönderir.</summary>
        private async void TxtMessage_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;   // "dıt" sesini engelle
                await SendCurrentMessageAsync();
            }
        }

        private async Task SendCurrentMessageAsync()
        {
            string text = txtMessage.Text.Trim();
            if (text.Length == 0 || !_connection.IsConnected) return;

            try
            {
                await _connection.SendMessageAsync(text);
                txtMessage.Clear();
                txtMessage.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mesaj gönderilemedi:\n" + ex.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddMessage(string text)
        {
            lstMessages.Items.Add(text);
            lstMessages.TopIndex = lstMessages.Items.Count - 1;   // en yeni mesaja kaydır
        }

        private void OnDisconnected(string reason)
        {
            AddMessage($"[{DateTime.Now:HH:mm:ss}] *** {reason} ***");
            UpdateUi();
        }

        /// <summary>Kontrolleri bağlantı durumuna göre açıp kapatır.</summary>
        private void UpdateUi()
        {
            bool connected = _connection.IsConnected;

            txtServer.Enabled = !connected;
            txtPort.Enabled = !connected;
            txtUserName.Enabled = !connected;
            btnConnect.Enabled = !connected;
            btnDisconnect.Enabled = connected;
            txtMessage.Enabled = connected;
            btnSend.Enabled = connected;

            lblStatus.Text = connected
                ? $"Bağlı: {_connection.UserName} ({txtServer.Text}:{txtPort.Text})"
                : "Bağlı değil";
            lblStatus.ForeColor = connected ? Color.Green : Color.Gray;
        }
    }
}
