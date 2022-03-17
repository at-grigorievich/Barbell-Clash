using System;
using UnityEngine;

namespace Barbell
{
    [Serializable]
    public class BarbellConfigContainer
    {
        [Space(5)]
        [SerializeField] private BarbellLogic _barbellPrefab;
        [Space(5)] 
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _rotation;

        public BarbellLogic InstantiateBarbell()
        {
            BarbellLogic instance =
                GameObject.Instantiate(_barbellPrefab, _position, Quaternion.Euler(_rotation));

            return instance;
        }
    }
}