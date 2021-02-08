using UnityEngine;

namespace ModsThanos.Stone.Map {

    public static class Space {
        public static void Place(Vector3 position) {
            if (!GlobalVariable.stoneObjects.ContainsKey("Space")) {
                new ComponentMap(position, "ModsThanos.Resources.space.png", "Space", CustomGameOptions.VisibilitySpace);
            }
        }
    }
}