using HarmonyLib;
using System.Globalization;

namespace ModsThanos.Patch {
    [HarmonyPatch(typeof(RegionMenu), nameof(RegionMenu.OnEnable))]
    public static class ServerRegionPatch {
        public static bool Prefix(ref RegionMenu __instance) {
            if (ServerManager.DefaultRegions.Count != 4) {
                UnhollowerBaseLib.Il2CppReferenceArray<ServerInfo> ModdedServer = new ServerInfo[1] { new ServerInfo("Modded", "yourIpHere", 22023) };
                var regions = new RegionInfo[4] { ServerManager.DefaultRegions[0], ServerManager.DefaultRegions[1], ServerManager.DefaultRegions[2],
                        new RegionInfo("Modded", "0", ModdedServer) };

                ServerManager.DefaultRegions = regions;
            }

            return true;
        }
    }
}
