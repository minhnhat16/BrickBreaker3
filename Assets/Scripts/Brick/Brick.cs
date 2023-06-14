using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    //private bool breakable = false;
    public BallSystem ballSystem;
    public enum BrickType
    {
        Red,
        Gray,
        Green,
        Orange,
        Yellow,
        Blue

    };
    public BrickTypeScriptableObject brickTypeScriptableObject;
    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")){
            BrickPoolManager.instance.pool.DeSpawnNonGravity(this);
            BrickPoolManager.instance.destroyCount++ ;
        }
    }
}
