using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    public static InGameController Instance;
    public bool isGameOver;
    public GameObject brickPref;
    public GameObject ballPref;
    public GameObject paddlePref;
    public CameraMain cam;
    
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
        //loading prefab
        Debug.Log("Loaded Prefab");
        GameObject prefabBrickInstance = Instantiate(brickPref, transform.parent);
        prefabBrickInstance.SetActive(true);
        GameObject prefabPaddleInstance = Instantiate(paddlePref, transform.parent);
        prefabPaddleInstance.SetActive(true);
        GameObject prefabBallInstance = Instantiate(ballPref,transform.parent);
        prefabBallInstance.SetActive(true);

        //set up camera with ball and paddle
        prefabPaddleInstance.GetComponent<Paddle>().SetUpCamera();
        prefabBallInstance.GetComponent<BallSystem>().SetUpCamera();

        //set ball as paddle parent
        prefabBallInstance.transform.SetParent(prefabPaddleInstance.transform);
        prefabBallInstance.transform.position = Vector3.up;

    }

}
