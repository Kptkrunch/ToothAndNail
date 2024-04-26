using System;
using System.Collections.Generic;
using JAssets.Scripts_SC.Lists;
using JAssets.Scripts_SC.SOScripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.ScriptableObjects_SO
{
    public class CraftingMatrix : MonoBehaviour
    {
        public static CraftingMatrix instance;
        [ShowInInspector] public Dictionary<string, Dictionary<string, Recipe_SO>> matrixDict = new();

        private void Awake()
        {
            instance = this;
        }

        public void OnEnable()
        {
            ScrapDictFill();
            StickDictFill();
            StoneDictFill();
            BoneDictFill();
            VineDictFill();
        }

        public string GetRecipeFromMatrix(string row, string material)
        {
            Debug.Log(row + ", mat: " + material);

            return !matrixDict[row][material] ? "" : matrixDict[row][material].resultItem;
        }

        private void VineDictFill()
        {
            
             Recipe_SO VineRecipeSo;
             Recipe_SO StoneRecipeSo;
             Recipe_SO StickRecipeSo;
             Recipe_SO BoneRecipeSo;
             Recipe_SO ScrapRecipeSo;
             VineRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
             StoneRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
             StickRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
             BoneRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
             ScrapRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
             
            StickRecipeSo.name = "Stick";
            StickRecipeSo.resultItem = "Bow";
            StoneRecipeSo.name = "Stone";
            StoneRecipeSo.resultItem = "Bolos";
            VineRecipeSo.name = "Vine";
            VineRecipeSo.resultItem = "Net";
            BoneRecipeSo.name = "Bone";
            BoneRecipeSo.resultItem = "Whip";
            ScrapRecipeSo.name = "Scrap";
            ScrapRecipeSo.resultItem = "Knife";
            
            var newDict = new Dictionary<string, Recipe_SO>();
            matrixDict["Vine"] = newDict;
            matrixDict["Vine"]["Vine"] = VineRecipeSo;
            matrixDict["Vine"]["Stone"] = StoneRecipeSo;
            matrixDict["Vine"]["Stick"] = StickRecipeSo;
            matrixDict["Vine"]["Bone"] = BoneRecipeSo;
            matrixDict["Vine"]["Scrap"] = ScrapRecipeSo;
        }

        private void StickDictFill()
        {
            Recipe_SO VineRecipeSo;
            Recipe_SO StoneRecipeSo;
            Recipe_SO StickRecipeSo;
            Recipe_SO BoneRecipeSo;
            Recipe_SO ScrapRecipeSo;
            VineRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StoneRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StickRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            BoneRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            ScrapRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StickRecipeSo.name = "Stick";
            StickRecipeSo.resultItem = "SnareTrap";
            StoneRecipeSo.name = "Stone";
            StoneRecipeSo.resultItem = "Sledge";
            VineRecipeSo.name = "Vine";
            VineRecipeSo.resultItem = "Bow";
            BoneRecipeSo.name = "Bone";
            BoneRecipeSo.resultItem = "HandAx";
            ScrapRecipeSo.name = "Scrap";
            ScrapRecipeSo.resultItem = "Caltrops";
            
            var newDict1 = new Dictionary<string, Recipe_SO>();
            matrixDict["Stick"] = newDict1;
            matrixDict["Stick"]["Vine"] = VineRecipeSo;
            matrixDict["Stick"]["Stone"] = StoneRecipeSo;
            matrixDict["Stick"]["Stick"] = StickRecipeSo;
            matrixDict["Stick"]["Bone"] = BoneRecipeSo;
            matrixDict["Stick"]["Scrap"] = ScrapRecipeSo;
        }

        private void StoneDictFill()
        {
            Recipe_SO VineRecipeSo;
            Recipe_SO StoneRecipeSo;
            Recipe_SO StickRecipeSo;
            Recipe_SO BoneRecipeSo;
            Recipe_SO ScrapRecipeSo;
            VineRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StoneRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StickRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            BoneRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            ScrapRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StickRecipeSo.name = "Stick";
            StickRecipeSo.resultItem = "Sledge";
            StoneRecipeSo.name = "Stone";
            StoneRecipeSo.resultItem = "Sword";
            VineRecipeSo.name = "Vine";
            VineRecipeSo.resultItem = "Bow";
            BoneRecipeSo.name = "Bone";
            BoneRecipeSo.resultItem = "Spear";
            ScrapRecipeSo.name = "Scrap";
            ScrapRecipeSo.resultItem = "Buckler";
            
            var newDict2 = new Dictionary<string, Recipe_SO>();
            matrixDict["Stone"] = newDict2;
            matrixDict["Stone"]["Vine"] = VineRecipeSo;
            matrixDict["Stone"]["Stone"] = StoneRecipeSo;
            matrixDict["Stone"]["Stick"] = StickRecipeSo;
            matrixDict["Stone"]["Bone"] = BoneRecipeSo;
            matrixDict["Stone"]["Scrap"] = ScrapRecipeSo;
        }

        private void BoneDictFill()
        {
            Recipe_SO VineRecipeSo;
            Recipe_SO StoneRecipeSo;
            Recipe_SO StickRecipeSo;
            Recipe_SO BoneRecipeSo;
            Recipe_SO ScrapRecipeSo;
            VineRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StoneRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StickRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            BoneRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            ScrapRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StickRecipeSo.name = "Stick";
            StickRecipeSo.resultItem = "Spear";
            StoneRecipeSo.name = "Stone"; 
            StoneRecipeSo.resultItem = "HandAx"; 
            VineRecipeSo.name = "Vine";
            VineRecipeSo.resultItem = "Whip";
            BoneRecipeSo.name = "Bone";
            BoneRecipeSo.resultItem = "Cross";
            ScrapRecipeSo.name = "Scrap";
            ScrapRecipeSo.resultItem = "TowerShield";
            
            var newDict3 = new Dictionary<string, Recipe_SO>();
            matrixDict["Bone"] = newDict3;
            matrixDict["Bone"]["Vine"] = VineRecipeSo;
            matrixDict["Bone"]["Stone"] = StoneRecipeSo;
            matrixDict["Bone"]["Stick"] = StickRecipeSo;
            matrixDict["Bone"]["Bone"] = BoneRecipeSo;
            matrixDict["Bone"]["Scrap"] = ScrapRecipeSo;
        }
        private void ScrapDictFill()
        {
            Recipe_SO VineRecipeSo;
            Recipe_SO StoneRecipeSo;
            Recipe_SO StickRecipeSo;
            Recipe_SO BoneRecipeSo;
            Recipe_SO ScrapRecipeSo;
            VineRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StoneRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StickRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            BoneRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            ScrapRecipeSo = ScriptableObject.CreateInstance<Recipe_SO>();
            StickRecipeSo.name = "Stick";
            StickRecipeSo.resultItem = "Buckler";
            StoneRecipeSo.name = "Stone";
            StoneRecipeSo.resultItem = "Caltrops";
            VineRecipeSo.name = "Vine";
            VineRecipeSo.resultItem = "Knife";
            BoneRecipeSo.name = "Bone";
            BoneRecipeSo.resultItem = "Cross";
            ScrapRecipeSo.name = "Scrap";
            ScrapRecipeSo.resultItem = "BattleAx";
            
            var newDict4 = new Dictionary<string, Recipe_SO>();
            matrixDict["Scrap"] = newDict4;
            matrixDict["Scrap"]["Vine"] = VineRecipeSo;
            matrixDict["Scrap"]["Stone"] = StoneRecipeSo;
            matrixDict["Scrap"]["Stick"] = StickRecipeSo;
            matrixDict["Scrap"]["Bone"] = BoneRecipeSo;
            matrixDict["Scrap"]["Scrap"] = ScrapRecipeSo;
        }
    }
}