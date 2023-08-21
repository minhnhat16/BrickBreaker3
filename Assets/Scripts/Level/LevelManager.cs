using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int levelNum = 50;
    public GameObject levelPrefab;
    public int currentLevel;
    public List<LevelConfigRecord> levelConfigRecordList;
    public List<GameObject> selectLevelList;

    //public List<Sprite> backgroundLevel;
    private void Awake()
    {
        levelConfigRecordList = ConfigFileManager.Instance.Level.GetAllRecord();
    }
    private void Start()
    {
        // Debug.Log("levelconfigrecordlist" + levelConfigRecordList.Count);
    }
    public void SpawnLevel()
    {
        if (selectLevelList.Count == 0)
        {
            //Debug.Log("select level list:" + selectLevelList.Count);
            //Debug.Log($"Level config record count {levelConfigRecordList.Count}");
            for (int i = 0; i < levelConfigRecordList.Count; i++)
            {
                GameObject selectedLevel = Instantiate(levelPrefab, this.transform);
                selectedLevel.transform.GetChild(1).GetComponent<Text>().text = levelConfigRecordList[i].iD + "";
                selectLevelList.Add(selectedLevel);
                selectedLevel.GetComponent<LevelButton>().levelID = levelConfigRecordList[i].iD;
                IncompletedLevel(selectedLevel, i, HighestLevelOn);
            }
        }
    }
    public void CompleteLevelOn()
    {
        //Debug.Log("CompleteLevelOn");
        int currentlv = DataAPIController.instance.GetCurrentLevel();
        selectLevelList[currentlv - 1].GetComponent<LevelButton>().highetsSprite.SetActive(false);
        selectLevelList[currentlv - 1].GetComponent<LevelButton>().lockSprite.SetActive(false);
        selectLevelList[currentlv - 1].GetComponent<LevelButton>().starList.gameObject.SetActive(true);
    }
    public void HighestLevelOn()
    {
        //Debug.Log("HighestLevelON");

        int highId = DataAPIController.instance.GetHighestLevel();
        //Debug.Log("Level" + (highId - 1));
        selectLevelList[highId - 1].GetComponent<LevelButton>().highetsSprite.SetActive(true);
        selectLevelList[highId - 1].GetComponent<LevelButton>().lockSprite.SetActive(false);
        selectLevelList[highId - 1].GetComponent<LevelButton>().starList.gameObject.SetActive(false);
        if (highId > 1) {
        selectLevelList[highId - 2].GetComponent<LevelButton>().starList.gameObject.SetActive(true);
        }
    }
    public void HighestLevelOff()
    {
        int current = DataAPIController.instance.GetCurrentLevel();
        //Debug.Log("HIGHTID " + current);
        selectLevelList[current - 1].GetComponent<LevelButton>().highetsSprite.SetActive(false);
    }
    public void IncompletedLevel(GameObject gameObject, int index, Action callback)
    {
        //Debug.Log("WonLevelON");
        StarOn(gameObject);
        if (index >= DataAPIController.instance.GetHighestLevel())
        {
            gameObject.GetComponent<LevelButton>().lockSprite.SetActive(true);
            gameObject.GetComponent<LevelButton>().starList.gameObject.SetActive(false);
            callback?.Invoke();
        }
    }
    public void StarOn(GameObject obj)
    {
        obj.GetComponent<LevelButton>().starList.gameObject.SetActive(true);
    }
}
