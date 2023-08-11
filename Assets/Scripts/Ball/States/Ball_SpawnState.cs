using UnityEngine;

[System.Serializable]
public class Ball_SpawnState : FSMState<BallSystemVer2>
{
    //private float yOffset;
    public Vector3 lastestPaddlePosition;
    public Vector3 currentPaddlePosition;
    public Vector3 deltaPosition;
    public float distance;
    public override void OnEnter()
    {
        float ballPosX = sys.transform.position.x;
        if (sys.isOnMagnet)
        {
            currentPaddlePosition = sys.paddle.transform.position;
            sys.direction1 = new Vector3(0, 6.25f, 0);
            //Vector2 tempVec1 = new Vector2(sys.hitpoint.x, currentPaddlePosition.y);
            if (ballPosX < currentPaddlePosition.x)
            {
                if (ballPosX > 0 && currentPaddlePosition.x > 0)
                {
                    Debug.Log("CASE A1");
                    distance =  ballPosX - currentPaddlePosition.x ;
                }
                else if (sys.hitpoint.x < 0 && currentPaddlePosition.x < 0)
                {
                    Debug.Log("CASE A2");
                    distance =  currentPaddlePosition.x - ballPosX ;
                }
                else if ((sys.hitpoint.x < 0 && currentPaddlePosition.x > 0) || (sys.hitpoint.x > 0 && currentPaddlePosition.x < 0))
                {
                    Debug.Log("CASE A3");

                    distance = ballPosX + currentPaddlePosition.x;
                }
            }
            else if (ballPosX > currentPaddlePosition.x)
            {
                distance = ballPosX - currentPaddlePosition.x;

                if (ballPosX > 0 || currentPaddlePosition.x > 0)
                {
                    Debug.Log("CASE B1");
                    distance =  ballPosX - currentPaddlePosition.x;

                }
                else if (ballPosX < 0 || currentPaddlePosition.x < 0)
                {
                    Debug.Log("CASE B2");
                    distance = - currentPaddlePosition.x + ballPosX;
                }
            }
            Debug.Log("distance" + distance);
            Debug.Log("Lastest position" + lastestPaddlePosition + "currentPaddlePosition.x"
                        + currentPaddlePosition.x + " hitpoint " + sys.hitpoint.x + "Ball position" + sys.transform.position);
            sys.transform.position = new Vector3(currentPaddlePosition.x + distance, currentPaddlePosition.y + 0.7f);
        }
        else if (!Paddle.instance.isTrippleBall)
        {
            //Debug.Log("TRANSFORM ON ENTER NO MAGNET");

            lastestPaddlePosition = sys.paddle.spawnPosition;
            sys.transform.position = lastestPaddlePosition + Vector3.up;
            sys.direction1 = new Vector3(0, 6.25f, 0);
            sys.tempX = 0;
        }
        else
        {
            sys.GotoState(sys.MoveState);
        }

    }
    public override void OnUpdate()
    {
        GetPaddlePosition();
        CalculatePossitionOnMagnet();
        if (!InGameController.Instance.isGameOver && (Input.GetKey(KeyCode.Space)))
        {
            sys.GotoState(sys.MoveState);
        }
    }
    public void GetPaddlePosition()
    {
        currentPaddlePosition = sys.paddle.transform.position;
        if (!sys.isOnMagnet)
        {
            deltaPosition.x = currentPaddlePosition.x - lastestPaddlePosition.x;
            sys.transform.position = new Vector3(deltaPosition.x, currentPaddlePosition.y + 1f, currentPaddlePosition.z);
        }
        sys.AngleMoverment();
    }

    public void CalculatePossitionOnMagnet()
    {
        currentPaddlePosition = sys.paddle.transform.position;

        if (sys.isOnMagnet)
        {
            float temp;
            temp = Mathf.Clamp(distance + currentPaddlePosition.x, CameraMain.instance.GetLeft(), CameraMain.instance.GetRight());
            sys.transform.position = new Vector3(temp, currentPaddlePosition.y + 1f);
        }
    }
    public void OnEnterSpawnMagnet()
    {
        if ((sys.hitpoint.x * currentPaddlePosition.x) > 0)
        {

            if (sys.hitpoint.x > currentPaddlePosition.x)
            {
                Debug.Log("a case");
                if (currentPaddlePosition.x > 0)
                {
                    Debug.Log("a1 case");
                    distance = sys.hitpoint.x - currentPaddlePosition.x;
                }
                else if (currentPaddlePosition.x < 0)
                {
                    Debug.Log("a2 case");
                    distance = (sys.hitpoint.x - currentPaddlePosition.x);
                }
            }
            else if (sys.hitpoint.x < currentPaddlePosition.x)
            {
                Debug.Log("b case");
                distance = currentPaddlePosition.x - sys.hitpoint.x;

                if (currentPaddlePosition.x > 0)
                {
                    Debug.Log("b1 case");

                    distance = -currentPaddlePosition.x + sys.hitpoint.x;
                }
                else if (currentPaddlePosition.x < 0)
                {
                    Debug.Log("b2 case");
                    distance = (-currentPaddlePosition.x + sys.hitpoint.x);
                }
            }

        }
        else if ((sys.hitpoint.x * currentPaddlePosition.x) < 0)
        {

            if (sys.hitpoint.x > currentPaddlePosition.x)
            {
                Debug.Log("c case");
                distance = currentPaddlePosition.x + sys.hitpoint.x;
                if (currentPaddlePosition.x > 0)
                {
                    Debug.Log("c1 case");
                    distance = (currentPaddlePosition.x + sys.hitpoint.x);
                }
                else if (currentPaddlePosition.x < 0)
                {
                    Debug.Log("c2 case");
                    distance = -(sys.hitpoint.x + currentPaddlePosition.x);
                }
            }
            else if (sys.hitpoint.x < currentPaddlePosition.x)
            {
                Debug.Log("d case");
                distance = currentPaddlePosition.x + sys.hitpoint.x;
                if (currentPaddlePosition.x > 0)
                {
                    Debug.Log("d1 case");

                    distance = (currentPaddlePosition.x + sys.hitpoint.x);

                }
                else if (currentPaddlePosition.x < 0)
                {
                    Debug.Log("d2 case");
                    distance = -(sys.hitpoint.x + currentPaddlePosition.x);

                }
            }
        }
    }
}
