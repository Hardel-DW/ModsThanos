using HarmonyLib;
using UnityEngine;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(Vent), "CanUse")]
    public static class VentPatch {
        public static bool Prefix(Vent __instance, ref float __result, [HarmonyArgument(0)], GameData.PlayerInfo playerInfo, [HarmonyArgument(1)] out bool canUse, [HarmonyArgument(2)] out bool couldUse) {
            float maxFloat = float.MaxValue;
            PlayerControl player = playerInfo.Object;
            couldUse = (playerInfo.IsImpostor || RoleHelper.IsThanos(playerInfo.PlayerId) && !playerInfo.IsDead && (player.CanMove || player.inVent))
            canUse = couldUse;
            if (canUse) {
                maxFloat = Vector2.Distance(player.GetTruePosition(), __instance.transform.position);
                canUse &= maxFloat <= __instance.UsableDistance;
            }

            __result = maxFloat;
            return false;
        }
    }
}
