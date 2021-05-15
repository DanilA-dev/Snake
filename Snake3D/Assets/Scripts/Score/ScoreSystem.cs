using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ScoreSystem : MonoBehaviour
{
    private int score;

    public event Action<int> OnScoreChanged;


    #region PROPERTIES

    public int Score { get => score; }

    #endregion

    #region SINGLETON

    public static ScoreSystem Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    #endregion

    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }
}
