using _Scripts;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Inputs _inputs;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Mouse _mouse;

    private Vector2 JumpVector => _inputs.Player.Jump.ReadValue<Vector2>();
    public bool IsHoldingJump => JumpVector.y >= 1;
    
    public event Action OnReplay;
    
    void Start()
    {
        _playerMovement = _playerMovement ?? GetComponent<PlayerMovement>();
        _inputs = _inputs ?? new Inputs();
        _inputs.Enable();
        //_inputs.Player.Attack.performed += _ => _playerController.Shoot(); <-- Left click
        _inputs.Player.Jump.performed += _ => _playerMovement.Jump(JumpVector);
        _inputs.Player.Replay.performed += _ => OnReplay?.Invoke();
    }

    private void OnEnable()
    {
        _inputs = _inputs ?? new Inputs();
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        _playerMovement.Move(_inputs.Player.Move.ReadValue<Vector2>());
    }

    private void Jump()
    {
        _playerMovement.Jump(_inputs.Player.Jump.ReadValue<Vector2>());
    }
}
