using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ball_MovermentState : FSMState<BallSystem>
{
    public Vector2 direction;
 
    public override void OnEnter()
    {
      
        sys.moveDirection = sys.forwardDirection;
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnUpdate()
    {
        sys.MoveBall();
        sys.BallDeath();
        sys.GetBallDirection();
        InGameController.Instance.LevelComplete();
        InGameController.Instance.GameOver();
        
    }
}
