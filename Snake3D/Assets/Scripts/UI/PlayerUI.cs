using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        ScoreSystem.Instance.OnScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        scoreText.text = score.ToString();
        scoreText.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
    }
}
