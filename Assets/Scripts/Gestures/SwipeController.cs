using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }


    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isDragging = true;
                tap = true;
                startTouch = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }

        // Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.GetTouch(0).position - startTouch;
            }
        }


        // Did we cross the deadzone?
        if (swipeDelta.magnitude > 125)
        {
            // Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or Right
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                // Up or Down
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }
}
