using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    public BrickTypeScriptableObject brickTypeScriptable;
    public Shape shape;
    public int spacingShape = 3;
    private void Start()
    {
        for(int i = 0;i < ShapePool.instance.spawnAmount; i ++)
        {
           ShapePool.instance.pool.SpawnGravity();
           ShapePool.instance.pool.list[i].transform.position = brickTypeScriptable.brickSpawnPosArray[i];
        }
    }
}
    