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
    private float minDuration = 10f, maxDuration = 15f;
    public Vector3 currenPaddlePosition;
    public Vector3 spawnPosition = new Vector3(0, -8, 0);




    private void Awake()
    {
        PaddleEvent.onTripple.AddListener(OnTripple);
        instance = this;
        MoveState.Setup(this);
    }
    private void Start()
    {
        Init();
        StartCoroutine(RandomSpawnItem());

    }

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
        ball.BallOnMagnet();
        ball.BallReflectPaddle();
    }
 
    private void OnTripple()
    {
        //Debug.LogError("On Tripple");
        InGameController.Instance.isTrippleBall = true;
        Debug.Log("ADD BALL ACTIVE IN PADDLE");
        Debug.Log("LOAD BALL TRIPPLE LIST IN PADDLE");
        InGameController.Instance.LoadBallInTrippleList();
    }

    public void CheckItemEvent()
    {
        if (InGameController.Instance.isLongBar)
        {
            //Debug.LogWarning("=============LONGBAR=============");
            longBarDuration -= Time.deltaTime;
            if (longBarDuration <= 0)
            {
                transform.GetChild(0).GetComponent<Transform>().DOScaleX(2.20f, 1.2f);
                transform.GetComponent<BoxCollider2D>().size = new Vector2(2.65f + 0.5f, 0.5f) ;
                InGameController.Instance.isLongBar = false;
                longBarDuration = 5f;
            }
        }

        if (InGameController.Instance.isShortBar)
        {
            //Debug.LogWarning("=============SHORTBAR=============");

            shortBarDuration -= Time.deltaTime;
            if (shortBarDuration <= 0)
            {
                transform.GetChild(0).GetComponent<Transform>().DOScaleX(1.5f, 1f);
                InGameController.Instance.isShortBar = false;
                shortBarDuration = 5f;
            }
        }

        if (InGameController.Instance.isSpeedUp)
        {
            //Debug.LogWarning("=============SPEEDUP=============");

            speedUpBarDuration -= Time.deltaTime;
            if (speedUpBarDuration <= 0)
            {
                paddleSpeed = 10f;
                InGameController.Instance.isSpeedUp = false;
                speedUpBarDuration = 5f;
            }
        }

        if (InGameController.Instance.isSpeedDown)
        {
            //Debug.LogWarning("=============SPEEDDOWN=============");

            speedDownBarDuration -= Time.deltaTime;
            if (speedDownBarDuration <= 0)
            {
                paddleSpeed = 10f;
                InGameController.Instance.isSpeedDown = false;
                speedDownBarDuration = 5f;
            }
        }

        if (InGameController.Instance.isTrippleBall)
        {   
            trippleDuration -= Time.deltaTime;
            if (trippleDuration <= 0)
            {
                trippleDuration = 5f;
                InGameController.Instance.isTrippleBall = false;
            }
        }
    }
    [HideInInspector]
    public IEnumerator RandomSpawnItem()
    {
        while (true)
        {
            float spawnDuration = UnityEngine.Random.Range(minDuration, maxDuration);
            yield return new WaitForSeconds(spawnDuration);
            RandomItem();
        }
    }
    public void RandomItem()
    {
        ItemPoolManager.instance.SpawnItem();
        ItemPoolManager.instance.item.transform.DOMoveY((CameraMain.instance.GetBottom() - 3), (CameraMain.instance.GetTop()));

        float randomValue = UnityEngine.Random.Range(1f, 10f);
        int value = (int)(randomValue * 10);
    }

}
public static class PaddleEvent
{
    public static UnityEvent onTripple = new UnityEvent();
    public static UnityEvent onLongBar = new UnityEvent();
}
