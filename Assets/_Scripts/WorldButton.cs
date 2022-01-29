using UnityEngine;

namespace _Scripts
{
    public class WorldButton : MonoBehaviour
    {
        [SerializeField] private WorldReactiveMover _mover;
        [SerializeField] private Transform _movableButton;
        [SerializeField] private float _depressAmount;

        public void Unlock()
        {
            _mover.Unlock();
            var pos = _movableButton.position;

            pos.y -= _depressAmount;

            _movableButton.position = pos;
        }
        
        public void Lock()
        {
            _mover.Lock();
            
            var pos = _movableButton.position;

            pos.y += _depressAmount;

            _movableButton.position = pos;
        }

        private void OnDrawGizmosSelected()
        {
            if (_mover != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(transform.position, _mover.transform.position);
            }
        }
    }
}