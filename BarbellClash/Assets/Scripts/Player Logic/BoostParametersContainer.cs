using System;
using UnityEngine;

namespace PlayerLogic
{
    [Serializable]
    public class BoostParametersContainer
    {
        [field: SerializeField] private float _deltaBoostSpeed;
        [field: Space(15)] 
        [field: SerializeField] private float _minSpeed;
        [field: SerializeField] private float _maxSpeed;

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