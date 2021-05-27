using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    [SerializeField] private List<SimpleTweenAnimation> animations = new List<SimpleTweenAnimation>();


    private void OnEnable ()
    {
        ScoreSystem.OnScoreChanged += OnScoreChanged;
        ScoreSystem.OnHighScoreChanged += OnHighScoreChanged;
    }


    private void OnDisable()
    {
        ScoreSystem.OnScoreChanged -= OnScoreChanged;
        ScoreSystem.OnHighScoreChanged -= OnHighScoreChanged;
    }

    private void Start()
    {
        OnHighScoreChanged(PlayerPrefs.GetInt("HighScore", 0));
        AnimateElements();
    }


    private void OnHighScoreChanged(int score)
    {
        highScoreText.text = "HighScore : " + score;
    }


    private void OnScoreChanged(int score)
    {
        var endScale = 1.2f;
        var duration = 0.1f;

        scoreText.text = score.ToString();
        scoreText.transform.DOScale(endScale, duration).From(1);
    }


    private void AnimateElements()
    {
        if(animations.Count > 0)
        {
            for (int i = 0; i < animations.Count; i++)
            {
                animations[i].Animate();
            }
        }
    }
  
}
