using HarmonyLib;
using UnityEngine;
using ModsThanos.Utility.Enumerations;

namespace ModsThanos {

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public static class Power {
        public static void Postfix(HudManager __instance) {
            GlobalVariable.buttonPower = new CooldownButton
                (() => OnClick(),
                30f,
                "ModsThanos.Resources.power.png",
                300f,
                new Vector2(1f, 1f),
                Visibility.OnlyImpostor,
                __instance,
                () => OnUpdate(GlobalVariable.buttonPower)
            );
        }

        private static void OnClick() {
            Stone.System.Power.OnPowerPressed();
        }

        private static void OnUpdate(CooldownButton button) {
            if (!GlobalVariable.UsableButton)
                button.SetCanUse(false);
            else
                button.SetCanUse(GlobalVariable.hasPowerStone);
        }
    }
}
