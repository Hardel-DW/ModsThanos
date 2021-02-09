using System;
using System.Collections.Generic;
using HarmonyLib;
using Hazel;
using ModsThanos.Map;
using ModsThanos.Utility;
using UnhollowerBaseLib;
using UnityEngine;

namespace ModsThanos.Patch {
    using PlayerControl = FFGALNAPKCD;
    using GameDataPlayerInfo = EGLJNOMOGNP.DCJMABDDJCF;
    using AmongUsClient = FMLLKEACGIO;

    [HarmonyPatch]
    class PlayerControlPatch {

        public static bool IsThanos(PlayerControl player) {
            return player.PlayerId == GlobalVariable.Thanos.PlayerId;
        }

        public static List<PlayerControl> GetImpostor(Il2CppReferenceArray<GameDataPlayerInfo> infection) {
            List<PlayerControl> list = new List<PlayerControl>();
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                bool isImpostor = false;
                foreach (GameDataPlayerInfo player1 in infection) {
                    if (player.PlayerId == player1.LAOEJKHLKAI.PlayerId) {
                        isImpostor = true;
                        break;
                    }
                }
                if (isImpostor)
                    list.Add(player);
            }
            return list;
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
        public static void Postfix([HarmonyArgument(0)] Il2CppReferenceArray<GameDataPlayerInfo> infected) {
            List<PlayerControl> impostors = GetImpostor(infected);

            if (impostors != null && impostors.Count > 0) {
                System.Random random = new System.Random();
                GlobalVariable.Thanos = impostors[random.Next(0, impostors.Count)];
                byte playerId = GlobalVariable.Thanos.PlayerId;
                MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.setThanos, SendOption.None, -1);
                messageWriter.Write(playerId);
                AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
            }
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
        public static void Postfix(PlayerControl __instance) {
            if (GlobalVariable.useSnap)
                Stone.System.Snap.Incremente();

            if (GlobalVariable.hasSoulStone && Player.LocalPlayer.PlayerData.IsDead) {

                if (GlobalVariable.stoneObjects.ContainsKey("Soul"))
                    GlobalVariable.stoneObjects["Soul"].DestroyThisObject();

                Vector2 placement = StonePlacement.GetRandomLocation("Soul");
                Stone.Map.Soul.Place(placement);
                GlobalVariable.hasSoulStone = false;

                GlobalVariable.PlayerSoulStone = null;
                MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.RemovePlayerSoulStone, SendOption.None, -1);          
                AmongUsClient.Instance.FinishRpcImmediately(write);
                Player.LocalPlayer.RpcSetColorName(new Color(1f, 1f, 1f, 1f), Player.LocalPlayer.PlayerId);

                write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.ReplaceStone, SendOption.None, -1);
                write.Write("Soul");
                write.WriteVector2(GlobalVariable.stonePositon["Soul"]);
                AmongUsClient.Instance.FinishRpcImmediately(write);
            }
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSyncSettings))]
        public static void Postfix(KMOGFLPJLLK IOFBPLNIJIC) {
            if (FFGALNAPKCD.AllPlayerControls.Count > 1) {
                MessageWriter writer = FMLLKEACGIO.Instance.StartRpcImmediately(FFGALNAPKCD.LocalPlayer.NetId, (byte) CustomRPC.SyncCustomSettings, Hazel.SendOption.None, -1);

                foreach (var item in CustomGameOptions.stringOptions)
                    item.ValueChanged();

                foreach (var item in CustomGameOptions.numberOptions)
                    writer.Write(item.Value);

                foreach (var item in CustomGameOptions.stringOptions)
                    writer.Write(item.Value);

                FMLLKEACGIO.Instance.FinishRpcImmediately(writer);
            }
        }
    }
}
