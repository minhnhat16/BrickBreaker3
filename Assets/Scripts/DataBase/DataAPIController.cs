
using System;
using System.Collections.Generic;
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
    public void Level()
    {
        Debug.Log("DATA === LEVEL");

        dataModel.ReadData<string>(DataPath.NAME);
        LevelConfig level = dataModel.ReadDictionary<LevelConfig>(DataPath.LEVEL, DataPath.HIGHESTLV);
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
        Debug.Log("DATA === highestLevel");
        int level = dataModel.ReadData<int>(DataPath.HIGHESTLV);
        return level;
    }
    public void SaveHighestLevel(int level)
    {
        int highlevel = GetHighestLevel();
        if (highlevel > level)
        {
            dataModel.UpdateData(DataPath.HIGHESTLV, level);

        }
    }
    public void SaveLevel(int id, int hightSc, int totalStr, bool Win)
    {
        Debug.Log("DATA === SAVE LEVEL");
        LevelData levelData = new LevelData
        {
            ID = id.ToString(),
            highestScore = hightSc,
            totalStarInLevel = totalStr,
            isWin = Win
        };
        SaveHighestLevel(id);
        dataModel.UpdateDataDictionary(DataPath.LEVEL, id.ToString(), levelData);
    }
    #region Others
    public ItemData GetItemData(int idItem)
    {
        Debug.Log("DATA === ITEM DATA");

        ItemData itemData = dataModel.ReadDictionary<ItemData>(DataPath.ITEM, idItem.ToKey());
        return itemData;
    }
    public void SaveGold(int gold)
    {
        dataModel.UpdateData(DataPath.GOLD, gold);
    }
    #endregion
}
