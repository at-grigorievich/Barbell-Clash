using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Barbell
{
    [RequireComponent(typeof(BoxCollider))]
    public class PlateSpawnerLogic : MonoBehaviour
    {
        [SerializeField] private Transform[] _targets;

        [SerializeField] private PlateDataContainer _plates;

        [Header("Animation parameters")] [Space(10)] 
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _jumpStrength;

        private BoxCollider _collider;

        private List<PlateLogic> _platesInstances = new List<PlateLogic>();
        private List<Sequence> _destroyPlatesSequences = new List<Sequence>();

        private Action<IStackable> _dropPlates;
        private Action _destroyInstances;
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _collider.isTrigger = true;
            
            InstantiatePlates();

            _dropPlates = AddPlatesToBarbell;
        }
        

        private void OnTriggerStay(Collider other)
        {
            if (other.attachedRigidbody.TryGetComponent(out IStackable stack))
            {
                if (stack is BarbellLogic bl)
                {
                    if (bl.HeightStatus == HeightStatus.Down)
                    {
                        _dropPlates?.Invoke(stack);
                    }
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.attachedRigidbody.TryGetComponent(out IStackable stack))
            {
                _destroyInstances?.Invoke();
            }
        }

        private void InstantiatePlates()
        {
            for (var i = 0; i < _targets.Length; i++)
            {
                Vector3 pos = _targets[i].position;
                Quaternion rot = _targets[i].rotation;
                
                Vector3 upDelay = Vector3.zero;
                
                foreach (var plate in _plates.Plates)
                {
                    var instance = Instantiate(plate, pos+upDelay, rot, transform);
                    instance.transform.localScale = instance.MeshSize;
                    _platesInstances.Add(instance);

                    upDelay += Vector3.up * plate.Thickness;
                }
            }
        }

        
        private void AddPlatesToBarbell(IStackable stack)
        {
            _dropPlates = null;
            _destroyInstances = DestroyPlatesInstances;
            
            PlateLogic plate = _plates.Plates[0];
            stack.AddPlate(plate);

            AnimateDestroying();
            
            Taptic.Vibrate();
        }
        private void DestroyPlatesInstances()
        {
            _destroyInstances = null;
            
            for (var i = 0; i < _platesInstances.Count; i++)
            {
                _destroyPlatesSequences[i].Kill();
                GameObject.Destroy(_platesInstances[i].gameObject);
            }
            
            _platesInstances.Clear();
            _destroyPlatesSequences.Clear();
        }
        
        private void AnimateDestroying()
        {
            foreach (var platesInstance in _platesInstances)
            {
                Vector3 endJump = platesInstance.transform.position;

                var seq =DOTween.Sequence()
                    .Append(platesInstance.transform.DOShakeRotation(_jumpDuration, _shakeDuration*90f))
                    .Join(platesInstance.transform.DOJump(endJump, _jumpStrength, 1, _jumpDuration)
                        .SetEase(Ease.OutExpo));
                _destroyPlatesSequences.Add(seq);
            }
        }
    }
}