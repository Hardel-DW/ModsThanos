using HarmonyLib;
using Hazel;
using ModsThanos.Map;
using ModsThanos.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    public static class ShipStatusPatch {
        public static void Prefix(ShipStatus __instance) {
            Stone.System.Time.pointsInTime.Clear();
            Stone.System.Time.recordTime = CustomGameOptions.TimeDuration.GetValue();
            GlobalVariable.hasMindStone = false;
            GlobalVariable.hasPowerStone = false;
            GlobalVariable.hasRealityStone = false;
            GlobalVariable.hasSoulStone = false;
            GlobalVariable.hasSpaceStone = false;
            GlobalVariable.realityStoneUsed = false;
            GlobalVariable.hasTimeStone = false;
            GlobalVariable.useSnap = false;
            GlobalVariable.mindStoneUsed = false;
            GlobalVariable.GameStarted = true;
            GlobalVariable.UsableButton = true;
            GlobalVariable.PlayerName = null;
            
            GlobalVariable.allPlayersData.Clear();
            foreach (var player in PlayerControl.AllPlayerControls) {
                GlobalVariable.allPlayersData.Add(new Stone.System.Mind.PlayerData(
                    player.PlayerId,
                    player.Data.ColorId,
                    player.Data.HatId,
                    player.Data.PetId,
                    player.Data.SkinId,
                    player.nameText.Text
                ));
            }

            // Button Timer
            Stone.System.Time.recordTime = CustomGameOptions.TimeDuration.GetValue();
            GlobalVariable.buttonMind.MaxTimer = CustomGameOptions.CooldownMindStone.GetValue();
            GlobalVariable.buttonMind.EffectDuration = CustomGameOptions.MindDuration.GetValue();
            GlobalVariable.buttonReality.MaxTimer = CustomGameOptions.CooldownRealityStone.GetValue();
            GlobalVariable.buttonReality.EffectDuration = CustomGameOptions.RealityDuration.GetValue();
            GlobalVariable.buttonTime.MaxTimer = CustomGameOptions.CooldownTimeStone.GetValue();
            GlobalVariable.buttonTime.EffectDuration = CustomGameOptions.TimeDuration.GetValue() / 2;
            GlobalVariable.buttonPower.MaxTimer = CustomGameOptions.CooldownPowerStone.GetValue();
            GlobalVariable.buttonSpace.MaxTimer = CustomGameOptions.CooldownSpaceStone.GetValue();

            Dictionary<string, Vector2> stonePosition = StonePlacement.SetAllStonePositions();
            StonePlacement.PlaceAllStone();

            if (PlayerControlUtils.AmHost()) {
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
            GlobalVariable.stoneObjects.Clear();
            GlobalVariable.stonePositon.Clear();
            GlobalVariable.allThanos.Clear();
            GlobalVariable.hasMindStone = false;
            GlobalVariable.hasPowerStone = false;
            GlobalVariable.hasRealityStone = false;
            GlobalVariable.hasSoulStone = false;
            GlobalVariable.hasSpaceStone = false;
            GlobalVariable.hasTimeStone = false;
            GlobalVariable.realityStoneUsed = false;
            GlobalVariable.useSnap = false;
            GlobalVariable.mindStoneUsed = false;
            GlobalVariable.UsableButton = false;
        }
    }
}
