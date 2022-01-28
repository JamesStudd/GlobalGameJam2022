using System;
using UnityEngine;

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
        
        private int _floorLayer;
        private float _distanceToFloor;

        private void Awake()
        {
            _floorLayer = LayerMask.NameToLayer("Floor");
            _distanceToFloor = _collider.bounds.extents.y + 0.5f;
        }

        public void Move(Vector2 input)
        {
            float xTranslation = input.x * _movementSpeed * Time.deltaTime * (IsGrounded() ? 1f : _airMovementModifier);

            var movement = new Vector3(xTranslation, 0.0f, 0.0f);
            movement = transform.TransformDirection(movement);
            
            _rigidbody.AddForce(movement, ForceMode.Force);
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity.x > _speedLimit)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _speedLimit;
            }
        }

        public void Jump()
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce);
            }
        }

        private bool IsGrounded()
        {
            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Vector3.down * _distanceToFloor);
        }
    }
}