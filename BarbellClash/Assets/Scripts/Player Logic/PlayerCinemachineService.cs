using Cinemachine;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerCinemachineService : MonoBehaviour, ICinemachinable
    {
        private const float _refWidth = 1080f;
        private const float _refHeight = 1920f;

        private Camera _mainCamera;
        
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private CinemachineTransformLocker _cameraLocker;
        
        public void InitCinemachine(Transform target, CameraConfigContainer cameraConfig)
        {
            _mainCamera = Camera.main;
            
            _virtualCamera.Follow = target;
            SetFOV(cameraConfig.Fov);

            _cameraLocker.LockPosition = cameraConfig.CamPosition;
            _cameraLocker.LockRotation = cameraConfig.CamRotation;
        }

        public void SetFOV(float nextFOV)
        {
            float curWidth = (float)_mainCamera.pixelWidth;
            float curHeight = (float) _mainCamera.pixelHeight;

            float camFOV = nextFOV*(_refWidth/_refHeight) / (curWidth / curHeight);

            _virtualCamera.m_Lens.FieldOfView = camFOV;
        }

        public void Off()
        {
            _virtualCamera.transform.gameObject.SetActive(false);
        }
    }
}