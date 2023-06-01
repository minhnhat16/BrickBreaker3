using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ball_SpawnState : FSMState<BallSystem>
{
    public Vector2 SpawnPosition;
    private float yOffset;
    [SerializeField]
    public Transform _object;
    public Vector3 ParentPosition { get; }

    public override void OnEnter()
    {  
        yOffset = 1;
        //sys.transform.SetParent(sys.paddle.transform);
        SpawnPosition = ParentPosition;
        //sys.transform.position = new Vector2(0, 0);
        sys.transform.position = new Vector2(SpawnPosition.x, SpawnPosition.y + yOffset);
        sys.direction1 = new Vector3(0, 6.25f, 0);
        sys.tempX = 0;
    }

    public override void OnUpdate()
    {   
        sys.AngleMoverment();
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            sys.GotoState(sys.MoveState);
        }
    }

   

}
