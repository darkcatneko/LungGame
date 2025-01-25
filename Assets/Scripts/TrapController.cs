using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    //總生成時間
    public float createTimeMax = 20f;
    //生成間格
    public float createTime = 5f;
    //現在時間
    public float nowTime = 0f;
    //總時間
    public float totalTime = 0f;
    //最大生成個數
    public int trapMax = 10;
    //現在trap數
    public int nowTrap = 0;
    //生成關卡
    public int nowLevel = 0;
    //生成範圍最大區間
    [SerializeField] public int rangeX;
    //生成範圍的時間
    [SerializeField] public int nowShinkTime;
    //生成範圍的時間
    [SerializeField] public int nowShinkRange;
    //主要陷阱的prefab
    [SerializeField] public List<GameObject> trapPrefab;
    //管理的pool,debug開啟來看
    [SerializeField] private List<Trap> trapPool = new List<Trap>();
    private Queue<Trap> activeTraps = new Queue<Trap>();
    [SerializeField] public Transform playerPosition;
    [SerializeField] public WallShrinkController WallShrinkData;

    //[SerializeField] public    // Start is called before the first frame update
    void Start()
    {
        trapAllCreateManager();
    }

    // Update is called once per frame
    void Update()
    {
        //trapTimeManager();        
    }

    private void trapAllCreateManager()
    {
        for (int i = 0; i < trapMax; i++)
        {
            if (i % 10 == 0)
            {
                nowShinkTime += 1;
            }
            spawnNewTrap(i);
        }
    }

    // private void trapTimeManager()
    // {

    //     if(totalTime<createTimeMax)
    //     {
    //         nowTime += Time.deltaTime;
    //         if (nowTime >= createTime && nowTrap < trapMax)
    //         {
    //             totalTime += nowTime;
    //             spawnNewTrap();
    //             nowTime = 0f;
    //         }
    //     }

    // }

    public Vector3 randomPos(int i)
    {
        Debug.Log(WallShrinkData.wallShrinkData.wallShrinkValue[nowShinkTime]);
        Debug.LogWarning("result" + (rangeX - getWallShrinkTotal()));
        float randomPosX = Random.Range(0, rangeX - getWallShrinkTotal() + 1);//16秒-0 20秒-10 24秒-20  15 10 2 1  //0 5 4 2 1WallShrinkData.wallShrinkValue[nowShinkTime]
        randomPosX = Mathf.Clamp(randomPosX, 1, 16);//這裡clamp是因為不想讓玩家在最後一定會撞到
        randomPosX = Random.Range(0, 2) == 0 ? randomPosX : -randomPosX;
        int posY = Random.Range(0, 0);
        int posZ = nowShinkRange * i;

        return new Vector3(randomPosX, posY, posZ);
    }


    int getWallShrinkTotal() //因為牆體的推進是會累加的，所以我這裡用累加的方式去算出剩餘可生成空間
    {
        var result = 0;
        for (int i = 0; i < nowShinkTime; i++)
        {
            result = result + (int)WallShrinkData.wallShrinkData.wallShrinkValue[i];
        }
        return result;
    }

    private void spawnNewTrap(int i)
    {
        // Get a random index for which prefab to spawn
        int randomIndex = Random.Range(1, 4);
        var randomResult = 0;
        if (randomIndex%3 == 0)
        {
            randomResult = 1;
        }


        switch (randomResult)
        {
            default:
                break;
            case 0:
                // Does it already exist within our pool
                //Trap trap = trapPool.Find(x => !x.gameObject.activeSelf );//&& x.name == (trapPrefab[randomIndex].name + "(Clone)")

                // Create a chunk, if were not able to find one to reuse
                // if (!trap)
                // {
                GameObject tempObject = Instantiate(trapPrefab[randomResult], transform);
                Trap trap = tempObject.GetComponent<Trap>();
                //}
                
                // Place the object, and show it
                trap.transform.position = randomPos(i);
                //chunkSpawnZ += trap.chunkLength;

                // Store the value, to reuse in our pool
                activeTraps.Enqueue(trap);
                trapPool.Add(trap);
                trap.showTrap();
                break;
            case 1:
                spawnTopNeedle(i);
                break;
        }

    }
    void spawnTopNeedle(int valid)
    {
        var validSpace = rangeX - getWallShrinkTotal();
        if (nowShinkTime <= 1)
        {
            // 生成從 -15 到 15 的數字範圍
            int[] allNumbers = Enumerable.Range(-15, 31).ToArray();

            // 隨機打亂數字順序
            allNumbers = allNumbers.OrderBy(x => Random.value).ToArray();

            // 挑選前3個不重複的數字
            int[] randomNumbers = allNumbers.Take(3).ToArray();
            for (int i = 0; i < 3; i++)
            {
                GameObject tempObject = Instantiate(trapPrefab[1], transform);
                int posY = 4;
                int posZ = nowShinkRange * valid;

                var pos = new Vector3(randomNumbers[i], posY, posZ);
                tempObject.transform.position = pos;
            }
        }
        else if (nowShinkTime == 2)
        {
            // 生成從 -15 到 15 的數字範圍
            int[] allNumbers = Enumerable.Range(-10, 21).ToArray();
            // 隨機打亂數字順序
            allNumbers = allNumbers.OrderBy(x => Random.value).ToArray();

            // 挑選前3個不重複的數字
            int[] randomNumbers = allNumbers.Take(2).ToArray();
            for (int i = 0; i < 2; i++)
            {
                GameObject tempObject = Instantiate(trapPrefab[1], transform);
                int posY = 4;
                int posZ = nowShinkRange * valid;

                var pos = new Vector3(randomNumbers[i], posY, posZ);
                tempObject.transform.position = pos;
            }
        }
        else if (nowShinkTime > 2)
        {

            GameObject tempObject = Instantiate(trapPrefab[1], transform);
            float randomPosX = Random.Range(0, rangeX - getWallShrinkTotal() + 1);//16秒-0 20秒-10 24秒-20  15 10 2 1  //0 5 4 2 1WallShrinkData.wallShrinkValue[nowShinkTime]
            randomPosX = Mathf.Clamp(randomPosX, 1, 16);//這裡clamp是因為不想讓玩家在最後一定會撞到
            randomPosX = Random.Range(0, 2) == 0 ? randomPosX : -randomPosX;
            int posY = 4;
            int posZ = nowShinkRange * valid;

            var pos = new Vector3(randomPosX, posY, posZ);
            tempObject.transform.position = pos;

        }
    }
    private void DeleteLastChunk()
    {
        Trap trap = activeTraps.Dequeue();
        trap.hideTrap();
        trapPool.Add(trap);
    }
}
