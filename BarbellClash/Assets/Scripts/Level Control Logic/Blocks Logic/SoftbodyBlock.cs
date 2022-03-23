using System;
using ATG.LevelControl;
using Barbell;
using Softbody;
using UnityEngine;
using Zenject;

[Serializable]
public class BlockInstanceTarget
{
    [field: SerializeField]
    public Transform TargetPosition { get; private set; }
}

public class SoftbodyBlock : EnvironmentBlock
{
    [Space(15)]
    [SerializeField] private SoftbodyLogic _softbodyInstance;
    
    [Space(15)] 
    [SerializeField] private BlockInstanceTarget _instanceOptions;

    private ICrushable _curKinematic;
    
    public SoftbodyLogic Softbody { get; private set; }
    
    [Inject]
    private void Constructor(SoftbodyLogic.Factory factory)
    {
        Softbody = factory.Create(_softbodyInstance.gameObject);
        
        Softbody.transform.SetParent(transform);
        Softbody.transform.position =_instanceOptions.TargetPosition.position;
        Softbody.transform.rotation = _instanceOptions.TargetPosition.rotation;
        
        Softbody.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_curKinematic != null)
            return;
        
        if (other.attachedRigidbody.TryGetComponent(out ICrushable k))
        {
            Softbody.AnimateSoftbodyCrush();
            if (k.MaxPlateId == Softbody.NeededPlateId)
            {
                _curKinematic = k;
                
                Softbody.SetSoftbodyActive(true);
            }
            else if(k.MaxPlateId < Softbody.NeededPlateId)
            {
                Softbody.DoDie();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_curKinematic == null)
            return;

        if (other.attachedRigidbody.TryGetComponent(out ICrushable k))
        {
            if (ReferenceEquals(k, _curKinematic))
            {
                _curKinematic = null;
                
                Softbody.SetSoftbodyActive(false);
            }
        }
    }
}
