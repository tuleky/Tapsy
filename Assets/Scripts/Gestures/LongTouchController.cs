using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LongTouchController : MonoBehaviour, IGestures, IHoldable
{
    private float timer;
    private float remainingTime;
    [SerializeField] Sprite sprite;
    [SerializeField] Color color;
    [SerializeField] TextMeshProUGUI textMesh;

    private void OnEnable()
    {
        timer = Random.Range(0.5f, 2f);
        remainingTime = timer;
        textMesh.gameObject.SetActive(true);
        textMesh.text = (Mathf.Round(remainingTime * 1000) / 1000).ToString();
    }

    //private void OnDisable()
    //{
    //    textMesh.gameObject.SetActive(false);
    //}

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Hold()
    {
        Timer.Instance.isTimeStopped = false;

        if (Input.touchCount == 1)
        {
            // DO Something
            remainingTime -= Time.deltaTime;
            Timer.Instance.isTimeStopped = true;
            if (remainingTime <= 0)
            {
                Correct();
            }
            textMesh.text = (Mathf.Round(remainingTime * 1000) / 1000).ToString();

            if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Timer.Instance.isTimeStopped = false;
                remainingTime = timer;
                textMesh.text = (Mathf.Round(remainingTime * 1000) / 1000).ToString();
                if (GameManager.Instance.difficulty == 1)
                {
                    GameManager.Instance.WrongMove();
                }
            }
        }
        //else if (Input.touches[0].phase == TouchPhase.Canceled && GameManager.Instance.difficulty == 1)
        //{
        //    remainingTime = timer;
        //    textMesh.text = timer.ToString();
        //    GameManager.Instance.WrongMove();
        //}
    }

    private void Correct()
    {
        gameObject.SetActive(false);
        textMesh.gameObject.SetActive(false);
        Timer.Instance.isTimeStopped = false;
        GameManager.Instance.CorrectMove();
    }

    private void Update()
    {
        Hold();
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
