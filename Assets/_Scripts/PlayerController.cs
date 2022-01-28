﻿using UnityEngine;

namespace _Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _airMovementModifier = 0.1f;
        [SerializeField] private float _speedLimit = 10f;
        
        [SerializeField] private float _fallMultiplier = 10f;
        [SerializeField] private float _lowJumpMultiplier = 10f;
        
        [SerializeField] private float _distToFloorMultiplier = 2.5f;

        private PlayerInput _playerInput;
        private int _floorLayer;
        private float _distanceToFloor;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            
            _floorLayer = LayerMask.NameToLayer("Floor");
            _distanceToFloor = _collider.bounds.extents.y * _distToFloorMultiplier;
        }

        public void Move(Vector2 input)
        {
            float xTranslation = input.x * _movementSpeed * Time.deltaTime * (IsGrounded() ? 1f : _airMovementModifier);

            var movement = new Vector3(xTranslation, 0.0f, 0.0f);
            movement = transform.TransformDirection(movement);
            
            _rigidbody.AddForce(movement, ForceMode.Force);;
        }

        private void Update()
        {
            if (_rigidbody.velocity.y < 0)
            {
                _rigidbody.velocity += Vector3.up * (Physics.gravity.y * (_fallMultiplier - 1) * Time.deltaTime);
            }
            else if (_rigidbody.velocity.y > 0 && !_playerInput.IsHoldingJump)
            {
                _rigidbody.velocity += Vector3.up * (Physics.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime);
            }
        }
        
        public void Jump(Vector2 input)
        {
            if (input.y >= 1 && IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce);
            }
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.down), out var _, _distanceToFloor, ~_floorLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Vector3.down * _distanceToFloor);
        }
    }
}