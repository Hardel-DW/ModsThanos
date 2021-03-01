namespace ModsThanos.Stone.System.Mind {
    public class PlayerData {
        public byte PlayerId;
        public byte PlayerColor;
        public uint PlayerHat;
        public uint PlayerPet;
        public uint PlayerSkin;
        public string PlayerName;

        public PlayerData(byte playerId, byte playerColor, uint playerHat, uint playerPet, uint playerSkin, string playerName) {
            PlayerId = playerId;
            PlayerColor = playerColor;
            PlayerHat = playerHat;
            PlayerPet = playerPet;
            PlayerSkin = playerSkin;
            PlayerName = playerName;
        }
    }
}
