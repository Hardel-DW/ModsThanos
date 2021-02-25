using HarmonyLib;
using Hazel;
using ModsThanos.Utility;
using UnityEngine;

namespace ModsThanos.Stone.System {
    class Power {
        public static void OnPowerPressed() {
            Vector2 localPositon = PlayerControlUtils.Position(PlayerControl.LocalPlayer);
            HelperSprite.ShowAnimation(1, 24, true, "ModsThanos.Resources.anim-power.png", 10, 1, PlayerControl.LocalPlayer.gameObject.transform.position, 5);

            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PowerStone, SendOption.None, -1);
            write.Write(PlayerControl.LocalPlayer.PlayerId);
            write.WriteVector2(localPositon);
            AmongUsClient.Instance.FinishRpcImmediately(write);
            PlayerControlUtils.KillPlayerArea(localPositon, PlayerControl.LocalPlayer, 3f);
        }
    }
}
