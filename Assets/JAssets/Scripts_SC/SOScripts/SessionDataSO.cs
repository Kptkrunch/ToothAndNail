using System.Collections.Generic;
using UnityEngine;

namespace JAssets.Scripts_SC.SOScripts
{
    [CreateAssetMenu(fileName = "New Session Data", menuName = "SessionData_SO")]
    public class SessionDataSo : ScriptableObject
    {
        public int totalPlayers = 0;
        public List<DataTypeController.PlayerSlotData> PlayerDataList = new();
        
        public void UpdateTotalPlayers()
        {
            totalPlayers++;
        }
    }
}