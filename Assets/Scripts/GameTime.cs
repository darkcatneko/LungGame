using System.Collections;
using System.Collections.Generic;
using Gamemanager;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelTimeWallShrinkData
    {
        [field: SerializeField] public float maxTime { get; set; } // 最大時間
        [field: SerializeField] public float[] levelTime { get; private set; } // 每個階段的時間點
    }

    public LevelTimeWallShrinkData timeData;

    [SerializeField] private float curTime; // 當前時間
    private HashSet<float> triggeredTimes = new HashSet<float>(); // 已觸發的時間點，避免重複觸發

    private void Start()
    {
        curTime = timeData.maxTime; // 初始化當前時間為最大時間
    }

    private void Update()
    {
        if (curTime > 0)
        {
            curTime -= Time.deltaTime; // 每幀減少時間
            CheckLevelTimes(); // 檢查是否達到了某個時間點
        }
        else
        {
            if (curTime < 0) curTime = 0; // 確保時間不會低於0
            OnTimeUp(); // 當時間結束時觸發的事件
        }
    }

    private void CheckLevelTimes()
    {
        foreach (float levelTime in timeData.levelTime)
        {
            // 如果當前時間下降到指定的 levelTime 且尚未觸發
            if (curTime <= levelTime && !triggeredTimes.Contains(levelTime))
            {
                TriggerLevelTime(levelTime); // 觸發事件
                triggeredTimes.Add(levelTime); // 標記為已觸發
            }
        }
    }

    private void TriggerLevelTime(float levelTime)
    {
        // 觸發對應 levelTime 的方法
        GameManager.Instance.MainGameEvent.Send(new WallShrink());
        Debug.Log($"Triggered at levelTime: {levelTime}");
        // 在這裡添加你想要執行的具體邏輯
    }

    private void OnTimeUp()
    {
        Debug.Log("Time is up!"); 
        // 在這裡添加時間結束時的邏輯
    }
}
