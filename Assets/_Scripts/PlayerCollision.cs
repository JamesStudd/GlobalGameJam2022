using UnityEngine;

namespace _Scripts
{
    public class PlayerCollision : MonoBehaviour
    {
        private const string EndGameTag = "EndGame";
        private const string WorldButtonTag = "WorldButton";
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(EndGameTag))
            {
                GameEvents.GameEnd(true);
            }

            if (other.gameObject.CompareTag(WorldButtonTag))
            {
                other.GetComponent<WorldButton>().Unlock();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(WorldButtonTag))
            {
                other.GetComponent<WorldButton>().Lock();
            }
        }
    }
}