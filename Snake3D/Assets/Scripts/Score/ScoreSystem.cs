using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ScoreSystem : MonoBehaviour
{
    private int score;

    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnHighScoreChanged;


    #region PROPERTIES

    public int Score { get => score; }

    #endregion

   

    public void AddScore(int amount)
    {
        score += amount;
        if(score >= PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            OnHighScoreChanged?.Invoke(score);
        }
        
        OnScoreChanged?.Invoke(Score);
    }

   
}
