using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public static CameraMain instance;
    public Camera main;
    public GameObject camPrefab;
    public float height;
    public float width;
    public GameObject _obj;


    private const float baseAspect = 9f / 16f;
    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        
    }
    public void GetCamera()
    {

        _obj = Instantiate(Resources.Load("Prefab/Camera/camPrefab", typeof(GameObject))) as GameObject;
        _obj.transform.SetParent(InGameController.Instance.transform);
        main = _obj.GetComponent<Camera>();
        float targetAspect = main.aspect;
        main.orthographicSize = baseAspect / targetAspect * main.orthographicSize;
        height = main.orthographicSize * 2;
        width = height * main.aspect;
    }
    public float GetLeft()
    {
        return main.transform.position.x - width * 0.5f;
    }
    public float GetRight()
    {
        return main.transform.position.x + width * 0.5f;
    }
    public float GetTop()
    {
        return main.transform.position.y + height* 0.5f;
    }
    public float GetBottom()
    {
        return main.transform.position.y - height * 0.5f;
    }
}
