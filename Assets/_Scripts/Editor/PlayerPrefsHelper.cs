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
            Debug.Log(PlayerPrefs.GetString(SaveManager.SaveGamePlayerPrefsKey));
        }
        
        [MenuItem("TimeBot/Clear Player Prefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteKey(SaveManager.SaveGamePlayerPrefsKey);
        }
    }
}