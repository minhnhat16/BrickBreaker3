using UnityEngine;

[System.Serializable]
public class Ball_SpawnState : FSMState<BallSystemVer2>
{
    //private float yOffset;
    public Vector3 lastestPaddlePosition;
    public Vector3 currentPaddlePosition;
    public Vector3 deltaPosition;


    public override void OnEnter()
    {
        //sys.BallDeath();
        Debug.Log("TRANSFORM ON ENTER");

        if (sys.isOnMagnet)
        {
            Debug.Log("TRANSFORM ON MAGNET");
            sys.transform.position = new Vector3(sys.hitpoint.x, lastestPaddlePosition.y + 0.5f);
            sys.direction1 = new Vector3(0, 6.25f, 0);
            sys.tempX = 0;
        }
        else if (!Paddle.instance.isTrippleBall)
        {
            Debug.Log("TRANSFORM ON ENTER NO MAGNET");

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
        if (!InGameController.Instance.isGameOver && (Input.GetKey(KeyCode.Space)))
        {
            sys.GotoState(sys.MoveState);
        }
    }
    public void GetPaddlePosition()
    {
        currentPaddlePosition = sys.paddle.transform.position;
        Collider2D collider = sys.paddle.GetComponent<Collider2D>();
        Bounds bounds = collider.bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        if (!sys.isOnMagnet)
        {
            deltaPosition.x = currentPaddlePosition.x - lastestPaddlePosition.x;
            sys.transform.position = new Vector3(deltaPosition.x, currentPaddlePosition.y + 0.7f, currentPaddlePosition.z);
        }
        else
        {
            if (currentPaddlePosition.x < sys.hitpoint.x || currentPaddlePosition.x > 0)
            {
                sys.transform.position = new Vector3(sys.hitpoint.x + currentPaddlePosition.x, currentPaddlePosition.y + 0.7f);

            }
            else if (currentPaddlePosition.x > sys.hitpoint.x || currentPaddlePosition.x < 0)
            {
                sys.transform.position = new Vector3(currentPaddlePosition.x + sys.hitpoint.x, currentPaddlePosition.y + 0.7f);
            }
            else
            {
                sys.transform.position = new Vector3(sys.hitpoint.x + currentPaddlePosition.x, currentPaddlePosition.y + 0.7f);

            }
            Debug.LogWarning("LASTEST PADDLE POSITION" + lastestPaddlePosition.x);
        }
        //sys.transform.position = deltaPosition;
        //Debug.Log("transform ball" + sys.transform.position);
        sys.AngleMoverment();
    }

}
