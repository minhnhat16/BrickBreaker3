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
    public int winScore;
    public int currentScore;
    public int highetsLevel = 1;
    public bool isBossLevel;
    private void Awake()
    {
        Instance = this;

    }
    public void Start()
    {
    }

    public void LoadOnInGameController()
    {
        InGameController.LoadGameObject();
    }
}
