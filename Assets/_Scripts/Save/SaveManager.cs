using System;
using System.Linq;
using UnityEngine;

namespace _Scripts.Save
{
    public static class SaveManager
    {
        private const string SaveGamePlayerPrefsKey = "time_boy_savegame";

        private static int[] Rounds = new[]
        {
            0,
        };

        private static Savegame _savegame;
        
        public static Savegame Load()
        {
            var cachedSavegame = PlayerPrefs.GetString(SaveGamePlayerPrefsKey, string.Empty);

            if (cachedSavegame == string.Empty)
            {
                _savegame = Create();
                return _savegame;
            }

            try
            {
                _savegame = JsonUtility.FromJson<Savegame>(cachedSavegame);
                return _savegame;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load savegame {e.Message}");
                throw;
            }
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
                RoundSavegames = Rounds.Select(e => new RoundSavegame {Id = e, BestTime = 0f}).ToArray()
            };
        }
    }
}