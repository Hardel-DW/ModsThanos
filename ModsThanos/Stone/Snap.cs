using HarmonyLib;
using UnityEngine;
using ModsThanos.Utility.Enumerations;

namespace ModsThanos.Stone {

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public static class Snap {
        public static void Postfix(HudManager __instance) {
            new CooldownButton
                (() => OnClick(),
                15f,
                "ModsThanos.Resources.snap.png",
                450f,
                new Vector2(0.5f,3f),
                Visibility.OnlyImpostor,
                __instance,
                5f,
                () => OnEndedSnap(),
                () => OnUpdate(GlobalVariable.buttonSnap)
            );
        }

        private static void OnEndedSnap() {
            Stone.System.Snap.OnSnapEnded();
        }

        private static void OnClick() {
            Stone.System.Snap.OnSnapPressed();
        }

        private static void OnUpdate(CooldownButton button) {
            if (!GlobalVariable.UsableButton || CustomGameOptions.DisableSnap.GetValue()) {
                button.SetCanUse(false);
            } else {
                if (StoneManager)
                    button.SetCanUse(true);
                else
                    button.SetCanUse(false);        
            }
        }
    }
}
