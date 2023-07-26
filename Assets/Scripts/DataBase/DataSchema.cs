using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    [SerializeField]
    UserInfo userInfo;
    [SerializeField]
    ItemData itemData;
}
[Serializable]
public class UserInfo
{
    public string name;
    public int level;
}
[Serializable]
public class UserItem
{
    public Dictionary<int, ItemData> item_got = new Dictionary<int, ItemData>();
    public int gold;
}
[Serializable]
public class ItemData
{
    public int id;
    public int total_have;
}
