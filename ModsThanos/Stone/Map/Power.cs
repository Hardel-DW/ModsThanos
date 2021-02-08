using UnityEngine;

namespace ModsThanos.Stone.Map {
    public static class Power {

        public static void Place(Vector3 position) {
            if (!GlobalVariable.stoneObjects.ContainsKey("Power")) {
                new ComponentMap(position, "ModsThanos.Resources.power-bis.png", "Power", CustomGameOptions.VisibilityPower);
            }
        }
    }
}