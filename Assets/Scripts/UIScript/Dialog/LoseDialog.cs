using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseDialog : BaseDialog
{
    public void onRestartBtn()
    {
        InGameController.Instance.isGameOver = false;
        LoadLevel.instance.RestartLevel();
        InGameController.Instance.ResumeGame();
        DialogManager.Instance.HideDialog(DialogIndex.LoseDialog);
    }
    public void onHomeBtn()
    {
        ViewManager.Instance.SwitchView(ViewIndex.MainScreenView);
        InGameController.Instance.DeSpawnAll();
        DialogManager.Instance.HideDialog(DialogIndex.LoseDialog);
    }
    public void OnSelectLevelBtn()
    {
        ViewManager.Instance.SwitchView(ViewIndex.SelectLevelView);
        InGameController.Instance.DeSpawnAll();
        DialogManager.Instance.HideDialog(DialogIndex.LoseDialog);
    }
}
