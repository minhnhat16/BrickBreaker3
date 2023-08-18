using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class WinDialog : BaseDialog
{
    [SerializeField] private string TextLevel;
    [SerializeField] private int _crLevel;
    [SerializeField] private int _nextLv;
    [SerializeField] private int _score;
    [SerializeField] private int _star;
    public override void OnStartShowDialog()
    {
        _crLevel = LoadLevel.instance.currentLevelLoad;
        _score = GameManager.Instance.currentScore;
        _star = GameManager.Instance.starCount;
        _nextLv = _crLevel + 1;
        GameManager.Instance.currentLevel = _nextLv;
       
    }
    public override void OnEndHideDialog()
    {
        SaveOnWin();
    }
    public void OnNextLevelButton()
    {

        InGameController.Instance.isGameOver = false;
        LoadLevel.instance.LevelSelect(_nextLv.ToString());
        GameManager.Instance.currentScore = 0;
        GameManager.Instance.LoadOnInGameController();
        LoadLevel.instance.currentLevelLoad = _nextLv;
        DialogManager.Instance.HideDialog(DialogIndex.WinDialog);
    }
    public void OnHomeBtn()
    {
        SaveOnWin();
        GameManager.Instance.currentScore = 0;

        ViewManager.Instance.SwitchView(ViewIndex.SelectLevelView);
        InGameController.Instance.DeSpawnAll();
        DialogManager.Instance.HideDialog(DialogIndex.WinDialog);
    }
    public void OnRestartLevel()
    {
        GameManager.Instance.currentLevel = _crLevel;
        DialogManager.Instance.HideDialog(DialogIndex.WinDialog);
        LoadLevel.instance.RestartLevel();
        InGameController.Instance.ResumeGame();
    }
    public void SaveOnWin()
    {
        DataAPIController.instance.SaveLevel(_crLevel, _score, _star, true);
        DataAPIController.instance.SaveHighestLevel(_nextLv);
        //DataAPIController.instance.SaveCurrentLevel(_crLevel);
    }
}
