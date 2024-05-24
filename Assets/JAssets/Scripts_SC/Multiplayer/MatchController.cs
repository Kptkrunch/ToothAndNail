using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JAssets.Scripts_SC.Multiplayer
{
    public class MatchController : MonoBehaviour
    {
        public static MatchController Instance;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
