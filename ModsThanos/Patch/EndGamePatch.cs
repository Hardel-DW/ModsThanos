using HarmonyLib;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.ExitGame))]
    public static class EndGamePatch {

        public static void Prefix(AmongUsClient __instance) {
            EndGameCommons.ResetGlobalVariable();
        }
    }

    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
    public static class EndGameManagerPatch {
        public static bool Prefix(EndGameManager __instance) {
            EndGameCommons.ResetGlobalVariable();

            return true;
        }
    }

    public static class EndGameCommons {
        public static void ResetGlobalVariable() {
            //ModThanos.Logger.LogInfo("Test End");

            GlobalVariable.GameStarted = false;
            GlobalVariable.Thanos = null;
            GlobalVariable.hasMindStone = false;
            GlobalVariable.hasPowerStone = false;
            GlobalVariable.hasRealityStone = false;
            GlobalVariable.hasSoulStone = false;
            GlobalVariable.hasSpaceStone = false;
            GlobalVariable.hasTimeStone = false;
            GlobalVariable.useSnap = false;
            GlobalVariable.soulStoneUsed = false;
            GlobalVariable.UsableButton = false;

            GlobalVariable.stoneObjects.Clear();
            GlobalVariable.stonePositon.Clear();

            //ModThanos.Logger.LogInfo("Test End Fin");
        }

    }
}