using System.Net.Sockets;
using System.Text;

namespace ChatClient
{
    /// <summary>
    /// İstemcinin sunucuyla olan bağlantısını yönetir: bağlanma, mesaj gönderme,
    /// gelen mesajları dinleme ve bağlantıyı kesme. Arayüzden bağımsızdır;
    /// forma olaylar (event) aracılığıyla haber verir.
    ///
    /// Mesaj biçimi (her mesaj tek satır):
    ///   İstemci → Sunucu:  JOIN|kullanıcıAdı      MSG|mesaj
    ///   Sunucu → İstemci:  MSG|saat|ad|mesaj      SYS|saat|bilgi
    /// </summary>
    public class ChatConnection
    {
        private TcpClient? _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;

        public bool IsConnected { get; private set; }
        public string UserName { get; private set; } = "";

        /// <summary>Sunucudan ekranda gösterilecek bir mesaj geldiğinde tetiklenir.</summary>
        public event Action<string>? MessageReceived;

        /// <summary>Bağlantı kesildiğinde tetiklenir; parametre kesilme sebebidir.</summary>
        public event Action<string>? Disconnected;

        /// <summary>
        /// Sunucuya bağlanır ve kullanıcı adını gönderir.
        /// Sunucuya ulaşılamazsa SocketException fırlatır.
        /// </summary>
        public async Task ConnectAsync(string host, int port, string userName)
        {
            _client = new TcpClient();
            await _client.ConnectAsync(host, port);

            NetworkStream stream = _client.GetStream();
            _reader = new StreamReader(stream, Encoding.UTF8);
            _writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true };

            UserName = userName;
            IsConnected = true;

            await _writer.WriteLineAsync("JOIN|" + userName);
            _ = ReceiveMessagesAsync();   // arka planda gelen mesajları dinle
        }

        /// <summary>Sunucuya bir sohbet mesajı gönderir.</summary>
        public async Task SendMessageAsync(string text)
        {
            if (!IsConnected || _writer == null)
                throw new InvalidOperationException("Sunucuya bağlı değilsiniz.");

            await _writer.WriteLineAsync("MSG|" + text);
        }

        /// <summary>Bağlantıyı kullanıcı isteğiyle keser.</summary>
        public void Disconnect()
        {
            if (!IsConnected) return;
            IsConnected = false;
            _client?.Close();
            Disconnected?.Invoke("Bağlantı kesildi.");
        }

        /// <summary>Bağlı kalındığı sürece sunucudan gelen satırları okur.</summary>
        private async Task ReceiveMessagesAsync()
        {
            string reason = "Sunucu bağlantıyı kapattı.";
            try
            {
                while (IsConnected)
                {
                    string? line = await _reader!.ReadLineAsync();
                    if (line == null) break;   // sunucu kapandı

                    string? text = FormatLine(line);
                    if (text != null)
                        MessageReceived?.Invoke(text);
                }
            }
            catch (IOException)
            {
                reason = "Sunucuyla bağlantı koptu.";
            }
            catch (ObjectDisposedException)
            {
                return;   // Disconnect() ile kapatıldı; olay zaten tetiklendi
            }

            // Bağlantı sunucu tarafında koptuysa forma haber ver
            if (IsConnected)
            {
                IsConnected = false;
                _client?.Close();
                Disconnected?.Invoke(reason);
            }
        }

        /// <summary>Sunucudan gelen satırı ekranda gösterilecek metne çevirir.</summary>
        private static string? FormatLine(string line)
        {
            // En fazla 4 parçaya böl; mesajın içinde "|" olsa bile bozulmaz
            string[] parts = line.Split(new[] { '|' }, 4);

            if (parts[0] == "MSG" && parts.Length == 4)
                return $"[{parts[1]}] {parts[2]}: {parts[3]}";

            if (parts[0] == "SYS" && parts.Length >= 3)
                return $"[{parts[1]}] *** {parts[2]} ***";

            return null;   // tanınmayan satır
        }
    }
}
