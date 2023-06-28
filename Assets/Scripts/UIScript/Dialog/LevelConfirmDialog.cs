using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelConfirmDialog : BaseDialog
{
    [SerializeField] private Text TextLevel;

    private void Start()
    {
       TextLevel =  GameObject.Find("Level_Text").GetComponent<Text>();
    }

    public void OnClickQuitBtn()
    {
        DialogManager.Instance.HideDialog(DialogIndex.LevelConfirmDialog);
    }
    public void OnStartBtn()
    {
        DialogManager.Instance.HideDialog(DialogIndex.LevelConfirmDialog);
        LoadSceneManager.Instance.LoadScene("Ingame");
        ViewManager.Instance.SwitchView(ViewIndex.GameplayView);
        GameManager.Instance.LoadOnInGameController();
    } 
}
