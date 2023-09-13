using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Brick : MonoBehaviour, InteractBall
{
    //private bool breakable = false;
    public int brickType;
    public int brickHealth;
    public BallSystemVer2 ballSystemV2;
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
    public void DestroyBrick()
    {
        BrickPoolManager.instance.pool.DeSpawnNonGravity(this);
        InGameController.Instance.currentScore += 100;

    }
    public void OnContact(RaycastHit2D hit, BallSystemVer2 ball)
    {
        if (CheckBallPositionInBrick(ball))
        {
            ball.transform.position = Vector3.zero;
        }
        else
        {
            switch (brickType)
            {
                case 1:
                    brickHealth--;
                    if (!ball.onItemPowerUP)
                    {
                        Debug.Log("BRICK TYPE 2");
                        //ball.CheckBallAngle(Vector3.up);
                        float temp;
                        if (hit.point.x == 0)
                        {
                            temp = Random.Range(-4, 4);
                            normalVector = new Vector2(temp, -hit.point.y).normalized;
                            ball.moveDir = Vector2.Reflect(ball.moveDir.normalized, normalVector.normalized);



                        }
                        else
                        {

                        }
                        {
                            temp = hit.point.x;
                            normalVector = new Vector2(temp, -hit.point.y).normalized;

                            ball.moveDir = Vector2.Reflect(hit.point, ball.tempDirection - transform.position).normalized;


                        }

                        normalVector.Normalize();
                        Debug.DrawLine(hit.point, normalVector, Color.magenta);
                        //if(ball.tempDirection == Vector3.zero)
                        //{
                        //    ball.moveDir = Vector3.down;
                        //}
                    }
                    else
                    {
                        brickHealth = 0;
                    }
                    if (brickHealth == 0)
                    {
                        DestroyBrick();
                    }
                    break;
                case 2:
                    //Debug.Log("BRICK TYPE 2");
                    normalVector = new Vector2(-hit.point.y, hit.point.x).normalized /*- (Vector2)this.transform.position.normalized*/;
                    normalVector.Normalize();
                    Debug.DrawLine(hit.point, normalVector, Color.red);
                    ball.moveDir = Vector2.Reflect(hit.point,transform.position + Vector3.up).normalized;
                    break;
            }
        }
        }

    private void ClaimDir(BallSystemVer2 ball, RaycastHit2D hit, Vector2 normalVector)
    {

        Debug.Log("NORMAL VECTOR " + normalVector);

        if (ball.transform.position.y < this.transform.position.y)
        {

            if (ball.transform.position.x > this.transform.position.x)
            {
                Debug.Log("Case 2 DOWN RIGHT");
                Debug.Log(ball.transform.position + " + " + this.transform.position + " + " + hit.point);



                Vector2 vect = new Vector2(1, -1);
                ball.moveDir = Vector2.Reflect(hit.point, (Vector2)ball.tempDirection - vect).normalized;

            }
            else if (ball.transform.position.x < this.transform.position.x)
            {
                Debug.Log("Case 2 DOWN LEFT");
                Debug.Log(ball.transform.position + " + " + this.transform.position + " + " + hit.point);

                Vector2 vect = new Vector2(-1, -1);
                ball.moveDir = Vector2.Reflect(hit.point, (Vector2)ball.tempDirection - vect).normalized;


            }
        }
        else if (ball.transform.position.y > this.transform.position.y)
        {


            if (ball.transform.position.x > this.transform.position.x)
            {
                Debug.Log("Case 2 UP RIGHT");
                Debug.Log(ball.transform.position + " + " + this.transform.position + " + " + hit.point);

                Vector2 vect = new Vector2(1, 1);
                ball.moveDir = Vector2.Reflect(hit.point, (Vector2)ball.tempDirection - vect).normalized;

            }
            else if (ball.transform.position.x < this.transform.position.x)
            {
                Debug.Log("Case 2 UP LEFT");
                Vector2 vect = new Vector2(-1, 1);
                ball.moveDir = Vector2.Reflect(hit.point, (Vector2)ball.tempDirection - vect).normalized;
                Debug.Log(ball.transform.position + " + " + this.transform.position + " + " + hit.point);

            }
        }
    }

    private bool CheckBallPositionInBrick(BallSystemVer2 ball)
    {
        Bounds bounds = boxCollider2D.bounds;
        Vector2 min = bounds.min;
        Vector2 max = bounds.max;
        float x = ball.transform.position.x;
        float y = ball.transform.position.y;
        if (!((x + ball.ballRadius < min.x && x - ball.ballRadius > max.x) && (y + ball.ballRadius < min.y && y - ball.ballRadius > max.y)))
        {

            Debug.Log("IN BRICK");
            LerpBallPosition(ball, min, max);

            return false;
        }
        else {
        Debug.Log("OUT BRICK");
            LerpBallPosition(ball, min, max);
        return true;
        }
    }
    private void LerpBallPosition(BallSystemVer2 ball, Vector3 min, Vector3 max) 
    {
        float x;
        float y;
        if (ball.transform.position.x < min.x)
        {
             x = Mathf.Lerp(ball.transform.position.x, min.x, 0.1f);
            if (ball.transform.position.y < min.y)
            {
                y = Mathf.Lerp(ball.transform.position.y, min.y, 0.1f);
                ball.transform.position = new Vector3(x, y);

            }
            else
            {
                y = Mathf.Lerp(ball.transform.position.y, max.y, 0.1f);
                ball.transform.position = new Vector3(x, y);

            }
        }
        else
        {
           x = Mathf.Lerp(ball.transform.position.x, max.x, 0.1f);
            if (ball.transform.position.y < min.y)
            {
                y = Mathf.Lerp(ball.transform.position.y, min.y, 0.1f);
                ball.transform.position = new Vector3(x, y);
            }
            else
            {
                y = Mathf.Lerp(ball.transform.position.y, max.y, 0.1f);
                ball.transform.position = new Vector3(x, y);

            }
        }

    }
    //private void ClaimpBallPosition(BallSystemVer2 ball, RaycastHit2D hit, Vector2 normalVector)
    //{
    //    float x;
    //    float y;
    //    Debug.Log("NORMAL VECTOR " + normalVector);

    //    if (ball.transform.position.y < this.transform.position.y)
    //    {

    //        if (ball.transform.position.x > this.transform.position.x)
    //        {
    //            Debug.Log("Case 2 DOWN RIGHT");

    //            Debug.Log(ball.transform.position + " + " + this.transform.position);

    //            x = Mathf.Clamp(ball.transform.position.x, hit.point.x + ball.ballRadius + 0.05f, hit.point.x + ball.ballRadius + 0.1f);
    //            y = Mathf.Clamp(ball.transform.position.y, hit.point.y - ball.ballRadius - 0.05f, hit.point.y - ball.ballRadius - 0.1f);
    //            Vector2 vect = new Vector2(1, -1);
    //            ball.moveDir = Vector2.Reflect(hit.point, (Vector2)ball.tempDirection - vect).normalized;
    //            ball.transform.position = new Vector3(x, y, 0);

    //        }
    //        else if (ball.transform.position.x < this.transform.position.x)
    //        {
    //            Debug.Log("Case 2 DOWN LEFT");
    //            Debug.Log(ball.transform.position + " + " + this.transform.position);

    //            x = Mathf.Clamp(ball.transform.position.x, hit.point.x - ball.ballRadius - 0.05f, hit.point.x - ball.ballRadius + 0.1f);
    //            y = Mathf.Clamp(ball.transform.position.y, hit.point.y - ball.ballRadius - 0.05f, hit.point.y - ball.ballRadius + 0.1f);
    //            //ball.moveDir = Vector2.Reflect(-ball.moveDir.normalized, normalVector.normalized);
    //            Vector2 vect = new Vector2(-1, -1);
    //            ball.moveDir = Vector2.Reflect(hit.point, (Vector2)ball.tempDirection - vect).normalized;

    //            ball.transform.position = new Vector3(x, y, 0);

    //        }
    //    }
    //    else if (ball.transform.position.y > this.transform.position.y)
    //    {


    //        if (ball.transform.position.x > this.transform.position.x)
    //        {
    //            Debug.Log("Case 2 UP RIGHT");
    //            Debug.Log(ball.transform.position + " + " + this.transform.position);

    //            x = Mathf.Clamp(ball.transform.position.x, hit.point.x + ball.ballRadius + 0.05f, hit.point.x + ball.ballRadius + 0.1f);
    //            y = Mathf.Clamp(ball.transform.position.y, hit.point.y + ball.ballRadius + 0.05f, hit.point.y + ball.ballRadius + 0.1f);
    //            ball.transform.position = new Vector3(x, y, 0);
    //            Vector2 vect = new Vector2(1, 1);
    //            ball.moveDir = Vector2.Reflect(hit.point, (Vector2)ball.tempDirection - vect).normalized;

    //        }
    //        else if (ball.transform.position.x < this.transform.position.x)
    //        {
    //            Debug.Log("Case 2 UP LEFT");
    //            Debug.Log(ball.transform.position + " + " + this.transform.position);
    //            x = Mathf.Clamp(ball.transform.position.x, hit.point.x - ball.ballRadius - 0.05f, hit.point.x - ball.ballRadius - 0.1f);
    //            y = Mathf.Clamp(ball.transform.position.y, hit.point.y + ball.ballRadius + 0.05f, hit.point.y + ball.ballRadius - 0.1f);
    //            ball.transform.position = new Vector3(x, y, 0);
    //            Vector2 vect = new Vector2(-1, 1);
    //            ball.moveDir = Vector2.Reflect(hit.point, (Vector2)ball.tempDirection - vect).normalized;

    //        }
    //    }
    //}
    public void SettingBrick(int type)
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
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
        transform.position -= new Vector3(0, -0.3f, 0);
        boxCollider2D.size = new Vector2(0.75f, 0.55f);
    }

}
