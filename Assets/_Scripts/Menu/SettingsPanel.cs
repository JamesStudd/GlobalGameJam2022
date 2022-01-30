using UnityEngine;

namespace _Scripts.Menu
{
    public class SettingsPanel : MonoBehaviour
    {
        public void SetEffectVolume(float value)
        {
            AudioManager.Instance.SetEffectVolume(value);
        }

        public void SetMusicVolume(float value)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }
    }
}