using HarmonyLib;
using Hazel;
using ModsThanos.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace ModsThanos.Stone.System {
    class Reality {
        public static List<byte> invisPlayers = new List<byte>();

        public static void OnRealityPressed(bool isInvis) {
            HelperSprite.ShowAnimation(1, 8, true, "ModsThanos.Resources.anim-reality.png", 48, 1, Player.LocalPlayer.GameObject.transform.position, 1);
            var writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.TurnInvisibility, SendOption.Reliable);
            writer.Write(isInvis);
            writer.Write(PlayerControl.LocalPlayer.PlayerId);
            writer.EndMessage();
            RpcFunctions.TurnInvis(isInvis, PlayerControl.LocalPlayer);
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class RealityRPC {
        public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] byte HKHMBLJFLMC, [HarmonyArgument(1)] MessageReader ALMCIJKELCP) {
            if (HKHMBLJFLMC == (byte) CustomRPC.TurnInvisibility) {
                bool isInvis = ALMCIJKELCP.ReadBoolean();
                byte playerId = ALMCIJKELCP.ReadByte();

                Player player = Player.FromPlayerId(playerId);
                HelperSprite.ShowAnimation(1, 8, true, "ModsThanos.Resources.anim-reality.png", 48, 1, player.GameObject.transform.position, 1);

                RpcFunctions.TurnInvis(isInvis, __instance);
                return false;
            }
            return true;
        }
    }

    public static class RpcFunctions {
        static public void TurnInvis(bool isInvis, PlayerControl player) {
            var playerRenderer = player.myRend;
            if (isInvis) {
                Reality.invisPlayers.Add(player.PlayerId);
                if (player == PlayerControl.LocalPlayer || PlayerControl.LocalPlayer.Data.IsImpostor || PlayerControl.LocalPlayer.Data.IsDead) {
                    playerRenderer.SetColorAlpha(0.2f);
                    player.HatRenderer.FrontLayer.SetColorAlpha(0.2f);
                    player.HatRenderer.BackLayer.SetColorAlpha(0.2f);
                    player.MyPhysics.Skin.layer.SetColorAlpha(0.2f);
                    if (player.CurrentPet != null) {
                        player.CurrentPet.rend.SetColorAlpha(0.2f);
                    }
                } else {
                    playerRenderer.SetColorAlpha(0f);
                    player.HatRenderer.FrontLayer.SetColorAlpha(0f);
                    player.HatRenderer.BackLayer.SetColorAlpha(0f);
                    player.MyPhysics.Skin.layer.SetColorAlpha(0f);
                    player.nameText.Color = new Color(player.nameText.Color.r, player.nameText.Color.g, player.nameText.Color.b, 0f);
                    if (player.CurrentPet != null) {
                        player.CurrentPet.rend.SetColorAlpha(0f);
                    }
                }
            } else {
                Reality.invisPlayers.Remove(player.PlayerId);

                ModThanos.Logger.LogInfo("Test 1");
                playerRenderer.SetColorAlpha(1f);

                ModThanos.Logger.LogInfo("Test 2");
                player.HatRenderer.FrontLayer.SetColorAlpha(1f);

                ModThanos.Logger.LogInfo("Test 3");
                player.HatRenderer.BackLayer.SetColorAlpha(1f);

                ModThanos.Logger.LogInfo("Test 4");
                player.MyPhysics.Skin.layer.SetColorAlpha(1f);

                ModThanos.Logger.LogInfo("Test 5");
                player.nameText.Color = new Color(player.nameText.Color.r, player.nameText.Color.g, player.nameText.Color.b, 1f);
                if (player.CurrentPet != null) {

                    ModThanos.Logger.LogInfo("Test 6");
                    player.CurrentPet.rend.SetColorAlpha(1f);

                    ModThanos.Logger.LogInfo("Test 7");
                }
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
                if (PlayerControl.LocalPlayer.Data.IsImpostor || PlayerControl.LocalPlayer.Data.IsDead) {
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
