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
    public override void OnStartShowView()
    {
        //Debug.Log("Onstart show select level view");
        ScrollView.GetComponentInChildren<LevelManager>().SpawnLevel();

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
