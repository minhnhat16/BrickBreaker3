using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayView : BaseView
{
    public void Update()
    {
        InGameController.Instance.CheckCompleteScore();
    }
    public void CompleteLevel()
    {
      
    }
    public void LoadWinDialog()
    {
        DialogManager.Instance.ShowDialog(DialogIndex.WinDialog);

    }
    public override void OnStartShowView()
    {
      
    }
    public void OnPauseButton()
    {
        InGameController.Instance.PauseGame();
        DialogManager.Instance.ShowDialog(DialogIndex.PauseDialog);
    }
}
