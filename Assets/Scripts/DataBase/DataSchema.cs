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
    public Dictionary<string, ItemData> itemInventory = new Dictionary<string, ItemData>();
    public int gold;
}
[Serializable]
public class ItemData
{
    public string id;
    public int total;
}
[Serializable]
public class UserLevelData 
{
    public Dictionary<string, LevelData> level = new Dictionary<string, LevelData>();
    public int highestLevel;
    public int totalStar;
    public int currentLevel;
}
public class LevelData
{
    public string ID;
    public int highestScore;
    public int totalStarInLevel;
    public bool isWin = false;
}

