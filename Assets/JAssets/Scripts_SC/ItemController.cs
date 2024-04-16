using System.Collections;
using JAssets.Scripts_SC.Items;
using JAssets.Scripts_SC.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
    public class ItemController : MonoBehaviour
    {
        public PlayerUI playerUi;
        public EquippedGear gear;
        public LayerMask pickupLayer;
        public string activeWeapon;
        public string activeTool;

        public float attackCd;
        public float specialCd;
        public float toolCd;
        
        [SerializeField] public string attackString;
        [SerializeField] public string specialString;

        private bool _canAttack;
        private bool _canSpecial;
        private bool _canTool;
        private Animator _toolAnimator;
        private string _useToolString;
        private Animator _weaponAnimator;

        private void Start()
        {
            _canAttack = true;
            _canSpecial = true;
            _canTool = true;
        }

        public void GetWeaponOrConsumable(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var pickup = Physics2D.OverlapCircle(transform.position, 1f, pickupLayer);
                if (!pickup) return;
                var item = pickup.GetComponent<Pickup>();
                if (item.itemType == "Tool") return;
                item.PickupItem();

                if (item.itemType == "Weapon")
                {
                    if (activeWeapon != "") DropEquippedGear("Weapon", true);
                    gear.weapons[item.itemName].gameObject.SetActive(true);
                    activeWeapon = item.itemName;

                    var getGearParticle = Library.instance.particleDict["PickupFlash"].GetPooledGameObject();
                    getGearParticle.SetActive(true);
                    getGearParticle.transform.position = pickup.transform.position;
                    UpdateWeapon();

                }
                else if (item.itemType == "Consumable")
                {
                    if (activeTool != "") DropEquippedGear("Consumable", true);
                    gear.tools[item.itemName].gameObject.SetActive(true);
                    activeTool = item.itemName;

                    UpdateTool();

                    var getItemParticle = Library.instance.particleDict["PickupFlash"].GetPooledGameObject();
                    getItemParticle.SetActive(true);
                    getItemParticle.transform.position = pickup.transform.position;
                }
            }
        }
        public void GetToolOrConsumable(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var pickup = Physics2D.OverlapCircle(transform.position, 1f, pickupLayer);
                if (!pickup) return;
                var item = pickup.GetComponent<Pickup>();
                if (item.itemType == "Weapon") return;
                item.PickupItem();
                
                if (item.itemType == "Tool")
                {
                    if (activeTool != "") DropEquippedGear("Tool", false);
                    gear.tools[item.itemName].gameObject.SetActive(true);
                    activeTool = item.itemName;

                    UpdateTool();

                    var getGearParticle = Library.instance.particleDict["PickupFlash"].GetPooledGameObject();
                    getGearParticle.SetActive(true);
                    getGearParticle.transform.position = pickup.transform.position;
                } 
                else if (item.itemType == "Consumable")
                {
                    if (activeTool != "") DropEquippedGear("Consumable", false);
                    gear.tools[item.itemName].gameObject.SetActive(true);
                    activeTool = item.itemName;

                    UpdateTool();

                    var getItemParticle = Library.instance.particleDict["PickupFlash"].GetPooledGameObject();
                    getItemParticle.SetActive(true);
                    getItemParticle.transform.position = pickup.transform.position;
                }
            }
        }
        
        public void Attack(InputAction.CallbackContext context)
        {
            if (activeWeapon == "") return;
            if (context.started && _canAttack)
            {
                _weaponAnimator.SetBool(attackString, true);
                StartCoroutine(AttackCooldownTimer());
            }
        }

        public void Special(InputAction.CallbackContext context)
        {
            if (activeWeapon == "") return;
            if (context.started && _canSpecial)
            {
                _weaponAnimator.SetBool(specialString, true);
                StartCoroutine(SpecialCooldownTimer());
                print("Special");
            }
        }

        public void UseTool(InputAction.CallbackContext context)
        {
            if (context.started && _canTool)
            {
                _toolAnimator.SetBool(_useToolString, true);
                StartCoroutine(ToolCooldownTimer());
                print("Tool");
            }
        }

        private void UpdateWeapon()
        {
            attackString = gear.weapons[activeWeapon].rtso.attackAnimString;
            specialString = gear.weapons[activeWeapon].rtso.specAnimString;
            _weaponAnimator = gear.weapons[activeWeapon].animator;
            Debug.Log("got here");
            playerUi.UpdateItemsUi("Weapon", "Weapon", gear.weapons[activeWeapon].GetComponent<SpriteRenderer>().sprite);
        }

        private void UpdateTool()
        {
            _toolAnimator = gear.tools[activeTool].animator;
            _useToolString = gear.tools[activeTool].rtso.useToolString;
            playerUi.UpdateItemsUi("Tool", "Tool", gear.tools[activeTool].GetComponent<SpriteRenderer>().sprite);
            var craftable = CraftingMatrix.instance.GetRecipeFromMatrix(activeWeapon, activeTool);
            
        }

        private IEnumerator AttackCooldownTimer()
        {
            _canAttack = false;
            gear.weapons[activeWeapon].Attack();
            yield return new WaitForSeconds(attackCd);
            _canAttack = true;
        }

        private IEnumerator SpecialCooldownTimer()
        {
            _canSpecial = false;
            gear.weapons[activeWeapon].Special();
            yield return new WaitForSeconds(specialCd);
            _canSpecial = true;
        }

        private IEnumerator ToolCooldownTimer()
        {
            _canTool = false;
            gear.tools[activeTool].UseTool();
            yield return new WaitForSeconds(toolCd);
            _canTool = true;
        }

        private void DropEquippedGear(string itemType, bool weaponSlot)
        {
            switch (itemType)
            {
                case "Weapon":
                {
                    var weaponDrop = Library.instance.pickupsDict["P" + activeWeapon + "-0"];
                    gear.weapons[activeWeapon].gameObject.SetActive(false);
                    weaponDrop.gameObject.SetActive(true);
                    weaponDrop.transform.position = transform.position;
                    break;
                }
                case "Tool":
                {
                    var toolDrop = Library.instance.pickupsDict["P" + activeTool + "-0"];
                    gear.tools[activeTool].gameObject.SetActive(false);
                    toolDrop.gameObject.SetActive(true);
                    toolDrop.transform.position = transform.position;
                    break;
                }
                case "Consumable":
                    var slot = activeWeapon;
                    if (!weaponSlot) slot = activeTool;
                    
                    var consumableDrop = Library.instance.consumableDict["P" + slot + "-0"];
                    gear.weapons[activeWeapon].gameObject.SetActive(false);
                    consumableDrop.gameObject.SetActive(true);
                    consumableDrop.transform.position = transform.position;
                    break;
            }
        }
    }
}