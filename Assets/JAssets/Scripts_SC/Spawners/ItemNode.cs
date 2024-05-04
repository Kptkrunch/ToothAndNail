using System.Collections;
using JAssets.Scripts_SC.Lists;
using JAssets.Scripts_SC.SOScripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC.Spawners
{
    public class ItemNode : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        [ShowInInspector] public LootTable_SO lootTable;
        [SerializeField] private float refreshCd;
        private bool looted;
        private static readonly int OutlineAlpha = Shader.PropertyToID("_OutlineAlpha");
        private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
        private static readonly int ColorChangeTolerance = Shader.PropertyToID("_ColorChangeTolerance");

        private void Start()
        {
            lootTable = Instantiate(lootTable);
            spriteRenderer.sharedMaterial = new Material(spriteRenderer.sharedMaterial);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!looted && other.CompareTag("Player") || other.CompareTag("Interactable"))
            {
                spriteRenderer.sharedMaterial.SetColor(OutlineColor, Color.blue);
                spriteRenderer.sharedMaterial.SetFloat(OutlineAlpha, 0.5f);
            } else if (looted && other.CompareTag("Player") || other.CompareTag("Interactable"))
            {
                spriteRenderer.sharedMaterial.SetColor(OutlineColor, Color.grey);
                spriteRenderer.sharedMaterial.SetFloat(OutlineAlpha, 0.5f);
            } 
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            spriteRenderer.sharedMaterial.SetFloat(OutlineAlpha, 0.0f);
        }

        public void ActivateNode()
        {
            if (looted) return;
            StartCoroutine(NodeRefreshTimer());
        }

        private IEnumerator NodeRefreshTimer()
        {
            if (!looted)
            {
                looted = true;
                spriteRenderer.sharedMaterial.SetFloat(ColorChangeTolerance, 0.0f);
                var lootName = lootTable.GetRandomLoot();
                var pickup = Library.instance.pickupsDict["P" + lootName + "-0"].GetPooledGameObject();
                pickup.transform.position = transform.position;
                pickup.SetActive(true);

                yield return new WaitForSeconds(refreshCd);
                looted = false;
                spriteRenderer.sharedMaterial.SetFloat(ColorChangeTolerance, 1.0f);
            }
        }
    }
}
