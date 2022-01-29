using System.Collections;
using UnityEngine;

namespace _Scripts
{
    // Is this the worst named class? I think so
    public class WorldReactiveMover : MonoBehaviour
    {
        [SerializeField] private Vector3 _targetPosition;
        [SerializeField] private float _movementTime;

        private Vector3 _initialPosition;
        private Vector3 _endPosition;
        
        private IEnumerator _movementCoroutine;

        private void Awake()
        {
            _initialPosition = transform.position;
            _endPosition = _initialPosition + _targetPosition;
        }

        public void Unlock()
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }
            
            _movementCoroutine = MovementCoroutine(_endPosition);
            StartCoroutine(_movementCoroutine);
        }

        public void Lock()
        {
            StopCoroutine(_movementCoroutine);
            
            _movementCoroutine = MovementCoroutine(_initialPosition);
            StartCoroutine(_movementCoroutine);
        }

        private IEnumerator MovementCoroutine(Vector3 target)
        {
            var elapsedTime = 0.0f;
            
            while (elapsedTime < _movementTime)
            {
                transform.position = Vector3.Lerp(transform.position, target, (elapsedTime / _movementTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = target;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;

            var position = transform.position;
            
            Gizmos.DrawLine(position, position + _targetPosition);
            Gizmos.DrawWireSphere(position + _targetPosition, 0.5f);
        }
    }
}