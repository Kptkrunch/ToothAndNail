using System.Collections.Generic;
using JAssets.Scripts_SC.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class EquippedGear : MonoBehaviour
    {
        [Header("Tools")][ShowInInspector]
        public Dictionary<string, Tool> tools;
        [Header("Weapons")][ShowInInspector]
        public Dictionary<string, Weapon> weapons;
    }
}