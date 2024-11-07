using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Public variable for setting the bullet speed
    public float speed = 20f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the bullet
        rb = GetComponent<Rigidbody>();

        // Set the velocity to move forward in the bullet's local forward direction
        rb.linearVelocity = transform.forward * speed;
    }
}
