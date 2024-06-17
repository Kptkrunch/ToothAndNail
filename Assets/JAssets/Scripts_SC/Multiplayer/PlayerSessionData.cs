using System.Collections.Generic;

namespace JAssets.Scripts_SC.Multiplayer
{
    public static class PlayerSessionData
    {
        public static int PlayersReady = 0;
        public static int TotalPlayers = 0;
        public static List<DataTypeController.PlayerSlotData> PlayerDataList = new();
        public static void AddToTotalPlayers()
        {
            TotalPlayers++;
            if (TotalPlayers > 4) TotalPlayers = 4;
            if (TotalPlayers < 0) TotalPlayers = 0;
        }

        public static void AddToReadyPlayers()
        {
            PlayersReady++;
            if (PlayersReady > 4) PlayersReady = 4;
            if (PlayersReady < 0) PlayersReady = 0;
        }

        public static void RemoveFromTotalPlayers()
        {
            TotalPlayers--;
            if (TotalPlayers > 4) TotalPlayers = 4;
            if (TotalPlayers < 0) TotalPlayers = 0;
        }

        public static void RemoveFromReadyPlayers()
        {
            PlayersReady--;
            if (PlayersReady > 4) PlayersReady = 4;
            if (PlayersReady < 0) PlayersReady = 0;
        }
    }
}