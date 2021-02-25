using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Hazel;
using UnhollowerBaseLib;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    class SetInfectedPatch {
        public static void Postfix([HarmonyArgument(0)] Il2CppReferenceArray<GameData.PlayerInfo> infected) {
            List<PlayerControl> impostorsList = PlayerControl.AllPlayerControls.ToArray().ToList().FindAll(x => x.Data.IsImpostor).ToArray().ToList();
            GlobalVariable.allThanos.Clear();

            // Investigator
            if (impostorsList != null && impostorsList.Count > 0 && CustomGameOptions.EnableThanosMods.GetValue()) {
                MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SetThanos, SendOption.None, -1);
                List<byte> playerSelected = new List<byte>();

                for (int i = 0; i < impostorsList.Count; i++) {
                    Random random = new Random();
                    PlayerControl selectedPlayer = impostorsList[random.Next(0, impostorsList.Count)];
                    GlobalVariable.allThanos.Add(selectedPlayer);
                    impostorsList.Remove(selectedPlayer);
                    playerSelected.Add(selectedPlayer.PlayerId);
                }

                messageWriter.WriteBytesAndSize(playerSelected.ToArray());
                AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
            }
        }
    }
}
