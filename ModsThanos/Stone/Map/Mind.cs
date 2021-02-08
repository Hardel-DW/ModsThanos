using UnityEngine;

namespace ModsThanos.Stone.Map {

    public static class Mind {
        public static void Place(Vector3 position) {
            if (!GlobalVariable.stoneObjects.ContainsKey("Mind")) {
                new ComponentMap(position, "ModsThanos.Resources.mind-bis.png", "Mind", CustomGameOptions.VisibilityMind);
            }
        }
    }
}