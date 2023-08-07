using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    public static InGameController Instance;
    public bool isBallDeath;
    public bool isGameOver;
    public bool isLevelComplete;

    public List<BallSystemVer2> pool;

    public List<BallSystemVer2> ballActiveList;


    public ConfigFileManager configFile;
    public GameObject ballPref;
    public GameObject paddlePref;
    public CameraMain cam;
    public GameObject item;

    public Paddle paddle;
    public BallSystemVer2 main;
    public static GameObject prefabPaddleInstance;
    public static GameObject prefabBallInstance;
    public Vector3 position;
    private int listIndex = 1;


    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

    }
    void Start()
    {
        pool = BallPoolManager.instance.pool.list;
        ballActiveList = new List<BallSystemVer2>(100);
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
        if (check)
        {
            //Debug.Log(" load");
            LoadPaddle();
            LoadBall(1);
            SetUpCamera();
        }
        else
        {
            //Debug.Log("reload ");
            ReloadGameObject();
        }

    }
    public bool CheckGameObjectNull()
    {
        if (prefabPaddleInstance == null || prefabBallInstance == null) return true;
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
        if (GameManager.Instance.currentScore == LoadLevel.instance.completeScore)
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
        prefabPaddleInstance = Instantiate(paddlePref, transform.parent);
        prefabPaddleInstance.SetActive(true);
    }

    public void LoadBall()
    {
        GameObject gameObject1 = Instantiate(ballPref, transform.parent);
        prefabBallInstance = gameObject1;
        prefabBallInstance.SetActive(true);
        prefabBallInstance.GetComponent<BallSystemVer2>().paddle =
        prefabPaddleInstance.GetComponent<Paddle>();
        //Debug.Log(prefabPaddleInstance.GetComponent<Paddle>());
    }
    public void LoadBall(int num)
    {
        main = BallPoolManager.instance.pool.SpawnNonGravity().GetComponentInChildren<BallSystemVer2>();
        GameObject gameObject1 = main.gameObject;
        prefabBallInstance = gameObject1;
        prefabBallInstance.GetComponent<BallSystemVer2>().paddle =
            prefabPaddleInstance.GetComponent<Paddle>();
        SettingPaddleComp();
        ResetBallPosition();
        AddBallActive();
        //ballActiveList.Add(main);
    }
    public void SettingPaddleComp()
    {
        for (int i = 0; i < BallPoolManager.instance.pool.total; i++)
        {
            BallPoolManager.instance.pool.list[i].paddle =
            prefabPaddleInstance.GetComponent<Paddle>();
        }
    }
    public void AddBallActive()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            //Debug.Log("FOREACH " +  +" IN " +);
            //int j = ballActiveList.Count + 1;
            if (pool[i].gameObject.activeSelf == true)
            {
                //Debug.Log("CHECK ACTIVE LIST");
                BallSystemVer2 b = pool[i];
                ballActiveList.Add(b);
                Debug.Log(ballActiveList.Count);
                //Debug.Log("ball activelist " + ballActiveList[j]);
            }
        }
    }
    public void SettingMainBall()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            //Debug.Log("FOREACH " +  +" IN " +);
            //int j = ballActiveList.Count + 1;
            if (pool[i].gameObject.activeSelf == true)
            {
                //Debug.Log("CHECK ACTIVE LIST");
                main = pool[i];
                break;
                //Debug.Log("ball activelist " + ballActiveList[j]);
            }
        }
    }
    public void LoadBallInTrippleList()
    {
        int i = 0;
        AddBallActive();
        Debug.Log("LoadBallInTripplelist " + ballActiveList.Count);
        if (ballActiveList.Count == 1 && ballActiveList.Count <3)
        {   
            i = 1;
            Debug.LogError($"Just have 1 ball");
            BallPoolManager.instance.pool.SpawnNonGravity();

            for (i = 0; i < ballActiveList.Count; i++)
            {
                Debug.Log("MULTIPLY BALL " + i);
                Debug.Log($"ballActiveList[{i}] {ballActiveList[i]}");
                ballActiveList[i].BallMultiply(ballActiveList[i]);
            }

        }
        else if(ballActiveList.Count >= 3)
        {
            for (i = 1; i < ballActiveList.Count; i++)
            {
                Debug.Log("MULTIPLY BALL " + i);
                //Debug.Log($"ballActiveList[{i}] {ballActiveList[i]}");
                ballActiveList[i].BallMultiply(ballActiveList[i]);
            }
        }
       // AddBallActive();

    }
    public bool CheckBallList()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            //Debug.Log("FOREACH " +  +" IN " +);
            //int j = ballActiveList.Count + 1;
            if (pool[i].gameObject.activeSelf == true)
            {
                //Debug.Log("CHECK ACTIVE LIST");
                main = pool[i];
                return false;
                //Debug.Log("ball activelist " + ballActiveList[j]);
            }
        }
        return false;
    }
    public void DeSpawnBall()
    {
        BallPoolManager.instance.pool.DeSpawnAll();
        prefabBallInstance.GetComponent<BallSystemVer2>().ResetBall();
    }
    public void DeSpawnPaddle()
    {
        prefabPaddleInstance.GetComponent<Paddle>().ResetPaddle();
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
        //Debug.Log("======>RESUME");

        Time.timeScale = 1f;
    }

    public void DeSpawnAll()
    {
        DeSpawnBall();
        DeSpawnPaddle();
        BrickPoolManager.instance.pool.DeSpawnAll();
    }
    public void ReloadGameObject()
    {
        BallPoolManager.instance.pool.DeSpawnAll();
        ballActiveList.Clear();
        listIndex = 0;
        prefabBallInstance.SetActive(true);
        prefabBallInstance.GetComponent<BallSystemVer2>().ResetBall();
        prefabPaddleInstance.SetActive(true);
        prefabPaddleInstance.GetComponent<Paddle>().ResetPaddle();
        ResetBallPosition();
        // BallPoolManager.instance.ResetAllPoolPostion();
        ItemPoolManager.instance.pool.DeSpawnAll();
       
    }
    public void ResetBallPosition()
    {
        Debug.Log("RESET BALL POSITION");
        for (int i = 0; i < BallPoolManager.instance.pool.total; i++)
        {
            BallPoolManager.instance.pool.list[i].ResetBall();
        }
    }
    public Vector3 MainBallPosition()
    {
        return main.transform.position;
    }

}
