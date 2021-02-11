using HarmonyLib;
using Hazel;
using ModsThanos.Utility;
using System.Linq;
using UnityEngine;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class HandleRpcPatch {

        public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader) {
            //ModThanos.Logger.LogInfo("Packet du Serveur No : " + callId);

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
                GlobalVariable.PlayerSoulStone = Player.FromPlayerIdFFGALNPKCD(playerId);
                return false;
            }

            if (callId == (byte) CustomRPC.RemovePlayerSoulStone) {
                GlobalVariable.PlayerSoulStone = null;
                return false;
            }

            if (callId == (byte) CustomRPC.setThanos) {
                GlobalVariable.allThanos.Clear();
                int readInt = reader.ReadInt32();

                byte readByte;
                for (int i = 0; i < readInt; i++) {
                    readByte = reader.ReadByte();
                    GlobalVariable.allThanos.Add(Player.FromPlayerIdFFGALNPKCD(readByte));
                }

                foreach (var item in GlobalVariable.allThanos) {
                    ModThanos.Logger.LogInfo($"Get Thanos Name: {item.nameText.Text}, {item.PlayerId}");
                }

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

            if (callId == (byte) CustomRPC.SyncCustomSettings) {
                foreach (var item in CustomGameOptions.numberOptions) {
                    item.Value  = System.BitConverter.ToSingle(reader.ReadBytes(4).ToArray(), 0);
                    item.ValueChanged();
                }

                foreach (var item in CustomGameOptions.stringOptions) {
                    string newValue = reader.ReadString();
                    item.ValueChanged(newValue);
                }

                return false;
            }

            return true;
        }
    }
}
