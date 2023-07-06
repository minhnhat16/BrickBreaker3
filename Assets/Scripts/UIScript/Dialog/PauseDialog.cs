using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDialog : BaseDialog
{
   public void OnHomeBtn()
    {
        ViewManager.Instance.SwitchView(ViewIndex.SelectLevelView);
        InGameController.Instance.DeSpawnAll();
        DialogManager.Instance.HideDialog(DialogIndex.PauseDialog);

    }
    public void OnRestartBtn()
    {
        DialogManager.Instance.HideDialog(DialogIndex.PauseDialog);
        LoadLevel.instance.RestartLevel();
        InGameController.Instance.ResumeGame();
        Debug.Log("======> RESTART ON YOUR HAND ");
    }
    public void OnReturnBtn()
    {
        DialogManager.Instance.HideDialog(DialogIndex.PauseDialog);
        InGameController.Instance.ResumeGame();
    }
}
