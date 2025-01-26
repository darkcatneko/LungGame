using System.Collections;
using Game.Audio;
using Gamemanager;
using UnityEngine;

public class WallShrinkController : MonoBehaviour
{
    [System.Serializable]
    public class WallShrinkData
    {
        [field: SerializeField] public float maxWidth { get; private set; } = 15f;
        [field: SerializeField] public float[] wallShrinkValue { get; private set; }
        [field: SerializeField] public float shrinkDuration { get; private set; } = 1f; // 控制平滑過渡的持續時間
    }

    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject leftWall;

    public WallShrinkData wallShrinkData;

    [SerializeField] private float curWidth;
    [SerializeField] private int curWallShrinkIndex = 0; // 當前使用的縮放索引

    [SerializeField] private AudioData wallShrinkAudioData;

    private Coroutine shrinkCoroutine; // 用於追蹤當前過渡協程

    private void Awake()
    {
        curWidth = wallShrinkData.maxWidth;
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.TriggerWallShrink, cmd => { SmoothWallShrink(); });
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnPlayerHurt, cmd => { PlayerHurtWallShrink(); });
    }

    void SmoothWallShrink()
    {
        if (curWallShrinkIndex < wallShrinkData.wallShrinkValue.Length)
        {
            StartShrink(wallShrinkData.wallShrinkValue[curWallShrinkIndex]);
            curWallShrinkIndex++; // 更新索引值
        }
    }

    void PlayerHurtWallShrink()
    {
        StartShrink(1f); // 玩家受傷時的縮放值
    }

    void StartShrink(float value)
    {
        // 如果有正在執行的協程，先停止
        if (shrinkCoroutine != null)
        {
            StopCoroutine(shrinkCoroutine);
        }

        // 啟動新的縮放協程
        shrinkCoroutine = StartCoroutine(IESmoothWallShrink(value));
    }

    IEnumerator IESmoothWallShrink(float value)
    {
        float targetWidth = Mathf.Max(curWidth - value, 1f); // 確保目標寬度不低於 1f
        float elapsedTime = 0f; // 記錄時間
        float startWidth = curWidth; // 記錄初始寬度
        
        while (elapsedTime < wallShrinkData.shrinkDuration)
        {
            elapsedTime += Time.deltaTime;
            curWidth = Mathf.Lerp(startWidth, targetWidth, elapsedTime / wallShrinkData.shrinkDuration); // 線性插值

            // 更新牆壁的位置
            rightWall.transform.position = new Vector3(curWidth, rightWall.transform.position.y, rightWall.transform.position.z);
            leftWall.transform.position = new Vector3(-curWidth, leftWall.transform.position.y, leftWall.transform.position.z);

            yield return null; // 等待下一幀
        }

        // 確保最終到達目標寬度
        curWidth = targetWidth;
        rightWall.transform.position = new Vector3(curWidth, rightWall.transform.position.y, rightWall.transform.position.z);
        leftWall.transform.position = new Vector3(-curWidth, leftWall.transform.position.y, leftWall.transform.position.z);

        AudioManager.Instance.PlayRandomSFX(wallShrinkAudioData);

        if (curWidth == 1.0f)
        {
            GameManager.Instance.MainGameEvent.Send(new GameOver());
        }
        
        shrinkCoroutine = null; // 過渡結束後清空協程引用
    }
}
