using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallPoolManager : MonoBehaviour
{
    public BY_Local_Pool<BallSystemVer2> pool;
    public BallSystemVer2 prefab;
    public static BallPoolManager instance;
    private void Awake()
    {
        instance = this;
        pool = new BY_Local_Pool<BallSystemVer2>(prefab, 100, this.transform);
    }
    public void ResetBallPool()
    {
        int i = 0;
        while ( i < pool.list.Count)
        {
            pool.list[i].ResetBall();
            pool.DeSpawnNonGravity(pool.list[i]);
        }
        //pool.DeSpawnAll();
    }
    public void ResetAllPoolPostion()
    {
        int i = 0;
        while ( i < pool.list.Count)
        {
            pool.list[i].ResetBall();
        }
    }
}
