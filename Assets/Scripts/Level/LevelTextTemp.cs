using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextTemp : MonoBehaviour
{
    public string currentLevelText;
    public void PopUpPlay()
    {
        currentLevelText = this.gameObject.GetComponent<Text>().text;
        LoadSceneManager.Instance.LoadScene("InGame");
        GameManager.Instance.LoadOnInGameController();
        LoadLevel.instance.LevelSelect(currentLevelText);
        LoadLevel.instance.currentLevelLoad = Convert.ToInt32(currentLevelText);
       
    }
}
