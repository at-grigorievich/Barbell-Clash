using System;
using ATG.LevelControl;
using Barbell;
using PlayerLogic;
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

    private Action<ICrushable> _upBerbellCollider;
    private Action<ICrushable> _downBarbellCollider;
    
    
    [Inject]
    private void Constructor(SoftbodyLogic.Factory factory)
    {
        Softbody = factory.Create(_softbodyInstance.gameObject);

        Softbody.transform.SetParent(transform);
        Softbody.transform.position =_instanceOptions.TargetPosition.position;
        Softbody.transform.rotation = _instanceOptions.TargetPosition.rotation;
        
        Softbody.gameObject.SetActive(true);
    }

    private void Start()
    {
        _upBerbellCollider = UpBerbellCollider;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(_curKinematic != null)
            return;
        
        if (other.attachedRigidbody.TryGetComponent(out ICrushable k))
        {
            _curKinematic = k;
            
            if (k.MaxPlateId == Softbody.NeededPlateId)
            {
                Softbody.SetSoftbodyActive(true);
                _upBerbellCollider?.Invoke(k);
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
                
                Softbody.MainPart.ChangeBarbellIgnoreMovement(k,false);
                Softbody.SetSoftbodyActive(false);
                
                _downBarbellCollider?.Invoke(k);
            }
        }
    }
    
    private void UpBerbellCollider(ICrushable c)
    {
        _upBerbellCollider = null;
        _downBarbellCollider = DownBarbellCollider;

        Vector3 curPos = c.CollidersContainer.localPosition;
        Vector3 add = Softbody.YDelta * Vector3.up;

        c.CollidersContainer.localPosition = curPos + add;
    }
    private void DownBarbellCollider(ICrushable c)
    {
        _downBarbellCollider = null;
        _upBerbellCollider = null;
        
        Vector3 curPos = c.CollidersContainer.localPosition;
        Vector3 add = Softbody.YDelta * Vector3.up;

        c.CollidersContainer.localPosition = curPos - add;
    }

}
