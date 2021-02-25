using HarmonyLib;
using UnityEngine;
using ModsThanos.Utility.Enumerations;

namespace ModsThanos {

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public static class Space {
        public static void Postfix(HudManager __instance) {
            GlobalVariable.buttonSpace = new CooldownButton
                (() => OnClick(),
                CustomGameOptions.CooldownSpaceStone.GetValue(),
                "ModsThanos.Resources.space.png",
                300f,
                new Vector2(0f, 1f),
                Visibility.OnlyImpostor,
                __instance,
                () => OnUpdate(GlobalVariable.buttonSpace)
            );
        }

        private static void OnClick() {
            Stone.System.Space.OnSpacePressed();
        }

        private static void OnUpdate(CooldownButton button) {
            if (!GlobalVariable.UsableButton) button.SetCanUse(false);
            else button.SetCanUse(GlobalVariable.hasSpaceStone);
        }
    }
}
