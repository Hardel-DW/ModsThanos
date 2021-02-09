using UnityEngine;

namespace ModsThanos.Stone.Map {

    public static class Mind {
        public static void Place(Vector3 position) {
            if (!GlobalVariable.stoneObjects.ContainsKey("Mind")) {
                ModThanos.Logger.LogInfo(CustomGameOptions.VisibilityMind.ToString());
                new ComponentMap(position, "ModsThanos.Resources.mind.png", "Mind", CustomGameOptions.VisibilityMind);
            }
        }
    }
}