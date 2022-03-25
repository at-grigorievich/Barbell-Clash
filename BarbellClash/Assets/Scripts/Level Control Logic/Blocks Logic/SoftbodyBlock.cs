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

    [Inject] private IBoostable _boostService;
    
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

                if (k is BarbellLogic bl)
                {
                    if (bl.HeightStatus == HeightStatus.Down)
                    {
                        _boostService.RemoveBoostSpeed();
                    }
                }
            }
            else
            {
                Softbody.DoDisableDetecting();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent(out BarbellLogic crushable))
        {
            if (ReferenceEquals(_curKinematic, crushable))
            {
                if (crushable.MaxPlateId == Softbody.NeededPlateId 
                    && crushable.HeightStatus == HeightStatus.Down)
                {
                    _boostService.AddBoostSpeed();
                }
                else if (crushable.HeightStatus == HeightStatus.Down 
                         && crushable.MaxPlateId < Softbody.NeededPlateId)
                {
                    _boostService.RemoveBoostSpeed();
                }
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
