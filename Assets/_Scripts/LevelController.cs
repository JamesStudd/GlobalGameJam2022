using UnityEngine;

namespace _Scripts
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private MovementPlayback _playerPrefab;
        [SerializeField] private Vector3 _spawnPoint;
        [SerializeField] private int _respawnsAllowed;
        
        [SerializeField] private GameEndView _gameEndView;

        private MovementPlayback _currentPlayer;

        private int _respawnsDone;
        private float _startTime;

        public static int SpawnCount = 0;
        
        private void Awake()
        {
            FeatureLocker.SetPlayerInputEnabled(true);
            
            GameEvents.OnGameEnd += OnGameEnd;

            _startTime = Time.realtimeSinceStartup;
            SpawnCount = 0;
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
                _currentPlayer.GetComponent<MovementPlayback>().OnReplayed -= SpawnPlayer;
            }
            
            _currentPlayer = Instantiate(_playerPrefab, _spawnPoint, _playerPrefab.transform.rotation);
            _currentPlayer.OnReplayed += SpawnPlayer;

            _respawnsDone++;
            SpawnCount++;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawSphere(_spawnPoint, 0.5f);
        }
    }
}