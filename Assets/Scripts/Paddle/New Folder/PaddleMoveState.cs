using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class PaddleMoveState : FSMState<Paddle>
{
    public Vector2 direction;

    Vector3 sliderPos;

    public override void OnEnter()
    {
        sys.transform.position = sys.GetCurrentPosition();
    }

    public override void OnUpdate()
    {
        MovePaddle();
        sliderPos = InGameController.FindAnyObjectByType<SliderButton>().transform.position;
        MovePaddleWithSlider();
        sys.CheckItemEvent();
        //InGameController.Instance.RandomItem();
        //IngameController.instance.main.StartCoroutine(RandomSpawnItem());

    }
    public void MovePaddleWithSlider()
    {
        if (InGameController.Instance.isBallDeath)
        {
            sys.ResetPaddle();
            InGameController.Instance.isBallDeath = false;

        }
        else {
            sys.transform.position = new Vector3(sliderPos.x, sliderPos.y + 1f);
        }

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
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
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
