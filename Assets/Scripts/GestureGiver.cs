using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GestureGiver : MonoBehaviour
{
    public static GestureGiver Instance;

    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private TextMeshProUGUI holdTimerText;
    [SerializeField] private Image sprite;
    [SerializeField] private Camera cam;

    private List<IGestures> gestureList = new List<IGestures>();

    [SerializeField] private GameObject gesturesParentGameObject;

    private int randomGesture;
    private int previousGesture = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        GetAllGestures();
    }

    private void GetAllGestures()
    {
        foreach (var item in gesturesParentGameObject.GetComponentsInChildren<IGestures>(includeInactive: true))
        {
            gestureList.Add(item);
            //Debug.Log(item);
        }
    }

    public void EnableAllGestures()
    {
        foreach (var item in gestureList)
        {
            item.GetGameObject().SetActive(true);
        }
    }

    public void DisableAllGestures()
    {
        foreach (var item in gestureList)
        {
            item.GetGameObject().SetActive(false);
        }
        RemoveAllGestureProperties();
    }

    //public void GetRandomGesture()  // Gives one random gesture
    //{
    //    //if (Timer.Instance.remainingGameTime > 0)
    //    //{
    //        if (previousGesture == -1)
    //        {
    //            GenerateGesture();
    //        }
    //        else
    //        {
    //            if (previousGesture == randomGesture)
    //            {
    //                GenerateGesture();
    //            }
    //        }
    //        previousGesture = randomGesture;
    //    //}
    //}

    public void GenerateGesture()
    {
        randomGesture = Random.Range(0, gestureList.Count - 1);
        textMesh.text = gestureList[randomGesture].GetGameObject().name;
        sprite.sprite = gestureList[randomGesture].GetSprite();
        cam.backgroundColor = gestureList[randomGesture].GetColor();
        GameManager.Instance.isPressed = true;
        gestureList[randomGesture].GetGameObject().SetActive(true);
    }

    public void RemoveAllGestureProperties()
    {
        //randomGesture = Random.Range(0, gestureList.Count);
        textMesh.text = " ";
        holdTimerText.text = " ";
        sprite.sprite = null;
        cam.backgroundColor = Color.white;
    }

    public IGestures GetCurrentGesture()
    {
        return gestureList[randomGesture];
    }

    //private float GetRandomHoldingTime()
    //{
    //    float randomHoldingTime = Random.Range(1f, 4f);
    //    return randomHoldingTime;
    //}
}
