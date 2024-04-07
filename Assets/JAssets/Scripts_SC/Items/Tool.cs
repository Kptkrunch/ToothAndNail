using JAssets.Scripts_SC.Spawners;
using JetBrains.Annotations;
using UnityEngine;

namespace JAssets.Scripts_SC.Items
{
    public class Tool : MonoBehaviour
    {
        public GearController controller;
        public float weight;

        public int uses, fullUses;
        public string useToolString;
        public float toolCd;

        [CanBeNull] public Animator animator;

        public virtual void UseTool()
        {
            uses--;
            controller.activeToolUses = uses;

            print(uses);
        }

        private void RemainingUsesCheck()
        {
            if (uses <= 0)
            {
                gameObject.SetActive(false);
                controller.activeTool = "";
                var particle = WorldParticleSpawner.instance.toolBreakParticle.GetPooledGameObject();
                particle.SetActive(true);
                particle.transform.position = transform.position;
            }
        }
    }
}