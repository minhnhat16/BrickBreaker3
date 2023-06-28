using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    
    public int levelID;
    public Text levelTxt;
    public Text levelTxtTemp;
    
    private void Start()
    {
        levelTxt = GameObject.Find("Level_Text").GetComponent<Text>();
        levelTxtTemp = GameObject.Find("Level_Text_Temp").GetComponent<Text>();
    }

    public void OnclickLevelButton()
    {
        levelTxt.text = "Level " + levelID + "";
        levelTxtTemp.text = levelID + "";
        DialogManager.Instance.ShowDialog(DialogIndex.LevelConfirmDialog);
        //ViewManager.Instance.SwitchView(ViewIndex.GameplayView);
    }

}
