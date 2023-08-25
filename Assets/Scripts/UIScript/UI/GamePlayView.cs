using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayView : BaseView
{
    [SerializeField] private Text score_lb;
    private void Update()
    {
        UpdateScore();

    }
    public override void OnStartShowView()
    {

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
    private void UpdateScore()
    {
        //Debug.Log("UPDATE SCORE");
        int temp = 0;
        if(InGameController.Instance.currentScore > temp)
        {
            temp = InGameController.Instance.currentScore;
            score_lb.text = "Score: " + temp.ToString();
            //Debug.Log("SCORE_LB" + score_lb.text);
        }
    }
}
