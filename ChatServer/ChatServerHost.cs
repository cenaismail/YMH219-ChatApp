using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    /// <summary>
    /// Sunucunun tüm mantığı: bağlantıları kabul eder, istemcileri listede tutar,
    /// gelen mesajları bütün istemcilere iletir. Arayüzden bağımsızdır;
    /// forma olaylar (event) aracılığıyla haber verir.
    ///
    /// Mesaj biçimi (her mesaj tek satır):
    ///   İstemci → Sunucu:  JOIN|kullanıcıAdı      MSG|mesaj
    ///   Sunucu → İstemci:  MSG|saat|ad|mesaj      SYS|saat|bilgi
    /// </summary>
    public class ChatServerHost
    {
        private TcpListener? _listener;
        private readonly List<ClientConnection> _clients = new List<ClientConnection>();

        public bool IsRunning { get; private set; }
        public int Port { get; private set; }
        public IReadOnlyList<ClientConnection> Clients => _clients;

        /// <summary>Kayıt listesine yazılacak bir olay olduğunda tetiklenir.</summary>
        public event Action<string>? Log;

        /// <summary>Bağlı istemci listesi değiştiğinde tetiklenir.</summary>
        public event Action? ClientsChanged;

        /// <summary>Sunucuyu başlatır. Port kullanımdaysa SocketException fırlatır.</summary>
        public void Start(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            Port = port;
            IsRunning = true;
            WriteLog($"Sunucu {port} portunda başlatıldı.");

            _ = AcceptClientsAsync();   // arka planda bağlantı beklemeye başla
        }

        public void Stop()
        {
            if (!IsRunning) return;
            IsRunning = false;

            _listener?.Stop();
            foreach (ClientConnection c in _clients.ToList())
                c.Close();
            _clients.Clear();

            ClientsChanged?.Invoke();
            WriteLog("Sunucu durduruldu.");
        }

        /// <summary>Sunucu çalıştığı sürece yeni bağlantıları kabul eder.</summary>
        private async Task AcceptClientsAsync()
        {
            while (IsRunning)
            {
                TcpClient tcp;
                try
                {
                    tcp = await _listener!.AcceptTcpClientAsync();
                }
                catch (Exception)
                {
                    break;   // Stop() çağrılınca dinleyici kapanır ve buraya düşülür
                }

                _ = HandleClientAsync(new ClientConnection(tcp));
            }
        }

        /// <summary>Tek bir istemciyle konuşur: önce adını alır, sonra mesajlarını okur.</summary>
        private async Task HandleClientAsync(ClientConnection client)
        {
            try
            {
                // İlk satır "JOIN|kullanıcıAdı" olmalı
                string? first = await client.ReadLineAsync();
                if (first == null || !first.StartsWith("JOIN|"))
                {
                    client.Close();
                    return;
                }

                string name = first.Substring(5).Replace("|", "").Trim();
                if (name.Length == 0) name = "İsimsiz";
                client.UserName = name;

                _clients.Add(client);
                ClientsChanged?.Invoke();
                WriteLog($"{name} bağlandı ({client.Address}).");
                await BroadcastAsync($"SYS|{Now()}|{name} sohbete katıldı.");

                // Mesajları oku ve herkese ilet
                while (true)
                {
                    string? line = await client.ReadLineAsync();
                    if (line == null) break;   // istemci bağlantıyı kapattı

                    if (line.StartsWith("MSG|"))
                    {
                        string text = line.Substring(4);
                        WriteLog($"{name}: {text}");
                        await BroadcastAsync($"MSG|{Now()}|{name}|{text}");
                    }
                }
            }
            catch (IOException) { }               // bağlantı aniden koptu
            catch (ObjectDisposedException) { }   // sunucu durduruldu
            finally
            {
                // Listede hâlâ varsa çıkar ve diğerlerine haber ver
                if (_clients.Remove(client))
                {
                    client.Close();
                    ClientsChanged?.Invoke();
                    WriteLog($"{client.UserName} ayrıldı.");
                    if (IsRunning)
                        await BroadcastAsync($"SYS|{Now()}|{client.UserName} sohbetten ayrıldı.");
                }
            }
        }

        /// <summary>Bir satırı bağlı bütün istemcilere gönderir.</summary>
        private async Task BroadcastAsync(string line)
        {
            foreach (ClientConnection c in _clients.ToList())
            {
                try
                {
                    await c.SendAsync(line);
                }
                catch (Exception)
                {
                    // Bu istemcinin bağlantısı kopmuş; kendi döngüsü onu listeden çıkaracak
                }
            }
        }

        private static string Now() => DateTime.Now.ToString("HH:mm:ss");

        private void WriteLog(string text) => Log?.Invoke($"[{Now()}] {text}");
    }
}