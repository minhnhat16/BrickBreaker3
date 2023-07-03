using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelConfirmDialog : BaseDialog
{
    [SerializeField] private string TextLevel;
    [SerializeField] private Text _levelNumText;
    public Text _showLevelNum;
    private void Start()
    {
        _levelNumText = GameObject.Find("Level_Text").GetComponent<Text>();
    }
    private void Update()
    {
        Text text = GameObject.Find("Level_Text").GetComponent<Text>();
        string tempLevel = text.text ;
        _showLevelNum.text = tempLevel;

    }
    public void OnClickQuitBtn()
    {
        DialogManager.Instance.HideDialog(DialogIndex.LevelConfirmDialog);
    }
    public void OnStartBtn()
    {
       
        TextLevel = GameObject.Find("Level_Text_Temp").GetComponent<Text>().text;
        DialogManager.Instance.HideDialog(DialogIndex.LevelConfirmDialog);
        LoadSceneManager.Instance.LoadScene("Ingame");
        ViewManager.Instance.SwitchView(ViewIndex.GameplayView);
        GameManager.Instance.LoadOnInGameController();
        LoadLevel.instance.LevelSelect(TextLevel);
        LoadLevel.instance.currentLevelLoad = Convert.ToInt32(TextLevel);

    }
}
