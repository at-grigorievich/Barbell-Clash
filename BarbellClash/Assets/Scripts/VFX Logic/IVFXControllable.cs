using UnityEngine;

namespace VFXLogic
{
    public interface IVFXControllable
    {
        public ParticleSystem PlayVFX(VFXType type, Vector3 position, Vector3 lookTo);
        public ParticleSystem PlayVFX(VFXType type, Vector3 position, Transform lookTo);
        
        public ParticleSystem PlayVFXLoop(VFXType type, Vector3 position, Vector3 lookTo);
        public ParticleSystem PlayVFXLoop(VFXType type, Vector3 position, Transform lookTo);
    }
}