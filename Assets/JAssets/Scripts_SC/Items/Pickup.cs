using System;
using JAssets.Scripts_SC.Lists;
using UnityEngine;

namespace JAssets.Scripts_SC.Items
{
    public class Pickup : MonoBehaviour
    {
        
        public GameObject parent;
        public string itemName;
        public string itemType;
        [SerializeField] private float spottedTimer = 5;
        [SerializeField] private SpriteRenderer material;
        private static readonly int Glow = Shader.PropertyToID("_Glow");
        private bool spotted;

        private void Awake()
        {
            material.sharedMaterial = Instantiate(material.sharedMaterial);
        }

        private void OnEnable()
        {
            spottedTimer = 5;
            spotted = false;
            
        }
        private void FixedUpdate()
        {
            if (!spotted) return;
            if (spottedTimer <= 0)
            {
                spotted = false;
                material.sortingOrder = 14;
            }
            spottedTimer -= Time.deltaTime;
            material.sharedMaterial.SetFloat(Glow, spottedTimer);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.name);
            {
                spotted = true;
                if (spottedTimer > 0) return;
                spottedTimer = 5.0f;
                
            }
        }

        public void PickupItem()
        {
            var pickupFlash = Library.instance.particleDict["ItemFlash-0"].GetPooledGameObject();
            pickupFlash.transform.position = transform.position;
            pickupFlash.SetActive(true);
            parent.SetActive(false);
        }
    }
}