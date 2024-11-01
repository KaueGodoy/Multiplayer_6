using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    public event EventHandler OnPlayerJump;

    private InputSystem_Actions _playerInputSystem;

    private void Awake()
    {
        Instance = this;

        _playerInputSystem = new InputSystem_Actions();
        _playerInputSystem.Enable();

        _playerInputSystem.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerJump?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputMovementVector()
    {
        Vector2 inputVector = _playerInputSystem.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    private void OnDestroy()
    {
        _playerInputSystem.Player.Jump.performed -= Jump_performed;
        _playerInputSystem.Dispose();
    }
}
