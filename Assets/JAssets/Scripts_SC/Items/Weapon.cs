using System;
using JAssets.Scripts_SC.SOScripts;
using JAssets.Scripts_SC.Spawners;
using JetBrains.Annotations;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace JAssets.Scripts_SC.Items
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected PlayerMoveController playerController;
        public Animator animator;
        public Weapon_SO weaponSo;
        internal Weapon_SO rtso;
        public ItemController controller;
        [CanBeNull] public Collider2D hitBox;
        private void OnEnable()
        {
            rtso = Instantiate(weaponSo);
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerMoveController>().isPlayer && other.CompareTag(playerController.tag) == false)
            {
                DealDamageAndSpawnDmgText(other);
                ApplyKnockback(other);
                Debug.Log("after knockback");
                HitParticleSpawner.instance.GetRandomBlood(other.transform.position);
            }
            else if (other.gameObject.CompareTag("Weapon"))
            {
                DealDamageAndSpawnDmgText(other);
                HitParticleSpawner.instance.GetRandomClash(other.transform.position);
            }
        }

        public virtual void AttackOff()
        {
            animator.SetBool(rtso.attackAnimString, false);
            if (rtso.durability <= 0)
            {
                gameObject.SetActive(false);
                
                var particle = WorldParticleSpawner.instance.weaponsBreakParticle.GetPooledGameObject();
                particle.SetActive(true);
                particle.gameObject.transform.position = transform.position;
            }
        }

        public virtual void SpecialOff()
        {
            animator.SetBool(rtso.specAnimString, false);
        }

        public virtual void Attack()
        {
            print("Attack");
        }

        public virtual void Special()
        {
            print("Special");
        }

        protected void DealDamageAndSpawnDmgText(Collider2D other)
        {
            rtso.durability--;
            var otherPlayer = other.gameObject.GetComponentInChildren<PlayerHealthController>();
            otherPlayer.GetDamaged(rtso.damage);

            var dmgText = DamageNumberController.instance.player.GetFeedbackOfType<MMF_FloatingText>();
            dmgText.Value = rtso.damage.ToString();
            DamageNumberController.instance.player.PlayFeedbacks(otherPlayer.transform.position);
            DurabilityCheck();
        }


        public virtual void DurabilityCheck()
        {
            if (rtso.durability <= 0)
            {
                rtso.durability = rtso.fullDurability;
                
                gameObject.SetActive(false);
                
                var particle = WorldParticleSpawner.instance.weaponPickupParticle.GetPooledGameObject();
                particle.SetActive(true);
                particle.transform.position = transform.position;
            }
        }
        
        private void ApplyKnockback(Collider2D other)
        {
            Debug.Log(other.name);
            // Calculate the Knockback direction (from enemy to player)
            Vector2 knockbackDirection = (this.transform.position - other.transform.position).normalized;

            // Define the Knockback strength
            float knockbackStrength = 1000f;

            // Get the player's Rigidbody2D
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            Debug.Log(playerRigidbody);
            if (!playerRigidbody) return;
            // Apply the knockback force to the player's Rigidbody
            playerRigidbody.AddForce(knockbackDirection * knockbackStrength, ForceMode2D.Impulse);
        }
    }
}