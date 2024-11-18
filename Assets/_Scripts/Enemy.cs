using UnityEngine;

public class Enemy : MonoBehaviour
{
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }

    public void OnHit(Projectile projectile)
    {
        _healthSystem.TakeDamage(projectile.Damage);
    }
}
