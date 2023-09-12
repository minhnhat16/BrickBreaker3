using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LoadSceneManager LoadSceneManager;
    public InGameController InGameController;
    public ViewManager ViewManager;
    public DialogManager DialogManager;
    public LevelManager LevelManager;
   
    private void Awake()
    {
        Instance = this;

    }
    public void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void LoadOnInGameController()
    {
        //Debug.Log("LOAD ON INGAME CONTROLLER");
        InGameController.LoadGameObject();
        //currentLevel = DataAPIController.instance.GetHighestLevel();
    }
    //public void LoadOnData()
    //{
    //    ingamecurrentLevel = DataAPIController.instance.GetHighestLevel();
    //}
}
