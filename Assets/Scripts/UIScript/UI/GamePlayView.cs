using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayView : BaseView
{

    public override void OnStartShowView()
    {
        //InGameController.Instance.CheckCompleteScore();
        //InGameController.Instance.UpdateTimer(InGameController.Instance.CalStar, 10f);
        //InGameController.Instance.UpdateTimer(InGameController.Instance.CalStar, 10f);
    }
    public override void OnEndHideView()
    {
        InGameController.Instance.DeSpawnAll();
    }
    public void CompleteLevel()
    {
      
    }
    public void LoadWinDialog()
    {
        DialogManager.Instance.ShowDialog(DialogIndex.WinDialog);

    }
    public void OnPauseButton()
    {
        InGameController.Instance.PauseGame();
        DialogManager.Instance.ShowDialog(DialogIndex.PauseDialog);
    }
}
