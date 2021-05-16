using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text gameTitleText;

    [SerializeField] private Button playButton;
    [SerializeField] private Image blackScreen;


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
        BlackScreenFade(0f, 1f);
        ButtonBounce(playButton);
        TextMove(gameTitleText);
    }


    private void OnScoreChanged(int score)
    {
        scoreText.text = score.ToString();
        scoreText.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
    }

    #region UI Animations

    private void BlackScreenFade(float endValue, float startValue)
    {
        blackScreen.DOFade(endValue, 2f).From(startValue);
    }

    private void ButtonBounce(Button button)
    {
        var endScale = new Vector3(1.1f, 1.1f, 1.1f);

        if(button != null)
        {
            var buttonSeq = DOTween.Sequence();
            buttonSeq.Append(button.transform.DOScale(endScale, 0.4f));
            buttonSeq.Append(button.transform.DOScale(Vector3.one, 0.4f));
            buttonSeq.SetLoops(-1);


        }
    }

    private void TextMove(TMP_Text text)
    {
        var yIndex = 3f;
        var startPos = text.transform.localPosition.y;
        var newYPos = text.transform.localPosition.y + yIndex;

        if(text != null)
        {
            var textSeq = DOTween.Sequence();
            textSeq.Append(text.transform.DOLocalMoveY(newYPos, 2f));
            textSeq.Join(text.transform.DOLocalRotate(new Vector3(0, 0, 1), 2f));
            textSeq.Append(text.transform.DOLocalMoveY(startPos, 2f));
            textSeq.SetLoops(-1, LoopType.Yoyo);
        }
    }

    #endregion
}
