using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class Ball_DeathState : FSMState<BallSystemVer2>
{ 

    public override void OnEnter()
    {
        Debug.Log(InGameController.Instance.CheckBallList());
        
        if (InGameController.Instance.CheckBallList() && InGameController.Instance.lives <= 0)
        {
            InGameController.Instance.isGameOver = true ;
        }
        else if (InGameController.Instance.CheckBallList() && InGameController.Instance.lives > 0)
        {
            BallPoolManager.instance.pool.SpawnNonGravity();
            InGameController.Instance.AddBallActive();
        }
       
        sys.GotoState(sys.SpawnState);
        
        //sys.CheckBallLive();        
    }
    public override void OnUpdate()
    {
        
    }
    
}
