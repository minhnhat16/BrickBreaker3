using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Ball_MovermentState : FSMState<BallSystemVer2>
{

    public override void OnEnter()
    {

        if(!InGameController.Instance.isTrippleBall)
        {
            sys.moveDir = sys.forwardDir;
        }
       
    }


    public override void OnUpdate()
    {
        sys.transform.rotation = new Quaternion(0, 0, 0, 0);
        sys.GetBallDirection();
        sys.CheckItemEvent();
        sys.CheckCollider();
        sys.BallMoverment();
        sys.BallDeath();
        InGameController.Instance.GameOver();
    }
}
