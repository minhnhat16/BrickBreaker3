using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour
{
    public Text amount_lb;
    public Text bonus_lb;
    [SerializeField] private ShopView shopView;
    private int buy_gold;
    private int total_gold;

    private void Start()
    {
        buy_gold = Convert.ToInt32(amount_lb.text);
        total_gold = DataAPIController.instance.GetGold();
    }
    public void OnBuyButton()
    {
        BuyConfirmDialogParam param = new BuyConfirmDialogParam();
        param.bonus_lb = bonus_lb.text;
        param.amount_lb = amount_lb.text;
        DialogManager.Instance.ShowDialog(DialogIndex.BuyConfirmDialog,param);
    }
}
