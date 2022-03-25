using Barbell;
using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    public class FinishBlock: EnvironmentBlock
    {
        [Inject] private ILevelStatus _levelStatus;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody != null)
            {
                if (other.attachedRigidbody.TryGetComponent(out IKinematic k))
                {
                    _levelStatus.StartDebrief();
                }
            }
        }
    }
}