using JAssets.Scripts_SC.Spawners;
using UnityEngine;

namespace JAssets.Scripts_SC.Items
{
    public class Tool : MonoBehaviour
    {
        public GearController controller;
        public Tool_SO tool_so;
        internal Tool_SO rtso;

        private void Start()
        {
            rtso = Instantiate(tool_so);
        }

        public virtual void UseTool()
        {
            rtso.uses--;
            controller.activeToolUses = rtso.uses;

            print(rtso.uses);
        }

        private void RemainingUsesCheck()
        {
            if (rtso.uses <= 0)
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