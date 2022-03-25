using System;
using UnityEngine;

namespace UILogic
{
    [RequireComponent(typeof(TweenSingleMovement))]
    public class PanelElement : MonoBehaviour
    {
        [SerializeField] private int MoveEnableIndex;
        [SerializeField] private int MoveDisableIndex;

        private TweenSingleMovement _movement;

        private void Awake()
        {
            _movement = GetComponent<TweenSingleMovement>();
        }

        public void ElementEnable() => _movement.MoveTo(MoveEnableIndex);
        public void ElementDisable() => _movement.MoveTo(MoveDisableIndex);
        
    }
}