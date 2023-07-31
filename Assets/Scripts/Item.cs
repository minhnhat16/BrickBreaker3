using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    BIGBALL,
    TRIPLLE,
    SCALEUP,
    MAGNET,
    POWER,
    SPEED_UP,
    SPEED_DOWN,
}
public class Item : MonoBehaviour
{
    public ItemType type;
    SpriteRenderer spriteRenderer;
    public CircleCollider2D circleCollider;
    public float radius;

    public void SetUp(ItemType itemType,Sprite sprite)
    {
        if(spriteRenderer == null)
        {
           
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = sprite;
        this.type = itemType;
    }

}


