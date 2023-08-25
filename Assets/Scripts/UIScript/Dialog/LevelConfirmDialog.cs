using Mono.Cecil.Cil;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelConfirmDialog : BaseDialog
{
    [SerializeField] private string TextLevel;
    [SerializeField] private Text _levelNumText;
    [SerializeField] private Text _score_lb;
    [SerializeField] private StarList _star;
    [SerializeField] private ItemButton itemButton;
    [SerializeField] private StartItem bigBall;
    [SerializeField] private StartItem power;
    [SerializeField] private StartItem addLive;

    public Text _showLevelNum;
    
    private void Start()
    {
        _levelNumText = GameObject.Find("Level_Text").GetComponent<Text>();
    }
    public override void OnStartShowDialog()
    {
        ItemSprite();
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
        ItemDataSave();
        TextLevel = GameObject.Find("Level_Text_Temp").GetComponent<Text>().text;
        DialogManager.Instance.HideDialog(DialogIndex.LevelConfirmDialog);
        LoadSceneManager.Instance.LoadScene("Ingame");
        ViewManager.Instance.SwitchView(ViewIndex.GameplayView);
        Debug.Log("GAME MANAGER" + GameManager.Instance);
        GameManager.Instance.LoadOnInGameController();
        GameManager.Instance.currentScore = 0;      
        LoadLevel.instance.LevelSelect(TextLevel);
        LoadLevel.instance.currentLevelLoad = Convert.ToInt32(TextLevel);
    }
    private void ItemDataSave()
    {
        power.OnItemType();
        bigBall.OnItemType();
        addLive.OnItemType();
    }
    private void ItemSprite()
    {
        power.checkUse = false;
        power.OffItemType();
        power.checkStatus.SetActive(false);
        bigBall.checkUse = false;
        bigBall.OffItemType();
        bigBall.checkStatus.SetActive(false);
        addLive.checkUse = false;
        addLive.OffItemType();
        addLive.checkStatus.SetActive(false);
    }
}
