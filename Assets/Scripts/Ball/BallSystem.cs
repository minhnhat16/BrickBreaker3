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
    public float ballSpeed = 10f;
    public float maxAngle = 45f;
    [SerializeField] private Transform Forward;
    [SerializeField] private Transform Anchor;
    public Vector2 forwardDirection { get => (Forward.position - Anchor.position).normalized; }
    public Vector3 moveDirection;
    public float ballRadius;
    public Vector2 tempDirection;
    public bool isLeft = false;
    public bool isRight = false;
    public bool isTop = false;
    public float tempDirectionX;
    public Vector3 direction1 = new Vector3(0, 4, 0);
    public float tempX = 0;
    public float tempY = 0;
    float angleMoveSpeed = 0.25f;
    float angle;
    public float temp;

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
        Debug.Log("set up ball");
        if (cameraMain == null)
        {
            cameraMain = GetComponent<CameraMain>();
            cameraMain.GetCamera();
        }
    }

    private void Init()
    {

        GotoState(SpawnState);
    }
    public void MoveBall()
    {
        transform.position += ballSpeed * Time.deltaTime * moveDirection;
    }

    public void AngleMoverment()
    {
        //Vector3 direction = _object.position - sys.transform.position;
        //Vector3 direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10) - transform.position);
        //Debug.DrawRay(transform.position, direction, Color.red);
        //Debug.Log("angle" + angle);
        // Quaternion angleAxis = Quatrnion.AngleAxis(angle, Vector3.forward);
        //sys.transform.rotation = Quaternion.Slerp(sys.transform.rotation, angleAxis, Time.deltaTime * 50)
        
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
        if (transform.position.y < cameraMain.GetBottom() - ballRadius)
        {
            InGameController.Instance.isGameOver = true;
            GotoState(DeathState);
        }
    }
}
