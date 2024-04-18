using System.Collections;
using JAssets.Scripts_SC.Items;
using JAssets.Scripts_SC.SOScripts;
using JAssets.Scripts_SC.UI;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
    public class ItemController : MonoBehaviour
    {
        public PlayerUI playerUi;
        public EquippedGear gear;
        public LayerMask pickupLayer;
        private Animator _weaponAnimator;

        [CanBeNull] public string itemSlotA = "";
        [CanBeNull] public string itemSlotB = "";
        [CanBeNull] public Recipe_SO activeRecipe;
        [SerializeField] private int openSlot;


        public float attackCd;
        public float specialCd;
        public float consumeCd;
        private bool _canAttack;
        private bool _canSpecial;
        private bool _canConsume;
        private string _attackString;
        private string _specString;
        private string _consumeString;
        public string weaponHand;

        private void Start()
        {
            _canAttack = true;
            _canSpecial = true;
        }

        public void GetItemA(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var pickup = Physics2D.OverlapCircle(transform.position, 1f, pickupLayer);
            var item = pickup.GetComponent<Pickup>();

            if (!item) return;
            item.PickupItem();
            Debug.Log(itemSlotA);
            DropEquippedGear(itemSlotA, "itemSlotA");
            Debug.Log(itemSlotA);
            
            itemSlotA = item.itemName;

            Debug.Log(itemSlotA);
            Debug.Log("Here");
            CheckForCraftableHandler();
            Debug.Log(item.itemType);

            var sprite = item.GetComponent<SpriteRenderer>().sprite;
            if (item.itemType == "Consumable")
            {
                _canConsume = true;
                _consumeString = "";
                consumeCd = 1f;
                
            }
            else
            {
                gear.items[itemSlotA].SetActive(true);
                var weapon = gear.items[itemSlotA].GetComponentInChildren<Weapon>();
                _weaponAnimator = weapon.animator;
                weaponHand = itemSlotA;
                _attackString = weapon.rtso.attackAnimString;
                _specString = weapon.rtso.specAnimString;
                attackCd = weapon.rtso.attackCd;
                specialCd = weapon.rtso.specialCd;
            }

            if (itemSlotA != null) playerUi.UpdateItemsUi("A", sprite);
        }

        public void GetItemB(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var pickup = Physics2D.OverlapCircle(transform.position, 1f, pickupLayer);
            var item = pickup.GetComponent<Pickup>();

            DropEquippedGear(itemSlotB, "itemSlotB");
            Debug.Log(itemSlotA);
            
            itemSlotB = item.itemName;
            CheckForCraftableHandler();

            var sprite = item.GetComponent<SpriteRenderer>().sprite;
            switch (item.itemType)
            {
                case "Consumable":
                    _canConsume = true;
                    _consumeString = "";
                    consumeCd = 1f;
                    break;
                case "Weapon":
                {
                    gear.items[itemSlotB].SetActive(true);
                    var weapon = gear.items[itemSlotB].GetComponentInChildren<Weapon>();
                    _weaponAnimator = weapon.animator;
                    weaponHand = itemSlotB;
                    _attackString = weapon.rtso.attackAnimString;
                    _specString = weapon.rtso.specAnimString;
                    attackCd = weapon.rtso.attackCd;
                    specialCd = weapon.rtso.specialCd;
                    break;
                }
            }

            if (itemSlotB != null) playerUi.UpdateItemsUi("B", sprite);
        }
        
        private void DropEquippedGear(string itemName, string itemSlot)
        {
            switch (itemSlot)
            {
                case "itemSlotA":
                    if (itemSlotA == "") return;
                    gear.items[itemSlotA].gameObject.SetActive(false);
                    itemSlotA = "";
                    break;
                case "itemSlotB":
                    if (itemSlotB == "") return;
                    gear.items[itemSlotB].gameObject.SetActive(false);
                    itemSlotB = "";
                    break;
            }
            var itemToDrop = Library.instance.pickupsDict["P" + itemName].GetPooledGameObject();
            itemToDrop.transform.position = transform.position;
            itemToDrop.SetActive(true);
        }

        private void CheckForCraftableHandler()
        {
            if (string.IsNullOrEmpty(itemSlotA) || string.IsNullOrEmpty(itemSlotB)) return;

            var recipe = CraftingMatrix.instance.GetRecipeFromMatrix(itemSlotA, itemSlotB);
            if (recipe == null) return;
            activeRecipe = recipe;
            playerUi.UpdateItemsUi("C", activeRecipe.sprite);
        }

        public void Attack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            gear.items[weaponHand].GetComponentInChildren<Weapon>().Attack();
            _weaponAnimator.SetBool(_attackString, true);
            StartCoroutine(AttackCooldownTimer());
        }
        public void Special(InputAction.CallbackContext context)
        {
            var slot = weaponHand;
            if (!context.performed) return;
            gear.items[weaponHand].GetComponentInChildren<Weapon>().Special();
            _weaponAnimator.SetBool(_specString, true);
            StartCoroutine(SpecialCooldownTimer());
        }

        private IEnumerator AttackCooldownTimer()
        {
            _canAttack = false;
            gear.items[weaponHand].GetComponentInChildren<Weapon>().Attack();
            yield return new WaitForSeconds(attackCd);
            _canAttack = true;
        }
        private IEnumerator SpecialCooldownTimer()
        {
            _canSpecial = false;
            gear.items[weaponHand].GetComponentInChildren<Weapon>().Special();
            yield return new WaitForSeconds(specialCd);
            _canSpecial = true;
        }

        public void CraftItem(InputAction.CallbackContext context)
        {
            if (!context.performed || !activeRecipe) return;

            var newItem = Library.instance.pickupsDict["P" + activeRecipe.name].GetPooledGameObject();
            if (!newItem) return;

            newItem.transform.position = transform.position;
            newItem.SetActive(true);

            activeRecipe = null;
            itemSlotA = null;
            itemSlotB = null;
            playerUi.ClearImagesUi();
        }
    }
}