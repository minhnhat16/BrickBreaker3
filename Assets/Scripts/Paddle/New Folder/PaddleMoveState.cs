using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PaddleMoveState : FSMState<Paddle>
{
    public Vector2 direction;
    public float tempX;

    public override void OnEnter()
    {
        sys.transform.position = sys.spawnPosition;
    }

    public override void OnUpdate()
    {
        MovePaddle();

    }

    private void MovePaddle()
    {
        if (InGameController.Instance.isGameOver)
        {
            sys.ResetPaddle();
            InGameController.Instance.isGameOver = false;
            tempX = 0;
        }
        else
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                float tempDirection = -1;
                MovePaddle(tempDirection);
                Debug.Log(tempX);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                float tempDirection = 1;
                MovePaddle(tempDirection);
                Debug.Log(tempX);
            }
        }
    }

  private void MovePaddle(float xDirection)
    {
       
        sys.rightLimit = sys.cameraMain.GetRight() - sys.paddleLenght /10f;
        sys.leftLimit = sys.cameraMain.GetLeft() + sys.paddleLenght /10f;
        tempX += Time.deltaTime * sys.paddleSpeed * xDirection ;
        tempX = Mathf.Clamp(tempX, sys.leftLimit, sys.rightLimit);
        sys.transform.position = new Vector3(tempX, sys.transform.position.y, sys.transform.position.z);
    }
}
