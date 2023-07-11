using NaughtyAttributes.Test;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEditor.PackageManager;
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

    public Paddle paddle;
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
        bool check = CheckGameObjectNull();
        if(check)
        {
            Debug.Log(" load");
            LoadPaddle();
            LoadBall();
            SetUpCamera();
        }
        else
        {
            Debug.Log("reload ");
            ReloadGameObject();
        }
       
    }
    public bool CheckGameObjectNull()
    {
        if (prefabPaddleInstance == null || prefabBallInstance == null)  return true; 
        else return false;
    }
    public void SetUpCamera()
    {
        GameObject camObject = GameObject.FindGameObjectWithTag("testcam");
        if (camObject == null)
        {
            CameraMain.instance.GetCamera();
            CameraMain.instance.GetCameraAspect();
        }
        else
        {
            CameraMain.instance.main = camObject.GetComponent<Camera>();
            CameraMain.instance.GetCameraAspect();
        }
    }
  
    public bool CheckCompleteScore()
    {
        if(GameManager.Instance.currentScore ==  LoadLevel.instance.completeScore)
        {
            isLevelComplete = true;
            return true;

        }
        return false;
    }
    public bool CheckBrickClear()
    {
        if (true)
        {
            isLevelComplete = true;
            return isLevelComplete;
        }

    }
    public void LoadPaddle()
    {
       
        prefabPaddleInstance = Instantiate(paddlePref,transform.parent);
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
    public void DeSpawnBall()
    {
        prefabBallInstance.GetComponent<BallSystem>().ResetBall();
    }
    public void DeSpawnPaddle()
    {
        prefabPaddleInstance.GetComponent <Paddle>().ResetPaddle();
    }
    public void LevelComplete()
    {
        CheckCompleteScore();
       if (isLevelComplete)
       {
                GameManager.Instance.currentLevel++;
                PauseGame();
                DeSpawnAll();
                DialogManager.Instance.ShowDialog(DialogIndex.WinDialog);
        }
    }
 
    public void PauseGame()
    {
        //Debug.Log("======>PAUSE");
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
    public void ReloadGameObject()
    {
        prefabBallInstance.SetActive(true);
        prefabBallInstance.GetComponent<BallSystem>().ResetBall();

        prefabPaddleInstance.SetActive(true);
        prefabPaddleInstance.GetComponent<Paddle>().ResetPaddle();

    }
}
