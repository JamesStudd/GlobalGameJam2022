using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class WorldReactiveRotator : WorldReactive
    {
        [SerializeField] private Transform _root;
        [SerializeField] private Transform _rootCollider;
        [SerializeField] private Vector3 _targetRotation;
        [SerializeField] private float _movementTime;

        private Vector3 _initialRotation;
        
        private void Awake()
        {
            _initialRotation = _root.localRotation.eulerAngles;
        }

        public override void Unlock()
        {
            _root.DOLocalRotate(_targetRotation, _movementTime);
        }

        public override void Lock()
        {
            _root.DOLocalRotate(_initialRotation, _movementTime);
        }
    }
}