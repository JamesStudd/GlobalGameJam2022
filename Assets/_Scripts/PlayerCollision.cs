using UnityEngine;

namespace _Scripts
{
    public class PlayerCollision : MonoBehaviour
    {
        private const string EndGameTag = "EndGame";
        private const string WorldButtonTag = "WorldButton";
        private const string PlayerSpawnTag = "PlayerSpawn";
        private const string DialogCollisionTag = "DialogCollision";
        private const string KillBlockTag = "KillBlock";

        private int _ignorePlayerLayer;
        private int _playerLayer;

        private PlayerMovement _playerMovement;
        private bool IsMainPlayer => _playerMovement.enabled;
        
        private void Awake()
        {
            _ignorePlayerLayer = LayerMask.NameToLayer("IgnorePlayer");
            _playerLayer = LayerMask.NameToLayer("Player");

            _playerMovement = GetComponent<PlayerMovement>();

            if (FindObjectOfType<LevelController>().RespawnsDone > 1)
            {
                gameObject.layer = _ignorePlayerLayer;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(EndGameTag) && IsMainPlayer)
            {
                Destroy(other.gameObject);
                FeatureLocker.SetPlayerInputEnabled(false);
                GameEvents.GameEnd(true);
            }

            if (other.gameObject.CompareTag(WorldButtonTag))
            {
                var wb = other.GetComponent<WorldButton>();

                if (wb.IsPermanent && !wb.CanCollide)
                {
                    return;
                }
                
                if (wb.IsPermanent)
                {
                    wb.CanCollide = false;
                }
                
                if (wb.IsPermanent && wb.IsOn)
                {
                    wb.Lock();
                    return;
                }

                wb.Unlock();
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

            if (other.gameObject.CompareTag(KillBlockTag))
            {
                other.gameObject.GetComponent<KillBlock>().Collide(gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(WorldButtonTag))
            {
                var wb = other.GetComponent<WorldButton>();
                if (!wb.IsPermanent)
                {
                    wb.Lock();
                }
            }

            if (other.gameObject.CompareTag(PlayerSpawnTag))
            {
                gameObject.layer = _playerLayer;
            }
            
            if (other.gameObject.CompareTag(DialogCollisionTag) && IsMainPlayer)
            {
                GameEvents.DialogEnd();
            }
        }
    }
}