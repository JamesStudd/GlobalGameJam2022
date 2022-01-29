using UnityEngine;

namespace _Scripts
{
    public class DialogCollisionBox : MonoBehaviour
    {
        [SerializeField] private string[] _dialogs;
        [SerializeField] private bool _enablesReplay;
        public string[] Dialogs => _dialogs;
        public bool EnablesReplay => _enablesReplay;

        public bool HasBeenCollidedWith;
    }
}