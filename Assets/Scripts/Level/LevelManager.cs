using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int levelNum = 50;
    public GameObject levelPrefab;
    public int currentLevel = 10;
    //public List<Sprite> backgroundLevel;
    private void Start()
    { 
    }

    public void SpawnLevel()
    {
        gameObject.SetActive(true);
        for (int i = 0;  i <  levelNum; i++)
        {
            GameObject selectedLevel = Instantiate(levelPrefab, this.transform);
            selectedLevel.transform.GetChild(0).GetComponent<Text>().text = (i + 1) + "";
            //selectedLevel.transform.GetComponentInChildren<Text>().text = (i + 1) + "";
            selectedLevel.GetComponent<LevelButton>().levelID = (i + 1);
        }
    }
}
