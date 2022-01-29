using System;

namespace _Scripts
{
    public static class GameEvents
    {
        public static event Action<bool> OnGameEnd;
        public static event Action<string[]> OnDialogStart;
        public static event Action OnDialogEnd;

        public static void GameEnd(bool victory)
        {
            OnGameEnd?.Invoke(victory);
        }

        public static void DialogStart(string[] dialogs)
        {
            OnDialogStart?.Invoke(dialogs);
        }

        public static void DialogEnd()
        {
            OnDialogEnd?.Invoke();
        }
    }
}