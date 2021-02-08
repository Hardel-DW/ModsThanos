using HarmonyLib;
using Hazel;
using ModsThanos.Map;
using ModsThanos.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    public static class ShipStatusPatch {
        public static void Prefix(ShipStatus __instance) {
            GlobalVariable.hasMindStone = false;
            GlobalVariable.hasPowerStone = false;
            GlobalVariable.hasRealityStone = false;
            GlobalVariable.hasSoulStone = false;
            GlobalVariable.hasSpaceStone = false;
            GlobalVariable.hasTimeStone = false;
            GlobalVariable.useSnap = false;
            GlobalVariable.GameStarted = true;
            GlobalVariable.UsableButton = true;

            Dictionary<string, Vector2> stonePosition = StonePlacement.SetAllStonePositions();
            StonePlacement.PlaceAllStone();

            foreach (string item in GlobalVariable.stonesNames) {
                ModThanos.Logger.LogInfo($"Name: {item}, Position: {GlobalVariable.stonePositon[item].x}, {GlobalVariable.stonePositon[item].y}");
            }

            if (Player.amHost()) {
                foreach (var stone in stonePosition) {
                    MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SyncStone, SendOption.None, -1);
                    write.Write(stone.Key);
                    write.WriteVector2(stone.Value);
                    AmongUsClient.Instance.FinishRpcImmediately(write);
                }
            }
        }
    }

    [HarmonyPatch(typeof(LobbyBehaviour), nameof(LobbyBehaviour.Start))]
    public static class LobbyBehaviourPatch {
        public static void Prefix() {
            ModThanos.Logger.LogInfo("Test");

            GlobalVariable.stoneObjects.Clear();
            GlobalVariable.stonePositon.Clear();
            GlobalVariable.hasMindStone = false;
            GlobalVariable.hasPowerStone = false;
            GlobalVariable.hasRealityStone = false;
            GlobalVariable.hasSoulStone = false;
            GlobalVariable.hasSpaceStone = false;
            GlobalVariable.hasTimeStone = false;
            GlobalVariable.useSnap = false;
            GlobalVariable.UsableButton = false;
            GlobalVariable.Thanos = null;

            ModThanos.Logger.LogInfo("Fin");
        }
    }
}
