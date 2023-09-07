using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossMoveState : FSMState<BossSystem>
{
    // Start is called before the first frame update
    public override void OnEnter()
    {
        Debug.Log("ON ENTER BOSS MOVE STATE");
        sys.moveDir = sys.forwardDir;

    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        //Debug.Log("ON UPDATE BOSS MOVE STATE");
        //sys.Rotation();
        sys.BossMoverment();
        sys.BossCheckHP();
        //Debug.Log("ATTACK COOLDOWN" + sys.attackCooldown);
        //sys.StartCoroutine(AtackTimer(sys.Attack));
    }
    IEnumerator AtackTimer(Action callback)
    {
        yield return new WaitForSeconds(sys.attackCooldown);
        callback?.Invoke();
    }
   
}
