using UnityEngine;

namespace PlayerLogic
{
    public interface IControllable
    {
        Transform MyTransform { get; }
    }
}