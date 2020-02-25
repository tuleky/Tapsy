using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLeft : MonoBehaviour, IGestures
{
    //private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;

    [SerializeField] Sprite sprite;
    [SerializeField] Color color;



    private void Update()
    {
        //tap = swipeLeft = false;

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isDragging = true;
                //tap = true;
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
                //Left or Right
                if (x < -2)
                {
                    //swipeLeft = true;
                    gameObject.SetActive(false);
                    GameManager.Instance.CorrectMove();
                }
                else if (x > 3 && GameManager.Instance.difficulty == 1)
                {
                    //swipeRight = true;
                    GameManager.Instance.WrongMove();
                }
            }
            //else
            //{
            //    //// Up or Down
            //    //if (y < 0)
            //    //{
            //    //    swipeDown = true;
            //    //}
            //    //else
            //    //{
            //    //    swipeUp = true;
            //    //}
            //}

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public Color GetColor()
    {
        return color;
    }
}

