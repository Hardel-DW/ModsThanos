﻿using HarmonyLib;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(GameOptionsData), nameof(GameOptionsData.Method_24))]
    class GameSettingsPatch {
        static void Postfix(ref string __result) {
            DestroyableSingleton<HudManager>.Instance.GameSettings.scale = 0.425f;
        }
    }

    [HarmonyPatch]
    class GameOptionsMenuPatch {
        static float defaultBounds = 0f;

        [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Start))]
        class Start {
            static void Postfix(ref GameOptionsMenu __instance) {
                defaultBounds = __instance.GetComponentInParent<Scroller>().YBounds.max;
            }
        }

        [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Update))]
        class Update {
            static void Postfix(ref GameOptionsMenu __instance) {
                __instance.GetComponentInParent<Scroller>().YBounds.max = 17f;
            }
        }
    }
}