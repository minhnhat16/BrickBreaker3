using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossAttackState : FSMState<BossSystem>
{
    // Start is called before the first frame update
    public override void OnEnter()
    {
        Debug.Log("BOSS ATTACK");
    }


    // Update is called once per frame
    public override void OnUpdate()
    {
        sys.GotoState(sys.MoveState);
    }
}
