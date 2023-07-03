using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDialog : BaseDialog
{
   public void OnHomBtn()   
    {
        LoadSceneManager.Instance.LoadScene("Buffer");
        ViewManager.Instance.SwitchView(ViewIndex.SelectLevelView);
        InGameController.Instance.DeSpawnAll();
        DialogManager.Instance.HideDialog(DialogIndex.PauseDialog);

    }
    public void OnRestartBtn()
    {
        DialogManager.Instance.HideDialog(DialogIndex.PauseDialog);
        InGameController.Instance.ResumeGame();
        LoadLevel.instance.RestartLevel();

    }
}
