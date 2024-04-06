using System.Collections;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public Material shader;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private int _maxHealth = 100;

    private const float InvulTime = 0.75f;
    private const float HitEffectBlendValue = 0.25f;
    private const float WaitTimeHitEffect = 0.1f;

    private int _currentHealth;
    public int Health
    {
        get => _currentHealth;
        set
        {
            // clamp the value between 0 and maxHealth
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth); 
            
            if (_currentHealth <= 0)
            {
                animator.Play("Death");
                Destroy(player, 1);
            }
        }
    }

    private float _invulTimer;
    private bool _canBeDamaged = true;

    private void Start()
    {
        Health = _maxHealth;
        _invulTimer = InvulTime;
        var uniqueMaterial = new Material(shader);
        spriteRenderer.material = uniqueMaterial;
        shader.SetFloat("_HitEffectBlend", 0.0f);
    }

    private void FixedUpdate()
    {
        if (!_canBeDamaged) _invulTimer -= Time.deltaTime;
        if (_invulTimer <= 0)
        {
            _canBeDamaged = true;
            _invulTimer = InvulTime;
        }
    }

    public void GetDamaged(int damage)
    {
        if (!_canBeDamaged) return;
        Health -= damage; // Using property to update health and check for death
        if (damage > 0) _canBeDamaged = false;
        StartCoroutine(HitEffect());
    }

    public void GetHealed(int healAmount)
    {
        Health += healAmount; // Using property to prevent exceeding max health
    }

    private IEnumerator HitEffect()
    {
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.material.SetFloat("_HitEffectBlend", HitEffectBlendValue);
            yield return new WaitForSeconds(WaitTimeHitEffect);
            spriteRenderer.material.SetFloat("_HitEffectBlend", 0.0f);
            yield return new WaitForSeconds(WaitTimeHitEffect);
        }
    }
}