using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(HatManager), nameof(HatManager.GetHatById))]
    class HatManagerHatsPatch {
        static bool modded = false;
        static List<HatBehaviour> allHats;

        private static void AddHat(Sprite texture, string id) {
            HatBehaviour newHat = new HatBehaviour();
            newHat.MainImage = texture;
            newHat.ProductId = $"+{id}";
            newHat.InFront = true;
            newHat.NoBounce = true;

            allHats.Add(newHat);
        }

        public static void InitHat() {
            foreach (var hat in ResourceLoader.allHats)
                AddHat(hat.Key, hat.Value);
        }

        public static bool Prefix(HatManager __instance) {
            try {
                if (!modded) {
                    modded = true;
                    foreach (var hat in allHats)
                        __instance.AllHats.Add(hat);
                    
                    __instance.AllHats.Sort((Il2CppSystem.Comparison<HatBehaviour>) ((h1, h2) => h2.ProductId.CompareTo(h1.ProductId)));
                }

                return true;
            } catch (Exception e) {
                throw e;
            }
        }
    }
}
