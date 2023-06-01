using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    public static InGameController Instance; 
    public bool isGameOver;
    public GameObject BrickPrefab;
    public GameObject BallPrefab;
    public GameObject PaddlePrefab;
    public Transform PaddleParent { get; }  
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void GameOver()
    {
        if (isGameOver)
        {
            // di den scene game over
        }
        else
        {
            //tiep tuc
        }
    }

    public void LoadGameObject()
    {
        Debug.Log("Loaded Prefab");
        GameObject prefabBrickInstance = Instantiate(BrickPrefab, transform.parent);
        prefabBrickInstance.SetActive(true);
        GameObject prefabPaddleInstance = Instantiate(PaddlePrefab, transform.parent);
        prefabPaddleInstance.SetActive(true);
        GameObject prefabBallInstance = Instantiate(BallPrefab,transform.parent);
        prefabBallInstance.SetActive(true);

    }

}
