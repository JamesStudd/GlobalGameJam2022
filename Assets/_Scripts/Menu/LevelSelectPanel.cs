using _Scripts.RoundManagement;
using _Scripts.Save;
using UnityEngine;

namespace _Scripts.Menu
{
    public class LevelSelectPanel : MonoBehaviour
    {
        [SerializeField] private LevelPanelItem _levelPanelPrefab;
        [SerializeField] private Transform _levelItemParent;

        private void Awake()
        {
            var savegame = SaveManager.Load();

            for (int i = 0; i < savegame.RoundSavegames.Length; i++)
            {
                var save = savegame.RoundSavegames[i];

                var instance = Instantiate(_levelPanelPrefab, _levelItemParent, false);

                instance.Button
                    .onClick
                    .AddListener(() =>
                    {
                       SceneController.LoadRound(save.Id);
                       AudioManager.Instance.PlayMusic(MusicId.Game);
                    });

                instance.LevelText = (save.Id + 1).ToString();
                instance.FastestTimeText = save.HasCompleted ? $"Fastest Time: {save.BestTime:F2}" : string.Empty;
                
                var canPlay = i == 0 || savegame.RoundSavegames[i - 1].HasCompleted;

                instance.CanPlay = canPlay;
            }
        }
    }
}