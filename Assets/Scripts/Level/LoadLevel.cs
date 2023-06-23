using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class LoadLevel : MonoBehaviour
{
    [SerializeField]private Level level;
    [SerializeField] private GameManager gameManager;
    public Paddle paddle;
    public BallSystem ball;
    [SerializeField] private Text levelNum;
    public static LoadLevel instance;
    public int currentLevelLoad;
    public Vector2 rootPosition = new Vector2(-2.5f,2.5f);
    public float brickScale;
    public static float BRICK_WIDTH_IMAGE = 0.79f;
    public static float BRICK_HEIGHT_IMAGE = 0.32f;
    private void Awake()
    {
        instance = this;
    }
    
    public void ExitLevel()
    {
        ResetData();
    }
    public void RestartLevel()
    {
        LevelSelect(currentLevelLoad.ToString());
    }
    public void LevelSelect(string level)
    {
        //Debug.Log($"level ===> {level}");
        //ResetData();
        string levelPath = "Levels/level_" + level;
        // Debug.Log($"before level path");

        //Debug.Log($"level path ===> {levelPath}");
        Debug.Log($"Load level data {levelPath}");

        LoadLevelData(levelPath);
        //Debug.Log($"levelnum.text = {levelNum.text = "level" + level}");
    }
    public void LoadLevelData(string levelPath)
    {
        //Debug.Log("===> Load level data");
        int colCount;
        level = Resources.Load<Level>(levelPath);


        colCount = level.collumnCount;
        Debug.Log(colCount);
        brickScale = 14f / colCount + 0.01f;
        //Debug.Log($"Brick Scale {brickScale}");

        string[] arrColor = level.bricks.Split(';');
       


        rootPosition.x = CameraMain.instance.GetLeft() + BRICK_WIDTH_IMAGE * brickScale * 0.5f;
        rootPosition.y = CameraMain.instance.GetTop() - BRICK_HEIGHT_IMAGE * brickScale * 0.6f - 1;

        List<List<int>> matrix = new List<List<int>>();
        List<int> rows = new List<int>();
        int rowCount = 0;
   

        for ( int i = 0; i < arrColor.Length; i++ )
        {
            int color; 
            bool checkColor = int.TryParse(arrColor[i], out color);

            if(!checkColor)
            {
                continue;                
            }
            rows.Add(color);
            rowCount++;
            if(rowCount >= colCount)
            {
                matrix.Add(rows);
                rows = new List<int>();
                rowCount = 0;
            }
        }
        //Debug.Log($"Matrix row {matrix.Count}");
        if(level.isBossLevel)
        {
            //LoadBossData()
            gameManager.winScore = level.winScore;
            gameManager.isBossLevel = level.isBossLevel;
        }
        SetUp(matrix);
        gameManager.winScore = level.winScore;

        Time.timeScale = 1;
    }
    //public void LoadBossData()
    //{
    //    bossPrefabs.SetActive(true);
    //    string bossPath = "Bosses/boss_" + level.bossID;
    //    bossConfig = Resources.Load<BossConfig>(bossPath);
    //    Bos
    //}
    private void SetUp(List<List<int>> matrix)
    {
        Debug.Log($"SETUP BRICK ===>>");

        for (int i = 0; i < matrix.Count; i++)
        {
            Debug.Log($"SETUP {i} BRICK ===>> count");

            List<int> rows = matrix[i];
          

            for (int j = 0; j < rows.Count; j++)
            {

                Vector3 position = rootPosition;
                position.x += j * BRICK_WIDTH_IMAGE * brickScale;
                position.y -= i * BRICK_HEIGHT_IMAGE * brickScale;
                Brick brick = BrickPoolManager.instance.pool.SpawnNonGravity();
                brick.transform.position = position;
                brick.transform.localScale = new Vector2(brickScale, brickScale);
                brick.SettingBrick(rows[j]);
            }

        }
    }
    public void ResetData()
    {
        BrickPoolManager.instance.pool.DeSpawnAll();
        //RESET BALL'
        //RESET PADDLE
        paddle.ResetPaddle();
        //RESET SCORE
    }
}
