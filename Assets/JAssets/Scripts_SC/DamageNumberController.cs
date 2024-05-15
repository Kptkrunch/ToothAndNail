using MoreMountains.Feedbacks;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class DamageNumberController : MonoBehaviour
    {
        public static DamageNumberController Instance;
        public MMF_Player player;
        private void Awake()
        {
            Instance = this;
        }
    }
}
