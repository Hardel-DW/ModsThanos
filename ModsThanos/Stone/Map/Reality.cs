using UnityEngine;

namespace ModsThanos.Stone.Map {

    public static class Reality {
        public static void Place(Vector3 position) {
            if (!GlobalVariable.stoneObjects.ContainsKey("Reality")) {
                new ComponentMap(position, "ModsThanos.Resources.reality-bis.png", "Reality", CustomGameOptions.VisibilityReality);
            }
        }
    }
}