using HarmonyLib;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    public static class VersionShowerPatch {
        public static void Postfix(VersionShower __instance) {
            __instance.text.Text += " + [BF00D6FF]Thanos[] Mod par Hardel - Serveur de Cheep-YT.com";
        }
    }
}