using _Scripts.Save;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Editor
{
    public class PlayerPrefsHelper
    {
        [MenuItem("TimeBot/Print Player Prefs")]
        public static void PrintPlayerPrefs()
        {
            Debug.Log($"Music: {PlayerPrefs.GetFloat(AudioManager.PlayerPrefsMusicVolume)}");
            Debug.Log($"Effects: {PlayerPrefs.GetFloat(AudioManager.PlayerPrefsEffectVolume)}");
            Debug.Log($"Savegame: {PlayerPrefs.GetString(SaveManager.SaveGamePlayerPrefsKey)}");
        }
        
        [MenuItem("TimeBot/Clear Player Prefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteKey(AudioManager.PlayerPrefsMusicVolume);
            PlayerPrefs.DeleteKey(AudioManager.PlayerPrefsEffectVolume);
            PlayerPrefs.DeleteKey(SaveManager.SaveGamePlayerPrefsKey);
        }
    }
}