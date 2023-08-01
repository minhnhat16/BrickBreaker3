using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class PaddleMoveState : FSMState<Paddle>
{
    public Vector2 direction;


    public override void OnEnter()
    {
        sys.transform.position = sys.spawnPosition;
        
    }

    public override void OnUpdate()
    {
        MovePaddle();
        sys.CheckItemEvent();
    }

    public void MovePaddle()
    {

        if (InGameController.Instance.isBallDeath)
        {
            sys.ResetPaddle();
            InGameController.Instance.isBallDeath = false;
            sys.tempX = 0;
        }
        else if(!InGameController.Instance.isBallDeath && !InGameController.Instance.isGameOver)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                float tempDirection = -1;
                sys.MoveCalculation(tempDirection);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                float tempDirection = 1;
                sys.MoveCalculation(tempDirection);
            }
            
        }
        
    }

   
  

}
