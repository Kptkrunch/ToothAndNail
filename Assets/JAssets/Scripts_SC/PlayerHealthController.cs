using System.Collections;
using JAssets.Scripts_SC.UI;
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
        [SerializeField] private int currentHealth;
        [SerializeField] private int _maxHealth = 5;
        private bool _canBeDamaged = true;
        private float _invulTimer;
        private static readonly int HitEffectBlend = Shader.PropertyToID("_HitEffectBlend");


        private void Start()
        {
            SetAllHealthValues();
            SetShaderAndInvul();
        }

        private void FixedUpdate()
        {
            InvulnerableHandler();
        }

        public void GetDamaged(int damage)
        {
            if (!_canBeDamaged) return;
            currentHealth -= damage; // Using property to update health and check for death
            if (damage > 0) _canBeDamaged = false;
            StartCoroutine(HitEffect());
            playerUi.TakeDamage(damage);

            if (currentHealth > 0) return;
            currentHealth = 0;
            animator.Play("Death");
        }

        public void GetHealed(int healAmount)
        {
            currentHealth += healAmount; // Using property to prevent exceeding max health
            if (currentHealth > _maxHealth) currentHealth = _maxHealth;
            playerUi.HealDamage(healAmount);
        }
        private void SetAllHealthValues()
        {
            currentHealth = _maxHealth;
            playerUi.SetBarValues(_maxHealth);
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