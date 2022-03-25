namespace UILogic
{
    public class LobbyPanel : UIPanel
    {
        public override void Show()
        {
            base.Show();
            
            foreach (var panelElement in elements)
            {
                panelElement.ElementEnable();
            }
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