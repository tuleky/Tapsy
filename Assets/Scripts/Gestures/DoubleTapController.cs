using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapController : MonoBehaviour, IGestures
{
    [SerializeField] private Color color;
    [SerializeField] private Sprite sprite;

    private int touchCount = 0;

    private void Update()
    {
        if (Input.touchCount == 1 && !GameManager.Instance.isPressed)
        {
            GameManager.Instance.isPressed = true;
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchCount++;
            }
        }

        if (touchCount == 2)
        {
            gameObject.SetActive(false);
            GameManager.Instance.CorrectMove();
        }
        //if (Input.touchCount > 0)
        //{
        //    if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
        //    {
        //        GameManager.Instance.isPressed = false;
        //    }
        //}

        PenaltyCalculation();
    }

    private void OnDisable()
    {
        touchCount = 0;
    }

    private void PenaltyCalculation()
    {
        if (Input.touchCount > 1 && !GameManager.Instance.isPressed && GameManager.Instance.difficulty == 1) // If its in hard mode
        {
            GameManager.Instance.WrongMove();
            GameManager.Instance.isPressed = true;
        }
    }


    public Color GetColor()
    {
        return color;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}
