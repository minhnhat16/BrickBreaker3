using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SelectLevelView : BaseView
{
    public GameObject ScrollView;
    public Text total_lb;
    public int highestlevel;
    private LevelConfigRecord lv_cf;

    public void Setup(LevelConfigRecord lv_cf)
    {
        this.lv_cf = lv_cf;
        LevelData levelData = DataAPIController.instance.GetLevelData(lv_cf.iD);
        if (levelData != null)
        {
            Debug.Log("LEVEL DATA NULL");
        }
        DataTrigger.RegisterValueChange(DataPath.LEVEL + "/" + lv_cf.iD.ToKey(), (data) =>
        {
            levelData = (LevelData)data;
            Debug.Log("TRIGGER VALUE CHANGE");
        });
    }
    public override void OnInit()
    {
        GameManager.Instance.currentLevel = DataAPIController.instance.GetHighestLevel();
    }
    public override void OnStartShowView()
    {
        Debug.Log("On Start Show View SelecDiaglog");
        GameManager.Instance.currentLevel = DataAPIController.instance.GetHighestLevel();
        ScrollView.GetComponentInChildren<LevelManager>().SpawnLevel();
        ScrollView.GetComponentInChildren<LevelManager>().CompleteLevelOn();
        ScrollView.GetComponentInChildren<LevelManager>().HighestLevelOn();
        int gold =  DataAPIController.instance.GetGold();
        total_lb.text = gold.ToString();
    }
    public override void OnEndHideView()
    {
        ScrollView.GetComponentInChildren<LevelManager>().HighestLevelOff();

    }
    public void OnShopButton()
    {
        ViewManager.Instance.SwitchView(ViewIndex.ShopView);
    }

}
