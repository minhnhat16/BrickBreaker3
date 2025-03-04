using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Threading;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelButton : MonoBehaviour
{
    public int levelID;
    public Level level;
    public Text levelTxt;
    public Text levelTxtTemp;
    public GameObject lockSprite;
    public GameObject highetsSprite;

    public int currentLevel;
    public bool isComplete;
    private void Start()
    {
        levelTxt = GameObject.Find("Level_Text").GetComponent<Text>();
        levelTxtTemp = GameObject.Find("Level_Text_Temp").GetComponent<Text>();
    }

    public void OnclickLevelButton()
    {
        if(isComplete) {
            levelTxt.text = "Level " + levelID + "";
            levelTxtTemp.text = levelID + "";
            DialogManager.Instance.ShowDialog(DialogIndex.LevelConfirmDialog);
        }
        
        //ViewManager.Instance.SwitchView(ViewIndex.GameplayView);
    }
   
    public Level LoadCheckLevel(int index)
    {
        string path = "Levels/level_" + index.ToString(); 
        level = Resources.Load<Level>(path);
        return level;
    }
    public void HighestLevel(int index)
    {
        if(GameManager.Instance.currentLevel == index)
        {
            Debug.Log("Highest level on" + index);
            highetsSprite.gameObject.SetActive(true);
        }
    }
    
}

