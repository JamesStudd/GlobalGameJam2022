using UnityEngine;

namespace _Scripts
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerPrefab;
        [SerializeField] private Vector3 _spawnPoint;
        [SerializeField] private int _respawnsAllowed;
        
        [SerializeField] private GameEndView _gameEndView;

        private PlayerInput _currentPlayer;

        private int _respawnsDone;
        private float _startTime;
        
        private void Awake()
        {
            GameEvents.OnGameEnd += OnGameEnd;

            _startTime = Time.realtimeSinceStartup;
        }

        private void OnDestroy()
        {
            GameEvents.OnGameEnd -= OnGameEnd;
        }

        private void OnGameEnd(bool _)
        {
            var levelTime = Time.realtimeSinceStartup - _startTime;
            _gameEndView.SetLevelTime(levelTime);
        }
        
        private void Start()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            if (_respawnsDone >= _respawnsAllowed)
            {
                GameEvents.GameEnd(false);
                return;
            }
            
            if (_currentPlayer != null)
            {
                _currentPlayer.OnReplay -= SpawnPlayer;
            }
            
            _currentPlayer = Instantiate(_playerPrefab, _spawnPoint, Quaternion.identity);
            _currentPlayer.OnReplay += SpawnPlayer;

            _respawnsDone++;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawSphere(_spawnPoint, 0.5f);
        }
    }
}