using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDialog : BaseDialog
{
   public void OnHomBtn()
    {
        ViewManager.Instance.SwitchView(ViewIndex.SelectLevelView);
        InGameController.Instance.DeSpawnAll();
        DialogManager.Instance.HideDialog(DialogIndex.PauseDialog);

    }
    public void OnContinueBtn()
    {
        DialogManager.Instance.HideDialog(DialogIndex.PauseDialog);
        InGameController.Instance.ResumeGame();

    }
}
