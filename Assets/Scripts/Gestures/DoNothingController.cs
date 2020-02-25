using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoNothingController : MonoBehaviour, IGestures
{
    [SerializeField] private Color color;
    [SerializeField] private Sprite sprite;

    private float timer;
    private float remainingTime;
    [SerializeField] private TextMeshProUGUI textMesh;

    private void OnEnable()
    {
        timer = Random.Range(0.5f, 1.5f);
        remainingTime = timer;
        textMesh.gameObject.SetActive(true);
        textMesh.text = (Mathf.Round(remainingTime * 1000) / 1000).ToString();
    }

    private void DoNothing()
    {
        Timer.Instance.isTimeStopped = false;
        if (Input.touchCount == 0)
        {
            remainingTime -= Time.deltaTime;
            Timer.Instance.isTimeStopped = true;
            if (remainingTime <= 0)
            {
                Correct();
            }
            textMesh.text = (Mathf.Round(remainingTime * 1000) / 1000).ToString();
        }

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                remainingTime = timer;
                textMesh.text = (Mathf.Round(remainingTime * 1000) / 1000).ToString();
                GameManager.Instance.WrongMove();
                //if (GameManager.Instance.difficulty == 1)
                //{
                //    GameManager.Instance.WrongMove();
                //}
            }
        }
    }

    private void Update()
    {
        DoNothing();
    }

    private void Correct()
    {
        gameObject.SetActive(false);
        textMesh.gameObject.SetActive(false);
        Timer.Instance.isTimeStopped = false;
        GameManager.Instance.CorrectMove();
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
