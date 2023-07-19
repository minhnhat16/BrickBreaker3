using Unity.VisualScripting;
using UnityEngine;

public class BallSystemVer2 : FSMSystem
{

    [HideInInspector]
    public Ball_MovermentState MoveState;
    [HideInInspector]
    public Ball_SpawnState SpawnState;
    [HideInInspector]
    public Ball_DeathState DeathState;
    [SerializeField]
    private Transform Forward;
    [SerializeField]
    private Transform Anchor;
    [SerializeField]
    public ContactHandle Contact;
    [SerializeField]
    public Paddle paddle;
    [SerializeField]
    public Vector3 moveDir;
    public Vector3 tempDirection;
    public Vector2 forwardDir { get => (Forward.position - Anchor.position).normalized; }
    public Vector3 direction1 = new Vector3(0, 4, 0);

    public bool isLeft = false;
    public bool isRight = false;
    public bool isTop = false;

    public float ballRadius = 0.5f;
    public float tempDirX;
    public float tempX = 0;
    public float tempY = 0;
    public float temp;
    public float ballSpeed;
    public float maxAngle;
    public float tempCamPosition;
    public float angle;
    public float angleMoveSpeed = 0.2f;
    public float ballMess;
    public float ballForce;
    public float bounceFact = 0.2f;
    public int maxLives;
    public int currentLive;
    public int hitAngleCount = 0;

    private void Awake()
    {
        SpawnState.Setup(this);
        MoveState.Setup(this);
        DeathState.Setup(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        GotoState(SpawnState);
    }
    public void AngleMoverment()
    {
        Debug.DrawRay(transform.position, direction1, Color.blue);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && (transform.position.x > -1.0f))
        {
            tempDirX = 1;
            AngleCalculation(tempDirX);
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (transform.position.x < 1.0f))
        {
            tempDirX = -1;
            AngleCalculation(tempDirX);
        }
    }
    private void AngleCalculation(float tempXDir)
    {
        tempX += angleMoveSpeed * tempXDir;
        tempX = Mathf.Clamp(tempX, CameraMain.instance.GetLeft() - 2, CameraMain.instance.GetRight() + 2);
        temp = (-CameraMain.instance.GetLeft() + CameraMain.instance.GetRight());
        tempY = Mathf.Sqrt((temp * temp) - (tempX * tempX)) - 5;
        direction1 = new Vector3(tempX, tempY, transform.position.z);
        Debug.DrawRay(transform.position, direction1, Color.blue);
        angle = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = Vector3.forward * angle;
    }
    public void ObjecstHitOnRayCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(Anchor.position, (Vector2)moveDir, ballRadius);
        Debug.DrawRay(Anchor.position, (Vector2)Anchor.position - (Vector2)moveDir, Color.red);
        if (hit.collider != null && hit.collider.CompareTag("Paddle"))
        {
            BallReflectPaddle();
        }
        else if (hit.collider != null && hit.collider.CompareTag("Brick"))
        {
            tempDirection = Vector2.Reflect(moveDir, Vector2.up);
            moveDir = tempDirection;
            // Debug.Log("HIT OBJECT ====>" + hitObject);  
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Anchor.position, (Vector2)Anchor.position + (Vector2)moveDir);
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawLine((new Vector2(5.625f , 10f)), (new Vector2(-5.625f, 10f)));
        Gizmos.DrawLine((new Vector2(5.625f, -10f)), (new Vector2(-5.625f, -10f)));
        Gizmos.DrawLine((new Vector2(5.625f, 10f)), (new Vector2(5.625f, -10f)));
        Gizmos.DrawLine((new Vector2(-5.625f, 10f)), (new Vector2(-5.625f, -10f)));



    }
    public void BallMoverment()
    {
        transform.position += ballSpeed * Time.deltaTime * moveDir.normalized;
    }
    public void GetBallDirection()
    {
        float right = CameraMain.instance.GetRight();
        float left = CameraMain.instance.GetLeft();
        float top = CameraMain.instance.GetTop();
        float tempPosX = transform.position.x;
        float tempPosY = transform.position.y;
        float theta = Mathf.PI / 4;
        double pointX = tempPosX + ballRadius * Mathf.Cos(theta);
        double pointY = tempPosY + ballRadius * Mathf.Sin(theta);
        Debug.Log($"Point on ball:({pointX},{pointY})") ;
        if (top -1  <= pointY)
        {
            tempDirection = Vector3.Reflect(moveDir, Vector3.down);
            hitAngleCount++;
            CheckBallAngle(Vector3.up);

        }
        else if (right >= pointX )
        {
            tempDirection = Vector3.Reflect(moveDir, Vector3.left);
            hitAngleCount++;
            CheckBallAngle(Vector3.up);

        }
        else if (left + ballRadius >= pointX)
        {
            tempDirection = Vector3.Reflect(moveDir, Vector3.right);
            hitAngleCount++;
            CheckBallAngle(Vector3.up);
        }
    }
    public Vector3 RandomVector(float minX, float maxX,float minY,float maxY)
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector3(randomX, randomY);
    }
    public Vector3 ReflectRandomAngle( int defind)
    {
        
        switch (defind)
        {
           
            case 0:
                //RANDOMVECTOR(minX, maxX, minY,maxY)
                Debug.Log("CASE ANGLE 0:");
                return RandomVector(-1f, 1f, 0f, -1f); 
            case 1:
                Debug.Log("CASE ANGLE 1:");

                return RandomVector(-0.5f, 0.5f, 0f, 1f);
            case 2:
                Debug.Log("CASE ANGLE 2:");

                return RandomVector(0f, 1f, -1f, 1f);
            case 3:
                Debug.Log("CASE ANGLE 3:");

                return RandomVector(-1f, 0f, -1f, 1f);
            default:
                Debug.Log("CASE ANGLE DEFAULT:");
                return Vector3.zero ;
        }

    }
    public Vector3 ReflectRandomAngleY()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector3 randomVect = new Vector3(randomX, randomY);
        Debug.Log("RandomY" + randomY);
        return randomVect;
    }
    public void CheckBallAngle(Vector3 vector)
    {

        float dotProduct = Vector3.Dot(moveDir.normalized, vector.normalized);
        float anglecheck = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

        float anglecheck_no1 = Vector3.Angle(moveDir.normalized, vector.normalized);
        Vector3 cross = Vector3.Cross(moveDir.normalized, vector.normalized);
        if (cross.z < 0)
        {
            anglecheck_no1 = 360f - anglecheck_no1;

        }
        Debug.Log("Angle Checking NO.2 =======>" + anglecheck_no1);

        if ((anglecheck_no1 >= 0 && anglecheck_no1 <= 10) && (anglecheck_no1 >= 350 && anglecheck_no1 <=360))
        {
            //Debug.Log("Angle Between 170 200");
            moveDir = ReflectRandomAngle(0).normalized;
            hitAngleCount = 0;
        }
        else if (anglecheck_no1 >= 170 && anglecheck_no1 <= 180)
        {
            //Debug.Log("Angle Between 170 190");
            moveDir = ReflectRandomAngle(1).normalized;
        }
        else if (anglecheck_no1 >= 80 && anglecheck_no1 <= 100)
        {
            //Debug.Log("Angle Between 80 100");
            moveDir = ReflectRandomAngle(2).normalized;
            //Debug.Log("Movedir after Y" + moveDir);
            hitAngleCount = 0;
        }
        else if (anglecheck_no1 <= 280 && anglecheck_no1 >= 260)
        {
            Debug.Log("Angle Between 280 260");
            moveDir = ReflectRandomAngle(3).normalized;
        }
        else
        {
            moveDir = tempDirection.normalized;
        }
    }
    public void BallReflectPaddle()
    {
        Collider2D collider = paddle.GetComponent<Collider2D>();
        Bounds bounds = collider.bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        float xMoveDir = transform.position.x;

        CheckBallAngle(Vector2.up);
        if (xMoveDir >= min.x && xMoveDir < min.x + 1)
        {
            tempDirection = Vector3.Reflect(moveDir, new Vector3(-1, 1));
            moveDir = tempDirection.normalized;
        }
        else if (xMoveDir > max.x - 1 && xMoveDir <= max.x)
        {
            tempDirection = Vector3.Reflect(moveDir, new Vector3(1, 1));
            moveDir = tempDirection.normalized;
        }
        else if (xMoveDir > min.x + 1 && xMoveDir < max.x - 1)
        {
            tempDirection = Vector3.Reflect(moveDir, Vector2.up);
            moveDir = tempDirection.normalized;
        }
        else
        {
            moveDir = tempDirection.normalized;
        }
    }
    public void ResetBall()
    {
        tempX = 0;
        tempDirection = Vector3.zero;
        moveDir = Vector3.zero;
        //SetMaxLive();
        GotoState(SpawnState);
    }
}
