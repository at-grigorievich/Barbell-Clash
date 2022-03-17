using System;
using ATG.LevelControl;
using ATGStateMachine;
using Barbell;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    [RequireComponent(typeof(Camera))]
    public class PlayerLogicService: StatementBehaviour<PlayerLogicService>, IControllable
    {
        private Camera _camera;
        
        public Transform MyTransform => transform;
        

        [Inject]
        private void Constructor(ILevelSystem levelSystem)
        {
            _camera = GetComponent<Camera>();
            
            if (levelSystem.CurrentLevel is BarbellLevelData barbellLevelData)
            {
                BarbellLogic bl = barbellLevelData.BarbellInitialConfig.InstantiateBarbell();
                barbellLevelData.CameraInitialConfig.SetupCamera(_camera);
            }
            else
                throw new Exception($"{levelSystem.CurrentLevel.Id} level " +
                                    $"isnt barbell level data !");
        }
    }
}