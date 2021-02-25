using HarmonyLib;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Close))]
    public static class MeetingClosePatch {

        public static void Postfix(MeetingHud __instance) {
            if (GlobalVariable.allThanos != null) {
                if (RoleHelper.IsThanos(PlayerControl.LocalPlayer.PlayerId)) {
                    GlobalVariable.buttonMind.Timer = CustomGameOptions.CooldownMindStone.GetValue();
                    GlobalVariable.buttonPower.Timer = CustomGameOptions.CooldownPowerStone.GetValue();
                    GlobalVariable.buttonReality.Timer = CustomGameOptions.CooldownRealityStone.GetValue();
                    GlobalVariable.buttonSoul.Timer = CustomGameOptions.CooldownSoulStone.GetValue();
                    GlobalVariable.buttonTime.Timer = CustomGameOptions.CooldownTimeStone.GetValue();
                    GlobalVariable.buttonSpace.Timer = CustomGameOptions.CooldownSpaceStone.GetValue();
                    GlobalVariable.buttonSnap.Timer = 15f;
                }
            }
        }
    }

    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
    public static class MeetingStartPatch {
        public static void Postfix(MeetingHud __instance) {
            if (GlobalVariable.allThanos != null) {
                if (RoleHelper.IsThanos(PlayerControl.LocalPlayer.PlayerId)) {
                    GlobalVariable.buttonMind.SetCanUse(false);
                    GlobalVariable.buttonPower.SetCanUse(false);
                    GlobalVariable.buttonSnap.SetCanUse(false);
                    GlobalVariable.buttonReality.SetCanUse(false);
                    GlobalVariable.buttonMind.SetCanUse(false);
                    GlobalVariable.buttonTime.SetCanUse(false);
                    GlobalVariable.buttonSpace.SetCanUse(false);
                }
            }
        }
    }
}