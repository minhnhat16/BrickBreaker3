using UnityEngine;

public class WinDialog : BaseDialog
{
    [SerializeField] private string TextLevel;

    public void OnNextLevelButton()
    {
        int _crLevel = LoadLevel.instance.currentLevelLoad + 1;
        InGameController.Instance.isGameOver = false;
        BrickPoolManager.instance.destroyCount = 0;
        LoadLevel.instance.LevelSelect(_crLevel.ToString());
        GameManager.Instance.LoadOnInGameController();
        LoadLevel.instance.currentLevelLoad = _crLevel;
        DialogManager.Instance.HideDialog(DialogIndex.WinDialog);
    }
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
