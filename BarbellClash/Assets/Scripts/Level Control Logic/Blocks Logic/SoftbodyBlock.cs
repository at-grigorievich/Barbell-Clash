using System;
using ATG.LevelControl;
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
    
    public SoftbodyLogic Softbody { get; private set; }
    
    [Inject]
    private void Constructor(SoftbodyLogic.Factory factory)
    {
        Softbody = GameObject.Instantiate(_softbodyInstance,transform);
        Softbody.transform.position = _instanceOptions.TargetPosition.position;
        Softbody.transform.rotation = _instanceOptions.TargetPosition.rotation;
    }
}
