using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace _Scripts.RoundManagement
{
    public static class SceneController
    {
        private static readonly Dictionary<int, string> Scenes = new Dictionary<int, string>
        {
            {0, "Level_01"}
        };

        private const string MenuScene = "MenuScene";
        private const string CreditsScene = "CreditsScene";
        
        public static void LoadRound(int id)
        {
            SceneManager.LoadScene(Scenes[id]);
        }

        public static void LoadMenu()
        {
            SceneManager.LoadScene(MenuScene);
        }

        public static void LoadCredits()
        {
            SceneManager.LoadScene(CreditsScene);
        }
    }
}