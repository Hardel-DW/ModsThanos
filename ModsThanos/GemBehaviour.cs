using System;
using UnityEngine;
using ModsThanos.Utility;
using Hazel;
using Reactor;

namespace ModsThanos {

    [RegisterInIl2Cpp]
    public class GemBehaviour : MonoBehaviour {

        public GemBehaviour(IntPtr ptr) : base(ptr) { }

        void OnTriggerEnter2D(Collider2D collider) {
            PlayerControl player = collider.GetComponent<PlayerControl>();

            if (player != null && !player.Data.IsDead && player.Data.PlayerId == PlayerControl.LocalPlayer.PlayerId) {
                if (name == "Soul") {
                    GlobalVariable.hasSoulStone = true;
                    GlobalVariable.PlayerSoulStone = PlayerControlUtils.FromPlayerId(PlayerControl.LocalPlayer.PlayerId);

                    MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SetPlayerSoulStone, SendOption.None, -1);
                    write.Write(player.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(write);

                    Destroy(GlobalVariable.arrow);
                    StonePickup();
                }
            }

            if (player != null && !player.Data.IsDead && player.Data.PlayerId == PlayerControl.LocalPlayer.PlayerId && name != "Soul") {
                if (RoleHelper.IsThanos(PlayerControl.LocalPlayer.PlayerId)) {
                    switch (name) {
                        case "Mind":
                        GlobalVariable.hasMindStone = true;
                        break;

                        case "Power":
                        GlobalVariable.hasPowerStone = true;
                        break;

                        case "Reality":
                        GlobalVariable.hasRealityStone = true;
                        break;

                        case "Space":
                        GlobalVariable.hasSpaceStone = true;
                        break;

                        case "Time":
                        GlobalVariable.hasTimeStone = true;
                        break;

                        default:
                        ModThanos.Logger.LogInfo("Pierre inconnu rammasser");
                        break;
                    }

                    StonePickup();
                }
            }
        }

        void StonePickup() {
            HelperSprite.ShowAnimation(1, 28, true, "ModsThanos.Resources.anim-pickup.png", 128, 1, this.gameObject.transform.position);

            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.StonePickup, SendOption.None, -1);
            write.Write(name);
            AmongUsClient.Instance.FinishRpcImmediately(write);

            GlobalVariable.stoneObjects[name].DestroyThisObject();
        }

        void Update() {
            if (Vector2.Distance(gameObject.transform.position, new Vector2(-10.418f, 113.000f)) < 1f)
                gameObject.transform.position = new Vector3(32.596f, -15.570f, -0.5f);
        }
    }
}