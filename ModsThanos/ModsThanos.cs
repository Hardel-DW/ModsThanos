﻿using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
using Reactor;
using System;
using System.Linq;
using System.Net;
using UnhollowerBaseLib;

namespace ModsThanos {

    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class ModThanos : BasePlugin
    {
        public const string Id = "gg.fuzeIII.ModsThanos";
        public static ManualLogSource Logger;

        public Harmony Harmony { get; } = new Harmony(Id);

        public ConfigEntry<string> Name { get; set; }

        public ConfigEntry<string> Ip { get; set; }

        public ConfigEntry<ushort> Port { get; set; }

        public override void Load() {
            Logger = Log;
            Logger.LogInfo("ThanosMods est charger !");
            RegisterInIl2CppAttribute.Register();
            Harmony.PatchAll();
            ResourceLoader.LoadAssets();

            #region Cheeps Server
            Name = Config.Bind("Server", "Name", "Cheep-YT.com");
            Ip = Config.Bind("Server", "Ipv4 or Hostname", "207.180.234.175");
            Port = Config.Bind("Server", "Port", (ushort) 22023);

            var defaultRegions = ServerManager.DefaultRegions.ToList();
            var ip = Ip.Value;
            if (Uri.CheckHostName(Ip.Value).ToString() == "Dns") {
                Log.LogMessage("Resolving " + ip + " ...");
                try {
                    foreach (IPAddress address in Dns.GetHostAddresses(Ip.Value)) {
                        if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                            ip = address.ToString();
                            break;
                        }
                    }
                } catch {
                    Log.LogMessage("Hostname could not be resolved" + ip);
                }

                Log.LogMessage("IP is " + ip);
            }

            var port = Port.Value;
            Il2CppReferenceArray<ServerInfo> serverInfo = new ServerInfo[1] { 
                new ServerInfo(Name.Value, Ip.Value, Port.Value) 
            };

            defaultRegions.Insert(0, new StaticRegionInfo(Name.Value, StringNames.NoTranslation, "50", serverInfo).Cast<IRegionInfo>());            

            ServerManager.DefaultRegions = defaultRegions.ToArray();
            #endregion
        }
    }
}
