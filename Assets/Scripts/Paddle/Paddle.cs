using System.Collections;
using System.Collections.Generic;
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
    public Vector3 spawnPosition = new Vector3(0, -8, 0);
    internal static Transform paddle;

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
}
