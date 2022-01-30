using System;

namespace _Scripts.Save
{
    [Serializable]
    public class RoundSavegame
    {
        public int Id;
        public float BestTime;
        public bool HasCompleted;
    }
}