using HarmonyLib;
using Hazel;
using ModsThanos.Utility;
using UnityEngine;

namespace ModsThanos.Stone.System {
    class Snap {
        public static void OnSnapPressed() {
            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.Snap, SendOption.None, -1);
            write.Write(PlayerControl.LocalPlayer.PlayerId);
            AmongUsClient.Instance.FinishRpcImmediately(write);

            GlobalVariable.useSnap = true;
            Camera.main.GetComponent<KGIKNCBGPFJ>().shakeAmount = 0.3f;
            Camera.main.GetComponent<KGIKNCBGPFJ>().shakePeriod = 600f;
            HudManager.Instance.FullScreen.enabled = true;
            HudManager.Instance.FullScreen.color = new Color(1f, 1f, 1f, 0f);
        }

        public static void Incremente() {
            Camera.main.GetComponent<KGIKNCBGPFJ>().shakeAmount = 0.3f;
            Camera.main.GetComponent<KGIKNCBGPFJ>().shakePeriod = 600f;

            Color currentColor = HudManager.Instance.FullScreen.color;
            HudManager.Instance.FullScreen.enabled = true;
            HudManager.Instance.FullScreen.color = new Color(1f, 1f, 1f, currentColor.a + 0.002f);
        }

        public static void OnSnapEnded() {
            GlobalVariable.useSnap = false;
            Camera.main.GetComponent<KGIKNCBGPFJ>().shakeAmount = 0f;
            Camera.main.GetComponent<KGIKNCBGPFJ>().shakePeriod = 0f;

            HudManager.Instance.FullScreen.color = new Color(1f, 1f, 1f, 0f);
            HudManager.Instance.FullScreen.enabled = false;

            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SnapEnded, SendOption.None, -1);
            write.Write(PlayerControl.LocalPlayer.PlayerId);
            AmongUsClient.Instance.FinishRpcImmediately(write);

            PlayerControlUtils.KillEveryone(PlayerControl.LocalPlayer);
        }
    }
}
