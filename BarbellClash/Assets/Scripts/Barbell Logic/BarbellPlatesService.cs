using System;
using System.Collections.Generic;
using System.Linq;
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

        public ISizeable PlateWithMaxRadius => _sortedPlates.Count > 0 ? _sortedPlates.Last() : null;
        
        
        public void InitDefaultPlate()
        {
            PlateLogic[] plates = _defaultPlates.Plates;
            Array.Sort(plates,new DescendingContainer());
            
            foreach (var platePrefab in plates)
            {
                if (Mathf.Abs(_minTarget.position.x) >= Mathf.Abs(_maxTarget.position.x))
                {
                    var instance =
                        Instantiate(platePrefab, _minTarget.position, Quaternion.identity, transform);

                    Vector3 nextTarget = _direction.normalized * instance.Thickness;
                    _minTarget.position += nextTarget;

                    _sortedPlates.Add(instance);
                }
            }
        }

        public void AddPlate(PlateLogic plate)
        {
            throw new System.NotImplementedException();
        }
        
        private class DescendingContainer: IComparer<PlateLogic>
        {
            public int Compare(PlateLogic x, PlateLogic y)
            {
                if (x.Radius > y.Radius)
                    return 1;
                if (x.Radius < y.Radius)
                    return -1;
                return 0;
            }
        }
    }
}