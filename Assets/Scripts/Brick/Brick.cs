using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : MonoBehaviour
{
    //private bool breakable = false;
    public int brickType;
    public int brickHealth;
    public BallSystem ballSystem;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    public List<Sprite> sprites = new List<Sprite>();
    public Vector2 normalVector;
    public enum BrickType
    {
        Red,
        Gray,
        Green,
        Orange,
        Yellow,
        Blue

    };
    public BrickTypeScriptableObject brickTypeScriptableObject;
    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")){
            if (brickType == 1 )
            {
                brickHealth--;
                DestroyBrick();
                GameManager.Instance.currentScore += 100;

            }
            else if(brickType == 2)
            {
                Debug.Log("Hit rock");

            }

        }
    }
    public void DestroyBrick()
    {
        if(brickHealth <= 0)
        {
            BrickPoolManager.instance.pool.DeSpawnNonGravity(this);
        }
    }
    //public void OnContact(RaycastHit2D hit, BallSystem ball)
    //{
    //    switch (brickType)
    //    {
    //        case 1:
    //            brickHealth--;
    //            if (!ball.onItemPowerUP)
    //            {
    //                normalVector = hit.point - (Vector2)this.transform.position;
    //            }
    //            else
    //            {
    //                brickHealth = 0;
    //            }
    //            if(brickHealth == 0)
    //            {
    //                DeSpawnBrick();
    //            }
    //            break;
    //        case 2:
    //            {
    //                normalVector = hit.point - (Vector2)(this.transform.position);
    //                normalVector.Normalize();

    //                ball.moveDirection = Vector2.Reflect(ball.moveDirection, normalVector);
    //                break;
    //            }
    //        default:
    //            break;
    //    }
    //}
    public void SettingBrick(int type)
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D> ();
        switch (type)
        {
            case 0:
                BrickPoolManager.instance.pool.DeSpawnNonGravity(this);
                break;
            case 1: //yellow
                spriteRenderer.sprite = sprites[5];
                spriteRenderer.color = new Color32(245, 217, 32, 255);
                brickType = 1;
                brickHealth = 1;
                 break;
            case 2: //green
                spriteRenderer.sprite = sprites[5];
                spriteRenderer.color = new Color32(7, 214, 35, 255);
                brickType = 1;
                brickHealth = 1;
                break;
            case 3: // blue
                spriteRenderer.sprite = sprites[5];
                spriteRenderer.color = new Color32(41, 130, 252, 255);
                brickType = 1;
                brickHealth = 1;
                break;
            case 4: // orange
                spriteRenderer.sprite = sprites[5];
                spriteRenderer.color = new Color32(252, 125, 41, 255);
                brickType = 1;
                brickHealth = 1;
                break;
            case 5: // purple
                spriteRenderer.sprite = sprites[5];
                spriteRenderer.color = new Color32(168, 23, 242, 255);
                brickType = 1;
                brickHealth = 1;
                break;
            case 6: // red
                spriteRenderer.sprite = sprites[5];
                spriteRenderer.color = new Color32(255, 39, 85, 255);
                brickType = 1;
                brickHealth = 1;
                break;
            case 7: // white
                spriteRenderer.sprite = sprites[5];
                spriteRenderer.color = Color.white;
                brickType = 1;
                brickHealth = 1;
                break;
            case 8: // deep green
                spriteRenderer.sprite = sprites[5];
                spriteRenderer.color = new Color32(52, 152, 35, 255);
                brickType = 1;
                brickHealth = 1;
                break;
            case 9: // brown
                spriteRenderer.sprite = sprites[5];
                spriteRenderer.color = new Color32(77, 48, 15, 255);
                brickType = 1;
                brickHealth = 1;
                break;
            case 10: // vani
                spriteRenderer.sprite = sprites[5];
                spriteRenderer.color = new Color32(250, 221, 190, 255);
                brickType = 1;
                brickHealth = 1;
                break;
            case 11: // yellow 2
                spriteRenderer.sprite = sprites[2];
                spriteRenderer.color = new Color32(245, 217, 32, 255);
                brickType = 1;
                brickHealth = 2;
                break;
            case 12: // green 2
                spriteRenderer.sprite = sprites[2];
                spriteRenderer.color = new Color32(7, 214, 35, 255);
                brickType = 1;
                brickHealth = 2;
                break;
            case 13: // blue 2
                spriteRenderer.sprite = sprites[2];
                spriteRenderer.color = new Color32(41, 130, 252, 255);
                brickType = 1;
                brickHealth = 2;
                break;
            case 14: // orange 2
                spriteRenderer.sprite = sprites[2];
                spriteRenderer.color = new Color32(252, 125, 41, 255);
                brickType = 1;
                brickHealth = 2;
                break;
            case 15: // purple 2
                spriteRenderer.sprite = sprites[2];
                spriteRenderer.color = new Color32(168, 23, 242, 255);
                brickType = 1;
                brickHealth = 2;
                break;
            case 16: // red 2
                spriteRenderer.sprite = sprites[2];
                spriteRenderer.color = new Color32(255, 39, 85, 255);
                brickType = 1;
                brickHealth = 2;
                break;
            case 17: // white 2
                spriteRenderer.sprite = sprites[2];
                spriteRenderer.color = Color.white;
                brickType = 1;
                brickHealth = 2;
                break;
            case 18: // brown 2
                spriteRenderer.sprite = sprites[2];
                spriteRenderer.color = new Color32(77, 48, 15, 255);
                brickType = 1;
                brickHealth = 2;
                break;
            case 19: // deep green 2
                spriteRenderer.sprite = sprites[2];
                spriteRenderer.color = new Color32(52, 152, 35, 255);
                brickType = 1;
                brickHealth = 2;
                break;
            case 20: // vani 2
                spriteRenderer.sprite = sprites[2];
                spriteRenderer.color = new Color32(250, 221, 190, 255);
                brickType = 1;
                brickHealth = 2;
                break;
            case 21: // diamond brick
                spriteRenderer.sprite = sprites[0];
                SetBigBrick();
                brickType = 2;
                break;
            case 22: // circle brick
                spriteRenderer.sprite = sprites[1];
                SetBigBrick();
                brickType = 2;
                break;
            case 23: // stone brick
                spriteRenderer.sprite = sprites[3];
                spriteRenderer.color = Color.white;
                brickType = 1;
                brickHealth = 3;
                break;
            case 24: // wall brick
                spriteRenderer.sprite = sprites[4];
                spriteRenderer.color = Color.white;
                brickType = 2;
                break;
            default:
                break;
        }
    }
    private void SetBigBrick()
    {
        spriteRenderer.color = Color.white;
        transform.position -= new Vector3(0, 0, 0);
        boxCollider2D.size = new Vector2(1f,1f);
    }
    private void DeSpawnBrick()
    {
        BrickPoolManager.instance.pool.DeSpawnNonGravity(this);
        
        //IF: is boss level
    }
}
