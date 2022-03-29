using System;
using System.Collections;
using Barbell;
using PlayerLogic;
using UILogic;
using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    public class TutorialPlatesBlock: EnvironmentBlock
    {
        [Inject] private PlayerData.PlayerData _playerData;
        [Inject] private UIControlSystem _uiControl;

        private float _curMovementSpeed;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody == null )
                return;
            
            if(_playerData.CurrentLevel > 1)
                return;
            
            if (other.attachedRigidbody.TryGetComponent(out IKinematic k))
            {
                if (k is BarbellLogic bl)
                {
                    PlayerLogicService player = FindObjectOfType<PlayerLogicService>();

                    StartCoroutine(WaitToDown(player, bl));
                }
            }
        }

        private IEnumerator WaitToDown(PlayerLogicService pl,BarbellLogic bl)
        {
            if (bl.HeightStatus == HeightStatus.Down)
                yield break;

            GamePanel gp = _uiControl.GetPanel<GamePanel>();
            gp.ShowTutorial(true);

            _curMovementSpeed = pl.SpeedParameters.MovementSpeed;
            
            while (bl.HeightStatus == HeightStatus.Up)
            {
                pl.RemoveBoostSpeed(0f);
                yield return null;
            }
            
            gp.ShowTutorial(false);
            pl.RemoveBoostSpeed(_curMovementSpeed);
        }
    }
}