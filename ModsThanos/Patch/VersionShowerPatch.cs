using HarmonyLib;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    public static class VersionShowerPatch {
        public static void Postfix() {
            Reactor.Patches.ReactorVersionShower.Text.Text += " + [BF00D6FF]Thanos[] Mod par Hardel - Serveur de Cheep-YT.com";
        }
    }
}