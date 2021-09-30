using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using Opgave_1;
using System.Text.Json;

namespace TCP_Server
{
    class Program
    {
        private static StreamReader reader;
        private static StreamWriter writer;

        public static List<FootballPlayer> _players = new List<FootballPlayer>
        {
            new FootballPlayer() {ID = 1, Name = "Erik Hansen", Price= 22000, ShirtNumber=13},
            new FootballPlayer() {ID = 2, Name = "Karl Eriksen", Price= 99882, ShirtNumber=87},
            new FootballPlayer() {ID = 3, Name = "Hans Ziegler", Price= 87633, ShirtNumber=12},
            new FootballPlayer() {ID = 4, Name = "Johan Salo", Price= 44000, ShirtNumber=74},
        };

        static void Main(string[] args)
        {
            Console.WriteLine("TCP-Server");

            TcpListener listner = new TcpListener(IPAddress.Any, 2121);
            listner.Start();

            while (true)
            {
                TcpClient socket = listner.AcceptTcpClient();

                Task.Run(() =>
                {
                    HandleClient(socket);
                });
            }
        }

        public static void HandleClient(TcpClient socket)
        {
            Console.WriteLine("Client joined");

            string msg = "";

            NetworkStream ns = socket.GetStream();
            reader = new StreamReader(ns);
            writer = new StreamWriter(ns);

            writer.WriteLine("Write 'end' to disconnect");
            writer.Flush();

            while (!msg.Equals("end"))
            {
                writer.WriteLine("\n----------\nCommands:\n1. HentAlle + Blank\n2. Hent + ID\n3. Gem + Json objekt\n----------------");
                writer.Flush();

                msg = reader.ReadLine().ToLower();

                switch(msg)
                {
                    case "hentalle":
                        HentAlle();
                        break;

                    case "hent":
                        Hent();
                        break;

                    case "gem":
                        Gem();
                        break;

                    case "end":
                        writer.WriteLine("Bye bye");
                        writer.Flush();
                        break;

                    default:
                        writer.WriteLine("Unknown command");
                        writer.Flush();
                        break;
                }

            }

            socket.Close();
        }

        public static void HentAlle()
        {
            Console.WriteLine("---HentAlle Request---");

            string msg = reader.ReadLine().ToLower();

            if (msg.Equals(""))
            {
                foreach(FootballPlayer player in _players)
                {
                    writer.WriteLine(JsonSerializer.Serialize(player));
                }
                
                writer.Flush();
            }
        }

        public static void Hent()
        {
            Console.WriteLine("---Hent Request---");

            int id = Int32.Parse(reader.ReadLine());

            var player = _players.Find(p => p.ID == id);

            writer.WriteLine(JsonSerializer.Serialize(player));
            writer.Flush();
        }

        public static void Gem()
        {
            Console.WriteLine("---Gem Request---");

            FootballPlayer player = new FootballPlayer();
            string msg = reader.ReadLine();

            player = JsonSerializer.Deserialize<FootballPlayer>(msg);

            _players.Add(player);

            writer.WriteLine("Player Saved to database");
            writer.Flush();
        }
    }
}
