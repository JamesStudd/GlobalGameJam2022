using UnityEngine;

namespace _Scripts
{
    public class WorldButton : MonoBehaviour
    {
        [SerializeField] private WorldReactiveMover _mover;

        public void Unlock()
        {
            _mover.Unlock();
        }
        
        public void Lock()
        {
            _mover.Lock();
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