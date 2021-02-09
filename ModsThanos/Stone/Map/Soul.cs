using ModsThanos.Utility;
using System;
using UnityEngine;

namespace ModsThanos.Stone.Map {
    public static class Soul {

        public static void Place(Vector3 position) {

            if (!GlobalVariable.stoneObjects.ContainsKey("Soul")) {
                ModThanos.Logger.LogInfo(CustomGameOptions.VisibilitySoul.ToString());
                new ComponentMap(position, "ModsThanos.Resources.soul.png", "Soul", CustomGameOptions.VisibilitySoul);


                if (AmongUsClient.Instance.GameMode == GameModes.FreePlay || !PlayerControl.LocalPlayer.Data.IsImpostor) {
                    var gameObject = new GameObject { layer = 5 };
                    var arrow = gameObject.AddComponent<DGPHMLNNPDN>();
                    var renderer = gameObject.AddComponent<SpriteRenderer>();
                    arrow.image = renderer;
                    arrow.target = position;
                    renderer.sprite = HelperSprite.LoadSpriteFromEmbeddedResources("ModsThanos.Resources.arrow.png", 150);
                    GlobalVariable.arrow = gameObject;
                }
            }
        }
    }
}