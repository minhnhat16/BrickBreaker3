using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Items", order = 1)] 
public class ConfigItemRecord : ScriptableObject
{
    public ItemDataInGame[] itemData;
    public int ID;
    public string icon;
}

[System.Serializable]
public class ItemDataInGame
{
    public ItemType itemType;
    public float rate;
    public Sprite sprite;
}
