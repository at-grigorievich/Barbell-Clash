using Cinemachine;
using UnityEngine;

namespace PlayerLogic
{
    public class CinemachineTransformLocker: CinemachineExtension
    {
        [field: SerializeField]
        public Vector3 LockPosition { get; set; } = Vector3.zero;
        
        [field: SerializeField]
        public Vector3 LockRotation { get; set; } = Vector3.zero;
        
        
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, 
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.x = LockPosition.x;
                pos.y = LockPosition.y;
                
                state.RawPosition = pos;
                state.RawOrientation = Quaternion.Euler(LockRotation);
            }  
        }
    }
}