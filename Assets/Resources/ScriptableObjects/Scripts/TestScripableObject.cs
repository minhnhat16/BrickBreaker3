using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestScriptableObjects", menuName = "ScriptableObjects/TestScriptableObjects ")]
public class TestScripableObject : ScriptableObject
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Sprite[] spriteArray;
    [SerializeField] private GameObjectPool _brickPool;
    [SerializeField] private GameObjectPool _groundPool;
}
    