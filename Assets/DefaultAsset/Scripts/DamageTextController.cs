using MoreMountains.Feedbacks;
using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    public static DamageTextController instance;
    public MMF_Player player;
    private void Awake()
    {
        instance = this;
    }
}
