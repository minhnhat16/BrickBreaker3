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
    private void Awake()
    {
        levelConfigRecordList = ConfigFileManager.Instance.Level.GetAllRecord();


    }
    private void Start()
    {
        Debug.Log("levelconfigrecordlist" + levelConfigRecordList.Count);
    }   
    public void SpawnLevel()
    {
        if (selectLevelList.Count == 0)
        {
            Debug.Log("select level list:" + selectLevelList.Count);
            Debug.Log($"Level config record count {levelConfigRecordList.Count}");
            for (int i = 0;  i < levelConfigRecordList.Count; i++)
            {
                GameObject selectedLevel = Instantiate(levelPrefab, this.transform);

                selectedLevel.transform.GetChild(1).GetComponent<Text>().text = levelConfigRecordList[i].iD + "";
                selectLevelList.Add(selectedLevel); 
                selectedLevel.GetComponent<LevelButton>().levelID = levelConfigRecordList[i].iD;
            }
            //WonLevel();

        }
    }
    public void HighestLevelOn()
    {
        
        int highId = GameManager.Instance.currentLevel;
        selectLevelList[highId - 1].GetComponent<LevelButton>().highetsSprite.SetActive(true);
    }
    public void HighestLevelOff()
    {
        int highId = GameManager.Instance.currentLevel;
        selectLevelList[highId - 1].GetComponent<LevelButton>().highetsSprite.SetActive(false);

    }
    public void WonLevel(GameObject gameObject, int index)
    {
        if (levelConfigRecordList[index].isWon == 0)
        {
            gameObject.GetComponent<LevelButton>().lockSprite.SetActive(true);
            
        }
    }
    
}
