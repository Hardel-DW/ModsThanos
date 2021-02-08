using HarmonyLib;
using ModsThanos.Patch.CustomGameOption;
using UnityEngine;

namespace ModsThanos.Patch {

    public static class HudPatch {
        public static void UpdateMeetingHUD(MeetingHud __instance) {
            foreach (PlayerVoteArea player in __instance.playerStates) {
                if (player.NameText.Text == GlobalVariable.PlayerSoulStone.name && !GlobalVariable.soulStoneUsed)
                    player.NameText.Color = new Color(1f, 0.65f, 0f, 1f);

                if (player.NameText.Text == GlobalVariable.Thanos.name && PlayerControlPatch.IsThanos(FFGALNAPKCD.LocalPlayer))
                    player.NameText.Color = new Color(0.639f, 0.501f, 0f, 1f);
            }
        }
    }

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public static class HudUpdatePatch {
        public static void Postfix(HudManager __instance) {
            if (MeetingHud.Instance != null)
                HudPatch.UpdateMeetingHUD(MeetingHud.Instance);

            foreach (var player in PlayerControl.AllPlayerControls)
                if (PlayerControl.AllPlayerControls.Count > 1 && GlobalVariable.PlayerSoulStone != null)
                    if (player.PlayerId == GlobalVariable.PlayerSoulStone.PlayerId)
                        player.nameText.Color = new Color(1f, 0.65f, 0f, 1f);

            if (PlayerControl.AllPlayerControls.Count > 1 && GlobalVariable.Thanos != null && PlayerControlPatch.IsThanos(FFGALNAPKCD.LocalPlayer))
                PlayerControl.LocalPlayer.nameText.Color = new Color(0.749f, 0f, 0.839f, 1f);

            CooldownButton.HudUpdate();
            GameOptionsHud.UpdateGameMenu();
        }
    }
}