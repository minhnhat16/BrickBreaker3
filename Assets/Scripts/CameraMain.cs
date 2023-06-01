using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    private Camera main;
    public float height;
    public float width;

    private const float baseAspect = 9f / 16f;

    private void Start()
    {
        main = Camera.main;
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
