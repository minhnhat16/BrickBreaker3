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
    //public List<Sprite> backgroundLevel;
    private void Start()
    {
     
    }

    public void SpawnLevel()
    {
        Debug.Log("Spawn Level");
        for (int i = 1;  i <  levelNum; i++)
        {   
            GameObject selectedLevel = Instantiate(levelPrefab, this.transform);
            selectedLevel.transform.GetChild(1).GetComponent<Text>().text = (i) + "";
            //selectedLevel.transform.GetComponentInChildren<Text>().text = (i + 1) + "";
            selectedLevel.GetComponent<LevelButton>().LoadCheckLevel(i);
            selectedLevel.GetComponent<LevelButton>().HighestLevel(i);
            selectedLevel.GetComponent<LevelButton>().CheckLevelComplete();
            selectedLevel.GetComponent<LevelButton>().levelID = (i);
        }
    }

}
