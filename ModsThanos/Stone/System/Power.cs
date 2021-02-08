using HarmonyLib;
using Hazel;
using ModsThanos.Utility;
using UnityEngine;

namespace ModsThanos.Stone.System {
    class Power {
        public static void OnPowerPressed() {
            Vector2 localPositon = Player.LocalPlayer.Position;
            HelperSprite.ShowAnimation(1, 30, true, "ModsThanos.Resources.anim-power.png", 48, 1, Player.LocalPlayer.GameObject.transform.position, 1);

            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PowerStone, SendOption.None, -1);
            write.Write(PlayerControl.LocalPlayer.PlayerId);
            write.WriteVector2(localPositon);
            AmongUsClient.Instance.FinishRpcImmediately(write);
            Player.KillPlayerArea(localPositon, Player.LocalPlayer, 3f);
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class PowerRPC {
        public static bool Prefix([HarmonyArgument(0)] byte HKHMBLJFLMC, [HarmonyArgument(1)] MessageReader ALMCIJKELCP) {
            if (HKHMBLJFLMC == (byte) CustomRPC.PowerStone) {
                Player murder = Player.FromPlayerId(ALMCIJKELCP.ReadByte());
                Vector2 murderPosition = ALMCIJKELCP.ReadVector2();

                Player.KillPlayerArea(murderPosition, murder, 3f);
                return false;
            }
            return true;
        }
    }
}
