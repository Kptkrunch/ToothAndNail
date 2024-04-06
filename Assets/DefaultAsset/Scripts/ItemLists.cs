using System.Collections.Generic;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

public class ItemLists : MonoBehaviour
{
    public static ItemLists instance;
    
    [Header("Weapons List")][ShowInInspector]
    public List<Weapon> weaponsList = new();
    
    [Header("Tools List")][ShowInInspector]
    public List<Tool> toolList = new();

    [Header("Tool Effects List")] [ShowInInspector]
    public List<MMSimpleObjectPooler> toolEffectsList = new();
    
    [Header("Pickups List")][ShowInInspector]
    public List<MMSimpleObjectPooler> pickupList = new();

    [Header("Particles List")] [ShowInInspector]
    public List<MMSimpleObjectPooler> particleList = new();
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    private void Start()
    {
        PopulateLibrary();
    }

    private void PopulateLibrary()
    {
        foreach (var w in weaponsList)
        {
            Library.instance.weaponsDict[w.name] = w;
        }

        foreach (var t in toolList)
        {
            Library.instance.toolsDict[t.name] = t;
        }

        foreach (var p in pickupList)
        {
            Library.instance.pickupsDict[p.name] = p;
        }

        foreach (var p in particleList)
        {
            Library.instance.particleDict[p.name] = p;
        }
        
        foreach (var t in toolEffectsList)
        {
            Library.instance.toolEffectsDict[t.name] = t;
        }
    }
    
}
