using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : BaseView
{

    public Text name_lb;
    public Image Icon;
    public Text total_lb;
    public Sprite gold_bar;

    public override void Setup(ViewParam viewParam) { 
        base.Setup(viewParam);
        if (viewParam != null)
        {
            ShopViewParam param = (ShopViewParam)viewParam;
            total_lb.text = param.totalGold.ToString();
        }
    }
    public override void ShowView()
    {
        total_lb.text = DataAPIController.instance.GetGold().ToString();
        base.ShowView();
    }
    public override void OnStartShowView()
    {
        total_lb.text = DataAPIController.instance.GetGold().ToString();
    }
    public void OnUpdateGold()
    {
        total_lb.text = DataAPIController.instance.GetGold().ToString();

    }
    public void OnSelectLevel()
    {
        ViewManager.Instance.SwitchView(ViewIndex.SelectLevelView);
    }
}

public class ItemView
{
}