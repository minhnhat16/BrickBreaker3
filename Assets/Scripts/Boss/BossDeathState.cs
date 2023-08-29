using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossDeathState : FSMState<BossSystem>
{
    // Start is called before the first frame update
    public override void OnEnter()
    {
        sys.gameObject.SetActive(true);
        InGameController.Instance.isLevelComplete = true;
    }


    // Update is called once per frame
    public override void OnUpdate()
    {
    }
}
