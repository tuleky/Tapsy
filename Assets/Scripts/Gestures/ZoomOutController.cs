using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutController : MonoBehaviour, IGestures
{
    [SerializeField] Sprite sprite;
    [SerializeField] Color color;


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

    private void Update()
    {
        if (Input.touchCount == 2 && !GameManager.Instance.isPressed)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            //Debug.Log(difference);
            if (difference < -80)
            {
                gameObject.SetActive(false);
                GameManager.Instance.CorrectMove();
            }
            else if (difference > 60 && GameManager.Instance.difficulty == 1) // If its hard mode
            {
                GameManager.Instance.WrongMove();
                GameManager.Instance.isPressed = true;
            }
        }
    }
}
