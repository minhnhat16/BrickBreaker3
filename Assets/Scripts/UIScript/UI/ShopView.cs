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
    private ConfigItemRecord cf;
    public Text total_lb;
    public Sprite gold_bar; 

    public void Setup(ConfigItemRecord cf)
    {
        this.cf = cf;
        ItemData dataItem = DataAPIController.instance.GetItemData(cf.ID);
        if (dataItem != null)
        {
            total_lb.text = dataItem.total.ToString();
        }
        else
        {
            total_lb.text = "";
        }
        name_lb.text = cf.name;
        Icon.overrideSprite = SpriteLibraryControl.Instance.GetSpriteByName(cf.Icon);
        Icon.SetNativeSize();
        DataTrigger.RegisterValueChange(DataPath.ITEM + "/" + cf.ID.ToKey(), (data) =>
        {
            dataItem = (ItemData)data;
            total_lb.text = "Total " + dataItem.total;
        });

    }
    public override void OnStartShowView()
    {
        total_lb.text = DataAPIController.instance.GetGold().ToString();
    }
    public void OnAddGold()
    {
        int gold =Convert.ToInt32(total_lb.text);
        gold += 10;
        DataAPIController.instance.SaveGold(gold);
        total_lb.text = DataAPIController.instance.GetGold().ToString();

    }
    public void OnMinusGold()
    {
        int gold = Convert.ToInt32(total_lb.text);
        gold -= 10;
        DataAPIController.instance.SaveGold(gold);
        total_lb.text = DataAPIController.instance.GetGold().ToString();

    }
    public void OnShowInfo()
    {
        // DialogManager.Instance.ShowDialog(DialogIndex.BuyConfirmDialog, new Dialog)

    }
    public void OnSelectLevel()
    {
        ViewManager.Instance.SwitchView(ViewIndex.SelectLevelView);
    }
}

public class ItemView
{
}