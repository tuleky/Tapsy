using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefInitializer : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("life"))
        {
            PlayerPrefs.SetInt("life", 1);
        }
    }
}
