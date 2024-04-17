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
        public PlayerUI playerUi;
        public EquippedGear gear;
        public LayerMask pickupLayer;
        public string activeWeapon;
        public string activeTool;
        [CanBeNull] public string activeCraftable;

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
                    Debug.Log(item.itemName);
                    
                    if (activeWeapon != "") DropEquippedGear("Weapon");
                    gear.weapons[item.itemName].gameObject.SetActive(true);
                    activeWeapon = item.itemName;
                    UpdateWeapon(item.itemType);

                    var getGearParticle = Library.instance.particleDict["PickupFlash"].GetPooledGameObject();
                    getGearParticle.SetActive(true);
                    getGearParticle.transform.position = pickup.transform.position;
                }
                else if (item.itemType == "Consumable")
                {
                    Debug.Log("w consumable");
                    
                    if (activeTool != "") DropEquippedGear("Consumable");
                    Consumable consumable = gear.consumables[item.itemName].GetComponent<Consumable>();
                    activeWeapon = consumable.name;
                    UpdateWeapon("Consumable");

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
                Debug.Log(item.itemName);
                if (item.itemType == "Tool")
                {
                    if (activeTool != "") DropEquippedGear("Tool");
                    gear.tools[item.itemName].gameObject.SetActive(true);
                    activeTool = item.itemName;
                    UpdateTool(item.itemType);

                    var getGearParticle = Library.instance.particleDict["PickupFlash"].GetPooledGameObject();
                    getGearParticle.SetActive(true);
                    getGearParticle.transform.position = pickup.transform.position;
                } 
                else if (item.itemType == "Consumable")
                {
                    if (activeTool != "") DropEquippedGear("Consumable");
                    Consumable consumable = gear.consumables[item.itemName].GetComponent<Consumable>();
                    Debug.Log(consumable);
                    activeTool = consumable.name;
                    UpdateTool("Consumable");

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

        private void UpdateWeapon(string type)
        {
            if (type == "Weapon")
            {
                attackString = gear.weapons[activeWeapon].rtso.attackAnimString;
                specialString = gear.weapons[activeWeapon].rtso.specAnimString;
                _weaponAnimator = gear.weapons[activeWeapon].animator;
            }
            else
            {
                attackString = "";
                specialString = "";
                _weaponAnimator = null;
            }
            Debug.Log("got this far");
            UpdateUI(type, "Weapon");
        }

        private void UpdateTool(string type)
        {
            if (type == "Tool")
            {
                _toolAnimator = gear.tools[activeTool].animator;
                _useToolString = gear.tools[activeTool].rtso.useToolString;
            } 
            else
            {
                _useToolString = "";
	            _toolAnimator = null;
            }

            UpdateUI(type, "Tool");
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

        private void DropEquippedGear(string itemType)
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

                    var slot = "";
                    if (itemType == "Weapon")
                    {
                        slot = activeWeapon;
                        gear.weapons[slot].gameObject.SetActive(false);
                    }
                    else
                    {
                        slot = activeTool;
                        gear.tools[slot].gameObject.SetActive(false);
                    }
                    
                    var consumableDrop = Library.instance.consumableDict["P" + slot + "-0"];
                    consumableDrop.gameObject.SetActive(true);
                    consumableDrop.transform.position = transform.position;
                    break;
            }
        }

        private void UpdateUI(string type, string slot)
        {
            if (type == "Weapon" && gear.weapons.ContainsKey(activeWeapon))
            {
	            playerUi.UpdateItemsUi(type, slot, gear.weapons[activeWeapon].GetComponent<SpriteRenderer>().sprite);
            }
            else if (type == "Tool" && gear.tools.ContainsKey(activeTool))
            {
	            playerUi.UpdateItemsUi(type, slot, gear.tools[activeTool].GetComponent<SpriteRenderer>().sprite);
            } else if (type == "Consumable" && slot == "Weapon")
            {
                playerUi.UpdateItemsUi(type, slot, gear.consumables[activeWeapon].GetComponent<Consumable>().sprite);
                Debug.Log("here");
            } else if (type == "Consumable" && slot == "Tool")
            {
                playerUi.UpdateItemsUi(type, slot, gear.consumables[activeTool].GetComponent<Consumable>().sprite);
            }
            
            var craftable = CraftingMatrix.instance.GetRecipeFromMatrix(activeWeapon, activeTool);
            Debug.Log(craftable);
            if (craftable != null)
            {
	            playerUi.UpdateCraftable(craftable.sprite);
            } else
            {
                return;
            }
            activeCraftable = craftable.name;
            Debug.Log(craftable);
            Debug.Log("active craftable");
        }

        public void CraftItem(InputAction.CallbackContext context)
        {
            if (string.IsNullOrEmpty(activeCraftable)) return;
            var newItem = Library.instance.pickupsDict["P" + activeCraftable].GetPooledGameObject();
            Debug.Log(newItem);
            Debug.Log("got past library");
            if (newItem != null)
            {
	            newItem.transform.position = transform.position;
                newItem.SetActive(true);
                
                activeWeapon = "";
                activeTool = "";
                activeCraftable = "";
                
                playerUi.UpdateItemsUi("All", "All", null);
            }
        }
    }
}