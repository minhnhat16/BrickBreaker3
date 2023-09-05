using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class BallSystemVer2 : FSMSystem
{

    [HideInInspector]
    public Ball_MovermentState MoveState;
    [HideInInspector]
    public Ball_SpawnState SpawnState;
    [HideInInspector]
    public Ball_DeathState DeathState;
    [HideInInspector]
    public Ball_MagnetState MagnetState;
    [SerializeField]
    private Transform Forward;
    [SerializeField]
    private Transform Anchor;
    [SerializeField]
    public ContactHandle Contact;
    [SerializeField]
    public Paddle paddle;
    [SerializeField]
    public ContactHandle contactHandler;
    public Vector3 moveDir;
    public Vector3 tempDirection;
    public Vector2 forwardDir { get => (Forward.position - Anchor.position).normalized; }
    public Vector2 hitpoint;
    public Vector3 direction1 = new Vector3(0, 1, 0);

    public bool isLeft = false;
    public bool isRight = false;
    public bool isTop = false;
    public bool onItemPowerUP = false;
    public bool isScaleUp = false;
    public bool isOnMagnet = false;
    public bool isItemTypePower = false;

    public float ballRadius = 0.5f;
    public float castRadius = 0.5f;

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
    private float timeDecrease1 = 0.1f, timeDecrease2 = 0.1f, timeDecrease3 = 0.1f;
    //private float minDuration = 1f, maxDuration = 5f;

    public int maxLives = 1;
    public int currentLive;
    public int hitAngleCount = 0;

    private void Awake()
    {
        BallEvent.onScaleUp.AddListener(ScaleUp);
        BallEvent.onMagnet.AddListener(Magnet);
        BallEvent.onPower.AddListener(Power);
        BallEvent.onReset.AddListener(ResetBall);
        SpawnState.Setup(this);
        MoveState.Setup(this);
        DeathState.Setup(this);
        contactHandler = new ContactHandle();
        contactHandler.contactUnit = null;
        contactHandler.unitType = UnitType.OTHERS;
    }

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
    public void CheckCollider()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, ballRadius,Vector2.zero);
        if (hit.collider != null)
        {
            InteractBall interactBall = hit.collider.GetComponent(typeof(InteractBall)) as InteractBall;
            //Debug.Log("InteractBall" + interactBall);
            if (interactBall != null)
            {
                //Debug.Log("Interac not null");
                if (contactHandler.contactUnit != hit.collider.transform)
                {
                    Debug.Log("HIT" + hit);
                    Debug.Log("Hit" + interactBall);
                    interactBall.OnContact(hit, this);
                    contactHandler.contactUnit = hit.collider.transform;
                    contactHandler.unitType = UnitType.OTHERS;
                    hitpoint = hit.point;
                    Debug.Log("=======HITPOINT======" + hit.point); 

                }
                else
                {
                    Debug.Log("null contact handle");
                    contactHandler.contactUnit = null;
                }
            }

        }
    }
    public void CheckItemEvent()
    {
        //Debug.Log($"scaleUpDuration{InGameController.Instance.scaleUpDuration}" +
        //    $"magnetDuration  {InGameController.Instance.magnetDuration}" +
        //    $"powerDuration {InGameController.Instance.powerDuration}");
        //Debug.Log("Check Item Event");
        if (isScaleUp)
        {
            //Debug.Log("On ScaleUp");

            if (InGameController.Instance.scaleUpDuration <= 0)
            {
                Debug.Log("Scale up duration < 0");
                this.gameObject.transform.GetChild(0).localScale = new Vector2(2f, 2f);
                ballRadius -= ballRadius / 2f;
                castRadius -= castRadius / 2f;
                isScaleUp = false;
            }
        }

        if (isOnMagnet)
        {

            if (InGameController.Instance.magnetDuration <= 0)
            {
                Debug.Log("Magnet duration < 0");

                this.transform.SetParent(BallPoolManager.instance.transform);
                ballSpeed = 15f;
                isOnMagnet = false;
            }
        }

        if (isItemTypePower || onItemPowerUP || InGameController.Instance.isItemTypePower)
        {
            isItemTypePower = true;
            onItemPowerUP = true;
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(41, 130, 252, 255);
            if (InGameController.Instance.powerDuration <= 0)
            {
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                isItemTypePower = false;
                onItemPowerUP = false;

            }
        }

        if (isRight)
        {
            timeDecrease1 -= Time.deltaTime;
            if (timeDecrease1 <= 0)
            {
                isRight = false;
                timeDecrease1 = 0.1f;
            }
        }

        if (isLeft)
        {
            timeDecrease2 -= Time.deltaTime;
            if (timeDecrease2 <= 0)
            {
                isLeft = false;
                timeDecrease2 = 0.1f;
            }
        }

        if (isTop)
        {
            timeDecrease3 -= Time.deltaTime;
            if (timeDecrease3 <= 0)
            {
                isTop = false;
                timeDecrease3 = 0.1f;
            }
        }
    }
    Tween t;
    private void ScaleUp()
    {
        isScaleUp = true;
        InGameController.Instance.scaleUpDuration = 5f;

        if (t == null)
        {
            Debug.Log("On ScaleUp");

            t = this.gameObject.transform.GetChild(0).DOScale(new Vector2(3f, 3f), 0.5f);
            ballRadius += ballRadius / 2f;
            castRadius += castRadius / 2f;
            t.SetAutoKill(false);
        }
        else
        {
            t.Restart();
            t.Play();
        }
    }

    private void Power()
    {
        InGameController.Instance.powerDuration = 7f;
        InGameController.Instance.isItemTypePower = true;
        isItemTypePower = true;
        onItemPowerUP = true;
    }
    private void Magnet()
    {
        InGameController.Instance.magnetDuration = 10f;

        isOnMagnet = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Anchor.position, (Vector2)Anchor.position + (Vector2)moveDir);
        Gizmos.DrawWireSphere(transform.position, ballRadius);
        Gizmos.DrawLine((new Vector2(5.625f, 10f)), (new Vector2(-5.625f, 10f)));
        Gizmos.DrawLine((new Vector2(5.625f, -10f)), (new Vector2(-5.625f, -10f)));
        Gizmos.DrawLine((new Vector2(5.625f, 10f)), (new Vector2(5.625f, -10f)));
        Gizmos.DrawLine((new Vector2(-5.625f, 10f)), (new Vector2(-5.625f, -10f)));
    }
    public void BallMoverment()
    {
        Vector3 currentPosition = transform.position;
        float leftCam = CameraMain.instance.GetLeft();
        float rightCam = CameraMain.instance.GetRight();
        float topCam = CameraMain.instance.GetTop();
        float botCam = CameraMain.instance.GetBottom();
        //float theta = Mathf.PI / 4;
        //float pointX = currentPosition.x + ballRadius * Mathf.Cos(theta);
        //float pointY = currentPosition.y + ballRadius * Mathf.Sin(theta);
        // transform.position = Mathf.Clamp(CameraMain.instance.GetLeft(), CameraMain.instance.GetRight());
        //transform.Translate(ballSpeed * Time.deltaTime * moveDir.normalized);
        if (currentPosition.x <= rightCam && currentPosition.x >= leftCam && currentPosition.y <= topCam - ballRadius - 2f && currentPosition.y >= botCam)
        {
            //Debug.Log("Point X:" + pointX);
            transform.Translate(ballSpeed * Time.deltaTime * moveDir.normalized);
            //RandomItem();

        }
        else if (currentPosition.x > rightCam - ballRadius || currentPosition.x < leftCam + ballRadius)
        {
            currentPosition = transform.position + (ballSpeed * Time.deltaTime * moveDir.normalized);
            currentPosition.x = Mathf.Clamp(currentPosition.x, leftCam + 0.2f, rightCam - 0.2f);
            //Debug.Log("CLAMPED: " + currentPosition.x);
            transform.position = currentPosition;
            //RandomItem();

        }
        else if (currentPosition.y > topCam - ballRadius - 2f)
        {
            currentPosition = transform.position + (ballSpeed * Time.deltaTime * moveDir.normalized);
            currentPosition.y = Mathf.Clamp(currentPosition.y, botCam + 2f, topCam - ballRadius - 2.2f);
            //Debug.Log("CLAMPED: " + currentPosition.y);
            transform.position = currentPosition;
        }
        //RandomItem();

    }
    public void BallDeath()
    {
        if (transform.position.y < CameraMain.instance.GetBottom())
        {
            Debug.Log($"Ball active list {InGameController.Instance.ballActiveList.IndexOf(this)}") ; 
            Debug.Log($"LIVES {InGameController.Instance.lives}");
            GotoState(DeathState);
        }
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
        //  Debug.Log($"Point on ball:({pointX},{pointY})") ;
        if (tempPosX <= left || tempPosX >= right || pointY >= top - 2.2f ||
            (pointX <= left && pointY >= top - 2.2f) || (pointX >= right && pointY >= top - 2.2f))
        {
            if (tempPosY > top - ballRadius - 2.2f)
            {

                //Debug.Log("Hit left");
                tempDirection = Vector3.Reflect(moveDir, Vector3.down).normalized;
                hitAngleCount++;
                CheckBallAngle(Vector3.up);

            }
            else if (tempPosX > right - ballRadius)
            {
                //Debug.Log("Hit right");
                // Debug.Log("tempposX " + tempPosX);

                tempDirection = Vector3.Reflect(moveDir, Vector3.left).normalized;
                // tempDirection = Vector3.Reflect(moveDir, new Vector3(-1, 0.05f, 0));
                hitAngleCount++;
                CheckBallAngle(Vector3.up);

            }
            else if (tempPosX - ballRadius < left)
            {
                //Debug.Log("tempposX " + tempPosX);
                //Debug.Log("Hit top");
                tempDirection = Vector3.Reflect(moveDir, Vector3.right).normalized;
                // tempDirection = Vector3.Reflect(moveDir, new Vector3(1, 0.05f, 0));

                hitAngleCount++;
                CheckBallAngle(Vector3.up);
            }
        }
    }
    public Vector3 RandomVector(float minX, float maxX, float minY, float maxY)
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);

        return new Vector3(randomX, randomY);
    }
    public Vector3 ReflectRandomAngle(int defind)
    {

        switch (defind)
        {

            case 0:
                //RANDOMVECTOR(minX, maxX, minY,maxY)
                //Debug.Log("CASE ANGLE 0:");
                return RandomVector(-1f, 1f, 0f, -1f);
            case 1:
                //Debug.Log("CASE ANGLE 1:");

                return RandomVector(-0.5f, 0.5f, 0f, 1f);
            case 2:
                //Debug.Log("CASE ANGLE 2:");

                return RandomVector(0f, 1f, -1f, 1f);
            case 3:
                //Debug.Log("CASE ANGLE 3:");

                return RandomVector(-1f, 0f, -1f, 1f);
            default:
                //Debug.Log("CASE ANGLE DEFAULT:");
                return Vector3.zero;
        }
    }

    public void CheckBallAngle(Vector3 vector)
    {

        // float dotProduct = Vector3.Dot(moveDir.normalized, vector.normalized);
        //float anglecheck = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

        float anglecheck_no1 = Vector3.Angle(moveDir.normalized, vector.normalized);
        Vector3 cross = Vector3.Cross(moveDir.normalized, vector.normalized);
        if (cross.z < 0)
        {
            anglecheck_no1 = 360f - anglecheck_no1;

        }
        //Debug.Log("Angle Checking NO.2 =======>" + anglecheck_no1);

        if ((anglecheck_no1 >= 0 && anglecheck_no1 <= 10) && (anglecheck_no1 >= 350 && anglecheck_no1 <= 360))
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
    public void BallOnMagnet()
    {
        if (isOnMagnet && (contactHandler.unitType == UnitType.OTHERS))
        {
            GotoState(SpawnState);
        }
    }
    public void BallReflectPaddle()
    {
        //Debug.Log("HIT POINT: " + hitpoint);

        Collider2D collider = paddle.GetComponent<Collider2D>();
        Bounds bounds = collider.bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        float xMoveDir = transform.position.x;
        float yMoveDir = transform.position.y;
        paddle.GetCurrentPosition();
        if (xMoveDir > min.x - ballRadius && xMoveDir < min.x + 1)
        {
            //Debug.Log("hit  left");
            // Debug.Log(xMoveDir);
            xMoveDir = Mathf.Clamp(transform.position.x, min.x - ballRadius, min.x + ballRadius);
            yMoveDir = Mathf.Clamp(transform.position.y, max.y + ballRadius, min.y);
            //Debug.Log(max.y+ " "+ min.y);
            tempDirection = Vector3.Reflect(moveDir, new Vector3(-1, 1));
            moveDir = tempDirection.normalized;
        }
        else if (xMoveDir > max.x - 1 && xMoveDir - ballRadius <= max.x + ballRadius)
        {
            //Debug.Log("hit  right");
            //Debug.Log(xMoveDir);
            xMoveDir = Mathf.Clamp(transform.position.x, max.x - ballRadius, max.x + ballRadius);
            yMoveDir = Mathf.Clamp(transform.position.y, max.y + ballRadius, min.y);
            tempDirection = Vector3.Reflect(moveDir, new Vector3(1, 1));
            moveDir = tempDirection.normalized;
        }
        else if (xMoveDir > min.x + 1 && xMoveDir < min.x + 1.5)
        {
            // Debug.Log("hit half left");
            //Debug.Log(xMoveDir);
            yMoveDir = Mathf.Clamp(transform.position.y, max.y + ballRadius, max.y + ballRadius);
            Vector2 add = new Vector2(-0.5f, 1f);
            tempDirection = Vector3.Reflect(moveDir, add);
            moveDir = tempDirection.normalized;
        }
        else if (xMoveDir < max.x - 1 && xMoveDir > max.x - 1.5)
        {
            //Debug.Log("hit half right");
            //Debug.Log(xMoveDir);
            //yMoveDir = Mathf.Clamp(transform.position.y, -7.25f, -7.30f);
            yMoveDir = Mathf.Clamp(transform.position.y, max.y + ballRadius, max.y + ballRadius);
            Vector2 add = new Vector2(0.5f, 1f);
            tempDirection = Vector3.Reflect(moveDir, add);
            moveDir = tempDirection.normalized;
        }
        else if (xMoveDir < max.x - 1.5 && xMoveDir > min.x + 1.5)
        {
            //Debug.Log("hit half right");
            //Debug.Log(xMoveDir);
            //yMoveDir = Mathf.Clamp(transform.position.y, -7.25f, -7.30f);
            yMoveDir = Mathf.Clamp(transform.position.y, max.y + ballRadius, max.y + ballRadius);
            Vector2 add = Vector2.up;
            tempDirection = Vector3.Reflect(moveDir, add);
            moveDir = tempDirection.normalized;

        }
        else
        {
            //Debug.Log(xMoveDir);
            //yMoveDir = Mathf.Clamp(transform.position.y, -7.25f, -7.30f);
            yMoveDir = Mathf.Clamp(transform.position.y, max.y + ballRadius, max.y + ballRadius);
            moveDir = tempDirection.normalized;
        }
        if (!InGameController.Instance.isOnMagnet)
        {
            transform.position = new Vector3(xMoveDir, yMoveDir);
        }
    }

    public void ResetBall()
    {
        //Debug.Log("Reset Ball");
        tempX = 0;
        tempDirection = Vector3.zero;
        moveDir = Vector3.zero;
        SetMaxLive();
        this.gameObject.transform.GetChild(0).localScale = new Vector2(2f, 2f);
        ballRadius = 0.3f;
        castRadius = 0.3f;
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;

        isOnMagnet = false;
        isItemTypePower = false;
        isScaleUp = false;
        onItemPowerUP = false;
        GotoState(SpawnState);
    }
    public void SetDefaultScale()
    {

    }
    public void SetMaxLive()
    {
        currentLive = maxLives;
    }
    public void DecreaseLive()
    {
        currentLive--;
    }

    public void BallMultiply(BallSystemVer2 ballparent)
    {
        for (int i = 0; i < 2; i++)
        {
            //Debug.Log("BallMultiply" + i);
            BallSystemVer2 ball = BallPoolManager.instance.pool.SpawnNonGravity();
            //Debug.Log("BALL PARENT POSITION" + ballparent.transform.position);
            ball.transform.position = ballparent.transform.position;
            //Debug.Log("ball.transform.position " + ball.transform.position);
            //InGameController.Instance.ballActiveList.Add(ball);
            //Debug.Log("BALL " + ball);
            ball.moveDir = new Vector3(Random.Range(-1, 1), 1);
            if(ball.moveDir == ballparent.moveDir)
            {
                ball.moveDir = Vector3.up;
            }
            ball.moveDir.Normalize();
        }

    }
}

public static class BallEvent
{
    public static UnityEvent onScaleUp = new UnityEvent();
    public static UnityEvent onMagnet = new UnityEvent();
    public static UnityEvent onPower = new UnityEvent();
    public static UnityEvent onReset = new UnityEvent();
}
