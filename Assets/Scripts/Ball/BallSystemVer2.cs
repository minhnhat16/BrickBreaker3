using NaughtyAttributes.Test;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor;
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

    public float ballRadius = 0.5f;
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
     private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    public void ObjecstHitOnRayCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(Anchor.position,  (Vector2)moveDir, ballRadius);
        Debug.DrawRay(Anchor.position, (Vector2)Anchor.position - (Vector2)moveDir, Color.red);
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            tempDirection = Vector2.Reflect(moveDir, Vector2.up);
            moveDir = tempDirection;
            Debug.Log("HIT OBJECT ====>" + hitObject);  
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Anchor.position, (Vector2)Anchor.position + (Vector2)moveDir);
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
    public void BallMoverment()
    {
        transform.position += ballSpeed * Time.deltaTime * moveDir;
    }
    public void GetBallDirection()
    {
        float right = CameraMain.instance.GetRight() ;
        float left = CameraMain.instance.GetLeft();
        float top = CameraMain.instance.GetTop();
        float tempPosX = transform.position.x;
        float tempPosY = transform.position.y;

        CheckBallAngle(angle);
        if(tempPosY == top)
        {
            Debug.Log("Hit top");
        }
        else if (tempPosX  <= right && tempPosX >= right - 1)
        {

            Debug.Log("Hit right");

        }
        else if (tempPosX  >= left && tempPosX <= left +1)
        {
            Debug.Log("Hit left");

        }
    
    }
    public void CheckBallAngle(float _angle)
    {

        if (angle < 30) 
        {
            
            Debug.Log("Check Ball Angle smaller than 30");

        }
        else if (angle < 120)
        {
            Debug.Log("Check Ball Angle lagger than 120");

        }

    }
   public void VectorCalculator(Vector2 vector, float angleInRad)
    {
        tempDirection.x = vector.x * Mathf.Cos(angleInRad) - vector.y * Mathf.Sin(angleInRad);
        tempDirection.y = vector.x * Mathf.Sin(angleInRad) + vector.y * Mathf.Cos(angleInRad);
    }

    public void ResetBall()
    {
        tempX = 0;
        tempDirection = Vector3.zero;
        moveDir = Vector3.zero;
        //SetMaxLive();
        GotoState(SpawnState) ;
    }
}
