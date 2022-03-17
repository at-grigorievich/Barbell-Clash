namespace ATG.PlayerData
{
    public static class PlayerItemsParser
    {
        private const char Separator = '.';

        public static string CreateNewItem(int itemId) => itemId.ToString() + Separator;
    }
}