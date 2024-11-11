using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _boostedSpeed = 5f; // Speed when boosted
    [SerializeField] private float _boostDuration = 2f; // Duration of the speed boost
    private bool _isBoosted = false; // To track if the player is currently boosted

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded = false;

    [Header("Ground")]
    [SerializeField] private float groundDistance = 0.1f; // Distance to check for ground
    [SerializeField] private LayerMask groundLayer; // 
    [Header("Shooting")]
    [SerializeField] private Transform _projectileTransform;
    [SerializeField] private GameObject _bulletPrefab;

    private Rigidbody2D _rb;
    private int facingDirection = 1; // 1 = facing right, -1 = facing left

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        PlayerInput.Instance.OnPlayerJump += PlayerInput_OnPlayerJump;
        PlayerInput.Instance.OnPlayerSkill += PlayerInput_OnPlayerSkill;
        PlayerInput.Instance.OnPlayerUlt += PlayerInput_OnPlayerUlt;
    }

    private void PlayerInput_OnPlayerUlt(object sender, System.EventArgs e)
    {
        Instantiate(_bulletPrefab, _projectileTransform.position, _projectileTransform.rotation);
    }

    private void PlayerInput_OnPlayerSkill(object sender, System.EventArgs e)
    {
        if (_isBoosted) return;

        StartCoroutine(SpeedBoost());
    }

    private void PlayerInput_OnPlayerJump(object sender, System.EventArgs e)
    {
        if (!_isGrounded) return;

        _rb.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
        _isGrounded = false;
    }

    void Update()
    {
        CheckIfGrounded();
        Move();
    }

    private IEnumerator SpeedBoost()
    {
        _isBoosted = true;
        yield return new WaitForSeconds(_boostDuration);
        _isBoosted = false;
    }

    private void Move()
    {
        Vector2 input = PlayerInput.Instance.GetInputMovementVector();
        float currentSpeed = _isBoosted ? _boostedSpeed : _moveSpeed; // Use boosted speed if active
        Vector2 moveDirection = new Vector2(input.x * currentSpeed, _rb.linearVelocity.y);

        _rb.linearVelocity = moveDirection;

        // Update facing direction based on input
        if (input.x > 0 && facingDirection != 1)
        {
            facingDirection = 1;
            FlipCharacter();
        }
        else if (input.x < 0 && facingDirection != -1)
        {
            facingDirection = -1;
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        // Flip character by inverting the X scale
        Vector3 scale = transform.localScale;
        scale.x = facingDirection; // Set scale.x based on facing direction (1 for right, -1 for left)
        transform.localScale = scale;
    }


    private void CheckIfGrounded()
    {
        // Cast a ray downward from the character's position to check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);

        // Set _isGrounded to true if the ray hits a ground object
        _isGrounded = hit.collider != null;
    }

    // Optional: For visualization in the editor
    private void OnDrawGizmos()
    {
        // Draw the ground detection ray in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDistance);
    }
}
