using System;
using UnityEngine;

namespace _Scripts
{
    public class DialogCollisionBox : MonoBehaviour
    {
        [SerializeField] private string[] _dialogs;
        [SerializeField] private bool _enablesReplay;
        [SerializeField] private bool _becomesAvailableAfterReplay;
        [SerializeField] private Collider _collider;
        
        public string[] Dialogs => _dialogs;
        public bool EnablesReplay => _enablesReplay;

        public bool HasBeenCollidedWith;

        private void Awake()
        {
            if (_becomesAvailableAfterReplay)
            {
                _collider.enabled = false;
            }

            GameEvents.OnPlayerRewound += () => _collider.enabled = true;
        }
    }
}