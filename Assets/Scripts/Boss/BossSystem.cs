using NaughtyAttributes.Test;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossSystem : FSMSystem,InteractBall
{
    [HideInInspector]
    public BossAttackState AttackState;
    [HideInInspector]
    public BossMoveState MoveState;
    [HideInInspector]
    public BossDeathState DeathState;
    [HideInInspector]
    public BossSpawnState SpawnState;

    [SerializeField]
    private Transform Forward;
    [SerializeField]
    private Transform Anchor;
    [SerializeField]
    public ContactHandle Contact;
    public CircleCollider2D CircleCollider2D;
    [SerializeField] private int hp;
    [SerializeField] private int id;
    [SerializeField] private GameObject core;
    [SerializeField] private GameObject mid;
    [SerializeField] private GameObject crust;
    [SerializeField] private Vector3 postion;
    [SerializeField] private float movespeed = 4f;
    [SerializeField] private float rotationspeed = 0.5f;
    [SerializeField] private float radius = 2.2f;
    [SerializeField] public float attackCooldown = 10f;

    public Vector2 forwardDir { get => (Forward.position - Anchor.position).normalized; }
    public Vector3 moveDir;
    public Vector3 spawnPosition = new Vector3(0, 5, 0);

    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Awake");
        MoveState.Setup(this);
        AttackState.Setup(this);
        DeathState.Setup(this);
        SpawnState.Setup(this);
    }
    private void Start()
    {
        Debug.Log("Start");
        Init();
    }
    private void Init()
    {
        Debug.Log("Init");
        GotoState(SpawnState);
    }
    public void OnContact(RaycastHit2D hit, BallSystemVer2 ball)
    {
        Debug.Log("HIT BOSS");
    }
    // Update is called once per frame
    public Vector3 ClaimPosition(Vector3 vector)
    {
        //vector.x = Mathf.Clamp(transform.position.x, CameraMain.instance.GetLeft(), CameraMain.instance.GetRight());
        //vector.y = Mathf.Clamp(transform.position.y, CameraMain.instance.GetTop(), CameraMain.instance.GetBottom());
        vector.x = Mathf.Clamp(transform.position.x, -9.5f, 9.5f);
        return vector;
    }
    public void Rotation()
    {
        //Debug.Log("BOSS ROTATION");
        core.gameObject.transform.Rotate(0, 0, rotationspeed);
        mid.gameObject.transform.Rotate(0, 0, rotationspeed * -1f);
        crust.gameObject.transform.Rotate(0, 0, rotationspeed * 2f);

    }
    public void BossMoverment()
    {
        //ClaimPosition();
        BossDirection();
        //Debug.Log("BOSS MOVERMENT");
        Vector3 currentPosition = transform.position;
        currentPosition = transform.position + (movespeed * Time.deltaTime * moveDir.normalized);
        //Debug.Log("MoveDire" + moveDir);
        transform.position = new Vector3 (currentPosition.x, 0,0);
    }
    public void BossDirection()
    {
        float right = CameraMain.instance.GetRight();
        float left = CameraMain.instance.GetLeft();
        float tempPosX = transform.position.x;
        //Debug.Log($"right {right} left {left}");
        Vector3 tempDirection;
        if (tempPosX > 10)
        {
            tempDirection = Vector3.Reflect(moveDir, Vector3.left).normalized;
            moveDir = tempDirection;
            transform.position = ClaimPosition(transform.position);
        }
        else if(tempPosX < -10)
        {
            tempDirection = Vector3.Reflect(moveDir, Vector3.right).normalized;
            moveDir = tempDirection;
            transform.position = ClaimPosition(transform.position);

        }
    }
    public void BossCheckHP()
    {
        if (hp < 0) GotoState(DeathState);
    }
    public void Attack()
    {
        GotoState(AttackState);
    }
}
