using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            for (int i = 0;  i < levelConfigRecordList.Count; i++)
            {
                GameObject selectedLevel = Instantiate(levelPrefab, this.transform);

                selectedLevel.transform.GetChild(1).GetComponent<Text>().text = levelConfigRecordList[i].iD + "";
                selectLevelList.Add(selectedLevel); 
                selectedLevel.GetComponent<LevelButton>().levelID = levelConfigRecordList[i].iD;
                IncompletedLevel(selectedLevel,i, HighestLevelOn);
                
            }

        }
    }

    public void HighestLevelOn()
    {
        Debug.Log("HighestLevelON");

        int highId = DataAPIController.instance.GetHighestLevel();
        int level = Convert.ToInt32(highId);
        selectLevelList[level - 1].GetComponent<LevelButton>().highetsSprite.SetActive(true);
        selectLevelList[level - 1].GetComponent<LevelButton>().lockSprite.SetActive(false);
        selectLevelList[level - 1].GetComponent<LevelButton>().starList.gameObject.SetActive(false);
    }
    public void HighestLevelOff()
    {
        int highId = GameManager.Instance.currentLevel;
        Debug.Log("HIGHTID "+ highId);  
        selectLevelList[highId - 1].GetComponent<LevelButton>().highetsSprite.SetActive(false);

    }
    public void IncompletedLevel(GameObject gameObject, int index, Action callback)
    {
        Debug.Log("WonLevelON");
        if (levelConfigRecordList[index].isWon == 0 || index == DataAPIController.instance.GetHighestLevel())
        {
            gameObject.GetComponent<LevelButton>().lockSprite.SetActive(true);
            gameObject.GetComponent<LevelButton>().starList.gameObject.SetActive(false);
            callback?.Invoke();
        }
    }
    
}
