using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    //private Action<Shape> _killAction;
    //public void Init(Action<Shape> killAction)
    //{
    //    _killAction = killAction;
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Ground"))
    //    {
    //        _killAction(this);
    //    }
    //}
    public Sprite currentSprite;
    public BrickTypeScriptableObject brickTypeScriptable;
    private void Awake()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {   
            ShapePool.instance.pool.DeSpawnNonGravity(this);
        }
    }
    public void TransformShape(int count )
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 vector3 = brickTypeScriptable.brickSpawnPosArray[i];
            transform.position = vector3;
        }
    }
}
