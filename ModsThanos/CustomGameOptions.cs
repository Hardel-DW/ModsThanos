using Essentials.CustomOptions;
using ModsThanos.Utility.Enumerations;

namespace ModsThanos {
    public static class CustomGameOptions {
        public static string[] visibilityValue = new string[] { "Everyone", "Thanos", "Crewmate" };

        public static CustomToggleOption EnableThanosMods = CustomOption.AddToggle("Enable Thanos Mods", true);
        public static CustomStringOption ThanosSide = CustomOption.AddString("Thanos Side", new string[] { "Impostor", "Crewmate" });
        public static CustomNumberOption NumberThanos = CustomOption.AddNumber("Thanos Number", 1f, 1f, 10f, 1f);
        public static CustomToggleOption DisableSnap = CustomOption.AddToggle("Disable Snap", false);

        public static CustomNumberOption CooldownTimeStone = CustomOption.AddNumber("Cooldown Time Stone", 30f, 10f, 300f, 5f);
        public static CustomNumberOption CooldownRealityStone = CustomOption.AddNumber("Cooldown Reality Stone", 10f, 10f, 300f, 5f);
        public static CustomNumberOption CooldownSoulStone = CustomOption.AddNumber("Cooldown Soul Stone", 30f, 10f, 300f, 5f);
        public static CustomNumberOption CooldownSpaceStone = CustomOption.AddNumber("Cooldown Space Stone", 30f, 10f, 300f, 5f);
        public static CustomNumberOption CooldownMindStone = CustomOption.AddNumber("Cooldown Mind Stone", 30f, 10f, 300f, 5f);
        public static CustomNumberOption CooldownPowerStone = CustomOption.AddNumber("Cooldown Power Stone", 30f, 10f, 300f, 5f);
        public static CustomNumberOption TimeDuration = CustomOption.AddNumber("Time Duration", 5f, 5f, 40f, 2.5f);
        public static CustomNumberOption RealityDuration = CustomOption.AddNumber("Reality Duration", 10f, 5f, 40f, 2.5f);
        public static CustomNumberOption MindDuration = CustomOption.AddNumber("Mind Duration", 4f, 5f, 40f, 2.5f);
        public static CustomNumberOption MaxPortal = CustomOption.AddNumber("Max Portal", 4f, 1f, 20f, 1f);
        public static CustomNumberOption StoneSize = CustomOption.AddNumber("Stone Size", 320f, 50f, 1000f, 10f);

        public static CustomStringOption VisibilityTime = CustomOption.AddString("Time Stone Visibility", visibilityValue);
        public static CustomStringOption VisibilityPower = CustomOption.AddString("Power Stone Visibility", visibilityValue);
        public static CustomStringOption VisibilityMind = CustomOption.AddString("Mind Stone Visibility", visibilityValue);
        public static CustomStringOption VisibilitySoul = CustomOption.AddString("Soul Stone Visibility", visibilityValue);
        public static CustomStringOption VisibilitySpace = CustomOption.AddString("Space Stone Visibility", visibilityValue);
        public static CustomStringOption VisibilityReality = CustomOption.AddString("Reality Stone Visibility", visibilityValue);

        public static Visibility VisibilityStringToEnum(string visibility) {
            return visibility switch {
                "Everyone" => Visibility.Everyone,
                "Thanos" => Visibility.OnlyImpostor,
                "Crewmate" => Visibility.OnlyCrewmate,
                _ => Visibility.OnlyImpostor,
            };
        }

        public static Visibility SideStringToEnum(string visibility) {
            return visibility switch
            {
                "Everyone" => Visibility.Everyone,
                "Impostor" => Visibility.OnlyImpostor,
                "Crewmate" => Visibility.OnlyCrewmate,
                _ => Visibility.OnlyImpostor,
            };
        }
    }
}
