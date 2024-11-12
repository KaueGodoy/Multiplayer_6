using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(float direction)
    {
        // Adjust the velocity based on the direction
        _rb.linearVelocity = new Vector2(direction * _speed, 0);
    }
}
