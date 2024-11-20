using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _destroyTime = 3f;
    [SerializeField] private float _damage = 3f;

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, _destroyTime);
    }

    public void SetDirection(float direction)
    {
        // Adjust the velocity based on the direction
        _rb.linearVelocity = new Vector2(direction * _speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Notify the target it has been hit
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                enemy.OnHit(this); // Pass the projectile as a parameter
            }
        }

        Destroy(gameObject); // Destroy projectile on impact
    }

}
