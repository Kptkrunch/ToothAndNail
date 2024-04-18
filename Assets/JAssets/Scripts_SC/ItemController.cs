using System.Collections;
using JAssets.Scripts_SC.Items;
using JAssets.Scripts_SC.UI;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private PlayerUI playerUi;
        [SerializeField] private EquippedGear gear;
        [SerializeField] private LayerMask pickupLayer;
        [SerializeField] private Animator weaponAnimator;
        [SerializeField] private Sprite noIdeasSprite;

        [CanBeNull] [SerializeField] private string itemSlotA = "";
        [CanBeNull] [SerializeField] private string itemSlotB = "";
        [CanBeNull] [SerializeField] private string activeRecipe = "";
        [SerializeField] private int openSlot;


        [SerializeField] private float attackCd;
        [SerializeField] private float specialCd;
        [SerializeField] private float consumeCd;
        [SerializeField] private bool canAttack;
        [SerializeField] private bool canSpecial;
        [SerializeField] private bool canConsume;
        [SerializeField] private string attackString;
        [SerializeField] private string specString;
        [SerializeField] private string consumeString;
        [SerializeField] public string weaponHand;

        private void Start()
        {
            canAttack = true;
            canSpecial = true;
            canConsume = true;
        }

        public void GetItemA(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var pickup = Physics2D.OverlapCircle(transform.position, 1f, pickupLayer);
            var item = pickup.GetComponent<Pickup>();

            if (!item) return;
            item.PickupItem();
            
            DropEquippedGear(itemSlotA, "itemSlotA");

            itemSlotA = item.itemName;
            if (CraftingMatrix.instance.matrix.ContainsKey(itemSlotA) &&
                CraftingMatrix.instance.matrix.ContainsKey(itemSlotB))
            {
                CheckForCraftableHandler();
            }
            else
            {
                playerUi.UpdateItemsUi("C", noIdeasSprite);
            }
            
            var sprite = item.GetComponent<SpriteRenderer>().sprite;
            if (item.itemType == "Consumable")
            {
                canConsume = true;
                consumeString = "";
                consumeCd = 1f;
                
            }
            else
            {
                if (itemSlotA != null)
                {
                    gear.items[itemSlotA].SetActive(true);
                    var weapon = gear.items[itemSlotA].GetComponentInChildren<Weapon>();
                    sprite = weapon.GetComponent<SpriteRenderer>().sprite;
                    weaponAnimator = weapon.animator;
                    weaponHand = itemSlotA;
                    attackString = weapon.rtso.attackAnimString;
                    specString = weapon.rtso.specAnimString;
                    attackCd = weapon.rtso.attackCd;
                    specialCd = weapon.rtso.specialCd;
                }
            }

            if (itemSlotA != null) playerUi.UpdateItemsUi("A", sprite);
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

            if (CraftingMatrix.instance.matrix.ContainsKey(itemSlotA) &&
                CraftingMatrix.instance.matrix.ContainsKey(itemSlotB))
            {
                CheckForCraftableHandler();
            }
            
            var sprite = item.GetComponent<SpriteRenderer>().sprite;
            if (item.itemType == "Consumable")
            {
                canConsume = true;
                consumeString = "";
                consumeCd = 1f;
            }
            else
            {
                if (itemSlotB != null)
                {
                    gear.items[itemSlotB].SetActive(true);
                    var weapon = gear.items[itemSlotB].GetComponent<Weapon>();
                    sprite = weapon.GetComponent<SpriteRenderer>().sprite;
                    weaponAnimator = weapon.animator;
                    weaponHand = itemSlotB;
                    attackString = weapon.rtso.attackAnimString;
                    specString = weapon.rtso.specAnimString;
                    attackCd = weapon.rtso.attackCd;
                    specialCd = weapon.rtso.specialCd;
                }
            }

            if (itemSlotB != null) playerUi.UpdateItemsUi("B", sprite);
        }
        
        private void DropEquippedGear(string itemName, string itemSlot)
        {
            if (itemName == "") return;
            switch (itemSlot)
            {
                case "itemSlotA":
                    if (itemSlotA == "") return;
                    if (itemSlotA != null) gear.items[itemSlotA].gameObject.SetActive(false);
                    itemSlotA = "";
                    break;
                case "itemSlotB":
                    if (itemSlotB == "") return;
                    if (itemSlotB != null) gear.items[itemSlotB].gameObject.SetActive(false);
                    itemSlotB = "";
                    break;
            }
            
            var itemToDrop = Library.instance.pickupsDict["P" + itemName].GetPooledGameObject();
            itemToDrop.transform.position = transform.position;
            itemToDrop.SetActive(true);
        }

        private void CheckForCraftableHandler()
        {
            var recipe = CraftingMatrix.instance.GetRecipeFromMatrix(itemSlotA, itemSlotB);
            if (recipe == null) return;
            activeRecipe = recipe;
            playerUi.UpdateItemsUi("C", gear.items[activeRecipe].GetComponent<SpriteRenderer>().sprite);
        }

        public void Attack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            gear.items[weaponHand].GetComponentInChildren<Weapon>().Attack();
            weaponAnimator.SetBool(attackString, true);
            StartCoroutine(AttackCooldownTimer());
        }
        public void Special(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            gear.items[weaponHand].GetComponentInChildren<Weapon>().Special();
            weaponAnimator.SetBool(specString, true);
            StartCoroutine(SpecialCooldownTimer());
        }

        private IEnumerator AttackCooldownTimer()
        {
            canAttack = false;
            gear.items[weaponHand].GetComponentInChildren<Weapon>().Attack();
            yield return new WaitForSeconds(attackCd);
            canAttack = true;
        }
        private IEnumerator SpecialCooldownTimer()
        {
            canSpecial = false;
            gear.items[weaponHand].GetComponentInChildren<Weapon>().Special();
            yield return new WaitForSeconds(specialCd);
            canSpecial = true;
        }

        public void CraftItem(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            var newItem = Library.instance.pickupsDict["P" + activeRecipe].GetPooledGameObject();
            if (!newItem) return;

            newItem.transform.position = transform.position;
            newItem.SetActive(true);

            activeRecipe = "";
            itemSlotA = "";
            itemSlotB = "";
            playerUi.ClearImagesUi();
        }
    }
}