using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    [RequireComponent(typeof(Button))]
    public class ButtonController : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            AudioManager.Instance.PlayAudioClip(AudioId.Button);   
        }
    }
}