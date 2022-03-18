using System;
using ATG.LevelControl;
using ATGStateMachine;
using Barbell;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    [Serializable]
    public class SpeedValues
    {
        [field: SerializeField]
        public float MovementSpeed { get; private set; }
        
        [field: Space(10)]
        
        [field: SerializeField]
        public float DropSpeed { get; private set; }
    }
    
    public class PlayerLogicService: StatementBehaviour<IControllable>, IControllable
    {
        [field: SerializeField]
        public SpeedValues SpeedParameters { get; private set; }

        public ICinemachinable CinemachineService { get; private set; }
        public IInputable InputService { get; private set; }
        
        public Transform MyTransform => transform;


        [Inject]
        private void Constructor(ILevelSystem levelSystem, IInputable inputService, 
            ICinemachinable camService)
        {
            InputService = inputService;
            CinemachineService = camService;
            
            if (levelSystem.CurrentLevel is BarbellLevelData barbellLevelData)
            {
                BarbellLogic bl = barbellLevelData.BarbellInitialConfig.InstantiateBarbell();
                
                CinemachineService.InitCinemachine(bl.transform,barbellLevelData.CameraInitialConfig);
                
                AllStates.Add(new PlayerBriefState(this,this));
                AllStates.Add(new PlayerMoveState(this,this));
                
                InitStartState();
                OnState();
            }
            else
                throw new Exception($"{levelSystem.CurrentLevel.Id} level " +
                                    $"isnt barbell level data !");
        }

        private void Update()
        {
            OnExecute();
        }
    }
}