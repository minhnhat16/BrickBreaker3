using UnityEngine;

public class Paddle : FSMSystem
{
    [HideInInspector]
    public PaddleMoveState MoveState;
    [SerializeField] public Collision collision { get; set; }
    public float paddleSpeed = 4f;
    public BallSystem ballSystem;
    public CameraMain cameraMain;
    public float paddleLenght;
    public float rightLimit;
    public float leftLimit;
    public float PaddleMoveLimit;
    public float tempX;
    public Vector3 currenPaddlePosition;
    public Vector3 spawnPosition = new Vector3(0,-8,0);

    private void Awake()
    {
        MoveState.Setup(this);
    }
    private void Start()
    {
        if (cameraMain == null)
        {
            cameraMain = GetComponent<CameraMain>();
        }
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
        GotoState(MoveState);
    }

    public void ResetPaddle()
    {
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
        rightLimit = cameraMain.GetRight() - paddleLenght / 10f;
        leftLimit = cameraMain.GetLeft() +  paddleLenght / 10f;
        tempX += Time.deltaTime * paddleSpeed * xDirection;
        tempX = Mathf.Clamp(tempX, leftLimit, rightLimit);
        transform.position = new Vector3(tempX, transform.position.y, transform.position.z);
    }
}
