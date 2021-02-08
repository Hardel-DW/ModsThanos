using HarmonyLib;
using UnityEngine;
using ModsThanos.Utility.Enumerations;

namespace ModsThanos {

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public static class Time {
        public static void Postfix(HudManager __instance) {
            GlobalVariable.buttonTime = new CooldownButton
                (() => OnClick(),
                30f,
                "ModsThanos.Resources.time.png",
                300f,
                new Vector2(0f, 2f),
                Visibility.OnlyImpostor,
                __instance,
                5f,
                () => OnEffectEnd(),
                () => OnUpdate(GlobalVariable.buttonTime)
            );
        }

        private static void OnEffectEnd() {
            Stone.System.Time.StopRewind();
        }

        private static void OnClick() {
            Stone.System.Time.StartRewind();
        }

        private static void OnUpdate(CooldownButton button) {
            if (!GlobalVariable.UsableButton)
                button.SetCanUse(false);
            else
                button.SetCanUse(GlobalVariable.hasTimeStone);

            if (Stone.System.Time.isRewinding)
                for (int i = 0; i < 2; i++) Stone.System.Time.Rewind();  
            else Stone.System.Time.Record();
        }
    }
}
