using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Menu
{
    public class LevelPanelItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _lockIcon;
        [SerializeField] private TMP_Text _levelText;
        
        public Button Button => _button;

        public bool CanPlay
        {
            set
            {
                _button.interactable = value;
                _lockIcon.SetActive(!value);
            }
        }

        public string LevelText
        {
            set => _levelText.text = value;
        }
    }
}