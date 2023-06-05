using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class son : MonoBehaviour
{
    public Dad dad;
    // Start ed before the first frame update
    void Start()
    {
       transform.SetParent(dad.transform);
    }

}
