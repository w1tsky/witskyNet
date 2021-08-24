using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace witskyNet
{
    class Program
    {

        static string remoteAddress; // хост для отправки данных
        static int remotePort; // порт для отправки данных
        static int localPort; // локальный порт для прослушивания входящих подключений
        static Blockchain blockchain;
        static void Main(string[] args)
        {              

            string fileName = "blockchain.json";
            string filePath = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            string jsonString = File.ReadAllText(filePath);
            blockchain = JsonSerializer.Deserialize<Blockchain>(jsonString);     
            System.Console.WriteLine("Введите комманду");
            System.Console.WriteLine("1: Сервер");
            System.Console.WriteLine("2: Клиент");
            System.Console.WriteLine("3: Создать цепочку");
            System.Console.WriteLine("4: Добавить блок в цепочку");
            System.Console.WriteLine("5: Просмотреть цепочку");
            
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
                        case 3:
                            Console.WriteLine("Блокчейн:");
                            Console.WriteLine("Введите имя цепочки :");
                            string chainName = Console.ReadLine();
                            blockchain = new Blockchain(chainName);
                            // save chain
                            jsonString = JsonSerializer.Serialize(blockchain);
                            File.WriteAllText(filePath, jsonString);
                            Console.WriteLine(File.ReadAllText(filePath));     
                            continue;
                        case 4:
                            Console.WriteLine("Добавить блок:");
                            Console.WriteLine("Введите имя цепочки :");   
                            chainName = Console.ReadLine();
                            Console.WriteLine("Введите данные для блока:");  
                            string blockData = Console.ReadLine();
                            Block lastBlock = blockchain.GetLatestBlock();
                            blockchain.AddBlock(new Block(lastBlock.Hash, blockData));
                            jsonString = JsonSerializer.Serialize(blockchain);
                            File.WriteAllText(filePath, jsonString);
                            Console.WriteLine(File.ReadAllText(filePath));     
                            break;
                        case 5:      
                            System.Console.WriteLine(blockchain.BlockchainName);  
                            foreach(var b in blockchain.Chain)
                            {
                                System.Console.WriteLine($"Index: {b.Index}");
                                System.Console.WriteLine($"PreviousHash: {b.PreviousHash}");
                                System.Console.WriteLine($"Hash: {b.Hash}");
                                System.Console.WriteLine($"Data: {b.Data}");
                                System.Console.WriteLine("-------------------");   
                            }
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
