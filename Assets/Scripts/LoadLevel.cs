using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField ]private Level level;
    [SerializeField] private GameManager gameManager;
    public Paddle paddle;
    public BallSystem ball;
    [SerializeField] private Text levelNum;
    public static LoadLevel instance;
    public int currentLevelLoad;
    public Vector3 rootPosition;
    public float brickScale;
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
        ResetData();
        string levelPath  = "Levels/level_" + level;
        LoadLevelData(levelPath);
        levelNum.text = "Level " + level;   
    }
    public void LoadLevelData(string levelPath)
    {
        int colCount;
        level = Resources.Load<Level>(levelPath);
        colCount = level.collumnCount;
        //brickScale = 7f / colCount + 0.01f;

        string[] arrColor = level.bricks.Split(';');

       // rootPosition.x = CameraMain.instance.GetLeft() + GameConstant.BRICK_WIDTH_IMAGE * brickScale * 0.5f;
       //rootPosition.y = CameraMain.instance.GetTop() - GameConstant.BRICK_HEIGHT_IMAGE * brickScale * 0.6f - GameConstant.TOP_PADDING;
        
        List<List<int>> matrix = new List<List<int>>();
        List<int> rows = new List<int>();
        int rowCount = 0;

        for( int i = 0; i < arrColor.Length; i++ )
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
        for(int i = 0; i < matrix.Count; i++)
        {
            List<int> rows = matrix[i];
            for (int j = 0; j < rows.Count; j++)
            {
                Vector3 position = rootPosition;
                //position.x += j * GameConstant.BRICK_WIDTH_IMAGE * brickScale;
                //position.y - = i* GameConstant.BRICK_HEIGHT_IMAGE * brickScale;
                Brick brick = BrickPoolManager.instance.pool.SpawnNonGravity();
                brick.transform.position = position;
                brick.transform.localScale = new Vector2(brickScale, brickScale);
                brick.SettingBrick(rows[j]);
            }

        }
    }
    public void ResetData()
    {
        //RESET BALL'
        ball.GotoState(ball.SpawnState) ;
        //RESET PADDLE
        paddle.ResetPaddle();
        //RESET SCORE

        BrickPoolManager.instance.pool.DeSpawnAll();


        //RESPAWN BALL

        //RESPAWN PADDLE    
    }
}
