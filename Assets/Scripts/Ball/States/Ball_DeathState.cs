using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ball_DeathState : FSMState<BallSystemVer2>
{ 

    public override void OnEnter()
    {
        sys.GotoState(sys.SpawnState);
        sys.CheckBallLive();        
    }
    public override void OnUpdate()
    {
        
    }
    
}
