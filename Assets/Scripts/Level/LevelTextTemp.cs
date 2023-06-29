using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextTemp : MonoBehaviour
{
    public string currentLevelText;
    public void GetCurrentLevel()
    {
        currentLevelText = this.gameObject.GetComponent<Text>().text;
    }
}
