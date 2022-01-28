using System;
using UnityEngine;

namespace _Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private const string FloorTag = "Floor";
        
        [SerializeField] private float _speed;
        [SerializeField] private float _jump;
        
        private float _moveVelocity;
        private bool _grounded = true;

        private Rigidbody _rigidbody;

        private bool _isJumping = false;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update () 
        {
            //Jumping
            if (Input.GetKeyDown (KeyCode.Space)) 
            {
                if (_grounded)
                {
                    _isJumping = true;
                }
            }

            //Left Right Movement
            if (Input.GetKey (KeyCode.A)) 
            {
                _moveVelocity = -_speed;
            }
            if (Input.GetKey (KeyCode.D)) 
            {
                _moveVelocity = _speed;
            }
        }

        private void FixedUpdate()
        {
            if (_isJumping)
            {
                _rigidbody.AddForce(Vector3.up * _jump, ForceMode.Impulse);
                _isJumping = false;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(FloorTag))
            {
                _grounded = true;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag(FloorTag))
            {
                _grounded = false;
            }
        }
    }
}