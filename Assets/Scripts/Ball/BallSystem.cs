using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BallSystem : FSMSystem
{
    #region
    [HideInInspector]
    public Ball_MovermentState MoveState;
    [HideInInspector]
    public Ball_SpawnState SpawnState;
    [HideInInspector]
    public Ball_DeathState DeathState;
    public CameraMain cameraMain;
    #endregion

    public ContactHandle contactHandle;
    public Paddle paddle;
    
    [SerializeField] private Transform Forward;
    [SerializeField] private Transform Anchor;

    public Vector2 forwardDirection { get => (Forward.position - Anchor.position).normalized; }
    public Vector3 moveDirection;
    public Vector3 direction1 = new Vector3(0, 4, 0);
    public Vector2 tempDirection;
    public Vector3 moveBall;
    public float ballRadius;
    public bool isLeft = false;
    public bool isRight = false;
    public bool isTop = false;
    public float tempDirectionX;
    public float tempX = 0;
    public float tempY = 0;
    public float ballSpeed = 10f;
    public float maxAngle = 45f;
    public float temp;
    public float angleMoveSpeed = 0.2f;
    public float angle;
    public int maxLives;
    public int currentLive;
    private void Awake()
    {
        SpawnState.Setup(this);
        MoveState.Setup(this);
        DeathState.Setup(this);
    }

    private void Start()
    {
        Init();
    }

    public void SetUpCamera()
    {
        if (cameraMain == null)
        {
            cameraMain = GetComponent<CameraMain>();
            cameraMain.GetCamera();
        }
    }

    private void Init()
    {
        SetMaxLive();
        GotoState(SpawnState);
    }
    public void MoveBall()
    {
      moveBall += ballSpeed * Time.deltaTime * moveDirection;
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
        tempX = Mathf.Clamp(tempX, cameraMain.GetLeft() - 2, cameraMain.GetRight() + 2);
        temp = (-cameraMain.GetLeft() + cameraMain.GetRight());
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
        if (transform.position.x > cameraMain.GetRight() - ballRadius)
        {
            tempDirection = Vector2.Reflect(moveDirection, Vector2.left);
            moveDirection = tempDirection;
        }
        else if (transform.position.x < cameraMain.GetLeft() - ballRadius)
        {
            tempDirection = Vector2.Reflect(moveDirection, Vector2.right);
            moveDirection = tempDirection;
        }
        else if (transform.position.y > cameraMain.GetTop() - ballRadius)
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
        if ((transform.position.y < cameraMain.GetBottom() - ballRadius) && currentLive >0)
        {
            DecreaseLive();
            InGameController.Instance.isBallDeath = true;
            InGameController.Instance.isGameOver = false;
            GotoState(DeathState);
        }
        else if ( currentLive <= 0 ) {
            
            InGameController.Instance.isGameOver = true;
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
}
