using System;
using System.Collections;
using JAssets.ScriptableObjects_SO;
using JAssets.Scripts_SC.Items;
using JAssets.Scripts_SC.Lists;
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
        
        [CanBeNull] [SerializeField] public string itemSlotA = "";
        [CanBeNull] [SerializeField] public string itemSlotB = "";
        [CanBeNull] [SerializeField] private string activeRecipeName = "";

        public void GetItemA(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var pickup = Physics2D.OverlapCircle(transform.position, 1f, pickupLayer);
            var item = pickup.GetComponent<Pickup>();

            if (!item) return;
            item.PickupItem();
            DropEquippedGear(itemSlotA, "itemSlotA");
            
            itemSlotA = item.itemName;
            if (itemSlotA == "") return;
            
            imageA.sprite = Library.instance.itemDict[itemSlotA].GetComponent<SpriteRenderer>().sprite;
            CheckForCraftableHandler(itemSlotA, itemSlotB, item);
            if (item.itemType == "Consumable") return;
            attackController.UpdateGearStats("A", item.itemName);
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
            CheckForCraftableHandler(itemSlotA, itemSlotB, item);
            if (item.itemType == "Consumable") return;
            attackController.UpdateGearStats("B", item.itemName);
        }
        
        private void DropEquippedGear(string itemName, string itemSlot)
        {
            Debug.Log(itemName + ",  " + itemSlot);
            if (itemName == "") return;
            switch (itemSlot)
            {
                case "itemSlotA":
                    if (itemSlotA == "") return;
                    if (itemSlotA != null) attackController.gear.Items[itemSlotA].gameObject.SetActive(false);
                    itemSlotA = "";
                    break;
                case "itemSlotB":
                    if (itemSlotB == "") return;
                    if (itemSlotB != null) attackController.gear.Items[itemSlotB].gameObject.SetActive(false);
                    itemSlotB = "";
                    break;
            }
            var itemToDrop = Library.instance.pickupsDict["P" + itemName + "-0"].GetPooledGameObject();

            itemToDrop.transform.position = transform.position;
            itemToDrop.SetActive(true);
        }

        private void CheckForCraftableHandler(string a, string b, Pickup item)
        {
            if (item.itemType == "Weapon" || a == "" || b == "")
            {
                activeRecipeName = "";
                imageC.sprite = null;
                return;
            }
            var recipe = CraftingMatrix.instance.GetRecipeFromMatrix(a, b);
            if (recipe == "") return;
            activeRecipeName = recipe;
            imageC.sprite = Library.instance.itemDict[recipe].GetComponent<SpriteRenderer>().sprite;
        }

        public void CraftItem(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            var newItem = Library.instance.pickupsDict["P" + activeRecipeName + "-0"].GetPooledGameObject();
            if (!newItem) return;

            newItem.transform.position = transform.position;
            newItem.SetActive(true);

            ClearItemSlots();
        }

        private void ClearItemSlots()
        {
            activeRecipeName = "";
            itemSlotA = "";
            itemSlotB = "";
            imageA.sprite = null;
            imageB.sprite = null;
            imageC.sprite = null;
        }
    }
}