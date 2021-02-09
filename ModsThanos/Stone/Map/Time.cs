using UnityEngine;

namespace ModsThanos.Stone.Map {

    public static class Time {
        public static void Place(Vector3 position) {
            if (!GlobalVariable.stoneObjects.ContainsKey("Time")) {

                ModThanos.Logger.LogInfo(CustomGameOptions.VisibilityTime.ToString());
                new ComponentMap(position, "ModsThanos.Resources.time.png", "Time", CustomGameOptions.VisibilityTime);
            }
        }
    }
}