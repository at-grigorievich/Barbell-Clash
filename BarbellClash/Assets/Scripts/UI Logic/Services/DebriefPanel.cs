using System;
using ATG.LevelControl;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UILogic
{
    public enum DebriefType
    {
        CompleteDebrief,
        FailedDebrief
    }
    
    public class DebriefPanel : UIPanel
    {
        [Inject] private ILevelSystem _levelSystem;
        
        [Header("Buttons")]
        [SerializeField] private Button levelButton;
        [SerializeField] private Button replayButton;
        [Header("Panels")]
        [SerializeField] private RectTransform completeDebriefUI;
        [SerializeField] private RectTransform failedDebriefUI;
        
        public DebriefType DebriefType { get; set; }
        
        private void Start()
        {
            levelButton.onClick.AddListener(() => _levelSystem.LoadNextLevel());
            replayButton.onClick.AddListener(() => _levelSystem.ReloadLevel());
        }

        public override void Show()
        {
            base.Show();

            switch (DebriefType)
            {
                case DebriefType.CompleteDebrief:
                    completeDebriefUI.gameObject.SetActive(true);
                    failedDebriefUI.gameObject.SetActive(false);
                    break;
                case DebriefType.FailedDebrief:
                    completeDebriefUI.gameObject.SetActive(false);
                    failedDebriefUI.gameObject.SetActive(true);
                    break;
            }
            
            foreach (var panelElement in elements)
            {
                panelElement.ElementEnable();
            }
            //levelButton.GetComponent<RectTransform>().DOPunchScale(0.3f * Vector3.one, 1f, 5, 1f);
        }
        public override void Hide()
        {
            foreach (var panelElement in elements)
            {
                panelElement.ElementDisable();
            }
            base.Hide();
        }
    }
}