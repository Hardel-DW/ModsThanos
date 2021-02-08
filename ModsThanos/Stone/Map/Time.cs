using UnityEngine;

namespace ModsThanos.Stone.Map {

    public static class Time {
        public static void Place(Vector3 position) {
            if (!GlobalVariable.stoneObjects.ContainsKey("Time")) {
                new ComponentMap(position, "ModsThanos.Resources.time-bis.png", "Time", CustomGameOptions.VisibilityTime);
            }
        }
    }
}