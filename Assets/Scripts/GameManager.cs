using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LoadSceneManager LoadSceneManager;
    public InGameController InGameController;

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
