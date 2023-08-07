using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Ball_SpawnState : FSMState<BallSystemVer2>
{
    //private float yOffset;
    public Vector3 lastestPaddlePosition;
    public Vector3 currentPaddlePosition;
    public Vector3 deltaPosition;


    public override void OnEnter()
    {
        //sys.BallDeath();
        lastestPaddlePosition = sys.paddle.spawnPosition;
        sys.transform.position = lastestPaddlePosition + Vector3.up;
        sys.direction1 = new Vector3(0, 6.25f, 0);
        sys.tempX = 0;
    }    
    public override void OnUpdate()
    {
        GetPaddlePosition();
        if (!InGameController.Instance.isGameOver && (Input.GetKey(KeyCode.Space)))
        {
            sys.GotoState(sys.MoveState);
        }
    }
    public void GetPaddlePosition()
    {
        currentPaddlePosition = sys.paddle.transform.position;
        deltaPosition.x = currentPaddlePosition.x - lastestPaddlePosition.x;
        sys.transform.position = new Vector3(deltaPosition.x, currentPaddlePosition.y + 1, currentPaddlePosition.z);
       //sys.transform.position = deltaPosition;
       //Debug.Log("transform ball" + sys.transform.position);
        sys.AngleMoverment();
    }

}
    