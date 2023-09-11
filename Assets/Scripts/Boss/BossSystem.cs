using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSystem : FSMSystem, InteractBall
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
    public List<Collider2D> Collider2D;
    public int id;
    public GameObject core;
    public GameObject mid;
    public GameObject crust;
    //[SerializeField] private GameObject hp_bar;

    [SerializeField] private Vector3 postion;
    [SerializeField] private float movespeed = 4f;
    [SerializeField] private float rotationspeed = 0.5f;
    [SerializeField] private float radius = 2.2f;
    [SerializeField] public float attackCooldown = 10f;
    [SerializeField] private BaseView gamePlayView;
    public BossHub hub;
    public Transform anchorHUB;

    public int hp;
    public int maxHp;
    public bool isBossDefeat;
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
    }
    public virtual void Setup()
    {
        Debug.Log("Setup on boss system");
        Debug.Log("GAMEPLAY VIEW " + gamePlayView.gameObject + "anchorHUB" + anchorHUB);
        maxHp = hp;
        hub.Setup(gamePlayView.GetComponent<GamePlayView>().parentBossHub, anchorHUB);
        GotoState(SpawnState);
    }
    private IEnumerator Start()
    {
        Debug.Log("Start");
        ViewManager.Instance.dicView.TryGetValue(ViewIndex.GameplayView, out gamePlayView);
        yield return new WaitUntil(() => gamePlayView != null);
        Setup();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Bounds bounds = Collider2D[0].bounds;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
        Gizmos.DrawWireSphere(transform.position, radius);

    }
    private IEnumerator HitCoolDown()
    {
        if (!InGameController.Instance.isItemTypePower)
        {

            hp -= 100;
            hub.ShowEffect(hp, maxHp);
            yield return new WaitForSeconds(3f);

        }
        else
        {
            hp -= 50;
            hub.ShowEffect(hp, maxHp);
            yield return new WaitForSeconds(4f);

        }
    }
    public void OnContact(RaycastHit2D hit, BallSystemVer2 ball)
    {
        //Debug.Log("HIT BOSS");
        StartCoroutine(HitCoolDown());
        int hitcase;
        Vector2 normalVector;

        //if (ball.transform.position.y < transform.position.y - ball.ballRadius && ball.transform.position.y > transform.position.y - ball.ballRadius)
        //{
        //    Debug.Log("CASE 1 ");

        //    if (ball.transform.position.x > transform.position.x)
        //    {
        //        Debug.Log("RIGHT");
        //        hitcase = 0;
        //        ball.moveDir = Vector2.Reflect(hit.point, new Vector2(-1, -1));
        //        //ball.CheckBallAngle(Vector2.right);
        //        ReflectBoss(ball, hit, hitcase);
        //        Physics2D.IgnoreCollision(hit.collider, Collider2D[0]);

        //    }
        //    else if (ball.transform.position.x < transform.position.x)
        //    {
        //        Debug.Log("LEFT");
        //        hitcase = 1;
        //        ball.moveDir = Vector2.Reflect(hit.point, new Vector2(1, -1));

        //        //ball.CheckBallAngle(Vector2.left);
        //        ReflectBoss(ball, hit, hitcase);

        //        Physics2D.IgnoreCollision(hit.collider, Collider2D[0]);
        //    }
        //}
         if (ball.transform.position.y > transform.position.y)
        {
            Debug.Log("CASE 2 ");
            hitcase = 2;
            normalVector = hit.point + (Vector2)ball.transform.position;
            normalVector.Normalize();
            Debug.DrawLine(hit.point, normalVector, Color.yellow);
            //Debug.Log("NormalVector "+ normalVector);
            if (ball.transform.position.x > transform.position.x)
            {
                Debug.Log("RIGHT");
                hitcase = 0;
                ReflectBoss(ball, hit, hitcase);    
                ball.moveDir = Vector2.Reflect(hit.point, new Vector2(-1, 1)).normalized;
                //ball.CheckBallAngle(Vector2.right);
                Physics2D.IgnoreCollision(hit.collider, Collider2D[0]);

            }
            else if (ball.transform.position.x < transform.position.x)
            {
                Debug.Log("LEFT");
                hitcase = 1;
                ReflectBoss(ball, hit, hitcase);
                ball.moveDir = Vector2.Reflect(hit.point, new Vector2(1, 1)).normalized;

                //ball.CheckBallAngle(Vector2.left);

                Physics2D.IgnoreCollision(hit.collider, Collider2D[0]);
            }
            //ball.moveDir = Vector2.Reflect(-ball.direction1, normalVector);
            //ReflectBoss(ball, hit, hitcase);

            //Physics2D.IgnoreCollision(hit.collider, Collider2D[0]);

        }
        else if (ball.transform.position.y < transform.position.y)
        {
            Debug.Log("CASE 3");

            normalVector = hit.point - (Vector2)ball.transform.position;
            normalVector.Normalize();
            Debug.DrawLine(hit.point, normalVector, Color.yellow);
            //Debug.Log("NormalVector " + normalVector);
            if (ball.transform.position.x > transform.position.x)
            {
                Debug.Log("RIGHT case 3 ");
                hitcase = 2;
                ReflectBoss(ball, hit, hitcase);
                ball.moveDir = Vector2.Reflect(hit.point, new Vector2(1, -1)).normalized;
                //ball.CheckBallAngle(Vector2.right);
                Physics2D.IgnoreCollision(hit.collider, Collider2D[0]);

            }
            else if (ball.transform.position.x < transform.position.x)
            {
                Debug.Log("LEFT");
                hitcase = 3;
                ReflectBoss(ball, hit, hitcase);
                ball.moveDir = Vector2.Reflect(hit.point, new Vector2(1, 1)).normalized;

                //ball.CheckBallAngle(Vector2.left);

                Physics2D.IgnoreCollision(hit.collider, Collider2D[0]);
            }
            //ball.moveDir = Vector2.Reflect(ball.direction1, normalVector);

            //ReflectBoss(ball, hit, hitcase);
            //Physics2D.IgnoreCollision(hit.collider, Collider2D[0]);

        }

    }
    private int ColliderSwitchCase()
    {
        switch (Collider2D[0])
        {
            case CircleCollider2D:
                return 1;
            case PolygonCollider2D:
                if (Collider2D[0].GetComponent<PolygonCollider2D>().points.Length == 3)
                {
                    return 2;
                }
                else if (Collider2D[0].GetComponent<PolygonCollider2D>().points.Length == 6)
                {
                    return 3;
                }
                else { return 4; }
        }
        return 0;
    }
    private void ReflectBoss(BallSystemVer2 ball, RaycastHit2D hit, int hitcase)
    {
        Bounds bounds = Collider2D[0].bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        float x = ball.transform.position.x;
        float y = ball.transform.position.y;
        //x = Mathf.Clamp(ball.transform.position.x, hit.point.x - ball.ballRadius - 0.2f, hit.point.x + ball.ballRadius + 0.2f);
        //y = Mathf.Clamp(ball.transform.position.y, hit.point.y - ball.ballRadius - 0.2f, hit.point.y + ball.ballRadius + 0.2f);
        //ball.transform.position = new Vector3(x, y, 0);

        switch (hitcase)
        {
            case 0:
                Debug.Log("Right 1 " + hit.point);
                x = Mathf.Clamp(ball.transform.position.x, hit.point.x + ball.ballRadius + 0.2f, hit.point.x + ball.ballRadius + 0.3f);
                y = Mathf.Clamp(ball.transform.position.y, hit.point.y + ball.ballRadius + 0.2f, hit.point.y + ball.ballRadius + 0.3f);
                ball.transform.position = new Vector3(x, y, 0);
                break;
            case 1:
                Debug.Log("Left 1 " + hit.point);
                x = Mathf.Clamp(ball.transform.position.x, hit.point.x - ball.ballRadius - 0.2f, hit.point.x + ball.ballRadius + 0.3f);
                y = Mathf.Clamp(ball.transform.position.y, hit.point.y - ball.ballRadius - 0.2f, hit.point.y + ball.ballRadius + 0.3f);
                ball.transform.position = new Vector3(x, y, 0);
                break;
            case 2:
                Debug.Log("right 2 " + hit.point);
                x = Mathf.Clamp(ball.transform.position.x, hit.point.x + ball.ballRadius + 0.2f, hit.point.x + ball.ballRadius + 0.3f);
                y = Mathf.Clamp(ball.transform.position.y, hit.point.y - ball.ballRadius - 0.2f, hit.point.y + ball.ballRadius - 0.3f);
                ball.transform.position = new Vector3(x, y, 0);
                break;
            case 3:
                Debug.Log("left 2 " + hit.point);
                x = Mathf.Clamp(ball.transform.position.x, hit.point.x - ball.ballRadius - 0.2f, hit.point.x - ball.ballRadius - 0.3f);
                y = Mathf.Clamp(ball.transform.position.y, hit.point.y - ball.ballRadius - 0.2f, hit.point.y - ball.ballRadius - 0.3f);
                
                ball.transform.position = new Vector3(x, y, 0);
                break;
        }
        //Debug.Log("BALL POSTION REFLECT BOSS " + ball.transform.position);
        //Debug.Log($"MAX BOUND {max} + MIN BOUND {min}");
    }
    private void ClaimBallPostion(BallSystemVer2 ball, RaycastHit2D hit)
    {

    }
    private void CheckCase(BallSystemVer2 ball)
    {
        Vector3 ballPos = ball.transform.position;

        //if()
    }
    private void ReflectBoss2(BallSystemVer2 ball)
    {
        int colliderCase = ColliderSwitchCase();
        switch (colliderCase)
        {
            case 1: //CIRCLE CASE
                Bounds bounds = Collider2D[0].bounds;
                Vector3 min = bounds.min;
                Vector3 max = bounds.max;
                float x = ball.transform.position.x;
                float y = ball.transform.position.y;
                x = Mathf.Clamp(ball.transform.position.x, min.x - ball.ballRadius - 0.2f, max.x + ball.ballRadius + 0.2f);
                y = Mathf.Clamp(ball.transform.position.y, min.y - ball.ballRadius - 0.2f, max.y + ball.ballRadius + 0.2f);
                ball.transform.position = new Vector3(x, y, 0);
                break;
            case 2:// TRIANGLE CASE
            case 3:// HEX CASE
            default: break;
        }

    }
    // Update is called once per frame
    public Vector3 ClaimPosition(Vector3 vector)
    {
        vector.x = Mathf.Clamp(transform.position.x, CameraMain.instance.GetLeft() + radius - 1f, CameraMain.instance.GetRight() - (radius - 1f));
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
        BossDirection();
        //Debug.Log("BOSS MOVERMENT");
        Vector3 currentPosition = transform.position;
        currentPosition = transform.position + (movespeed * Time.deltaTime * moveDir.normalized);
        //Debug.Log("MoveDire" + moveDir);
        transform.position = new Vector3(currentPosition.x, transform.position.y, 0);
        ClaimPosition(transform.position);
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
        else if (tempPosX < left + radius)
        {
            tempDirection = Vector3.Reflect(moveDir, Vector3.right).normalized;
            moveDir = tempDirection;
            transform.position = ClaimPosition(transform.position);

        }
    }

    public void BossCheckHP()
    {
        if (hp <= 0) GotoState(DeathState);
    }
    public void Attack()
    {
        GotoState(AttackState);
    }
    public void ResetBossHealth()
    {
        hp = maxHp = LoadLevel.instance.Level.bossHP;
        hub.fg_image.fillAmount = 1;
        hub.gameObject.SetActive(true);
    }
    public void ResetPosition()
    {
        transform.position = spawnPosition;
    }
    public void ResetBoss()
    {
        this.gameObject.SetActive(true);
        hub.ResetHub();
        ResetBossHealth();
        ResetPosition();
        GotoState(SpawnState);
    }
    public void TurnOffHub()
    {
        Destroy(gamePlayView.gameObject.GetComponentInChildren<BossHub>());
        Destroy(hub.gameObject);
    }

}
