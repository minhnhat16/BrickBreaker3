using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelCompleteScrObj", menuName = "ScriptableObjects/LevelCompleteScrObj ")]

public class LevelCompleteScrObj : ScriptableObject
{
   public List<LevelButton> buttonToolList;
    public string levelList;
}
