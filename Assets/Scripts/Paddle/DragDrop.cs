using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UIElements;

public class DragDrop : MonoBehaviour
{
    private bool isDragging;
    [SerializeField] private Paddle paddle;
    private float initialX;
    public void OnMouseDown()
    {
        paddle.GotoState(paddle.MoveState);
        initialX = paddle.transform.position.x;
        isDragging = true;
    }
    public void OnMouseUp()
    {
        isDragging=false;
        paddle.currenPaddlePosition = paddle.transform.position;
    }
    private void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = CameraMain.instance.main.ScreenToWorldPoint(Input.mousePosition) - paddle.transform.position;
            mousePosition.y = Mathf.Clamp(paddle.transform.position.y, 8, 8);
            //mousePosition.x = initialX + (mousePosition.x - initialX);
            //float tempX = mousePosition.x;
            //tempX = Mathf.Clamp(tempX, paddle.leftLimit , paddle.rightLimit );
            mousePosition = new Vector3(mousePosition.x,0);

            transform.Translate(mousePosition);
        }
    }
}
