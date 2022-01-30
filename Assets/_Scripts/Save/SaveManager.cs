using _Scripts.RoundManagement;
using System;
using System.Linq;
using UnityEngine;

namespace _Scripts.Save
{
    public static class SaveManager
    {
        public const string SaveGamePlayerPrefsKey = "time_boy_savegame";

        private static Savegame _savegame;
        
        public static Savegame Load()
        {
            return Create();
        }

        public static void UpdateRound(int id, float time)
        {
        }
        
        public static void Save()
        {
            var json = JsonUtility.ToJson(_savegame);
            PlayerPrefs.SetString(SaveGamePlayerPrefsKey, json);
        }

        private static Savegame Create()
        {
            return new Savegame
            {
                RoundSavegames = SceneController.Scenes.Select(e => new RoundSavegame {Id = e.Key, BestTime = float.MaxValue, HasCompleted = false}).ToArray()
            };
        }
    }
}