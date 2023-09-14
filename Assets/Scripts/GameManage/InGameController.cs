using JetBrains.Annotations;
using NaughtyAttributes.Test;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    public static InGameController Instance;
    public bool isBallDeath;
    public bool isGameOver;
    public bool isLevelComplete;


    public List<BallSystemVer2> pool;
    public List<BallSystemVer2> ballActiveList;


    public GameObject particletest;
    public ConfigFileManager configFile;
    public GameObject ballPref;
    public GameObject paddlePref;
    public GameObject boss;
    public CameraMain cam;
    public GameObject item;
    public GameObject particles;
    public GameObject slider;
    public SpriteRenderer backGround;
    public BallSystemVer2 main;
    public static Paddle prefabPaddleInstance;
    public static GameObject prefabBallInstance;
    public Vector3 position;

    [HideInInspector] public float scaleUpDuration = 0;
    [HideInInspector] public float magnetDuration = 0;
    [HideInInspector] public float powerDuration = 0;
    [HideInInspector] public float deltatime = 0;

    public int winScore;
    public int starCount;
    public int currentScore;
    public int currentLevel;
    public int lives = 1;

    public bool isBossLevel;
    public bool isShortBar = false;
    public bool isLongBar = false;
    public bool isSpeedDown = false;
    public bool isSpeedUp = false;
    public bool isTrippleBall = false;
    public bool isScaleUp = false;
    public bool isItemTypePower = false;
    public bool isOnMagnet = false;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        lives = 1;
        pool = BallPoolManager.instance.pool.list;
        ballActiveList = new List<BallSystemVer2>(50);
    }

    void Update()
    {
        DecreaItemDurationMagnet();
        DecreaItemDurationPower();
        DecreaItemDurationScaleUp();
        InGameController.Instance.CheckCompleteScore();
        InGameController.Instance.UpdateTimer(InGameController.Instance.CalStar, 30f);
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
        //Debug.Log("LOAD GAME OBJECT");
        bool check = CheckGameObjectNull();
        if (check)
        {
            //Debug.Log("load");
            LoadPaddle();
            LoadBall(1);
            LoadBackGround();
            SetUpCamera();
        }
        else
        {
            //Debug.Log("reload ");
            ResetEvent();
            ReloadGameObject();
        }
    }
    
    public bool CheckGameObjectNull()
    {
        if (prefabPaddleInstance == null || prefabBallInstance == null || particletest == null || slider == null) return true;
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

    public void CheckCompleteScore( )
    {
        if (InGameController.Instance.currentScore == LoadLevel.instance.completeScore && LoadLevel.instance.completeScore != 0)
        {
            isLevelComplete = true;
            LevelComplete();
        }
    }
    public bool CheckBrickClear()
    {
        if (true)
        {
            isLevelComplete = true;
            return isLevelComplete;
        }

    }
    public void LoadBoss()
    {
        string bossPath = LoadLevel.instance.Level.bossID.ToString();
        LoadLevel.instance.bossPrefab = Resources.Load<GameObject>("Prefab/BossPrefab/Boss" + bossPath);
        GameObject bossPrefab = LoadLevel.instance.bossPrefab;
        boss = Instantiate(bossPrefab, transform.parent);
        ResetBossData();
        Debug.Log("Load boss  pos: " + bossPrefab.GetComponent<BossSystem>().spawnPosition);
    }
    public void ResetBossData()
    {
        boss.GetComponent<BossSystem>().spawnPosition = LoadLevel.instance.Level.bossPos;
        boss.GetComponent<BossSystem>().ResetBoss();
    }
    public void LoadBackGround()
    {
        Debug.Log("LOAD BACKGROUND");
        particletest = Instantiate(particletest, transform.parent);
        slider = Instantiate(slider, transform.parent);
        backGround = Instantiate(backGround, transform.parent);
        backGround.gameObject.SetActive(true);
        particletest.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
    }
    public void LoadPaddle()
    {
        GameObject gameObject = Instantiate(paddlePref, transform.parent);
        //Debug.Log("game object paddle" + gameObject);
        prefabPaddleInstance = gameObject.GetComponent<Paddle>();
        prefabPaddleInstance.gameObject.SetActive(true);
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
            if (pool[i].gameObject.activeSelf == true)
            {
                main = pool[i];
                break;
            }
        }
    }
    public void LoadBallInTrippleList()
    {
        AddBallActive();
        int ballActiveListCount = ballActiveList.Count;
        Debug.Log("LoadBallInTripplelist " + ballActiveList.Count);
        Debug.Log("LoadBallInTripplelist 2 " + GameManager.Instance.InGameController.ballActiveList.Count);
        if (ballActiveListCount <3 /*&& ballActiveListCount < 3*/)
        {
            BallPoolManager.instance.pool.SpawnNonGravityWithIndex(1);    
            Debug.LogError($"Just have 1 ball");
            for (int i = 0; i < ballActiveListCount; i++)
            {
                Debug.Log("MULTIPLY BALL " + i);
                Debug.Log($"ballActiveList[{i}] {ballActiveList[i]}");
                ballActiveList[i].BallMultiply(ballActiveList[i]);
            }

        }
        else if (ballActiveListCount >= 3)
        {
            for (int i = 1; i < ballActiveList.Count; i++)
            {
                //Debug.Log("MULTIPLY BALL " + i);
                //Debug.Log($"ballActiveList[{i}] {ballActiveList[i]}");
                ballActiveList[i].BallMultiply(ballActiveList[i]);
            }
        }

    }
    public bool CheckBallList()
    {
        //Debug.Log("CHECKBALLLIST");
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].gameObject.activeSelf == true)
            {
                main = pool[i];
                return false;
            }
        }
        Debug.Log("BALL ACTIVE LIST NULL");
        return true;
    }
    public void DeSpawnBall()
    {
        //Debug.Log("BallPoolManager" + BallPoolManager.instance.pool);
        BallPoolManager.instance.pool.DeSpawnAll();
        //Debug.Log("prefab ball" + prefabBallInstance);
        //Debug.Log("prefab ball get component" + prefabBallInstance.GetComponent<BallSystemVer2>());
        prefabBallInstance.GetComponent<BallSystemVer2>().ResetBall();
    }
    public void DeSpawnPaddle()
    {
        prefabPaddleInstance.GetComponent<Paddle>().ResetPaddle();
        prefabPaddleInstance.gameObject.SetActive(false);
    }
    public void LevelComplete()
    {
        PauseGame();
        DeSpawnAll();
        InGameController.Instance.backGround.gameObject.SetActive(true);
        WinDialogParam param = new WinDialogParam();
        param.crLevel = LoadLevel.instance.currentLevelLoad;
        param.score = InGameController.Instance.currentScore;
        param.star = InGameController.Instance.starCount;
        param.nextLv = param.crLevel + 1;

        DialogManager.Instance.ShowDialog(DialogIndex.WinDialog,param);
        return;
    }
    //public void SaveLevelData()
    //{
    //    int currentlv = GameManager.Instance.currentLevel;
    //    int currentSc = GameManager.Instance.winScore;
    //    int totalStar = 3;
    //    DataAPIController.instance.SaveLevel(currentlv, currentSc, totalStar, isLevelComplete);
        
    //}
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
    public void DeSpawnBoss()
    {
       if(boss !=  null)
        {
            //boss.gameObject.SetActive(false);
            //Destroy(boss.GetComponent<BossSystem>().hub.);
            Debug.Log("HUB " + boss.GetComponent<BossSystem>().hub.gameObject);
            boss.GetComponent<BossSystem>().TurnOffHub();
            Destroy(boss);
        }
    }
    public void DeSpawnAll()
    {
        DeSpawnBall();
        DeSpawnPaddle();
        DeSpawnBoss();
        particletest.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        backGround.gameObject.SetActive(false);
        BrickPoolManager.instance.pool.DeSpawnAll();
    }
    public void ReloadGameObject()
    {
        BallPoolManager.instance.pool.DeSpawnAll();
        ballActiveList.Clear();
        magnetDuration = 7f;
        scaleUpDuration = 7f;
        powerDuration = 7f;
        prefabBallInstance.SetActive(true);
        prefabPaddleInstance.gameObject.SetActive(true);
        particletest.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
        backGround.gameObject.SetActive(true);
        prefabBallInstance.GetComponent<BallSystemVer2>().ResetBall();
        prefabPaddleInstance.GetComponent<Paddle>().ResetPaddle();
        ResetBallPosition();
        // BallPoolManager.instance.ResetAllPoolPostion();
        ItemPoolManager.instance.pool.DeSpawnAll();
        ballActiveList.Clear();
    }
    public void ResetBallPosition()
    {
        //Debug.Log("RESET BALL POSITION");
        for (int i = 0; i < BallPoolManager.instance.pool.total; i++)
        {
            BallPoolManager.instance.pool.list[i].ResetBall();
        }
    }
    public float DecreaseItemDuration(float duration)
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        return duration;
    }
    public void DecreaItemDurationMagnet()
    {
        magnetDuration = DecreaseItemDuration(magnetDuration);

    }
    public void DecreaItemDurationPower()
    {
        powerDuration = DecreaseItemDuration(powerDuration);

    }
    public void DecreaItemDurationScaleUp()
    {
        scaleUpDuration = DecreaseItemDuration(scaleUpDuration);

    }
    public void ResetEvent()
    {
        //Debug.Log("Action callback");
        for (int i = 0; i < pool.Count; i++)
        {
            pool[i].ResetBall();
        }

    }
    public void ClearOnMagnet()
    {
        Debug.Log("clearONmagnet");
        InGameController.Instance.isOnMagnet = false;
    }
    public Vector3 MainBallPosition()
    {
        return main.transform.position;
    }
    public void UpdateTimer(Action callback, float timer)
    {
        deltatime += Time.deltaTime;    
        if (deltatime > timer && InGameController.Instance.starCount > 1)
        {
            //Debug.Log("Trigger Timer");
            deltatime = 0;
            callback?.Invoke();
           
        }
    }

    public void CalStar() // Calculating Star
    {
        deltatime += Time.deltaTime;
        InGameController.Instance.starCount -= 1;
        //Debug.Log("Calculating Star" + GameManager.Instance.starCount);
    }
}
