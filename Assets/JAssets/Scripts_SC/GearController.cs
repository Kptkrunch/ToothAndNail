using System.Collections;
using JAssets.Scripts_SC.Items;
using JAssets.Scripts_SC.Spawners;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
    public class GearController : MonoBehaviour
    {
        public EquippedGear gear;
        public LayerMask pickupLayer;
        public string activeWeapon;
        public string activeTool;

        public float attackCd;
        public float specialCd;
        public float toolCd;

        public int activeWeaponDurability;
        public int activeToolUses;
        private string _attackString;

        private bool _canAttack;
        private bool _canSpecial;
        private bool _canTool;
        private string _specialString;
        private Animator _toolAnimator;
        private string _useToolString;
        private Animator _weaponAnimator;

        private void Start()
        {
            _canAttack = true;
            _canSpecial = true;
            _canTool = true;
        }

        public void GetGear(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var gearPickup = Physics2D.OverlapCircle(transform.position, 1f, pickupLayer);
                if (!gearPickup) return;
                var newGear = gearPickup.GetComponent<Pickup>();
                newGear.PickupGear();

                if (newGear.isWeapon)
                {
                    if (activeWeapon != "") DropEquippedGear(true);
                    gear.weapons[newGear.gearName].gameObject.SetActive(true);
                    activeWeapon = newGear.gearName;

                    UpdateWeapon();

                    var getGearParticle = WorldParticleSpawner.instance.weaponPickupParticle.GetPooledGameObject();
                    getGearParticle.SetActive(true);
                    getGearParticle.transform.position = gearPickup.transform.position;
                }
                else if (newGear.isTool)
                {
                    if (activeTool != "") DropEquippedGear(false);
                    gear.tools[newGear.gearName].gameObject.SetActive(true);
                    activeTool = newGear.gearName;

                    UpdateTool();

                    var getGearParticle = WorldParticleSpawner.instance.weaponPickupParticle.GetPooledGameObject();
                    getGearParticle.SetActive(true);
                    getGearParticle.transform.position = gearPickup.transform.position;
                }
            }
        }

        public void Attack(InputAction.CallbackContext context)
        {
            if (activeWeapon == "") return;
            if (context.started && _canAttack)
            {
                _weaponAnimator.SetBool(_attackString, true);
                StartCoroutine(AttackCooldownTimer());
            }
        }

        public void Special(InputAction.CallbackContext context)
        {
            if (activeWeapon == "") return;
            if (context.started && _canSpecial)
            {
                _weaponAnimator.SetBool(_specialString, true);
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
            _weaponAnimator = gear.weapons[activeWeapon].rtso.animator;
            _attackString = gear.weapons[activeWeapon].rtso.attackAnimString;
            _specialString = gear.weapons[activeWeapon].rtso.specAnimString;
        }

        private void UpdateTool()
        {
            _toolAnimator = gear.tools[activeTool].rtso.animator;
            _attackString = gear.tools[activeTool].rtso.useToolString;
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

        private void DropEquippedGear(bool isWeapon)
        {
            switch (isWeapon)
            {
                case true:
                {
                    var weaponDrop = Library.instance.pickupsDict["P" + activeWeapon + "-0"];
                    gear.weapons[activeWeapon].gameObject.SetActive(false);
                    weaponDrop.gameObject.SetActive(true);
                    weaponDrop.transform.position = transform.position;
                    break;
                }
                case false:
                {
                    var toolDrop = Library.instance.pickupsDict["P" + activeTool + "-0"];
                    gear.tools[activeTool].gameObject.SetActive(false);
                    toolDrop.gameObject.SetActive(true);
                    toolDrop.transform.position = transform.position;
                    break;
                }
            }
        }
    }
}