using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 2f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded = false;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        PlayerInput.Instance.OnPlayerJump += PlayerInput_OnPlayerJump;
    }

    private void PlayerInput_OnPlayerJump(object sender, System.EventArgs e)
    {
        if (!_isGrounded) return;

        _rb.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
        _isGrounded = false;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 input = PlayerInput.Instance.GetInputMovementVector();
        Vector3 moveDir = new Vector3(input.x, 0f, input.y);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}
