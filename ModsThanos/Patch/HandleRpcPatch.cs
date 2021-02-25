using HarmonyLib;
using Hazel;
using ModsThanos.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class HandleRpcPatch {

        public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader) {
            if (callId == (byte) CustomRPC.SyncStone) {
                string stoneName = reader.ReadString();
                Vector2 vector = reader.ReadVector2();

                if (!GlobalVariable.stoneObjects.ContainsKey(stoneName))
                    GlobalVariable.stonePositon.Add(stoneName, vector);
                else
                    GlobalVariable.stoneObjects[stoneName].ModifyPosition(vector);

                return false;
            }

            if (callId == (byte) CustomRPC.ReplaceStone) {
                string stoneName = reader.ReadString();
                Vector2 vector = reader.ReadVector2();

                Stone.StoneDrop.ReplaceStone(stoneName, vector);
                return false;
            }

            if (callId == (byte) CustomRPC.SetVisiorColor) {
                byte playerId = reader.ReadByte();
                float colorR = reader.ReadSingle();
                float colorG = reader.ReadSingle();
                float colorB = reader.ReadSingle();
                float colorA = reader.ReadSingle();

                foreach (var player in PlayerControl.AllPlayerControls) {
                    if (player.PlayerId == playerId) {
                        player.myRend.material.SetColor("_VisorColor", new Color(colorR, colorG, colorB, colorA));
                    }
                }

                return false;
            }

            if (callId == (byte) CustomRPC.SetColorName) {
                byte playerId = reader.ReadByte();
                float colorR = reader.ReadSingle();
                float colorG = reader.ReadSingle();
                float colorB = reader.ReadSingle();
                float colorA = reader.ReadSingle();

                foreach (var player in PlayerControl.AllPlayerControls)
                    if (player.PlayerId == playerId)
                        player.nameText.Color = new Color(colorR, colorG, colorB, colorA);

                return false;
            }

            if (callId == (byte) CustomRPC.SetPlayerSoulStone) {
                byte playerId = reader.ReadByte();
                GlobalVariable.PlayerSoulStone = PlayerControlUtils.FromPlayerId(playerId);
                return false;
            }

            if (callId == (byte) CustomRPC.RemovePlayerSoulStone) {
                GlobalVariable.PlayerSoulStone = null;
                return false;
            }

            if (callId == (byte) CustomRPC.SetThanos) {
                GlobalVariable.allThanos.Clear();
                List<byte> selectedPlayers = reader.ReadBytesAndSize().ToList();

                for (int i = 0; i < selectedPlayers.Count; i++)
                    GlobalVariable.allThanos.Add(PlayerControlUtils.FromPlayerId(selectedPlayers[i]));

                return false;
            }

            if (callId == (byte) CustomRPC.StonePickup) {
                string nameStone = reader.ReadString();

                if (GlobalVariable.stoneObjects.ContainsKey(nameStone))
                    GlobalVariable.stoneObjects[nameStone].DestroyThisObject();

                if (nameStone == "Soul")
                    Object.DestroyImmediate(GlobalVariable.arrow);

                return false;
            }

            if (callId == (byte) CustomRPC.MindChangedValue) {
                GlobalVariable.mindStoneUsed = reader.ReadBoolean();
                return false;
            }

            if (callId == (byte) CustomRPC.TimeRewind) {
                Stone.System.Time.isRewinding = true;
                GlobalVariable.UsableButton = false;
                PlayerControl.LocalPlayer.moveable = false;
                HudManager.Instance.FullScreen.color = new Color(0f, 0.639f, 0.211f, 0.3f);
                HudManager.Instance.FullScreen.enabled = true;
                return false;
            }

            if (callId == (byte) CustomRPC.TimeRevive) {
                PlayerControl player = PlayerControlUtils.FromPlayerId(reader.ReadByte());
                player.Revive();
                var body = Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == player.PlayerId);

                if (body != null) Object.Destroy(body.gameObject);
                return false;
            }

            if (callId == (byte) CustomRPC.PowerStone) {
                PlayerControl murder = PlayerControlUtils.FromPlayerId(reader.ReadByte());
                HelperSprite.ShowAnimation(1, 24, true, "ModsThanos.Resources.anim-power.png", 10, 1, murder.gameObject.transform.position, 5);
                Vector2 murderPosition = reader.ReadVector2();

                PlayerControlUtils.KillPlayerArea(murderPosition, murder, 3f);
                return false;
            }

            if (callId == (byte) CustomRPC.TurnInvisibility) {
                bool isInvis = reader.ReadBoolean();
                byte playerId = reader.ReadByte();

                PlayerControl player = PlayerControlUtils.FromPlayerId(playerId);
                HelperSprite.ShowAnimation(1, 8, true, "ModsThanos.Resources.anim-reality.png", 48, 1, player.gameObject.transform.position, 1);

                GlobalVariable.realityStoneUsed = isInvis;
                Stone.System.RpcFunctions.TurnInvis(isInvis, __instance);
                return false;
            }

            if (callId == (byte) CustomRPC.Snap) {
                GlobalVariable.useSnap = true;
                Camera.main.GetComponent<KGIKNCBGPFJ>().shakeAmount = 0.2f;
                Camera.main.GetComponent<KGIKNCBGPFJ>().shakePeriod = 1200f;
                HudManager.Instance.FullScreen.enabled = true;
                HudManager.Instance.FullScreen.color = new Color(1f, 1f, 1f, 0f);

                return false;
            }

            if (callId == (byte) CustomRPC.SnapEnded) {
                GlobalVariable.useSnap = false;
                Camera.main.GetComponent<KGIKNCBGPFJ>().shakeAmount = 0f;
                Camera.main.GetComponent<KGIKNCBGPFJ>().shakePeriod = 0f;
                HudManager.Instance.FullScreen.enabled = false;
                HudManager.Instance.FullScreen.color = new Color(1f, 1f, 1f, 0f);

                PlayerControl player = PlayerControlUtils.FromPlayerId(reader.ReadByte());
                PlayerControlUtils.KillEveryone(player);

                return false;
            }

            return true;
        }
    }
}
