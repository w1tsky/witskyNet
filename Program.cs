using System;

namespace witskyNet
{
    class Program
    {

        static string remoteAddress; // хост для отправки данных
        static int remotePort; // порт для отправки данных
        static int localPort; // локальный порт для прослушивания входящих подключений

        static void Main(string[] args)
        {              
            System.Console.WriteLine("Введите комманду");
            System.Console.WriteLine("1: Сервер");
            System.Console.WriteLine("2: Клиент");
            
            bool done = false;

            do
            {

                string strSelection = Console.ReadLine ();
                int caseSwitch;

                try
                {
                    caseSwitch = int.Parse(strSelection);
                }
                catch (FormatException)
                {
                    Console.WriteLine ("\r\nWhat?\r\n");
                    continue;
                }
                Console.WriteLine ("Вы выбрали:  " + caseSwitch);

                switch (caseSwitch)
                {
                        case 1:
                            Console.WriteLine("Сервер:");
                            Console.WriteLine("Введите порт для прослушивания:");
                            localPort = Int32.Parse(Console.ReadLine());
                            Server server = new Server(localPort);
                            break;
                        case 2:
                            Console.WriteLine("Клиент:");
                            Console.WriteLine("Введите адрес для подключения:");
                            remoteAddress = Console.ReadLine();
                            Console.Write("Введите порт для подключения: ");
                            remotePort = Int32.Parse(Console.ReadLine()); // порт, к которому мы подключаемся
                            Console.WriteLine("Введите сообщение:");
                            string message = Console.ReadLine();
                            Client client = new Client(message, remoteAddress, remotePort);
                            break;
                        case 0:
                            done = true;
                            break;
                        default:
                            Console.WriteLine ("Комманда не распознанна, введите заного:\r", caseSwitch);
                            continue;
                    }
                }
            while (!done);
            System.Console.WriteLine("Пока");
        }
    }
}
