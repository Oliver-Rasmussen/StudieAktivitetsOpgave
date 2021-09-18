﻿using System;
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
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);

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
                        HentAlle(reader, writer);
                        break;

                    case "hent":
                        Hent(reader, writer);
                        break;

                    case "gem":
                        Gem(reader, writer);
                        break;

                    default:
                        writer.WriteLine("Unknown command");
                        writer.Flush();
                        break;
                }

            }

            socket.Close();
        }

        public static void HentAlle(StreamReader reader, StreamWriter writer)
        {
            Console.WriteLine("---HentAlle Request---");

            string msg = reader.ReadLine().ToLower();

            if (msg.Equals(""))
            {
                writer.WriteLine(JsonSerializer.Serialize(_players));
                writer.Flush();
            }
        }

        public static void Hent(StreamReader reader, StreamWriter writer)
        {
            Console.WriteLine("---Hent Request---");

            int id = Int32.Parse(reader.ReadLine());

            var player = _players.Find(p => p.ID == id);

            writer.WriteLine(JsonSerializer.Serialize(player));
        }

        public static void Gem(StreamReader reader, StreamWriter writer)
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