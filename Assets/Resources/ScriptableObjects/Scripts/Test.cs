using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private TestScripableObject testScripableObject;

    private void Start()
    {
        //Debug.Log(testScripableObject.myString);
    }
}
