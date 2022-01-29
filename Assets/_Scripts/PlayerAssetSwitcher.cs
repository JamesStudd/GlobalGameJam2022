using UnityEngine;

namespace _Scripts
{
    public class PlayerAssetSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _copiedPlayer;

        private MovementPlayback _movementPlayback;

        private void Awake()
        {
            _movementPlayback = GetComponent<MovementPlayback>();

            _movementPlayback.OnReplayed += () =>
            {
                _player.SetActive(false);
                _copiedPlayer.SetActive(true);
            };
        }
    }
}