using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    //private bool breakable = false;
    public BallSystem ballSystem;
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);

    }
}
