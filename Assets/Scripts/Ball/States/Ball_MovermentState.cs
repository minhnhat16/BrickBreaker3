using UnityEngine;

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
        sys.transform.rotation = new Quaternion(0, 0, 0, 0);
        //sys.CheckBallAngle();
        sys.GetBallDirection();
        sys.CheckCollider();
        //sys.RandomItem();
        sys.BallMoverment();
        sys.BallDeath();
        
        InGameController.Instance.LevelComplete();
        InGameController.Instance.GameOver();

    }
}
