using MoreMountains.Feedbacks;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;
    public MMF_Player player;
    private void Awake()
    {
        instance = this;
    }
}
