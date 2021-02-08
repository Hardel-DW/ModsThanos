using System;
using UnityEngine;
using ModsThanos.Utility;
using Hazel;
using HarmonyLib;

namespace ModsThanos {
    public class PickupGem : MonoBehaviour {

        public PickupGem(IntPtr ptr) : base(ptr) { }

        void OnTriggerEnter2D(Collider2D collider) {
            PlayerControl player = collider.GetComponent<PlayerControl>();

            if (player != null && !player.Data.IsDead && player.Data.PlayerId == Player.LocalPlayer.PlayerId) {
                if (name == "Soul") {
                    GlobalVariable.hasSoulStone = true;
                
                    GlobalVariable.PlayerSoulStone = Player.FromPlayerIdFFGALNPKCD(Player.LocalPlayer.PlayerId);

                    MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SetPlayerSoulStone, SendOption.None, -1);
                    write.Write(player.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(write);

                    Destroy(GlobalVariable.arrow);
                    StonePickup();
                }
            }

            if (player != null && !player.Data.IsDead && player.Data.PlayerId == Player.LocalPlayer.PlayerId && Player.LocalPlayer.PlayerData.IsImpostor && name != "Soul") {
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

        void StonePickup() {
            HelperSprite.ShowAnimation(1, 28, true, "ModsThanos.Resources.anim-pickup.png", 128, 1, this.gameObject.transform.position);

            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.StonePickup, SendOption.None, -1);
            write.Write(name);
            AmongUsClient.Instance.FinishRpcImmediately(write);

            foreach (var item in GlobalVariable.stoneObjects) {
                ModThanos.Logger.LogInfo($"Name: {item}");
            }

            GlobalVariable.stoneObjects[name].DestroyThisObject();
        }
    }
}