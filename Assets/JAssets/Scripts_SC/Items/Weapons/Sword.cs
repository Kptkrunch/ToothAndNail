using JAssets.Scripts_SC.Spawners;
using Photon.Pun;
using UnityEngine;

namespace JAssets.Scripts_SC.Items.Weapons
{
    public class Sword : Weapon
    {
        private bool _parryTrigger;
        private bool _specialTrigger;
        private int weaponDamage;

        private void Start()
        {
            weaponDamage = damage;
        }

        public override void OnCollisionEnter2D(Collision2D other)
        {
            var thisId = gameObject.GetComponentInParent<PhotonView>().ViewID;
            if (thisId == other.gameObject.GetComponentInParent<PhotonView>().ViewID) return;
            if (other.gameObject.CompareTag("Player"))
            {
                DealDamageAndSpawnDmgText(other);
                HitParticleSpawner.instance.GetRandomGoo(other.transform.position);
            }
            else if (other.gameObject.CompareTag("Weapon"))
            {
                DealDamageAndSpawnDmgText(other);
                HitParticleSpawner.instance.GetRandomClash(other.transform.position);
            }
        }

        public void WindowOpen()
        {
            _specialTrigger = true;
        }

        public void WindowClosed()
        {
            _specialTrigger = false;
        }

        public override void Attack()
        {
            damage = 1;
        }

        public override void AttackOff()
        {
            animator.SetBool(attackAnimString, false);
            damage = 1;
            if (durability <= 0)
            {
                gameObject.SetActive(false);
                controller.activeWeapon = "";
                var particle = WorldParticleSpawner.instance.weaponsBreakParticle.GetPooledGameObject();
                particle.SetActive(true);
                particle.gameObject.transform.position = transform.position;
            }
        }

        public override void Special()
        {
            if (!_specialTrigger)
            {
                animator.SetBool(attackAnimString, false);
                animator.SetBool(specAnimString, true);
                damage = 0;
            }
        }
    }
}