using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ConfigFileManager : MonoBehaviour
{
    public BrickTypeScriptableObject brickScript ;
    [SerializeField] private LevelConfig _level;
    public static ConfigFileManager Instance;
    [SerializeField] private ConfigItemRecord _item;
     public BossConfig _boss;
    public LevelConfig Level { get => _level; }

    private void Awake()
    {
        Instance = this; 
    }
    public void Init(Action callback)
    {
        StartCoroutine(WaitInit(callback));
    }
    IEnumerator WaitInit(Action callback)
    {
        Debug.Log("WAIT IN INIT");
        _level = Resources.Load("Config/LevelConfig", typeof(ScriptableObject)) as LevelConfig;
        _boss = Resources.Load("ScriptableObjects/BossScriptableObject", typeof(ScriptableObject)) as BossConfig;
        //Debug.Log($"_level ===========> {_level}");
        yield return new WaitUntil(() => _level != null) ;
        yield return new WaitUntil(() => _boss != null);
        Debug.Log("==========> LOAD CONFIG FILE ");

        yield return null;
        callback?.Invoke();
    }
 }
