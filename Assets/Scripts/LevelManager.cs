using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int levelNum = 50;
    public GameObject levelPrefab;
    public int currentLevel = 0;
    public List<Sprite> backgroundLevel;

    private void Start()
    {
       // SpawnLevel();
    }

    public void SpawnLevel()
    {
        int levelCount = 0;
        GameObject selectedLevel = Instantiate(levelPrefab, this.transform);
        selectedLevel.transform.GetChild(1).GetComponent<Text>().text = (levelCount + 1) + "";
        selectedLevel.GetComponent<LevelButton>().levelID = (levelCount + 1);


    }
}
