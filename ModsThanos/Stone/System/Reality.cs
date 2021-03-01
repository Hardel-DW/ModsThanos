using HarmonyLib;
using Hazel;
using ModsThanos.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace ModsThanos.Stone.System {
    class Reality {
        public static List<byte> invisPlayers = new List<byte>();

        public static void OnRealityPressed(bool isInvis) {
            HelperSprite.ShowAnimation(1, 8, true, "ModsThanos.Resources.anim-reality.png", 48, 1, PlayerControl.LocalPlayer.gameObject.transform.position, 1);
            var writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.TurnInvisibility, SendOption.Reliable);
            writer.Write(isInvis);
            writer.Write(PlayerControl.LocalPlayer.PlayerId);
            writer.EndMessage();
            RpcFunctions.TurnInvis(isInvis, PlayerControl.LocalPlayer);
        }
    }

    public static class RpcFunctions {
        static public void TurnInvis(bool isInvis, PlayerControl player) {
            var playerRenderer = player.myRend;

            if (isInvis) {
                Reality.invisPlayers.Add(player.PlayerId);
                if (player == PlayerControl.LocalPlayer || RoleHelper.IsThanos(PlayerControl.LocalPlayer.PlayerId) || PlayerControl.LocalPlayer.Data.IsDead) {
                    playerRenderer.SetColorAlpha(0.2f);
                    player.HatRenderer.FrontLayer.SetColorAlpha(0.2f);
                    player.HatRenderer.BackLayer.SetColorAlpha(0.2f);
                    player.MyPhysics.Skin.layer.SetColorAlpha(0.2f);
                    if (player.CurrentPet != null) player.CurrentPet.rend.SetColorAlpha(0.2f);       
                    
                } else {
                    playerRenderer.SetColorAlpha(0f);
                    player.HatRenderer.FrontLayer.SetColorAlpha(0f);
                    player.HatRenderer.BackLayer.SetColorAlpha(0f);
                    player.MyPhysics.Skin.layer.SetColorAlpha(0f);
                    player.nameText.Color = new Color(player.nameText.Color.r, player.nameText.Color.g, player.nameText.Color.b, 0f);
                    if (player.CurrentPet != null) player.CurrentPet.rend.SetColorAlpha(0f);
                }
            } else {
                Reality.invisPlayers.Remove(player.PlayerId);
                player.nameText.Color = new Color(player.nameText.Color.r, player.nameText.Color.g, player.nameText.Color.b, 1f);
                playerRenderer.SetColorAlpha(1f);
                player.HatRenderer.FrontLayer.SetColorAlpha(1f);
                player.HatRenderer.BackLayer.SetColorAlpha(1f);
                player.MyPhysics.Skin.layer.SetColorAlpha(1f);
                if (player.CurrentPet != null) player.CurrentPet.rend.SetColorAlpha(1f);                
            }
        }

        static public void SetColorAlpha(this SpriteRenderer renderer, float alpha) {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
        }
    }

    [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.LateUpdate))]
    public static class PlayerPhysicsLateUpdatePatch {
        public static void Prefix(PlayerPhysics __instance) {
            if (Reality.invisPlayers.Contains(__instance.Field_5.PlayerId)) {
                if (RoleHelper.IsThanos(PlayerControl.LocalPlayer.PlayerId) || PlayerControl.LocalPlayer.Data.IsDead) {
                    __instance.Field_5.SetHatAlpha(0.2f);
                } else {
                    __instance.Field_5.SetHatAlpha(0f);
                }
            }
        }
    }

    [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Start))]
    public class GameStartManagerStartPatch {
        public static void Prefix() {
            Reality.invisPlayers.Clear();
        }
    }
}
