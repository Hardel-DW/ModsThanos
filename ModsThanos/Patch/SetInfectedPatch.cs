using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Hazel;
using ModsThanos.Utility.Enumerations;
using UnhollowerBaseLib;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    class SetInfectedPatch {
        public static void Postfix([HarmonyArgument(0)] Il2CppReferenceArray<GameData.PlayerInfo> infected) {
            List<PlayerControl> playersSelections = PlayerControl.AllPlayerControls.ToArray().ToList();
            Visibility visibility = CustomGameOptions.SideStringToEnum(CustomGameOptions.ThanosSide.GetText());
            GlobalVariable.allThanos.Clear();

            if (Visibility.OnlyImpostor == visibility)
                playersSelections.RemoveAll(x => !x.Data.IsImpostor);

            if (Visibility.OnlyCrewmate == visibility)
                playersSelections.RemoveAll(x => x.Data.IsImpostor);

            // playersSelections
            if (playersSelections != null && playersSelections.Count > 0 && CustomGameOptions.EnableThanosMods.GetValue()) {
                MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SetThanos, SendOption.None, -1);
                List<byte> playerSelected = new List<byte>();

                for (int i = 0; i < CustomGameOptions.NumberThanos.GetValue(); i++) {
                    Random random = new Random();
                    PlayerControl selectedPlayer = playersSelections[random.Next(0, playersSelections.Count)];
                    GlobalVariable.allThanos.Add(selectedPlayer);
                    playersSelections.Remove(selectedPlayer);
                    playerSelected.Add(selectedPlayer.PlayerId);
                }

                messageWriter.WriteBytesAndSize(playerSelected.ToArray());
                AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
            }
        }
    }
}
