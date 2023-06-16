using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    private Level level;
    private GameManager gameManager;
    public Paddle paddle;
    public BallSystem ball;
    [SerializeField] private Text levelNum;
    public static LoadLevel instance;
    public int currentLevelLoad;

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
        colCount = level.
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
