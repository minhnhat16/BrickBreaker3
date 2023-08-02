using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class ItemPoolManager : MonoBehaviour
{
    public BY_Local_Pool<Item> pool;
    public Item prefab;
    public Item item;
    public List<ConfigItemRecord> itemConfig = new List<ConfigItemRecord>();
    public static ItemPoolManager instance;
    private void Awake()
    {
        instance = this;
        pool = new BY_Local_Pool<Item>(prefab, 50, this.transform);
    }

    public void SpawnItem()
    {
        int itemType = Random.Range(0, 2);
      
        float random = Random.Range(0f, 1f);
        float x = Random.Range(CameraMain.instance.GetLeft() + 2, CameraMain.instance.GetRight() - 2);
        int i;
        Debug.Log("itemType " + itemType);

        for (i = 0; i < itemConfig[itemType].itemData.Length; i++)
        {
            random -= itemConfig[itemType].itemData[i].rate;
            Debug.LogWarning("random" + random);

            if (random - itemConfig[itemType].itemData[i].rate <= 0)
            {
                //Debug.LogWarning("random" + random);

                break;
            }
        }

        item = pool.SpawnNonGravity();
        item.transform.position = new Vector2 (x, item.transform.position.y);
        item.SetUp(itemConfig[itemType].itemData[i].itemType, itemConfig[itemType].itemData[i].sprite);
    }
}
