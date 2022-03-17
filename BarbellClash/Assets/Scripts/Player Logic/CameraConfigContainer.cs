using System;
using UnityEngine;

namespace PlayerLogic
{
    [Serializable]
    public class CameraConfigContainer
    {
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private float _fov;

        public void SetupCamera(Camera cam)
        {
            cam.transform.position = _position;
            cam.transform.rotation = Quaternion.Euler(_rotation);
            cam.fieldOfView = _fov;
        }
    }
}