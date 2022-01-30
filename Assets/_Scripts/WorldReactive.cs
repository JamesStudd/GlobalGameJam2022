using UnityEngine;

namespace _Scripts
{
    public abstract class WorldReactive : MonoBehaviour
    {
        [SerializeField] private bool _isPermanent;

        public bool IsPermanent => _isPermanent;
        
        public abstract void Unlock();
        public abstract void Lock();
    }
}