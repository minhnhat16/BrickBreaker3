using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    public static InGameController Instance;
    public bool isBallDeath;
    public bool isGameOver;
    public bool isLevelComplete;

    public ConfigFileManager configFile;
   //public GameObject brickPref;
    public GameObject ballPref;
    public GameObject paddlePref;
    public CameraMain cam;
    //public GameObject prefabBrickInstance;
    public static GameObject prefabPaddleInstance;
    public static GameObject prefabBallInstance;
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
        CheckBrickCondition();
    }
    public void GameOver()  
    {

        if (isGameOver)
        {
            DialogManager.Instance.ShowDialog(DialogIndex.LoseDialog);
        }
    }

    public void LoadGameObject()
    {
        //loading prefab
        
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
        for (int i = 0; i < BrickPoolManager.instance.spawnAmount; i++)
        {
            BrickPoolManager.instance.pool.SpawnNonGravity();
            BrickPoolManager.instance.pool.list[i].transform.position = configFile.brickScript.brickSpawnPosArray[i];
        }
       
    }
    public void CheckBrickCondition()
    {
        if (BrickPoolManager.instance.destroyCount == BrickPoolManager.instance.spawnAmount)
        {
            isLevelComplete = true;
        }

    }
    public void LoadPaddle()
    {
        prefabPaddleInstance = Instantiate(paddlePref, transform.parent);
        prefabPaddleInstance.SetActive(true);
    }

    public void LoadBall()
    {
        GameObject gameObject1 = Instantiate(ballPref, transform.parent);
        prefabBallInstance = gameObject1;
        prefabBallInstance.SetActive(true);
        prefabBallInstance.GetComponent<BallSystem>().paddle =
        prefabPaddleInstance.GetComponent<Paddle>();
        //Debug.Log(prefabPaddleInstance.GetComponent<Paddle>());
    }

    public void LevelComplete(GameObject gameObject)
    {
        if (isLevelComplete || isGameOver)
        {
            gameObject.SetActive(false);
        }
    }
    public void PauseGame()
    {
        Debug.Log("======>PAUSE");
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Debug.Log("======>RESUME");

        Time.timeScale = 1f;
    }

    public void DeSpawnAll()
    {
        prefabBallInstance.SetActive(false);
        prefabPaddleInstance.SetActive(false);
        BrickPoolManager.instance.pool.DeSpawnAll();

    }
}
