using Hazel;
using ModsThanos.Utility;
using ModsThanos.Utility.Enumerations;
using System;
using System.Collections.Generic;

namespace ModsThanos.Stone.System {
    public static class Mind {

        public static void OnMindPressed() {
            HelperSprite.ShowAnimation(1, 15, true, "ModsThanos.Resources.anim-mind.png", 48, 1, Player.LocalPlayer.GameObject.transform.position, 1);

            GlobalVariable.PlayerHat = Player.LocalPlayer.PlayerData.HatId;
            GlobalVariable.PlayerPet = Player.LocalPlayer.PlayerData.PetId;
            GlobalVariable.PlayerSkin = Player.LocalPlayer.PlayerData.SkinId;
            GlobalVariable.PlayerName = Player.LocalPlayer.PlayerData.PlayerName;
            GlobalVariable.PlayerColor = Player.LocalPlayer.PlayerData.ColorId;
            GlobalVariable.PlayerColorName = PlayerControl.LocalPlayer.nameText.Color;

            List<byte> players = new List<byte>();

            foreach (var element in PlayerControl.AllPlayerControls)
                if (element.PlayerId != PlayerControl.LocalPlayer.PlayerId && !element.Data.IsDead) 
                    players.Add(element.PlayerId);

            Random random = new Random();
            byte RandomPlayer = players[random.Next(0, players.Count)];

            Player player = Player.FromPlayerId(PlayerControl.LocalPlayer.PlayerId);
            Player target = Player.FromPlayerId(RandomPlayer);

            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.MindChangedValue, SendOption.None, -1);
            write.Write(true);
            AmongUsClient.Instance.FinishRpcImmediately(write);

            player.RpcSetHat((HatType) target.PlayerData.HatId);
            player.RpcSetSkin((SkinType) target.PlayerData.SkinId);
            player.RpcSetPet((PetType) target.PlayerData.PetId);
            player.RpcSetName(target.PlayerData.PlayerName);
            player.RpcSetColor((ColorType) target.PlayerData.ColorId);
            player.RpcSetColorName(Player.FromPlayerIdFFGALNPKCD(target.PlayerId).nameText.Color, player.PlayerId);
        }

        public static void OnMindEnded() {
            HelperSprite.ShowAnimation(1, 15, true, "ModsThanos.Resources.anim-mind.png", 48, 1, Player.LocalPlayer.GameObject.transform.position, 1);
            Player player = Player.FromPlayerId(PlayerControl.LocalPlayer.PlayerId);
            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.MindChangedValue, SendOption.None, -1);
            write.Write(false);
            AmongUsClient.Instance.FinishRpcImmediately(write);

            player.RpcSetHat((HatType) GlobalVariable.PlayerHat);
            player.RpcSetSkin((SkinType) GlobalVariable.PlayerSkin);
            player.RpcSetPet((PetType) GlobalVariable.PlayerPet);
            player.RpcSetName(GlobalVariable.PlayerName);
            player.RpcSetColor((ColorType) GlobalVariable.PlayerColor);
            player.RpcSetColorName(new UnityEngine.Color(1f, 1f, 1f, 1f), PlayerControl.LocalPlayer.PlayerId);

        }
    }
}
