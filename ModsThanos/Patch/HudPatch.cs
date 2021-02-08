using HarmonyLib;
using ModsThanos.Patch.CustomGameOption;
using ModsThanos.Utility;
using UnityEngine;

namespace ModsThanos.Patch {

    public static class HudPatch {
        public static void UpdateMeetingHUD(MeetingHud __instance) {
            foreach (PlayerVoteArea player in __instance.playerStates) {
                if (player.NameText.Text == GlobalVariable.PlayerSoulStone.name && !GlobalVariable.mindStoneUsed)
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
                    if (player.PlayerId == GlobalVariable.PlayerSoulStone.PlayerId && !GlobalVariable.mindStoneUsed)
                        player.nameText.Color = new Color(1f, 0.65f, 0f, 1f);

            if (GlobalVariable.realityStoneUsed && PlayerControl.AllPlayerControls.Count > 1 && GlobalVariable.Thanos != null)
                Player.FromPlayerIdFFGALNPKCD(GlobalVariable.Thanos.PlayerId).nameText.Color = new Color(1f, 1f, 1f, 0f);

            if (PlayerControl.AllPlayerControls.Count > 1 && GlobalVariable.Thanos != null && PlayerControlPatch.IsThanos(FFGALNAPKCD.LocalPlayer)) {
                PlayerControl.LocalPlayer.nameText.Color = new Color(0.749f, 0f, 0.839f, 1f);
                __instance.TaskText.Text = "[FFFFFFFF]Objectif: Trouver les pierres pour obtenir le snap.[]\n\n[808080FF]Snap:[] Termine la partie.\n[008516FF]Pierre du temps :[] Permet de revenir dans le temps.\n[822FA8FF]Pierre de pouvoir :[] Permet de tuer en zone.\n[C46f1AFF]Pierre de l'âme :[] les crewmate peuvent la ramasser.\n[A6A02EFF]Pierre de l'esprit :[] Permet de se transformer en quelqu'un.\n[3482BAFF]Pierre de l'espace[]: Pose des portails.\n[D43D3DFF]Pierre de Réalité[]: Permet de se rendre invisible";
            }

            CooldownButton.HudUpdate();
            GameOptionsHud.UpdateGameMenu();
        }
    }
}