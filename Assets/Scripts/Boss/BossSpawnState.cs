using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossSpawnState : FSMState<BossSystem>
{
    // Start is called before the first frame update
    public override void OnEnter()
    {
        Debug.Log("Enter Spaw");
        sys.transform.position = sys.spawnPosition;
        sys.GotoState(sys.MoveState);
        
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
    }
}
