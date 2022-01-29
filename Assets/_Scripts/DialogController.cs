using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] private Speechbubble _speechbubble;

        private void Awake()
        {
            GameEvents.OnDialogStart += HandleDialogStart;
            GameEvents.OnDialogEnd += HandleDialogEnd;
        }

        private void HandleDialogStart(string[] dialogs)
        {
            var index = 0;
            
            FeatureLocker.SetPlayerInputEnabled(false);
            ShowText(dialogs, index);
        }

        private void HandleDialogEnd()
        {
            _speechbubble.Close();
        }

        private void ShowText(string[] dialogs, int index)
        {
            _speechbubble.Open(dialogs[index], () =>
            {
                index++;
                if (index < dialogs.Length)
                {
                    ShowText(dialogs, index);
                }
                else
                {
                    FeatureLocker.SetPlayerInputEnabled(true);
                }
            });
        }
    }
}