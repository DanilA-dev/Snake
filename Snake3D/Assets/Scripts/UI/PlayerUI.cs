using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private List<SimpleTweenAnimation> animations = new List<SimpleTweenAnimation>();

    private void OnEnable ()
    {
        ScoreSystem.OnScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        ScoreSystem.OnScoreChanged -= OnScoreChanged;
    }

    private void Start()
    {
        AnimateElements();
    }


    private void OnScoreChanged(int score)
    {
        scoreText.text = score.ToString();
        scoreText.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
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
