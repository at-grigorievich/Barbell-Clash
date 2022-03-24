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

    private void UpBerbellCollider(ICrushable c)
    {
        _upBerbellCollider = null;
        _downBarbellCollider = DownBarbellCollider;
        
        foreach (var cCollider in c.Colliders)
        {
            if (cCollider is BoxCollider collider)
            {

                Vector3 pos = collider.center;
                Vector3 add = Vector3.up * Softbody.YDelta;
                collider.center = pos + add;
            }
            
            if (cCollider is CapsuleCollider col)
            {

                Vector3 pos = col.center;
                Vector3 add = Vector3.up * Softbody.YDelta;
                col.center = pos + add;
            }
        }
    }

    private void DownBarbellCollider(ICrushable c)
    {
        _downBarbellCollider = null;
        _upBerbellCollider = null;
        
        foreach (var cCollider in c.Colliders)
        {
            if (cCollider is BoxCollider collider)
            {

                Vector3 pos = collider.center;
                Vector3 add = Vector3.up * Softbody.YDelta;
                collider.center = pos - add;
            }
            
            if (cCollider is CapsuleCollider col)
            {

                Vector3 pos = col.center;
                Vector3 add = Vector3.up * Softbody.YDelta;
                col.center = pos - add;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_curKinematic != null)
            return;
        
        if (other.attachedRigidbody.TryGetComponent(out ICrushable k))
        {
            Softbody.AnimateSoftbodyCrush();
            _curKinematic = k;
            
            if (k.MaxPlateId == Softbody.NeededPlateId)
            {
                Softbody.SetSoftbodyActive(true);
                _upBerbellCollider?.Invoke(k);
            }
            else if(k.MaxPlateId < Softbody.NeededPlateId)
            {
                Softbody.DoDie();
                ChangeBarbellIgnoreMovement(k,true);
            }
            else
            {
                Softbody.DoDisableDetecting();
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
                _downBarbellCollider?.Invoke(k);
                
                if (k.MaxPlateId < Softbody.NeededPlateId)
                {
                    ChangeBarbellIgnoreMovement(k, false);
                }
            }
        }
    }

    private void ChangeBarbellIgnoreMovement(ICrushable k, bool isIgnore)
    {
        if (k is IKinematic kinematic)
        {
            kinematic.SetUpdateMovement(isIgnore);
        }
    }
}
