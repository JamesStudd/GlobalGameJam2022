using UnityEngine;

namespace _Scripts
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerPrefab;
        [SerializeField] private Vector3 _spawnPoint;

        private PlayerInput _currentPlayer;
        
        private void Start()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            if (_currentPlayer != null)
            {
                _currentPlayer.OnReplay -= SpawnPlayer;
            }
            
            _currentPlayer = Instantiate(_playerPrefab, _spawnPoint, Quaternion.identity);
            _currentPlayer.OnReplay += SpawnPlayer;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawSphere(_spawnPoint, 0.5f);
        }
    }
}