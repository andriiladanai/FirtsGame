using System;
using System.Net.Sockets;
using System.Text;

namespace RockPaperScissorsClient
{
    /// <summary>
    /// Основний клас клієнта для гри "Камінь, Ножиці, Папір".
    /// </summary>
    public class Client
    {
        private TcpClient _client;
        private NetworkStream _stream;

        /// <summary>
        /// Підключення до сервера.
        /// </summary>
        /// <param name="ipAddress">IP-адреса сервера.</param>
        /// <param name="port">Порт сервера.</param>
        public void Connect(string ipAddress, int port)
        {
            _client = new TcpClient(ipAddress, port);
            _stream = _client.GetStream();
            Console.WriteLine("Пiдключення до сервера успiшне!");
        }

        /// <summary>
        /// Надсилає вибір клієнта на сервер.
        /// </summary>
        /// <param name="choice">Вибір клієнта ("камінь", "ножиці" або "папір").</param>
        public void SendChoice(string choice)
        {
            byte[] dataToSend = Encoding.UTF8.GetBytes(choice);
            _stream.Write(dataToSend, 0, dataToSend.Length);
        }

        /// <summary>
        /// Отримання відповіді від сервера.
        /// </summary>
        /// <returns>Результат гри.</returns>
        public string ReceiveResponse()
        {
            byte[] buffer = new byte[256];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        /// <summary>
        /// Закриття з'єднання з сервером.
        /// </summary>
        public void Disconnect()
        {
            _stream.Close();
            _client.Close();
            Console.WriteLine("З'єднання закрито.");
        }

        /// <summary>
        /// Точка входу для клієнта.
        /// </summary>
        public static void Main(string[] args)
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.White;
                // Встановлення чорного кольору тексту
                Console.ForegroundColor = ConsoleColor.Black;

                // Очищення консолі, щоб зміни кольору фону застосувалися до всього вікна
                Console.Clear();

                // Створення клієнта та підключення до сервера
                Client client = new Client();
                client.Connect("127.0.0.1", 5000);

                // Основний цикл гри
                while (true)
                {
                    // Введення вибору користувача
                    Console.WriteLine("Введiть свiй вибiр (камiнь, ножицi, папiр) або 'вихiд' для завершення:");
                    string userChoice = Console.ReadLine();

                    // Перевірка на завершення гри
                    if (string.IsNullOrEmpty(userChoice) || userChoice.ToLower() == "вихід")
                    {
                        client.SendChoice("вихiд");
                        Console.WriteLine("Ви вийшли з гри.");
                        break;
                    }

                    // Відправка вибору на сервер
                    client.SendChoice(userChoice);

                    // Отримання результату від сервера
                    string result = client.ReceiveResponse();
                    Console.WriteLine($"Результат: {result}");
                }

                // Закриття з'єднання після завершення гри
                client.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка: {ex.Message}");
            }
        }
    }
}
