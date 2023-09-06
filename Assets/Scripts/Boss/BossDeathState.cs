using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossDeathState : FSMState<BossSystem>
{
    // Start is called before the first frame update
    public override void OnEnter()
    {
        sys.gameObject.SetActive(false);
        InGameController.Instance.currentScore += sys.maxHp;
        sys.hub.gameObject.SetActive(false);
    }


    // Update is called once per frame
    public override void OnUpdate()
    {
    }
}
