using System;

namespace _Scripts
{
    public static class GameEvents
    {
        public static event Action<bool> OnGameEnd;

        public static void GameEnd(bool victory)
        {
            OnGameEnd?.Invoke(victory);
        }
    }
}