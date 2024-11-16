using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    void Update()
    {

    }
}
