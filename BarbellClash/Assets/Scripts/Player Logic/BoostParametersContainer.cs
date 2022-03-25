using System;
using UnityEngine;

namespace PlayerLogic
{
    [Serializable]
    public class BoostParametersContainer
    {
        [field: SerializeField] private float _deltaBoostSpeed;
        [field: Space(15)] 
        [field: SerializeField] public float _minSpeed { get; private set; }
        [field: SerializeField] public float _maxSpeed { get; private set; }

        public float BoostCurrentSpeed(float currentSpeed)
        {
            float nextSpeed = currentSpeed + _deltaBoostSpeed*Time.deltaTime;
            return Mathf.Clamp(nextSpeed, _minSpeed, _maxSpeed);
        }

        public float DebuffCurrentSpeed(float currentSpeed)
        {
            return _minSpeed;
        }
    }
}