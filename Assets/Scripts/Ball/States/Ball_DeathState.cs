using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ball_DeathState : FSMState<BallSystem>
{ 

    public override void OnEnter()
    {
        sys.GotoState(sys.SpawnState);
    }
    public override void OnUpdate()
    {
       
    }
}
