using System.Collections;
using JAssets.Scripts_SC.Items;
using JAssets.Scripts_SC.SOScripts;
using JAssets.Scripts_SC.UI;
using JetBrains.Annotations;
using Unity.VisualScripting;
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

        [CanBeNull] public GameObject itemSlotA;
        [CanBeNull] public GameObject itemSlotB;
        [CanBeNull] public Recipe_SO activeRecipe;
        [SerializeField] [ItemCanBeNull] private GameObject[] slotArray = new GameObject?[2];
        [SerializeField] [ItemCanBeNull] private int openSlot;


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

            slotArray[0] = item.gameObject;
            itemSlotA = slotArray[0];
            if (string.IsNullOrEmpty(itemSlotA.name)) return;
            
            DropEquippedGear(itemSlotA.name, item.itemType);
            playerUi.UpdateItemsUi("A", itemSlotA.GetComponent<SpriteRenderer>().sprite);

            CheckForCraftableHandler();
            
            if (item.itemType == "Consumable")
            {
                _canConsume = true;
                _consumeString = "";
                consumeCd = 1f;
            }
            else
            {
                gear.items[itemSlotA.name].SetActive(true);
                var weapon = item.GetComponentInChildren<Weapon>();
                _weaponAnimator = slotArray[0].GetComponent<Weapon>().animator;
                weaponHand = "itemSlotB";
                _attackString = weapon.weaponSo.attackAnimString;
                _specString = weapon.weaponSo.specAnimString;
                attackCd = weapon.weaponSo.attackCd;
                specialCd = weapon.weaponSo.specialCd;
            }
        }

        public void GetItemB(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var pickup = Physics2D.OverlapCircle(transform.position, 1f, pickupLayer);
            var item = pickup.GetComponent<Pickup>();

            if (!item) return;
            item.PickupItem();

            slotArray[1] = item.gameObject;
            itemSlotB = slotArray[1];
            if (!string.IsNullOrEmpty(itemSlotB.name)) DropEquippedGear(itemSlotB.name, item.itemType);
            playerUi.UpdateItemsUi("B", itemSlotB.GetComponent<SpriteRenderer>().sprite);

            CheckForCraftableHandler();

            if (item.itemType == "Consumable")
            {
                _canConsume = true;
                _consumeString = "";
                consumeCd = 1f;
            }
            else if (itemSlotB.CompareTag("Weapon"))
            {
                gear.items[itemSlotB.name].SetActive(true);
                var weapon = item.GetComponentInChildren<Weapon>();
                _weaponAnimator = slotArray[1].GetComponent<Weapon>().animator;
                weaponHand = "itemSlotB";
                _attackString = weapon.weaponSo.attackAnimString;
                _specString = weapon.weaponSo.specAnimString;
                attackCd = weapon.weaponSo.attackCd;
                specialCd = weapon.weaponSo.specialCd;
            }

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

        private void DropEquippedGear(string itemName, string itemType)
        {
            var pickup = Library.instance.pickupsDict["P" + itemName].GetPooledGameObject();
            pickup.transform.position = transform.position;
            pickup.SetActive(true);
            if (itemType == "Weapon") gear.items[itemName].SetActive(false);
        }

        private void CheckForCraftableHandler()
        {
            if (!itemSlotA || !itemSlotB) return;

            var recipe = CraftingMatrix.instance.GetRecipeFromMatrix(itemSlotA.name, itemSlotB.name);
            if (recipe == null) return;
            activeRecipe = recipe;
            playerUi.UpdateItemsUi("C", activeRecipe.sprite);
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