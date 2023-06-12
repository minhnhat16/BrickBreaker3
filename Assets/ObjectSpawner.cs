using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner instance;
    public Shape shapePref;
    public int spawnAmount = 10;
    public bool _usePool;
    public int _defaultCapacity = 10;
    public int _maxCapacity = 20;
    private ObjectPool<Shape> _pool;
    private BrickTypeScriptableObject _spawnBrick;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        _pool = new ObjectPool<Shape>(() =>{
            return Instantiate(shapePref);
        }, shape =>{
            shape.gameObject.SetActive(true);
        }, shape =>{
            shape.gameObject.SetActive(false);
        }, shape =>{
            Destroy(shape.gameObject);
        }, false, _defaultCapacity, _maxCapacity);
        Spawn();
    }

    public void Spawn() 
    {
        for (var i = 0; i < spawnAmount; i++)
        {
            var shape = _usePool ? _pool.Get() : Instantiate(shapePref);

            // NOTE: get transform position form sciptabel object
            Vector3 spawnPos = _spawnBrick.brickSpawnPosArray[i];
            shape.transform.position = spawnPos;
           // shape.transform.position = transform.position + Random.insideUnitSphere * 5;
            shape.transform.SetParent(this.transform);
           // shape.Init(KillShape);
        }  
    }

    public void KillShape(Shape shape)
    {
        if (_usePool) _pool.Release(shape);
        else {
            Destroy(shape.gameObject);
        }
    }
}
