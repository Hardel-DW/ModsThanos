using UnityEngine;

namespace ModsThanos.Stone.Map {

    public static class Reality {
        public static void Place(Vector3 position) {
            if (!GlobalVariable.stoneObjects.ContainsKey("Reality")) {
                ModThanos.Logger.LogInfo(CustomGameOptions.VisibilityReality.ToString());
                new ComponentMap(position, "ModsThanos.Resources.reality.png", "Reality", CustomGameOptions.VisibilityStringToEnum(CustomGameOptions.VisibilityReality.GetText()));
            }
        }
    }
}