using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    public static InGameController Instance;
    public bool isBallDeath;
    public bool isGameOver;
    public bool isLevelComplete;

    public ConfigFileManager configFile;
    public GameObject ballPref;
    public GameObject paddlePref;
    public CameraMain cam;

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

    }

    void Update()
    {

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
        LoadPaddle();
        LoadBall();
        SetUpCamera();


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
    public bool CheckBrickClear()
    {
        if (BrickPoolManager.instance.destroyCount == LoadLevel.instance.totalBrickInLevel)
        {
            isLevelComplete = true;
            return isLevelComplete;
        }
            return isLevelComplete= false;

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

    public void LevelComplete()
    {
       if(isLevelComplete)
        {
            PauseGame();
            DialogManager.Instance.ShowDialog(DialogIndex.WinDialog);
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
        Destroy(prefabBallInstance);
        Destroy(prefabPaddleInstance);
        gameObject.GetComponent
        BrickPoolManager.instance.pool.DeSpawnAll();
    }
}
