using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BallSystem : FSMSystem
{
    #region
    [HideInInspector]
    public Ball_MovermentState MoveState;
    public Ball_SpawnState SpawnState;
    [HideInInspector]
    public Ball_DeathState DeathState;
    #endregion

    public ContactHandle contactHandle;
    public Paddle paddle;
    [SerializeField] private Transform Forward;
    [SerializeField] private Transform Anchor;

    [HideInInspector] public Vector2 forwardDirection { get => (Forward.position - Anchor.position).normalized; }
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public Vector3 direction1 = new Vector3(0, 4, 0);
    [HideInInspector] public Vector2 tempDirection;
    [HideInInspector] public Vector3 moveBall;
    [HideInInspector] public Vector3 spawnPosition = new Vector3(0, -7, 0);
    [HideInInspector] public float ballRadius;
    [HideInInspector] public bool isLseft = false;
    [HideInInspector] public bool isRight = false;
    [HideInInspector] public bool isTop = false;
    [HideInInspector] public bool onItemPowerUP = false; // in item powerup
    [HideInInspector] public float tempDirectionX;
    [HideInInspector] public float tempX = 0;
    [HideInInspector] public float tempY = 0;
    [HideInInspector] public float ballSpeed = 10f;
    [HideInInspector] public float maxAngle = 45f;
    [HideInInspector] public float temp;
    [HideInInspector] public float angleMoveSpeed = 0.2f;
    [HideInInspector] public float angle;
    [HideInInspector] public int maxLives;
    [HideInInspector] public int currentLive;
    private void Awake()
    {
       
    }

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        SetMaxLive();
        GotoState(SpawnState);
    }
    public void MoveBall()
    {
       transform.position += ballSpeed * Time.deltaTime * moveDirection;
    }
    public void AngleMoverment()
    {
        Debug.DrawRay(transform.position, direction1, Color.blue);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && (transform.position.x > -1.0f))
        {
            tempDirectionX = 1;
            AngleCalculation(tempDirectionX);
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (transform.position.x < 1.0f))
        {
            tempDirectionX = -1;
            AngleCalculation(tempDirectionX);
        }
    }
    private void AngleCalculation(float tempXDir)
    {
        tempX += angleMoveSpeed * tempXDir;
        tempX = Mathf.Clamp(tempX, CameraMain.instance.GetLeft() - 2, CameraMain.instance.GetRight() + 2);
        temp = (- CameraMain.instance.GetLeft() + CameraMain.instance.GetRight());
        tempY =  Mathf.Sqrt((temp*temp) - (tempX * tempX)) -5;
        direction1 = new Vector3(tempX, tempY, transform.position.z);
        Debug.DrawRay(transform.position, direction1, Color.blue);
        angle = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = Vector3.forward * angle;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Anchor.position, (Vector2)Anchor.position + (Vector2)moveDirection);
    } 
    public void GetBallDirection()
    {
        if (transform.position.x > CameraMain.instance.GetRight() - ballRadius)
        {
            tempDirection = Vector2.Reflect(moveDirection, Vector2.left);
            moveDirection = tempDirection;
        }
        else if (transform.position.x < CameraMain.instance.GetLeft() - ballRadius)
        {
            tempDirection = Vector2.Reflect(moveDirection, Vector2.right);
            moveDirection = tempDirection;
        }
        else if (transform.position.y > CameraMain.instance.GetTop() - ballRadius)
        {
            tempDirection = Vector2.Reflect(moveDirection, Vector2.down);
            moveDirection = tempDirection;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Paddle"))
        {
            //Debug.Log("hit");
            tempDirection = Vector2.Reflect(moveDirection, Vector2.up);
            moveDirection = tempDirection;
        }
        else if(collision.collider.CompareTag("Brick")){
            BallHitBrick();
            //Debug.Log("hit brick");
        }
    }
    public void BallHitBrick()
    {
        tempDirection = Vector2.Reflect(moveDirection, Vector2.down);
        moveDirection = tempDirection;
    }
    public void BallDeath()
    {
        if ((transform.position.y < CameraMain.instance.GetBottom() - ballRadius) && currentLive >0)
        {
            DecreaseLive();
            InGameController.Instance.isBallDeath = true;
            InGameController.Instance.isGameOver = false;
            transform.position = paddle.spawnPosition + Vector3.up;
            GotoState(DeathState);
        }      
    }
    public void DecreaseLive()
    {
        currentLive--; 
    }
    public void IncreasLive()
    {
        currentLive++;
    }
    public void SetMaxLive()
    {
        currentLive = maxLives;
    }
    public void SetBallDeath()
    {
        currentLive = 0;
    }
    public void ResetBall()
    {
        tempX = 0;
        moveDirection = Vector3.zero;
        SetMaxLive();
        GotoState(SpawnState);
    }
    public void CheckBallLive()
    {
        if(currentLive <= 0)
        {
            InGameController.Instance.isGameOver = true;
            InGameController.Instance.GameOver();
        }
        else{
            InGameController.Instance.isBallDeath = true;
            InGameController.Instance.isGameOver = false;
            Debug.Log(InGameController.Instance.isGameOver);
        }
    }
   
}
