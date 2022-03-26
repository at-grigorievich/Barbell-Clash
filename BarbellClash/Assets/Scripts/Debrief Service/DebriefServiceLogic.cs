using System;
using System.Linq;
using ATG.LevelControl;
using PlayerLogic;
using UILogic;
using UnityEngine;
using Zenject;

namespace Debrief
{
    public class DebriefServiceLogic : MonoBehaviour, IBonusDetector
    {
        [SerializeField] private SpeedProgressionVisualizer _speedProgressionVisualizer;
        [Inject] private ILevelStatus _levelStatus;
        [Inject] private ILevelSystem _levelSystem;

        public BonusBlock TargetPoint { get; private set; } = null;

        private BonusBlock[] _blocks;
        
        private void Start()
        {
            _levelStatus.OnDebriefStart += OnDebriefStart;

            _blocks = Array.FindAll(_levelSystem.BlockInstances, b => b is BonusBlock)
                .Cast<BonusBlock>()
                .ToArray();
            Array.Sort(_blocks, 
                (f, s) 
                    => f.NeedProgressValue > s.NeedProgressValue ? 1 : -1);
        }

        private void OnDebriefStart(object sender, EventArgs e)
        {
            float curSpeed = _speedProgressionVisualizer.CurrentPercentProgress;

            BonusBlock bb = _blocks.FirstOrDefault(b => b.NeedProgressValue > curSpeed);
            if (bb == null)
            {
                bb = _blocks.Last();
            }

            TargetPoint = bb;
            TargetPoint.EnableBodybuilder();
        }
    }
}