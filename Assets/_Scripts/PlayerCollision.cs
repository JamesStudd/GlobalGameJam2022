using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class PlayerCollision : MonoBehaviour
    {
        private const string EndGameTag = "EndGame";
        private const string WorldButtonTag = "WorldButton";
        private const string PlayerSpawnTag = "PlayerSpawn";
        private const string DialogCollisionTag = "DialogCollision";

        private int _ignorePlayerLayer;
        private int _playerLayer;

        private void Awake()
        {
            _ignorePlayerLayer = LayerMask.NameToLayer("IgnorePlayer");
            _playerLayer = LayerMask.NameToLayer("Player");

            if (LevelController.SpawnCount > 0)
            {
                gameObject.layer = _ignorePlayerLayer;
            }
        }

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

            if (other.gameObject.CompareTag(DialogCollisionTag))
            {
                var dialogCollisionBox = other.GetComponent<DialogCollisionBox>();

                if (dialogCollisionBox.HasBeenCollidedWith)
                {
                    return;
                }

                dialogCollisionBox.HasBeenCollidedWith = true;
                GameEvents.DialogStart(dialogCollisionBox.Dialogs);

                if (dialogCollisionBox.EnablesReplay)
                {
                    FeatureLocker.SetReplayingEnabled(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(WorldButtonTag))
            {
                other.GetComponent<WorldButton>().Lock();
            }

            if (other.gameObject.CompareTag(PlayerSpawnTag))
            {
                gameObject.layer = _playerLayer;
            }
            
            if (other.gameObject.CompareTag(DialogCollisionTag))
            {
                GameEvents.DialogEnd();
            }
        }
    }
}