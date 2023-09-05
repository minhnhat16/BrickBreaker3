using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseDialog : BaseDialog
{
   public void OnHomeBtn()
    {
        ViewManager.Instance.SwitchView(ViewIndex.SelectLevelView);
        InGameController.Instance.DeSpawnAll();
        DialogManager.Instance.HideDialog(DialogIndex.PauseDialog);
        SceneManager.LoadScene("Buffer");
        LoadSceneManager.Instance.currrentSence = "Buffer";
    }
    public void OnRestartBtn()
    {
        DialogManager.Instance.HideDialog(DialogIndex.PauseDialog);
        LoadLevel.instance.RestartLevel();
        InGameController.Instance.ResumeGame();
        InGameController.Instance.lives = 1;
        Debug.Log("======> RESTART ON YOUR HAND ");
    }
    public void OnReturnBtn()
    {
        DialogManager.Instance.HideDialog(DialogIndex.PauseDialog);
        InGameController.Instance.ResumeGame();
    }
}
