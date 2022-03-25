using Softbody.Interfaces;
using UnityEngine;
using VFXLogic;
using Zenject;

namespace Softbody
{
    public class SoftbodyVFXService : MonoBehaviour, IVisualable
    {
        [SerializeField] private Transform _smokeTransform;
        [SerializeField] private Transform _emojiTransform;

        [Inject] private IVFXControllable _vfxControllable;

        public void SmokeAfterShake()
        {
            ParticleSystem ps = _vfxControllable.PlayVFX(VFXType.Smoke, _smokeTransform.position, Vector3.zero);
        }

        public void ShowCompleteEmotion()
        {
            ParticleSystem ps = _vfxControllable.PlayVFX(VFXType.CompleteEmoji, _emojiTransform.position, Vector3.zero);
        }

        public void ShowDieEmotion()
        {
            ParticleSystem ps = _vfxControllable.PlayVFX(VFXType.FailedEmoji, _emojiTransform.position, Vector3.zero);
        }

        public void ShowHitDie(Vector3 position)
        {
            ParticleSystem ps = _vfxControllable.PlayVFXLoop(VFXType.Death, position, Vector3.zero);
            ps.Play();
        }
    }
}