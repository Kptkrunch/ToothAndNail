using JAssets.Scripts_SC.SOScripts;
using JAssets.Scripts_SC.Spawners;
using JetBrains.Annotations;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace JAssets.Scripts_SC.Items
{
    public class Weapon : MonoBehaviour
    {
        
        public Animator animator;
        public Weapon_SO weaponSo;
        internal Weapon_SO rtso;
        public ItemController controller;
        [CanBeNull] public Collider2D hitBox;

        private void OnEnable()
        {
            rtso = Instantiate(weaponSo);
        }

        public virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                DealDamageAndSpawnDmgText(other);
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

        protected void DealDamageAndSpawnDmgText(Collision2D other)
        {
            rtso.durability--;
            var otherPlayer = other.gameObject.GetComponentInChildren<PlayerHealthController>();
            otherPlayer.GetDamaged(rtso.damage);

            var dmgText = DamageTextController.instance.player.GetFeedbackOfType<MMF_FloatingText>();
            dmgText.Value = rtso.damage.ToString();
            DamageTextController.instance.player.PlayFeedbacks(otherPlayer.transform.position);
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
    }
}