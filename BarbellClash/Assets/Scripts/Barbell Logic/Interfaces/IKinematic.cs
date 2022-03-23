using UnityEngine;

namespace Barbell
{
    public interface IKinematic
    {
        void DoMove(Vector3 direction);

        void DoUp(float upSpeed);
        void DoDown(float downSpeed);

        void SetUpdateMovement(bool isIgnore);
    }
}