using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelCompleteScrObj", menuName = "ScriptableObjects/LevelCompleteScrObj     ")]

public class LevelCompleteScrObj : ScriptableObject
{
    public List<LevelButton> buttonToolList;
    public string levelList;

    public void SaveLevel()
    {
        for (int i = 0; i < buttonToolList.Count; i++)
        {
            //buttonToolList[i]
        }   
    }
}

