using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class GameEndView : MonoBehaviour
    {
        [SerializeField] private GameObject _rootObject;
        [SerializeField] private TMP_Text _levelTimeText;
        [SerializeField] private TMP_Text _outcomeText;
        
        public void NextLevel()
        {
            
        }

        public void Menu()
        {
            
        }

        public void Replay()
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }

        public void SetLevelTime(float levelTime)
        {
            _levelTimeText.text = levelTime.ToString("F2");
        }
        
        private void Awake()
        {
            GameEvents.OnGameEnd += OnGameEnd;
            
            _rootObject.SetActive(false);
        }

        private void OnDestroy()
        {
            GameEvents.OnGameEnd -= OnGameEnd;
        }

        private void OnGameEnd(bool victory)
        {
            _outcomeText.text = victory ? "You win!" : "You lose :(";
            _rootObject.SetActive(true);
        }
    }
}