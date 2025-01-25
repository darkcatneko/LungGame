using System.Collections;
using Gamemanager;
using UnityEngine;

public class WallShrinkController : MonoBehaviour
{
    [System.Serializable]
    public class WallShrinkData
    {
        [field: SerializeField] public float maxWidth { get; private set; } = 15f;
        [field: SerializeField] public float[] wallShrinkValue { get; private set; }
        [field: SerializeField] public float shrinkDuration { get; private set; } = 1f; // 控制平滑过渡的持续时间
    }

    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject leftWall;

    public WallShrinkData wallShrinkData;

    [SerializeField] private float curWidth;
    [SerializeField] private int curWallShrinkIndex = 0; // 当前使用的缩放索引

    private bool isShrinking = false; // 防止多次调用导致的过渡冲突

    private void Awake()
    {
        curWidth = wallShrinkData.maxWidth;
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.TriggerWallShrink,
            cmd => { SmoothWallShrink(); });
        
    }

    void SmoothWallShrink()
    {
        StartCoroutine(IESmoothWallShrink(wallShrinkData.wallShrinkValue[curWallShrinkIndex]));
        curWallShrinkIndex++; // 更新索引值
    }
    
    IEnumerator IESmoothWallShrink(float value)
    {
        isShrinking = true; // 标记为正在过渡

        float targetWidth = curWidth - value; // 目标宽度
        float elapsedTime = 0f; // 记录时间
        float startWidth = curWidth; // 记录初始宽度

        while (elapsedTime < wallShrinkData.shrinkDuration)
        {
            elapsedTime += Time.deltaTime;
            curWidth = Mathf.Lerp(startWidth, targetWidth, elapsedTime / wallShrinkData.shrinkDuration); // 线性插值

            // 更新墙壁的位置
            rightWall.transform.position = new Vector3(curWidth, rightWall.transform.position.y, rightWall.transform.position.z);
            leftWall.transform.position = new Vector3(-curWidth, leftWall.transform.position.y, leftWall.transform.position.z);

            yield return null; // 等待下一帧
        }

        // 确保最终到达目标宽度
        curWidth = targetWidth;
        rightWall.transform.position = new Vector3(curWidth, rightWall.transform.position.y, rightWall.transform.position.z);
        leftWall.transform.position = new Vector3(-curWidth, leftWall.transform.position.y, leftWall.transform.position.z);

        isShrinking = false; // 过渡结束
    }
}
