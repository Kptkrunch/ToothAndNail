using System.Collections;
using JAssets.Scripts_SC.Items;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
    public sealed class AttackController : MonoBehaviour
    {
        [SerializeField] [CanBeNull] public Animator weaponAnimator;
        [SerializeField] public EquippedGear gear;
    
        [SerializeField] public string itemSlotA;
        [SerializeField] public string itemSlotB;
    
        [SerializeField] public float attackCd;
        [SerializeField] public float specialCd;
        [SerializeField] public bool canAttack;
        [SerializeField] public bool canSpecial;
        [SerializeField] public string attackString;
        [SerializeField] public string specString;
        [SerializeField] public string weaponHand;

        private void Start()
        {
            itemSlotA = "";
            itemSlotB = "";
            canAttack = true;
            canSpecial = true;
        }
    
        public void Attack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (gear.Items != null) gear.Items[weaponHand].GetComponentInChildren<Weapon>().Attack();
            if (weaponAnimator != null) weaponAnimator.SetBool(attackString, true);
            StartCoroutine(AttackCooldownTimer());
        }
        public void Special(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            gear.Items[weaponHand].GetComponentInChildren<Weapon>().Special();
            if (weaponAnimator != null) weaponAnimator.SetBool(specString, true);
            StartCoroutine(SpecialCooldownTimer());
        }

        private IEnumerator AttackCooldownTimer()
        {
            canAttack = false;
            gear.Items[weaponHand].GetComponentInChildren<Weapon>().Attack();
            yield return new WaitForSeconds(attackCd);
            canAttack = true;
        }

        private IEnumerator SpecialCooldownTimer()
        {
            canSpecial = false;
            gear.Items[weaponHand].GetComponentInChildren<Weapon>().Special();
            yield return new WaitForSeconds(specialCd);
            canSpecial = true;
        }

        public void UpdateGearStats(string slotToUpdate, string newItem)
        {
            var weapon = gear.Items[newItem].GetComponentInChildren<Weapon>();

            switch (slotToUpdate)
            {
                case "A":
                    itemSlotA = newItem;
                    if (weapon) weaponHand = "itemSlotA";
                    break;
                case "B":
                    itemSlotB = newItem;
                    if (weapon) weaponHand = "itemSlotB";
                    break;
            }
            gear.Items[newItem].gameObject.SetActive(true);
        
            if (weapon == null) return;
            gear.Items[newItem].SetActive(true);
            weaponHand = weapon.name;
            weaponAnimator = gear.Items[newItem].GetComponent<Animator>();
            attackString = weapon.rtso.attackAnimString;
            specString = weapon.rtso.specAnimString;
            attackCd = weapon.rtso.attackCd;
            specialCd = weapon.rtso.specialCd;
        }
    }
}
