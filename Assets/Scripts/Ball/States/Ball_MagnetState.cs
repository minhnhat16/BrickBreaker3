using UnityEngine;

public class Ball_MagnetState : FSMState<BallSystemVer2>
{
    private Vector2 currentPaddlePosition;
    private Vector2 lastestPaddlePosition;

    public float distance;

    // Start is called before the first frame update
    public override void OnEnter()
    {
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
    }
    
}
