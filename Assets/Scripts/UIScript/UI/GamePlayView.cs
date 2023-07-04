using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayView : BaseView
{
    public void Update()
    {
    }
    public void OnPauseButton()
    {
        InGameController.Instance.PauseGame();
        DialogManager.Instance.ShowDialog(DialogIndex.PauseDialog);
    }
}
