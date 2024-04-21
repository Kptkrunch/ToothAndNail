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
        private float refreshTimer;
        private bool looted;
        private static readonly int OutlineAlpha = Shader.PropertyToID("_OutlineAlpha");
        private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");

        private void Start()
        {
            refreshTimer = refreshCd;
            lootTable = Instantiate(lootTable);
            spriteRenderer.sharedMaterial = new Material(spriteRenderer.sharedMaterial);
        }
        private void FixedUpdate()
        {
            if (looted) refreshTimer -= Time.deltaTime;
            if (refreshTimer <= 0) looted = false;
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
            looted = true;

            var lootName = lootTable.GetRandomLoot();
            Debug.Log(lootName);
            var pickup = Library.instance.pickupsDict["P" + lootName].GetPooledGameObject();
            Debug.Log(pickup);
            pickup.transform.position = transform.position;
            pickup.SetActive(true);
        }
    }
}
