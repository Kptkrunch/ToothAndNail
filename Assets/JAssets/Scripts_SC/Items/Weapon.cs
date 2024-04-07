using JAssets.Scripts_SC.Spawners;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace JAssets.Scripts_SC.Items
{
    public class Weapon : MonoBehaviour
    {
        public GearController controller;
        public float weight;
        public int damage;
        public int durability, fullDurability = 3;

        public Collider2D hitBox;

        public Animator animator;
        public string attackAnimString;
        public string specAnimString;

        public float attackCd;
        public float specialCd;

        public virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                DealDamageAndSpawnDmgText(other);
                HitParticleSpawner.instance.GetRandomBlood(other.transform.position);
                controller.activeWeaponDurability = durability;
            }
            else if (other.gameObject.CompareTag("Weapon"))
            {
                DealDamageAndSpawnDmgText(other);
                HitParticleSpawner.instance.GetRandomClash(other.transform.position);
                controller.activeWeaponDurability = durability;
            }
        }

        public virtual void AttackOff()
        {
            animator.SetBool(attackAnimString, false);
            if (durability <= 0)
            {
                gameObject.SetActive(false);
                controller.activeWeapon = "";
                var particle = WorldParticleSpawner.instance.weaponsBreakParticle.GetPooledGameObject();
                particle.SetActive(true);
                particle.gameObject.transform.position = transform.position;
            }
        }

        public virtual void SpecialOff()
        {
            animator.SetBool(specAnimString, false);
        }

        public virtual void Attack()
        {
            print("Attack");
        }

        public virtual void Special()
        {
            print("Special");
        }

        public void DealDamageAndSpawnDmgText(Collision2D other)
        {
            durability--;
            var otherPlayer = other.gameObject.GetComponentInChildren<PlayerHealthController>();
            otherPlayer.GetDamaged(damage);

            var dmgText = DamageTextController.instance.player.GetFeedbackOfType<MMF_FloatingText>();
            dmgText.Value = damage.ToString();
            DamageTextController.instance.player.PlayFeedbacks(otherPlayer.transform.position);
        }

        public virtual void DurabilityCheck()
        {
            if (durability <= 0)
            {
                Debug.Log("dur");
                durability = fullDurability;
                Debug.Log(durability);
                gameObject.SetActive(false);
                controller.activeWeapon = "";
                var particle = WorldParticleSpawner.instance.weaponPickupParticle.GetPooledGameObject();
                particle.SetActive(true);
                particle.transform.position = transform.position;
            }
        }
    }
}