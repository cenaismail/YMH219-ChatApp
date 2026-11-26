using System.Net.Sockets;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        private readonly ChatServerHost _server = new ChatServerHost();

        public Form1()
        {
            InitializeComponent();

            // Sunucudan gelen olayları dinle
            _server.Log += OnServerLog;
            _server.ClientsChanged += RefreshClientList;

            // Buton olayları
            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
            FormClosing += (s, e) => _server.Stop();

            UpdateUi();
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtPort.Text, out int port) || port < 1024 || port > 65535)
            {
                MessageBox.Show("Port 1024 ile 65535 arasında bir sayı olmalı.", "Geçersiz port",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _server.Start(port);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Sunucu başlatılamadı. Port başka bir program tarafından kullanılıyor olabilir.\n\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateUi();
        }

        private void BtnStop_Click(object? sender, EventArgs e)
        {
            _server.Stop();
            UpdateUi();
        }

        private void OnServerLog(string text)
        {
            lstLog.Items.Add(text);
            lstLog.TopIndex = lstLog.Items.Count - 1;   // en yeni kayda kaydır
        }

        private void RefreshClientList()
        {
            lstClients.Items.Clear();
            foreach (ClientConnection c in _server.Clients)
                lstClients.Items.Add(c.ToString());

            lblClientsTitle.Text = $"Bağlı istemciler: {_server.Clients.Count}";
        }

        /// <summary>Butonları ve durum yazısını sunucunun durumuna göre ayarlar.</summary>
        private void UpdateUi()
        {
            bool running = _server.IsRunning;
            btnStart.Enabled = !running;
            btnStop.Enabled = running;
            txtPort.Enabled = !running;

            lblStatus.Text = running ? $"Sunucu çalışıyor (port {_server.Port})" : "Sunucu kapalı";
            lblStatus.ForeColor = running ? Color.Green : Color.Gray;
        }
    }
}