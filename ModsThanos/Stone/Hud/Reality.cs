using HarmonyLib;
using UnityEngine;
using ModsThanos.Utility.Enumerations;

namespace ModsThanos {

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public static class Reality {
        public static void Postfix(HudManager __instance) {
            GlobalVariable.buttonReality = new CooldownButton
                (() => onClick(),
                30f,
                "ModsThanos.Resources.reality-bis.png",
                300f,
                new Vector2(1f, 0f),
                Visibility.OnlyImpostor,
                __instance,
                10f,
                () => OnEffectEnd(),
                () => OnUpdate(GlobalVariable.buttonReality)
            );
        }

        private static void OnEffectEnd() {
            Stone.System.Reality.OnRealityPressed(false);
        }

        private static void onClick() {
            Stone.System.Reality.OnRealityPressed(true);
        }

        private static void OnUpdate(CooldownButton button) {
            if (!GlobalVariable.UsableButton)
                button.SetCanUse(false);
            else
                button.SetCanUse(GlobalVariable.hasRealityStone);
        }
    }
}
