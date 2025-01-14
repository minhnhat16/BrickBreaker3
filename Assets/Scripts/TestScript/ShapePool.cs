using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShapePool : MonoBehaviour
{
    [System.NonSerialized]
    public BY_Local_Pool<Shape> pool;
    public Shape prefab;
    public static ShapePool instance;
    public int spawnAmount = 6;

    private void Awake()
    {
        instance = this;
        pool = new BY_Local_Pool<Shape>(prefab, spawnAmount, this.transform);
    }
}

