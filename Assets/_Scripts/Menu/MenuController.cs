using _Scripts.RoundManagement;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;

        [SerializeField] private Transform _rootMenu;
        [SerializeField] private LevelSelectPanel _levelSelectPanel;
        [SerializeField] private SettingsPanel _settingsPanel;

        [SerializeField] private float _menuMovementX;
        [SerializeField] private float _panelMovementX;
        [SerializeField] private float _menuMoveTime;
        [SerializeField] private float _panelMoveTime;

        private Transform _lastMovedPanel;
        
        private void Awake()
        {
            _creditsButton.onClick.AddListener(SceneController.LoadCredits);
        }

        public void GoToSettings()
        {
            _lastMovedPanel = _settingsPanel.transform;
            MoveMenu(false, () =>
            {
                MovePanel(_settingsPanel.transform, true, () => {});
            });
        }

        public void GoToLevelSelect()
        {
            _lastMovedPanel = _levelSelectPanel.transform;
            MoveMenu(false, () =>
            {
                MovePanel(_levelSelectPanel.transform, true, () => {});
            });
        }

        public void GoBack()
        {
            MovePanel(_lastMovedPanel, false, () =>
            {
                MoveMenu(true, () => {});
            });
        }

        private void MoveMenu(bool intoCamera, Action callback)
        {
            SetButtonsInteractable(false);
            
            var position = _rootMenu.localPosition;

            var targetPos = intoCamera ? position.x - _menuMovementX : position.x + _menuMovementX;
            var target = new Vector3(targetPos, position.y, position.z);

            _rootMenu.DOLocalMove(target, _menuMoveTime)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    SetButtonsInteractable(true);
                    callback?.Invoke();
                });
        }
        
        private void MovePanel(Transform t, bool intoCamera, Action callback)
        {
            var position = t.localPosition;

            var targetPos = intoCamera ? position.x - _panelMovementX : position.x + _panelMovementX;
            var target = new Vector3(targetPos, position.y, position.z);

            t.DOLocalMove(target, _panelMoveTime)
                .SetEase(Ease.InOutSine)
                .OnComplete(() => callback?.Invoke());
        }

        private void SetButtonsInteractable(bool interactable)
        {
            _creditsButton.interactable = interactable;
            _playButton.interactable = interactable;
            _settingsButton.interactable = interactable;
        }
    }
}
