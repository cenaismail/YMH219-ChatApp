using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    /// <summary>
    /// Sunucuya bağlı tek bir istemciyi temsil eder: bağlantısı, kullanıcı adı,
    /// adresi ve bağlanma zamanı bu sınıfta tutulur.
    /// </summary>
    public class ClientConnection
    {
        private readonly TcpClient _client;
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;

        // Aynı istemciye aynı anda iki mesaj yazılmasın diye kilit
        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);

        public string UserName { get; set; } = "";
        public string Address { get; }
        public DateTime ConnectedAt { get; }

        public ClientConnection(TcpClient client)
        {
            _client = client;
            NetworkStream stream = client.GetStream();
            _reader = new StreamReader(stream, Encoding.UTF8);
            _writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true };

            Address = client.Client.RemoteEndPoint?.ToString() ?? "bilinmiyor";
            ConnectedAt = DateTime.Now;
        }

        /// <summary>İstemciden bir satır okur. Bağlantı kapandıysa null döner.</summary>
        public Task<string?> ReadLineAsync()
        {
            return _reader.ReadLineAsync();
        }

        /// <summary>İstemciye bir satır gönderir.</summary>
        public async Task SendAsync(string line)
        {
            await _sendLock.WaitAsync();
            try
            {
                await _writer.WriteLineAsync(line);
            }
            finally
            {
                _sendLock.Release();
            }
        }

        public void Close()
        {
            _client.Close();
        }

        public override string ToString()
        {
            return $"{UserName}  |  {Address}  |  {ConnectedAt:HH:mm:ss}";
        }
    }
}