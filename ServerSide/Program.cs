using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RockPaperScissorsServer
{
    /// <summary>
    /// Основний клас сервера для гри "Камінь, Ножиці, Папір".
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Точка входу для сервера.
        /// </summary>
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            // Встановлення чорного кольору тексту
            Console.ForegroundColor = ConsoleColor.Black;

            // Очищення консолі, щоб зміни кольору фону застосувалися до всього вікна
            Console.Clear();

            // Налаштування серверного сокета
            TcpListener server = new TcpListener(IPAddress.Any, 5000);
            server.Start();
            Console.WriteLine("Сервер запущено, очiкування пiдключення клiєнтiв...");

            // Прийняття підключень клієнта
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Клiєнт пiдключився!");

            // Створення потоків для читання та запису
            NetworkStream stream = client.GetStream();

            // Основні варіанти гри
            string[] options = { "камiнь", "ножицi", "папiр" };
            Random random = new Random();

            while (true)
            {
                // Прийом вибору від клієнта
                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string clientChoice = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Вибiр клiєнта: {clientChoice}");

                // Перевірка на вихід
                if (clientChoice.ToLower() == "вихiд")
                {
                    Console.WriteLine("Клiєнт вийшов iз гри.");
                    break;
                }

                // Випадковий вибір сервера
                string serverChoice = options[random.Next(options.Length)];
                Console.WriteLine($"Вибiр сервера: {serverChoice}");

                // Визначення переможця
                string result;
                if (clientChoice == serverChoice)
                {
                    result = "Нiчия!";
                }
                else if ((clientChoice == "камiнь" && serverChoice == "ножицi") ||
                         (clientChoice == "ножицi" && serverChoice == "папiр") ||
                         (clientChoice == "папiр" && serverChoice == "камiнь"))
                {
                    result = "Ви перемогли!";
                }
                else
                {
                    result = "Ви програли!";
                }

                // Надсилання результату клієнту
                string message = $"Ваш вибiр: {clientChoice}, Вибiр сервера: {serverChoice} - {result}";
                byte[] dataToSend = Encoding.UTF8.GetBytes(message);
                stream.Write(dataToSend, 0, dataToSend.Length);
            }

            // Закриття з'єднання
            client.Close();
            server.Stop();
            Console.WriteLine("Сервер завершив роботу.");
        }
    }
}
