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
        GameManager.Instance.LoadOnInGameController();

        LoadSceneManager.Instance.LoadScene("InGame");
        LoadSceneManager.Instance._LevelScrollView.SetActive(false);
        LoadSceneManager.Instance._LevelPopUpUI.SetActive(false);

        currentLevelText = this.gameObject.GetComponent<Text>().text;


       // Debug.Log("current level ==>> " + currentLevelText);
        LoadLevel.instance.LevelSelect(currentLevelText);
        LoadLevel.instance.currentLevelLoad = Convert.ToInt32(currentLevelText);
        //Debug.Log("current load ==>> " + LoadLevel.instance.currentLevelLoad);
    }
}
