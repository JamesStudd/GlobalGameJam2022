using _Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Inputs _inputs;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Mouse _mouse;
    void Start()
    {
        _playerController = _playerController ?? GetComponent<PlayerController>();
        _inputs = _inputs ?? new Inputs();
        _inputs.Enable();
        //_inputs.Player.Attack.performed += _ => _playerController.Shoot(); <-- Left click
        //_inputs.Player.Jump.performed += _ => _playerController.Jump();
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
        Jump();
    }

    private void Move()
    {
        _playerController.Move(_inputs.Player.Move.ReadValue<Vector2>());
    }

    private void Jump()
    {
        _playerController.Jump(_inputs.Player.Jump.ReadValue<int>());
    }
}
