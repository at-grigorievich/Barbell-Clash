using System;

namespace ATG.LevelControl
{
    public interface ILevelStatus
    {
        event EventHandler OnLevelStart;
        event EventHandler OnCompleteLevel;
        event EventHandler OnFailedLevel;

        public void StartLevel();
        public void CompleteLevel();
        public void FailedLevel();

    }
}