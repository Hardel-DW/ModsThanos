using ModsThanos.Patch.CustomGameOption;
using ModsThanos.Utility.Enumerations;
using System.Collections.Generic;

namespace ModsThanos {
    public static class CustomGameOptions {

        public static float CooldownTime = 30f;
        public static float CooldownPower = 30f;
        public static float CooldownMind = 30f;
        public static float CooldownSoul = 30f;
        public static float CooldownSpace = 30f;
        public static float CooldownReality = 30f;

        public static float RealityDuration = 10f;
        public static float MindDuration = 10f;
        public static float TimeDuration = 5f;
        public static int maxPortal = 4;
        public static float stoneSize = 320;

        public static Visibility VisibilityTime = Visibility.Everyone;
        public static Visibility VisibilityPower = Visibility.Everyone;
        public static Visibility VisibilityMind = Visibility.Everyone;
        public static Visibility VisibilitySoul = Visibility.Everyone;
        public static Visibility VisibilitySpace = Visibility.Everyone;
        public static Visibility VisibilityReality = Visibility.Everyone;

        // Custom Game Options
        public static List<CustomNumberOptions> numberOptions = new List<CustomNumberOptions>();
        public static List<CustomStringOptions> stringOptions = new List<CustomStringOptions>();
        public static PCGDGFIAJJI ButtonCooldownTime;
        public static PCGDGFIAJJI ButtonTimeDuration;
        public static PCGDGFIAJJI ButtonCooldownReality;
        public static PCGDGFIAJJI ButtonRealityDuration;
        public static PCGDGFIAJJI ButtonCooldownSpace;
        public static PCGDGFIAJJI ButtonMaxPortal;
        public static PCGDGFIAJJI ButtonCooldownMind;
        public static PCGDGFIAJJI ButtonMindDuration;
        public static PCGDGFIAJJI ButtonCooldownSoul;
        public static PCGDGFIAJJI ButtonStoneSize;

        public static PCGDGFIAJJI ButtonVisibilityTime;
        public static PCGDGFIAJJI ButtonVisibilitySpace;
        public static PCGDGFIAJJI ButtonVisibilitynMind;
        public static PCGDGFIAJJI ButtonVisibilityReality;
        public static PCGDGFIAJJI ButtonVisibilityPower;
        public static PCGDGFIAJJI ButtonVisibilitySoul;

        public static string[] visibilityValue = new string[] {"Everyone", "Thanos", "Crewmate"};

        public static List<CustomNumberOptions> InitNumberOptions() {
            numberOptions.Add(new CustomNumberOptions("Cooldown Time Stone", CooldownTime, 300f, 10f, 5f, ButtonCooldownTime, () => {
                CooldownTime = numberOptions.Find(item => item.Name == "Cooldown Time Stone").Value;
                ModThanos.Logger.LogInfo($"CooldownTime: {CooldownTime}");
                GlobalVariable.buttonTime.MaxTimer = CooldownTime;
            }));

            numberOptions.Add(new CustomNumberOptions("Time Duration", TimeDuration, 30f, 5f, 2.5f, ButtonTimeDuration, () => {
                TimeDuration = numberOptions.Find(item => item.Name == "Time Duration").Value;
                GlobalVariable.buttonTime.EffectDuration = TimeDuration;
                Stone.System.Time.recordTime = TimeDuration;
            }));

            numberOptions.Add(new CustomNumberOptions("Cooldown Reality Stone", CooldownReality, 300f, 10f, 5f, ButtonCooldownReality, () => {
                CooldownReality = numberOptions.Find(item => item.Name == "Cooldown Reality Stone").Value;
                GlobalVariable.buttonReality.MaxTimer = CooldownReality;
            }));

            numberOptions.Add(new CustomNumberOptions("Reality Duration", RealityDuration, 60f, 5f, 5f, ButtonRealityDuration, () => {
                RealityDuration = numberOptions.Find(item => item.Name == "Reality Duration").Value;
                GlobalVariable.buttonReality.EffectDuration = RealityDuration;
            }));

            numberOptions.Add(new CustomNumberOptions("Cooldown Space Stone", CooldownSpace, 300f, 10f, 5f, ButtonCooldownSpace, () => {
                CooldownSpace = numberOptions.Find(item => item.Name == "Cooldown Space Stone").Value;
                GlobalVariable.buttonSpace.MaxTimer = CooldownSpace;
            }));

            numberOptions.Add(new CustomNumberOptions("Max Portal", maxPortal, 20f, 1f, 1f, false, ButtonMaxPortal, () => {
                maxPortal = (int) numberOptions.Find(item => item.Name == "Max Portal").Value;
            }));

            numberOptions.Add(new CustomNumberOptions("Cooldown Mind Stone", CooldownMind, 300f, 10f, 5f, ButtonCooldownMind, () => {
                CooldownMind = numberOptions.Find(item => item.Name == "Cooldown Mind Stone").Value;
                GlobalVariable.buttonMind.MaxTimer = CooldownMind;
            }));

            numberOptions.Add(new CustomNumberOptions("Mind Duration", MindDuration, 60f, 5f, 5f, ButtonMindDuration, () => {
                MindDuration = numberOptions.Find(item => item.Name == "Mind Duration").Value;
                GlobalVariable.buttonMind.EffectDuration = MindDuration;
            }));

            numberOptions.Add(new CustomNumberOptions("Cooldown Soul Stone", CooldownSoul, 300f, 10f, 15f, ButtonCooldownSoul, () => {
                CooldownSoul = numberOptions.Find(item => item.Name == "Cooldown Soul Stone").Value;
                GlobalVariable.buttonSoul.MaxTimer = CooldownSoul;
            }));

            numberOptions.Add(new CustomNumberOptions("Cooldown Power Stone", CooldownPower, 300f, 10f, 5f, ButtonCooldownSoul, () => {
                CooldownPower = numberOptions.Find(item => item.Name == "Cooldown Power Stone").Value;
                GlobalVariable.buttonPower.MaxTimer = CooldownPower;
            }));

            numberOptions.Add(new CustomNumberOptions("Stone Size", stoneSize, 1000f, 50f, 10f, false, ButtonStoneSize, () => {
                stoneSize = numberOptions.Find(item => item.Name == "Stone Size").Value;
            }));

            return numberOptions;
        }

        public static List<CustomStringOptions> InitStringOptions() {
            stringOptions.Add(new CustomStringOptions("Time Stone Visibility", visibilityValue[0], visibilityValue, ButtonVisibilityTime, () => {
                var thisButton = stringOptions.Find(item => item.Name == "Time Stone Visibility").Value;

                if (thisButton == "Everyone")
                    VisibilityTime = Visibility.Everyone;
                else if (thisButton == "Thanos")
                    VisibilityTime = Visibility.OnlyImpostor;
                else if (thisButton == "Crewmate")
                    VisibilityTime = Visibility.OnlyCrewmate;
            }));

            stringOptions.Add(new CustomStringOptions("Reality Stone Visibility", visibilityValue[0], visibilityValue, ButtonVisibilityTime, () => {
                var thisButton = stringOptions.Find(item => item.Name == "Reality Stone Visibility").Value;

                if (thisButton == "Everyone")
                    VisibilityReality = Visibility.Everyone;
                else if (thisButton == "Thanos")
                    VisibilityReality = Visibility.OnlyImpostor;
                else if (thisButton == "Crewmate")
                    VisibilityReality = Visibility.OnlyCrewmate;
            }));

            stringOptions.Add(new CustomStringOptions("Power Stone Visibility", visibilityValue[0], visibilityValue, ButtonVisibilityTime, () => {
                var thisButton = stringOptions.Find(item => item.Name == "Power Stone Visibility").Value;

                if (thisButton == "Everyone")
                    VisibilityPower = Visibility.Everyone;
                else if (thisButton == "Thanos")
                    VisibilityPower = Visibility.OnlyImpostor;
                else if (thisButton == "Crewmate")
                    VisibilityPower = Visibility.OnlyCrewmate;
            }));

            stringOptions.Add(new CustomStringOptions("Soul Stone Visibility", visibilityValue[0], visibilityValue, ButtonVisibilityTime, () => {
                var thisButton = stringOptions.Find(item => item.Name == "Soul Stone Visibility").Value;

                if (thisButton == "Everyone")
                    VisibilitySoul = Visibility.Everyone;
                else if (thisButton == "Thanos")
                    VisibilitySoul = Visibility.OnlyImpostor;
                else if (thisButton == "Crewmate")
                    VisibilitySoul = Visibility.OnlyCrewmate;
            }));

            stringOptions.Add(new CustomStringOptions("Mind Stone Visibility", visibilityValue[0], visibilityValue, ButtonVisibilityTime, () => {
                var thisButton = stringOptions.Find(item => item.Name == "Mind Stone Visibility").Value;

                if (thisButton == "Everyone")
                    VisibilityMind = Visibility.Everyone;
                else if (thisButton == "Thanos")
                    VisibilityMind = Visibility.OnlyImpostor;
                else if (thisButton == "Crewmate")
                    VisibilityMind = Visibility.OnlyCrewmate;
            }));

            stringOptions.Add(new CustomStringOptions("Space Stone Visibility", visibilityValue[0], visibilityValue, ButtonVisibilityTime, () => {
                var thisButton = stringOptions.Find(item => item.Name == "Space Stone Visibility").Value;

                if (thisButton == "Everyone")
                    VisibilitySpace = Visibility.Everyone;
                else if (thisButton == "Thanos")
                    VisibilitySpace = Visibility.OnlyImpostor;
                else if (thisButton == "Crewmate")
                    VisibilitySpace = Visibility.OnlyCrewmate;
            }));

            return stringOptions;
        }
    }
}
