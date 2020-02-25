using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyHolder : MonoBehaviour
{
    public static DifficultyHolder Instance;
    public int difficulty = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void SetDifficulty(int diff)
    {
        difficulty = diff;
        Debug.Log(difficulty);
    }
}
