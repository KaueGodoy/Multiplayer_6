using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    public event EventHandler OnPlayerJump;
    public event EventHandler OnPlayerSkill;

    private InputSystem_Actions _playerInputSystem;

    private void Awake()
    {
        Instance = this;

        _playerInputSystem = new InputSystem_Actions();
        _playerInputSystem.Enable();

        _playerInputSystem.Player.Jump.performed += Jump_performed;
        _playerInputSystem.Player.Skill.performed += Skill_performed;
    }

    private void Skill_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerSkill?.Invoke(this, EventArgs.Empty);
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
