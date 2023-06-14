using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPoolManager : MonoBehaviour
{
    [System.NonSerialized]
    public BY_Local_Pool<Brick> pool;
    public Brick prefab;
    public static BrickPoolManager instance;
    public int spawnAmount;
    public int destroyCount;
    private void Awake()
    {
        instance = this;
        destroyCount = 0;
        pool = new BY_Local_Pool<Brick>(prefab, spawnAmount, this.transform);
    }
}
