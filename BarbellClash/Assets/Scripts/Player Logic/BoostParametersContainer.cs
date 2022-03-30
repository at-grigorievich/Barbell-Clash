using System;
using UnityEngine;

namespace PlayerLogic
{
    [Serializable]
    public class BoostParametersContainer
    {
        [field: SerializeField] private float _deltaBoostSpeed;
        [field: SerializeField] public float DebuffDelay { get; private set; }
        [field: Space(15)] 
        [field: SerializeField] public float _minSpeed { get; private set; }
        [field: SerializeField] public float _maxSpeed { get; private set; }

        public float Delta => _deltaBoostSpeed;
        
        public float BoostCurrentSpeed(float currentSpeed)
        {
            float nextSpeed = currentSpeed + _deltaBoostSpeed;
            return Mathf.Clamp(nextSpeed, _minSpeed, _maxSpeed);
        }
        public float DebuffCurrentSpeed() => _minSpeed/2f;
        
    }
}