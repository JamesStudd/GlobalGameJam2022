using UnityEngine;

namespace _Scripts
{
    public class PlayerCollision : MonoBehaviour
    {
        private const string EndGameTag = "EndGame";
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(EndGameTag))
            {
                GameEvents.GameEnd(true);
            }
        }
    }
}