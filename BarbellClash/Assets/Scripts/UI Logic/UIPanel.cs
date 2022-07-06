using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UILogic
{
    [RequireComponent(typeof(Canvas))]
    public abstract class UIPanel : MonoBehaviour, IPanel
    {
        [SerializeField] protected List<PanelElement> elements;
        
        private float _disableDelay = 0.3f;
        private Canvas _rect;
        
        private void Awake()
        {
            _rect = GetComponent<Canvas>();
            _rect.enabled = false;
        }

        public virtual void Show()
        {
            _rect.enabled = true;
        }

        public virtual void Hide()
        {
            StartCoroutine(WaitToDisable());
        }

        private IEnumerator WaitToDisable()
        {
            yield return new WaitForSeconds(_disableDelay);
            _rect.enabled = false;
        }
    }
}