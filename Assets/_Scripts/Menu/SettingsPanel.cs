using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Menu
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _effectsSlider;
        [SerializeField] private Slider _voiceSlider;

        private void Awake()
        {
            _musicSlider.value = AudioManager.Instance.MusicVolume;
            _effectsSlider.value = AudioManager.Instance.EffectVolume;
            _voiceSlider.value = AudioManager.Instance.VoiceVolume;
        }

        public void SetEffectVolume(float value)
        {
            AudioManager.Instance.SetEffectVolume(value);
        }

        public void SetMusicVolume(float value)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }

        public void SetVoiceVolume(float value)
        {
            AudioManager.Instance.SetVoiceVolume(value);
        }
    }
}