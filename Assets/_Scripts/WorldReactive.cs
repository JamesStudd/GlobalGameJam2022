using UnityEngine;

namespace _Scripts
{
    public abstract class WorldReactive : MonoBehaviour
    {
        public bool IsLocked;
        
        public abstract void Unlock();
        public abstract void Lock();
    }
}