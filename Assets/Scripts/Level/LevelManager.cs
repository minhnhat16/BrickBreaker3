using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int levelNum = 50;
    public GameObject levelPrefab;
    public int currentLevel = 1;
    public List<LevelConfigRecord> levelConfigRecordList;
    public List<GameObject> selectLevelList;

    //public List<Sprite> backgroundLevel;
    private void Start()
    {
        levelConfigRecordList = ConfigFileManager.Instance.Level.GetAllRecord();
    }

    public void SpawnLevel()
    {

        if (selectLevelList.Count == 0 ) {
            for (int i = 0;  i < levelConfigRecordList.Count; i++)
            {
                
                GameObject selectedLevel = Instantiate(levelPrefab, this.transform);
                selectedLevel.transform.GetChild(1).GetComponent<Text>().text = levelConfigRecordList[i].iD + "";
                selectLevelList.Add(selectedLevel); 
                selectedLevel.GetComponent<LevelButton>().levelID = levelConfigRecordList[i].iD;
            }
            //HighestLevelOn();
            WonLevel();

        }
    }
    public void HighestLevelOn()
    {
        int highId = GameManager.Instance.highetsLevel;
        selectLevelList[highId].GetComponent<LevelButton>().highetsSprite.SetActive(true);
    }
    public void WonLevel()
    {
        for (int i = 0; i < levelConfigRecordList.Count; i++)
        {
             if(levelConfigRecordList[i].isWon == 0)
            {
                selectLevelList[i].GetComponent<LevelButton>().lockSprite.SetActive(true);
            }
        }
    }
    
}
