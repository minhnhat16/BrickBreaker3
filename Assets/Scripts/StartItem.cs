using NaughtyAttributes.Test;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StartItem : MonoBehaviour
{
    public ItemType Type;
    public GameObject checkStatus;
    public bool checkUse;
    [SerializeField] private int bigBallTotal;
    [SerializeField] private int powerTotal;
    [SerializeField] private int liveTotal;
    [SerializeField] private Text amount;
    
    // Start is called before the first frame update
    void Start()
    {
        int total = DataAPIController.instance.GetItemTotal();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnItemButton()
    {
        if (!checkUse)
        {
            checkStatus.SetActive(true);
            OnItemType();
            checkUse =true;
        }
        else
        {
            checkStatus.SetActive(false);
            OffItemType();
            checkUse= false;
        }

    }
    private void ItemDataCalculation(string type)
    {
        Debug.Log("ItemDataCalculation");
        int total = DataAPIController.instance.GetItemTotal(type);
        if (total > 0)
        {
            total -= 1;
        }
        DataAPIController.instance.SetItemTotal(type,total);
    }
    public void OnItemType()
    {
        switch (Type)
        {
            case (ItemType.BIGBALL):
                Debug.Log("BIGBALL Item");
                InGameController.Instance.isLongBar = true;
                InGameController.Instance.isScaleUp = true;
                Debug.Log($"InGameController.Instance.isLongBar ={InGameController.Instance.isLongBar} InGameController.Instance.isScaleUp ={InGameController.Instance.isScaleUp}");
                ItemDataCalculation("0");
                break;
            case (ItemType.POWER):
                Debug.Log("POWER Item");
                InGameController.Instance.isItemTypePower = true;
                ItemDataCalculation("1");

                Debug.Log("InGameController.Instance.isItemTypePower" + InGameController.Instance.isItemTypePower );
                //checkStatus.SetActive(true);
                break;
            case (ItemType.ADD_LIVE):
                Debug.Log("ADD_LIVE Item");
                //Add data status
                ItemDataCalculation("2");

                InGameController.Instance.lives += 1;
                Debug.Log("LIVES" + InGameController.Instance.lives);
                //checkStatus.SetActive(true);
                break;
            default:
                break;
        }
    }
    private void OffItemType()
    {
        switch (Type)
        {
            case (ItemType.BIGBALL):
                Debug.Log("OFF BIGBALL Item");
                InGameController.Instance.isLongBar = false;
                InGameController.Instance.isScaleUp = false;
                //Add data status
                Debug.Log($"InGameController.Instance.isLongBar ={InGameController.Instance.isLongBar} InGameController.Instance.isScaleUp ={InGameController.Instance.isScaleUp}");
                break;
            case (ItemType.POWER):
                Debug.Log("OFF POWER Item");

                //Add data status
                InGameController.Instance.isItemTypePower = false;
                Debug.Log("InGameController.Instance.isItemTypePower" + InGameController.Instance.isItemTypePower);
                break;
            case (ItemType.ADD_LIVE):
                Debug.Log("OFF ADD_LIVE Item");
                //Add data status

                InGameController.Instance.lives -= 1;
                Debug.Log("LIVES" + InGameController.Instance.lives);
                break;
            default:
                break;
        }
    }
    private string GetItemType()
    {
        switch (Type)
        {
            case (ItemType.BIGBALL):

                return '0';
            case (ItemType.POWER):
                
                break;
            case (ItemType.ADD_LIVE):
               
                break;
            default:
                break;
        }

    }
}
