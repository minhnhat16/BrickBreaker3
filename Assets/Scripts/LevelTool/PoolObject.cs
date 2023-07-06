using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
  public GameObjectPool pool;

    private void OnDisable()
    {
        pool.ReturnToPool(this);

    }
    private void OnDestroy()
    {
        pool.RemoveObject(this);
    }


}
