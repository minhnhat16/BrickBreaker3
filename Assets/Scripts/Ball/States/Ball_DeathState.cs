using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ball_DeathState : FSMState<BallSystemVer2>
{ 

    public override void OnEnter()
    {
        if (InGameController.Instance.CheckBallList())
        {
            InGameController.Instance.isGameOver = true ;
        }
        sys.GotoState(sys.SpawnState);
        //sys.CheckBallLive();        
    }
    public override void OnUpdate()
    {
        
    }
    
}
