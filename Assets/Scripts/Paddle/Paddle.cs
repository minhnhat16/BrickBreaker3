using Unity.VisualScripting;
using UnityEngine;

public class Paddle : FSMSystem,InteractBall
{
    [HideInInspector]
    public PaddleMoveState MoveState;
    [SerializeField] public Collision collision { get; set; }
    public BallSystemVer2 ballSystem;
    [SerializeField] private BoxCollider2D boxCollider2D;
    public float paddleSpeed = 4f;
    public float paddleLenght;
    public float rightLimit;
    public float leftLimit;
    public float PaddleMoveLimit;
    public float tempX;
    public Vector3 currenPaddlePosition;
    public Vector3 spawnPosition = new Vector3(0, -8, 0);

    private void Awake()
    {
        MoveState.Setup(this);
    }
    private void Start()
    {
        //if (cameraMain == null)
        //{
        //    cameraMain = GetComponent<CameraMain>();    
        //}
        Init();
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
        Debug.Log("Ball reflect Paddle");
        ball.BallReflectPaddle();
    }
}
