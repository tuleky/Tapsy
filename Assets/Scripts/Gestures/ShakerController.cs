using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerController : MonoBehaviour, IGestures
{
    [SerializeField] Sprite sprite;
    [SerializeField] Color color;



    public GameObject GetGameObject()
    {
        return (gameObject);
    }


    // variable to hold shaking acceleration value
    private Vector3 accelerationDir;

    private void Update()
    {
        // Read new acceleration input from mobile device
        accelerationDir = Input.acceleration;

        // If you shake device hard enough
        if (accelerationDir.sqrMagnitude >= 4f)
        {
            // Do Something...
            gameObject.SetActive(false);
            GameManager.Instance.CorrectMove();
        }
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
