using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class Ball_DeathState : FSMState<BallSystemVer2>
{ 

    public override void OnEnter()
    {

        if (InGameController.Instance.CheckBallList())
        {
            InGameController.Instance.lives--;
            InGameController.Instance.isGameOver = true ;

        }
        //else
        //{
        //    sys.GotoState(sys.SpawnState);
        //    BallPoolManager.instance.pool.SpawnNonGravity();
        //    InGameController.Instance.AddBallActive();
        //}
            
    }
    public override void OnUpdate()
    {
        
    }
    
}
