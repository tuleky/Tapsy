using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthShow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        UpdateHealthText();
    }

    public void CallUpdateHealthText()
    {
        Invoke("UpdateHealthText", 0.3f);
    }

    public void UpdateHealthText(){
        healthText.text = "Health: " + PlayerPrefs.GetInt("life");
    }
}
