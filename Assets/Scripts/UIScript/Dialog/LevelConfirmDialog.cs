using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelConfirmDialog : BaseDialog
{
    [SerializeField] private string TextLevel;
    [SerializeField] private Text _levelNumText;
    [SerializeField] private Text _score_lb;
    [SerializeField] private StarList _star;
    public Text _showLevelNum;
    
    private void Start()
    {
        _levelNumText = GameObject.Find("Level_Text").GetComponent<Text>();  
    }
    public override void OnStartShowDialog()
    {
        string key = GameObject.Find("Level_Text_Temp").GetComponent<Text>().text;
        string score_txt = "High Score: " + DataAPIController.instance.GetLevelScore(key).ToString();
        _score_lb.text = score_txt;
        
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
        UnityEngine.Debug.Log("GAME MANAGER" + GameManager.Instance);
        GameManager.Instance.LoadOnInGameController();
        
        GameManager.Instance.currentScore = 0;
        LoadLevel.instance.LevelSelect(TextLevel);
        LoadLevel.instance.currentLevelLoad = Convert.ToInt32(TextLevel);
    }
    private void ShowStar()
    {
        //int i = DataAPIController.instance.TotalStarOnLevel();
    }
}
