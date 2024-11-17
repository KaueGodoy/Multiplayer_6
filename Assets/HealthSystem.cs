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
        // Example debug to test taking damage (remove in production)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
        }
    }

    /// <summary>
    /// Reduces the current health by a specified amount.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to take.</param>
    public void TakeDamage(float damageAmount)
    {
        if (damageAmount < 0)
        {
            Debug.LogWarning("Damage amount cannot be negative!");
            return;
        }

        CurrentHealth -= damageAmount;

        // Clamp health to prevent it from going below zero
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        Debug.Log($"Took {damageAmount} damage. Current health: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles the logic when health reaches zero.
    /// </summary>
    private void Die()
    {
        Debug.Log("The character has died!");
        // Implement additional death logic here, such as triggering animations or game over state
    }
}
