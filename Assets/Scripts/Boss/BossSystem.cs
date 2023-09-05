using NaughtyAttributes.Test;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public Collider2D CircleCollider2D;
    [SerializeField] private int id;
    [SerializeField] private GameObject core;
    [SerializeField] private GameObject mid;
    [SerializeField] private GameObject crust;
    //[SerializeField] private GameObject hp_bar;

    [SerializeField] private Vector3 postion;
    [SerializeField] private float movespeed = 4f;
    [SerializeField] private float rotationspeed = 0.5f;
    [SerializeField] private float radius = 2.2f;
    [SerializeField] public float attackCooldown = 10f;
    [SerializeField] private GamePlayView gamePlayView;
    public BossHub hub;
    public Transform anchorHUB;   
    
     public int hp;
    public Vector2 forwardDir { get => (Forward.position - Anchor.position).normalized; }
    public Vector3 moveDir;
    public Vector3 spawnPosition;

    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Awake");
        MoveState.Setup(this);
        AttackState.Setup(this);
        DeathState.Setup(this);
        SpawnState.Setup(this);
        //hub.Setup(base, anchorHUB);
    }
    public virtual void Setup()
    {
        gamePlayView = ViewManager.Instance.currentView.GetComponent<GamePlayView>();
        Debug.Log("Setup on boss system");
        Debug.Log("GAMEPLAY VIEW " + gamePlayView.gameObject + "anchorHUB" + anchorHUB);
        hub.Setup(gamePlayView.parentBossHub, anchorHUB);
        GotoState(SpawnState);
    }
    private IEnumerator Start()
    {
        Debug.Log("Start");
        yield return new WaitForSeconds(0);
        Setup();
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Bounds bounds = CircleCollider2D.bounds;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
        Gizmos.DrawWireSphere(transform.position, radius);

    }
    public void OnContact(RaycastHit2D hit, BallSystemVer2 ball)
    {
        Debug.Log("HIT BOSS");
        hp -= 100;
        if (!ball.onItemPowerUP)
        {

            Vector2 normalVector;
            if (ball.transform.position.y< transform.position.y + ball.ballRadius && ball.transform.position.y > transform.position.y - ball.ballRadius)
            {
                Debug.Log("CASE 1 ");
              
                if ( ball.transform.position.x > transform.position.x)
                {
                    Debug.Log("RIGHT");

                    ball.moveDir = Vector2.right;


                }
                else if (ball.transform.position.x < transform.position.x)
                {
                    Debug.Log("LEFT");
                    ball.moveDir = Vector2.left;


                }
                ReflectBoss(ball);
            }
            else if(ball.transform.position.y > transform.position.y) 
            {
                Debug.Log("CASE 2 ");
                normalVector = -hit.point + (Vector2)ball.transform.position;
                normalVector.Normalize();
                Debug.DrawLine(hit.point, normalVector, Color.yellow);
                Debug.Log("NormalVector "+ normalVector);

                ball.moveDir = Vector2.Reflect(-ball.direction1, normalVector);
                ReflectBoss(ball);
            }   
            else if (ball.transform.position.y < transform.position.y)
            {
                Debug.Log("CASE 3");

                normalVector = hit.point - (Vector2)ball.transform.position;
                normalVector.Normalize();
                Debug.DrawLine(hit.point, normalVector, Color.yellow);
                Debug.Log("NormalVector " + normalVector);
                ball.moveDir = Vector2.Reflect(ball.direction1, normalVector);
                ReflectBoss(ball);
            }
            

        }
    }
    private void ReflectBoss(BallSystemVer2 ball)
    {
        Bounds bounds = CircleCollider2D.bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        float x = ball.transform.position.x;
        float y = ball.transform.position.y;
        x = Mathf.Clamp(ball.transform.position.x, min.x - ball.ballRadius - 0.2f, max.x + ball.ballRadius + 0.2f);
        y = Mathf.Clamp(ball.transform.position.y, min.y - ball.ballRadius - 0.2f, max.y + ball.ballRadius + 0.2f);
        ball.transform.position = new Vector3(x,y,0);
        Debug.Log("BALL POSTION REFLECT BOSS " + ball.transform.position);
        Debug.Log($"MAX BOUND {max} + MIN BOUND {min}");
    }
    // Update is called once per frame
    public Vector3 ClaimPosition(Vector3 vector)
    {
        vector.x = Mathf.Clamp(transform.position.x, CameraMain.instance.GetLeft() +radius - 1f, CameraMain.instance.GetRight() -(radius - 1f));
   
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
        transform.position = new Vector3 (currentPosition.x, transform.position.y, 0);
    }
    public void BossDirection()
    {
        float right = CameraMain.instance.GetRight();
        float left = CameraMain.instance.GetLeft();
        float tempPosX = transform.position.x;
        //Debug.Log($"right {right} left {left}");
        Vector3 tempDirection;
        if (tempPosX > right - radius)
        {
            tempDirection = Vector3.Reflect(moveDir, Vector3.left).normalized;
            moveDir = tempDirection;
            transform.position = ClaimPosition(transform.position);
        }
        else if(tempPosX < left + radius)
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
    public void ResetPosition()
    {
        transform.position = spawnPosition;
    }
}