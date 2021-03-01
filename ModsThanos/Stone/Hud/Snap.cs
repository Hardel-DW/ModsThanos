using HarmonyLib;
using UnityEngine;
using ModsThanos.Utility.Enumerations;

namespace ModsThanos {

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public static class Snap {
        public static void Postfix(HudManager __instance) {
            GlobalVariable.buttonSnap = new CooldownButton
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
            //ModThanos.Logger.LogInfo("Snap Click");
            Stone.System.Snap.OnSnapPressed();
            //ModThanos.Logger.LogInfo("Snap Click End");
        }

        private static void OnUpdate(CooldownButton button) {
            if (!GlobalVariable.UsableButton || CustomGameOptions.DisableSnap.GetValue()) {
                button.SetCanUse(false);
            } else {
                if (GlobalVariable.hasMindStone && GlobalVariable.hasSpaceStone && GlobalVariable.hasPowerStone && GlobalVariable.hasTimeStone && GlobalVariable.hasSoulStone && GlobalVariable.hasRealityStone)
                    button.SetCanUse(true);
                else
                    button.SetCanUse(false);        
            }
        }
    }
}
