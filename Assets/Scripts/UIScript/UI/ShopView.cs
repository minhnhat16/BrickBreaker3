using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : BaseView
{
    public void OnShowInfo()
    {
        // DialogManager.Instance.ShowDialog(DialogIndex.BuyConfirmDialog, new Dialog)
    }
}

public class ItemView
{
    public Text name_lb;
    public Image Icon;
    private ConfigItemRecord cf;
    public Text total_lb;

    public void Setup(ConfigItemRecord cf)
    {
        this.cf = cf;
        ItemData dataItem = DataAPIController.instance.GetItemData(cf.ID);
        if(dataItem != null)
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
}