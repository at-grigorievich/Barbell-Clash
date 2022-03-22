using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Barbell
{
    public class BarbellPlatesService : MonoBehaviour, IPlateContainer
    {
        [Space(10)]
        [SerializeField] private PlateDataContainer _defaultPlates;

        [Space(10)] 
        [SerializeField] private Vector3 _direction;
        [SerializeField] private Transform _minTarget;
        [SerializeField] private Transform _maxTarget;

        [Space(10)] [Header("Animation Values")] 
        [SerializeField] private float _delay;
        [SerializeField] private float _punchDuration;
        [SerializeField] private int _punchVibrato;
        [SerializeField] private float _punchElasticy;
        [SerializeField] private Vector3 _punchVector;
        
        private SortedSet<PlateLogic> _sortedPlates = 
            new SortedSet<PlateLogic>(new DescendingContainer());

        private Dictionary<PlateLogic, Tween> _rotatePlates 
            = new Dictionary<PlateLogic, Tween>();

        private float _angle;

        public ISizeable PlateWithMaxRadius => _sortedPlates.Count > 0 ? _sortedPlates.Last() : null;

        public void InitDefaultPlate()
        {
            PlateLogic[] plates = _defaultPlates.Plates;
            Array.Sort(plates,new DescendingContainer());

            if (plates.Length > 0)
            {
                AddPlate(plates[0]);
            }
        }

        public void AddPlate(PlateLogic plate)
        {
            if (_sortedPlates.Count == 0)
            {
                StartCoroutine(AnimatePlateAdding(plate));
                return;
            }
            
            if (_sortedPlates.Count > 0 && plate.Id == _sortedPlates.Last().Id)
            {
                StartCoroutine(AnimateCurrentPlates());
            }
            else if(_sortedPlates.Count > 0 && plate.Id != _sortedPlates.Last().Id)
            {
                RemoveAllPlates();
                StartCoroutine(AnimatePlateAdding(plate));
            }
        }

        [ContextMenu("Test Animate")]
        public void TestAnimate()
        {
            foreach (var sortedPlate in _sortedPlates)
            {
                sortedPlate.transform
                    .DOPunchScale(_punchVector, _punchDuration,_punchVibrato,_punchElasticy);
            }
        }
        
        public void StartRotatePlates()
        {
            Vector3 rot = new Vector3(180f, 0f, 0f);

            foreach (var sortedPlate in _sortedPlates)
            {
                if (_rotatePlates.ContainsKey(sortedPlate))
                    continue;
                
                Tween rotTween = sortedPlate.transform
                    .DORotate(rot, sortedPlate.RotateSpeed,RotateMode.LocalAxisAdd)
                    .SetLoops(-1)
                    .SetEase(Ease.Linear);

                rotTween.Play();
                _rotatePlates.Add(sortedPlate, rotTween);
            }
        }
        public void StopRotatePlates()
        {
            if(_rotatePlates.Count == 0)
                return;
            
            IEnumerable<Tween> rotatesTweens = _rotatePlates.Values;

            foreach (var tween in rotatesTweens)
            {
                tween.Kill();
            }
            
            _rotatePlates.Clear();
        }


        private void RemoveAllPlates()
        {
            StopAllCoroutines();
            Vector3 resetPos = _minTarget.position;
            resetPos.x = _sortedPlates.First().transform.position.x;

            _minTarget.position = resetPos;
            
            foreach (var sortedPlate in _sortedPlates)
            {
                GameObject.Destroy(sortedPlate.gameObject);
            }
            _sortedPlates.Clear();
        }
        
        private void AnimateScale(PlateLogic instance) => 
            instance.transform
                .DOPunchScale(_punchVector, _punchDuration,_punchVibrato,_punchElasticy);

        private IEnumerator AnimatePlateAdding(PlateLogic plate)
        {
            while (Mathf.Abs(_minTarget.position.x) >= Mathf.Abs(_maxTarget.position.x))
            {
                Quaternion rot = Quaternion.Euler(_angle, 0f, 0f);
                var instance =
                    Instantiate(plate, _minTarget.position, rot, transform);

                Vector3 nextTarget = _direction.normalized * instance.Thickness;
                _minTarget.position += nextTarget;

                
                _sortedPlates.Add(instance);

                yield return new WaitForSeconds(_delay);
                AnimateScale(instance);
                _angle += 45f;
            }
        }

        private IEnumerator AnimateCurrentPlates()
        {
            var plates = (PlateLogic[])_sortedPlates.ToArray<PlateLogic>().Clone();
            foreach (var sortedPlate in plates)
            {
                if (sortedPlate != null)
                {
                    AnimateScale(sortedPlate);
                    yield return new WaitForSeconds(_delay);
                }
            }
        }
        
        private class DescendingContainer: IComparer<PlateLogic>
        {
            public int Compare(PlateLogic x, PlateLogic y)
            {
                if (x.Radius > y.Radius)
                    return 1;
                if (x.Radius < y.Radius)
                    return -1;
                return 1;
            }
        }
    }
}