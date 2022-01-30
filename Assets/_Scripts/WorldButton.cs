using DG.Tweening;
using Sirenix.Utilities;
using System;
using System.Collections;
using UnityEngine;

namespace _Scripts
{
    public class WorldButton : MonoBehaviour
    {
        [SerializeField] private WorldReactive[] _reactives;
        [SerializeField] private Transform _movableButton;
        [SerializeField] private bool _isPermanent;
        
        [Header("Movement")]
        [SerializeField] private bool _rotate;
        [SerializeField] private float _depressAmount;
        [SerializeField] private Transform _rotateRoot;
        [SerializeField] private Vector3 _rotateAmount;
        [SerializeField] private float _rotateTime;

        private Vector3 _initialRotation;
        
        private bool _canCollide = true;
        public bool CanCollide
        {
            get => _canCollide;
            set
            {
                _canCollide = value;
                StartCoroutine(DoAfterSeconds(0.25f, () => _canCollide = !value));
            }
        }

        private void Awake()
        {
            _initialRotation = transform.rotation.eulerAngles;
        }

        public bool IsPermanent => _isPermanent;
        [HideInInspector] public bool IsOn;

        private IEnumerator DoAfterSeconds(float x, Action callback)
        {
            yield return new WaitForSeconds(x);
            callback?.Invoke();
        }
        
        public void Unlock()
        {
            _reactives.ForEach(e =>
            {
                if (e.IsLocked)
                {
                    e.Unlock();    
                }
                else
                {
                    e.Lock();
                }
            });

            if (_rotate)
            {
                _rotateRoot.DOLocalRotate(_rotateAmount, _rotateTime);
            }
            else
            {
                var pos = _movableButton.position;
                pos.y -= _depressAmount;
                _movableButton.position = pos;   
            }

            IsOn = true;
        }
        
        public void Lock()
        {
            _reactives.ForEach(e =>
            {
                if (e.IsLocked)
                {
                    e.Unlock();    
                }
                else
                {
                    e.Lock();
                }
            });
            
            if (_rotate)
            {
                _rotateRoot.DOLocalRotate(_initialRotation, _rotateTime);
            }
            else
            {
                var pos = _movableButton.position;
                pos.y += _depressAmount;
                _movableButton.position = pos; 
            }

            IsOn = false;
        }

        private void OnDrawGizmosSelected()
        {
            if (_reactives != null)
            {
                Gizmos.color = Color.magenta;

                foreach (WorldReactive t in _reactives)
                {
                    Gizmos.DrawLine(transform.position, t.transform.position);
                }
            }
        }
    }
}