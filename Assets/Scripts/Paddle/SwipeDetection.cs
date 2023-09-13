using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float minSwipeDistance = 50.0f; // Adjust this value for your needs

    public void SwipingScreen(Paddle padde)
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("TOUCHED ");
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("GET TOUCHED POSITION START");

                touchStartPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("GET TOUCHED POSITION END");

                touchEndPos = touch.position;
                Vector2 swipeDelta = touchEndPos - touchStartPos;

                // Check if the swipe distance is greater than the minimum required
                if (swipeDelta.magnitude >= minSwipeDistance)
                {
                    // Check the direction of the swipe
                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    {
                        // Horizontal swipe
                        if (swipeDelta.x > 0)
                        {
                            Debug.Log("SWIPPING RIGHT");

                            // Swipe to the right
                            padde.MoveCalculation(1);
                        }
                        else
                        {
                            Debug.Log("SWIPPING LEFT");

                            // Swipe to the left
                            padde.MoveCalculation(-1);

                        }
                    }
                }
            }
        }
    }
    public void SwipingMouse(Paddle padde)
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Debug.Log("TOUCHED ");
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("GET TOUCHED POSITION START");

                touchStartPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("GET TOUCHED POSITION END");

                touchEndPos = touch.position;
                Vector2 swipeDelta = touchEndPos - touchStartPos;

                // Check if the swipe distance is greater than the minimum required
                if (swipeDelta.magnitude >= minSwipeDistance)
                {
                    // Check the direction of the swipe
                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    {
                        // Horizontal swipe
                        if (swipeDelta.x > 0)
                        {
                            Debug.Log("SWIPPING RIGHT");

                            // Swipe to the right
                            padde.MoveCalculation(1);
                        }
                        else
                        {
                            Debug.Log("SWIPPING LEFT");

                            // Swipe to the left
                            padde.MoveCalculation(-1);

                        }
                    }
                }
            }
        }
    }
}
