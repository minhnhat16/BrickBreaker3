using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private Action<Shape> _killAction;
    public void Init(Action<Shape> killAction)
    {
        _killAction = killAction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _killAction(this);
        }
    }
}
