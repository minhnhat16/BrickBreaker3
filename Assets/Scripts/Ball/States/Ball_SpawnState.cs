using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Ball_SpawnState : FSMState<BallSystem>
{
    //private float yOffset;
    public Vector3 lastestPaddlePosition;

    public override void OnEnter()
    {
        lastestPaddlePosition = sys.paddle.transform.position;
        SpawnPosition();
        //sys.transform.position = sys.spawnPosition;
        sys.direction1 = new Vector3(0, 6.25f, 0);
        sys.tempX = 0;
    }    
    public override void OnUpdate()
    {
        //GetCurrenPaddlePosition();
        sys.AngleMoverment();
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            sys.GotoState(sys.MoveState);
        }
    }

    public void SpawnPosition()
    {
        sys.transform.position = lastestPaddlePosition + Vector3.up;
    }
    public void GetCurrenPaddlePosition()
    {
        Vector3 currentPaddlePosition = sys.paddle.transform.position;
        Vector3 deltaPosition = currentPaddlePosition - lastestPaddlePosition;
        sys.transform.position = deltaPosition;
    }
}
    