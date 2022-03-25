using System;
using Barbell;
using Debrief;
using PlayerLogic;
using UnityEngine;

namespace ATG.LevelControl
{
    public class BonusBlock: EnvironmentBlock
    {
        [SerializeField] private Vector3 _addScale;
        [SerializeField] private DebriefBodybuilder _bodybuilder;
        
        [field: SerializeField]
        public int BoostIndex { get; private set; }
        
        [field: Range(0f,1f)]
        [field: SerializeField]
        public float NeedProgressValue { get; private set; }

        private Action<BarbellLogic> _addScaleCallback;

        private PlayerLogicService _player;
        
        private void Start()
        {
            _player = FindObjectOfType<PlayerLogicService>();
            
            _bodybuilder.gameObject.SetActive(false);
            _addScaleCallback = (bl) =>
            {
                _addScaleCallback = null;
                bl.AddScale(_addScale);
            };
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody != null)
            {
                if (other.attachedRigidbody.TryGetComponent(out BarbellLogic bl))
                {
                    _addScaleCallback?.Invoke(bl);
                }
            }
        }

        public void EnableBodybuilder()
        { }

        public void ActivateBodybuilder(GameObject gameObject)
        {
            //_bodybuilder.StartSquat(BoostIndex,gameObject.transform);
        }
    }
}