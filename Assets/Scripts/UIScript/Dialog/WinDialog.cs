using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDialog : BaseDialog
{
   public void OnHomeBtn()
    {
        ViewManager.Instance.SwitchView(ViewIndex.SelectLevelView);
        InGameController.Instance.DeSpawnAll();
        DialogManager.Instance.HideDialog(DialogIndex.WinDialog);
    }
    public void OnRestartLevel()
    {
        
    }
}
