using HarmonyLib;
using Hazel;
using ModsThanos.Utility;
using UnityEngine;

namespace ModsThanos.Stone.System {
    class Snap {
        public static KGIKNCBGPFJ camera = Camera.main.GetComponent<KGIKNCBGPFJ>();

        public static void OnSnapPressed() {

            ModThanos.Logger.LogInfo("Snap Click Top");
            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.Snap, SendOption.None, -1);
            write.Write(PlayerControl.LocalPlayer.PlayerId);
            AmongUsClient.Instance.FinishRpcImmediately(write);

            ModThanos.Logger.LogInfo("Snap Click Bttom");

            GlobalVariable.useSnap = true;
            camera.shakeAmount = 0.2f;
            camera.shakePeriod = 1200f;
            HudManager.Instance.FullScreen.enabled = true;
            HudManager.Instance.FullScreen.color = new Color(1f, 1f, 1f, 0f);
        }

        public static void Incremente() {
            camera.shakeAmount += 0.001f;
            camera.shakePeriod -= 1;

            Color currentColor = HudManager.Instance.FullScreen.color;
            HudManager.Instance.FullScreen.enabled = true;
            HudManager.Instance.FullScreen.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + 0.002f);
        }

        public static void OnSnapEnded() {
            GlobalVariable.useSnap = false;
            camera.shakeAmount = 0f;
            camera.shakePeriod = 0f;

            HudManager.Instance.FullScreen.color = new Color(1f, 1f, 1f, 0f);
            HudManager.Instance.FullScreen.enabled = false;

            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SnapEnded, SendOption.None, -1);
            write.Write(PlayerControl.LocalPlayer.PlayerId);
            AmongUsClient.Instance.FinishRpcImmediately(write);

            Player.KillEveryone(Player.LocalPlayer);
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class SnapRPC {
        public static bool Prefix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader) {
            if (callId == (byte) CustomRPC.Snap) {
                GlobalVariable.useSnap = true;
                Snap.camera.shakeAmount = 0.2f;
                Snap.camera.shakePeriod = 1200f;
                HudManager.Instance.FullScreen.enabled = true;
                HudManager.Instance.FullScreen.color = new Color(1f, 1f, 1f, 0f);

                return false;
            }

            if (callId == (byte) CustomRPC.SnapEnded) {
                GlobalVariable.useSnap = false;
                Snap.camera.shakeAmount = 0f;
                Snap.camera.shakePeriod = 0f;
                HudManager.Instance.FullScreen.enabled = false;
                HudManager.Instance.FullScreen.color = new Color(1f, 1f, 1f, 0f);

                Player player = Player.FromPlayerId(reader.ReadByte());
                Player.KillEveryone(player);

                return false;
            }

            return true;
        }
    }
}
