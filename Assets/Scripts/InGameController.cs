using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    public static InGameController Instance;
    public bool isBallDeath;
    public bool isGameOver;
    public GameObject brickPref;
    public GameObject ballPref;
    public GameObject paddlePref;
    public CameraMain cam;
    public GameObject prefabBrickInstance;
    public GameObject prefabPaddleInstance;
    public GameObject prefabBallInstance;
    public Vector3 position;


    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

    }
    void Start()
    {
        // LiveOnStart();
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }
    public void GameOver()
    {
        if (isGameOver)
        {
            LoadSceneManager.Instance._GameOverUI.SetActive(true);
        }
    }

    public void LoadGameObject()
    {
        //loading prefab
        Debug.Log("Loaded Prefab");
        LoadBrick();
        LoadPaddle();
        LoadBall();
        SetUpCamera();
        //SetBallParent();
    }
    public void SetUpCamera()
    {
        //set up camera with ball and paddle
        prefabPaddleInstance.GetComponent<Paddle>().SetUpCamera();
        prefabBallInstance.GetComponent<BallSystem>().SetUpCamera();
    }
    public void SetBallParent()
    {
        //set ball as paddle parent
        prefabBallInstance.transform.SetParent(prefabPaddleInstance.transform);
        position = prefabPaddleInstance.transform.position + Vector3.up;
        prefabBallInstance.transform.position = position;

    }
    public void LoadBrick()
    {

        prefabBrickInstance = Instantiate(brickPref, transform.parent);
        prefabBrickInstance.SetActive(true);
    }
    public void LoadPaddle()
    {
        prefabPaddleInstance = Instantiate(paddlePref, transform.parent);
        prefabPaddleInstance.SetActive(true);
    }
    public void LoadBall()
    {
        prefabBallInstance = Instantiate(ballPref, transform.parent);
        prefabBallInstance.SetActive(true);
    }
}
