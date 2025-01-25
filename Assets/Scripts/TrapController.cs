using Gamemanager;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    //總生成時間
    public float createTimeMax=20f;
    //生成間格
    public float createTime=5f;
    //現在時間
    public float nowTime=0f;
    //總時間
    public float totalTime=0f;
    //最大生成個數
    public int trapMax=10;
    //現在trap數
    public int nowTrap=0;
    //生成關卡
    public int nowLevel=0;
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
        for(int i=0;i<trapMax;i++)
        {
            if(i%10==0){
            nowShinkTime+=1;
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

    public Vector3 randomPos(int i){
        Debug.Log(WallShrinkData.wallShrinkData.wallShrinkValue[nowShinkTime]);
        Debug.LogWarning("result"+(rangeX-WallShrinkData.wallShrinkData.wallShrinkValue[nowShinkTime]));
        float randomPosX = Random.Range(0, rangeX-WallShrinkData.wallShrinkData.wallShrinkValue[nowShinkTime]);//16秒-0 20秒-10 24秒-20  15 10 2 1  //0 5 4 2 1WallShrinkData.wallShrinkValue[nowShinkTime]
        randomPosX=Random.Range(0, 2) == 0 ? randomPosX : -randomPosX;
        int posY = Random.Range(0, 0);
        int posZ = nowShinkRange*i;
               
        return new Vector3(randomPosX, posY, posZ);
    }

    private void spawnNewTrap(int i)
    {
        // Get a random index for which prefab to spawn
        int randomIndex = Random.Range(0, trapPrefab.Count);
        

        // Does it already exist within our pool
        //Trap trap = trapPool.Find(x => !x.gameObject.activeSelf );//&& x.name == (trapPrefab[randomIndex].name + "(Clone)")

        // Create a chunk, if were not able to find one to reuse
        // if (!trap)
        // {
            GameObject tempObject = Instantiate(trapPrefab[randomIndex], transform);
            Trap trap = tempObject.GetComponent<Trap>();
        //}

        // Place the object, and show it
        trap.transform.position = randomPos(i);
        //chunkSpawnZ += trap.chunkLength;

        // Store the value, to reuse in our pool
        activeTraps.Enqueue(trap);
        trapPool.Add(trap);
        trap.showTrap();
    }

    private void DeleteLastChunk()
    {
        Trap trap = activeTraps.Dequeue();
        trap.hideTrap();
        trapPool.Add(trap);
    }
}
