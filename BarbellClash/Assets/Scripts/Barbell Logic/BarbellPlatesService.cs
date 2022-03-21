using System;
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
            
            foreach (var platePrefab in plates)
            {
                if (Mathf.Abs(_minTarget.position.x) >= Mathf.Abs(_maxTarget.position.x))
                {
                    Quaternion rot = Quaternion.Euler(_angle, 0f, 0f);
                    var instance =
                        Instantiate(platePrefab, _minTarget.position, rot, transform);

                    Vector3 nextTarget = _direction.normalized * instance.Thickness;
                    _minTarget.position += nextTarget;
                    
                    _sortedPlates.Add(instance);

                    _angle += 45f;
                }
            }
        }

        public void AddPlate(PlateLogic plate)
        {
            throw new System.NotImplementedException();
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