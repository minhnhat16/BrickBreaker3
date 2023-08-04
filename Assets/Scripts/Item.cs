using DG.Tweening;
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
    SHORT_BAR,
    LONG_BAR,
}

public class Item : MonoBehaviour
{
    public ItemType type;
    SpriteRenderer spriteRenderer;
    public Vector3 circleDistance;
    public Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
    public float cornerDistance_sq;
    public float scaleSpeed = 5f;
    public float radius;

    private void Start()
    {
    }
    private void Update()
    {
        if (intersects(this, Paddle.instance))
        {
            switch (type)
            {
                case ItemType.BIGBALL:
                    BallEvent.onScaleUp?.Invoke();
                    ItemPoolManager.instance.pool.DeSpawnNonGravity(this);

                    break;
                case ItemType.TRIPLLE:
                    PaddleEvent.onTripple?.Invoke();
                    ItemPoolManager.instance.pool.DeSpawnNonGravity(this);

                    break;
                case ItemType.LONG_BAR:
                    Paddle.instance.isLongBar = true;
                    targetScale = new Vector3(1.5f, 1.5f, 1.5f);
                    Paddle.instance.transform.GetChild(0).GetComponent<Transform>().DOScaleX(0.7f, 0.7f);
                    Paddle.instance.transform.localScale = Vector3.Lerp(Paddle.instance.transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
                    ItemPoolManager.instance.pool.DeSpawnNonGravity(this);

                    break;
                case ItemType.MAGNET:
                    BallEvent.onMagnet?.Invoke();
                    ItemPoolManager.instance.pool.DeSpawnNonGravity(this);

                    break;
                case ItemType.POWER:
                    BallEvent.onPower?.Invoke();
                    ItemPoolManager.instance.pool.DeSpawnNonGravity(this);

                    break;
                case ItemType.SPEED_UP:
                    Paddle.instance.isSpeedUp = true;
                    Paddle.instance.paddleSpeed = 15f;
                    
                    ItemPoolManager.instance.pool.DeSpawnNonGravity(this);

                    break;
                case ItemType.SPEED_DOWN:
                    Paddle.instance.isSpeedDown = true;
                    Paddle.instance.paddleSpeed = 5f;
                    ItemPoolManager.instance.pool.DeSpawnNonGravity(this);

                    break;
                case ItemType.SHORT_BAR:
                    Paddle.instance.isShortBar = true;
                    targetScale = new Vector3(0.7f, 0.7f, 0.7f);
                    Paddle.instance.transform.GetChild(0).GetComponent<Transform>().DOScaleX(0.7f, 0.7f);
                    Paddle.instance.transform.localScale = Vector3.Lerp(Paddle.instance.transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
                    ItemPoolManager.instance.pool.DeSpawnNonGravity(this);
                    break;
                default:
                    break;
            }
        }

        if (transform.position.y < -10 )
        {
            Debug.Log("de5pawn item" + transform.position);
            ItemPoolManager.instance.pool.DeSpawnNonGravity(this);
        }
    }
    public void SetUp(ItemType itemType,Sprite sprite)
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = sprite;
        this.type = itemType;
    }
    bool intersects(Item circle, Paddle rect)
    {
        //Debug.LogWarning("Start InterSects");
        //Debug.LogWarning("Position item:" + transform.position);
        rect = Paddle.instance;

        circleDistance.x = Mathf.Abs(this.transform.position.x - rect.GetComponent<Transform>().position.x);
        circleDistance.y = Mathf.Abs(this.transform.position.y - rect.GetComponent<Transform>().position.y);
        //Debug.Log(" Circledistance x " + circleDistance.x + "Circledistance y " +    circleDistance.y);
        if (circleDistance.x > (rect.GetComponent<BoxCollider2D>().size.x / 2 + this.GetComponent<CircleCollider2D>().radius)) { return false; }
        if (circleDistance.y > (rect.GetComponent<BoxCollider2D>().size.y / 2 + this.GetComponent<CircleCollider2D>().radius)) { return false; }

        if (circleDistance.x <= (rect.GetComponent<BoxCollider2D>().size.x / 2)) { return true; }
        if (circleDistance.y <= (rect.GetComponent<BoxCollider2D>().size.y / 2)) { return true; }

        cornerDistance_sq = (circleDistance.x - rect.GetComponent<BoxCollider2D>().size.x / 2) * (circleDistance.x - rect.GetComponent<BoxCollider2D>().size.x / 2) +
                             (circleDistance.y - rect.GetComponent<BoxCollider2D>().size.y / 2) * (circleDistance.y - rect.GetComponent<BoxCollider2D>().size.y / 2);

        return (cornerDistance_sq <= (this.GetComponent<CircleCollider2D>().radius * this.GetComponent<CircleCollider2D>().radius));
    }
}


