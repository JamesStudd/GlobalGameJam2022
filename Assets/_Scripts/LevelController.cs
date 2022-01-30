using _Scripts.RoundManagement;
using _Scripts.Save;
using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private int _levelId;
        [SerializeField] private MovementPlayback _playerPrefab;
        [SerializeField] private Vector3 _spawnPoint;
        [SerializeField] private int _respawnsAllowed;
        [SerializeField] private CanvasGroup _fadeCanvasGroup;
        [SerializeField] private float _timeToFade;
        
        private MovementPlayback _currentPlayer;

        private int _respawnsDone = 0;
        private float _startTime;

        public int RespawnsDone => _respawnsDone;
        public bool CanSpawnAgain => _respawnsDone < _respawnsAllowed;
        
        private void Awake()
        {
            FeatureLocker.SetPlayerInputEnabled(true);
            GameEvents.OnGameEnd += OnGameEnd;
            
            _startTime = Time.realtimeSinceStartup;

            if (_levelId > 0)
            {
                FeatureLocker.SetReplayingEnabled(true);
            }
        }

        private void OnDestroy()
        {
            GameEvents.OnGameEnd -= OnGameEnd;
        }

        private void Start()
        {
            SpawnPlayer();
        }
        
        private void OnGameEnd(bool victory)
        {
            if (victory)
            {
                var levelTime = Time.realtimeSinceStartup - _startTime;
                SaveManager.UpdateRound(_levelId, levelTime);

                if (SceneController.HasAnotherLevel(_levelId))
                {
                    SceneController.LoadRound(_levelId + 1);
                }
                else
                {
                    SceneController.LoadCredits();
                }
                
                return;
            }

            _fadeCanvasGroup.DOFade(1f, _timeToFade)
                .OnComplete(() =>
                {
                    FeatureLocker.SetReplayingEnabled(false);
                    SceneController.LoadRound(_levelId);
                });
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
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawSphere(_spawnPoint, 0.5f);
        }
    }
}