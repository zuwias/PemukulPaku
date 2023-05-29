﻿using Common;
using System.Net.NetworkInformation;
using PemukulPaku.GameServer;
using Common.Database;
using PemukulPaku.GameServer.Game;
using Common.Utils.ExcelReader;

namespace PemukulPaku
{
    class Program
    {
        public static void Main()
        {
#if DEBUG
            Global.config.VerboseLevel = VerboseLevel.Debug;
#endif
            Global.c.Log("Starting...");

            Global.config.Gameserver.Host = NetworkInterface.GetAllNetworkInterfaces().Where(i => i.NetworkInterfaceType != NetworkInterfaceType.Loopback && i.OperationalStatus == OperationalStatus.Up).First().GetIPProperties().UnicastAddresses.Where(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First().Address.ToString();

            PacketFactory.LoadPacketHandlers();
            new Thread(HttpServer.Program.Main).Start();
            _ = Server.GetInstance();

            Player Player = new(User.FromName("test"));

            Console.Read();
        }
    }
}