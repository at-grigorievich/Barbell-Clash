using System;
using UnityEngine;

namespace PlayerLogic
{
    [Serializable]
    public class CameraConfigContainer
    {
        [field: SerializeField] 
        public Vector3 CamPosition { get; private set; }
        
        [field: SerializeField] 
        public Vector3 CamRotation { get; private set; }
        
        [field: SerializeField] 
        public float Fov { get; private set; }
    }
}