using UnityEngine;

namespace PlayerLogic
{
    public interface ICinemachinable
    {
        void InitCinemachine(Transform target, CameraConfigContainer cameraConfig);
    }
}