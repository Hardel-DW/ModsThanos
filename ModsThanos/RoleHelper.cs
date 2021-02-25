namespace ModsThanos {

    public static class RoleHelper {
        public static bool IsThanos(byte playerId) {
            bool isThanos = false;

            for (int i = 0; i < GlobalVariable.allThanos.Count; i++) {
                if (playerId == GlobalVariable.allThanos[i].PlayerId)
                    isThanos = true;
            }

            return isThanos;
        }
    }
}
