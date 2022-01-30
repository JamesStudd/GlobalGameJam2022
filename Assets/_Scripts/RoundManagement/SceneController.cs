﻿using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace _Scripts.RoundManagement
{
    public static class SceneController
    {
        public static readonly Dictionary<int, string> Scenes = new Dictionary<int, string>
        {
            {0, "Level_01"},
            {1, "Level_02"},
            {2, "Level_03"},
        };

        private const string MenuScene = "MenuScene";
        private const string CreditsScene = "CreditsScene";

        public static bool HasAnotherLevel(int currentLevel)
        {
            return Scenes.ContainsKey(currentLevel + 1);
        }
        
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