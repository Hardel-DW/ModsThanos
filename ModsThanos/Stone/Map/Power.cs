using UnityEngine;

namespace ModsThanos.Stone.Map {
    public static class Power {

        public static void Place(Vector3 position) {
            if (!GlobalVariable.stoneObjects.ContainsKey("Power")) {

                ModThanos.Logger.LogInfo(CustomGameOptions.VisibilityPower.ToString());
                new ComponentMap(position, "ModsThanos.Resources.power.png", "Power", CustomGameOptions.VisibilityPower);
            }
        }
    }
}