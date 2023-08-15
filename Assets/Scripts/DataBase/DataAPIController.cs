
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataAPIController : MonoBehaviour
{
    public static DataAPIController instance;

    [SerializeField]
    private DataModel dataModel;

    private void Awake()
    {
        instance = this;
    }

    public void InitData(Action callback)
    {
        Debug.Log("(BOOT) // INIT DATA");

        dataModel.InitData(() =>
        {
           // CheckDailyLogin();
            callback();
        });

        Debug.Log("==========> BOOT PROCESS SUCCESS <==========");
    }

    #region Get Data
    public void GetName()
    {
        dataModel.ReadData<string>(DataPath.NAME);
        Level level = dataModel.ReadData<Level>(DataPath.LEVEL);
    }
    public int GetGold()
    {
        Debug.LogWarning("GETTING GOLD .....");
        int gold = dataModel.ReadData<int>(DataPath.GOLD);
        //int gold = 0;
        return gold;
    }
    #endregion
    public int GetHighestLevel()
    {
        int level = dataModel.ReadData<int>(DataPath.LEVEL);
        return level;
    }
    #region Others
    public ItemData GetItemData(int idItem)
    {
        ItemData itemData = dataModel.ReadDictionary<ItemData>(DataPath.ITEM, idItem.ToKey());
        return itemData;
    }
    public void SaveGold(int gold)
    {
       dataModel.UpdateData(DataPath.GOLD, gold);
    }
    #endregion
}
