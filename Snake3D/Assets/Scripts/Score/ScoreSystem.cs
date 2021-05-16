using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ScoreSystem : MonoBehaviour
{
    private int score;

    public static event Action<int> OnScoreChanged;


    #region PROPERTIES

    public int Score { get => score; }

    #endregion


    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(Score);
    }
}
