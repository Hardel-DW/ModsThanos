using ModsThanos.Stone.System.Mind;
using System.Collections.Generic;
using UnityEngine;

namespace ModsThanos {
    public static class GlobalVariable {
        // Player       
        public static List<PlayerControl> allThanos = new List<PlayerControl>();

        // Player Data
        public static byte PlayerColor;
        public static uint PlayerHat;
        public static uint PlayerPet;
        public static uint PlayerSkin;
        public static string PlayerName;
        public static Color PlayerColorName;
        public static List<PlayerData> allPlayersData = new List<PlayerData>();

        // Misc
        public static bool GameStarted = false;
        public static bool UsableButton = true;
    }
}
