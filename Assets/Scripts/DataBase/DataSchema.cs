using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    [SerializeField]
    public UserInfo userInfo;
    [SerializeField]
    public ItemData itemData;
    [SerializeField]
    public UserInventory inventory;
    [SerializeField]
    public UserLevelData levelData;
}
[Serializable]
public class UserInfo
{
    public string name;
    public int level;
}
[Serializable]
public class UserInventory
{
    [SerializeField]
    public Dictionary<int, ItemData> inventory = new Dictionary<int, ItemData>();
    public int gold;
}
[Serializable]
public class ItemData
{
    public int id;
    public int total;
}
[Serializable]
public class UserLevelData 
{
    public Dictionary<int, Level> level = new Dictionary<int, Level>();
    public int highestLevel;
    public int totalStar;
}
public class LevelData
{
    public int highestScore;
    public int totalStarInLevel;
    public bool isWin = false;
}

