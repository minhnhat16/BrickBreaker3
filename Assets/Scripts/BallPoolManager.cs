using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPoolManager : MonoBehaviour
{
    public BY_Local_Pool<BallSystemVer2> pool;
    public BallSystemVer2 prefab;
    public static BallPoolManager instance;
    private void Awake()
    {
        instance = this;
        pool = new BY_Local_Pool<BallSystemVer2>(prefab, 20, this.transform);
    }
}
