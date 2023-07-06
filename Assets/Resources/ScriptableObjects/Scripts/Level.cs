using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class Level : ScriptableObject
{
    public int collumnCount;
    public int winScore;
    public string bricks;
    public bool isBossLevel;
    public int bossID;
    public bool isWin = false;
}
