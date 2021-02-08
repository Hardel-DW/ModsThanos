using HarmonyLib;
using UnityEngine;
using ModsThanos.Utility.Enumerations;

namespace ModsThanos {

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public static class Soul {
        public static void Postfix(HudManager __instance) {
            GlobalVariable.buttonSoul = new CooldownButton
                (() => OnClick(),
                0f,
                "ModsThanos.Resources.soul-bis.png",
                300f,
                new Vector2(0f, 0f),
                Visibility.OnlyImpostor,
                __instance,
                () => OnUpdate(GlobalVariable.buttonSoul)
            );
        }

        private static void OnClick() { }

        private static void OnUpdate(CooldownButton button) {
            if (!GlobalVariable.UsableButton)
                button.SetCanUse(false);
            else
                button.SetCanUse(GlobalVariable.hasSoulStone);
        }
    }
}
