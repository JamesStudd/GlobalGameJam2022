using DG.Tweening;
using Sirenix.Utilities;
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

        public void Unlock()
        {
            _reactives.ForEach(e => e.Unlock());

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
        }
        
        public void Lock()
        {
            if (_isPermanent)
            {
                return;
            }
            
            _reactives.ForEach(e => e.Lock());
            
            var pos = _movableButton.position;
            pos.y += _depressAmount;
            _movableButton.position = pos;
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