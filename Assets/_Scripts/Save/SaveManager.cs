﻿using _Scripts.RoundManagement;
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

        public static void UpdateRound(int id, float time)
        {
            _savegame ??= Load();
            
            var save = _savegame.RoundSavegames
                .First(e => e.Id == id);

            if (time < save.BestTime)
            {
                save.BestTime = time;    
            }

            save.HasCompleted = true;

            Save();
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