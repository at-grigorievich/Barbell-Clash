using Cinemachine;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerCinemachineService : MonoBehaviour, ICinemachinable
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private CinemachineTransformLocker _cameraLocker;
        
        public void InitCinemachine(Transform target, CameraConfigContainer cameraConfig)
        {
            _virtualCamera.Follow = target;
            _virtualCamera.m_Lens.FieldOfView = cameraConfig.Fov;
            
            _cameraLocker.LockPosition = cameraConfig.CamPosition;
            _cameraLocker.LockRotation = cameraConfig.CamRotation;
        }

        public void SetFOV(float nextFOV)
        {
            _virtualCamera.m_Lens.FieldOfView = nextFOV;
        }

        public void Off()
        {
            _virtualCamera.transform.gameObject.SetActive(false);
        }
    }
}