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
    private Vector2 MoveVector => FeatureLocker.PlayerInputEnabled ? _inputs.Player.Move.ReadValue<Vector2>() : Vector2.zero;
    
    public bool IsHoldingJump => JumpVector.y >= 1 && FeatureLocker.PlayerInputEnabled;
    
    public event Action OnReplay;
    
    void Start()
    {
        _playerMovement = _playerMovement ?? GetComponent<PlayerMovement>();
        _inputs = _inputs ?? new Inputs();
        _inputs.Enable();
        //_inputs.Player.Attack.performed += _ => Test();
        _inputs.Player.Jump.performed += _ =>
        {
            if (FeatureLocker.PlayerInputEnabled)
            {
                _playerMovement.Jump(JumpVector);
            }
        };
        _inputs.Player.Replay.performed += _ =>
        {
            if (FeatureLocker.ReplayingEnabled && FeatureLocker.PlayerInputEnabled)
            {
                if (FindObjectOfType<LevelController>().CanSpawnAgain)
                {
                    OnReplay?.Invoke();
                    enabled = false;    
                }
                else
                {
                    GameEvents.GameEnd(false);
                }
            }
        };
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
        _playerMovement.Move(MoveVector);
    }
}
