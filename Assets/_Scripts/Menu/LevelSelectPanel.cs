using _Scripts.RoundManagement;
using _Scripts.Save;
using UnityEngine;

namespace _Scripts.Menu
{
    public class LevelSelectPanel : MonoBehaviour
    {
        [SerializeField] private LevelPanelItem[] _levelButtons;

        private void Awake()
        {
            var savegame = SaveManager.Load();

            for (int i = 0; i < savegame.RoundSavegames.Length; i++)
            {
                var save = savegame.RoundSavegames[i];
                
                _levelButtons[i].Button
                    .onClick
                    .AddListener(() =>
                    {
                       SceneController.LoadRound(save.Id); 
                    });

                var canPlay = i == 0 || savegame.RoundSavegames[i - 1].BestTime != 0;

                _levelButtons[i].CanPlay = canPlay;
            }
        }
    }
}