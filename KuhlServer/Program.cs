using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace KuhlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatServer ChatServer = new ChatServer();
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "/clients":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Connected clients");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        foreach (DictionaryEntry entry in ChatServer.User)
                        {
                            Console.WriteLine("{1}, {0}", entry.Key, entry.Value);
                        }
                        Console.WriteLine("----------------");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case "/exit":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Sevrer is shutting down");
                        Console.ForegroundColor = ConsoleColor.White;
                        ChatServer.Server.stop();
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
