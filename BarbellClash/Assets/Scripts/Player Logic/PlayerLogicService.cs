using System;
using ATG.LevelControl;
using ATGStateMachine;
using Barbell;
using Debrief;
using UILogic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace PlayerLogic
{
    [Serializable]
    public class SpeedValues
    {
        [field: SerializeField]
        public float MovementSpeed { get; set; }
        
        [field: Space(10)]
        
        [field: SerializeField]
        public float DropSpeed { get; private set; }
    }
    
    public class PlayerLogicService: StatementBehaviour<IControllable>, IControllable, IBoostable
    {
        [SerializeField] private GameObject _cinemachineObject;

        [field: SerializeField] 
        public BoostParametersContainer BoostData { get; private set; }
        
        [field: SerializeField]
        public SpeedValues SpeedParameters { get; private set; }

        [field: SerializeField]
        public SpeedProgressionVisualizer SpeedProgressionVisualizer { get; private set; }

        public ICinemachinable CinemachineService { get; private set; }
        public IInputable InputService { get; private set; }
        
        public Transform MyTransform => transform;

        private void Awake()
        {
            SpeedProgressionVisualizer.Init(BoostData);
        }

        [Inject]
        private void Constructor(ILevelSystem levelSystem,ILevelStatus lvlStat, IInputable inputService,
            BarbellLogic.Factory barbelFactory,ICinemachinable camService, IBonusDetector bd)
        {
            InputService = inputService;
            CinemachineService = camService;

            if (levelSystem.CurrentLevel is BarbellLevelData barbellLevelData)
            {
                BarbellLogic bl = barbellLevelData.BarbellInitialConfig.InstantiateBarbell(barbelFactory);
                
                CinemachineService.InitCinemachine(bl.transform,barbellLevelData.CameraInitialConfig);
                lvlStat.OnDebriefStart += (sender, args) => CinemachineService.SetFOV(100f);
                
                
                AllStates.Add(new PlayerBriefState(this,this,lvlStat));
                AllStates.Add(new PlayerMoveState(this,this,bl,lvlStat));
                AllStates.Add(new PlayerDebriefState(this,this,bl,bd));
                InitStartState();
                OnState();
            }
            else
                throw new Exception($"{levelSystem.CurrentLevel.Id} level " +
                                    $"isnt barbell level data !");
        }

        private void Update()
        {
            SpeedProgressionVisualizer.UpdateValue(SpeedParameters.MovementSpeed);
            OnExecute();
        }

        public void AddBoostSpeed()
        {
            SpeedParameters.MovementSpeed
                = BoostData.BoostCurrentSpeed(SpeedParameters.MovementSpeed);
        }

        public void RemoveBoostSpeed()
        {
            SpeedParameters.MovementSpeed = 
                BoostData.DebuffCurrentSpeed(SpeedParameters.MovementSpeed);
        }
    }
}