using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPoolManager : MonoBehaviour
{
    
    public BY_Local_Pool<Brick> pool;
    public Brick prefab;
    public static BrickPoolManager instance;
    public int spawnAmount;
    private void Awake()
    {
        instance = this;
        pool = new BY_Local_Pool<Brick>(prefab, 150, this.transform);
    }
}
