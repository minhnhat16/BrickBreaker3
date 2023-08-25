using UnityEngine;
using UnityEngine.UI;

public class StartItem : MonoBehaviour
{
    public ItemType Type;
    public GameObject checkStatus;
    public bool checkUse;
    //[SerializeField] private int bigBallTotal;
    //[SerializeField] private int powerTotal;
    //[SerializeField] private int liveTotal;
    [SerializeField] private Text amount;
    [SerializeField] private string type;
    [SerializeField] private int total;
    // Start is called before the first frame update
    void Start()
    {
        type = GetItemType();
        total = DataAPIController.instance.GetItemTotal(type);
        amount.text = total.ToString();

    }
    // Update is called once per frame
    void Update()
    {
    }

    public void OnItemButton()
    {
        int total = DataAPIController.instance.GetItemTotal(type);
        if (!checkUse && total > 0)
        {
            checkStatus.SetActive(true);
            checkUse = true;
        }
        else
        {
            checkStatus.SetActive(false);
            checkUse = false;
        }

    }
    public void ItemDataCalculation(string type)
    {
        Debug.Log("ItemDataCalculation");
        int total = DataAPIController.instance.GetItemTotal(type);
        if (total > 0)
        {
            total -= 1;
            amount.text = total.ToString();

        }
        DataAPIController.instance.SetItemTotal(type, total);
    }
    public void OnItemType()
    {
        if (checkUse)
        {
            Debug.Log("CHECK USE" + checkUse);
            switch (Type)
            {
                case (ItemType.BIGBALL):
                    Debug.Log("BIGBALL Item");
                    InGameController.Instance.isLongBar = true;
                    InGameController.Instance.isScaleUp = true;
                    ItemDataCalculation("0");
                    Debug.Log($"InGameController.Instance.isLongBar ={InGameController.Instance.isLongBar} InGameController.Instance.isScaleUp ={InGameController.Instance.isScaleUp}");
                    break;
                case (ItemType.POWER):
                    Debug.Log("POWER Item");
                    InGameController.Instance.isItemTypePower = true;
                    ItemDataCalculation("1");

                    Debug.Log("InGameController.Instance.isItemTypePower" + InGameController.Instance.isItemTypePower);
                    //checkStatus.SetActive(true);
                    break;
                case (ItemType.ADD_LIVE):
                    Debug.Log("ADD_LIVE Item");
                    //Add data status
                    InGameController.Instance.lives =2 ;
                    ItemDataCalculation("2");
                    Debug.Log("LIVES" + InGameController.Instance.lives);
                    //checkStatus.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        //else
        //{
        //    Debug.Log("CHECK USE" + checkUse);
        //    OffItemType();
        //}
    }
    public void OffItemType()
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
                if (InGameController.Instance.lives > 0)
                {
                    InGameController.Instance.lives = 1;
                }
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
                type = "0";
                return type;
            case (ItemType.POWER):
                type = "1";
                return type;
            case (ItemType.ADD_LIVE):

                type = "2";
                return type;
            default:
                return null;
        }

    }
}
