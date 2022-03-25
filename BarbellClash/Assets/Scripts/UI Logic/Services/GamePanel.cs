using ATG.PlayerData;
using TMPro;
using TweenStatic;
using UnityEngine;
using Zenject;

namespace UILogic
{
    public class GamePanel : UIPanel
    {
        [Inject] private PlayerData _levelSystem;

        [SerializeField] private TextMeshProUGUI _lvlData;
        
        public override void Show()
        {
            string lvl = $"Level {_levelSystem.CurrentLevel}";
            _lvlData.SetText(lvl);
            foreach (var panelElement in elements)
            {
                panelElement.ElementEnable();
            }
            base.Show();
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