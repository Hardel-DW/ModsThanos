using System;
using System.Collections.Generic;
using System.Linq;
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

        // Method Global.
        public static bool IsThanos(PlayerControl player) {
            bool isThanos = false;

            foreach (var thanos in GlobalVariable.allThanos) {
                if (player.PlayerId == thanos.PlayerId)
                    isThanos = true;
            }

            return isThanos;
        }

        public static bool IsThanosByID(byte playerId) {
            bool isThanos = false;

            foreach (var thanos in GlobalVariable.allThanos) {
                if (playerId == thanos.PlayerId)
                    isThanos = true;
            }

            return isThanos;
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

        // Patch PlayerControl
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
        public static void Postfix([HarmonyArgument(0)] Il2CppReferenceArray<GameDataPlayerInfo> infected) {
            List<PlayerControl> impostors = GetImpostor(infected);

            if (impostors != null && impostors.Count > 0) {
                GlobalVariable.allThanos.Clear();
                MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.setThanos, SendOption.None, -1);
                
                messageWriter.Write(impostors.Count);
                foreach (var player in impostors) {
                    GlobalVariable.allThanos.Add(player);
                    messageWriter.Write(player.PlayerId);
                }
                AmongUsClient.Instance.FinishRpcImmediately(messageWriter);

                foreach (var item in GlobalVariable.allThanos) {
                    ModThanos.Logger.LogInfo($"Send Thanos Name: {item.nameText.Text}, {item.PlayerId}");
                }
            }
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
        public static void Postfix(PlayerControl __instance) {
            if (GlobalVariable.useSnap)
                Stone.System.Snap.Incremente();

            if (GlobalVariable.hasSoulStone && Player.LocalPlayer.PlayerData.IsDead && !IsThanosByID(PlayerControl.LocalPlayer.PlayerId)) {
                Stone.StoneDrop.tryReplaceStone("Soul");
            }

            if (IsThanos(PlayerControl.LocalPlayer) && Player.LocalPlayer.PlayerData.IsDead) {
                if (GlobalVariable.hasSoulStone || GlobalVariable.hasMindStone || GlobalVariable.hasPowerStone || GlobalVariable.hasSpaceStone || GlobalVariable.hasTimeStone || GlobalVariable.hasRealityStone) {
                    foreach (var stone in GlobalVariable.stonesNames)
                        Stone.StoneDrop.tryReplaceStone(stone);                 
                }
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
