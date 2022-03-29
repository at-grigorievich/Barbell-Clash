using System;
using Barbell;
using Debrief;
using UILogic;
using UnityEngine;

namespace ATG.LevelControl
{
    public class BonusBlock: EnvironmentBlock
    {
        [SerializeField] private Vector3 _addScale;
        [SerializeField] private DebriefBodybuilder _bodybuilder;

        private SpeedProgressionVisualizer _playerLogic;

        [field: SerializeField]
        public float BoostScale { get; private set; }
        
        [field: Range(0f,1f)]
        [field: SerializeField]
        public float NeedProgressValue { get; private set; }

        private Action<BarbellLogic> _addScaleCallback;

        public DebriefBodybuilder Bodybuilder => _bodybuilder;
        
        private void Start()
        {
            _playerLogic = FindObjectOfType<SpeedProgressionVisualizer>();
            
            _bodybuilder.gameObject.SetActive(false);
            _addScaleCallback = (bl) =>
            {
                _addScaleCallback = null;
                
                bl.AddScale(_addScale);
                _playerLogic.RemoveFillPercent();
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
        {
            _bodybuilder.gameObject.SetActive(true);
        }

        public void ActivateBodybuilder(Transform target)
        {
            _bodybuilder.StartSquat(BoostScale,target);
        }
    }
}