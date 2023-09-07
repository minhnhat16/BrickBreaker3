using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Boss3 : BossSystem
{
    public float maxvalue = 1.7f;
    public float minvalue = 1.2f;
    public float  duration = 1.0f;
    public float startTime;
    public float t;
    public float curentvalue = 1.2f;

    public override void Setup()
    {
        Debug.Log("Setup on boss 3");
        startTime = Time.time;
        base.Setup();
    }
    public override void OnSystemUpdate()
    {
        //Debug.Log("SYSTEM UPDATE OVERRIDE BOSS 3");

        core.gameObject.transform.Rotate(0,0, 1f);
        mid.gameObject.transform.Rotate(Vector3.zero);
        crust.gameObject.transform.Rotate(Vector3.zero);
        ZoomInSprite();
        base.OnSystemUpdate();
    }
    public void ZoomInSprite()
    {
        //Debug.Log("ZOOM IN SPRITE ");
        if (t >= 1.0f)
        {
            //Debug.Log("IF");
            t = 0;
            startTime = Time.time;
        }
        else if (curentvalue >= 1.7f)
        {
            mid.gameObject.transform.localScale = new Vector3(curentvalue, curentvalue);
            t = ((Time.time - startTime) / duration);
            //Debug.Log("ELSE IF");
            float lerpedValue = Mathf.SmoothStep(maxvalue, minvalue, t);
            //Debug.Log("lerpedValue " + lerpedValue);
            mid.gameObject.transform.localScale = new Vector3(lerpedValue, lerpedValue);
            if (lerpedValue <= 1.2f)
            {
                //Debug.Log("  if (lerpedValue <= 1.2f)");
                curentvalue = lerpedValue;
            }
        }
        else
        {
            t = (Time.time - startTime) / duration;
            //Debug.Log("ELSE ");
            float lerpedValue = Mathf.SmoothStep(minvalue, maxvalue,  t);
            mid.gameObject.transform.localScale = new Vector3(lerpedValue, lerpedValue);
            if (lerpedValue >=1.7f)
            {
                //Debug.Log("if (lerpedValue >=1.7f)");

                curentvalue = lerpedValue;
            }
        }
    }
}
