using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public Vector3 moveDirection;

    public bool isLeft = false;
    public bool isRight = false;
    public bool isTop = false;

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
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow)) && (transform.position.x > -1.0f))
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
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AngleCalculation(float tempXDir)
    {
        tempX += angleMoveSpeed * tempXDir;
        tempX = Mathf.Clamp(tempX, CameraMain.instance.GetLeft() -2, CameraMain.instance.GetRight() + 2);
        temp = (-CameraMain.instance.GetLeft() + CameraMain.instance.GetRight());
        tempY = Mathf.Sqrt((temp * temp) - (temp * temp)) - 5;
        direction1 = new Vector3(tempX,tempY,transform.position.z);
        Debug.DrawRay(transform.position, direction1, Color.blue);
        angle = Mathf.Atan2(direction1.y, direction1.x) *Mathf.Rad2Deg - 90;
        transform.eulerAngles = Vector3.forward *angle;
    }
    public void GetBallDir()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Brick"))
        {
            BallHitBrick(); 
        }
    }
    public void BallHitBrick()
    {
        tempDirection = Vector2.Reflect(moveDir, Vector2.down);
        moveDir = tempDirection;
    }
}
