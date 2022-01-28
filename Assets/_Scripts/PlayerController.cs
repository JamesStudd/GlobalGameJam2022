using System;
using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float _movementSpeed;
        public Rigidbody _rigidbody;
        
        public void Move(Vector2 input)
        {
            float xTranslation = input.x * _movementSpeed * Time.deltaTime;
            float zTranslation = input.y * _movementSpeed * Time.deltaTime;

            var movement = new Vector3(xTranslation, 0.0f, zTranslation);
            movement = transform.TransformDirection(movement);
            _rigidbody.AddForce(movement, ForceMode.Force);
        }
        
        public void Jump()
        {
        }
    }
}