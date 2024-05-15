using System.Collections.Generic;
using UnityEngine;

namespace JAssets.Scripts_SC.Multiplayer
{
    public static class PlayerSessionData
    {
        private static List<GameObject> _playerList;
        public static int PlayerCount => _playerList.Count;
        public static List<GameObject> PlayerTeam1;
        public static List<GameObject> PlayerTeam2;
        public static List<GameObject> PlayerTeam3;
        public static List<GameObject> PlayerTeam4;
        
        public static void AddPlayer(GameObject player)
        {
            _playerList.Add(player);
        }

        public static void RemovePlayer(GameObject player)
        {
            _playerList.Remove(player);
        }

        public static void ChangePlayerTeam(GameObject player, int teamNumber, int newTeamNumber)
        {
            switch (teamNumber)
            {
                case 1:
                    PlayerTeam1.Remove(player);
                    break;
                case 2:
                    PlayerTeam2.Remove(player);
                    break;
                case 3:
                    PlayerTeam3.Remove(player);
                    break;
                case 4:
                    PlayerTeam4.Remove(player);
                    break;
            }

            switch (newTeamNumber)
            {
                case 1:
                    PlayerTeam1.Add(player);
                    break;
                case 2:
                    PlayerTeam2.Add(player);
                    break;
                case 3:
                    PlayerTeam3.Add(player);
                    break;
                case 4:
                    PlayerTeam4.Add(player);
                    break;
            }
        }
    }
}