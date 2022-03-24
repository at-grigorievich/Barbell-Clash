using UnityEngine;

namespace Barbell
{
    public interface IKinematic
    {
        void DoMove(Vector3 direction);

        void DoUp(float upSpeed);
        void DoDown(float downSpeed);

        void UpdateCurrentHeight(ISizeable newSize);
        
        void SetUpdateMovement(bool isIgnore);
    }
}