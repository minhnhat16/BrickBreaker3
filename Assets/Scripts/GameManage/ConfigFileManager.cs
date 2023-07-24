using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigFileManager : MonoBehaviour
{
    public BrickTypeScriptableObject brickScript ;
    public LevelCompleteScrObj _lvComplete;
    [SerializeField] private LevelConfig _level;
    public static ConfigFileManager Instance;
    
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
        //Debug.Log("WAIT IN INIT");
        _level = Resources.Load("Config/LevelConfig", typeof(ScriptableObject)) as LevelConfig;
        //Debug.Log($"_level ===========> {_level}");
        yield return new WaitUntil(() => _level != null);

        //Debug.Log("==========> LOAD CONFIG FILE ");

        yield return null;
        callback?.Invoke();
    }
 }
