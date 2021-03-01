using HarmonyLib;
using ModsThanos.Utility;

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
    public static class MeetingUpdatePatch {
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

    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Awake))]
    public static class MeetingStartPatch {
        public static void Postfix(MeetingHud __instance) {
            if (GlobalVariable.allThanos != null && GlobalVariable.mindStoneUsed) {
                foreach (var playerData in GlobalVariable.allPlayersData) {
                    PlayerControl currentPlayer = PlayerControlUtils.FromPlayerId(playerData.PlayerId);

                    currentPlayer.SetPet(playerData.PlayerPet);
                    currentPlayer.SetSkin(playerData.PlayerSkin);
                    currentPlayer.SetName(playerData.PlayerName);
                    currentPlayer.SetHat(playerData.PlayerHat, playerData.PlayerColor);
                    currentPlayer.SetColor(playerData.PlayerColor);
                    GlobalVariable.mindStoneUsed = false;
                }
            }

            Stone.System.Time.StopRewind();
            if (GlobalVariable.allThanos != null && GlobalVariable.realityStoneUsed && RoleHelper.IsThanos(PlayerControl.LocalPlayer.PlayerId)) {
                Stone.System.Reality.OnRealityPressed(false);
            }
        }
    }
}