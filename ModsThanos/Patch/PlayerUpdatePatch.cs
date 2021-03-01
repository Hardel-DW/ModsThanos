using HarmonyLib;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    class PlayerUpdatePatch {
        public static void Postfix(PlayerControl __instance) {
            if (GlobalVariable.useSnap)
                Stone.System.Snap.Incremente();

            if (PlayerControl.LocalPlayer != null) {
                if (GlobalVariable.hasSoulStone && PlayerControl.LocalPlayer.Data.IsDead && !RoleHelper.IsThanos(PlayerControl.LocalPlayer.PlayerId))
                    Stone.StoneDrop.TryReplaceStone("Soul");

                if (RoleHelper.IsThanos(PlayerControl.LocalPlayer.PlayerId) && PlayerControl.LocalPlayer.Data.IsDead)
                    if (GlobalVariable.hasSoulStone || GlobalVariable.hasMindStone || GlobalVariable.hasPowerStone || GlobalVariable.hasSpaceStone || GlobalVariable.hasTimeStone || GlobalVariable.hasRealityStone) {
                        foreach (var stone in GlobalVariable.stonesNames)
                            Stone.StoneDrop.TryReplaceStone(stone);                 
                }
            }
        }
    }
}
