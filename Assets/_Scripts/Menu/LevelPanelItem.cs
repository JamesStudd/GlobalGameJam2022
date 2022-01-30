using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Menu
{
    public class LevelPanelItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _lockIcon;
        
        public Button Button => _button;

        public bool CanPlay
        {
            set
            {
                _button.interactable = value;
                _lockIcon.SetActive(!value);
            }
        }
    }
}