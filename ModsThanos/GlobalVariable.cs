using ModsThanos.Stone.System.Mind;
using System.Collections.Generic;
using UnityEngine;

namespace ModsThanos {
    public static class GlobalVariable {

        // Player       
        public static List<PlayerControl> allThanos = new List<PlayerControl>();
        public static PlayerControl PlayerSoulStone;

        // Stone Name
        public static string[] stonesNames = new string[] { "Reality", "Power", "Space", "Mind", "Soul", "Time" };

        // Stone Possession
        public static bool hasTimeStone = false;
        public static bool hasPowerStone = false;
        public static bool hasMindStone = false;
        public static bool hasSpaceStone = false;
        public static bool hasSoulStone = false;
        public static bool hasRealityStone = false;

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
        public static bool useSnap = false;
        public static GameObject arrow;
        public static bool mindStoneUsed = false;
        public static bool realityStoneUsed = false;

        // Button
        public static CooldownButton buttonTime = null;
        public static CooldownButton buttonSpace = null;
        public static CooldownButton buttonSoul = null;
        public static CooldownButton buttonSnap = null;
        public static CooldownButton buttonReality = null;
        public static CooldownButton buttonMind = null;
        public static CooldownButton buttonPower = null;

        // Dictionary
        internal static Dictionary<string, Vector2> stonePositon = new Dictionary<string, Vector2>();
        internal static Dictionary<string, ComponentMap> stoneObjects = new Dictionary<string, ComponentMap>();
    }
}
