using System;

namespace ATG.LevelControl
{
    public class ATGLevelStatus: ILevelStatus
    {
        public event EventHandler OnLevelStart;
        public event EventHandler OnDebriefStart;
        public event EventHandler OnCompleteLevel;
        public event EventHandler OnFailedLevel;
        
        public virtual void StartLevel()
        {
            OnLevelStart?.Invoke(this,null);
        }

        public virtual void CompleteLevel()
        {
            OnCompleteLevel?.Invoke(this,null);
        }

        public virtual void FailedLevel()
        {
            OnFailedLevel?.Invoke(this,null);
        }

        public void StartDebrief()
        {
            OnDebriefStart?.Invoke(this,null);
        }
    }
}