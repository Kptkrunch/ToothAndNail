using JAssets.Scripts_SC.Spawners;
using Photon.Pun;
using UnityEngine;

namespace JAssets.Scripts_SC.Items.Weapons
{
    public class BattleAx : Weapon
    {
        private bool _specialTrigger;

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

        public void MegaChopOff()
        {
            animator.SetBool("MegaChop", false);
        }

        public override void Attack()
        {
            if (!_specialTrigger)
            {
                animator.SetBool(attackAnimString, true);
                animator.SetBool(specAnimString, false);
            }
            else if (_specialTrigger)
            {
                animator.SetBool("MegaChop", true);
                _specialTrigger = false;
            }
        }

        public override void Special()
        {
            if (!_specialTrigger) animator.SetBool(specAnimString, true);
        }
    }
}