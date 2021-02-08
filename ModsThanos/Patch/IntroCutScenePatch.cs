using HarmonyLib;
using UnityEngine;
using IntroCutScene = PENEIDJGGAF.CKACLKCOJFO;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(IntroCutScene), nameof(IntroCutScene.MoveNext))]
    public static class IntroCutScenePatch {
        public static void Postfix(IntroCutScene __instance) {
            if (PlayerControlPatch.IsThanos(FFGALNAPKCD.LocalPlayer)) {
                __instance.__this.Title.Text = "Thanos";
                __instance.__this.Title.Color = new Color(0.749f, 0f, 0.839f, 1f);
                __instance.__this.ImpostorText.Text = "Trouver les pierres, et Défier les crewmates.";
                __instance.__this.BackgroundBar.material.color = new Color(0.749f, 0f, 0.839f, 1f);
            }
        }
    }
}
