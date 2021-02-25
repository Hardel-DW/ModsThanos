using HarmonyLib;
using UnityEngine;
using ModsThanos.Utility.Enumerations;

namespace ModsThanos {

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public static class Mind {
        public static void Postfix(HudManager __instance) {
            GlobalVariable.buttonMind = new CooldownButton
                (() => OnClick(),
                CustomGameOptions.CooldownMindStone.GetValue(),
                "ModsThanos.Resources.mind.png",
                300f,
                new Vector2(1f, 2f),
                Visibility.OnlyImpostor,
                __instance,
                CustomGameOptions.CooldownMindStone.GetValue(),
                () => OnEffectEnd(),
                () => OnUpdate(GlobalVariable.buttonMind)
            );
        }

        private static void OnEffectEnd() {
            Stone.System.Mind.OnMindEnded();
        }

        private static void OnClick() {
            Stone.System.Mind.OnMindPressed();
        }

        private static void OnUpdate(CooldownButton button) {
            if (!GlobalVariable.UsableButton)
                button.SetCanUse(false);
            else
                button.SetCanUse(GlobalVariable.hasMindStone);
        }
    }
}
