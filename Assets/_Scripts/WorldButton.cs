using Sirenix.Utilities;
using System.Linq;
using UnityEngine;

namespace _Scripts
{
    public class WorldButton : MonoBehaviour
    {
        [SerializeField] private WorldReactive[] _reactives;
        [SerializeField] private Transform _movableButton;
        [SerializeField] private float _depressAmount;

        public void Unlock()
        {
            _reactives.ForEach(e => e.Unlock());
            var pos = _movableButton.position;

            pos.y -= _depressAmount;

            _movableButton.position = pos;
        }
        
        public void Lock()
        {
            _reactives.Where(e => !e.IsPermanent).ForEach(e => e.Lock());
            
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