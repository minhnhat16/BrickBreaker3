using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2 : BossSystem
{ 
    public override void Setup()
    {
        Debug.Log("Setup on boss 4 ");

        base.Setup();

    }
    public override void OnSystemUpdate()
    {
        Rotation();
        base.OnSystemUpdate();
    }
}
