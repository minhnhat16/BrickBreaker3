using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigFileManager : MonoBehaviour
{
    public BrickTypeScriptableObject brickScript ;
    public static ConfigFileManager Instance;
    private void Awake()
    {
        Instance = this; 
    }
}
