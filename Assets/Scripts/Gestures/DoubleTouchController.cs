using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTouchController : MonoBehaviour, IGestures
{
    [SerializeField] Sprite sprite;
    [SerializeField] Color color;

    public Color GetColor()
    {
        return color;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    GameObject IGestures.GetGameObject()
    {
        return (gameObject);
    }


    private void Update()
    {
        if (Input.touchCount == 2 && !GameManager.Instance.isPressed)
        {
            GameManager.Instance.isPressed = true;
            if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)
            {
                gameObject.SetActive(false);
                GameManager.Instance.CorrectMove();
            }
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

    private void PenaltyCalculation()
    {
        if ((Input.touchCount > 2 || Input.touchCount == 1) && !GameManager.Instance.isPressed && GameManager.Instance.difficulty == 1) // If its in hard mode
        {
            GameManager.Instance.WrongMove();
            GameManager.Instance.isPressed = true;
        }
    }
}
