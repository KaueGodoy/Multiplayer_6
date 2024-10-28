using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem_Actions playerControls; // Your generated input actions class
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isJumping;
    private bool isSprinting;
    private bool isCrouching;
    private bool isAttacking;

    [SerializeField] private float moveSpeed = 5f; // Adjust the movement speed in the inspector
    private Rigidbody rb;

    private void Awake()
    {
        // Initialize the input actions
        playerControls = new InputSystem_Actions();

        // Assign callback functions for the actions
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        playerControls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        playerControls.Player.Jump.performed += ctx => isJumping = true;
        playerControls.Player.Jump.canceled += ctx => isJumping = false;

        playerControls.Player.Sprint.performed += ctx => isSprinting = true;
        playerControls.Player.Sprint.canceled += ctx => isSprinting = false;

        playerControls.Player.Crouch.performed += ctx => isCrouching = true;
        playerControls.Player.Crouch.canceled += ctx => isCrouching = false;

        playerControls.Player.Attack.performed += ctx => isAttacking = true;
        playerControls.Player.Attack.canceled += ctx => isAttacking = false;

        // Initialize the Rigidbody
        rb = GetComponent<Rigidbody>();

        // Make sure Rigidbody is set to not use gravity if this is a top-down game
        rb.useGravity = false;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        // Process movement
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        Move(movement);

        // Process looking
        Vector2 lookDirection = new Vector2(lookInput.x, lookInput.y);
        Look(lookDirection);

        // Check for actions
        if (isJumping)
        {
            Jump();
        }

        if (isSprinting)
        {
            Sprint();
        }

        if (isCrouching)
        {
            Crouch();
        }

        if (isAttacking)
        {
            Attack();
        }
    }

    private void Move(Vector3 direction)
    {
        // Ensure we normalize the direction to maintain consistent movement speed
        direction = direction.normalized;

        // Set the velocity directly for smooth movement
        rb.linearVelocity = new Vector3(direction.x * moveSpeed, rb.linearVelocity.y, direction.z * moveSpeed);

        Debug.Log("Moving in direction: " + direction);
    }

    private void Look(Vector2 direction)
    {
        // Implement look/aiming code here (e.g., rotating the camera or character)
        Debug.Log("Looking in direction: " + direction);
    }

    private void Jump()
    {
        // Implement jumping logic here
        Debug.Log("Jumping");
    }

    private void Sprint()
    {
        // Implement sprinting logic here
        Debug.Log("Sprinting");
    }

    private void Crouch()
    {
        // Implement crouch logic here
        Debug.Log("Crouching");
    }

    private void Attack()
    {
        // Implement attack logic here
        Debug.Log("Attacking");
    }
}
