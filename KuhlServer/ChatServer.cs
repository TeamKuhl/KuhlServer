using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationLibrary;
using System.Net.Sockets;
using System.Collections;

namespace KuhlServer
{
    class ChatServer
    {

        public Server Server;
        public Hashtable User = new Hashtable();

        public ChatServer()
        {
            Server = new Server();
            Server.start(45454);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Server started on Port 45454");
            Console.ForegroundColor = ConsoleColor.White;

            Server.onConnect += new ServerConnectHandler(ConnectEvent);
            Server.onDisconnect += new ServerDisconnectHandler(DisconnectEvent);
            Server.onReceive += new ServerReceiveHandler(ReceiveEvent);
        }

        public void ConnectEvent(TcpClient client)
        {
            Console.WriteLine("Connected: " + client.Client.GetHashCode());
            Server.send(client, "welcome", "");
        }

        public void DisconnectEvent(TcpClient client)
        {
            Console.WriteLine("Client " + User[client.Client.GetHashCode()] + "(" + client.Client.GetHashCode() + ") disconnected from Server");
            User.Remove(client.Client.GetHashCode());
        }

        public void ReceiveEvent(TcpClient client, String type, String message)
        {
            switch(type)
            {
                case "message":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Client " + User[client.Client.GetHashCode()] + "(" + client.Client.GetHashCode() + ") send message: " + message);
                    Console.ForegroundColor = ConsoleColor.White;
                    message = User[client.Client.GetHashCode()] + ": " + message;
                    Server.sendToAll("message", message);
                    break;
                case "name":
                    User[client.Client.GetHashCode()] = message;
                    Console.WriteLine("Client " + client.Client.GetHashCode() + " is connected as " + message);
                    break;
            }
        }
    }
}
