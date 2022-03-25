using System;
using System.Linq;
using ATG.LevelControl;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Debrief
{
    public class DebriefServiceLogic : MonoBehaviour, IBonusDetector
    {
        [SerializeField] private PlayerLogicService _player;
        
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
        }

        private void OnDebriefStart(object sender, EventArgs e)
        {
            float curSpeed = _player.SpeedParameters.MovementSpeed;

            int bb = Array.FindIndex(_blocks, b => b.NeedSpeed > curSpeed);
            
            if (bb == -1)
            {
                TargetPoint = _blocks.Last();
                _blocks.Last().EnableBodybuilder();
                return;
            }

            if (bb == 0)
            {
                TargetPoint = _blocks.First();
                _blocks.First().EnableBodybuilder();
                return;
            }

            TargetPoint = _blocks[bb - 1];
            _blocks[bb - 1].EnableBodybuilder();
        }
    }
}