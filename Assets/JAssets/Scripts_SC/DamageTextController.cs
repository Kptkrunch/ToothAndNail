using MoreMountains.Feedbacks;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class DamageTextController : MonoBehaviour
    {
        public static DamageTextController instance;
        public MMF_Player player;

        private void Awake()
        {
            instance = this;
        }
    }
}