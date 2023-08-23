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

    public override void Setup(DialogParam dialogParam)
    {
        base.Setup(dialogParam);
        if(dialogParam != null)
        {
            Debug.Log("LEVEL DATA NULL");

            WinDialogParam param = (WinDialogParam)dialogParam;
            _crLevel = param.crLevel;
            _nextLv = param.nextLv;
            _score = param.score;
            _star = param.star;
        }
        //DataTrigger.RegisterValueChange(DataPath.LEVEL + "/" + _crLevel.ToKey(), (data) =>
        //{
        //    = (LevelData)data;
        //    Debug.Log("TRIGGER VALUE CHANGE");
        //});
    }
    public override void OnStartShowDialog()
    {
        
    }
    public override void OnEndShowDialog()
    {
        //Debug.Log("On Start Hide Dialog");
    }
    public void OnNextLevelButton()
    {
        SaveOnWin();
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

        InGameController.Instance.isGameOver = false;
        GameManager.Instance.currentScore = 0;
        LoadLevel.instance.ResetData();
        InGameController.Instance.DeSpawnAll();
        DialogManager.Instance.HideDialog(DialogIndex.WinDialog);
        ViewManager.Instance.SwitchView(ViewIndex.SelectLevelView);
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
        Debug.Log("Save On Win");
        DataAPIController.instance.SaveLevel(_crLevel, _score, _star, true);
        DataAPIController.instance.SaveHighestLevel(_nextLv);
        //DataAPIController.instance.SaveCurrentLevel(_crLevel);
    }
}
