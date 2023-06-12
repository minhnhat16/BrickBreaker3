using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    public BrickTypeScriptableObject brickTypeScriptable;
    public Shape shape;
    private void Start()
    {
        for (int i = 0; i < ShapePool.instance.spawnAmount; i++)
        {
            Shape shape = ShapePool.instance.pool.SpawnNonGravity();
            shape.TransformShape(ShapePool.instance.spawnAmount);
            // transform.position = brickTypeScriptable.brickSpawnPosArray[i];
        }
    }
   
}
