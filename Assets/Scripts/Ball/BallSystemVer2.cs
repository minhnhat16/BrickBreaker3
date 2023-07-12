using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallSystemVer2 : FSMSystem
{

    [HideInInspector]
    public Ball_MovermentState MoveState;
    [HideInInspector]
    public Ball_SpawnState SpawnState;
    [HideInInspector]
    public Ball_DeathState DeathState;
    [SerializeField]
    private Transform Forward;
    [SerializeField]
    private Transform Anchor;
    [SerializeField]
    public ContactHandle Contact;
    [SerializeField]
    public Paddle paddle;
    [SerializeField]
    public Vector3 moveDir;
    public Vector3 tempDirection;
    public Vector2 forwardDir { get => (Forward.position - Anchor.position).normalized; }
    public Vector3 direction1 = new Vector3(0, 4, 0);

    public bool isLeft = false;
    public bool isRight = false;
    public bool isTop = false;

    public float ballRadius = 1.5f;
    public float tempDirX;
    public float tempX = 0;
    public float tempY = 0;
    public float temp;
    public float ballSpeed;
    public float maxAngle;
    public float tempCamPosition;
    public float angle;
    public float angleMoveSpeed = 0.2f;
    public float ballMess;
    public float ballForce;
    public float bounceFact = 0.2f;
    public int maxLives;
    public int currentLive;

    private void Awake()
    {
        SpawnState.Setup(this);
        MoveState.Setup(this);
        DeathState.Setup(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        GotoState(SpawnState);
    }
    public void AngleMoverment()
    {
        Debug.DrawRay(transform.position, direction1, Color.blue);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && (transform.position.x > -1.0f))
        {
            tempDirX = 1;
            AngleCalculation(tempDirX);
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (transform.position.x < 1.0f))
        {
            tempDirX = -1;
            AngleCalculation(tempDirX);
        }
    }
    private void AngleCalculation(float tempXDir)
    {
        tempX += angleMoveSpeed * tempXDir;
        tempX = Mathf.Clamp(tempX, CameraMain.instance.GetLeft() - 2, CameraMain.instance.GetRight() + 2);
        temp = (-CameraMain.instance.GetLeft() + CameraMain.instance.GetRight());
        tempY = Mathf.Sqrt((temp * temp) - (tempX * tempX)) - 5;
        direction1 = new Vector3(tempX, tempY, transform.position.z);
        Debug.DrawRay(transform.position, direction1, Color.blue);
        angle = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = Vector3.forward * angle;
    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Paddle"))
        {
            Debug.Log("hit paddle");
            CheckBallAngle(moveDir);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Anchor.position, (Vector2)Anchor.position + (Vector2)moveDir);
    }
    public void BallMoverment()
    {
        transform.position += ballSpeed * Time.deltaTime * moveDir;

    }
    public void GetBallDirection()
    {
        
        float right = CameraMain.instance.GetRight() ;
        float left = CameraMain.instance.GetLeft();
        if (transform.position.x >= right )
        {
            Debug.Log("Right camera" + right);
            tempDirection = Vector2.Reflect(moveDir, Vector2.left);
            moveDir = tempDirection + new Vector3(0.1f, 0, 0);

        }
        else if (transform.position.x <= left )
        {
            Debug.Log("Right camera" + left);

            tempDirection = Vector2.Reflect(moveDir, Vector2.right);
            moveDir = tempDirection + new Vector3(0.1f, 0, 0);

        }
        else if (transform.position.y >= CameraMain.instance.GetTop() + ballRadius)
        {
            tempDirection = Vector2.Reflect(moveDir, Vector2.down);
            moveDir = tempDirection + new Vector3(0.2f, 0);
        }
    }
    public void CheckBallAngle(Vector3 vector)
    {
        if(vector.y == 0)
        {
            tempDirection = Vector2.Reflect(moveDir, new Vector2(1,1));
            moveDir = tempDirection;
        }
    }
   
    //private Vector2 CalculateBounceDir( Vector2 incomingDir, Vector2 OnCollisionNormal)
    //{
    //    Vector2 reflectDir  = Vector2.Reflect(incomingDir,OnCollisionNormal);
    //    Vector2 newDir = Vector2.Lerp(reflectDir, -OnCollisionNormal, bounceFact);
    //    return newDir.normalized;
    //}
    public void ResetBall()
    {
        tempX = 0;
        moveDir = Vector3.zero;
        //SetMaxLive();
        GotoState(SpawnState) ;
    }
}
