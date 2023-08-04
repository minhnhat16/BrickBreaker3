using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Paddle : FSMSystem,InteractBall
{
    [HideInInspector]
    public PaddleMoveState MoveState;
    [SerializeField] public Collision collision { get; set; }
    public BallSystemVer2 ballSystem;
    [SerializeField] private BoxCollider2D boxCollider2D;
    public List<BallSystemVer2> trippleList = new List<BallSystemVer2>();
    [SerializeField]
    private BallSystemVer2 mainBall;

    public static Paddle instance;
    public float paddleSpeed = 4f;
    public float paddleLenght;
    public float rightLimit;
    public float leftLimit;
    public float PaddleMoveLimit;
    public float tempX;
    public float trippleDuration;
    public float longBarDuration;
    public float shortBarDuration;
    public float speedUpBarDuration;
    public float speedDownBarDuration;
    private float minDuration = 1f, maxDuration = 2f;
    public Vector3 currenPaddlePosition;
    public Vector3 spawnPosition = new Vector3(0, -8, 0);
    public bool isShortBar = false;
    public bool isLongBar = false ;
    public bool isSpeedDown = false ;
    public bool isSpeedUp = false;
    public bool isTrippleBall = false;




    private void Awake()
    {
        PaddleEvent.onTripple.AddListener(OnTripple);
        instance = this;
        MoveState.Setup(this);
    }
    private void Start()
    {
        //if (cameraMain == null)
        //{
        //    cameraMain = GetComponent<CameraMain>();    
        //}
        Init();
        StartCoroutine(RandomSpawnItem());

    }
    //public void SetUpCamera()
    //{
    //    if (cameraMain == null)
    //    {
    //        cameraMain = GetComponent<CameraMain>();
    //        cameraMain.GetCamera();
    //    }
    //}
    private void Init()
    {
        GotoState(MoveState);
        mainBall = InGameController.Instance.main;
        transform.localScale = new Vector3(1.5f, 1f, 1f);
    }

    public void ResetPaddle()
    {
        tempX = 0;
        GotoState(MoveState);
    }
    public Vector3 GetCurrentPosition()
    {
        currenPaddlePosition = transform.position;
        // Debug.Log(currenPaddlePosition + " dm ");
        return currenPaddlePosition;
    }
    public void MoveCalculation(float xDirection)
    {
        rightLimit = CameraMain.instance.GetRight() - paddleLenght / 10f;
        leftLimit = CameraMain.instance.GetLeft() + paddleLenght / 10f;
        tempX += Time.deltaTime * paddleSpeed * xDirection;
        tempX = Mathf.Clamp(tempX, leftLimit + ballSystem.ballRadius, rightLimit - ballSystem.ballRadius);
        transform.position = new Vector3(tempX, transform.position.y, transform.position.z);
    }

    public void OnContact(RaycastHit2D hit, BallSystemVer2 ball)
    {
        //Debug.Log("Ball reflect Paddle");
        ball.BallReflectPaddle();
    }
 
    private void OnTripple()
    {
        Debug.LogError("On Tripple");
        isTrippleBall = true;
        InGameController.Instance.LoadBallInTrippleList();

    }

    //private void TrippleBall()
    //{
    //    foreach (BallPoolManager.instance.pool.list in list)
    //    {
    //        ball.ballSpeed = 6f;
    //    }
    //}
    public void CheckItemEvent()
    {
        if (isLongBar)
        {
            longBarDuration -= Time.deltaTime;
            if (longBarDuration <= 0)
            {
                transform.GetChild(0).GetComponent<Transform>().DOScaleX(1.5f, 1f);
                isLongBar = false;
                longBarDuration = 5f;
            }
        }

        if (isShortBar)
        {
            shortBarDuration -= Time.deltaTime;
            if (shortBarDuration <= 0)
            {
                transform.GetChild(0).GetComponent<Transform>().DOScaleX(1.5f, 1f);
                isShortBar = false;
                shortBarDuration = 5f;
            }
        }

        if (isSpeedUp)
        {
            speedUpBarDuration -= Time.deltaTime;
            if (speedUpBarDuration <= 0)
            {
                paddleSpeed = 10f;
                isSpeedUp = false;
                speedUpBarDuration = 5f;
            }
        }

        if (isSpeedDown)
        {
            speedDownBarDuration -= Time.deltaTime;
            if (speedDownBarDuration <= 0)
            {
                paddleSpeed = 10f;
                isSpeedDown = false;
                speedDownBarDuration = 5f;
            }
        }

        if (isTrippleBall)
        {
            //TrippleBall();

            trippleDuration -= Time.deltaTime;
            if (trippleDuration <= 0)
            {
                trippleDuration = 5f;
                isTrippleBall = false;
            }
        }
    }
    public void RemoveClone(BallSystemVer2 ball)
    {
        if (BallPoolManager.instance.pool.list.Contains(ball))
        {
            BallPoolManager.instance.pool.DeSpawnNonGravity(ball);
        }
    }
    [HideInInspector]
    public IEnumerator RandomSpawnItem()
    {
        while (true)
        {
            // if (currentState == MoveState)
            // {
            float spawnDuration = UnityEngine.Random.Range(minDuration, maxDuration);
            yield return new WaitForSeconds(spawnDuration);
            RandomItem();
            //}     
        }
    }
    public void RandomItem()
    {
        //ItemPoolManager.instance.SpawnItem();
        ItemPoolManager.instance.SpawnItem();
        ItemPoolManager.instance.item.transform.DOMoveY((CameraMain.instance.GetBottom() - 3), (CameraMain.instance.GetTop()));

        float randomValue = UnityEngine.Random.Range(1f, 10f);
        int value = (int)(randomValue * 10);
        //Debug.Log("RANDOM OBJECT ...." + value);
    }

}
public static class PaddleEvent
{
    public static UnityEvent onTripple = new UnityEvent();
}
