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
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.game_start,
                currentSession.ToString());
        }

        public void OnLevelStartEvent(int level)
        {
#if UNITY_EDITOR
            
            Debug.Log($"Send level start event: {level}");
#endif
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.level_start,level.ToString());

            _startLevelTime = Time.time;
        }

        public void OnLevelCompleteEvent(int level)
        {
            float resTime = Time.time - _startLevelTime;
            
#if UNITY_EDITOR
            Debug.Log($"Send level complete event {resTime}");
#endif
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.level_complete,
                level.ToString(),resTime.ToString());
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