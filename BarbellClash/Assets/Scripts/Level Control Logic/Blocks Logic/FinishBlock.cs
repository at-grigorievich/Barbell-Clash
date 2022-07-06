using System;
using Barbell;
using Cinemachine;
using Debrief;
using UnityEngine;
using VFXLogic;
using Zenject;

namespace ATG.LevelControl
{
    /// <summary>
    /// Uncomment if need record creatives
    /// </summary>
    public class FinishBlock: EnvironmentBlock
    {
        [Inject] private ILevelStatus _levelStatus;
        [Inject] private IVFXControllable _vfxControllable;

        [SerializeField] private Transform[]_confettiTargets;
        [SerializeField] private DebriefBodybuilder _bodybuilder;


        private void Start()
        {
            //_bodybuilder.gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody != null)
            {
                if (other.attachedRigidbody.TryGetComponent(out IKinematic k))
                {
                   //  var a = FindObjectOfType<CinemachineVirtualCamera>();
                   //  Destroy(a.gameObject);
                   //  
                   // if(k is BarbellLogic bl)
                   //     Destroy(bl);
                   //  _bodybuilder.StartSquat(2f,other.attachedRigidbody.transform);

                    _levelStatus.StartDebrief();

                    foreach (var confettiTarget in _confettiTargets)
                    {
                        var ps = _vfxControllable
                            .PlayVFX(VFXType.Confetti, confettiTarget.position,
                                confettiTarget.eulerAngles);
                        
                        ps.Play();
                    }
                }
            }
        }
    }
}