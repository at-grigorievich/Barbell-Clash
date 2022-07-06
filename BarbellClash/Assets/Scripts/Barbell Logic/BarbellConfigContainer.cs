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

        public BarbellLogic InstantiateBarbell(BarbellLogic.Factory factory)
        {

            BarbellLogic instance = factory.Create(_barbellPrefab);
            instance.transform.position = _position;
            instance.transform.rotation = Quaternion.Euler(_rotation);
            
            return instance;
        }
    }
}