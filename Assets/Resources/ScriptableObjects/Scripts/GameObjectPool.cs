using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectPool", menuName = "ScriptableObjects/GameObjectPool")]
public class GameObjectPool : ScriptableObject
{
    public GameObject prefab;
    private List<PoolObject> objectsInPool;
    private List<PoolObject> objectsInUse;

    public GameObject SpawnObject(Vector3 position, Quaternion rotation)
    {
        PoolObject currentObject;
        if (objectsInPool.Count <= 0)
        {
            GameObject newGO = Instantiate(prefab);
            currentObject = newGO.AddComponent<PoolObject>();
            currentObject.pool = this;
        }
        else
        {
            currentObject = objectsInPool[0];
            objectsInPool.Remove(currentObject);
        }
        objectsInUse.Add(currentObject);

        currentObject.gameObject.SetActive(true);
        currentObject.transform.position = position;
        currentObject.transform.rotation = rotation;

        return currentObject.gameObject;
    }


    public void ReturnToPool(PoolObject objectToReturn)
    {
        if (objectsInPool.Contains(objectToReturn)) { return; }
        objectToReturn.gameObject.SetActive(false);
        objectsInUse.Remove(objectToReturn);
        objectsInPool.Add(objectToReturn);
    }
    public void RemoveObject(PoolObject objectToRemove)
    {
        objectsInPool.Remove(objectToRemove);
        objectsInUse.Remove(objectToRemove);
    }
   
}
