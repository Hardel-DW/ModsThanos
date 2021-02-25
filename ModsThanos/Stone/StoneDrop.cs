using Hazel;
using ModsThanos.Map;
using ModsThanos.Utility;
using UnityEngine;

namespace ModsThanos.Stone {
    public static class StoneDrop {

        public static void TryReplaceStone(string stone) {
            if (stone == "Mind" && GlobalVariable.hasMindStone) {
                DestroyStone(stone);
                Vector2 placement = StonePlacement.GetRandomLocation(stone);
                Map.Mind.Place(placement);
                GlobalVariable.hasMindStone = false;

            } else if (stone == "Soul" && GlobalVariable.hasSoulStone) {
                DestroyStone(stone);
                Vector2 placement = StonePlacement.GetRandomLocation(stone);
                Map.Soul.Place(placement);
                GlobalVariable.hasSoulStone = false;

                GlobalVariable.PlayerSoulStone = null;
                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.RemovePlayerSoulStone, SendOption.None, -1);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                PlayerControlUtils.RpcSetColorName(new Color(1f, 1f, 1f, 1f), PlayerControl.LocalPlayer.PlayerId);

            } else if (stone == "Power" && GlobalVariable.hasPowerStone) {
                DestroyStone(stone);
                Vector2 placement = StonePlacement.GetRandomLocation(stone);
                Map.Power.Place(placement);
                GlobalVariable.hasPowerStone = false;

            } else if (stone == "Time" && GlobalVariable.hasTimeStone) {
                DestroyStone(stone);
                Vector2 placement = StonePlacement.GetRandomLocation(stone);
                Map.Time.Place(placement);
                GlobalVariable.hasTimeStone = false;

            } else if (stone == "Space" && GlobalVariable.hasSpaceStone) {
                DestroyStone(stone);
                Vector2 placement = StonePlacement.GetRandomLocation(stone);
                Map.Space.Place(placement);
                GlobalVariable.hasSpaceStone = false;

            } else if (stone == "Reality" && GlobalVariable.hasRealityStone) {
                DestroyStone(stone);
                Vector2 placement = StonePlacement.GetRandomLocation(stone);
                Map.Reality.Place(placement);
                GlobalVariable.hasRealityStone = false;
            } else return;

            ModThanos.Logger.LogInfo($"Send {stone}, Position : {GlobalVariable.stonePositon[stone].x}, {GlobalVariable.stonePositon[stone].y}");
            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.ReplaceStone, SendOption.None, -1);
            write.Write(stone);
            write.WriteVector2(GlobalVariable.stonePositon[stone]);
            AmongUsClient.Instance.FinishRpcImmediately(write);
        }

        public static void ReplaceStone(string stone, Vector2 position) {
            DestroyStone(stone);

            if (stone == "Mind") {
                Map.Mind.Place(position);
                GlobalVariable.hasMindStone = false;
            } else if (stone == "Soul") {
                Map.Soul.Place(position);
                GlobalVariable.hasSoulStone = false;
            } else if (stone == "Power") {
                Map.Power.Place(position);
                GlobalVariable.hasPowerStone = false;
            } else if (stone == "Time") {
                Map.Time.Place(position);
                GlobalVariable.hasTimeStone = false;
            } else if (stone == "Space") {
                Map.Space.Place(position);
                GlobalVariable.hasSpaceStone = false;
            } else if (stone == "Reality") {
                Map.Reality.Place(position);
                GlobalVariable.hasRealityStone = false;
            } else return;
        }

        public static void DestroyStone(string stone) {
            if (GlobalVariable.stoneObjects.ContainsKey(stone))
                GlobalVariable.stoneObjects[stone].DestroyThisObject();
        }
    }
}
