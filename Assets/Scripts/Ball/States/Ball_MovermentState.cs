using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Ball_MovermentState : FSMState<BallSystemVer2>
{
 
    public override void OnEnter()
    {
      
        sys.moveDir = sys.forwardDir;
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnUpdate()
    {
        sys.GetBallDirection();
        //sys.CheckBallAngle();
        sys.BallMoverment();
        //sys.BallDeath();
        InGameController.Instance.LevelComplete();
        InGameController.Instance.GameOver();
        
    }
}
