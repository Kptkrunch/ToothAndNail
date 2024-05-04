using System.Collections;
using JAssets.Scripts_SC.UI;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class PlayerHealthController : MonoBehaviour
    {
        private const float InvulTime = 0.75f;
        private const float HitEffectBlendValue = 0.25f;
        private const float WaitTimeHitEffect = 0.1f;
        public Animator animator;
        public Material shader;
        public SpriteRenderer spriteRenderer;
        public PlayerUI playerUi;
        [SerializeField] public int currentHealth;
        [SerializeField] private int maxHealth = 5;
        [SerializeField] private GameObject parent;
        private bool _canBeDamaged = true;
        private float _invulTimer;
        private static readonly int HitEffectBlend = Shader.PropertyToID("_HitEffectBlend");

        private void Start()
        {
            SetAllHealthValues();
            playerUi.SetBarValues(maxHealth);
            SetShaderAndInvul();
        }

        private void FixedUpdate()
        {
            InvulnerableHandler();
        }

        public void GetDamaged(int damage)
        {
            Debug.Log("take damage: " + damage);
            if (!_canBeDamaged) return;
            currentHealth -= damage;
            if (damage > 0) _canBeDamaged = false;
            StartCoroutine(HitEffect());
            playerUi.TakeDamage(damage);

            if (currentHealth > 0) return;
            currentHealth = 0;
            animator.Play("Death");
            Destroy(parent, 3.0f);
        }

        public void GetHealed(int healAmount)
        {
            currentHealth += healAmount; 
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            playerUi.HealDamage(healAmount);
        }
        private void SetAllHealthValues()
        {
            currentHealth = maxHealth;
            playerUi.SetBarValues(maxHealth);
        }
        private void SetShaderAndInvul()
        {
            _invulTimer = InvulTime;
            var uniqueMaterial = new Material(shader);
            spriteRenderer.material = uniqueMaterial;
            shader.SetFloat(HitEffectBlend, 0.0f);
        }
        private IEnumerator HitEffect()
        {
            for (var i = 0; i < 2; i++)
            {
                spriteRenderer.material.SetFloat(HitEffectBlend, HitEffectBlendValue);
                yield return new WaitForSeconds(WaitTimeHitEffect);
                spriteRenderer.material.SetFloat(HitEffectBlend, 0.0f);
                yield return new WaitForSeconds(WaitTimeHitEffect);
            }
        }
        private void InvulnerableHandler()
        {
            if (!_canBeDamaged) _invulTimer -= Time.deltaTime;
            if (_invulTimer <= 0)
            {
                _canBeDamaged = true;
                _invulTimer = InvulTime;
            }
        }
    }
}