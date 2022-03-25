using System;
using Barbell;
using UnityEngine;

namespace ATG.LevelControl
{
    public class BonusBlock: EnvironmentBlock
    {
        [SerializeField] private Vector3 _addScale;
        
        [field: SerializeField]
        public int BoostIndex { get; private set; }
        
        [field: SerializeField]
        public float NeedSpeed { get; private set; }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody != null)
            {
                if (other.attachedRigidbody.TryGetComponent(out BarbellLogic bl))
                {
                    bl.AddScale(_addScale);
                }
            }
        }
    }
}