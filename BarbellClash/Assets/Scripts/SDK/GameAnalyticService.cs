using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

namespace ATG.SDK
{
    public class GameAnalyticService : MonoBehaviour
    {
        public const string SessionCount = "session_count";

        private float _startLevelTime;
        
        void Start()
        {
            GameAnalytics.Initialize();
            OnGameStartEvent();
        }

        public void OnGameStartEvent()
        {
            int currentSession = GetSessionCount();

#if UNITY_EDITOR
            
            Debug.Log($"Send game start event: {currentSession}");
#endif
           // GameAnalytics.NewProgressionEvent(GAProgressionStatus.game_start,
            //currentSession.ToString());
            //GameAnalytics.NewDesignEvent("game_start:session_count",currentSession);
               
            GameAnalytics.NewProgressionEvent(
                GAProgressionStatus.Start,"game_start","session_count",currentSession);
        }

        public void OnLevelStartEvent(int level)
        {
#if UNITY_EDITOR
            
            Debug.Log($"Send level start event: {level}");
#endif
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.level_start,level.ToString());
            //GameAnalytics.NewDesignEvent("level_start:level",level);
            
            GameAnalytics.NewProgressionEvent(
                GAProgressionStatus.Start,"level_start","level",level);
            
            _startLevelTime = Time.time;
        }

        public void OnLevelCompleteEvent(int level)
        {
            float resTime = Time.time - _startLevelTime;
            int res = (int) resTime;
            
#if UNITY_EDITOR
            Debug.Log($"Send level complete event {resTime}");
#endif
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.level_complete,
               // level.ToString(),res.ToString());
               
               Dictionary<string, object> fields = new Dictionary<string, object>();
               fields.Add("level", level);
               fields.Add("time_spent", res);
               
               GameAnalytics.NewProgressionEvent(
                   GAProgressionStatus.Complete,"level_complete",fields);
        }
        
        private int GetSessionCount()
        {
            int sCount = 1;
            
            if (PlayerPrefs.HasKey(SessionCount))
            {
                sCount = PlayerPrefs.GetInt(SessionCount);
                sCount++;
            }
            
            PlayerPrefs.SetInt(SessionCount,sCount);
            return sCount;
        }
    }
}