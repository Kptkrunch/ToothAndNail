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
            weaponDamage = rtso.damage;
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerMoveController>().isPlayer && other.CompareTag(playerController.tag) == false)
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
            rtso.damage = 1;
        }

        public override void AttackOff()
        {
            animator.SetBool(rtso.attackAnimString, false);
            rtso.damage = 1;
            if (rtso.durability <= 0)
            {
                gameObject.SetActive(false);
                var particle = WorldParticleSpawner.instance.weaponsBreakParticle.GetPooledGameObject();
                particle.SetActive(true);
                particle.gameObject.transform.position = transform.position;
            }
        }

        public override void Special()
        {
            if (!_specialTrigger)
            {
                animator.SetBool(rtso.attackAnimString, false);
                animator.SetBool(rtso.specAnimString, true);
                rtso.damage = 0;
            }
        }
    }
}