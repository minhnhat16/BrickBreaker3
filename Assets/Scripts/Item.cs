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
    [HideInInspector] private Paddle paddle;
    public ItemType type;
    SpriteRenderer spriteRenderer;
    public Vector2 circleDistance;
    public Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
    public float cornerDistance_sq;
    public float scaleSpeed = 2f;
    public float radius;

    private void Start()
    {
        paddle = InGameController.Instance.GetComponent<Paddle>();
    }
    private void Update()
    {
        if (intersects(this, paddle))
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
                    paddle.isShortBar = true;
                    targetScale = new Vector3(1.5f, 1.5f, 1.5f);
                    //paddle.transform.GetChild(0).GetComponent<Transform>().DOScaleX(0.7f, 0.7f);
                    paddle.transform.localScale = Vector3.Lerp(paddle.transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
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
                    paddle.isSpeedUp = true;
                    paddle.paddleSpeed = 15f;
                    
                    ItemPoolManager.instance.pool.DeSpawnNonGravity(this);

                    break;
                case ItemType.SPEED_DOWN:
                    paddle.isSpeedDown = true;
                    paddle.paddleSpeed = 5f;
                    ItemPoolManager.instance.pool.DeSpawnNonGravity(this);

                    break;
                case ItemType.SHORT_BAR:
                    paddle.isShortBar = true;
                    targetScale = new Vector3(0.7f, 0.7f, 0.7f);
                    //paddle.transform.GetChild(0).GetComponent<Transform>().DOScaleX(0.7f, 0.7f);
                    paddle.transform.localScale = Vector3.Lerp(paddle.transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
                    ItemPoolManager.instance.pool.DeSpawnNonGravity(this);
                    break;
                default:
                    break;
            }
        }

        if (transform.position.y < CameraMain.instance.GetBottom())
        {
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
        rect = paddle;

        circleDistance.x = Mathf.Abs(this.transform.position.x - rect.GetComponent<Transform>().position.x);
        circleDistance.y = Mathf.Abs(this.transform.position.y - rect.GetComponent<Transform>().position.y);

        if (circleDistance.x > (rect.GetComponent<BoxCollider2D>().size.x / 2 + this.GetComponent<CircleCollider2D>().radius)) { return false; }
        if (circleDistance.y > (rect.GetComponent<BoxCollider2D>().size.y / 2 + this.GetComponent<CircleCollider2D>().radius)) { return false; }

        if (circleDistance.x <= (rect.GetComponent<BoxCollider2D>().size.x / 2)) { return true; }
        if (circleDistance.y <= (rect.GetComponent<BoxCollider2D>().size.y / 2)) { return true; }

        cornerDistance_sq = (circleDistance.x - rect.GetComponent<BoxCollider2D>().size.x / 2) * (circleDistance.x - rect.GetComponent<BoxCollider2D>().size.x / 2) +
                             (circleDistance.y - rect.GetComponent<BoxCollider2D>().size.y / 2) * (circleDistance.y - rect.GetComponent<BoxCollider2D>().size.y / 2);

        return (cornerDistance_sq <= (this.GetComponent<CircleCollider2D>().radius * this.GetComponent<CircleCollider2D>().radius));
    }
}


