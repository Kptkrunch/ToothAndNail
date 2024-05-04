using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace JAssets.Scripts_SC.Multiplayer
{
    public class RoomButton : MonoBehaviour
    {
        private RoomInfo _info;
        public TMP_Text buttonText;

        public void SetButtonDetails(RoomInfo inputInfo)
        {
            _info = inputInfo;
            buttonText.text = _info.Name;
        }

        public void OpenRoom()
        {
            Launcher.instance.JoinRoom(_info);
        }
    }
}
