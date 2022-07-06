using UnityEngine;

namespace Softbody.Interfaces
{
    public interface IVisualable
    {
        void SmokeAfterShake();
        void ShowCompleteEmotion();
        void ShowDieEmotion();

        void ShowHitDie(Vector3 position);
    }
}