using System;
using System.Collections;
using JAssets.Scripts_SC.Items;
using JAssets.Scripts_SC.Spawners;
using JAssets.Scripts_SC.UI;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace JAssets.Scripts_SC
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private PlayerUI playerUi;
        [SerializeField] private AttackController attackController;
        [SerializeField] private LayerMask pickupLayer;
        public Image imageA;
        public Image imageB;
        public Image imageC;
        
        [CanBeNull] [SerializeField] private string itemSlotA = "";
        [CanBeNull] [SerializeField] private string itemSlotB = "";
        [CanBeNull] [SerializeField] private string activeRecipeName = "";

        public void GetItemA(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var pickup = Physics2D.OverlapCircle(transform.position, 1f, pickupLayer);
            Debug.Log("Pickup: " + pickup);
            var item = pickup.GetComponent<Pickup>();
            Debug.Log("item: " + item);

            if (!item) return;
            item.PickupItem();
            Debug.Log("A" + itemSlotA);

            DropEquippedGear(itemSlotA, "itemSlotA");
            
            itemSlotA = item.itemName;
            if (itemSlotA == "") return;

            imageA.sprite = Library.instance.itemDict[itemSlotA].GetComponent<SpriteRenderer>().sprite;
            Debug.Log("AA" + itemSlotA);
            
            Debug.Log("Checking");

            CheckForCraftableHandler(itemSlotA, itemSlotB);
            
        }

        public void GetItemB(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var pickup = Physics2D.OverlapCircle(transform.position, 1f, pickupLayer);
            var item = pickup.GetComponent<Pickup>();

            if (!item) return;
            item.PickupItem();
            
            DropEquippedGear(itemSlotB, "itemSlotB");
            
            itemSlotB = item.itemName;
            if (itemSlotB == "") return;
            imageB.sprite = Library.instance.itemDict[itemSlotB].GetComponent<SpriteRenderer>().sprite;

            CheckForCraftableHandler(itemSlotA, itemSlotB);
            Debug.Log("active recipe:" + activeRecipeName);
        }
        
        private void DropEquippedGear(string itemName, string itemSlot)
        {
            if (itemName == "") return;
            switch (itemSlot)
            {
                case "itemSlotA":
                    if (itemSlotA == "") return;
                    if (itemSlotA != null) attackController.gear.items[itemSlotA].gameObject.SetActive(false);
                    itemSlotA = "";
                    break;
                case "itemSlotB":
                    if (itemSlotB == "") return;
                    if (itemSlotB != null) attackController.gear.items[itemSlotB].gameObject.SetActive(false);
                    itemSlotB = "";
                    break;
            }
            
            var itemToDrop = Library.instance.pickupsDict["P" + itemName].GetPooledGameObject();
            itemToDrop.transform.position = transform.position;
            itemToDrop.SetActive(true);
        }

        private void CheckForCraftableHandler(string a, string b)
        {
            if (a == "" || b == "") return;
            var recipe = CraftingMatrix.instance.GetRecipeFromMatrix(a, b);
            activeRecipeName = recipe;
            imageC.sprite = Library.instance.itemDict[recipe].GetComponent<SpriteRenderer>().sprite;
            Debug.Log("Image C: " + imageC.sprite);
        }

        public void CraftItem(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            var newItem = Library.instance.pickupsDict["P" + activeRecipeName + "-0"].GetPooledGameObject();
            if (!newItem) return;

            newItem.transform.position = transform.position;
            newItem.SetActive(true);

            activeRecipeName = "";
            itemSlotA = "";
            itemSlotB = "";
            playerUi.ClearImagesUi();
        }
    }
}