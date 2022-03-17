using System;
using UnityEngine;

namespace ATG.PlayerData
{
    public enum PlayerInfo
    {
        Level_info,
        Money_info,
        Items_info
    }
    
    public class PlayerData
    {
        public int CurrentLevel { get; private set; }
        public int PreviousLevel => CurrentLevel > 1 ? CurrentLevel - 1 : 1;
        
        public int CurrentMoney { get; private set; }
        
        public string CurrentItems { get; private set; }
        

        public PlayerData()
        {
            CurrentLevel = GetDataFromPrefs(PlayerInfo.Level_info, 1);
            CurrentMoney = GetDataFromPrefs(PlayerInfo.Money_info, 0);
            CurrentItems = GetDataFromPrefs(PlayerInfo.Items_info, string.Empty);
        }

        public void UpdateLevel()
        {
            CurrentLevel++;
            PlayerPrefs.SetInt(PlayerInfo.Level_info.ToString(),CurrentLevel);
        }

        public void UpdateMoney(int addValue)
        {
            CurrentMoney += addValue;
            PlayerPrefs.SetInt(PlayerInfo.Money_info.ToString(),CurrentMoney);
        }

        public void UpdateItems(int addId)
        {
            CurrentItems += PlayerItemsParser.CreateNewItem(addId);
            PlayerPrefs.SetString(PlayerInfo.Items_info.ToString(),CurrentItems);
        }

        protected T GetDataFromPrefs<T>(PlayerInfo type, T defaultValue)
        {
            Func<object> resultCallback;

            switch (defaultValue)
            {
                case int b:
                    resultCallback = () => PlayerPrefs.GetInt(type.ToString());
                    break;
                case float f:
                    resultCallback = () => PlayerPrefs.GetFloat(type.ToString());
                    break;
                case string s:
                    resultCallback = () => PlayerPrefs.GetString(type.ToString());
                    break;
                default:
                    throw new ArgumentException($"Cant support '{typeof(T)}' prefs type");

            }
            
            return PlayerPrefs.HasKey(type.ToString()) ? (T)resultCallback() : defaultValue;
        }
    }
}