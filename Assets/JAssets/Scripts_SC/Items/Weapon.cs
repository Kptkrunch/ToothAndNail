using JAssets.Scripts_SC.Spawners;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC.Items
{
    public class Weapon : MonoBehaviour
    {
        public Weapon_SO weapon_so;
        internal Weapon_SO rtso;
        public GearController controller;
        public Collider2D hitBox;

        private void Start()
        {
            rtso = Instantiate(weapon_so);
        }

        public virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                DealDamageAndSpawnDmgText(other);
                HitParticleSpawner.instance.GetRandomBlood(other.transform.position);
                controller.activeWeaponDurability = rtso.durability;
            }
            else if (other.gameObject.CompareTag("Weapon"))
            {
                DealDamageAndSpawnDmgText(other);
                HitParticleSpawner.instance.GetRandomClash(other.transform.position);
                controller.activeWeaponDurability = rtso.durability;
            }
        }

        public virtual void AttackOff()
        {
            rtso.animator.SetBool(rtso.attackAnimString, false);
            if (rtso.durability <= 0)
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
            rtso.animator.SetBool(rtso.specAnimString, false);
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
                Debug.Log("dur");
                rtso.durability = rtso.fullDurability;
                Debug.Log(rtso.durability);
                gameObject.SetActive(false);
                controller.activeWeapon = "";
                var particle = WorldParticleSpawner.instance.weaponPickupParticle.GetPooledGameObject();
                particle.SetActive(true);
                particle.transform.position = transform.position;
            }
        }
    }
}